using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniDibs.Models {
    public class Airplane {
        public Airplane() {
            Seats = new HashSet<Seat>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; internal set; } = null!;
        public string Model { get; internal set; } = null!;
        public ISet<Seat> Seats { get; internal set; }

        public override string ToString() {
            return $"{Name} {Model} {(Seats.Any()?Seats.Count: null)}";
        }

    }
}
