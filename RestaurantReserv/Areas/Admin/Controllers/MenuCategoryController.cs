using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantReserv.Filters;
using RestaurantReserv.Models;

namespace RestaurantReserv.Areas.Admin.Controllers
{
    [Auth]
    public class MenuCategoryController : Controller
    {
        private SpicyRestaurantEntities db = new SpicyRestaurantEntities();

        // GET: Admin/MenuCategory
        public ActionResult Index()
        {
            return View(db.MenuCategories.ToList());
        }

        // GET: Admin/MenuCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/MenuCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] MenuCategory menuCategory)
        {
            if (ModelState.IsValid)
            {
                db.MenuCategories.Add(menuCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menuCategory);
        }

        // GET: Admin/MenuCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuCategory menuCategory = db.MenuCategories.Find(id);
            if (menuCategory == null)
            {
                return HttpNotFound();
            }
            return View(menuCategory);
        }

        // POST: Admin/MenuCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] MenuCategory menuCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menuCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menuCategory);
        }

        // GET: Admin/MenuCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuCategory menuCategory = db.MenuCategories.Find(id);
            if (menuCategory == null)
            {
                return HttpNotFound();
            }
            db.MenuCategories.Remove(menuCategory);
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
