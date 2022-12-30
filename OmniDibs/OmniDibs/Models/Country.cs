using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Models {
    internal class Country {
        public Country() {
            Languages = new List<string>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(64)]
        public string CountryName { get; set; } = null!;
        public Climate Climate { get; set; }
        public float BigMacIndex { get; set; }
        //SHOW: Collection in Database variant 1
        [NotMapped]
        public ICollection<string> Languages { get; set; }
        public string LanguagesString {
            get { return string.Join(",", Languages); }
            set { Languages = value.Split(',').ToList(); }
        }
    }
}
