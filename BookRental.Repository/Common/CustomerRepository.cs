using AutoMapper;
using BookRental.DataAccess.Application;
using BookRental.Dto.Common;
using BookRental.RepositoryContract.Common;
using Framework.Repository;

namespace BookRental.Repository.Common
{
    public class CustomerRepository : BaseRepository<BookRentalContext, ComCustomer, CustomerDto, int>, ICustomerRepository
    {
        public CustomerRepository(BookRentalContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
