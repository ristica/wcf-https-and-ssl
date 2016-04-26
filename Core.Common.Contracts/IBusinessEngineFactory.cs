namespace Core.Common.Contracts
{
    /// <summary>
    /// abstract factory
    /// </summary>
    public interface IBusinessEngineFactory
    {
        T GetBusinessEngine<T>() where T : IBusinessEngine;
    }
}
