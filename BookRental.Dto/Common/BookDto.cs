using Framework.Dto;

namespace BookRental.Dto.Common
{
    public class BookDto: AuditableDto<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
