namespace WordsCounter.Service.FileReader
{
    /// <summary>
    /// Mocks functions for file reading
    /// </summary>
    internal class FileReaderMock : IFileReader
    {
        private Dictionary<string, string> MockedFiles = new Dictionary<string, string>();
        private List<string> FailingFiles = new List<string>();
        public string GetFileContent(string fileName)
        {
            if (FailingFiles.Contains(fileName))
            {
                throw new Exception($"{fileName} exception!");
            }
            return MockedFiles[fileName];
        }

        public void SetFileContent(Dictionary<string, string> mockedFiles)
        {
            MockedFiles = mockedFiles;
        }

        public void SetFilingFile(string fileName)
        {
            FailingFiles.Add(fileName);
        }
    }
}
