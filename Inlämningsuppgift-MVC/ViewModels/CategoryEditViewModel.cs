using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inlämningsuppgift_MVC.ViewModels
{
    public class CategoryEditViewModel
    {
            public int CategoryId { get; set; }
            [Required(ErrorMessage = "* You cant leave the Product name blank")]
            [StringLength(50, MinimumLength = 3, ErrorMessage = "* Name must have min length of 3 and max Length of 50")]
            public string Name { get; set; }
        }

        public class CategoryCreateViewModel
        {
            public int CategoryId { get; set; }
            [Required(ErrorMessage = "* Please enter a Product name.")]
            [StringLength(50, MinimumLength = 3, ErrorMessage = "* Name must have min length of 3 and max Length of 50")]
            public string Name { get; set; }

            //dropdownlist
            public string CategoryName { get; set; }
            public List<SelectListItem> AvailableCategory { get; set; }

        }
    }