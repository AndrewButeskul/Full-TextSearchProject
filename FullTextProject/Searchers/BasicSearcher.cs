using FullTextProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextProject.Searchers
{
    public class BasicSearcher
    {
        public IEnumerable<string> Search(string word, string line)
        {
            int position = 0;
            while (true)
            {
                position = line.IndexOf(word, position);
                if (position >= 0)
                {
                    yield return Frame.FrameMatch(line, position);
                }
                else
                {
                    break;
                }
                position++;
            }
        }
        public IEnumerable<string> Search(string word, IEnumerable<string> stringsToSearch)
        {
            foreach (var line in stringsToSearch)
            {
               foreach(var match in Search(word, line))
                {
                    yield return match;
                }
            }
        }
        
        
    }
}
