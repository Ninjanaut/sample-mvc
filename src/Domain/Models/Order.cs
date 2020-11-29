using Domain.Exceptions;
using Domain.Services;
using Domain.Shared;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Order : IAggregate
    {
        // Identity can be of a Guid type instead.
        public int? Id { get; private set; }
        public int CustomerId { get; private set; }

        // This will increase the encapsulation and prevent manipulation of the public collection. 
        // But it will not prevent the direct calling of the method of 
        // a specific object in the collection.
        // --------------------------------------------------------------------------------
        private List<OrderItem> _orderItems = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
        // --------------------------------------------------------------------------------
        //public List<OrderItem> OrderItems { get; private set; }

        public int VoucherPercentageDiscount { get; private set; }
        public DateTime CreatedOn { get; private set; }

        // EF navigation property.
        // If we don't like to put navigation properties into the domain model. 
        // We can use the CQRS technique and split the model.
        public Customer Customer { get; private set; }

        // EF private constructor.
        private Order() { } 

        public Order(int customerId, List<OrderItem> orderItems)
        {
            if (orderItems == null || orderItems.Count == 0)
            {
                throw new DomainException("The order must contain at least one item.");
            }
            CustomerId = customerId;
            _orderItems = orderItems;
            CreatedOn = DateTime.Now;
        }

        public void SetDiscount(int voucherPercentageDiscount, IDiscountValidator validator)
        {
            // double dispatch technique
            validator.ValidateDiscountAmount(this, voucherPercentageDiscount);
            
            VoucherPercentageDiscount = voucherPercentageDiscount;
        }
    }
}
