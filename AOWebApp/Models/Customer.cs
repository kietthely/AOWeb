using System;
using System.Collections.Generic;

namespace AOWebApp.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerOrders = new HashSet<CustomerOrder>();
            Reviews = new HashSet<Review>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string MainPhoneNumber { get; set; } = null!;
        public string? SecondaryPhoneNumber { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual ICollection<CustomerOrder> CustomerOrders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
