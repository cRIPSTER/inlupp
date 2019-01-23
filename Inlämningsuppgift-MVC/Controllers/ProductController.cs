using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inlämningsuppgift_MVC.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            var model = new ViewModels.CategoryIndexViewModel();
            using (var db = new Models.CategoryDb())
            {
                model.Categories.AddRange(db.Categories.Select(c => new ViewModels.CategoryIndexViewModel.CategoryListViewModel
                {
                    Name = c.Name,
                    CategoryId = c.CategoryId
                }));
            }

            return View(model);
        }

        public ActionResult CurrentSort(string sort)
        {
            var model = new ViewModels.ProductIndexViewModel();
            using (var db = new Models.CategoryDb())
            {
                model.Products.AddRange(db.Products.Select(r => new ViewModels.ProductIndexViewModel.ProductListViewModel
                {
                    ProductId = r.ProductId,
                    Name = r.Name
                }));

                if (sort == "NamnAsc")
                    model.Products = model.Products.OrderBy(r => r.Name).ToList();
                else if (sort == "NamnDesc")
                    model.Products = model.Products.OrderByDescending(r => r.Name).ToList();


                if (sort == "PriceAsc")
                    model.Products = model.Products.OrderBy(r => r.Price).ToList();
                else if (sort == "PriceDesc")
                    model.Products = model.Products.OrderByDescending(r => r.Price).ToList();


                model.CurrentSort = sort;

                return View(model);
            }
        }

        public ActionResult Test()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CategoryCreate()
        {
            var model = new ViewModels.CategoryCreateViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult CategoryCreate(ViewModels.CategoryIndexViewModel.CategoryListViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new Models.CategoryDb())
            {
                var m = new Models.Category
                {
                    Name = model.Name,
                };
                db.Categories.Add(m);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
       
        [HttpGet]
        public ActionResult ProductCreate()
        {
            var model = new ViewModels.ProductCreateViewModel();
            SetupAvailableCatagories(model);

            return View(model);
        }

        
        [HttpPost]
        public ActionResult ProductCreate(ViewModels.ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new Models.CategoryDb())
            {
                var pro = new Models.Product

                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Category = db.Categories.First(c => c.CategoryId == model.CategoryId)
                };
                db.Products.Add(pro);
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { id = model.CategoryId });
        }

        //SetUpAvailableCategories För EDIT
        void SetupAvailableCatagories(ViewModels.ProductCreateViewModel model)
        {
            model.AvailableCategory = new List<SelectListItem>
            {
                 new SelectListItem {Value = null , Text ="Choose A Category"},


            };
            using (var db = new Models.CategoryDb())
            {
                foreach (var cat in db.Categories)
                {
                    model.AvailableCategory.Add(new SelectListItem { Value = cat.CategoryId.ToString(), Text = cat.Name });
                }
            }
        }

        [HttpGet]
        public ActionResult CategoryEdit(int id)
        {
            using (var db = new Models.CategoryDb())
            {
                var cat = db.Categories.FirstOrDefault(p => p.CategoryId == id);
                var model = new ViewModels.CategoryEditViewModel
                {
                    CategoryId = cat.CategoryId,
                    Name = cat.Name
                };
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult CategoryEdit(ViewModels.CategoryEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new Models.CategoryDb())
            {
                var cat = db.Categories.FirstOrDefault(r => r.CategoryId == model.CategoryId);
                cat.CategoryId = model.CategoryId;
                cat.Name = model.Name;
                db.SaveChanges();
            }


            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult ProductEdit(int id)
        {
            using (var db = new Models.CategoryDb())
            {
                var prod = db.Products.FirstOrDefault(p => p.ProductId == id);
                var model = new ViewModels.ProductEditViewModel
                {
                    ProductId = prod.ProductId,
                    Name = prod.Name,
                    Description = prod.Description,
                    Price = prod.Price,
                };
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult ProductEdit(ViewModels.ProductEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new Models.CategoryDb())
            {
                var p = db.Products.FirstOrDefault(r => r.ProductId == model.ProductId);
                p.ProductId = model.ProductId;
                p.Name = model.Name;
                p.Description = model.Description;
                p.Price = model.Price;

                db.SaveChanges();
            }


            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Search(string SearchProduct)
        {
            using (var db = new Models.CategoryDb())
            {
                var model = new ViewModels.ProductIndexViewModel
                {
                    SearchProduct = SearchProduct
                };
                model.Products.AddRange(db.Products.ToList().Select(r => new ViewModels.ProductIndexViewModel.ProductListViewModel
                {
                    ProductId = r.ProductId,
                    Name = r.Name,
                    Price = r.Price,
                    Description = r.Description,
                }).ToList().Where(c => c.Name.ToLower().Contains(model.SearchProduct.ToLower()) || c.Description.ToLower().Contains(model.SearchProduct.ToLower())
                    ));

                return View("Search", model);
            }
        }

        bool Matches(ViewModels.ProductIndexViewModel.ProductListViewModel product, string SearchProduct)
        {
            /*if (!string.IsNullOrEmpty(SearchCategory))
            {
                SearchCategory = SearchCategory.ToLower();
                if (!product.Name.ToLower().Contains(SearchCategory)) return false;
            }*/
            if (!string.IsNullOrEmpty(SearchProduct))
            {
                if (!product.Name.ToString().Contains(SearchProduct)) return false;
            }
            return true;
        }

        


        public ActionResult CategoryView(int id, string sort)
        {
            var model = new ViewModels.ProductIndexViewModel();
            using (var db = new Models.CategoryDb())
            {
                model.Products.AddRange(db.Products.Select(p => new ViewModels.ProductIndexViewModel.ProductListViewModel
                {
                    Name = p.Name,
                    ProductId = p.ProductId,
                    Description = p.Description,
                    CategoryId = p.Category.CategoryId,
                    Price = p.Price

                }).Where(p => p.CategoryId == id));

                if (sort == "NamnAsc")
                    model.Products = model.Products.OrderBy(r => r.Name).ToList();
                else if (sort == "NamnDesc")
                    model.Products = model.Products.OrderByDescending(r => r.Name).ToList();


                if (sort == "PriceAsc")
                    model.Products = model.Products.OrderBy(r => r.Price).ToList();
                else if (sort == "PriceDesc")
                    model.Products = model.Products.OrderByDescending(r => r.Price).ToList();


                model.CurrentSort = sort;

                return View(model);
            }
        }

        public ActionResult ShowAllProducts()
        {
            using (var db = new Models.CategoryDb())
            {
                return View(db);
            }
        }

        public ActionResult ProductView(int id)
        {
            using (var db = new Models.CategoryDb())
            {
                var p = db.Products.FirstOrDefault(r => r.ProductId == id);
                var model = new ViewModels.ProductViewViewModel
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price
                };

                return View(model);
            }
        }
    }
}