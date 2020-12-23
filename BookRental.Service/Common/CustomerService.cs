using BookRental.Dto.Common;
using BookRental.RepositoryContract.Common;
using BookRental.ServiceContract.Common;
using Framework.Service;

namespace BookRental.Service.Common
{
    public class CustomerService : BaseService<CustomerDto, int, ICustomerRepository>, ICustomerService
    {
        public CustomerService(ICustomerRepository repository) : base(repository)
        {
        }
    }
}
