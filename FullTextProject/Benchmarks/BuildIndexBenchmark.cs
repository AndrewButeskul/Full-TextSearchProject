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
    public class BuildIndexBenchmark
    {
        private readonly string[] _dataset;
        public BuildIndexBenchmark()
        {
            _dataset = ExtractorString.GetDataSet().ToArray();
        }

        [Benchmark(Baseline = true)]
        public void ReadDataset()
        {
            ExtractorString.GetDataSet().Take(5_000).ToArray();
        }

        [Benchmark]
        public void RecordLevelIndex()
        {
            FTSRecordLevel index = new();
            foreach (var text in _dataset)
                index.AddStringToIndex(text);
        }

        [Benchmark]
        public void WordLevelIndex()
        {
            FTSWordLevel index = new();
            foreach (var text in _dataset)
                index.AddStringToIndex(text);
        }

        [Benchmark]
        public void FragmentBasedIndex()
        {
            FragmentSearcher index = new();
            foreach (var text in _dataset)
                index.AddStringToIndex(text);
        }

        // TODO: Interface 
        //public void BuildIndex<T>(T index) where T : class
        //{
        //    foreach (var text in _dataset)
        //    {

        //    }
        //}
    }
}
