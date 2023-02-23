namespace ConsoleApp
{
    using Domain.Encapsulated.Domain;
    using Domain.Encapsulated.Infrastructure;
    using Microsoft.EntityFrameworkCore;

    public class ModelDbContext : ModelDatabase
    {
        public ModelDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase(databaseName: "Test");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerMemento>().HasKey(e => e.Id);
            modelBuilder.Entity<CustomerMemento>().Property(e => e.Code).IsRequired().HasMaxLength(Code.MaxLength);
            modelBuilder.Entity<CustomerMemento>().Property(e => e.Name).IsRequired().HasMaxLength(Name.MaxLength);
            modelBuilder.Entity<CustomerMemento>().Property(e => e.Gender).IsRequired().HasMaxLength(Gender.MaxLength);
        }
    }
}
