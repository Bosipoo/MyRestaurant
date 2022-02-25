using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRestaurant.Data;
using MyRestaurant.Models;

namespace MyRestaurant.Pages
{
    public class FeedbackModel : PageModel
    {
        private readonly MyRestaurantContext _context;

        public FeedbackModel(MyRestaurantContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Contact Contact { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Contacts.Add(Contact);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");

        }
    }
}
