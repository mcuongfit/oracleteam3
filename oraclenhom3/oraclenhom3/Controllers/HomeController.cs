using oraclenhom3.Models;
using System;
using System.Text;
using System.Threading.Tasks;

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

        [HttpGet]
        public ActionResult Index()
        {

            List<dubaoviewmodel> ctts = new List<dubaoviewmodel>();
            
            
            string d, m, y, anh;
            int dd, mm, yy;
            int Nhietd = 0, Aps = 0, Luongm = 0, Tmax = 0, Tmin = 0, Tocdogio = 0;
            d = DateTime.Now.Day.ToString();
            m = DateTime.Now.Month.ToString();
            y = DateTime.Now.Year.ToString();
            dd = (int.Parse(d));
            mm = int.Parse(m);
            yy = int.Parse(y);
            List<string> arr = new List<string>();
            var trams = db.TRAMS.Where(c => c.MANUOC == "VM");
            var nuocs = db.NUOCS.Where(c => c.MANUOC == "VM");
            anh = nuocs.First().HINH;
            foreach (var item in trams)
            {
                arr.Add(item.TENTRAM);
            }
            for (int j = 0; j < 3; j++)
            {
                dubaoviewmodel ctt = new dubaoviewmodel();
                //Random rd = new Random();
                
                //int rdom = rd.Next(1, 5);
               
                Nhietd = 0;
                Aps = 0;
                Luongm = 0;
                Tmax = 0;
                Tmin = 0;
                Tocdogio = 0;
                for (int i = 2008; i < yy; i++)
                {
                    var CTT = db.CHITIETTRAMS.Include(t => t.TRAM).Where(c => c.DA == (18+j) && c.MO == mm && c.YEAR == i && c.TRAM.NUOC.MANUOC == "VM" && c.TRAM.TENTRAM == "BAN ME THUOT").First();
                    Nhietd += Convert.ToInt32(CTT.NHIETDO);
                    Luongm += Convert.ToInt32(CTT.LUONGMUA);
                    Aps += Convert.ToInt32(CTT.APSUAT);
                    Tmax += Convert.ToInt32(CTT.TMAX);
                    Tmin += Convert.ToInt32(CTT.TMIN);
                    Tocdogio += Convert.ToInt32(CTT.TOCDOGIO);

                }
                
                Nhietd = Nhietd / (yy - 2008);
                Nhietd = (((Nhietd - 32) * 5) / 9);
                Luongm = Luongm / (yy - 2008);
                Aps = (Aps / (yy - 2008));
                Tmin = Tmin / (yy - 2008);
                Tmin = (((Tmin - 32) * 5) / 9);
                Tmax = Tmax / (yy - 2008);
                Tmax = (((Tmax - 32) * 5) / 9);
                Tocdogio = Tocdogio / (yy - 2008);
                ctt.apsuat = Aps;
                ctt.luongmua = Luongm;
                ctt.nhietdo = Nhietd;
                ctt.tmax = Tmax;
                ctt.tmin = Tmin;
                ctt.tocdogio = Tocdogio;
                ctt.tenTram = "BAN ME THUAT";
                ctt.Hinh = anh;
                ctt.Manuoc = "VM";
                ctts.Add(ctt);
               
            }
            ViewData["arr"] = arr;
          
            return View(ctts);
        }
        public ActionResult capnhat()
        {
            List<int> dstrams = new List<int>();
            var trams = db.TRAMS.ToList();
            foreach (var item in trams)
            {
                dstrams.Add(item.MATRAM);
            }


            return View();
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
            dubaoviewmodel ctt = new dubaoviewmodel();
            string d, m, y, anh,tenTram;
            int dd, mm, yy;
            int Nhietd = 0, Aps = 0, Luongm = 0, Tmax = 0, Tmin = 0, Tocdogio = 0;
            d = DateTime.Now.Day.ToString();
            m = DateTime.Now.Month.ToString();
            y = DateTime.Now.Year.ToString();
            dd = (int.Parse(d));
            mm = int.Parse(m);
            yy = int.Parse(y);
            List<string> arr = new List<string>();
            var trams = db.TRAMS.Where(c => c.MANUOC ==manuoc);
            tenTram = trams.First().TENTRAM;
            var nuocs = db.NUOCS.Where(c => c.MANUOC == manuoc);
            anh = nuocs.First().HINH;
            foreach (var item in trams)
            {
                arr.Add(item.TENTRAM);
            }
            for (int i = 2008; i < yy; i++)
            {
                var CTT = db.CHITIETTRAMS.Include(t => t.TRAM).Where(c => c.DA == 18 && c.MO == mm  && c.YEAR == i && c.TRAM.NUOC.MANUOC == manuoc).First();
                Nhietd += Convert.ToInt32(CTT.NHIETDO);
                Luongm += Convert.ToInt32(CTT.LUONGMUA);
                Aps += Convert.ToInt32(CTT.APSUAT);
                Tmax += Convert.ToInt32(CTT.TMAX);
                Tmin += Convert.ToInt32(CTT.TMIN);
                Tocdogio += Convert.ToInt32(CTT.TOCDOGIO);

            }
            Nhietd = Nhietd / (yy - 2008);
            Nhietd = ((Nhietd - 32) * 5) / 9;
            Luongm = Luongm / (yy - 2008);
            Aps = Aps / (yy - 2008);
            Tmin = Tmin / (yy - 2008);
            Tmin = ((Tmin - 32) * 5) / 9;
            Tmax = Tmax / (yy - 2008);
            Tmax = ((Tmax - 32) * 5) / 9;
            Tocdogio = Tocdogio / (yy - 2008);
            ctt.apsuat = Aps;
            ctt.luongmua = Luongm;
            ctt.nhietdo = Nhietd;
            ctt.tmax = Tmax;
            ctt.tmin = Tmin;
            ctt.tocdogio = Tocdogio;
            ctt.tenTram = tenTram;
            ctt.Hinh = anh;
            ctt.Manuoc = manuoc;
            ViewData["arr"] = arr;
            return View(ctt);
        }
        [HttpPost]
        public ActionResult dubao( string manuoc,string tentram)
        {
            dubaoviewmodel ctt = new dubaoviewmodel();
            string d, m, y, anh;
            int dd, mm, yy;
            int Nhietd = 0, Aps = 0, Luongm = 0, Tmax = 0, Tmin = 0,Tocdogio=0;
            d = DateTime.Now.Day.ToString();
            m = DateTime.Now.Month.ToString();
            y = DateTime.Now.Year.ToString();
            dd = (int.Parse(d));
            mm = int.Parse(m);
            yy = int.Parse(y);
            List<string> arr = new List<string>();
            var trams = db.TRAMS.Where(c => c.MANUOC == manuoc);
            var nuocs = db.NUOCS.Where(c => c.MANUOC == manuoc);
            anh = nuocs.First().HINH;
            foreach (var item in trams)
            {
                arr.Add(item.TENTRAM);
            }
            for (int i = 2008; i < yy; i++)
            {
                var CTT = db.CHITIETTRAMS.Include(t => t.TRAM).Where(c => c.DA == 18 && c.MO == mm && c.YEAR == i && c.TRAM.NUOC.MANUOC == manuoc && c.TRAM.TENTRAM == tentram).First();
                Nhietd += Convert.ToInt32(CTT.NHIETDO);
                Luongm += Convert.ToInt32(CTT.LUONGMUA);
                Aps += Convert.ToInt32(CTT.APSUAT);
                Tmax += Convert.ToInt32(CTT.TMAX);
                Tmin += Convert.ToInt32(CTT.TMIN);
                Tocdogio += Convert.ToInt32(CTT.TOCDOGIO);
                
            }
            Nhietd = Nhietd / (yy - 2008);
            Nhietd = ((Nhietd - 32) * 5) / 9;
            Luongm = Luongm / (yy - 2008);
            Aps = Aps / (yy - 2008);
            Tmin = Tmin / (yy - 2008);
            Tmin = ((Tmin - 32) * 5) / 9;
            Tmax = Tmax / (yy - 2008);
            Tmax = ((Tmax - 32) * 5) / 9;
            Tocdogio = Tocdogio/ (yy - 2008);
            ctt.apsuat = Aps;
            ctt.luongmua = Luongm;
            ctt.nhietdo = Nhietd;
            ctt.tmax = Tmax;
            ctt.tmin = Tmin;
            ctt.tocdogio = Tocdogio;
            ctt.tenTram = tentram;
            ctt.Hinh = anh;
            ctt.Manuoc = manuoc;
            ViewData["arr"] = arr;
            return View(ctt);


        }
        public ActionResult Update()
        {
            string d, m, y;
            int dd, mm, yy;//ngày tháng năm hiện tại, có gì tại lấy ngày -7
            int Nhietd = 0, Aps = 0, Luongm = 0, Tmax = 0, Tmin = 0, Tocdogio = 0;
            d = DateTime.Now.Day.ToString();
            m = DateTime.Now.Month.ToString();
            y = DateTime.Now.Year.ToString();
            dd = (int.Parse(d));
            mm = int.Parse(m);
            yy = int.Parse(y);
            List<CHITIETTRAM> re = new List<CHITIETTRAM>();//tài add data chi tiết trạm của mỡi trạm của ngày trên vào đây
            List<int> arr = new List<int>();//đây là arr gồm 682   trạm của tất cả các nước
            var arrtram = db.TRAMS.ToList();
            
      
            foreach (var item in arrtram)
            {
                arr.Add(item.MATRAM);
            }
            //using (BigQueryClient client = BigQueryClient.Create("phantantai", GoogleCredential.FromFile("../../phantantai-c3450caeb9b5.json")))
            //{
            //    string query = $@"
            //	SELECT stn as MATRAM,da as DA,mo as MO,year as YEAR ,temp as NHIETDO,slp as APSUAT,wdsp as TOCDOGIO,max as TMAX,min as TMIN,prcp as LUONGMUA 
            //	FROM `bigquery-public-data.noaa_gsod.gsod2019` 
            //	where stn = '{matram}' and mo = '{mo}' and da > '{da}'";
            //    BigQueryJob job = client.CreateQueryJob(
            //        sql: query,
            //       parameters: null,
            //       options: new QueryOptions { UseQueryCache = false });
            // Wait for the job to complete.
            //    job.PollUntilCompleted();
            //    CHITIETTRAM chitie = new CHITIETTRAM();
            //    foreach (BigQueryRow row in client.GetQueryResults(job.Reference))
            //  {
            //      chitie.MATRAM = int.Parse($"{row["MATRAM"]}");
            //      chitie.MO = byte.Parse($"{row["MO"]}");
            //     chitie.DA = short.Parse($"{row["DA"]}");
            //    chitie.YEAR = short.Parse($"{row["YEAR"]}");

            //    var nd = float.Parse($"{row["NHIETDO"]}");
            //    chitie.NHIETDO = (byte)nd;

            //    var tmp = float.Parse($"{row["APSUAT"]}");
            //    chitie.APSUAT = tmp < 900 ? (int)tmp : 0;
            //   tmp = float.Parse($"{row["TOCDOGIO"]}");
            //  chitie.TOCDOGIO = tmp < 900 ? (short)tmp : (short)0;
            //  tmp = float.Parse($"{row["TMAX"]}");
            //  chitie.TMAX = tmp < 900 ? (short)tmp : (short)0;
            //  tmp = float.Parse($"{row["TMIN"]}");
            //  chitie.TMIN = tmp < 900 ? (short)tmp : (short)0;
            //  tmp = float.Parse($"{row["LUONGMUA"]}");
            // chitie.LUONGMUA = tmp < 900 ? (short)tmp : (short)0;
            // re.Add(chitie);
            // }
            return View();
            
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}