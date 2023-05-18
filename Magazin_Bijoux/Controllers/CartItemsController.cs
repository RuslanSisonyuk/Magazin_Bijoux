using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Magazin_Bijoux.Data;
using Magazin_Bijoux.Models;

namespace Magazin_Bijoux.Controllers
{
    public class CartItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public string ShoppingCartId { get; set; }

        public CartItemsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public const string CartSessionKey = "CartId";
 
        // GET: CartItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CartItem.Include(c => c.product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CartItems/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItem
                .Include(c => c.product)
                .FirstOrDefaultAsync(m => m.itemId == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // GET: CartItems/Create
        public IActionResult Create()
        {
            ViewData["productId"] = new SelectList(_context.Product, "id", "id");
            return View();
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("itemId,cartId,quantity,dateCreated,productId")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["productId"] = new SelectList(_context.Product, "id", "id", cartItem.productId);
            return View(cartItem);
        }

        // GET: CartItems/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItem.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            ViewData["productId"] = new SelectList(_context.Product, "id", "id", cartItem.productId);
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("itemId,cartId,quantity,dateCreated,productId")] CartItem cartItem)
        {
            if (id != cartItem.itemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemExists(cartItem.itemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["productId"] = new SelectList(_context.Product, "id", "id", cartItem.productId);
            return View(cartItem);
        }

        // GET: CartItems/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItem
                .Include(c => c.product)
                .FirstOrDefaultAsync(m => m.itemId == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cartItem = await _context.CartItem.FindAsync(id);
            _context.CartItem.Remove(cartItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartItemExists(string id)
        {
            return _context.CartItem.Any(e => e.itemId == id);
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
                    HttpContext.Items.Add(CartSessionKey, tempCartId);
                }
            }
            return HttpContext.Items[CartSessionKey].ToString();
        }
        public List<CartItem> GetCartItems()
        {

            ShoppingCartId = GetCartId();

            return _context.CartItem.Where(
                c => c.cartId == ShoppingCartId).Include(c=>c.product).ToList();
        }
        public ViewResult Cart()
        {
            var cartContext = GetCartItems();
            return View(cartContext);
        }

        public void AddToCart(string id)
        {
            // Retrieve the product from the database.           
            ShoppingCartId = GetCartId();

            var cartItem = _context.CartItem.SingleOrDefault(
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
                    product = _context.Product.SingleOrDefault(
                   p => p.id == id),
                    quantity = 1,
                    dateCreated = DateTime.Now
                };

                _context.CartItem.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.                 
                cartItem.quantity++;
            }
            _context.SaveChanges();
        }
        public void RemoveFromCart(string id)
        {
            ShoppingCartId = GetCartId();
            var cartItem = _context.CartItem.SingleOrDefault(
                c => c.cartId == ShoppingCartId
                && c.productId == id);
            cartItem.quantity--;
            if (cartItem.quantity <= 0) _context.CartItem.Remove(cartItem);
            _context.SaveChanges();
        }
        public async Task<IActionResult> AddToCartAndReturn(string id, string action, string controller, int quantity, bool hasParameter)
        {
            for (int i = 0; i < quantity; i++)
            {
                AddToCart(id);
            }
            if (hasParameter == true)
                return RedirectToAction(action, controller, new { id = id });
            return RedirectToAction(action, controller);
        }
        public async Task<IActionResult> RemoveFromCartAndReturn(string id, string action, string controller, bool hasParameter)
        {
            RemoveFromCart(id);
            if (hasParameter == true)
                return RedirectToAction(action, controller, new { id = id });
            return RedirectToAction(action, controller);
        }
        public async Task<IActionResult> DeleteFromCart(string id, string action, string controller)
        {
            var cartItem = await _context.CartItem.FindAsync(id);
            _context.CartItem.Remove(cartItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(action, controller);
        }
    }
}