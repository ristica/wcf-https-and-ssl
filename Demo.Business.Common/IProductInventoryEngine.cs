using Core.Common.Contracts;

namespace Demo.Business.Common
{
    public interface IProductInventoryEngine : IBusinessEngine
    {
        string GenerateArticleNumber();
    }
}