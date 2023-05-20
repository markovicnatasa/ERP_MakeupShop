using AutoMapper;
using MakeupShop.Entities;
using MakeupShop.Models;

namespace MakeupShop.Profiles
{
    public class RecenzijaProfile : Profile
    {
        public RecenzijaProfile()
        {
            CreateMap<Recenzija, RecenzijaDto>();
            CreateMap<RecenzijaDto, Recenzija>();
            CreateMap<Recenzija, RecenzijaCreateDto>();
            CreateMap<RecenzijaCreateDto, Recenzija>();
            CreateMap<Recenzija, RecenzijaUpdateDto>();
            CreateMap<RecenzijaUpdateDto, Recenzija>();
            CreateMap<Recenzija, ProizvodDto>();
            CreateMap<ProizvodDto, Recenzija>();
            CreateMap<Recenzija, KorisnikDto>();
            CreateMap<KorisnikDto, Recenzija>();
            CreateMap<Recenzija, Recenzija>();
        }
    }
}
