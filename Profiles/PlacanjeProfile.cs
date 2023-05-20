using AutoMapper;
using MakeupShop.Entities;
using MakeupShop.Models;

namespace MakeupShop.Profiles
{
    public class PlacanjeProfile : Profile
    {
        public PlacanjeProfile()
        {
            CreateMap<Placanje, PlacanjeDto>();
            CreateMap<PlacanjeDto, Placanje>();
            CreateMap<Placanje, PlacanjeCreateDto>();
            CreateMap<PlacanjeCreateDto, Placanje>();
            CreateMap<Placanje, PlacanjeUpdateDto>();
            CreateMap<PlacanjeUpdateDto, Placanje>();
            CreateMap<Placanje, PorudzbinaDto>();
            CreateMap<PorudzbinaDto, Placanje>();
            CreateMap<Placanje, Placanje>();
        }
    }
}
