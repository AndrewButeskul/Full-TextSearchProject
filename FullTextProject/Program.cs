
using FullTextProject;
using BenchmarkDotNet.Running;
using FullTextProject.Benchmarks;

//var list = ExtractorString.GetDataSet().Take(20_000).ToArray();

//var searcher = new BasicSearcher();

//searcher.Search("News", list);

BenchmarkRunner.Run<SearchBenchmark>();

 