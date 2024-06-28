using CrudApplication.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrudApplication.Data
{
    public class AppDbContex : DbContext
    {

        public AppDbContex(DbContextOptions<AppDbContex> options) : base(options)
        {
        }

        public DbSet <Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Country> Countries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Contacts)
                .WithOne(e => e.Company)
                .HasForeignKey(e => e.CompanyId);

            modelBuilder.Entity<Country>()
                 .HasMany(c => c.Contacts)
                 .WithOne(e => e.Country)
                 .HasForeignKey(e => e.CountryId);

        }
    }

}
