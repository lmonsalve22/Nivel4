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
    public class LEVEL1Controller : Controller
    {
        private Entities db = new Entities();

        // GET: LEVEL1
        public ActionResult Index()
        {
            return View(db.LEVEL1.ToList());
        }

        // GET: LEVEL1/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LEVEL1 lEVEL1 = db.LEVEL1.Find(id);
            if (lEVEL1 == null)
            {
                return HttpNotFound();
            }
            return View(lEVEL1);
        }

        // GET: LEVEL1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LEVEL1/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_LEVEL1,NAME_LEVEL1")] LEVEL1 lEVEL1)
        {
            if (ModelState.IsValid)
            {
                db.LEVEL1.Add(lEVEL1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lEVEL1);
        }

        // GET: LEVEL1/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LEVEL1 lEVEL1 = db.LEVEL1.Find(id);
            if (lEVEL1 == null)
            {
                return HttpNotFound();
            }
            return View(lEVEL1);
        }

        // POST: LEVEL1/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_LEVEL1,NAME_LEVEL1")] LEVEL1 lEVEL1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lEVEL1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lEVEL1);
        }

        // GET: LEVEL1/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LEVEL1 lEVEL1 = db.LEVEL1.Find(id);
            if (lEVEL1 == null)
            {
                return HttpNotFound();
            }
            return View(lEVEL1);
        }

        // POST: LEVEL1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            LEVEL1 lEVEL1 = db.LEVEL1.Find(id);
            db.LEVEL1.Remove(lEVEL1);
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
