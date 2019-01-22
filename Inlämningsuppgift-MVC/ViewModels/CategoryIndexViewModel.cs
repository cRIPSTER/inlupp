using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inlämningsuppgift_MVC.ViewModels
{
    public class CategoryIndexViewModel
    {
        public string SearchCategory { get; set; }
        public string SearchProduct { get; set; }

        public CategoryIndexViewModel()
        {
            Categories = new List<CategoryListViewModel>();
        }

        public class CategoryListViewModel
        {
            public int CategoryId { get; set; }
            public string Name { get; set; }
            public string Product{ get; set; }
        }
        public List<CategoryListViewModel> Categories { get; set; }

        public string CurrentSort { get; set; }
    }
}