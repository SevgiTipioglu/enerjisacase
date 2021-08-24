using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnerjiSA.GenerationService.Data
{   
    public class EnerjiSaContextFactory : IDesignTimeDbContextFactory<EnerjiSaContext>
    {
        public EnerjiSaContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<EnerjiSaContext>();
            string connectionString = "Server=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres;";
            builder.UseNpgsql(connectionString);
            return new EnerjiSaContext(builder.Options);
        }
    }
}
