using Domain.Exceptions;
using Domain.Shared;
using System.Collections.Generic;

namespace Domain.Models.ValueObjects
{
    public class FullName : ValueObject
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        // EF private constructor.
        private FullName() { }

        public FullName(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new DomainException("Customer's first name is mandatory.");
            }
            if (string.IsNullOrEmpty(lastName))
            {
                throw new DomainException("Customer's last name is mandatory.");
            }
            // Reduplication is not allowed.
            if (firstName == lastName)
            {
                throw new DomainException("First name cannot be the same as last name.");
            }
            FirstName = firstName;
            LastName = lastName;
        }
        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
