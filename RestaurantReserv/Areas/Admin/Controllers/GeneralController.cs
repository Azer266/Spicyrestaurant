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
    public class GeneralController : Controller
    {
        private SpicyRestaurantEntities db = new SpicyRestaurantEntities();

        // GET: Admin/General
        public ActionResult Index()
        {
            return View(db.Generals.ToList());
        }

        // GET: Admin/General/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            General general = db.Generals.Find(id);
            if (general == null)
            {
                return HttpNotFound();
            }
            return View(general);
        }

        // POST: Admin/General/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Header,HeaderAddress,HeaderText,Story,Quote,Newsletter,Address,Email,Phone,Facebook,Twitter,Instagram,LinkedIn,HeaderPicture,OrderPhone,Logo,LogoWhite")]
        General general, HttpPostedFileBase Quote, HttpPostedFileBase HeaderPicture, HttpPostedFileBase Logo, HttpPostedFileBase LogoWhite, string oldQuote, string oldHeaderPicture, string oldLogo, string oldLogoWhite)
        {
            if (ModelState.IsValid)
            {
                if (Quote != null)
                {
                    if (Quote.ContentType != "image/png" && Quote.ContentType != "image/jpeg" && Quote.ContentType != "image/gif" && Quote.ContentType != "image/svg+xml" && Quote.ContentType != "image/x-icon")
                    {
                        Session["uploadError"] = "You can upload png,jpg or gif";
                        return RedirectToAction("edit", "general", new { id = general.Id });
                    }
                    if (Quote.ContentLength > 1048576)
                    {
                        Session["uploadError"] = "You file can be max 1MB";
                        return RedirectToAction("edit", "general", new { id = general.Id });
                    }
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "-" + Quote.FileName;
                    string path = Path.Combine(Server.MapPath("~/Public/img"), filename);
                    Quote.SaveAs(path);
                    general.Quote = filename;
                    string oldPath = Path.Combine(Server.MapPath("~/Public/img"), oldQuote);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }
                else
                {
                    general.Quote = oldQuote;
                }

                if (HeaderPicture != null)
                {
                    if (HeaderPicture.ContentType != "image/png" && HeaderPicture.ContentType != "image/jpeg" && HeaderPicture.ContentType != "image/gif" && HeaderPicture.ContentType != "image/svg+xml" && HeaderPicture.ContentType != "image/x-icon")
                    {
                        Session["uploadError"] = "You can upload png,jpg or gif";
                        return RedirectToAction("edit", "general", new { id = general.Id });
                    }
                    if (HeaderPicture.ContentLength > 1048576)
                    {
                        Session["uploadError"] = "You file can be max 1MB";
                        return RedirectToAction("edit", "general", new { id = general.Id });
                    }
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "-" + HeaderPicture.FileName;
                    string path = Path.Combine(Server.MapPath("~/Public/img"), filename);
                    HeaderPicture.SaveAs(path);
                    general.HeaderPicture = filename;
                    string oldPath = Path.Combine(Server.MapPath("~/Public/img"), oldHeaderPicture);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }
                else
                {
                    general.HeaderPicture = oldHeaderPicture;
                }

                if (Logo != null)
                {
                    if (Logo.ContentType != "image/png" && Logo.ContentType != "image/jpeg" && Logo.ContentType != "image/gif" && Logo.ContentType != "image/svg+xml" && Logo.ContentType != "image/x-icon")
                    {
                        Session["uploadError"] = "You can upload png,jpg or gif";
                        return RedirectToAction("edit", "general", new { id = general.Id });
                    }
                    if (Logo.ContentLength > 1048576)
                    {
                        Session["uploadError"] = "You file can be max 1MB";
                        return RedirectToAction("edit", "general", new { id = general.Id });
                    }
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "-" + Logo.FileName;
                    string path = Path.Combine(Server.MapPath("~/Public/img"), filename);
                    Logo.SaveAs(path);
                    general.Logo = filename;
                    string oldPath = Path.Combine(Server.MapPath("~/Public/img"), oldLogo);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }
                else
                {
                    general.Logo = oldLogo;
                }

                if (LogoWhite != null)
                {
                    if (LogoWhite.ContentType != "image/png" && LogoWhite.ContentType != "image/jpeg" && LogoWhite.ContentType != "image/gif" && LogoWhite.ContentType != "image/svg+xml" && LogoWhite.ContentType != "image/x-icon")
                    {
                        Session["uploadError"] = "You can upload png,jpg or gif";
                        return RedirectToAction("edit", "general", new { id = general.Id });
                    }
                    if (LogoWhite.ContentLength > 1048576)
                    {
                        Session["uploadError"] = "You file can be max 1MB";
                        return RedirectToAction("edit", "general", new { id = general.Id });
                    }
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "-" + LogoWhite.FileName;
                    string path = Path.Combine(Server.MapPath("~/Public/img"), filename);
                    LogoWhite.SaveAs(path);
                    general.LogoWhite = filename;
                    string oldPath = Path.Combine(Server.MapPath("~/Public/img"), oldLogoWhite);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }
                else
                {
                    general.LogoWhite = oldLogoWhite;
                }
                db.Entry(general).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(general);
        }

        // GET: Admin/General/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            General general = db.Generals.Find(id);
            if (general == null)
            {
                return HttpNotFound();
            }
            db.Generals.Remove(general);
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
