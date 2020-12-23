using BookRental.Dto.Common;
using BookRental.RepositoryContract.Common;
using BookRental.ServiceContract.Common;
using Framework.Service;

namespace BookRental.Service.Common
{
    public class BookService : BaseService<BookDto, int, IBookRepository>, IBookService
    {
        public BookService(IBookRepository repository) : base(repository)
        {
        }
    }
}
