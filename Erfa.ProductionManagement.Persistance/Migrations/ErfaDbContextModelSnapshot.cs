﻿// <auto-generated />
using System;
using Erfa.ProductionManagement.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Erfa.ProductionManagement.Persistence.Migrations
{
    [DbContext(typeof(ErfaDbContext))]
    partial class ErfaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Erfa.ProductionManagement.Domain.Entities.Product", b =>
                {
                    b.Property<string>("ProductNumber")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MaterialProductName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("ProductionTimeSec")
                        .HasColumnType("double precision");

                    b.HasKey("ProductNumber");

                    b.ToTable("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
