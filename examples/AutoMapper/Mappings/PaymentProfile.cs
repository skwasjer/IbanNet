using AutoMapper;
using AutoMapperExample.Domain;
using AutoMapperExample.Dtos;

namespace AutoMapperExample.Mappings
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentDto>()
                .ForMember(dest => dest.BankAccountNumber, opts => opts.MapFrom(src => src.BankAccountNumber))
                .ForMember(dest => dest.Amount, opts => opts.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Currency, opts => opts.MapFrom(src => src.Currency))
                .ReverseMap();
        }
    }
}
