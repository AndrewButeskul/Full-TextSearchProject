

namespace FullTextProject.Storage.IndexStorage
{
    public interface IIndexStorage
    {
        ISet<int> Get(string word);
        ISet<int> Set(string word, ISet<int> set);
    }
}
