using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Data.Configurations
{
    internal class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser 
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(g=>g.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);


            builder.Property(g => g.Phone)
                .HasColumnType("varchar")
                .HasMaxLength(11);


            builder.Property(g => g.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100);


            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("CK_GymUser_Email", "Email LIKE '_%@_%._%'");
                tb.HasCheckConstraint("CK_GymUser_Phone", "Phone LIKE '01%' and Phone Not LIKE '%[^0-9]%' ");
            });
            builder.HasIndex(g => g.Email).IsUnique();
            builder.HasIndex(g => g.Phone).IsUnique();


            builder.OwnsOne(g=>g.Address , AddressBuilder =>
            {
                AddressBuilder.Property(a => a.Street)
                    .HasColumnType("varchar")
                    .HasMaxLength(30)
                    .HasColumnName("Street");

                AddressBuilder.Property(a => a.City)
                    .HasColumnType("varchar")
                    .HasMaxLength(30)
                    .HasColumnName("City");

                AddressBuilder.Property(a => a.BuildingNumber)
                    .HasColumnName("BuildingNumber");

            });
        }
    }
}
