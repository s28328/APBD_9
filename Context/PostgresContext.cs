﻿using System;
using System.Collections.Generic;
using APBD_9.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_9.Contex;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientTrip> ClientTrips { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=postgres;User ID=postgres;Password=admin;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Idclient).HasName("client_pkey");

            entity.ToTable("client");

            entity.Property(e => e.Idclient).HasColumnName("idclient");
            entity.Property(e => e.Email)
                .HasMaxLength(120)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(120)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(120)
                .HasColumnName("lastname");
            entity.Property(e => e.Pesel)
                .HasMaxLength(120)
                .HasColumnName("pesel");
            entity.Property(e => e.Telephone)
                .HasMaxLength(120)
                .HasColumnName("telephone");
            
        });

        modelBuilder.Entity<ClientTrip>(entity =>
        {
            entity.HasKey(e => new { e.Idclient, e.Idtrip }).HasName("client_trip_pkey");

            entity.ToTable("client_trip");

            entity.Property(e => e.Idclient).HasColumnName("idclient");
            entity.Property(e => e.Idtrip).HasColumnName("idtrip");
            entity.Property(e => e.Paymentdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("paymentdate");
            entity.Property(e => e.Registeredat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("registeredat");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.ClientTrips)
                .HasForeignKey(d => d.Idclient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("client_trip_idclient_fkey");

            entity.HasOne(d => d.IdtripNavigation).WithMany(p => p.ClientTrips)
                .HasForeignKey(d => d.Idtrip)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("client_trip_idtrip_fkey");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Idcountry).HasName("country_pkey");

            entity.ToTable("country");

            entity.Property(e => e.Idcountry).HasColumnName("idcountry");
            entity.Property(e => e.Name)
                .HasMaxLength(120)
                .HasColumnName("name");

            entity.HasMany(d => d.Idtrips).WithMany(p => p.Countries)
                .UsingEntity<Dictionary<string, object>>(
                    "CountryTrip",
                    r => r.HasOne<Trip>().WithMany()
                        .HasForeignKey("Idtrip")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("country_trip_idtrip_fkey"),
                    l => l.HasOne<Country>().WithMany()
                        .HasForeignKey("Idcountry")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("country_trip_idcountry_fkey"),
                    j =>
                    {
                        j.HasKey("Idcountry", "Idtrip").HasName("country_trip_pkey");
                        j.ToTable("country_trip");
                        j.IndexerProperty<int>("Idcountry").HasColumnName("idcountry");
                        j.IndexerProperty<int>("Idtrip").HasColumnName("idtrip");
                    });
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.Idtrip).HasName("trip_pkey");

            entity.ToTable("trip");

            entity.Property(e => e.Idtrip).HasColumnName("idtrip");
            entity.Property(e => e.Datefrom)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datefrom");
            entity.Property(e => e.Dateto)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateto");
            entity.Property(e => e.Description)
                .HasMaxLength(220)
                .HasColumnName("description");
            entity.Property(e => e.Maxpeople).HasColumnName("maxpeople");
            entity.Property(e => e.Name)
                .HasMaxLength(120)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
