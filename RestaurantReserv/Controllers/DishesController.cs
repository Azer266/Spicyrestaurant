using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantReserv.Models;

namespace RestaurantReserv.Controllers
{
    public class DishesController : Controller
    {
        private SpicyRestaurantEntities db = new SpicyRestaurantEntities();
        // GET: Dishes
        public ActionResult Index(int page = 1 )
        {
            ViewBag.General = db.Generals.First();
            ViewBag.Dish = db.Dishes.OrderByDescending(dh => dh.Id).Skip((page - 1) * 6).Take(6).ToList();
            double d = (db.Dishes.Count() / 6.0);
            ViewBag.TotalPage = Math.Ceiling(d);
            ViewBag.ActivePage = page;
            ViewBag.OpeningHours = db.OpeningHours.ToList();
            return View();
        }
    }
}