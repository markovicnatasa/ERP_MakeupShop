using AutoMapper;
using MakeupShop.Entities;

namespace MakeupShop.Data
{
    public class StavkaPorudzbineRepository : IStavkaPorudzbineRepository
    {
        private readonly MakeupShopContext context;
        private readonly IMapper mapper;
        public StavkaPorudzbineRepository(MakeupShopContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public StavkaPorudzbine CreateStavkaPorudzbine(StavkaPorudzbine stavkaPorudzbine)
        {
            var createdEntity = context.Add(stavkaPorudzbine);
            context.SaveChanges();
            return mapper.Map<StavkaPorudzbine>(createdEntity.Entity);
        }

        public void DeleteStavkaPorudzbine(int stavkaPorudzbineID)
        {
            var stavkaPorudzbine = GetStavkaPorudzbineById(stavkaPorudzbineID);
            context.Remove(stavkaPorudzbine);
            context.SaveChanges();
        }
        public StavkaPorudzbine GetStavkaPorudzbineById(int stavkaPorudzbineID)
        {
            return context.StavkaPorudzbine.FirstOrDefault(s => s.stavkaPorudzbineID == stavkaPorudzbineID);
        }

        public List<StavkaPorudzbine> GetStavkaPorudzbineList()
        {
            return context.StavkaPorudzbine.ToList();
        }
        public void UpdateStavkaPorudzbine(StavkaPorudzbine stavkaPorudzbine)
        {
            //nije potrebno posebno implementirati update
        }

        public List<StavkaPorudzbine> GetAllStavkaPorudzbine(int korisnikID)
        {
            var list = from stavkaPorudzbine in context.StavkaPorudzbine
                       join
                       porudzbina in context.Porudzbina on stavkaPorudzbine.porudzbinaID equals porudzbina.porudzbinaID
                       join korisnik in context.Korisnik on porudzbina.korisnikID equals korisnik.korisnikID
                       where porudzbina.korisnikID == korisnikID
                       select new StavkaPorudzbine
                       {
                           stavkaPorudzbineID = stavkaPorudzbine.stavkaPorudzbineID,
                           porudzbinaID = stavkaPorudzbine.porudzbinaID,
                           proizvodID = stavkaPorudzbine.proizvodID,
                           kolicinaProizvoda = stavkaPorudzbine.kolicinaProizvoda

                       };
            return list.ToList();
        }
    }
}
