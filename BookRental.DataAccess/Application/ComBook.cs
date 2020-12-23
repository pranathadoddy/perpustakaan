using System;
using System.Collections.Generic;

#nullable disable

namespace BookRental.DataAccess.Application
{
    public partial class ComBook
    {
        public ComBook()
        {
            ComRentals = new HashSet<ComRental>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDateTime { get; set; }

        public virtual ICollection<ComRental> ComRentals { get; set; }
    }
}
