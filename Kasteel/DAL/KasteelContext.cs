using Kasteel.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Kasteel.DAL
{
    public class KasteelContext(DbContextOptions<KasteelContext> options) : DbContext(options)
    {
        public virtual DbSet<Castle> Castles { get; set; } = null!;
        public virtual DbSet<King> Kings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed database
            modelBuilder.Entity<Castle>().HasData(GetCastleSeedData());
            modelBuilder.Entity<King>().HasData(GetKingSeedData());
        }

        private static Castle[] GetCastleSeedData()
        {
            using var r = new StreamReader("Files/kasteel.json");
            return JsonConvert.DeserializeObject<Castle[]>(r.ReadToEnd()) ?? [];
        }

        private static King[] GetKingSeedData()
        {
            using var r = new StreamReader("Files/koning.json");
            return JsonConvert.DeserializeObject<King[]>(r.ReadToEnd()) ?? [];
        }
    }
}
