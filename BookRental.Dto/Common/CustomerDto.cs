using Framework.Dto;

namespace BookRental.Dto.Common
{
    public class CustomerDto : AuditableDto<int>
    {
        public string Name { get; set; }

        public string Address { get; set; }
    }
}
