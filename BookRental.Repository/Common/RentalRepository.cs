using AutoMapper;
using BookRental.DataAccess.Application;
using BookRental.Dto.Common;
using BookRental.RepositoryContract.Common;
using Framework.Repository;
using Framework.RepositoryContract;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookRental.Repository.Common
{
    public class RentalRepository : BaseRepository<BookRentalContext, ComRental, RentalDto, int>, IRentalRepository
    {
        public RentalRepository(BookRentalContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<PagedSearchResult<RentalDto>> PagedSearchAsync(PagedSearchParameter parameter)
        {
            var dbSet = this.Context.Set<ComRental>()
                .Include(item=> item.Book)
                .Include(item => item.Customer);
            
            var queryable = string.IsNullOrEmpty(parameter.Filters)
                ? dbSet.AsQueryable()
                : dbSet.Where(parameter.Filters);
            queryable = string.IsNullOrEmpty(parameter.Keyword)
                ? queryable
                : GetKeywordPagedSearchQueryable(queryable, parameter.Keyword);

            return await GetPagedSearchEnumerableAsync(parameter, queryable);
        }

        protected override void DtoToEntity(RentalDto dto, ComRental entity)
        {
            base.DtoToEntity(dto, entity);

            if (entity.Id == 0)
            {
                entity.Book = null;
                entity.Customer = null;
            }
            entity.CustomerId = dto.CustomerId;
            entity.BookId = dto.BookId;
        }

        protected override void EntityToDto(ComRental entity, RentalDto dto)
        {
            base.EntityToDto(entity, dto);
            if(entity.Book != null)
            {
                dto.BookTitle = entity.Book.Title;
            }
            
            if (entity.Customer != null)
            {
                dto.CustomerName = entity.Customer.Name;
            }
        }
    }
}
