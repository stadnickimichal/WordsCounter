namespace WordsCounter.Service.FileReader
{
    internal class FileReaderText : IFileReader
    {
        public string GetFileContent(string fileName)
        {
            using var streamReader = new StreamReader(fileName);
            return streamReader.ReadToEnd();
        }
    }
}
