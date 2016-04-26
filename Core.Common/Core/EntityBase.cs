using System.Runtime.Serialization;

namespace Core.Common.Core
{
    /// <summary>
    /// mark it with DataContract attribute
    /// just for the case that we are going
    /// to use SOA in th efuture
    /// </summary>
    [DataContract]
    public abstract class EntityBase : IExtensibleDataObject
    {
        #region IExtensibleDataObject Members

        public ExtensionDataObject ExtensionData { get; set; }

        #endregion
    }

}
