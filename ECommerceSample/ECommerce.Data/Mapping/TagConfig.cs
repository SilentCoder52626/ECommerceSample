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
    public class TagConfig : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> modelBuilder)
        {
            modelBuilder
                .ToTable("tag")
                .Property(c => c.TagId)
                .HasColumnName("id");
            modelBuilder
                .ToTable("tag")
                .HasKey(c => c.TagId)
                .HasName("id");

            modelBuilder
                .ToTable("tag")
                .Property(c => c.Name)
                .HasColumnName("name")
                .IsRequired();
            modelBuilder
                .ToTable("tag")
                .HasIndex(c => c.Name).IsUnique();
            modelBuilder
                .ToTable("tag")
                .HasMany(c => c.Products)
                .WithOne(c => c.Tag);

        }
    }
}
