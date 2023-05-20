using AutoMapper;
using MakeupShop.Entities;

namespace MakeupShop.Data
{
    public class RecenzijaRepository : IRecenzijaRepository
    {
        private readonly MakeupShopContext context;
        private readonly IMapper mapper;
        public RecenzijaRepository(MakeupShopContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public Recenzija CreateRecenzija(Recenzija recenzija)
        {
            var createdEntity = context.Add(recenzija);
            context.SaveChanges();
            return mapper.Map<Recenzija>(createdEntity.Entity);
        }

        public void DeleteRecenzija(int recenzijaID)
        {
            var recenzija = GetRecenzijaById(recenzijaID);
            context.Remove(recenzija);
            context.SaveChanges();
        }
        public Recenzija GetRecenzijaById(int recenzijaID)
        {
            return context.Recenzija.FirstOrDefault(r => r.recenzijaID == recenzijaID);
        }

        public List<Recenzija> GetRecenzijaList()
        {
            return context.Recenzija.ToList();
        }
        public void UpdateRecenzija(Recenzija recenzija)
        {
            //nije potrebno posebno implementirati update
        }
    }
}
