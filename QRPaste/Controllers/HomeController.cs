using QRPaste.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QRPaste.Controllers
{
    public class HomeController : CultureAwareController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input & set culture
            RouteData.Values["culture"] = CultureHelper.GetImplementedCulture(culture);  

            return RedirectToAction("Index");
        }
    }
}