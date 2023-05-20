using AutoMapper;
using MakeupShop.Entities;
using MakeupShop.Models;

namespace MakeupShop.Profiles
{
    public class StavkaPorudzbineProfile : Profile
    {
        public StavkaPorudzbineProfile()
        {
            CreateMap<StavkaPorudzbine, StavkaPorudzbineDto>();
            CreateMap<StavkaPorudzbineDto, StavkaPorudzbine>();
            CreateMap<StavkaPorudzbine, StavkaPorudzbineCreateDto>();
            CreateMap<StavkaPorudzbineCreateDto, StavkaPorudzbine>();
            CreateMap<StavkaPorudzbine, StavkaPorudzbineUpdateDto>();
            CreateMap<StavkaPorudzbineUpdateDto, StavkaPorudzbine>();
            CreateMap<StavkaPorudzbine, ProizvodDto>();
            CreateMap<ProizvodDto, StavkaPorudzbine>();
            CreateMap<StavkaPorudzbine, PorudzbinaDto>();
            CreateMap<PorudzbinaDto, StavkaPorudzbine>();
            CreateMap<StavkaPorudzbine, StavkaPorudzbine>();
        }

    }
}
