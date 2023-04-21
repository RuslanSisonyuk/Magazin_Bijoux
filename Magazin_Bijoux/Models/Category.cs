using System;
using System.ComponentModel.DataAnnotations;

namespace Magazin_Bijoux.Models
{
    public class Category
    {
        [Key]
        [Required]
        public string categoryId { get; set; }

        [Required]
        [Display(Name = "Denumire")]
        [RegularExpression(@"^[A-za-z\s]{2,30}", ErrorMessage = "Câmpul Denumire poate conține numai litere și spații.")]
        public string name { get; set; }
    }
}
