using System.Collections.Concurrent;
using WordsCounter.Service.DirectoryContentManager;
using WordsCounter.Service.Models;
using WordsCounter.Service.WordCountingServices;

namespace WordsCounter.Service
{
    /// <summary>
    /// Service for calculating words in all text files in provided directory
    /// </summary>
    internal class WordsCounterServiceText : IWordsCounterService
    {
        private int NumberOfThreads = 1;
        private readonly IWordCountingServices WordCountingServices;
        private readonly IDirectoryContentManager DirectoryContentManager;

        public WordsCounterServiceText(IWordCountingServices wordCountingServices, IDirectoryContentManager directoryContentManager, 
            int? numberOfThreads)
        {
            WordCountingServices = wordCountingServices;
            DirectoryContentManager = directoryContentManager;
            if (numberOfThreads.HasValue && numberOfThreads > 0)
            {
                NumberOfThreads = numberOfThreads.Value;
            }
        }

        public async Task<WordsCounterResponce> CountWordsInDirectory(string directory)
        {
            if(directory == null)
            {
                return ErrorMessage("Directory cant't be null");
            }

            if (!DirectoryContentManager.IsDirectory(directory))
            {
                return ErrorMessage($"{directory} is not a directory");
            }

            var fileList = DirectoryContentManager.GetAllFilesInDirectory(directory, WordCountingServices.CompatibleFileExtensions)
                    .ToList();

            if (fileList.Count == 0)
            {
                return ErrorMessage($"There are no files to read in directory: {directory}");
            }

            var queue = new ConcurrentQueue<string>(fileList);

            var processTaskas = new List<Task>();
            var wordCounts = new ConcurrentDictionary<string, int>();
            var unreadFiles = new ConcurrentBag<UnreadFileInformation>();

            for (int i = 0; i < NumberOfThreads; i++)
            {
                var task = new Task(() => WordCountingServices.ProcessFiles(queue, wordCounts, unreadFiles));
                task.Start();
                processTaskas.Add(task);
            }

            await Task.WhenAll(processTaskas);

            return new WordsCounterResponce()
            { 
                Success = !unreadFiles.Any(),
                FileProcessed = fileList.Count(),
                WordCounts = new Dictionary<string, int>(wordCounts),
                UnreadFiles = new List<UnreadFileInformation>(unreadFiles)
            };
        }

        private WordsCounterResponce ErrorMessage(string message)
        {
            return new WordsCounterResponce()
            {
                Success = false,
                ErrorMessage = message
            };
        }

        //private void ProcessFiles(ConcurrentQueue<FileInformation> files, ConcurrentDictionary<string, int> wordCounts, ConcurrentBag<UnreadFileInformation> unreadFiles)
        //{
        //    while (files.Count > 0)
        //    {
        //        string fileContent;

        //        files.TryDequeue(out var fileInfo);

        //        if(fileInfo == null)
        //        {
        //            return;
        //        }

        //        try
        //        {
        //            using var streamReader = new StreamReader(fileInfo.FileName);
        //            fileContent = streamReader.ReadToEnd();
        //        }
        //        catch (Exception ex)
        //        {
        //            if(fileInfo.RetriesCount < MaxNumberOfRetries)
        //            {
        //                //TODO: delay
        //                fileInfo.RetriesCount++;
        //                files.Enqueue(fileInfo);
        //                return;
        //            }
        //            else
        //            {
        //                unreadFiles.Add(new UnreadFileInformation(fileInfo.FileName, ex));
        //                return;
        //            }
        //        }

        //        fileContent = Regex.Replace(fileContent, "\r\n|\n", " ", RegexOptions.Compiled);
        //        fileContent = Regex.Replace(fileContent, "[^\\p{L}0-9\\'\\s]", "", RegexOptions.Compiled);
        //        //fileContent = Regex.Replace(fileContent, "(\\s|^)(\")", "$1", RegexOptions.Compiled);
        //        //fileContent = Regex.Replace(fileContent, "(\")(\\s|$)", "$2", RegexOptions.Compiled);
        //        fileContent = Regex.Replace(fileContent, "(\\s|^)(\')", "$1", RegexOptions.Compiled);
        //        fileContent = Regex.Replace(fileContent, "(\')(\\s|$)", "$2", RegexOptions.Compiled);
        //        ////fileContent = Regex.Replace(fileContent, "(.?)(\\.)", "$1", RegexOptions.Compiled);
        //        ////fileContent = Regex.Replace(fileContent, "(.?)(,)", "$1", RegexOptions.Compiled);
        //        //fileContent = Regex.Replace(fileContent, "\\.|\\,|\\<|\\>|\\(|\\)|\\:|\\;|\\?|\\!|\\{|\\}", "", RegexOptions.Compiled);

        //        var fileResult = fileContent.Split(' ').ToList();

        //        foreach (var word in CollectionsMarshal.AsSpan(fileResult))
        //        {
        //            var kay = word.Trim().ToLower();

        //            if (wordCounts.ContainsKey(kay))
        //            {
        //                wordCounts[kay]++;
        //            }
        //            else
        //            {
        //                wordCounts[kay] = 1;
        //            }
        //        }
        //    }

        //    return;
        //}
    }
}
