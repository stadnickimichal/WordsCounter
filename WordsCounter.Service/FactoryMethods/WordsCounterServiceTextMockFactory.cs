using WordsCounter.Service.DirectoryContentManager;
using WordsCounter.Service.FileReader;
using WordsCounter.Service.WordCountingServices;

namespace WordsCounter.Service.FactoryMethods
{
    /// <summary>
    /// Factory class Responsible for creating WordsCounterServiceText with mocked IFileReader and DirectoryContentManager
    /// </summary>
    public class WordsCounterServiceTextMockFactory : IWordsCounterServiceFactory
    {
        private FileReaderMock FileReader;
        private DirectoryContentManagerMock DirectoryContentManager;
        public WordsCounterServiceTextMockFactory()
        {
            FileReader = new FileReaderMock();
            DirectoryContentManager = new DirectoryContentManagerMock();
        }

        public IWordsCounterService GetWordsCounterServiceInstance(bool bostPerformance = false)
        {
            var countingService = new WordCountingServicesText(FileReader);
            return new WordsCounterServiceText(countingService, DirectoryContentManager, 1);
        }

        public void SetFileContent(Dictionary<string, string> mockedFiles)
        {
            FileReader.SetFileContent(mockedFiles);
        }

        public void SetDirectoryContent(List<string> directoryContent)
        {
            DirectoryContentManager.SetDirectoryContent(directoryContent);
        }

        public void SetFilingFile(string fileName)
        {
            FileReader.SetFilingFile(fileName);
        }
    }
}
