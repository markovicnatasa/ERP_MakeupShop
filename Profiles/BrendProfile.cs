using AutoMapper;
using MakeupShop.Entities;
using MakeupShop.Models;

namespace MakeupShop.Profiles
{
    public class BrendProfile : Profile
    {
        public BrendProfile() 
        {
            CreateMap<Brend, BrendDto>();
            CreateMap<BrendDto, Brend>();
            CreateMap<Brend, BrendCreateDto>();
            CreateMap<BrendCreateDto, Brend>();
            CreateMap< Brend, BrendUpdateDto>();
            CreateMap<BrendUpdateDto, Brend>();
            CreateMap< Brend, Brend > ();


         }
    }
}
