using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OmniDibs.Models
{
    public class Account {
        public Account() {
            Bookings = new HashSet<Booking>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(12)]
        public string UserName { get; set; } = null!;
        [Required]
        [StringLength(12)]
        public string Password { get; set; } = null!;
        [Required]
        public Privileges Privileges { get; set; }
        public ISet<Booking> Bookings { get; set; }
        [Required]
        public Person Person { get; set; } = null!;

        public override string ToString() {
            return "Id:"+Id + "  UserName:" + UserName;
        }
    }
}
