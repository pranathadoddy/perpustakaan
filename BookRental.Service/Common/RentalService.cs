using BookRental.Core.Resource.Rental;
using BookRental.Dto.Common;
using BookRental.RepositoryContract.Common;
using BookRental.ServiceContract.Common;
using Framework.Service;
using Framework.ServiceContract.Request;
using Framework.ServiceContract.Response;
using System.Threading.Tasks;

namespace BookRental.Service.Common
{
    public class RentalService : BaseService<RentalDto, int, IRentalRepository>, IRentalService
    {
        public RentalService(IRentalRepository repository) : base(repository)
        {
        }

        protected override async Task<GenericResponse<RentalDto>> ValidateAsync(GenericRequest<RentalDto> request)
        {
            var response = await base.ValidateAsync(request);
            var dto = request.Data;
            if(dto.ReturnDate.Date <= dto.RentDate.Date)
            {
                response.AddErrorMessage(RentalResource.ReturnDateErrorMessage);
            }

            return response;
        }
    }
}
