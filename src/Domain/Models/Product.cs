using Domain.Exceptions;
using Domain.Shared;

namespace Domain.Models
{
    public class Product : IEntity
    {
        public int? Id { get; private set; }
        public int Number { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        // EF private constructor
        private Product() { } 

        public Product(int number, string name, decimal price)
        {
            if (name.Length > 40)
            {
                throw new DomainException("Product name cannot exceed 40 characters.");
            }
            if (Price <= 0)
            {
                throw new DomainException("Price has to be positive value.");
            }
            Number = number;
            Name = name;
            Price = price;
        }
    }
}
