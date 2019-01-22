using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inlämningsuppgift_MVC.Models
{
        public class Category
        {
            public int CategoryId { get; set; }
            public string Name { get; set; }
            public virtual ICollection<Product> Products { get; set; }
        }

        public class Product
        {
            public int ProductId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public virtual Category Category { get; set; }
        }
}