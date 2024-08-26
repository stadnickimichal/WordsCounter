using WordsCounter.Service.Models;

namespace WordsCounter.Service
{
    /// <summary>
    /// Service for calculating words in all files in provided directory
    /// </summary>
    public interface IWordsCounterService
    {
        /// <summary>
        /// Calculates words in all files in provided directory
        /// </summary>
        /// <param name="directory">Directory containing files to process</param>
        /// <returns></returns>
        public Task<WordsCounterResponce> CountWordsInDirectory(string directory);
    }
}
