using System.ComponentModel.DataAnnotations;

namespace BookRental.Api.Model
{
    public class CustomerModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
