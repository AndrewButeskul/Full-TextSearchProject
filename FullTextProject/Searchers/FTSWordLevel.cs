using FullTextProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextProject.Searchers
{
    // Structure of Index
    // {
    //   "news" : [455: [5, 23, 345], 5643: [45, 466, 2023], 4562: [234, 1465], 2102: [68, 378, 978]]
    // {
    public class FTSWordLevel
    {
        //                         Word        DocumentId      List of positions
        private readonly Dictionary<string, Dictionary<int, List<int>>> _index = new();
        private readonly List<string> _content = new();
        private readonly Lexer _lexer = new();

        // Building Inverted Index: Word-level
        public void AddStringToIndex(string text)
        {
            int documentId = _content.Count;

            foreach (var token in _lexer.GetTokensWithPosition(text))
            {
                if (_index.TryGetValue(token.Token, out var set))
                {
                    if (set.TryGetValue(documentId, out var positions))
                    {
                        positions.Add(token.Position);
                    }
                    // in case when we've never met this document, we will add new set with new list of positions
                    else
                    {
                        set.Add(documentId, new List<int>() { token.Position });
                    }
                }
                // if we've never met any document or word, we need add to the index our new dictionary by token
                else
                {
                    _index.Add(token.Token, new Dictionary<int, List<int>>()
                    {
                        [documentId] = new List<int> { token.Position }
                    });
                }
            }
            _content.Add(text);
        }

        public Dictionary<int, List<int>> Search(string word)
        {
            word = word.ToLowerInvariant();

            if (_index.TryGetValue(word, out var set))
                return set;

            return new Dictionary<int, List<int>>();
        }

        public IEnumerable<string> SearchTest(string word)
        {
            var documentList = Search(word);
            foreach (var documentMatches in documentList)
            {
                foreach (var match in documentMatches.Value)
                {
                    yield return Frame.FrameMatch(_content[documentMatches.Key], match);
                }
            }
        }
    }
}
