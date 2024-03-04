
using FullTextProject;

var list = ExtractorString.GetDataSet().Take(10_000).ToArray();

var searcher = new BasicSearcher();

searcher.Search("Car", list);

