using BookRental.Dto.Common;
using Framework.ServiceContract;

namespace BookRental.ServiceContract.Common
{
    public interface ICustomerService : IBaseService<CustomerDto, int>
    {
    }
}
