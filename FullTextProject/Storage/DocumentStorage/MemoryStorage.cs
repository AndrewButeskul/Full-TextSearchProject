using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextProject.Storage.DocumentStorage
{
    public class MemoryStorage : IDocumentStorage
    {
        private readonly List<string> _documents = new();
        public void AddDocument(string document)
        {
            _documents.Add(document);
        }

        public string GetDocument(int id)
        {
            return _documents[id];
        }
    }
}
