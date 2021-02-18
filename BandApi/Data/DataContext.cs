using BandApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {

        }
       public DbSet<band> bands { get; set; }
       public DbSet<album> albums { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<band>().HasData(
                new band {Id=Guid.Parse("f2e4cffc-fcc7-11ea-adc1-0242ac120002"),Name="Metallica",Founded=new DateTime(1988,5,6),MainGenre="HeavyMetal" },
                new band { Id = Guid.Parse("f2e4d240-fcc7-11ea-adc1-0242ac120002"), Name = "Guns N Roses", Founded = new DateTime(1985, 2, 1), MainGenre = "Rock" },
                new band { Id = Guid.Parse("f2e4d344-fcc7-11ea-adc1-0242ac120002"), Name = "ABBA", Founded = new DateTime(1965, 7, 1), MainGenre = "Disco" },
                new band { Id = Guid.Parse("f2e4d416-fcc7-11ea-adc1-0242ac120002"), Name = "Oasis", Founded = new DateTime(1985, 2, 1), MainGenre = "Alternative" },
                new band { Id = Guid.Parse("f2e4d4de-fcc7-11ea-adc1-0242ac120002"), Name = "A-ha", Founded = new DateTime(1981, 2, 1), MainGenre = "Pop" }
                );
            modelBuilder.Entity<album>().HasData(
                new album{Id=Guid.Parse("7547c210-fcc8-11ea-adc1-0242ac120002"),Title="master of puppets", Description="one of the best bands ever",BandId=Guid.Parse("f2e4cffc-fcc7-11ea-adc1-0242ac120002") }, 
                new album { Id = Guid.Parse("7547c45e-fcc8-11ea-adc1-0242ac120002"), Title = "appetite for destrcution", Description = "amazing rock albumn with raw sound", BandId = Guid.Parse("f2e4cffc-fcc7-11ea-adc1-0242ac120002") },
                new album { Id = Guid.Parse("7547c558-fcc8-11ea-adc1-0242ac120002"), Title = "waterloo", Description = "very gloomy albumn", BandId = Guid.Parse("f2e4cffc-fcc7-11ea-adc1-0242ac120002") }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
