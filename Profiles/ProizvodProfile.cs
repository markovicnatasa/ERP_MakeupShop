using AutoMapper;
using MakeupShop.Entities;
using MakeupShop.Models;

namespace MakeupShop.Profiles
{
    public class ProizvodProfile : Profile
    {
        public ProizvodProfile()
        {
            CreateMap<Proizvod, ProizvodDto>();
            CreateMap<ProizvodDto, Proizvod>();
            CreateMap<Proizvod, ProizvodCreateDto>();
            CreateMap<ProizvodCreateDto, Proizvod>();
            CreateMap<Proizvod, ProizvodUpdateDto>();
            CreateMap<ProizvodUpdateDto, Proizvod>();
            CreateMap<Proizvod, BrendDto>();
            CreateMap<BrendDto, Proizvod>();
            CreateMap<Proizvod, Proizvod>();
        }
    }
}
