using evacuation.Domain.Interfaces;
using evacuation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace evacuation.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _db;
        protected readonly DbSet<T> _set;

        public GenericRepository(AppDbContext db)
        {
            _db = db;
            _set = db.Set<T>();
        }

        public async Task AddAsync(T entity) => await _set.AddAsync(entity);

        public Task<int> CountAsync() => _set.CountAsync();

        public void Delete(T entity) => _set.Remove(entity);

        public async Task<IReadOnlyList<T>> GetAllAsync() => await _set.ToListAsync();


        public async Task<T?> GetByIdAsync(object id) => await _set.FindAsync(id);

        public async Task<IReadOnlyList<T>> GetPagedAsync(int page, int pageSize) => await _set.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        public void Update(T entity) => _set.Update(entity);

        public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate) => await _set.Where(predicate).ToListAsync();

        public IQueryable<T> GetAll() => _set.AsNoTracking();

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate) => _set.AsNoTracking().Where(predicate);
       
    }
}
