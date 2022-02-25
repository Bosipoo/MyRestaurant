using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRestaurant.Data;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Pages.Admin
{
    public class AdminDashModel : PageModel
    {
        private readonly MyRestaurantContext _context;

        public AdminDashModel(MyRestaurantContext context)
        {
            _context = context;
        }
        public IList<Product> Product { get; set; }

        public async Task OnGetAsync()
        {
            Product = _context.Products.ToListAsync().Result;
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();

            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
