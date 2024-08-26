namespace WordsCounter.Service.FactoryMethods
{
    /// <summary>
    /// Factory class Responsible for creating IWordsCounterService
    /// </summary>
    public interface IWordsCounterServiceFactory
    {
        /// <summary>
        /// Returns instance of IWordsCounterService
        /// </summary>
        /// <param name="bostPerformance">Determines if more system resources should be used to speed up calculations</param>
        /// <returns>instance of IWordsCounterService</returns>
        IWordsCounterService GetWordsCounterServiceInstance(bool bostPerformance);
    }
}
