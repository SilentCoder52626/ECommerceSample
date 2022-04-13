using ECommerce.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Mapping
{
    public class BrandConfig : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> modelBuilder)
        {
            modelBuilder
                .ToTable("brand")
                .Property(c => c.BrandId)
                .HasColumnName("id");
            modelBuilder
                .ToTable("brand")
                .HasKey(c => c.BrandId)
                .HasName("id");

            modelBuilder
                .ToTable("brand")
                .Property(c => c.Name)
                .HasColumnName("name")
                .IsRequired();
            modelBuilder
                .ToTable("brand")
                .HasMany(c => c.Products)
                .WithOne(c => c.Brand);


        }
    }
}
