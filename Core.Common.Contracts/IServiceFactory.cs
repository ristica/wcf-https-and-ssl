namespace Core.Common.Contracts
{
    /// <summary>
    /// service factory
    /// </summary>
    public interface IServiceFactory
    {
        T CreateClient<T>() where T : IServiceContract;
        T CreateClient<T>(string endpoint) where T : IServiceContract;
    }
}