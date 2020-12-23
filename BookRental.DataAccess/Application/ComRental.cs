using System;
using System.Collections.Generic;

#nullable disable

namespace BookRental.DataAccess.Application
{
    public partial class ComRental
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int BookId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDateTime { get; set; }

        public virtual ComBook Book { get; set; }
        public virtual ComCustomer Customer { get; set; }
    }
}
