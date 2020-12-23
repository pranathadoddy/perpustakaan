using BookRental.Dto.Common;
using Framework.ServiceContract;

namespace BookRental.ServiceContract.Common
{
    public interface IRentalService : IBaseService<RentalDto, int>
    {
    }
}
