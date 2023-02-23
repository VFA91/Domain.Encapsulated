namespace Domain.Encapsulated.Domain
{
    using System.Threading.Tasks;

    public interface ICustomersRepository
    {
        Task Add(Customer entity, CancellationToken cancellationToken);
        void Delete(Customer entity);
        Task<Customer> Find(CustomerId id, CancellationToken cancellationToken);
    }
}
