using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VendingMachine.DAL.Data;
using VendingMachine.DAL.Entities;
using VendingMachine.DAL.Interfaces;

namespace VendingMachine.DAL.Repositories
{
    public abstract class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : struct
    {
        private DbSet<TEntity> _dbSet;
        public ApplicationDbContext DbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbSet = dbContext.Set<TEntity>();
            DbContext = dbContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate = null)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (include != null)
            {
                query = include(query);
            }

            return await query.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<TKey> Create(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Update(TEntity entity)
        {
            _dbSet.Update(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(TKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
            await DbContext.SaveChangesAsync();
        }
    }
}
