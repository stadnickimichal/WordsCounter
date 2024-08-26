namespace WordsCounter.Service.DirectoryContentManager
{
    internal class DirectoryContentManager : IDirectoryContentManager
    {
        public List<string> GetAllFilesInDirectory(string path, string[] extensions)
        {
            return Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                    .Where(file => extensions.Contains(Path.GetExtension(file).Trim('.')))
                    .ToList();
        }

        public bool IsDirectory(string path)
        {
            return Directory.Exists(path);
        }
    }
}
