using System.Collections.Concurrent;
using WordsCounter.Service.Models;

namespace WordsCounter.Service.WordCountingServices
{
    /// <summary>
    /// Represents class that calculetes words in provided files
    /// </summary>
    internal interface IWordCountingServices
    {
        /// <summary>
        /// Compatibil Extension for file calculation
        /// </summary>
        string[] CompatibleFileExtensions { get; }
        /// <summary>
        /// Calculates words in files
        /// </summary>
        /// <param name="files">Queue containing paths to files to be processed</param>
        /// <param name="wordCounts">Dictionary where all found words will be added</param>
        /// <param name="unreadFiles">List of all files that could not be read</param>
        void ProcessFiles(ConcurrentQueue<string> files, ConcurrentDictionary<string, int> wordCounts, 
            ConcurrentBag<UnreadFileInformation> unreadFiles);
    }
}
