using System.Collections.Generic;
using System.Linq;

namespace Core.Common.Extensions
{
    public static class DataExtensions
    {
        /// <summary>
        /// will be used for the entity framework's lazy loaded entities
        /// just a helper method to cast it to array 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToFullyLoaded<T>(this IQueryable<T> query)
        {
            return query.ToArray().ToList();
        }
    }
}
