using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRestaurant.Data;
using MyRestaurant.Models;

namespace MyRestaurant.Pages.Admin
{
    public class OrdersModel : PageModel
    {
        private readonly MyRestaurantContext _context;

        public OrdersModel(MyRestaurantContext context)
        {
            _context = context;
        }
        public IList<OrderDetail> list { get; set; }

        public async Task OnGetAsync()
        {
            list = await _context.OrderDetails.ToListAsync();
        }
    }
}
