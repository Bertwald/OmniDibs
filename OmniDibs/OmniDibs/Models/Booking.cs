using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniDibs.Models {
    public abstract class Booking {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Account? Account { get; set; } = null!;
        public abstract DateTime StartDate { get; set; }
        public abstract DateTime EndDate { get; set; }
        internal abstract bool Unbook(); 
        internal abstract string GetBookingInfo();
        internal abstract float GetCost();
        public override string ToString() {
            return GetBookingInfo();
        }
    }
    [Table("Tickets")]
    public class Ticket : Booking {
        public Flight Flight { get; internal set; } = null!;
        public float Cost { get; internal set; }
        public Seat Seat { get; internal set; } = null!;
        [NotMapped]
        private DateTime _startDate;
        [NotMapped]
        private DateTime _endDate;
        public override DateTime StartDate { get => _startDate; set { _startDate = value; } }
        public override DateTime EndDate { get => _endDate; set { _endDate = value; } }

        internal override string GetBookingInfo() {
            return $"Flight Ticket on {Flight.Name}. Service Level {Seat.Class} cost {Cost}§";
        }

        internal override float GetCost() {
            return Cost;
        }

        internal override bool Unbook() {

            using (var db = new OmniDibsContext()) {
                var ticket = db.Bookings.Where(x => x.Id == this.Id).Include(x => x.Account).ThenInclude(x => x.Bookings).First();
                if (ticket != null) {             
                    ticket.Account.Bookings.Remove(ticket);
                    ticket.Account = null;
                    db.SaveChanges();
                }
            }
            return true;
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
            return $"Model {Airplane.Model}. " +
                $"Booked {StartDate.Date:yyyy-MM-dd}{(StartDate.Date == EndDate.Date ? " " : $" - {EndDate.Date:yyyy-MM-dd}")} " +
                $"Cost: {Cost}§";
        }

        internal override float GetCost() {
            return Cost;
        }

        internal override bool Unbook() {
            using (var db = new OmniDibsContext()) {
                var apb = db.Bookings.Find(Id);
                if (apb != null) {
                    db.Remove(apb);
                    db.SaveChanges();
                }
            }
            return true;
        }
    }
}
