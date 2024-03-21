using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FullTextProject.Common;

namespace FullTextProject.Searchers
{
    // Structure of Index
    // {
    //   "news" : [455, 5643, 4562, 2102]
    // {
    public class FTSRecordLevel
    {
        private readonly Dictionary<string, HashSet<int>> _index = new Dictionary<string, HashSet<int>>();
        private readonly List<string> _content = new List<string>();
        private readonly Lexer _lexer = new();
        private readonly BasicSearcher _searcher = new BasicSearcher();
        public FTSRecordLevel() { }

        // Building Inverted Index: Record-level
        public void AddStringToIndex(string text)
        {
            int documentId = _content.Count;

            foreach (var token in _lexer.GetTokens(text))
            {
                if (_index.TryGetValue(token, out var set))
                {
                    set.Add(documentId);
                }
                else
                {
                    set = new HashSet<int>() { documentId };
                    _index.Add(token, set);
                }
            }
            _content.Add(text);
        }

        public IEnumerable<int> Search(string word)
        {
            word = word.ToLowerInvariant();

            if (_index.TryGetValue(word, out var set))
                return set;

            return Enumerable.Empty<int>();
        }

         /// <summary>
        /// Using BasicSearcer finds searched words only among documents that have these words
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public IEnumerable<string> SearchTest(string word)
        {
            var documentList = Search(word);
            foreach (var documentId in documentList)
            {
                foreach (var match in _searcher.Search(word, _content[documentId]))
                {
                    yield return match;
                }
            }

        }
    }
}
