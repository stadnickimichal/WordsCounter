using WordsCounter.Service.FactoryMethods;
using WordsCounter.Service.Models;
using WordsCounter.Tests.Data;

namespace WordsCounter.Tests
{
    public class WordCounterService_Tests
    {
        [Theory, ClassData(typeof(TextFileMockData))]
        public async void ProcessFiles_TextFile_Success(Dictionary<string, string> mockedFiles, List<string> directoryContent, WordsCounterResponce expectedResponce)
        {
            var factory = new WordsCounterServiceTextMockFactory();
            factory.SetFileContent(mockedFiles);
            factory.SetDirectoryContent(directoryContent);
            var wordsCounter = factory.GetWordsCounterServiceInstance();
            var responce = await wordsCounter.CountWordsInDirectory("");

            CompareWordsCounterResponce(responce, expectedResponce);
        }

        [Fact]
        public async void ProcessFiles_TextFile_OneFileInaccessible_Fails()
        {
            var factory = new WordsCounterServiceTextMockFactory();
            factory.SetFileContent(new Dictionary<string, string>
                {
                    { "testFile1" , "Ala Ma kota"},
                    { "testFile2" , "Kot ma ale"},
                });
            factory.SetDirectoryContent(new List<string>
                {
                    "testFile1", "testFile2"
                });
            factory.SetFilingFile("testFile1");
            var wordsCounter = factory.GetWordsCounterServiceInstance();
            var responce = await wordsCounter.CountWordsInDirectory("");

            Assert.True(responce.Success == false);
            Assert.True(responce.FileProcessed == 2);
            Assert.True(responce.UnreadFiles?.Count == 1);
            Assert.True(responce.UnreadFiles[0].Message == "testFile1 exception!");
            Assert.True(responce.UnreadFiles[0].FileName == "testFile1");
            Assert.True(responce.UnreadFiles[0].Exception != null);
        }

        private void CompareWordsCounterResponce(WordsCounterResponce responce1,  WordsCounterResponce responce2) 
        {
            Assert.True(responce1.Success == responce2.Success);
            Assert.True(responce1.FileProcessed == responce2.FileProcessed);
            Assert.True(responce1.UnreadFiles?.Count == responce2.UnreadFiles?.Count);

            foreach (var item in responce1.WordCounts)
            {
                Assert.True(responce2.WordCounts.ContainsKey(item.Key) && responce2.WordCounts[item.Key] == item.Value);
            }

            if (responce1.UnreadFiles != null)
            {
                foreach (var item in responce1.UnreadFiles)
                {
                    Assert.True(responce1.UnreadFiles.Any(f => f.FileName == item.FileName && f.Message == f.Message));
                }
            }
        }
    }
}