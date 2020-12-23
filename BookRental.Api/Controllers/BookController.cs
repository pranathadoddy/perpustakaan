using BookRental.Api.Model;
using BookRental.Core;
using BookRental.Dto.Common;
using BookRental.ServiceContract.Common;
using Framework.ServiceContract.Request;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BookRental.Api.Controllers
{
    [Route("api/book")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class BookController : ApiBaseController
    {
        private readonly IBookService _bookService;

        public BookController(IConfiguration configuration, IBookService bookService) : base(configuration)
        {
            this._bookService = bookService;
        }

        // GET: api/book/
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var response = await this._bookService.PagedSearchAsync(new PagedSearchRequest { 
                PageIndex = 0,
                PageSize = 1000,
                Filters = string.Empty,
                Keyword = string.Empty,
                OrderByFieldName = "Id",
                SortOrder = CoreConstant.SortOrder.Ascending
            });

            return this.Ok(this.GetSuccessJson(response, response.DtoCollection));
        }

        // GET: api/book/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var response = await this._bookService.ReadAsync(new GenericRequest<int>
            {
                Data = id
            });

            if (response.IsError())
            {
                return this.BadRequest(this.GetErrorJson(response));
            }

            return this.Ok(this.GetSuccessJson(response, response.Data));
        }

        // POST: api/book
        [HttpPost]
        public async Task<ActionResult> Post(BookModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.GetErrorJsonFromModelState());
            }

            var dto = new BookDto
            {
                Title = model.Title,
                Description = model.Description,
            };

            this.PopulateAuditFieldsOnCreate(dto);

            var response = await this._bookService.InsertAsync(new GenericRequest<BookDto> { 
                Data = dto
            });

            if (response.IsError())
            {
                return this.BadRequest(this.GetErrorJson(response));
            }

            return this.Ok(this.GetSuccessJson(response, response.Data));
        }

        // PUT: api/book/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, BookModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.GetErrorJsonFromModelState());
            }

            var readResPonse = await this._bookService.ReadAsync(new GenericRequest<int> { Data = id });
            if (readResPonse.IsError())
            {
                return this.BadRequest(this.GetErrorJson(readResPonse));
            }

            var dto = readResPonse.Data;
            dto.Title = model.Title;
            dto.Description = model.Description;

            this.PopulateAuditFieldsOnUpdate(dto);

            var response = await this._bookService.UpdateAsync(new GenericRequest<BookDto>
            {
                Data = dto
            });

            if (response.IsError())
            {
                return this.BadRequest(this.GetErrorJson(response));
            }

            return this.Ok(this.GetSuccessJson(response, response.Data));
        }

        // DELETE: api/book/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await this._bookService.DeleteAsync(new GenericRequest<int>
            {
                Data = id
            });

            if (response.IsError())
            {
                return this.BadRequest(this.GetErrorJson(response));
            }

            return this.Ok(this.GetSuccessJson(response, response.Data));
        }
    }
}
