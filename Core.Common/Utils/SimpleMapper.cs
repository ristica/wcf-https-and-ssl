using System;
using System.Linq;

namespace Core.Common.Utils
{
    /// <summary>
    /// maps entities to dtos
    /// see: DataRepositoryBase.Update
    /// </summary>
    public static class SimpleMapper
    {
        public static void PropertyMap<TSource, TDestination>(TSource source, TDestination destination)
            where TSource : class, new()
            where TDestination : class, new()
        {
            var sourceProperties = source.GetType().GetProperties().ToList();
            var destinationProperties = destination.GetType().GetProperties().ToList();

            foreach (var sourceProperty in sourceProperties)
            {
                var destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);

                if (destinationProperty == null) continue;
                try
                {
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                }
                catch (ArgumentException)
                {
                }
            }
        }
    }
}
