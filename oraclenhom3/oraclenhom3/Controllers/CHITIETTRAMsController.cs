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
    public class CHITIETTRAMsController : Controller
    {
        private contexts db = new contexts();

        // GET: CHITIETTRAMs
        public ActionResult Index()
        {
            var cHITIETTRAMS = db.CHITIETTRAMS.Include(c => c.TRAM);
            return View(cHITIETTRAMS.ToList());
        }

        // GET: CHITIETTRAMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETTRAM cHITIETTRAM = db.CHITIETTRAMS.Find(id);
            if (cHITIETTRAM == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETTRAM);
        }

        // GET: CHITIETTRAMs/Create
        public ActionResult Create()
        {
            ViewBag.MATRAM = new SelectList(db.TRAMS, "MATRAM", "MANUOC");
            return View();
        }

        // POST: CHITIETTRAMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MATRAM,DA,MO,YEAR,NHIETDO,APSUAT,TOCDOGIO,TMAX,TMIN,LUONGMUA")] CHITIETTRAM cHITIETTRAM)
        {
            if (ModelState.IsValid)
            {
                db.CHITIETTRAMS.Add(cHITIETTRAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MATRAM = new SelectList(db.TRAMS, "MATRAM", "MANUOC", cHITIETTRAM.MATRAM);
            return View(cHITIETTRAM);
        }

        // GET: CHITIETTRAMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETTRAM cHITIETTRAM = db.CHITIETTRAMS.Find(id);
            if (cHITIETTRAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MATRAM = new SelectList(db.TRAMS, "MATRAM", "MANUOC", cHITIETTRAM.MATRAM);
            return View(cHITIETTRAM);
        }

        // POST: CHITIETTRAMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MATRAM,DA,MO,YEAR,NHIETDO,APSUAT,TOCDOGIO,TMAX,TMIN,LUONGMUA")] CHITIETTRAM cHITIETTRAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHITIETTRAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MATRAM = new SelectList(db.TRAMS, "MATRAM", "MANUOC", cHITIETTRAM.MATRAM);
            return View(cHITIETTRAM);
        }

        // GET: CHITIETTRAMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETTRAM cHITIETTRAM = db.CHITIETTRAMS.Find(id);
            if (cHITIETTRAM == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETTRAM);
        }

        // POST: CHITIETTRAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CHITIETTRAM cHITIETTRAM = db.CHITIETTRAMS.Find(id);
            db.CHITIETTRAMS.Remove(cHITIETTRAM);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
