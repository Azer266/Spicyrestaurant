using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using RestaurantReserv.Models;
using RestaurantReserv.Filters;

namespace RestaurantReserv.Areas.Admin.Controllers
{
    [Auth]
    public class DashboardController : Controller
    {
        SpicyRestaurantEntities db = new SpicyRestaurantEntities();
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["loginned"] = null;
            return RedirectToAction("index", "home");
        }
    }
}