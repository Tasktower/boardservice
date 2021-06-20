using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tasktower.ProjectService.DataAccess.Repositories.Base
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

        public IQueryable<TEntity> Queryable()
        {
            return dbSet.AsQueryable();
        }

        public virtual async ValueTask<TEntity> GetById(TIdType idValues)
        {
            return await dbSet.FindAsync(idValues);
        }

        public virtual async ValueTask Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }
        
        public virtual async ValueTask InsertMany(IEnumerable<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities.ToArray());
        }

        public virtual async ValueTask Delete(TIdType id)
        {
            var entityToDelete = await dbSet.FindAsync(id);
            await Delete(entityToDelete);
        }

        public virtual async ValueTask Delete(TEntity entityToDelete)
        {
            var valueTask = new ValueTask();
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            await valueTask;
        }

        public async ValueTask DeleteAll()
        {
            var entities = await dbSet.ToListAsync();
            dbSet.AttachRange(entities);
            dbSet.RemoveRange(entities);
        }

        public virtual async ValueTask Update(TEntity entityToUpdate)
        {
            var valueTask = new ValueTask();
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            await valueTask;
        }
    }
}
