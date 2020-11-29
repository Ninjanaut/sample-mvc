using Domain.Models;
using Domain.Services;
using Domain.Tests.Factories;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Domain.Tests.Models
{
    // Naming convention
    // https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices#best-practices
    // For complex class use nested class for each method
    // https://haacked.com/archive/2012/01/02/structuring-unit-tests.aspx/

    /* Naming convention for method names
     * The name of the method being tested.
     * The scenario under which it's being tested.
     * The expected behavior when the scenario is invoked.
     */

    public class OrderTests
    {
        public class Constructor
        {
            [Fact]
            public void ValidInput_ValidObjectCreation()
            {
                // Arrange
                var orderItems = new List<OrderItem>() {
                    new OrderItem(orderId: null, productId: 5, quantity: 50),
                    new OrderItem(orderId: null, productId: 6, quantity: 60)
                };

                // Act
                var order = new Order(customerId: 1, orderItems);

                // Assert
                Assert.Equal(1, order.CustomerId);
                Assert.Equal(2, order.OrderItems.Count);
                Assert.Collection(order.OrderItems,
                    item => { Assert.Equal(5, item.ProductId); Assert.Equal(50, item.Quantity); },
                    item => { Assert.Equal(6, item.ProductId); Assert.Equal(60, item.Quantity); }
                );
            }
        }

        public class SetDiscount
        {
            [Fact]
            public void ValidDiscount_DiscountIsProperlySetted()
            {
                // Arrange
                var order = OrderFactory.CreateSimpleOrder();

                var validator 
                    = new Mock<IDiscountValidator>();
                        validator.Setup(m => m.ValidateDiscountAmount(
                            It.IsAny<Order>(), It.IsAny<int>()));

                // Act
                order.SetDiscount(voucherPercentageDiscount: 10, validator.Object);

                // Assert
                Assert.Equal(10, order.VoucherPercentageDiscount);
            }

        }
    }
}
