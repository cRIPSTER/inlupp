using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inlämningsuppgift_MVC.Controllers
{
    public class ProductController : Controller
    {

        public ActionResult Category()
        {
            var model = new Models.Category();
            return View(model);
        }
        public ActionResult Index(string sort)
        {
            var model = new ViewModels.CategoryIndexViewModel();
            using (var db = new Models.CategoryDb())
            {
                //var carsFromDB = db.Bilar.AsQueryable();
                //if (sort == "NamnAsc")
                //    carsFromDB = carsFromDB.OrderBy(r => r.Manufacturer);
                //else if (sort == "NamnDesc")
                //    carsFromDB = carsFromDB.OrderByDescending(r => r.Manufacturer);


                //if (sort == "YearAsc")
                //    carsFromDB = carsFromDB.OrderBy(r => r.Year);
                //else if (sort == "YearDesc")
                //    carsFromDB = carsFromDB.OrderByDescending(r => r.Year);

                //model.Cars.AddRange(carsFromDB.Select( r => new ViewModels.BilIndexViewModel.BilListViewModel
                //{
                //    Manufacturer = r.Manufacturer,
                //    Model = r.Model,
                //    Year = r.Year,
                //    Id = r.Id
                //}));




                model.Categories.AddRange(db.Categories.Select(r => new ViewModels.CategoryIndexViewModel.CategoryListViewModel
                {
                    CategoryId = r.CategoryId,
                    Name = r.Name,
                    Product = r.Products.ToString()
                }));

                if (sort == "NamnAsc")
                    model.Categories = model.Categories.OrderBy(r => r.Name).ToList();
                else if (sort == "NamnDesc")
                    model.Categories = model.Categories.OrderByDescending(r => r.Name).ToList();


                /*if (sort == "YearAsc")
                    model.Categories = model.Categories.OrderBy(r => r.Year).ToList();
                else if (sort == "YearDesc")
                    model.Cars = model.Cars.OrderByDescending(r => r.Year).ToList();*/


                model.CurrentSort = sort;

                return View(model);
            }
        }


        public ActionResult Test()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new ViewModels.CategoryIndexViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ViewModels.CategoryIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new Models.CategoryDb())
            {
                var mo = new Models.Category
                {
                    Color = model.Color,
                    Manufacturer = model.Manufacturer,
                    Model = model.Modell,
                    Year = model.Year
                };
                db.Categories.Add(mo);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var db = new Models.CategoryDb())
            {
                var cat = db.Categories.FirstOrDefault(p => p.CategoryId == id);
                var model = new ViewModels.CategoryIndexViewModel
                {
                    Color = cat.CategoryId,
                    Manufacturer = bil.Manufacturer,
                    Modell = bil.Model,
                    NumberOfWheels = bil.NumberOfWheels,
                    //Price = bil.Price,
                    Year = bil.Year,
                    Id = bil.Id
                };
                return View(model);
            }
        }




        [HttpGet]
        public ActionResult Search(string SearchCategory, string SearchProduct)
        {
            using (var db = new Models.CategoryDb())
            {
                var model = new ViewModels.CategoryIndexViewModel
                {
                    SearchCategory = SearchCategory,
                    SearchProduct = SearchProduct
                };
                model.Categories.AddRange(db.Categories.ToList().Select(r => new ViewModels.BilIndexViewModel.BilListViewModel
                {
                    Manufacturer = r.Manufacturer,
                    Model = r.Model,
                    Year = r.Year,
                    Id = r.Id
                }).Where(c => Matches(c, SearchCategory, SearchProduct)
                    ));

                return View("Index", model);
            }
        }

        bool Matches(ViewModels.CategoryIndexViewModel.CategoryListViewModel category, string SearchCategory, string SearchProduct)
        {
            if (!string.IsNullOrEmpty(SearchCategory))
            {
                SearchCategory = SearchCategory.ToLower();
                if (!category.Name.ToLower().Contains(SearchCategory)) return false;
            }
            if (!string.IsNullOrEmpty(SearchProduct))
            {
                if (!category.Product.ToString().Contains(SearchProduct)) return false;
            }
            return true;
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.BilEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new Models.BilModel2())
            {
                var bil = db.Bilar.FirstOrDefault(r => r.Id == model.Id);
                bil.Manufacturer = model.Manufacturer;
                bil.Model = model.Modell;
                //bil.Price = model.Price;
                bil.Year = model.Year;
                bil.NumberOfWheels = model.NumberOfWheels;
                bil.Color = model.Color;
                db.SaveChanges();
            }


            return RedirectToAction("Index");
        }


        public ActionResult Search(string q)
        {
            return View();
        }

        public ActionResult View(int id)
        {
            using (var db = new Models.BilModel2())
            {
                var bil = db.Bilar.FirstOrDefault(r => r.Id == id);
                var model = new ViewModels.BilViewViewModel
                {
                    Color = bil.Color,
                    Manufacturer = bil.Manufacturer,
                    Model = bil.Model,
                    Price = bil.Price,
                    Year = bil.Year
                };

                return View(model);
            }
        }
    }
}