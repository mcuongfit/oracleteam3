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
    public class NUOCsController : Controller
    {
        private contexts db = new contexts();

        // GET: NUOCs
        public ActionResult Index()
        {
            return View(db.NUOCS.ToList());
        }

        // GET: NUOCs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NUOC nUOC = db.NUOCS.Find(id);
            if (nUOC == null)
            {
                return HttpNotFound();
            }
            return View(nUOC);
        }

        // GET: NUOCs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NUOCs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MANUOC,TENNUOC,HINH")] NUOC nUOC)
        {
            if (ModelState.IsValid)
            {
                db.NUOCS.Add(nUOC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nUOC);
        }

        // GET: NUOCs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NUOC nUOC = db.NUOCS.Find(id);
            if (nUOC == null)
            {
                return HttpNotFound();
            }
            return View(nUOC);
        }

        // POST: NUOCs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MANUOC,TENNUOC,HINH")] NUOC nUOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nUOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nUOC);
        }

        // GET: NUOCs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NUOC nUOC = db.NUOCS.Find(id);
            if (nUOC == null)
            {
                return HttpNotFound();
            }
            return View(nUOC);
        }

        // POST: NUOCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NUOC nUOC = db.NUOCS.Find(id);
            db.NUOCS.Remove(nUOC);
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
