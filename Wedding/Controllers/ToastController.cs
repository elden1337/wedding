using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wedding.Controllers
{
    public class ToastController : Controller
    {
        // GET: Toast
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Admin()
        {
           
           return View(/* HOW DO I GET THE TABLE TO THE VIEW? */);
        }
    }
}