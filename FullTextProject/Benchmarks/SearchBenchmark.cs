using BenchmarkDotNet.Attributes;
using FullTextProject.Searchers;
using FullTextProject.Searchers.FragmentSearcher;
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
        private readonly FTSWordLevel _indexWordLevel;
        private readonly FragmentSearcher _indexFragment;
       
        public SearchBenchmark() 
        {
            _dataset = ExtractorString.GetDataSet().ToArray();

            // initializing indexes
            _indexRecordLevel = new();
            _indexWordLevel = new();
            _indexFragment = new();

            foreach (var item in _dataset)
            {
                _indexRecordLevel.AddStringToIndex(item);
                _indexWordLevel.AddStringToIndex(item);
                _indexFragment.AddStringToIndex(item);
            }
        }

        
        [Params("electronic", "future", "government")]
        public string Query { get; set; }

        // to compare searchers, we'll consider BasicSearch like '1'
        [Benchmark(Baseline = true)]
        public void BasicSearch()
        {
            new BasicSearcher().Search(Query, _dataset).ToArray();
        }

        [Benchmark]
        public void FullTextRecordLevelSearch()
        {
            _indexRecordLevel.SearchTest(Query).ToArray();
        }

        [Benchmark]
        public void FullTextWordLevelSearch()
        {
            _indexWordLevel.SearchTest(Query).ToArray();
        }

        [Benchmark]
        public void FragmentBasedSearch()
        {
            _indexFragment.SearchTest(Query).ToArray();
        }
    }
}
