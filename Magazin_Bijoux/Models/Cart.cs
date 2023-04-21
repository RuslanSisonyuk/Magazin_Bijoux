using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Magazin_Bijoux.Models
{
    public class Cart
    {
        [Key]

        public string cartId { get; set; }

        public ICollection<CartItem> cartItems { get; set; }

    }
}
