using Core.Common.Contracts;
using Demo.Business.Entities;

namespace Demo.Data.Contracts
{
    public interface IProductRepository : IDataRepository
    {
        Product GetProductByArticleNumber(string articleNumber);
        Product GetProductById(int productId);
        Product[] GetProducts();
        Product[] GetActiveProducts();
        void ActivateProduct(int productId);
        void DeactivateProduct(int productId);
        Product UpdateProduct(Product product);
    }
}