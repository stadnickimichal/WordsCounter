using System.Diagnostics;
using WordsCounter.Service;
using WordsCounter.Service.FactoryMethods;

Console.WriteLine("Words counter performance test");
Console.WriteLine(Environment.CurrentDirectory);
var directory = Path.Combine(Environment.CurrentDirectory, @"..\..\..\TestFiles");
var factory = new WordsCounterServiceTextFactory();
var counterService = factory.GetWordsCounterServiceInstance();// new ConcurrentWordsCounter(1, 2);

TimeSpan elapsedSum = new TimeSpan();

for (int i = 0; i < 50; i++)
{
    Stopwatch sw = new Stopwatch();
    sw.Start();

    var output = await counterService.CountWordsInDirectory(directory);

    sw.Stop();
    Console.WriteLine($"Elapsed: {sw.Elapsed}. Files processed: {output.FileProcessed}. Words Counted: {output.WordCounts.Select(x => x.Value).Sum()}. Distinct words: {output.WordCounts.Count}");
    elapsedSum += sw.Elapsed;
}

Console.WriteLine($"Medium time fot 50 runs: {elapsedSum / 50} ({elapsedSum.TotalMilliseconds / 50} ms)");
//Console.WriteLine("Words counter resault:");
//foreach(var word in output.WordCounts/*.Where(x => x.Key.Contains('\'') || x.Key.Contains('\"') || x.Key.Contains(',') || x.Key.Contains('.'))*/.OrderBy(x => x.Key))
//{
//    Console.WriteLine($"{word.Key}: {word.Value}");
//}

