namespace Domain.Encapsulated.Shared
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IRepository<TEntity, TId>
        where TEntity : AggregateRoot<TId>
        where TId : ValueObject<TId>
    {
        Task<TEntity> Find(TId id, CancellationToken cancellationToken);
        Task Add(TEntity entity, CancellationToken cancellationToken);
        void Delete(TEntity entity);
    }

    public interface IIdentityGenerator<TId>
         where TId : ValueObject<TId>
    {
        Task<TId> NextIdentity(CancellationToken cancellationToken);
    }
}