using AutoMapper;
using AutoMapperExample.Domain;
using AutoMapperExample.Dtos;
using IbanNet;

namespace AutoMapperExample.Mappings
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile(IIbanParser ibanParser)
        {
            CreateMap<Payment, PaymentDto>().ReverseMap();

            CreateMap<string, Iban>().ConvertUsing(s => ibanParser.Parse(s));
            CreateMap<Iban, string>().ConvertUsing(s => s.ToString(IbanFormat.Electronic));
        }
    }
}
