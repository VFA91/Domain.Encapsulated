namespace Domain.Encapsulated.Infrastructure
{
    using global::Domain.Encapsulated.Domain;
    using Microsoft.EntityFrameworkCore;

    public class ModelDatabase : DbContext
    {
        public DbSet<CustomerMemento> Customers { get; set; }

        public ModelDatabase(DbContextOptions options)
            : base(options)
        {
        }
    }
}
