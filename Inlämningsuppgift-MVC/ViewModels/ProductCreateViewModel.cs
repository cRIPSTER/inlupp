using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inlämningsuppgift_MVC.ViewModels
{
    public class ProductCreate
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "* You cant leave the product name blank")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "* Name must have min length of 2 and max Length of 50")]
        public string Name { get; set; }
        [Required(ErrorMessage = "* Please enter the product price.")]
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}