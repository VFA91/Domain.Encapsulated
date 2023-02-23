namespace Domain.Encapsulated.Infrastructure
{
    using global::Domain.Encapsulated.Domain;
    using global::Domain.Encapsulated.Shared;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public class CustomerRepository : ICustomersRepository, IIdentityGenerator<CustomerId>
    {
        public const string IdSequenceName = "customer_id_sequence";

        private readonly DbContext _db;

        public CustomerRepository(DbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<Customer> Find(CustomerId id, CancellationToken cancellationToken)
        {
            Ensure.Argument.NotNull(id, nameof(id));

            var entity = await _db.Set<CustomerMemento>().SingleOrDefaultAsync(e => e.Id == (int)id, cancellationToken);

            if (entity is null)
            {
                throw new EntityNotFoundException(typeof(CustomerMemento), (int)id);
            }

            return Customer.FromMemento(entity);
        }

        public async Task Add(Customer entity, CancellationToken cancellationToken)
        {
            Ensure.Argument.NotNull(entity, nameof(entity));

            await _db.AddAsync(entity.CreateMemento(), cancellationToken);
        }

        public void Delete(Customer entity)
        {
            Ensure.Argument.NotNull(entity, nameof(entity));
            _db.Remove(entity);
        }

        public async Task<CustomerId> NextIdentity(CancellationToken cancellationToken)
        {
            var result = new SqlParameter("@result", SqlDbType.Int) { Direction = ParameterDirection.Output };
            await _db.Database.ExecuteSqlRawAsync($"SELECT @result = (NEXT VALUE FOR {IdSequenceName})", new[] { result }, cancellationToken);
            return new CustomerId((int)result.Value);
        }
    }
}
