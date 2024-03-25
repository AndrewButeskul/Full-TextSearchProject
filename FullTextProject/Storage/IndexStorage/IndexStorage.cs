

namespace FullTextProject.Storage.IndexStorage
{
    public class IndexStorage : IIndexStorage
    {
        private readonly string _path;
        public IndexStorage(string path)
        {  
            _path = path;
        }
        public ISet<int> Get(string word)
        {
            var path = GetName(word);

            var resultSet = new SortedSet<int>();
            
            if(!File.Exists(path))
            {
                return resultSet;
            }

            using var reader = new BinaryReader(File.OpenRead(path));
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                resultSet.Add(reader.ReadInt32());
            }
            return resultSet;
        }

        public ISet<int> Set(string word, ISet<int> set)
        {
            var path = GetName(word);

            var directory = Path.GetDirectoryName(path);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var writer = new BinaryWriter(File.OpenWrite(path));

            foreach (var item in set)
            {
                writer.Write(item);
            }

            return set;
        }

        // Using File System
        // c:/index/
        // c:/index/m/o/n/d/a/y.ix

        private string GetName(string word)
        {
            var parts = new List<string>() { _path };
            parts.AddRange(word.Select(x => x.ToString())); // add character by characher

            parts[^1] += ".ix";
            var path = Path.Combine(parts.ToArray());

            return path;
        }
    }
}
