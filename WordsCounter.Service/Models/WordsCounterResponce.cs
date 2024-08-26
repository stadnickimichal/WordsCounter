
namespace WordsCounter.Service.Models
{
    public class WordsCounterResponce
    {
        public Dictionary<string, int> WordCounts { get; set; } = new Dictionary<string, int>();
        public int FileProcessed { get; set; }
        public List<UnreadFileInformation> UnreadFiles { get; set; } = new List<UnreadFileInformation> { };
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
