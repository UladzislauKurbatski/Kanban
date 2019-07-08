using Kanban.DataAccess.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kanban.DataAccess.Implementation.Repositories
{
    public class EFRepository<T> : IRepository<T> where T: class, Interfaces.IIdentity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> DbSet;

        public EFRepository(DbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<T> CreateAsync(T item)
        {
            await DbSet.AddAsync(item);
            await _context.SaveChangesAsync();
            return await DbSet.FindAsync(item.Id);
        }
        
        public async Task<T> UpdateAsync(T item)
        {
            DbSet.Update(item);
            await _context.SaveChangesAsync();
            return await DbSet.FindAsync(item.Id);
        }

        public async Task DeleteAsync(int id)
        {
            var item = await DbSet.FindAsync(id);
            DbSet.Remove(item);
            await _context.SaveChangesAsync();
        }
        
        public IQueryable<T> Query()
        {
            return DbSet.AsQueryable();
        }
    }
}
