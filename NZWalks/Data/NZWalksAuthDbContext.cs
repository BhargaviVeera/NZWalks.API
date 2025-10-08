using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NZWalks.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        //public NZWalksAuthDbContext(DbContextOptions options) : base(options)
        //{
        //}

        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var readerRoleId = "8677164c-0ede-4eed-ad8c-fea2c11e6c8c";
            var writerRoleId = "df3d5746-6255-417b-9669-5b5c4d75faf1";

            //Seed data for Roles
            var roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
