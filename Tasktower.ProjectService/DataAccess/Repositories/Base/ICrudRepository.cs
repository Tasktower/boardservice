using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasktower.ProjectService.DataAccess.Repositories.Base
{
    public interface ICrudRepository<TIdType, TEntity> where TEntity : class
    {

        public ValueTask<TEntity> GetById(TIdType id);
        public IQueryable<TEntity> Queryable();
        public ValueTask Insert(TEntity entity);
        public ValueTask InsertMany(IEnumerable<TEntity> entities);
        public ValueTask Delete(TIdType id);
        public ValueTask Delete(TEntity entityToDelete);
        public ValueTask DeleteAll();
        public ValueTask Update(TEntity entityToUpdate);
    }
}
