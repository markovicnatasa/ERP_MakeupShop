using AutoMapper;
using MakeupShop.Entities;

namespace MakeupShop.Data
{
    public class ListaZeljaRepository : IListaZeljaRepository
    {
        private readonly MakeupShopContext context;
        private readonly IMapper mapper;
        public ListaZeljaRepository(MakeupShopContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public ListaZelja CreateListaZelja(ListaZelja listaZelja)
        {
            var createdEntity = context.Add(listaZelja);
            context.SaveChanges();
            return mapper.Map<ListaZelja>(createdEntity.Entity);
        }

        public void DeleteListaZelja(int listaZeljaID)
        {
            var listaZelja = GetListaZeljaById(listaZeljaID);
            context.Remove(listaZelja);
            context.SaveChanges();
        }
        public ListaZelja GetListaZeljaById(int listaZeljaID)
        {
            return context.ListaZelja.FirstOrDefault(l => l.listaZeljaID == listaZeljaID);
        }

        public List<ListaZelja> GetListaZeljaList()
        {
            return context.ListaZelja.ToList();
        }
        public void UpdateListaZelja(ListaZelja listaZelja)
        {
            //nije potrebno posebno implementirati update
        }
    }
}
