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
    public class CartModel : PageModel
    {

        private readonly MyRestaurantContext _context;
        //private List<CartViewModel> cartList;

        public CartModel(MyRestaurantContext context)
        {
            _context = context;
            //cartList = new List<CartViewModel>();
        }

        public List<CartViewModel> cartList { get; set; }

        public async Task OnGet()
        {
            cartList = HttpContext.Session.GetComplexData<List<CartViewModel>>("CartItem");
        }

        public async Task<IActionResult> OnPostSaveOrder()
        {
            int OrderId = 0;
            cartList = HttpContext.Session.GetComplexData<List<CartViewModel>>("CartItem");
            var basedate = new DateTime(2022, 1, 1).Ticks;
            var ans = DateTime.Now.Ticks - basedate;
            var uniqueId = ans.ToString("x");



            Order order = new Order()
            {
                OrderDate = DateTime.Now,
                OrderNumber = uniqueId
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            OrderId = order.OrderId;

            foreach (var x in cartList)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.Total = x.Total;
                orderDetail.ItemId = x.ItemId.ToString();
                orderDetail.OrderId = OrderId;
                orderDetail.Quantity = x.Quantity;
                orderDetail.UnitPrice = x.UnitPrice;
                _context.OrderDetails.Add(orderDetail);
                await _context.SaveChangesAsync();
            }

            HttpContext.Session.SetInt32("CartCounter", 0);
            HttpContext.Session.SetComplexData("CartItem", null);

            return Redirect("/Menu");
        }
    }
}
