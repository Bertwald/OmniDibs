using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
        //[NotMapped]
        //public abstract float Cost { get; internal set; }
        public abstract DateTime StartDate { get; set; }
        public abstract DateTime EndDate { get; set; }
        internal abstract string GetBookingInfo();
        internal abstract float GetCost();
        public override string ToString() {
            return GetBookingInfo();
        }
    }
    [Table("Tickets")]
    public class Ticket : Booking {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int TicketId { get; set; }
        public Flight Flight { get; internal set; } = null!;
        public float Cost { get; internal set; }
        public Seat Seat { get; internal set; } = null!;
        public override DateTime StartDate { get => Flight.Departure; set { } }
        public override DateTime EndDate { get => Flight.Arrival; set { } }

        internal override string GetBookingInfo() {
            return $"Flight Ticket to {Flight.Destination} on {Flight.Name} {Seat.SeatNumber}: {Seat.Class} cost {Cost}§";
        }

        internal override float GetCost() {
            return Cost;
        }
    }

    [Table("AirPlaneBookings")]
    public class AirplaneBooking : Booking {
        public Airplane Airplane { get; internal set; } = null!;
        [NotMapped]
        private DateTime _startDate;
        [NotMapped] 
        private DateTime _endDate;
        public float Cost { get; internal set; }
        public override DateTime StartDate { get => _startDate; set => _startDate = value; }

        public override DateTime EndDate { get => _endDate; set => _endDate = value; }

        internal override string GetBookingInfo() {
            return $"Model {Airplane.Model}, {Airplane.Seats.Count()} seats. Booked {StartDate.Date.ToString("MM-dd")}{(StartDate.Date == EndDate.Date ? " " : " - " +EndDate.Date.ToString("MM-dd"))} Cost: {Cost}§";
        }

        internal override float GetCost() {
            return Cost * (1 + (StartDate.Date - EndDate.Date).Days);
        }
    }
}
