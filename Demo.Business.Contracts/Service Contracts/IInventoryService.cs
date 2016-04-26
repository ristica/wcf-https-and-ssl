using System.ServiceModel;
using Core.Common.Exceptions;
using Demo.Business.Entities;

namespace Demo.Business.Contracts
{
    [ServiceContract]
    public interface IInventoryService
    {
        [OperationContract]
        Product[] GetProducts();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Product GetProductById(int id, bool acceptNullable = false);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Product UpdateProduct(Product product);
    }
}
