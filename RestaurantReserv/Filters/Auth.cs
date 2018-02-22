using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantReserv.Filters
{
    public class Auth:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["loginned"] == null)
            {
                filterContext.Result = new RedirectResult("~/admin");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}