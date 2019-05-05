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
    public class TRAMsController : Controller
    {
        private contexts db = new contexts();

        // GET: TRAMs
        public ActionResult Index()
        {
            var tRAMs = db.TRAMS.Include(t => t.NUOC);
            return View(tRAMs.ToList());
        }

        // GET: TRAMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRAM tRAM = db.TRAMS.Find(id);
            if (tRAM == null)
            {
                return HttpNotFound();
            }
            return View(tRAM);
        }

        // GET: TRAMs/Create
        public ActionResult Create()
        {
            ViewBag.MANUOC = new SelectList(db.NUOCS, "MANUOC", "TENNUOC");
            return View();
        }

        // POST: TRAMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MATRAM,MANUOC,TENTRAM,KINHDO,VIDO,DOCAO,HINH")] TRAM tRAM)
        {
            if (ModelState.IsValid)
            {
                db.TRAMS.Add(tRAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MANUOC = new SelectList(db.NUOCS, "MANUOC", "TENNUOC", tRAM.MANUOC);
            return View(tRAM);
        }

        // GET: TRAMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRAM tRAM = db.TRAMS.Find(id);
            if (tRAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MANUOC = new SelectList(db.NUOCS, "MANUOC", "TENNUOC", tRAM.MANUOC);
            return View(tRAM);
        }

        // POST: TRAMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MATRAM,MANUOC,TENTRAM,KINHDO,VIDO,DOCAO,HINH")] TRAM tRAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tRAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MANUOC = new SelectList(db.NUOCS, "MANUOC", "TENNUOC", tRAM.MANUOC);
            return View(tRAM);
        }

        // GET: TRAMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRAM tRAM = db.TRAMS.Find(id);
            if (tRAM == null)
            {
                return HttpNotFound();
            }
            return View(tRAM);
        }

        // POST: TRAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TRAM tRAM = db.TRAMS.Find(id);
            db.TRAMS.Remove(tRAM);
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
