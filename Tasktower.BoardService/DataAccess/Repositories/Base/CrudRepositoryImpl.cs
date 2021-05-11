using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tasktower.BoardService.DataAccess.Repositories.Base
{
    public abstract class CrudRepositoryImpl<TIdType, TEntity, TContext> : ICrudRepository<TIdType, TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        internal TContext context;
        internal DbSet<TEntity> dbSet;

        public CrudRepositoryImpl(TContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual async ValueTask<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual async ValueTask<TEntity> GetById(TIdType idValues)
        {
            return await dbSet.FindAsync(idValues);
        }

        public virtual async ValueTask Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual async ValueTask Delete(TIdType idValues)
        {
            TEntity entityToDelete = await dbSet.FindAsync(idValues);
            await Delete(entityToDelete);
        }

        public virtual async ValueTask Delete(TEntity entityToDelete)
        {
            ValueTask valueTask = new ValueTask();
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            await valueTask;
        }

        public virtual async ValueTask Update(TEntity entityToUpdate)
        {
            ValueTask valueTask = new ValueTask();
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            await valueTask;
        }
    }
}
