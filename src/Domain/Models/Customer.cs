using Domain.Models.ValueObjects;
using Domain.Shared;
using System;

namespace Domain.Models
{
    public class Customer : IEntity
    {
        // Identity can be of a Guid type instead.
        public int? Id { get; private set; } 

        // Value Object's
        public FullName FullName { get; private set; }
        public DateTime CreatedOn { get; private set; }

        // EF private constructor
        private Customer() { } 

        public Customer(FullName fullName)
        {
            FullName = fullName;
            CreatedOn = DateTime.Now;
        }
    }
}
