using System;
using System.Collections.Generic;
using System.Linq;
using Magazin_Bijoux.Models;
using Magazin_Bijoux.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Magazin_Bijoux.Logic
{
    public class ShoppingCartActions : Controller
    {
        public string ShoppingCartId { get; set; }

        private ApplicationDbContext _db;

        public ShoppingCartActions(ApplicationDbContext context)
        {
            _db = context;
        }

        public const string CartSessionKey = "CartId";

        public void AddToCart(string id)
        {
            // Retrieve the product from the database.           
            ShoppingCartId = GetCartId();

            var cartItem = _db.CartItem.SingleOrDefault(
                c => c.cartId == ShoppingCartId
                && c.productId == id);
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists.                 
                cartItem = new CartItem
                {
                    itemId = Guid.NewGuid().ToString(),
                    productId = id,
                    cartId = ShoppingCartId,
                    product = _db.Product.SingleOrDefault(
                   p => p.id == id),
                    quantity = 1,
                    dateCreated = DateTime.Now
                };

                _db.CartItem.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.                 
                cartItem.quantity++;
            }
            _db.SaveChanges();
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }

        public string GetCartId()
        {
            if (!HttpContext.Items.ContainsKey(CartSessionKey))
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name))
                {
                    HttpContext.Items.Add(CartSessionKey, HttpContext.User.Identity.Name);
                }
                else
                {
                    // Generate a new random GUID using System.Guid class.     
                    Guid tempCartId = Guid.NewGuid();
                    HttpContext.Items.Add(CartSessionKey, tempCartId.ToString());
                }
            }
            return HttpContext.Items[CartSessionKey].ToString();
        }

        public List<CartItem> GetCartItems()
        {
            ShoppingCartId = GetCartId();

            return _db.CartItem.Where(
                c => c.cartId == ShoppingCartId).ToList();
        }
    }
}