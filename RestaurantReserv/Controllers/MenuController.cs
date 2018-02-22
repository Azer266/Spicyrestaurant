using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantReserv.Models;

namespace RestaurantReserv.Controllers
{
    public class MenuController : Controller
    {
        private SpicyRestaurantEntities db = new SpicyRestaurantEntities();
        // GET: Menu
        public ActionResult Index()
        {
            ViewBag.General = db.Generals.First();
            ViewBag.MenuCategories = db.MenuCategories.ToList();
            ViewBag.Menu = db.Menus.ToList();
            ViewBag.OpeningHours = db.OpeningHours.ToList();
            return View();
        }
    }
}