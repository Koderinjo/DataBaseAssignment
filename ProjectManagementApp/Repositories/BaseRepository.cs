using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementApp.Repositories
{
    public class BaseRepository<T>(AppDbContext context) where T : class
    {
        protected readonly AppDbContext _context = context;
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task AddAsync(T entity) { await _dbSet.AddAsync(entity); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(T entity) { _dbSet.Update(entity); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(T entity) { _dbSet.Remove(entity); await _context.SaveChangesAsync(); }
    }
}
