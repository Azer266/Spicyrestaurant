using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantReserv.Filters;
using RestaurantReserv.Models;

namespace RestaurantReserv.Areas.Admin.Controllers
{
    [Auth]
    public class DishesController : Controller
    {
        private SpicyRestaurantEntities db = new SpicyRestaurantEntities();

        // GET: Admin/Dishes
        public ActionResult Index()
        {
            return View(db.Dishes.ToList());
        }

        // GET: Admin/Dishes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Dishes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Image,Name,Text,Price,DiscountPrice")] Dish dish, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Image.ContentType != "image/png" && Image.ContentType != "image/jpeg" && Image.ContentType != "image/gif" && Image.ContentType != "image/svg+xml")
                    {
                        Session["uploadError"] = "You can upload png,jpg or gif";
                        return RedirectToAction("edit", "dish", new { id = dish.Id });
                    }
                    if (Image.ContentLength > 1048576)
                    {
                        Session["uploadError"] = "You file can be max 1MB";
                        return RedirectToAction("edit", "dish", new { id = dish.Id });
                    }
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "-" + Image.FileName;
                    string path = Path.Combine(Server.MapPath("~/Public/img"), filename);
                    Image.SaveAs(path);
                    dish.Image = filename;
                }
                db.Dishes.Add(dish);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dish);
        }

        // GET: Admin/Dishes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // POST: Admin/Dishes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Image,Name,Text,Price,DiscountPrice")] Dish dish, HttpPostedFileBase Image, string oldImage)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Image.ContentType != "image/png" && Image.ContentType != "image/jpeg" && Image.ContentType != "image/gif" && Image.ContentType != "image/svg+xml")
                    {
                        Session["uploadError"] = "You can upload png,jpg or gif";
                        return RedirectToAction("edit", "dish", new { id = dish.Id });
                    }
                    if (Image.ContentLength > 1048576)
                    {
                        Session["uploadError"] = "You file can be max 1MB";
                        return RedirectToAction("edit", "dish", new { id = dish.Id });
                    }
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "-" + Image.FileName;
                    string path = Path.Combine(Server.MapPath("~/Public/img"), filename);
                    Image.SaveAs(path);
                    dish.Image = filename;
                    string oldPath = Path.Combine(Server.MapPath("~/Public/img"), oldImage);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }
                else
                {
                    dish.Image = oldImage;
                }
                db.Entry(dish).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dish);
        }

        // GET: Admin/Dishes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            db.Dishes.Remove(dish);
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
