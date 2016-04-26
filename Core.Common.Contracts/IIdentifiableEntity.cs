namespace Core.Common.Contracts
{
    /// <summary>
    /// will be used to explicitly set the
    /// id property on an entity
    /// </summary>
    public interface IIdentifiableEntity
    {
        int EntityId { get; set; }
    }
}
