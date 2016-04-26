namespace Core.Common.Contracts
{
    /// <summary>
    /// it would be used by an entity just to be sure
    /// that the current user "owns" the current entity
    /// </summary>
    public interface ICustomerOwnedEntity
    {
        int OwnerCustomerId { get; }
    }
}
