using System.ComponentModel.Composition;
using Core.Common.Contracts;
using Core.Common.Core;

namespace Demo.Data
{
    /// <summary>
    /// this is the concrete repository factory
    /// </summary>
    [Export(typeof(IDataRepositoryFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        T IDataRepositoryFactory.GetDataRepository<T>()
        {
            return ObjectBase.Container.GetExportedValue<T>();
        }
    }
}
