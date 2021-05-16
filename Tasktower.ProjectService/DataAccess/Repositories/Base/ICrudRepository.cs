using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Tasktower.ProjectService.DataAccess.Repositories.Base
{
    public interface ICrudRepository<TIdType, TEntity> where TEntity : class
    {
        public ValueTask<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        public ValueTask<TEntity> GetById(TIdType id);
        public ValueTask Insert(TEntity entity);
        public ValueTask Delete(TIdType idValues);
        public ValueTask Delete(TEntity entityToDelete);
        public ValueTask Update(TEntity entityToUpdate);
    }
}
