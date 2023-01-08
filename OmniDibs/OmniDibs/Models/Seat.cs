using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniDibs.Models {
    public class Seat {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SeatNumber { get; internal set; }
        public bool IsWindowSeat { get; internal set; }
        public Standard Class { get; internal set; }
        public Airplane Airplane { get; internal set; } = null!;
    }
}
