using FullTextProject;
using FullTextProject.Searchers;
using BenchmarkDotNet.Running;
using FullTextProject.Benchmarks;

//var dataset = ExtractorString.GetDataSet().Take(5000).ToArray();
//string searchedWord = "government";

// ---------------------------------------------------------------

//TestBasicSearch(dataset, searchedWord);
//TestFTSRecordLevel(dataset, searchedWord);
//TestFTSWordLevel(dataset, searchedWord);

// ---------------------------------------------------------------

BenchmarkRunner.Run<SearchBenchmark>();

// ---------------------------------------------------------------
static void TestFTSWordLevel(string[] dataset, string searchedWord)
{
    var wordLevel = new FTSWordLevel();

    foreach (var item in dataset)
    {
        wordLevel.AddStringToIndex(item);
    }

    var resultList = wordLevel.SearchTest(searchedWord).ToArray();

    Console.WriteLine($"Count: {resultList.Count()}");
}
static void TestFTSRecordLevel(string[] dataset, string searchedWord)
{
    var recordLevel = new FTSRecordLevel();

    foreach (var item in dataset)
    {
        recordLevel.AddStringToIndex(item);
    }

    var resultList = recordLevel.SearchTest(searchedWord).ToArray();

    Console.WriteLine($"Count: {resultList.Count()}");
}
static void TestBasicSearch(string[] dataset, string searchedWord)
{
    var basicSearcher = new BasicSearcher();

    var resultList = basicSearcher.Search(searchedWord, dataset).ToArray();

    Console.WriteLine($"Count: {resultList.Count()}");
}
