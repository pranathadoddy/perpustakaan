using AutoMapper;
using BookRental.DataAccess.Application;
using BookRental.Dto.Common;

namespace BookRental.DataAccess
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ComBook, BookDto>().ReverseMap();
            CreateMap<ComCustomer, CustomerDto>().ReverseMap();
            CreateMap<ComRental, RentalDto>().ReverseMap();
        }
    }
}
