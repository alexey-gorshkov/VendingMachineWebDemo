using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VendingMachine.DAL.Entities;

namespace VendingMachine.DAL.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey: struct
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate = null);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null);

        //Task<TEntity> GetById(TKey id);

        Task<TKey> Create(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(TKey id);
    }
}
