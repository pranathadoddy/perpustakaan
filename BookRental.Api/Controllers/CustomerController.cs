using BookRental.Api.Model;
using BookRental.Core;
using BookRental.Dto.Common;
using BookRental.ServiceContract.Common;
using Framework.ServiceContract.Request;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookRental.Api.Controllers
{
    [Route("api/customer")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class CustomerController : ApiBaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(IConfiguration configuration, 
            ICustomerService customerService) : base(configuration)
        {
            this._customerService = customerService;
        }

        // GET: api/customer
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var response = await this._customerService.PagedSearchAsync(new PagedSearchRequest
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

        // GET api/customer/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var response = await this._customerService.ReadAsync(new GenericRequest<int>
            {
                Data = id
            });

            if (response.Data == null)
            {
                return this.NotFound();
            }

            return this.Ok(this.GetSuccessJson(response, response.Data));
        }

        // POST api/customer
        [HttpPost]
        public async Task<ActionResult> Post(CustomerModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.GetErrorJsonFromModelState());
            }

            var dto = new CustomerDto
            {
                Name = model.Name,
                Address = model.Address,
            };

            this.PopulateAuditFieldsOnCreate(dto);

            var response = await this._customerService.InsertAsync(new GenericRequest<CustomerDto>
            {
                Data = dto
            });

            if (response.IsError())
            {
                return this.BadRequest(this.GetErrorJson(response));
            }

            return this.Ok(this.GetSuccessJson(response, response.Data));
        }

        // PUT api/customer/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, CustomerModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.GetErrorJsonFromModelState());
            }

            var readResPonse = await this._customerService.ReadAsync(new GenericRequest<int> { Data = id });
            if (readResPonse.IsError())
            {
                return this.BadRequest(this.GetErrorJson(readResPonse));
            }

            var dto = readResPonse.Data;
            dto.Name = model.Name;
            dto.Address = model.Address;

            this.PopulateAuditFieldsOnUpdate(dto);

            var response = await this._customerService.UpdateAsync(new GenericRequest<CustomerDto>
            {
                Data = dto
            });

            if (response.IsError())
            {
                return this.BadRequest(this.GetErrorJson(response));
            }

            return this.Ok(this.GetSuccessJson(response, response.Data));
        }

        // DELETE api/customer/id
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await this._customerService.DeleteAsync(new GenericRequest<int>
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
