using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace AOWebApp.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerOrders = new HashSet<CustomerOrder>();
            Reviews = new HashSet<Review>();
        }
        [NotMapped]
        [Display(Name = "Full Name")]
        public  string FullName => FirstName + " " + LastName;
        [NotMapped]
        [Display(Name = "Contact Number")]
        public string ContactNumber 
        {
            get
            {
                var number = "";
                if (!string.IsNullOrWhiteSpace(MainPhoneNumber)) { number = MainPhoneNumber; }
                if (!string.IsNullOrWhiteSpace(SecondaryPhoneNumber)) { number += (number.Length > 0 ? "<br />" : "") + SecondaryPhoneNumber; }
                return number;
            }
            
            
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
