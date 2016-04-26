using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using Demo.Business.Common;
using Demo.Business.Contracts;
using Demo.Business.Entities;
using Demo.Data.Contracts;
using System.Diagnostics;

namespace Demo.Business.Managers
{
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.PerCall, 
        ConcurrencyMode = ConcurrencyMode.Multiple, 
        ReleaseServiceInstanceOnTransactionComplete = false)]
    public class InventoryManager : ManagerBase, IInventoryService
    {
        #region Fields

        [Import]
        private IDataRepositoryFactory _repositoryFactory;

        [Import]
        private IBusinessEngineFactory _businessFactory;

        #endregion

        #region IInventoryManager implementation

        public Product[] GetProducts()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var productRepository = this._repositoryFactory.GetDataRepository<IProductRepository>();
                var products = productRepository.GetProducts();

                return products.ToArray();
            });
        }

        public Product GetProductById(int id, bool acceptNullable = false)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var productRepository = this._repositoryFactory.GetDataRepository<IProductRepository>();
                var product = productRepository.GetProductById(id);

                if (product == null && acceptNullable)
                {
                    return null;
                }

                if (product == null)
                {
                    var ex = new NotFoundException($"Product with id: {id} not found!");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return product;
            });
        }

        [TransactionFlow(TransactionFlowOption.Allowed)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Product UpdateProduct(Product product)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var productRepository = this._repositoryFactory.GetDataRepository<IProductRepository>();
                if (product.ProductId == 0)
                {
                    product.ArticleNumber =
                        this._businessFactory.GetBusinessEngine<IProductInventoryEngine>().GenerateArticleNumber();
                }

                var updatedEntity = productRepository.UpdateProduct(product);
                return updatedEntity;
            });
        }

        #endregion        
    }
}
