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
    public class StatisticsController : Controller
    {
        private SpicyRestaurantEntities db = new SpicyRestaurantEntities();

        // GET: Admin/Statistics
        public ActionResult Index()
        {
            return View(db.Statistics.ToList());
        }

        // GET: Admin/Statistics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Statistics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Image,Text,DataCount,DataText")] Statistic statistic, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Image.ContentType != "image/png" && Image.ContentType != "image/jpeg" && Image.ContentType != "image/gif" && Image.ContentType != "image/svg+xml")
                    {
                        Session["uploadError"] = "You can upload png,jpg or gif";
                        return RedirectToAction("edit", "statistics", new { id = statistic.Id });
                    }
                    if (Image.ContentLength > 1048576)
                    {
                        Session["uploadError"] = "You file can be max 1MB";
                        return RedirectToAction("edit", "statistics", new { id = statistic.Id });
                    }
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "-" + Image.FileName;
                    string path = Path.Combine(Server.MapPath("~/Public/img"), filename);
                    Image.SaveAs(path);
                    statistic.Image = filename;
                }
                db.Statistics.Add(statistic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statistic);
        }

        // GET: Admin/Statistics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistic statistic = db.Statistics.Find(id);
            if (statistic == null)
            {
                return HttpNotFound();
            }
            return View(statistic);
        }

        // POST: Admin/Statistics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Image,Text,DataCount,DataText")] Statistic statistic, HttpPostedFileBase Image, string oldImage)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Image.ContentType != "image/png" && Image.ContentType != "image/jpeg" && Image.ContentType != "image/gif" && Image.ContentType != "image/svg+xml")
                    {
                        Session["uploadError"] = "You can upload png,jpg or gif";
                        return RedirectToAction("edit", "statistics", new { id = statistic.Id });
                    }
                    if (Image.ContentLength > 1048576)
                    {
                        Session["uploadError"] = "You file can be max 1MB";
                        return RedirectToAction("edit", "statistics", new { id = statistic.Id });
                    }
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "-" + Image.FileName;
                    string path = Path.Combine(Server.MapPath("~/Public/img"), filename);
                    Image.SaveAs(path);
                    statistic.Image = filename;
                    string oldPath = Path.Combine(Server.MapPath("~/Public/img"), oldImage);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }
                else
                {
                    statistic.Image = oldImage;
                }
                db.Entry(statistic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statistic);
        }

        // GET: Admin/Statistics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistic statistic = db.Statistics.Find(id);
            if (statistic == null)
            {
                return HttpNotFound();
            }
            db.Statistics.Remove(statistic);
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
