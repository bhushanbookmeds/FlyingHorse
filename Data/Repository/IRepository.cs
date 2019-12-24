using System;
using System.Collections.Generic;
using System.Linq;                                        // we define our methods that we require (in IRepository)
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetDbSet();
        TEntity GetByID(object id);
        void AddRange(IEnumerable<TEntity> entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        IEnumerable<TEntity> GetMany(Func<TEntity, bool> where);
        IQueryable<TEntity> GetManyQueryable(Func<TEntity, bool> where);
        TEntity Get(Func<TEntity, Boolean> where);
        int GetCount(Func<TEntity, bool> where);
        void Delete(Func<TEntity, Boolean> where);
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> GetWithInclude(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, params string[] include);
        bool Exists(object primaryKey);
        TEntity GetSingle(Func<TEntity, bool> predicate);
        TEntity GetFirst(Func<TEntity, bool> predicate);
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetByIDAsync(object id);
        Task InsertAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entity);
        Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, Boolean>> where);
        Task<TEntity> GetAsync(Expression<Func<TEntity, Boolean>> where);
        Task<int> GetCountAsync(Expression<Func<TEntity, Boolean>> where);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<bool> ExistsAsync(object primaryKey);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);

    }
}
