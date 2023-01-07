using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Models {
    public class Country {
        public Country() {
            Languages = new List<string>();
            Continents = new List<Continent>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(64)]
        public string CountryName { get; set; } = null!;
        public Climate Climate { get; set; }
        [NotMapped]
        public ICollection<Continent> Continents { get; set; }
        public string ContinentsString {
            get => string.Join(",", Continents);
            set => Continents = value.Split(',').Select(x => Enum.Parse(typeof(Continent), x)).Cast<Continent>().ToList();
        }
        public float BigMacIndex { get; set; }
        //SHOW: Collection in Database variant 1
        [NotMapped]
        public ICollection<string> Languages { get; set; }
        public string LanguagesString {
            get => string.Join(",", Languages);
            set => Languages = value.Split(',').ToList();
        }
        public override string ToString() {
            return CountryName;
        }
    }
}
