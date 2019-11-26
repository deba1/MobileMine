using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class AdminOnlyController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (Session["is_admin"]!=null && !(bool)Session["is_admin"])
            {
                Response.Redirect("/");
            }
        }
    }
}