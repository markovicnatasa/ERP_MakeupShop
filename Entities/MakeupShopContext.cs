using Microsoft.EntityFrameworkCore;

namespace MakeupShop.Entities
{
    public class MakeupShopContext : DbContext
    {
        public readonly IConfiguration configuration;
        public MakeupShopContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Brend> Brend { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<ListaZelja> ListaZelja { get; set; }
        public DbSet<ListaZeljaProizvod> ListaZeljaProizvod { get; set; }
        public DbSet<Placanje> Placanje { get; set; }
        public DbSet<Porudzbina> Porudzbina { get; set; }
        public DbSet<Proizvod> Proizvod { get; set; }
        public DbSet<Recenzija> Recenzija { get; set; }
        public DbSet<StavkaPorudzbine> StavkaPorudzbine { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MakeupShop"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StavkaPorudzbine>().ToTable(tb => tb.HasTrigger("dostupnostProizvoda"));
            modelBuilder.Entity<Placanje>().ToTable(tb => tb.HasTrigger("tacnostDatumaPlacanja"));
        }
    }
}
