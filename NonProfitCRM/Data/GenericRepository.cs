using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NonProfitCRM.Models;

namespace NonProfitCRM.Data
{
    /// <summary>
    /// Generic Repository class for Entity Operations
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity> where TEntity : class
    {
        #region Private member variables...
        internal DB_3221_crmContext Context;
        internal DbSet<TEntity> DbSet;
        #endregion

        #region Public Constructor...
        /// <summary>
        /// Public Constructor,initializes privately declared local variables.
        /// </summary>
        /// <param name="context"></param>
        public GenericRepository(DB_3221_crmContext context)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
        }
        #endregion

        #region Public member methods...


        public virtual IQueryable<TEntity> GetDbSet()
        {
            return DbSet;
        }

        /// <summary>
        /// generic Get method for Entities
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get()
        {
            IQueryable<TEntity> query = DbSet;
            return query.ToList();
        }

        /// <summary>
        /// Generic get method on the basis of id for Entities.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetByID(object id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// generic Insert method for the entities
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        /////// <summary>
        /////// generic bulk insert method for the entities
        /////// </summary>
        /////// <param name="entity"></param>
        ////public virtual void AddRangeAsync(IEnumerable<TEntity> entity)
        ////{
        ////    DbSet.AddRangeAsync(entity);
        ////}

        /// <summary>
        /// generic bulk insert method for the entities
        /// </summary>
        /// <param name="entity"></param>
        public virtual void AddRange(IEnumerable<TEntity> entity)
        {
            DbSet.AddRange(entity);
        }


        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="entityToDelete"></param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// Generic update method for the entities
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        /// <summary>
        /// generic method to get many record on the basis of a condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetMany(Func<TEntity, bool> where)
        {
            return DbSet.Where(where).ToList();
        }

        /// <summary>
        /// generic method to get many record on the basis of a condition but query able.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetManyQueryable(Func<TEntity, bool> where)
        {
            return DbSet.Where(where).AsQueryable();
        }

        /// <summary>
        /// generic get method , fetches data for the entities on the basis of condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public TEntity Get(Func<TEntity, Boolean> where)
        {
            return DbSet.Where(where).FirstOrDefault<TEntity>();
        }

        /// <summary>
        /// generic method to get count
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual int GetCount(Func<TEntity, bool> where)
        {
            return DbSet.Where(where).Count();
        }

        /// <summary>
        /// generic delete method , deletes data for the entities on the basis of condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public void Delete(Func<TEntity, Boolean> where)
        {
            IQueryable<TEntity> objects = DbSet.Where<TEntity>(where).AsQueryable();
            foreach (TEntity obj in objects)
                DbSet.Remove(obj);
        }

        /// <summary>
        /// generic method to fetch all the records from db
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        /// <summary>
        /// Inclue multiple
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetWithInclude(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = this.DbSet;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }

        /// <summary>
        /// Generic method to check if entity exists
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public bool Exists(object primaryKey)
        {
            return DbSet.Find(primaryKey) != null;
        }

        /// <summary>
        /// Gets a single record by the specified criteria (usually the unique identifier)
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record that matches the specified criteria</returns>
        public TEntity GetSingle(Func<TEntity, bool> predicate)
        {
            return DbSet.Single<TEntity>(predicate);
        }

        /// <summary>
        /// The first record matching the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record containing the first record matching the specified criteria</returns>
        public TEntity GetFirst(Func<TEntity, bool> predicate)
        {
            return DbSet.First<TEntity>(predicate);
        }


        #endregion

        #region Async Methods


        public virtual async Task<IEnumerable<TEntity>> GetAsync()
        {
            IQueryable<TEntity> query = DbSet;
            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIDAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entity)
        {
            await DbSet.AddRangeAsync(entity);
        }

        ////public virtual async Task DeleteAsync(object id)
        ////{
        ////    TEntity entityToDelete = DbSet.Find(id);
        ////    await DeleteAsync(entityToDelete);
        ////}


        ////public virtual async Task UpdateAsync(TEntity entityToUpdate)
        ////{
        ////   await Task.Run(() =>
        ////    {
        ////        DbSet.Attach(entityToUpdate);
        ////        Context.Entry(entityToUpdate).State = EntityState.Modified;
        ////    });

        ////}

        public virtual async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, Boolean>> where)
        {
            return await DbSet.Where(where).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, Boolean>> where)
        {
            return await DbSet.Where(where).FirstOrDefaultAsync<TEntity>();
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<TEntity, Boolean>> where)
        {
            return await DbSet.Where(where).CountAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<bool> ExistsAsync(object primaryKey)
        {
            var exist = await DbSet.FindAsync(primaryKey);

            return exist != null;

        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.SingleAsync<TEntity>(predicate);
        }

        public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.FirstAsync<TEntity>(predicate);
        }


        #endregion
    }
}
