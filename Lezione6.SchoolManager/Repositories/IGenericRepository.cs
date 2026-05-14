using Lezione6.SchoolManager.Data;

namespace Lezione6.SchoolManager.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        void DeleteAll();
        IEnumerable<T> GetAll();
        T? GetSingle(int id);
        bool SaveChanges();
        IEnumerable<T> Filter(Func<T, bool> predicate);

        Task AddAsync(T entity);
        Task DeleteAllAsync();
        Task<T?> GetSingleAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
