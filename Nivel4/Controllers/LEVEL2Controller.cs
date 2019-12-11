using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nivel4.Models;

namespace Nivel4.Controllers
{
    public class LEVEL2Controller : Controller
    {
        private Entities db = new Entities();

        // GET: LEVEL2
        public ActionResult Index()
        {
            var lEVEL2 = db.LEVEL2.Include(l => l.LEVEL1);
            return View(lEVEL2.ToList());
        }

        // GET: LEVEL2/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LEVEL2 lEVEL2 = db.LEVEL2.Find(id);
            if (lEVEL2 == null)
            {
                return HttpNotFound();
            }
            return View(lEVEL2);
        }

        // GET: LEVEL2/Create
        public ActionResult Create()
        {
            ViewBag.ID_LEVEL1 = new SelectList(db.LEVEL1, "ID_LEVEL1", "NAME_LEVEL1");
            return View();
        }

        // POST: LEVEL2/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_LEVEL2,ID_LEVEL1,NAME_LEVEL2")] LEVEL2 lEVEL2)
        {
            if (ModelState.IsValid)
            {
                db.LEVEL2.Add(lEVEL2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_LEVEL1 = new SelectList(db.LEVEL1, "ID_LEVEL1", "NAME_LEVEL1", lEVEL2.ID_LEVEL1);
            return View(lEVEL2);
        }

        // GET: LEVEL2/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LEVEL2 lEVEL2 = db.LEVEL2.Find(id);
            if (lEVEL2 == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_LEVEL1 = new SelectList(db.LEVEL1, "ID_LEVEL1", "NAME_LEVEL1", lEVEL2.ID_LEVEL1);
            return View(lEVEL2);
        }

        // POST: LEVEL2/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_LEVEL2,ID_LEVEL1,NAME_LEVEL2")] LEVEL2 lEVEL2)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lEVEL2).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_LEVEL1 = new SelectList(db.LEVEL1, "ID_LEVEL1", "NAME_LEVEL1", lEVEL2.ID_LEVEL1);
            return View(lEVEL2);
        }

        // GET: LEVEL2/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LEVEL2 lEVEL2 = db.LEVEL2.Find(id);
            if (lEVEL2 == null)
            {
                return HttpNotFound();
            }
            return View(lEVEL2);
        }

        // POST: LEVEL2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            LEVEL2 lEVEL2 = db.LEVEL2.Find(id);
            db.LEVEL2.Remove(lEVEL2);
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
