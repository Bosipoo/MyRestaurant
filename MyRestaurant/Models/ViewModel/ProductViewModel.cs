using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRestaurant.ViewModel
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public IFormFile Image { get; set; }
    }

    public enum Category
    {
        Pizza,
        Sides,
        Chicken,
        Drinks,
        Deals
    }
}
