using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Models {
    public class Person {
        public Person() {
            Accounts = new HashSet<Account>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(13)]
        public string? BirthDate { get; set; }
        [StringLength(255)]
        [Required]
        public string FirstName { get; set; } = null!;
        [StringLength(32)]
        [Required]
        public string LastName { get; set; } = null!;
        [StringLength(32)]
        public string? Alias { get; set; } = null!;
        [Required]
        public Country Country { get; set; } = null!;
        [Required]
        [StringLength(255)]
        public string MailAdress { get; set; } = null!;
        public virtual ISet<Account> Accounts { get; set; }
    }
}
