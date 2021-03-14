using Domain.Models;
using Domain.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mvc.Infrastructure.Data;
using Mvc.Tests.TestingTools;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Mvc.Tests.Controllers
{
    public class OrderControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _context;
        private readonly IServiceScope _scope;

        public OrderControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateDefaultClient();
            _scope = (factory.Services.GetRequiredService<IServiceScopeFactory>()).CreateScope();
            _context = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
            // database is now shared across tests
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task Create_Post_OrderIsCreated()
        {
            // Arrange 
            //// Create customer in database
            var customer = new Customer(new FullName("Han", "Solo"));
            _context.Customers.Add(customer);
            _context.SaveChanges();

            //// Prepare order post data
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Command.CustomerId", customer.Id.ToString()),
                new KeyValuePair<string, string>("Command.OrderItems[0].ProductId", "1"),
                new KeyValuePair<string, string>("Command.OrderItems[0].Quantity", "10"),
                new KeyValuePair<string, string>("Command.OrderItems[1].ProductId", "2"),
                new KeyValuePair<string, string>("Command.OrderItems[1].Quantity", "20"),
            });

            // Act
            var response = await _httpClient.PostAsync("order/create", formContent);

            var createdOrder
                = _context.Orders
                    .Include(x => x.OrderItems)
                    .Where(x => x.CustomerId == customer.Id)
                    .FirstOrDefault();

            // Assert
            Assert.NotEmpty(createdOrder.OrderItems);
            Assert.Equal(2, createdOrder.OrderItems.Count);
            Assert.Contains(createdOrder.OrderItems, x => x.ProductId == 1 && x.Quantity == 10);
            Assert.Contains(createdOrder.OrderItems, x => x.ProductId == 2 && x.Quantity == 20);
        }
    }
}

