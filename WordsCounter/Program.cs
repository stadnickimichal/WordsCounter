using System.Diagnostics;
using WordsCounter.Service;
using WordsCounter.Service.FactoryMethods;

Console.WriteLine("Welcome to words counter, using this tool you can count occurance of words in all text file in provided directory");
Console.WriteLine("Enter directory to process:");
var directory = Console.ReadLine();

var factory = new WordsCounterServiceTextFactory();
var counterService = factory.GetWordsCounterServiceInstance();

var output = await counterService.CountWordsInDirectory(directory);
Console.WriteLine("Words counter resault:");
foreach (var word in output.WordCounts.OrderBy(x => x.Key))
{
    Console.WriteLine($"{word.Key}: {word.Value}");
}