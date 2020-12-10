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

        public async static  Task<int> ToCount<TEntity>(this IEnumerable<TEntity> source)
        {
          return source.Count();          
 
        }

        public  static IEnumerable<TEntity> ToTake<TEntity>(this IEnumerable<TEntity> source, int rowCount)
        {
            return source.Take(rowCount);

        }
        public static IEnumerable<TSource> ToPaged<TSource>(this IEnumerable<TSource> source, int page, int pageSize, out int rowsCount)
        {
            rowsCount = source.Count();
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

    }
}
