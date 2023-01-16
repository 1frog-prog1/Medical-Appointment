namespace domain.models
{
    public interface IRepository<T> : IDisposable where T : class {

        IEnumerable<T> Get();

        void create(T item);

        void update(T item);

        void delete(int id);

        void save();

    }
}