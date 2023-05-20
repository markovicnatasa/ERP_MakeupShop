using AutoMapper;
using MakeupShop.Entities;
using MakeupShop.Models;

namespace MakeupShop.Profiles
{
    public class ListaZeljaProizvodProfile : Profile
    {
        public ListaZeljaProizvodProfile()
        {
            CreateMap<ListaZeljaProizvod, ListaZeljaProizvodDto>();
            CreateMap<ListaZeljaProizvodDto, ListaZeljaProizvod>();
            CreateMap<ListaZeljaProizvod, ListaZeljaProizvodCreateDto>();
            CreateMap<ListaZeljaProizvodCreateDto, ListaZeljaProizvod>();
            CreateMap<ListaZeljaProizvod, ListaZeljaProizvodUpdateDto>();
            CreateMap<ListaZeljaProizvodUpdateDto, ListaZeljaProizvod>();
            CreateMap<ListaZeljaProizvod, ProizvodDto>();
            CreateMap<ProizvodDto, ListaZeljaProizvod>();
            CreateMap<ListaZeljaProizvod, ListaZeljaDto>();
            CreateMap<ListaZeljaDto, ListaZeljaProizvod>();
            CreateMap<ListaZeljaProizvod, KorisnikDto>();
            CreateMap<KorisnikDto, ListaZeljaProizvod>();
            CreateMap<ListaZeljaProizvod, ListaZeljaProizvod>();
        }
    }
}
