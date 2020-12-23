using BookRental.Dto.Common;
using Framework.RepositoryContract;

namespace BookRental.RepositoryContract.Common
{
    public interface IBookRepository: IBaseRepository<BookDto>
    {
    }
}
