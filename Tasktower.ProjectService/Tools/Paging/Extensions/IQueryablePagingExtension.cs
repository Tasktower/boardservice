using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tasktower.ProjectService.Errors;
using Tasktower.ProjectService.Tools.Constants;

namespace Tasktower.ProjectService.Tools.Paging.Extensions
{
    public static class QueryablePagingExtension
    {
        public static async ValueTask<Page<TEntity>> GetPageAsync<TEntity>(this IQueryable<TEntity> query, 
            Pagination pagination, 
            Func<string, IQueryable<TEntity>, IQueryable<TEntity>> orderByQueryFunc) 
            where TEntity : class
        {
            var totalTask = query.CountAsync();
            
            var skip = pagination.Page * pagination.PageSize;
            
            query = orderByQueryFunc(pagination.OrderBy, query);
            
            var total = await totalTask;
            var results = await query.Skip(skip).Take(pagination.PageSize).ToListAsync();

            return new Page<TEntity>()
            {
                Pagination = pagination,
                ResultsSize = results.Count,
                ResultsList = results,
                Total = total
            };
        }
    }
}