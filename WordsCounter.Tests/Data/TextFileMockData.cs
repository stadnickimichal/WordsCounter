using System.Collections;
using WordsCounter.Service.Models;

namespace WordsCounter.Tests.Data
{
    internal class TextFileMockData : IEnumerable<object[]>
    {
        private readonly List<object[]> Data = new List<object[]>();

        public TextFileMockData()
        {
            Data.Add(TwoFilesTest_TestData());
            Data.Add(FileWithSpecialCharacters_TestData());
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private object[] TwoFilesTest_TestData() =>
            new object[]
            {
                new Dictionary<string, string>
                {
                    { "testFile1" , "Ala Ma kota"},
                    { "testFile2" , "Kot ma ale"},
                },
                new List<string>
                {
                    "testFile1", "testFile2"
                },
                new WordsCounterResponce()
                {
                     FileProcessed = 2,
                     WordCounts = new Dictionary<string, int>
                     {
                         { "ala", 1 },
                         { "ale", 1 },
                         { "ma", 2 },
                         { "kota", 1 },
                         { "kot", 1 },
                     },
                     Success = true
                }
            };

        private object[] FileWithSpecialCharacters_TestData() => 
            new object[]
            {
                new Dictionary<string, string>
                {
                    { "testFile1" , "Test! test? tes't TEST."},
                    { "testFile2" , @"'słowo to słowo'"},
                },
                new List<string>
                {
                    "testFile1", "testFile2"
                },
                new WordsCounterResponce()
                {
                     FileProcessed = 2,
                     WordCounts = new Dictionary<string, int>
                     {
                         { "test", 3 },
                         { "tes't", 1 },
                         { "słowo", 2 },
                         { "to", 1 },
                     },
                     Success = true
                }
            };
    }
}
