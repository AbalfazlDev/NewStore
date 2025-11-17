using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.Common
{
    public static class Pagination
    {
        public static IEnumerable<TSource> ToPage<TSource>(this IEnumerable<TSource> source, UInt16 page, byte pageSize, out uint rowsCount)
        {
            rowsCount = Convert.ToUInt32(source.Count());
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
