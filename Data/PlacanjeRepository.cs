using AutoMapper;
using MakeupShop.Entities;

namespace MakeupShop.Data
{
    public class PlacanjeRepository : IPlacanjeRepository
    {
        private readonly MakeupShopContext context;
        private readonly IMapper mapper;
        public PlacanjeRepository(MakeupShopContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public Placanje CreatePlacanje(Placanje placanje)
        {
            var createdEntity = context.Add(placanje);
            context.SaveChanges();
            return mapper.Map<Placanje>(createdEntity.Entity);
        }

        public void DeletePlacanje(int placanjeID)
        {
            var placanje = GetPlacanjeById(placanjeID);
            context.Remove(placanje);
            context.SaveChanges();
        }
        public Placanje GetPlacanjeById(int placanjeID)
        {
            return context.Placanje.FirstOrDefault(p => p.placanjeID == placanjeID);
        }

        public List<Placanje> GetPlacanjeList()
        {
            return context.Placanje.ToList();
        }
        public void UpdatePlacanje(Placanje placanje)
        {
            //nije potrebno posebno implementirati update
        }
    }
}

