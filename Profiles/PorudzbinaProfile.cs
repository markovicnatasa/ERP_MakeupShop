using AutoMapper;
using MakeupShop.Entities;
using MakeupShop.Models;

namespace MakeupShop.Profiles
{
    public class PorudzbinaProfile : Profile
    {
        public PorudzbinaProfile()
        {
            CreateMap<Porudzbina, PorudzbinaDto>();
            CreateMap<PorudzbinaDto, Porudzbina>();
            CreateMap<Porudzbina, PorudzbinaCreateDto>();
            CreateMap<PorudzbinaCreateDto, Porudzbina>();
            CreateMap<Porudzbina, PorudzbinaUpdateDto>();
            CreateMap<PorudzbinaUpdateDto, Porudzbina>();
            CreateMap<Porudzbina, KorisnikDto>();
            CreateMap<KorisnikDto, Porudzbina>();
            CreateMap<Porudzbina, Porudzbina>();
        }
    }
}
