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
        public string name { get; set; }

        [Required]
        [Display(Name = "Detalii")]
        [RegularExpression(@"^[A-Za-z0-9\s]*$", ErrorMessage = "Câmpul Email este invalid")]
        public string details { get; set; }

        [Required]
        [Display(Name = "Mărime")]
        public string size { get; set; }

        [Required]
        [Display(Name = "Culoare")]
        public string color { get; set; }

        [Required]
        [Display(Name = "Preț")]
        [Range(0, 100000, ErrorMessage = "Prețul admisibil este între 0 și 100000")]
        public float price { get; set; }

        public string imageURL { get; set; }
    }
}
