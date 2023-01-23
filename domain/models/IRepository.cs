namespace domain.models
{
    public interface IRepository<T>  {

        IEnumerable<T> getAll();

        void create(T item);

        T update(T item);

        bool delete(int id);
    }
}