using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Models {
    public abstract class Booking {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        internal Account OrderAccount { get; set; } = null!;
        public abstract DateTime StartDate { get; }
        public abstract DateTime? EndDate { get; }
        internal abstract string GetBookingInfo();
    }
    public class Flight {
        internal Flight() {
            Tickets = new HashSet<Ticket>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; internal set; } = null!;
        public DateTime Departure { get; internal set; }
        public DateTime Arrival { get; internal set; }
        [Column]
        public Country Origin { get; internal set; } = null!;
        [Column]
        public Country Destination { get; internal set; } = null!;
        [Column]
        public Airplane Airplane { get; internal set; } = null!;
        public float BaseCost { get; internal set; }
        public ISet<Ticket> Tickets { get; internal set; }
    }
    public class Airplane {
        public Airplane() {
            Seats = new HashSet<Seat>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; internal set; }
        public string Model { get; internal set; }
        public ISet<Seat> Seats { get; internal set; }

    }
    public class Seat {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SeatNumber { get; internal set; }
        public bool IsWindowSeat { get; internal set; }
        public Standard Class { get; internal set; }
        public Airplane Airplane { get; internal set; } = null!;
    }
    [Table("Tickets")]
    public class Ticket : Booking {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int TicketId { get; set; }
        public Flight Flight { get; internal set; } = null!;
        public float Cost { get; internal set; }
        public Seat Seat { get; internal set; } = null!;
        public override DateTime StartDate { get => Flight.Departure; }
        public override DateTime? EndDate { get => Flight.Arrival; }

        internal override string GetBookingInfo() {
            return $"Flight Ticket to {Flight.Destination} on {Flight.Name} {Seat.SeatNumber}: {Seat.Class}";
        }
    }
}
