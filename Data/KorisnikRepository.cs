using AutoMapper;
using MakeupShop.Entities;

namespace MakeupShop.Data
{
    public class KorisnikRepository : IKorisnikRepository
    {
        private readonly MakeupShopContext context;
        private readonly IMapper mapper;
        public KorisnikRepository(MakeupShopContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public Korisnik CreateKorisnik(Korisnik korisnik)
        {
            var createdEntity = context.Add(korisnik);
            context.SaveChanges();
            return mapper.Map<Korisnik>(createdEntity.Entity);
        }

        public void DeleteKorisnik(int korisnikID)
        {
            var korisnik = GetKorisnikById(korisnikID);
            context.Remove(korisnik);
            context.SaveChanges();
        }
        public Korisnik GetKorisnikById(int korisnikID)
        {
            return context.Korisnik.FirstOrDefault(k => k.korisnikID == korisnikID);
        }

        public List<Korisnik> GetKorisnikList()
        {
            return context.Korisnik.ToList();
        }
        public void UpdateKorisnik(Korisnik korisnik)
        {
            //nije potrebno posebno implementirati update
        }
    }
}

