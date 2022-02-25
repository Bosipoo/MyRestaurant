using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyRestaurant.Models
{
    [Table("Products")]
    public class Product
    {
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }
        [Display(Name = "Product Category")]
        [StringLength(100), Required]
        public string Category { get; set; }
        [Display(Name = "Product Name")]
        [StringLength(100), Required]
        public string Name { get; set; }
        [Display(Name = "Product Description")]
        [StringLength(1000), Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Product Price")]
        public double Price { get; set; }
        [Display(Name = "Product Image")]
        [MaxLength]
        public string ImagePath { get; set; }
        public DateTime CreatedOn { get; set; }

        
    }
}
