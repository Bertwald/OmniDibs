﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Models {
    internal class OmniDibsContext : DbContext {
        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Person> Persons { get; set; } = null!;
        public DbSet<Flight> Flights { get; set; } = null!;
        public DbSet<Airplane> Airplanes { get; set; } = null!;
        public DbSet<Seat> Seats { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<AirplaneBooking> AirplaneBookings { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=OmniDibs;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Ticket>().ToTable("Tickets");
            modelBuilder.Entity<AirplaneBooking>().ToTable("AirPlaneBookings");
            //modelBuilder.Entity<Booking>().Navigation(e => e.Account).AutoInclude(); //Creates Cycle
            //modelBuilder.Entity<Flight>().Navigation(e => e.Origin).AutoInclude();
            //modelBuilder.Entity<Flight>().Navigation(e => e.Destination).AutoInclude();
            modelBuilder.Entity<Ticket>().Navigation(e => e.Seat).AutoInclude();
            modelBuilder.Entity<Ticket>().Navigation(e => e.Flight).AutoInclude();
            modelBuilder.Entity<AirplaneBooking>().Navigation(e => e.Airplane).AutoInclude();
            //modelBuilder.Entity<Airplane>().Navigation(e => e.Seats).AutoInclude();

            // SHOW: Configure two way references
            modelBuilder.Entity<Flight>()
            .HasOne(e => e.Origin)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Flight>()
            .HasOne(e => e.Destination)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Flight>()
            .HasOne(e => e.Airplane)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Account>()
            .HasIndex(b => b.UserName)
            .IsUnique();
        }
    }
}
