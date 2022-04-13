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
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .ToTable("product")
                .Property(c => c.ProductId)
                .HasColumnName("id");
            builder
                .ToTable("product")
                .HasKey(c => c.ProductId)
                .HasName("id");

            builder
                .ToTable("product")
                .Property(c => c.Name)
                .HasColumnName("name")
                .IsRequired();
            builder
                .ToTable("product")
                .Property(c => c.SKU)
                .HasColumnName("sku")
                .IsRequired();
            builder
                .ToTable("product")
                .HasIndex(c => c.SKU).IsUnique();
            builder
                .ToTable("product")
                .Property(c => c.Description)
                .HasColumnName("description")
                .IsRequired();
            builder
                .ToTable("product")
                .Property(c => c.Color)
                .HasColumnName("color")
                .IsRequired();
            builder
                .ToTable("product")
                .Property(c => c.Image)
                .HasColumnName("image")
                .IsRequired();
            builder
                .ToTable("product")
                .Property(c => c.CreatedDate)
                .HasColumnName("created_date")
                .IsRequired();
            builder
                .ToTable("product")
                .Property(c => c.Price)
                .HasColumnName("price")
                .IsRequired();
            builder
                .ToTable("product")
                .Property(c => c.AvailabilityStatus)
                .HasColumnName("availability_status")
                .IsRequired();
            builder
                 .ToTable("product")
                 .Property(c => c.BrandId)
                 .HasColumnName("brand_id");
            builder
                .ToTable("product")
                .HasOne(a => a.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(c => c.BrandId);

            builder
                 .ToTable("product")
                 .Property(c => c.CategoryId)
                 .HasColumnName("category_id");
            builder
                .ToTable("product")
                .HasOne(a => a.Category)
                .WithMany(b => b.Products)
                .HasForeignKey(c => c.CategoryId);

            builder
                 .ToTable("product")
                 .Property(c => c.TagId)
                 .HasColumnName("tag_id");

            builder
                .ToTable("product")
                .HasOne(a => a.Tag)
                .WithMany(b => b.Products)
                .HasForeignKey(c => c.TagId);
        }
    }
}
