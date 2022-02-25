using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IFileProvider _fileprovider;

        public CreateProductModel(MyRestaurantContext context, IFileProvider fileprovider, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _fileprovider = fileprovider;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public ProductViewModel model { get; set; }

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
            else
            {
                string fileName = UploadImage(model);
                var product = new Product
                {
                    ProductId = model.ProductId,
                    Category = model.Category.ToString(),
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    ImagePath = fileName,
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
                return RedirectToPage("./AdminDash");
            }

        }

        private string UploadImage(ProductViewModel viewModel)
        {
            string fileName = null;
            if (viewModel.ImagePath != null)
            {
                string uploadDir = Path.Combine(_hostEnvironment.WebRootPath, "img");
                fileName = Guid.NewGuid().ToString() + "-" + viewModel.ImagePath.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    viewModel.ImagePath.CopyTo(fileStream);
                }

            }
            return fileName;
        }
    }
}
