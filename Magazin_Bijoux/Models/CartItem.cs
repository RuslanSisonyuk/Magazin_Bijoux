using System;
using System.ComponentModel.DataAnnotations;

namespace Magazin_Bijoux.Models
{
    public class CartItem
    {
        [Key]
        public string itemId { get; set; }

        public string cartId { get; set; }

        [Range(0,1000)]
        public int quantity { get; set; }
        public DateTime dateCreated { get; set; }
        public string productId { get; set; }

        public virtual Product product { get; set; }
        
        public float totalPrice { get; set; }


        public float getTotalPrice()
        {
            this.totalPrice = quantity * product.price;
            return totalPrice;
        }
    }
}
