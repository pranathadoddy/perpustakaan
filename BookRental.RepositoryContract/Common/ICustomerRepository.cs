using BookRental.Dto.Common;
using Framework.RepositoryContract;

namespace BookRental.RepositoryContract.Common
{
    public interface ICustomerRepository : IBaseRepository<CustomerDto>
    {
    }
}
