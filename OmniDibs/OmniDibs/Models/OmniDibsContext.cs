using Microsoft.EntityFrameworkCore;

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
            optionsBuilder.UseSqlServer(@"Server=tcp:thla.database.windows.net,1433;Initial Catalog=THLA;Persist Security Info=False;User ID=thlaAdmin;Password=Admin_thla;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Ticket>().ToTable("Tickets");
            modelBuilder.Entity<AirplaneBooking>().ToTable("AirPlaneBookings");
            modelBuilder.Entity<Ticket>().Navigation(e => e.Seat).AutoInclude();
            modelBuilder.Entity<Ticket>().Navigation(e => e.Flight).AutoInclude();
            modelBuilder.Entity<AirplaneBooking>().Navigation(e => e.Airplane).AutoInclude();

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
