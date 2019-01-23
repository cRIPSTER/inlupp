using Inlämningsuppgift_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inlämningsuppgift_MVC.ViewModels
{
    public class ProductViewViewModel
    {
            public int ProductId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public virtual Category Category { get; set; }
            public string CurrentSort { get; set; }
    }

    public class CategoryViewViewModel
    {
        public string Name { get; set; }
    }
}