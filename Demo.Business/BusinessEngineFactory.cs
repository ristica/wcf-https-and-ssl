using System.ComponentModel.Composition;
using Core.Common.Contracts;
using Core.Common.Core;

namespace Demo.Business
{
    [Export(typeof(IBusinessEngineFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BusinessEngineFactory : IBusinessEngineFactory
    {
        #region IBusinessEngineFactory Members

        T IBusinessEngineFactory.GetBusinessEngine<T>()
        {
            return ObjectBase.Container.GetExportedValue<T>();
        }

        #endregion
    }
}
