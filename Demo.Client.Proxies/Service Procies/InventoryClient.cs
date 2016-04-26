using System.ComponentModel.Composition;
using Core.Common.ServiceModel;
using Demo.Client.Contracts;
using Demo.Client.Entities;

namespace Demo.Client.Proxies.Service_Procies
{
    [Export(typeof(IInventoryService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InventoryClient : UserClientBase<IInventoryService>, IInventoryService
    {
        #region IInventoryService implementation

        public Product[] GetProducts()
        {
            return Channel.GetProducts();
        }

        public Product GetProductById(int id, bool acceptNullable = false)
        {
            return Channel.GetProductById(id, acceptNullable);
        }

        public Product UpdateProduct(Product product)
        {
            return Channel.UpdateProduct(product);
        }

        #endregion
    }
}
