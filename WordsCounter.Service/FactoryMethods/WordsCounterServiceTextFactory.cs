using WordsCounter.Service.FileReader;
using WordsCounter.Service.WordCountingServices;

namespace WordsCounter.Service.FactoryMethods
{
    /// <summary>
    /// Factory class Responsible for creating WordsCounterServiceText
    /// </summary>
    public class WordsCounterServiceTextFactory : IWordsCounterServiceFactory
    {
        public IWordsCounterService GetWordsCounterServiceInstance(bool bostPerformance = false)
        {
            var fileReader = new FileReaderText();
            var directoryManager = new DirectoryContentManager.DirectoryContentManager();
            var countingService = new WordCountingServicesText(fileReader);
            int threadsNumber = 2;
            if (bostPerformance)
            {
                threadsNumber = 4;
            }

            return new WordsCounterServiceText(countingService, directoryManager, threadsNumber);
        }
    }
}
