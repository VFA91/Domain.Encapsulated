// See https://aka.ms/new-console-template for more information
using ConsoleApp;
using Domain.Encapsulated.Domain;
using Domain.Encapsulated.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");
var options = new DbContextOptionsBuilder<ModelDbContext>()
    .UseInMemoryDatabase("test").Options;

using (var context = new ModelDbContext(options))
{
    ICustomersRepository customersRepository = new CustomerRepository(context);

    var id1 = new CustomerId(1);
    var customer1 = Customer.Create(id1, new Code("AAA"), new Name("Name1"), Gender.Man);
    var id2 = new CustomerId(2);
    var customer2 = Customer.Create(id2, new Code("BBB"), new Name("Name2"), Gender.Woman);
    await customersRepository.Add(customer1, default);
    await customersRepository.Add(customer2, default);

    context.SaveChanges();
    var customers = context.Customers.ToList();

    customers.Count.Should().Be(2);

    var getCustomer2 = await customersRepository.Find(id2, default);

    getCustomer2.Update(new Code("CCC"), new Name("Name3"), Gender.Unspecified);
    context.SaveChanges();

    context.Customers.Where(i => i.Code == "CCC").Should().NotBeNull();
}