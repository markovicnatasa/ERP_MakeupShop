using AutoMapper;
using MakeupShop.Entities;
using MakeupShop.Models;
using System.Security.Claims;

namespace MakeupShop.Profiles
{
    public class KorisnikProfile : Profile
    {
        public KorisnikProfile()
        {
            CreateMap<Korisnik, KorisnikDto>();
            CreateMap<KorisnikDto, Korisnik>();
            CreateMap<Korisnik, KorisnikCreateDto>();
            CreateMap<KorisnikCreateDto, Korisnik>();
            CreateMap<Korisnik, KorisnikUpdateDto>();
            CreateMap<KorisnikUpdateDto, Korisnik>();
            CreateMap<Korisnik, Korisnik>();
        }
    }
}
