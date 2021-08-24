using EnerjiSA.GenerationService.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnerjiSA.GenerationService.Data.Configurations
{
    public class PowerPlantDataConfiguration : IEntityTypeConfiguration<PowerPlantData>
    {
        public void Configure(EntityTypeBuilder<PowerPlantData> builder)
        {

            #region Primary Key

            builder.HasKey(q => q.Id);

            #endregion           

            #region Properties

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.PowerPlantId).IsRequired();

            #endregion

            #region Relations

            builder.HasOne(x => x.PowerPlant).WithMany(x => x.PowerPlantDatas).HasForeignKey(s => s.PowerPlantId);

            #endregion

            #region Table & Column Mappings

            builder.ToTable("PowerPlantData");

            #endregion
        }
    }

}
