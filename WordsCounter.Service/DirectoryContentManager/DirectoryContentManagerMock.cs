namespace WordsCounter.Service.DirectoryContentManager
{
    /// <summary>
    /// Mocks functions for file system access
    /// </summary>
    internal class DirectoryContentManagerMock : IDirectoryContentManager
    {
        List<string> DirectoryContnet;
        public List<string> GetAllFilesInDirectory(string path, string[] extensions)
        {
            return DirectoryContnet;
        }

        public bool IsDirectory(string path)
        {
            return true;
        }

        public void SetDirectoryContent(List<string> directoryContnet)
        {
            DirectoryContnet = directoryContnet;
        }
    }
}
