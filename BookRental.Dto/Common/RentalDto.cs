using Framework.Dto;
using System;

namespace BookRental.Dto.Common
{
    public class RentalDto : AuditableDto<int>
    {
        public int CustomerId { get; set; }
        
        public int BookId { get; set; }

        public string CustomerName { get; set; }

        public string BookTitle { get; set; }

        public DateTime RentDate { get; set; }
        
        public DateTime ReturnDate { get; set; }
    }
}
