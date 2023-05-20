using AutoMapper;
using MakeupShop.Entities;

namespace MakeupShop.Data
{
    public class ListaZeljaProizvodRepository : IListaZeljaProizvodRepository
    {
        private readonly MakeupShopContext context;
        private readonly IMapper mapper;
        public ListaZeljaProizvodRepository(MakeupShopContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public ListaZeljaProizvod CreateListaZeljaProizvod(ListaZeljaProizvod listaZeljaProizvod)
        {
            var createdEntity = context.Add(listaZeljaProizvod);
            context.SaveChanges();
            return mapper.Map<ListaZeljaProizvod>(createdEntity.Entity);
        }

        public void DeleteListaZeljaProizvod(int listaProID)
        {
            var listaZeljaProizvod = GetListaZeljaProizvodById(listaProID);
            context.Remove(listaZeljaProizvod);
            context.SaveChanges();
        }
        public ListaZeljaProizvod GetListaZeljaProizvodById(int listaProID)
        {
            return context.ListaZeljaProizvod.FirstOrDefault(l => l.listaProID == listaProID);
        }

        public List<ListaZeljaProizvod> GetListaZeljaProizvodList()
        {
            return context.ListaZeljaProizvod.ToList();
        }
        public void UpdateListaZeljaProizvod(ListaZeljaProizvod listaZeljaProizvod)
        {
            //nije potrebno posebno implementirati update
        }

        public List<ListaZeljaProizvod> GetAllListaZeljaProizvod(int korisnikID)
        {
            var list = from listaZeljaProizvod in context.ListaZeljaProizvod
                       join
                       listaZelja in context.ListaZelja on listaZeljaProizvod.listaZeljaID equals listaZelja.listaZeljaID
                       join korisnik in context.Korisnik on listaZelja.korisnikID equals korisnik.korisnikID
                       where listaZelja.korisnikID == korisnikID
                       select new ListaZeljaProizvod
                       {
                           listaProID = listaZeljaProizvod.listaProID,
                           listaZeljaID = listaZeljaProizvod.listaZeljaID,
                           proizvodID = listaZeljaProizvod.proizvodID,

                       };
            return list.ToList();
        }
    }
}

