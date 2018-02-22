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
    public class ReservationController : Controller
    {
        private SpicyRestaurantEntities db = new SpicyRestaurantEntities();

        // GET: Admin/Reservation
        public ActionResult Index()
        {
            return View(db.Reservations.ToList());
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
