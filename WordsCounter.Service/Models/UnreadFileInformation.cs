namespace WordsCounter.Service.Models
{
    public class UnreadFileInformation
    {
        public UnreadFileInformation(string fileName, string message)
        { 
            FileName = fileName;
            Message = message;
        }
        public UnreadFileInformation(string fileName, Exception exception)
        {
            FileName = fileName;
            Exception = exception;
            Message = exception.Message;
        }

        public string FileName { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
