using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Wedding.Controllers
{
    public class StartController : Controller
    {
        public ActionResult Index()
        {
            DateTime weddingDate = new DateTime(2016, 7, 16, 15, 0, 0, DateTimeKind.Utc);
            var timeUntilWedding = weddingDate.ToLocalTime().Subtract(DateTime.Now);
            Models.Start.IndexModel model = new Models.Start.IndexModel();
            model.TimeToWedding = timeUntilWedding;

            
            return View(model);
        }



        public ActionResult Info()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Story()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Blog()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Contact()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogIn", "Account");
            }

            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Robots()
        {
            Response.ContentType = "text/plain";
            return View();
        }

    }
}