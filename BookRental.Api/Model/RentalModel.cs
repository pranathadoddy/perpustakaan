using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookRental.Api.Model
{
    public class RentalModel
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }
    }
}
