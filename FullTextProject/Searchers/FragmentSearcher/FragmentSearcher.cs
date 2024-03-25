using FullTextProject.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FullTextProject.Searchers.FragmentSearcher
{
    public class FragmentSearcher
    {
        private record IndexItem(int DocumentId, int Position);

        private readonly Lexer _lexer = new();
        private readonly BasicSearcher _basicSearcher = new();

        private readonly Dictionary<string, HashSet<int>> _index = new();
        private readonly List<string> _content = new();
        private readonly List<Fragment> _fragments = new();

        public const int ChunkSize = 64;

        public void AddStringToIndex(string text)
        {
            foreach (var tokenChunk in _lexer.GetTokensWithPosition(text).Chunk(ChunkSize))
            {
                var fragment = new Fragment() { DocumentId = _content.Count, Start = tokenChunk[0].Position, End = tokenChunk[^1].Position };
                foreach (var token in tokenChunk)
                {
                    if (_index.TryGetValue(token.Token, out var set))
                        set.Add(_content.Count);
                    else
                        _index[token.Token] = new HashSet<int>() { _fragments.Count };
                }
                _fragments.Add(fragment);
            }
            _content.Add(text);
        }

        public IEnumerable<string> SearchWord(string word)
        {
            if (!_index.TryGetValue(word, out var set))
                yield break;


            foreach (var doc in set)
            {
                var fragment = _fragments[doc];
                var text = _content[fragment.DocumentId];
                foreach (var match in _basicSearcher.Search(word, text, fragment.Start, fragment.End))
                {
                    yield return match;
                }
            }
        }

        public IEnumerable<string> SearchTest(string word)
            => _basicSearcher.Search(word, SearchWord(word));
    }
}
