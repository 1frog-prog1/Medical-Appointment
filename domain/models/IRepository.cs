namespace domain.models
{
    public interface IRepository<T>  {

        Task<List<T>> getAll();

        void create(T item);

        Task<T> update(T item);

        Task<bool> delete(int id);
    }
}