using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorSearchBackend.DAL
{
    public class ApplicationContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Donation> Donations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Donorsearch;Username=postgres;Password=postgres");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
         .HasIndex(u => u.VkId)
         .IsUnique();

            modelBuilder.Entity<User>()
            .HasIndex(u => u.DSId)
            .IsUnique();

            modelBuilder.Entity<Donation>()
            .HasIndex(u => u.Id)
            .IsUnique();
        }
    }
}
