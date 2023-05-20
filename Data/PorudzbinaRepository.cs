using AutoMapper;
using MakeupShop.Entities;

namespace MakeupShop.Data
{
    public class PorudzbinaRepository : IPorudzbinaRepository
    {
        private readonly MakeupShopContext context;
        private readonly IMapper mapper;
        public PorudzbinaRepository(MakeupShopContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public Porudzbina CreatePorudzbina(Porudzbina porudzbina)
        {
            var createdEntity = context.Add(porudzbina);
            context.SaveChanges();
            return mapper.Map<Porudzbina>(createdEntity.Entity);
        }

        public void DeletePorudzbina(int porudzbinaID)
        {
            var porudzbina = GetPorudzbinaById(porudzbinaID);
            context.Remove(porudzbina);
            context.SaveChanges();
        }
        public Porudzbina GetPorudzbinaById(int porudzbinaID)
        {
            return context.Porudzbina.FirstOrDefault(p => p.porudzbinaID == porudzbinaID);
        }

        public List<Porudzbina> GetPorudzbinaList()
        {
            return context.Porudzbina.ToList();
        }
        public void UpdatePorudzbina(Porudzbina porudzbina)
        {
            //nije potrebno posebno implementirati update
        }

        public List<Porudzbina> GetAllPorudzbinaList(int korisnikID)
        {
            return context.Porudzbina.Where(p => p.korisnikID == korisnikID).ToList();        
        }
    }
}
