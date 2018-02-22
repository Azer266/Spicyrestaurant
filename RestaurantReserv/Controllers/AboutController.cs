using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantReserv.Models;

namespace RestaurantReserv.Controllers
{
    public class AboutController : Controller
    {
        private SpicyRestaurantEntities db = new SpicyRestaurantEntities();
        // GET: About
        public ActionResult Index()
        {
            ViewBag.General = db.Generals.First();
            ViewBag.About = db.Abouts.First();
            ViewBag.Statistic = db.Statistics.ToList();
            ViewBag.Chef = db.Chefs.ToList();
            ViewBag.Gallery = db.Gallerys.ToList();
            ViewBag.OpeningHours = db.OpeningHours.ToList();
            return View();
        }
    }
}