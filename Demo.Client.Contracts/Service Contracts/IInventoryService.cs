using System.ServiceModel;
using Core.Common.Exceptions;
using Demo.Client.Entities;
using Core.Common.Contracts;

namespace Demo.Client.Contracts
{
    [ServiceContract]
    public interface IInventoryService : IServiceContract
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
