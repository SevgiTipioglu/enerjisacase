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
    public class PowerPlantConfiguration : IEntityTypeConfiguration<PowerPlant>
    {
        public void Configure(EntityTypeBuilder<PowerPlant> builder)
        {

            #region Primary Key

            builder.HasKey(q => q.Id);

            #endregion

            #region Index

            builder.HasIndex(x => x.WebId).IsUnique().HasDatabaseName("IX_WEBID");

            #endregion

            #region Properties

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.WebId).IsRequired().HasMaxLength(50);

            #endregion

            #region Table & Column Mappings

            builder.ToTable("PowerPlant");

            #endregion

            builder.HasMany(x => x.PowerPlantDatas).WithOne(x=>x.PowerPlant).HasForeignKey(x=>x.PowerPlantId).OnDelete(DeleteBehavior.Cascade);

           // modelBuilder.Entity<Post>().HasRequired(n => n.Blog)
           //.WithMany(n => n.Posts)
           //.HasForeignKey(n => n.BlogId)
           //.WillCascadeOnDelete(true);
        }
    }
}
