using AutoMapper;
using BookRental.DataAccess.Application;
using BookRental.Dto.Common;
using BookRental.RepositoryContract.Common;
using Framework.Repository;

namespace BookRental.Repository.Common
{
    public class BookRepository : BaseRepository<BookRentalContext, ComBook, BookDto, int>, IBookRepository
    {
        public BookRepository(BookRentalContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
