using oraclenhom3.Models;
using System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using oraclenhom3.Models;
namespace oraclenhom3.Controllers
{
    public class HomeController : Controller
    {


        private contexts db = new contexts();

        public ActionResult Index()
        {
            var CTT = db.CHITIETTRAMS.Include(t => t.TRAM).Where(c => c.DA == 2 && c.MO == 2 && c.YEAR == 2008 && c.TRAM.NUOC.MANUOC == "VM" && c.TRAM.TENTRAM == "BAN ME THUAT");
            return View(CTT.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}