using AutoMapper;
using Financify_Api.Models.Enums.Extensions;
using Financify_Api.Models.Responses;

namespace Financify_Api.Models.Profiles
{
    public class ChargeProfile : Profile
    {
        public ChargeProfile()
        {
            CreateMap<Charge, Charge>().ForMember(c => c.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDescription()));
            CreateMap<Charge, ChargeResponse>();
        }
    }
}
