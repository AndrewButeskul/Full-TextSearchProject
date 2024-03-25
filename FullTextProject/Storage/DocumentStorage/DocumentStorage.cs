using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextProject.Storage.DocumentStorage
{
    public class DocumentStorage : MemoryStorage
    {
        private readonly string _path;
        private readonly List<int> _documentsPositions = new();

        public string ContentFile => Path.Combine(_path, ".content");
        public string HeaderFile => Path.Combine(_path, ".headcon");

        public DocumentStorage(string path)
        {
            _path = path;
            Initialize();
        }

        private void Initialize()
        {
            if (!File.Exists(HeaderFile))
            {
                using var file = File.Create(HeaderFile);
                return;
            }

            using var reader = new BinaryReader(File.OpenRead(HeaderFile));
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                _documentsPositions.Add(reader.ReadInt32());
            }
        }

        public void AddDocument(string document)
        {
            using var contentWriter = new BinaryWriter(File.Open(ContentFile, FileMode.Append));

            int position = (int)contentWriter.BaseStream.Position;
            _documentsPositions.Add(position);
            contentWriter.Write(document);

            using var headerWriter = new BinaryWriter(File.Open(HeaderFile, FileMode.Append));
            headerWriter.Write(position);
        }

        public string GetDocument(int id)
        {
            int position = _documentsPositions[id];

            using var reader = new BinaryReader(File.OpenRead(ContentFile));
            reader.BaseStream.Position = position;

            return reader.ReadString();
        }
    }
}
