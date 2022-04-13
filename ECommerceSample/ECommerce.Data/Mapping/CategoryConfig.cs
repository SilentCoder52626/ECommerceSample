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
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> modelBuilder)
        {
            modelBuilder
                .ToTable("category")
                .Property(c => c.CategoryId)
                .HasColumnName("id");
            modelBuilder
                .ToTable("category")
                .HasKey(c => c.CategoryId)
                .HasName("id");

            modelBuilder
                .ToTable("category")
                .Property(c => c.Name)
                .HasColumnName("name")
                .IsRequired();
            modelBuilder
                 .ToTable("category")
                 .HasMany(c => c.Products)
                 .WithOne(c => c.Category);
        }
    }
}
