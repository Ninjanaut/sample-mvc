using Domain.Models;

namespace Domain.Services
{
    public interface IDiscountValidator
    {
        void ValidateDiscountAmount(Order order, int voucherPercentageDiscount);
    }
}