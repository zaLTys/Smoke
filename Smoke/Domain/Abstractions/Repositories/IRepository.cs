namespace Domain.Abstractions.Repositories
{
    public interface IRepository<T>
    {
        // ToDo: Add Async/Await versions of these methods if necessary
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        T Create(T entity);
        T Update(T entity);
    }
}