using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextProject.Searchers.FragmentSearcher
{
    public class Fragment
    {
        public int Start { get; set; }
        public int End { get; set; }
        public int DocumentId { get; set; }
    }

}
