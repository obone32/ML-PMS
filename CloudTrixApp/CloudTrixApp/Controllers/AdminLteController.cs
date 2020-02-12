using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudTRIXApp.Controllers
{
    /// <summary>
    /// This is an example controller using the CloudTRIX NuGet package's CSHTML templates, CSS, and JavaScript
    /// You can delete these, or use them as handy references when building your own applications
    /// </summary>
    public class CloudTRIXController : Controller
    {
        /// <summary>
        /// The home page of the CloudTRIX demo dashboard, recreated in this new system
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The color page of the AdminLTE demo, demonstrating the CloudTRIX.Color enum and supporting methods
        /// </summary>
        /// <returns></returns>
        public ActionResult Colors()
        {
            return View();
        }
    }
}