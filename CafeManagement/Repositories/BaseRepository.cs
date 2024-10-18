using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly CafeManagementDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(CafeManagementDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

    }
}
