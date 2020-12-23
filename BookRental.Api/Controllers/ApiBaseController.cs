using Framework.Dto;
using Framework.ServiceContract.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookRental.Api.Controllers
{
    public abstract class ApiBaseController : ControllerBase
    {
        protected ApiBaseController(
            IConfiguration configuration
            )
        {
            Configuration = configuration;
        }

        protected IConfiguration Configuration { get; }

        private IEnumerable<string> GetModelStateError()
        {
            return ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
        }

        protected JsonResult GetBasicSuccessJson()
        {
            return new JsonResult(new { IsSuccess = true });
        }

        protected JsonResult GetSuccessJson(BasicResponse response, object value)
        {
            return new JsonResult(new
            {
                IsSuccess = true,
                MessageInfoTextArray = response.GetMessageInfoTextArray(),
                Value = value
            });
        }

        protected JsonResult GetErrorJson(string[] messages)
        {
            return new JsonResult(new
            {
                IsSuccess = false,
                MessageErrorTextArray = messages
            });
        }

        protected JsonResult GetErrorJson(string message)
        {
            var messageArray = new[] { message };
            return new JsonResult(new
            {
                IsSuccess = false,
                MessageErrorTextArray = messageArray
            });
        }

        protected JsonResult GetErrorJson(BasicResponse response)
        {
            return new JsonResult(new
            {
                IsSuccess = false,
                MessageErrorTextArray = response.GetMessageErrorTextArray()
            });
        }

        protected JsonResult GetErrorJsonFromModelState()
        {
            return GetErrorJson(GetModelStateError().ToArray());
        }

        protected ActionResult GetPagedSearchGridJson<TDto>(int pageIndex,
            int pageSize,
            List<object> rowJsonData,
            GenericPagedSearchResponse<TDto> response)
        {
            var jsonData = new
            {
                current = pageIndex,
                rowCount = pageSize,
                rows = rowJsonData,
                total = response.TotalCount
            };

            return new JsonResult(jsonData);
        }

        protected void PopulateAuditFieldsOnCreate<T>(AuditableDto<T> dto)
        {
            var currentUtcTime = DateTime.UtcNow;

            dto.CreatedBy = "User";
            dto.CreatedDateTime = currentUtcTime;
            dto.LastModifiedBy = "User";
            dto.LastModifiedDateTime = currentUtcTime;
        }

        protected void PopulateAuditFieldsOnUpdate<T>(AuditableDto<T> dto)
        {
            var currentUtcTime = DateTime.UtcNow;

            dto.LastModifiedBy = "User";
            dto.LastModifiedDateTime = currentUtcTime;
        }


    }
}
