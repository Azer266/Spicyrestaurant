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
    public class OpeningHourController : Controller
    {
        private SpicyRestaurantEntities db = new SpicyRestaurantEntities();

        // GET: Admin/OpeningHour
        public ActionResult Index()
        {
            return View(db.OpeningHours.ToList());
        }

        // GET: Admin/OpeningHour/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpeningHour openingHour = db.OpeningHours.Find(id);
            if (openingHour == null)
            {
                return HttpNotFound();
            }
            return View(openingHour);
        }

        // GET: Admin/OpeningHour/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/OpeningHour/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Time")] OpeningHour openingHour)
        {
            if (ModelState.IsValid)
            {
                db.OpeningHours.Add(openingHour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(openingHour);
        }

        // GET: Admin/OpeningHour/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpeningHour openingHour = db.OpeningHours.Find(id);
            if (openingHour == null)
            {
                return HttpNotFound();
            }
            return View(openingHour);
        }

        // POST: Admin/OpeningHour/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Time")] OpeningHour openingHour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(openingHour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(openingHour);
        }

        // GET: Admin/OpeningHour/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpeningHour openingHour = db.OpeningHours.Find(id);
            if (openingHour == null)
            {
                return HttpNotFound();
            }
            db.OpeningHours.Remove(openingHour);
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
