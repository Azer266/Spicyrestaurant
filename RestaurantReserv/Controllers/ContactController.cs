using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using RestaurantReserv.Models;

namespace RestaurantReserv.Controllers
{
    public class ContactController : Controller
    {
        private SpicyRestaurantEntities db = new SpicyRestaurantEntities();
        // GET: Contact
        public ActionResult Index()
        {
            ViewBag.General = db.Generals.First();
            ViewBag.Contact = db.Contacts.First();
            ViewBag.Branch = db.Branches.ToList();
            ViewBag.OpeningHours = db.OpeningHours.ToList();
            return View();
        }

        public ActionResult Message(Messages msg)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add("p103ca@gmail.com");
            mail.From = new MailAddress(msg.Email);
            mail.Subject = "Contact Message | " + msg.Subject;
            string Body = "Name : " + msg.Name + " <br> Email : " + msg.Email + " <br> Phone : " + msg.Phone + " <br> Subject : " + msg.Subject + " <br> " + msg.Message;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("p103ca@gmail.com", "P103_2017"); // Enter seders User name and password  
            smtp.EnableSsl = true;
            smtp.Send(mail);
            Session["mailSended"] = true;
            return RedirectToAction("index");
        }
    }
}