using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRestaurant.Data;
using MyRestaurant.Extentions;
using MyRestaurant.Models;
using MyRestaurant.ViewModel;

namespace MyRestaurant.Pages
{
    public class MenuModel : PageModel
    {
        private readonly MyRestaurantContext _context;
        private List<CartViewModel> cartList;

        public MenuModel(MyRestaurantContext context)
        {
            _context = context;
            cartList = new List<CartViewModel>();
        }
        public IList<Product> Product { get; set; }

        public async Task OnGetAsync()
        {
             Product = _context.Products.ToListAsync().Result;
        }

        public async Task<IActionResult> OnPostAdd(int id)
        {
            CartViewModel cartModel = new CartViewModel();
            var item = _context.Products.Find(id);

            if (cartList == null)
            {
                HttpContext.Session.SetInt32("CartCounter", 0);
            }

            if (HttpContext.Session.GetInt32("CartCounter") > 0)
            {
                cartList = HttpContext.Session.GetComplexData<List<CartViewModel>>("CartItem");
            }

            if (cartList.Any(x => x.ItemId == id))
            {
                cartModel = cartList.Single(x => x.ItemId == id);
                cartModel.Quantity = cartModel.Quantity + 1;
                cartModel.Total = cartModel.Quantity * cartModel.UnitPrice;

            }
            else
            {
                cartModel.ItemId = id;
                cartModel.ItemName = item.Name;
                cartModel.Quantity = 1;
                cartModel.Total = item.Price;
                cartModel.UnitPrice = item.Price;
                cartModel.ImagePath = item.ImagePath;
                cartList.Add(cartModel);
            }
            var Counter = cartList.Count();

            HttpContext.Session.SetInt32("CartCounter", Counter);
            HttpContext.Session.SetComplexData("CartItem", cartList);
            return RedirectToAction("Index");
        }
    }
}
