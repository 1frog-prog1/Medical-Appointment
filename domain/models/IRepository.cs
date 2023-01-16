namespace domain.models
{
    public interface IRepository<T> : IDisposable where T : class {

        IEnumerable<T> Get();

        void Create(T item);

        void Update(T item);

        void Delete(int id);

        void Save();

    }
}