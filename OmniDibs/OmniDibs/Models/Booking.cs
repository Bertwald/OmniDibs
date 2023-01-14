﻿using System;
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
            return $"Flight Ticket to {Flight.Destination} on {Flight.Name} {Seat.SeatNumber}: {Seat.Class} cost {Cost}§";
        }
    }

    [Table("AirPlaneBookings")]
    public class AirplaneBooking : Booking {
        public Airplane Airplane { get; internal set; } = null!;
        [NotMapped]
        private DateTime _startDate;
        [NotMapped] 
        private DateTime _endDate;
        public override DateTime StartDate { get => _startDate; }

        public override DateTime? EndDate { get => _endDate; }

        internal override string GetBookingInfo() {
            return $"An Aircraft of model {Airplane.Model} with {Airplane.Seats.Count()}. {Environment.NewLine} Booked between {StartDate.Date} and {EndDate.Value.Date}";
        }
    }
}
