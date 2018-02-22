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
    public class MenuController : Controller
    {
        private SpicyRestaurantEntities db = new SpicyRestaurantEntities();

        // GET: Admin/Menu
        public ActionResult Index()
        {
            var menus = db.Menus.Include(m => m.MenuCategory);
            return View(menus.ToList());
        }

        // GET: Admin/Menu/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.MenuCategories, "Id", "Name");
            return View();
        }

        // POST: Admin/Menu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryId,Menu1,Details,Price,Picture")] Menu menu, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                if (Picture != null)
                {
                    if (Picture.ContentType != "image/png" && Picture.ContentType != "image/jpeg" && Picture.ContentType != "image/gif" && Picture.ContentType != "image/svg+xml")
                    {
                        Session["uploadError"] = "You can upload png,jpg or gif";
                        return RedirectToAction("edit", "menu", new { id = menu.Id });
                    }
                    if (Picture.ContentLength > 1048576)
                    {
                        Session["uploadError"] = "You file can be max 1MB";
                        return RedirectToAction("edit", "menu", new { id = menu.Id });
                    }
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "-" + Picture.FileName;
                    string path = Path.Combine(Server.MapPath("~/Public/img"), filename);
                    Picture.SaveAs(path);
                    menu.Picture = filename;
                }
                db.Menus.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.MenuCategories, "Id", "Name", menu.CategoryId);
            return View(menu);
        }

        // GET: Admin/Menu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.MenuCategories, "Id", "Name", menu.CategoryId);
            return View(menu);
        }

        // POST: Admin/Menu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,Menu1,Details,Price,Picture")] Menu menu, HttpPostedFileBase Picture, string oldImage)
        {
            if (ModelState.IsValid)
            {
                if (Picture != null)
                {
                    if (Picture.ContentType != "image/png" && Picture.ContentType != "image/jpeg" && Picture.ContentType != "image/gif" && Picture.ContentType != "image/svg+xml")
                    {
                        Session["uploadError"] = "You can upload png,jpg or gif";
                        return RedirectToAction("edit", "menu", new { id = menu.Id });
                    }
                    if (Picture.ContentLength > 1048576)
                    {
                        Session["uploadError"] = "You file can be max 1MB";
                        return RedirectToAction("edit", "menu", new { id = menu.Id });
                    }
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "-" + Picture.FileName;
                    string path = Path.Combine(Server.MapPath("~/Public/img"), filename);
                    Picture.SaveAs(path);
                    menu.Picture = filename;
                    string oldPath = Path.Combine(Server.MapPath("~/Public/img"), oldImage);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }
                else
                {
                    menu.Picture = oldImage;
                }
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.MenuCategories, "Id", "Name", menu.CategoryId);
            return View(menu);
        }

        // GET: Admin/Menu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            db.Menus.Remove(menu);
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
