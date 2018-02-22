using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using RestaurantReserv.Models;

namespace RestaurantReserv.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        SpicyRestaurantEntities db = new SpicyRestaurantEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User usr)
        {
            User logined = db.Users.FirstOrDefault(u => u.Email == usr.Email);
            if (logined != null)
            {
                if (Crypto.VerifyHashedPassword(logined.Password, usr.Password))
                {
                    Session["loginned"] = true;
                    return RedirectToAction("index", "dashboard");
                }
            }

            Session["loginError"] = "Username or password incorrect";
            return RedirectToAction("index");
        }
    }
}