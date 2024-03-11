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
                Console.WriteLine(new string('-', 10) + " Docs doesn't have searched word " + new string('-', 50));
                int position = 0;
                while (true)
                {
                    position = item.IndexOf(word, position);
                    if(position >= 0 )
                    {
                        Console.WriteLine(FrameMatch(item, position));
                    }
                    else { break; }
                    position++;
                }
            }
        }
        /// <summary>
        /// Frame searched word in periods
        /// </summary>
        /// <param name="word">Searched word</param>
        /// <param name="text">Content</param>
        /// <returns></returns>
        private string FrameMatch(string text, int position)
        {
            int startPosition = Math.Max(0, position - 50);
            int endPosition = Math.Min(startPosition + 100, text.Length - 1);

            return (startPosition == 0 ? "" : "...") 
                + text.Substring(startPosition, endPosition - startPosition) 
                + (endPosition == text.Length - 1 ? "" : "...");
        } 
    }
}
