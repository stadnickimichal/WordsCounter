namespace WordsCounter.Service.DirectoryContentManager
{
    /// <summary>
    /// Mocks functions for file system access
    /// </summary>
    internal class DirectoryContentManagerMock : IDirectoryContentManager
    {
        List<string> DirectoryContnet = new List<string>();
        public List<string> GetAllFilesInDirectory(string path, string[] extensions)
        {
            return DirectoryContnet;
        }

        public bool IsDirectory(string path)
        {
            if (string.IsNullOrEmpty(path) || path == "badDirtectory")
            {
                return false;
            }
            return true;
        }

        public void SetDirectoryContent(List<string> directoryContnet)
        {
            DirectoryContnet = directoryContnet;
        }
    }
}
