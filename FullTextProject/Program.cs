
using FullTextProject;

var list = ExtractorString.GetDataSet().Take(20_000).ToArray();

var searcher = new BasicSearcher();

searcher.Search("News", list);

