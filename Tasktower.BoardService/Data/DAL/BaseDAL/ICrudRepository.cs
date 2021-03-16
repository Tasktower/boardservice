using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Tasktower.BoardService.Data.DAL.BaseDAL
{
    public interface ICrudRepository<TEntity> where TEntity : class
    {
        public ValueTask<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        public ValueTask<TEntity> GetById(params object[] idValues);
        public ValueTask Insert(TEntity entity);
        public ValueTask Delete(params object[] idValues);
        public ValueTask Delete(TEntity entityToDelete);
        public ValueTask Update(TEntity entityToUpdate);
    }
}
