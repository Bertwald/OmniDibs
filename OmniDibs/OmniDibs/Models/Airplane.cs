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
        public string Name { get; internal set; }
        public string Model { get; internal set; }
        public ISet<Seat> Seats { get; internal set; }

    }
}
