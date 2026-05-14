using Lezione6.SchoolManager.Data;
using Microsoft.EntityFrameworkCore;

namespace Lezione6.SchoolManager.Repositories
{
    public abstract partial class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly SchoolDbContext _ctx;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(SchoolDbContext ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<T>();
        }

        public void Add(T entity)
        {
            entity.Timestamp = DateTime.Now;
            _dbSet.Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            entity.Timestamp = DateTime.Now;
            await _dbSet.AddAsync(entity);
        }

        public void Delete(int id)
        {
            _dbSet.Remove(GetSingle(id));
        }

        public void DeleteAll()
        {
            _dbSet.RemoveRange(_dbSet);
        }

        /// <summary>
        /// ATTENZIONE: La cancellazione avviene senza richiedere la SaveChanges
        /// </summary>
        /// <returns></returns>
        public async Task DeleteAllAsync()
        {
            await _dbSet.ExecuteDeleteAsync();
        }

        public IEnumerable<T> Filter(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public T? GetSingle(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T?> GetSingleAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public bool SaveChanges()
        {
            return _ctx.SaveChanges() > 0;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _ctx.SaveChangesAsync() > 0;
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
