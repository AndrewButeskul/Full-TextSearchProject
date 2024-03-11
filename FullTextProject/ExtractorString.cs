using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextProject
{
    public class ExtractorString
    {
        public static IEnumerable<string> GetDataSet()
        {
            return ReadDataSet("articles_content.csv");
        }
        public static IEnumerable<string> ReadDataSet(string fileName)
        {
            using var reader = new CsvHelper.CsvReader(
                File.OpenText(Path.Combine(@"G:\dataset\articles", fileName)),
                System.Globalization.CultureInfo.InvariantCulture );

            reader.Read();
            reader.ReadHeader();

            while( reader.Read() )
            {
                var content = reader["content"];
                yield return content;
            }
        }
    }
}
