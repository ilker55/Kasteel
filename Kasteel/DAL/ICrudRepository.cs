namespace Kasteel.DAL
{
    public interface ICrudRepository<T>
    {
        Task<List<T>> GetAll();
        ValueTask<T?> GetByID(int id);
        Task Insert(T model);
        void Update(T model);
        Task<bool> Delete(int id);
        Task Save();
    }
}
