using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRestaurant.Data;
using MyRestaurant.Models;
using MyRestaurant.Services;

namespace MyRestaurant.Pages.Admin
{
    public class EditProductModel : PageModel
    {
        private readonly MyRestaurantContext _context;


        public EditProductModel(MyRestaurantContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; private set; }

        public IActionResult OnGet(int id)
        {
            Product = _context.Products.Find(id);

            if (Product == null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var ProdFromDb = await _context.Products.FindAsync(Product.ProductId);
                ProdFromDb.Category = Product.Category;
                ProdFromDb.Name = Product.Name;
                ProdFromDb.Description = Product.Description;
                ProdFromDb.Price = Product.Price;

                await _context.SaveChangesAsync();
                return RedirectToPage("Admin/AdminDash");
            }
            return RedirectToPage();
        }
    }
}
