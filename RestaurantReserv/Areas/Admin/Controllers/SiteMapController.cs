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
    public class SiteMapController : Controller
    {
        private SpicyRestaurantEntities db = new SpicyRestaurantEntities();

        // GET: Admin/SiteMap
        public ActionResult Index()
        {
            return View(db.SiteMaps.ToList());
        }

        // GET: Admin/SiteMap/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SiteMap/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Models.SiteMap siteMap)
        {
            if (ModelState.IsValid)
            {
                db.SiteMaps.Add(siteMap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(siteMap);
        }

        // GET: Admin/SiteMap/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.SiteMap siteMap = db.SiteMaps.Find(id);
            if (siteMap == null)
            {
                return HttpNotFound();
            }
            return View(siteMap);
        }

        // POST: Admin/SiteMap/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Models.SiteMap siteMap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siteMap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siteMap);
        }

        // GET: Admin/SiteMap/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.SiteMap siteMap = db.SiteMaps.Find(id);
            if (siteMap == null)
            {
                return HttpNotFound();
            }
            db.SiteMaps.Remove(siteMap);
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
