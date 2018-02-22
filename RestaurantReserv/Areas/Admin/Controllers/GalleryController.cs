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
    public class GalleryController : Controller
    {
        private SpicyRestaurantEntities db = new SpicyRestaurantEntities();

        // GET: Admin/Gallery
        public ActionResult Index()
        {
            return View(db.Gallerys.ToList());
        }

        // GET: Admin/Gallery/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Gallery/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Gallery gallery, HttpPostedFileBase Name)
        {
            if (ModelState.IsValid)
            {
                if (Name != null)
                {
                    if (Name.ContentType != "image/png" && Name.ContentType != "image/jpeg" && Name.ContentType != "image/gif" && Name.ContentType != "image/svg+xml")
                    {
                        Session["uploadError"] = "You can upload png,jpg or gif";
                        return RedirectToAction("edit", "gallery", new { id = gallery.Id });
                    }
                    if (Name.ContentLength > 1048576)
                    {
                        Session["uploadError"] = "You file can be max 1MB";
                        return RedirectToAction("edit", "gallery", new { id = gallery.Id });
                    }
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "-" + Name.FileName;
                    string path = Path.Combine(Server.MapPath("~/Public/img"), filename);
                    Name.SaveAs(path);
                    gallery.Name = filename;
                }
                db.Gallerys.Add(gallery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gallery);
        }

        // GET: Admin/Gallery/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Gallerys.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Admin/Gallery/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Gallery gallery, HttpPostedFileBase Name, string oldImage)
        {
            if (ModelState.IsValid)
            {
                if (Name != null)
                {
                    if (Name.ContentType != "image/png" && Name.ContentType != "image/jpeg" && Name.ContentType != "image/gif" && Name.ContentType != "image/svg+xml")
                    {
                        Session["uploadError"] = "You can upload png,jpg or gif";
                        return RedirectToAction("edit", "gallery", new { id = gallery.Id });
                    }
                    if (Name.ContentLength > 1048576)
                    {
                        Session["uploadError"] = "You file can be max 1MB";
                        return RedirectToAction("edit", "gallery", new { id = gallery.Id });
                    }
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "-" + Name.FileName;
                    string path = Path.Combine(Server.MapPath("~/Public/img"), filename);
                    Name.SaveAs(path);
                    gallery.Name = filename;
                    string oldPath = Path.Combine(Server.MapPath("~/Public/img"), oldImage);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }
                else
                {
                    gallery.Name = oldImage;
                }
                db.Entry(gallery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gallery);
        }

        // GET: Admin/Gallery/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Gallerys.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            db.Gallerys.Remove(gallery);
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
