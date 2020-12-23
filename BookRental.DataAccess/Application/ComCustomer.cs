using System;
using System.Collections.Generic;

#nullable disable

namespace BookRental.DataAccess.Application
{
    public partial class ComCustomer
    {
        public ComCustomer()
        {
            ComRentals = new HashSet<ComRental>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDateTime { get; set; }

        public virtual ICollection<ComRental> ComRentals { get; set; }
    }
}
