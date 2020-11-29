using Domain.Exceptions;
using Domain.Services;
using Domain.Tests.Factories;
using Xunit;

namespace Domain.Tests.Services
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

    public class DiscountValidatorTests
    {
        [Fact]
        public void ValidateDiscountAmount_ValidInput_ReturnsVoid()
        {
            // Assign
            var validator = new DiscountValidator();
            var order = OrderFactory.CreateSimpleOrder();

            // Act
            validator.ValidateDiscountAmount(order, voucherPercentageDiscount: 10);

            // Assert
            // No exception was thrown.
            Assert.True(true);
        }

        [Fact]
        public void ValidateDiscountAmount_InvalidInput_ReturnsDomainException()
        {
            // Assign
            var validator = new DiscountValidator();
            var order = OrderFactory.CreateSimpleOrder();
            var invalidDiscount = 1;

            // Assert
            Assert.Throws<DomainException>(() 
                // Act
                => validator.ValidateDiscountAmount(order, invalidDiscount));
        }
    }
}
