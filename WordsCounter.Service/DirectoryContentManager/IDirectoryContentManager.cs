namespace WordsCounter.Service.DirectoryContentManager
{
    /// <summary>
    /// Provides functions for file system access
    /// </summary>
    internal interface IDirectoryContentManager
    {
        List<string> GetAllFilesInDirectory(string path, string[] extensions);
        bool IsDirectory(string path);
    }
}
