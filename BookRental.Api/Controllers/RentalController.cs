using BookRental.Api.Model;
using BookRental.Core;
using BookRental.Dto.Common;
using BookRental.ServiceContract.Common;
using Framework.ServiceContract.Request;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace BookRental.Api.Controllers
{
    [Route("api/rental")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class RentalController : ApiBaseController
    {
        private readonly IRentalService _rentalService;

        public RentalController(IConfiguration configuration, 
            IRentalService rentalService) : base(configuration)
        {
            this._rentalService = rentalService;
        }

        // GET: api/rental
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var response = await this._rentalService.PagedSearchAsync(new PagedSearchRequest
            {
                PageIndex = 0,
                PageSize = 1000,
                Filters = string.Empty,
                Keyword = string.Empty,
                OrderByFieldName = "Id",
                SortOrder = CoreConstant.SortOrder.Ascending
            });

            return this.Ok(this.GetSuccessJson(response, response.DtoCollection));
        }

        // POST api/rental
        [HttpPost]
        public async Task<ActionResult> Post(RentalModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.GetErrorJsonFromModelState());
            }

          
            var dto = new RentalDto
            {
               BookId = model.BookId,
               CustomerId = model.CustomerId,
               RentDate = DateTime.UtcNow,
               ReturnDate = model.ReturnDate,
            };

            this.PopulateAuditFieldsOnCreate(dto);

            var response = await this._rentalService.InsertAsync(new GenericRequest<RentalDto>
            {
                Data = dto
            });

            if (response.IsError())
            {
                return this.BadRequest(this.GetErrorJson(response));
            }

            return this.Ok(this.GetSuccessJson(response, response.Data));
        }
    }
}
