using EnerjiSA.GenerationService.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnerjiSA.GenerationService.Data
{
    public class EnerjiSaContext : DbContext
    {
        public DbSet<PowerPlant> PowerPlants { get; set; }
        public DbSet<PowerPlantData> PowerPlantDatas { get; set; }

        public EnerjiSaContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EnerjiSaContext).Assembly);
        }
    }
}
