using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextProject
{
    public class BasicSearcher
    {
        public void Search(string word, IEnumerable<string> stringsToSearch)
        {
            foreach(var item in stringsToSearch)
            {
                if (item.Contains(word, StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}
