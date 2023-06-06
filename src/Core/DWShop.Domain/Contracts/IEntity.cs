namespace DWShop.Domain.Contracts
{
    /// <summary>
    /// La interface que define cuando es una entidad
    /// </summary>
    public interface IEntity
    {
    }

    public interface IEntity<TId> : IEntity 
    {
        public TId Id { get; set; }
    }
}
