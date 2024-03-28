using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextProject.Storage.DocumentStorage
{
    public interface IDocumentStorage
    {
        void AddDocument(string document);
        string GetDocument(int id);
    }
}
