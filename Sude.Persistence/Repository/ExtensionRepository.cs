using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Sude.Persistence.Repository
{
    public static class ExtensionRepository
    {

        public static IEnumerable<TEntity> ToPaged<TEntity>(this IEnumerable<TEntity> entities, int pageIndex, int pageSize , out int rowCount)
        {
             rowCount = entities.Count();          
            return entities.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public static IEnumerable<TEntity> ToTake<TEntity>(this IEnumerable<TEntity> entities, int rowCount)
        {
            return   entities.Take(rowCount);

        }
    }
}
