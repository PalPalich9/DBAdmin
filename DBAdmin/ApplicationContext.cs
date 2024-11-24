using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DBAdmin.Model;


namespace DBAdmin
{
    public class ApplicationContext : DbContext
    {
        public DbSet<LegoSet> LegoSets { get; set; }

        public DbSet<LegoCollection> LegoCollections { get; set; }

        public DbSet<User> Users { get; set; }
        //public DbSet<Order> Orders { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=legopass");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LegoSet>()
                .HasKey(ls => ls.article); // Указываем, что article является первичным ключом
            modelBuilder.Entity<LegoSet>()
            .ToTable("legoSet");

            modelBuilder.Entity<LegoCollection>()
                .HasKey(ls => ls.id); // Указываем, что id является первичным ключом
            modelBuilder.Entity<LegoCollection>()
            .ToTable("legoCollection");

            modelBuilder.Entity<User>()
                .HasKey(ls => ls.id); // Указываем, что id является первичным ключом
            modelBuilder.Entity<User>()
            .ToTable("user");
/*
            modelBuilder.Entity<Order>()
               .HasKey(ls => ls.id); // Указываем, что id является первичным ключом
            modelBuilder.Entity<Order>()
            .ToTable("order");*/
        }
    }
}
