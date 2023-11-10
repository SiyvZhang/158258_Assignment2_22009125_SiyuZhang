using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment2.Data;
using Assignment2.Models;
using static Assignment2.ViewModels.FoodIndexViewModel;
using Assignment2.ViewModels;
using System.Globalization;
using PagedList;

namespace Assignment2.Controllers
{
    public class FoodsController : Controller
    {
        private Assignment2Context db = new Assignment2Context();

        // GET: Foods
        public ActionResult Index(string category, string search, string sortBy, int? page)
        {
            FoodIndexViewModel viewModel = new FoodIndexViewModel();
            var foods = db.Foods.Include(f => f.Category);

            if (!String.IsNullOrEmpty(search))
            {
                foods = foods.Where(f => f.Name.Contains(search) ||
                f.Description.Contains(search) ||
                f.Category.Name.Contains(search));
                //ViewBag.Search = search;
                viewModel.Search = search;
            }
            viewModel.CatsWithCount = from matchingFoods in foods
                                      where
                                      matchingFoods.CategoryID != null
                                      group matchingFoods by
                                      matchingFoods.Category.Name into
                                      catGroup
                                      select new CategoryWithCount()
                                      {
                                          CategoryName = catGroup.Key,
                                          FoodCount = catGroup.Count()
                                      };

            if (!String.IsNullOrEmpty(category))
            {
                foods = foods.Where(f => f.Category.Name == category);
                viewModel.Category = category;
            }

            var categories = foods.OrderBy(f => f.Category.Name).Select(f=> f.Category.Name).Distinct();

            if (!String.IsNullOrEmpty(category))
            {
                foods = foods.Where(f => f.Category.Name == category);
            }

            switch (sortBy)
            {
                case "price_lowest":
                    foods = foods.OrderBy(f => f.Price);
                    break;
                case "price_highest":
                    foods = foods.OrderByDescending(f => f.Price);
                    break;
                default:
                    foods = foods.OrderBy(f => f.Name);
                    break;
            }

            viewModel.SortBy = sortBy;

            viewModel.Sorts = new Dictionary<string, string>
            {
                {"Price low to high","price_lowest" },
                {"Price high to low","price_highest" }
            };
            const int PageItems = 3;
            int currentPage = (page ?? 1);
            viewModel.Foods = foods.ToPagedList(currentPage, PageItems);
            return View(viewModel);
        }

        // GET: Foods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: Foods/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        // POST: Foods/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Price,Description,CategoryID")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.Foods.Add(food);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", food.CategoryID);
            return View(food);
        }

        // GET: Foods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", food.CategoryID);
            return View(food);
        }

        // POST: Foods/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Price,Description,CategoryID")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", food.CategoryID);
            return View(food);
        }

        // GET: Foods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food food = db.Foods.Find(id);
            db.Foods.Remove(food);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
