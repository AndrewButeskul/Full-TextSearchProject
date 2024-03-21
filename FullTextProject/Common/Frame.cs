using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextProject.Common
{
    public static class Frame
    {
        public static string FrameMatch(string text, int position)
        {
            int startPosition = Math.Max(0, position - 50);
            int endPosition = Math.Min(startPosition + 100, text.Length - 1);

            string result = (startPosition == 0 ? "" : "...")
                + text.Substring(startPosition, endPosition - startPosition)
                + (endPosition == text.Length - 1 ? "" : "...");
            
            // log to console
            //Console.WriteLine(result);

            return result;
        }
    }
}
