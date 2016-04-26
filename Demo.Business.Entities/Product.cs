using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace Demo.Business.Entities
{
    [DataContract]
    public class Product : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public string ArticleNumber { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public byte[] Image { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        #region IIdentifiableEntity

        public int EntityId
        {
            get { return this.ProductId; }
            set { this.ProductId = value; }
        }

        #endregion
    }
}
