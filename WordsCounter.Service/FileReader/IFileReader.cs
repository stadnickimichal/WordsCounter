namespace WordsCounter.Service.FileReader
{
    /// <summary>
    /// Provides functions for file reading
    /// </summary>
    internal interface IFileReader
    {
        string GetFileContent(string fileName);
    }
}
