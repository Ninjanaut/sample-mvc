using Domain.Exceptions;
using Domain.Models;

namespace Domain.Services
{
    public class DiscountValidator : IDiscountValidator
    {
        public void ValidateDiscountAmount(Order order, int voucherPercentageDiscount)
        {
            if (voucherPercentageDiscount <= 1 || voucherPercentageDiscount >= 100)
            {
                throw new DomainException("Discount has to be more than 1% and less than 100%.");
            }
        }
    }
}

