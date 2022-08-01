using AutoMapper;
using UserPayment.API.Entities;
using UserPayment.API.Models;

namespace UserPayment.API.Profiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentDto>();
        }
    }
}
