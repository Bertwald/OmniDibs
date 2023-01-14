using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace OmniDibs.Models
{
    internal class Account {
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
    }
}
