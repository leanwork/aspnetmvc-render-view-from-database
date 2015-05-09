using PocRazorFromDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PocRazorFromDatabase.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var p = new Person();
            p.Name = "Michel Banagouro";

            return View(p);
        }
    }
}