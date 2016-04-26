namespace Core.Common.Contracts
{
    /// <summary>
    /// Repository factory
    /// that is the way of using
    /// IDataRepository marker interface
    /// </summary>
    public interface IDataRepositoryFactory
    {
        T GetDataRepository<T>() where T : IDataRepository;
    }
}
