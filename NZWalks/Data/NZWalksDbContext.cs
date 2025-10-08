using Microsoft.EntityFrameworkCore;
using NZWalks.Models.Domain;


namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        //public NZWalksDbContext(DbContextOptions options) : base(options)
        //{
        //}

        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for Difficulty
            var difficlulties = new List<Difficulty>()
            {
                new Difficulty() {
                    Id = Guid.Parse("cbd26945-88a4-40ec-a9cc-ddf8d53488d9"),
                    Name = "Hard"
                },
                new Difficulty() {
                    Id = Guid.Parse("3779ac0a-c678-4edb-8da6-e7e6f23d2108"),
                    Name = "Medium"
                },
                new Difficulty() {
                    Id = Guid.Parse("ba2944d5-c08c-47a1-be9e-f1e575f979d5"),
                    //Id= Guid.NewGuid(),
                    Name = "Easy"
                }
            };

            modelBuilder.Entity<Difficulty>().HasData(difficlulties);

        }
    }
}
