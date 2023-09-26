using FluentResults;

namespace MovieManager.BLL.Interfaces
{
    public interface IRepository<T>
    {
        Result Add(T entity);
        Result<T> GetById(int id);
        Result<List<T>> List();
        void Update(int id, T entity);
        Result<int> Remove(int id);
    }
}
