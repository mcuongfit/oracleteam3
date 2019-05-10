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

            CHITIETTRAM a = new CHITIETTRAM();
            string d, m, y;
            int dd, mm, yy;
            d = DateTime.Now.Day.ToString();
            m = DateTime.Now.Month.ToString();
            y = DateTime.Now.Year.ToString();
            dd = (int.Parse(d))-3;
            mm = int.Parse(m);
            var CTT = db.CHITIETTRAMS.Include(t => t.TRAM).Where(c => c.DA == 2 && c.MO == 2 && c.YEAR == 2008 && c.TRAM.NUOC.MANUOC == "VM" && c.TRAM.TENTRAM == "BAN ME THUOT");
            
            var mot = db.CHITIETTRAMS.Include(t => t.TRAM).Where(c => c.DA == dd+1 && c.MO == 2 && c.YEAR == 2008 && c.TRAM.NUOC.MANUOC == "VM" && c.TRAM.TENTRAM == "BAN ME THUOT");
            var hai = db.CHITIETTRAMS.Include(t => t.TRAM).Where(c => c.DA == dd+2 && c.MO == 2 && c.YEAR == 2008 && c.TRAM.NUOC.MANUOC == "VM" && c.TRAM.TENTRAM == "BAN ME THUOT");
            ViewData["mot"] = 1;
            ViewBag.hai = hai;
            return View(CTT.ToList());
        }
        [HttpPost]
        public ActionResult thongke(string manuoc, int nam)
        {
            int songay;
            List<double> tongts = new List<double>();
            List<double> tonglms = new List<double>();
            List<double> tongass = new List<double>();
            List<double> tongtmins = new List<double>();
            List<double> tongtmaxs = new List<double>();
            double tongt = 0, tonglm = 0, tongas = 0, tongmin = 0, tongmax = 0;
            thongkeviewmodel model = new thongkeviewmodel();
            model.Nam = nam;
            var nuoc = db.CHITIETTRAMS.Where(c => c.TRAM.NUOC.MANUOC == manuoc).First();
            model.tennuoc = nuoc.TRAM.NUOC.TENNUOC;
            for (int j = 1; j < 13; j++)
            {
                var thang1 = db.CHITIETTRAMS.Where(c => c.MO == j && c.TRAM.NUOC.MANUOC == manuoc && c.YEAR == nam).ToList();
                songay = thang1.Count();
                for (int i = 0; i < songay; i++)
                {
                    tongt = tongt + Convert.ToDouble(thang1[i].NHIETDO);
                    tonglm = tonglm + Convert.ToDouble(thang1[i].LUONGMUA);
                    tongas = tongas + Convert.ToDouble(thang1[i].APSUAT);
                    tongmin = tongmin + Convert.ToDouble(thang1[i].TMIN);
                    tongmax = tongmax + Convert.ToDouble(thang1[i].TMAX);

                }
                tongt = (((tongt / songay) - 32) * 5) / 9;

                tongt = Math.Round(tongt, 2);

                tongts.Add(tongt);
                tonglm = tonglm / songay;
                tonglm = Math.Round(tonglm, 2);
                tonglms.Add(tonglm);
                tongas = tongas / songay;
                tongas = Math.Round(tongas, 2);
                tongass.Add(tongas);


                tongmin = (((tongmin / songay) - 32) * 5) / 9;
                tongmin = Math.Round(tongmin, 2);
                tongtmins.Add(tongmin);

                tongmax = (((tongmax / songay) - 32) * 5) / 9;
                tongmax = Math.Round(tongmax, 2);
                tongtmaxs.Add(tongmax);
                model.nhietdo = tongts;
                model.apsuat = tongass;
                model.luongmua = tonglms;
                model.tmin = tongtmins;
                model.tmax = tongtmaxs;
            }


            return View(model);
        }
        public ActionResult ServerTime()
        {
            return Content(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt"));
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpPost]
        public ActionResult chonnuoc( string manuoc)
        {
            List<string> arr = new List<string>();
            var trams = db.TRAMS.Where(c => c.MANUOC == manuoc);
            foreach(var item in trams)
            {
                arr.Add(item.TENTRAM);
            }
            var ctt = db.CHITIETTRAMS.Include(t => t.TRAM).Where(c => c.DA == 2 && c.MO == 2 && c.YEAR == 2008 && c.TRAM.NUOC.MANUOC == manuoc ).First();
            ViewData["arr"] = arr;
            return View(ctt);
        }
        [HttpPost]
        public ActionResult dubao( string manuoc,string tentram)
        {
            List<string> arr = new List<string>();
            var trams = db.TRAMS.Where(c => c.MANUOC == manuoc);
            foreach (var item in trams)
            {
                arr.Add(item.TENTRAM);
            }
            var ctt = db.CHITIETTRAMS.Include(t => t.TRAM).Where(c => c.DA == 2 && c.MO == 2 && c.YEAR == 2008 && c.TRAM.NUOC.MANUOC == manuoc&&c.TRAM.TENTRAM==tentram).First();
            ViewData["arr"] = arr;
            return View(ctt);
            
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}