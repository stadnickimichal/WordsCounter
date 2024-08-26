using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using WordsCounter.Service.FileReader;
using WordsCounter.Service.Models;

namespace WordsCounter.Service.WordCountingServices
{
    /// <summary>
    /// Represents class that calculetes words in provided text files
    /// </summary>
    internal class WordCountingServicesText : IWordCountingServices
    {
        private readonly IFileReader FileReader;

        public WordCountingServicesText(IFileReader fileReader) 
        {
            FileReader = fileReader;
            CompatibleFileExtensions = new string[]
            {
                "txt"
            };
        }

        public string[] CompatibleFileExtensions { get; private set; }

        public void ProcessFiles(ConcurrentQueue<string> files, ConcurrentDictionary<string, int> wordCounts, ConcurrentBag<UnreadFileInformation> unreadFiles)
        {
            while (files.Count > 0)
            {
                string fileContent;

                files.TryDequeue(out var fileName);
                if (fileName == null)
                {
                    return;
                }

                try
                {
                    fileContent = FileReader.GetFileContent(fileName);
                }
                catch (Exception ex)
                {
                    unreadFiles.Add(new UnreadFileInformation(fileName, ex));
                    return;
                }

                fileContent = Regex.Replace(fileContent, "\r\n|\n", " ", RegexOptions.Compiled);
                //Removing all not alphanumerical characters excpet for ' as it can be a part of a whol word (e.g. don't)
                fileContent = Regex.Replace(fileContent, "[^\\p{L}0-9\\'\\s]", "", RegexOptions.Compiled);
                // removing all ' that are on the end or begining of the word as a quotation signs
                fileContent = Regex.Replace(fileContent, "(\\s|^)(\')", "$1", RegexOptions.Compiled);
                fileContent = Regex.Replace(fileContent, "(\')(\\s|$)", "$2", RegexOptions.Compiled);

                var fileResult = fileContent.Split(' ').ToList();

                foreach (var word in CollectionsMarshal.AsSpan(fileResult))
                {
                    var kay = word.Trim().ToLower();

                    if (string.IsNullOrWhiteSpace(kay))
                    {
                        continue;
                    }

                    if (wordCounts.ContainsKey(kay))
                    {
                        wordCounts[kay]++;
                    }
                    else
                    {
                        wordCounts[kay] = 1;
                    }
                }
            }

            return;
        }
    }
}
