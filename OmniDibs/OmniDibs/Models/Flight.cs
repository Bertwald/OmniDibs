using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniDibs.Models {
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

        public override string ToString() {
            return $"{Name}: {Origin}-{Destination} {Departure:yyyy/MM/dd}";
        }
    }
}
