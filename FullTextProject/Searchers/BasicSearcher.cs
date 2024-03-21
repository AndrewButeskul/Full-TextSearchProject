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
        public IEnumerable<string> Search(string word, string item)
        {
            int position = 0;
            while (true)
            {
                position = item.IndexOf(word, position);
                if (position >= 0)
                {
                    yield return Frame.FrameMatch(item, position);
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
            foreach (var item in stringsToSearch)
            {
               foreach(var match in Search(word, item))
                {
                    yield return match;
                }
            }
        }
        
        
    }
}
