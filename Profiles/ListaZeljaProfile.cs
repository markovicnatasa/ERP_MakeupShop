using AutoMapper;
using MakeupShop.Entities;
using MakeupShop.Models;

namespace MakeupShop.Profiles
{
    public class ListaZeljaProfile : Profile
    {
        public ListaZeljaProfile()
        {
            CreateMap<ListaZelja, ListaZeljaDto>();
            CreateMap<ListaZeljaDto, ListaZelja>();
            CreateMap<ListaZelja, ListaZeljaCreateDto>();
            CreateMap<ListaZeljaCreateDto, ListaZelja>();
            CreateMap<ListaZelja, ListaZeljaUpdateDto>();
            CreateMap<ListaZeljaUpdateDto, ListaZelja>();
            CreateMap<ListaZelja, ListaZelja>();
        }
    }
}
