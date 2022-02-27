using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MyRestaurant.Data;
using MyRestaurant.Models;
using MyRestaurant.ViewModel;

namespace MyRestaurant.Pages.Admin
{
    public class CreateProductModel : PageModel
    {
        private readonly MyRestaurantContext _context;
        private readonly IFileProvider _fileprovider;
        private readonly IHostingEnvironment _ihostingEnvironment;

        public CreateProductModel(MyRestaurantContext context, IHostingEnvironment ihostingEnvironment)
        {
            _context = context;
            _ihostingEnvironment = ihostingEnvironment;
        }

        [BindProperty]
        public ProductViewModel model { get; set; }
        public string FileName { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile photo)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                //string fileName = UploadImage(model);

                var path = Path.Combine(_ihostingEnvironment.WebRootPath, "img", photo.FileName);
                var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream);

                var product = new Product
                {
                    ProductId = model.ProductId,
                    Category = model.Category.ToString(),
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    ImagePath = photo.FileName,
                    CreatedOn = DateTime.Now
                };

                if (model.ProductId == 0)
                {
                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.Entry(product).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

                return RedirectToPage("/Admin/AdminDash");
            }

        }

        //private string UploadImage(ProductViewModel viewModel)
        //{
        //    string fileName = null;
        //    if (viewModel.Image != null)
        //    {
        //        string uploadDir = Path.Combine(_hostEnvironment.WebRootPath, "img");
        //        fileName = Guid.NewGuid().ToString() + "-" + viewModel.Image.FileName;
        //        string filePath = Path.Combine(uploadDir, fileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            viewModel.Image.CopyTo(fileStream);
        //        }

        //    }
        //    return fileName;
        //}
    }
}
