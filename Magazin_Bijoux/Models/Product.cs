using System;
using System.ComponentModel.DataAnnotations;

namespace Magazin_Bijoux.Models
{
    public class Product
    {
        [Key]
        [Required]
        public string id { get; set; }

        [Required]
        [Display(Name = "Denumire")]
        [RegularExpression(@"^[A-za-z\s]{2,20}", ErrorMessage = "Câmpul Denumire poate conține numai litere și spații.")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Detalii")]
        [RegularExpression(@"^[A-Za-z0-9\s]*$", ErrorMessage = "Câmpul Email este invalid")]
        public string details { get; set; }

        [Required]
        [Display(Name = "Mărime")]
        [RegularExpression(@"^[0-9]{1,3} [\.]? [0-9]{0,3} [a-z]{0,2}", ErrorMessage = "Câmpul Mărime trebuie să conțină numere naturale")]
        public string size { get; set; }

        [Required]
        [Display(Name = "Culoare")]
        [RegularExpression(@"^[A-za-z]{2,20}$", ErrorMessage = "Câmpul Culoare nu trebuie să conține cifre sau caractere speciale")]
        public string color { get; set; }

        [Required]
        [Display(Name = "Preț")]
        [Range(0, 100000, ErrorMessage = "Prețul admisibil este între 0 și 100000")]
        public float price { get; set; }
    }
}
