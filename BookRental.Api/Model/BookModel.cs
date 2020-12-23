using System.ComponentModel.DataAnnotations;

namespace BookRental.Api.Model
{
    public class BookModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
