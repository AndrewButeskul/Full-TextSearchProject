using BenchmarkDotNet.Attributes;
using FullTextProject.Searchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextProject.Benchmarks
{
    [MemoryDiagnoser]
    [RankColumn]
    public class SearchBenchmark
    {
        private readonly string[] _dataset;
        private readonly FTSRecordLevel _indexRecordLevel;
       
        public SearchBenchmark() 
        {
            _dataset = ExtractorString.GetDataSet().ToArray();

            _indexRecordLevel = new();

            foreach (var item in _dataset)
            {
                _indexRecordLevel.AddStringToIndex(item);
            }
        }
        // Quantity of words:
        //Tech [616], rich [1108], total [3524], personal[5222], News [14_762]

        [Params("Tech", "total", "News")]
        public string Query {  get; set; }

        // to compare searchers, we'll consider BasicSearch like BaseLine
        [Benchmark(Baseline = true)]
        public void BasicSearch()
        {
            new BasicSearcher().Search(Query, _dataset).ToArray();
        }

        [Benchmark]
        public void FullTextRecordLevelSearch()
        {
            _indexRecordLevel.EfficientSearch(Query).ToArray();
        }
    }
}
