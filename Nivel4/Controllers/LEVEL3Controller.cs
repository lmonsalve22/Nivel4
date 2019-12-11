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
    public class LEVEL3Controller : Controller
    {
        private Entities db = new Entities();

        // GET: LEVEL3
        public ActionResult Index()
        {
            var lEVEL3 = db.LEVEL3.Include(l => l.LEVEL2);
            return View(lEVEL3.ToList());
        }

        // GET: LEVEL3/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LEVEL3 lEVEL3 = db.LEVEL3.Find(id);
            if (lEVEL3 == null)
            {
                return HttpNotFound();
            }
            return View(lEVEL3);
        }

        // GET: LEVEL3/Create
        public ActionResult Create()
        {
            ViewBag.ID_LEVEL2 = new SelectList(db.LEVEL2, "ID_LEVEL2", "NAME_LEVEL2");
            return View();
        }

        // POST: LEVEL3/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_LEVEL3,ID_LEVEL2,NAME_LEVEL3")] LEVEL3 lEVEL3)
        {
            lEVEL3.ID_LEVEL3 = db.LEVEL3.Count() + 1;
            if (ModelState.IsValid)
            {
                db.LEVEL3.Add(lEVEL3);
                db.SaveChanges();
                db.Database.ExecuteSqlCommand("BEGIN LLENAR_MENU; END; ");
                return RedirectToAction("Index");
            }

            ViewBag.ID_LEVEL2 = new SelectList(db.LEVEL2, "ID_LEVEL2", "NAME_LEVEL2", lEVEL3.ID_LEVEL2);
            return View(lEVEL3);
        }

        // GET: LEVEL3/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LEVEL3 lEVEL3 = db.LEVEL3.Find(id);
            if (lEVEL3 == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_LEVEL2 = new SelectList(db.LEVEL2, "ID_LEVEL2", "NAME_LEVEL2", lEVEL3.ID_LEVEL2);
            return View(lEVEL3);
        }

        // POST: LEVEL3/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_LEVEL3,ID_LEVEL2,NAME_LEVEL3")] LEVEL3 lEVEL3)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(lEVEL3).State = EntityState.Modified;
                db.SaveChanges();
                var list = db.LEVEL4.Where(c => c.ID_LEVEL3 == lEVEL3.ID_LEVEL3); 
                foreach (var item in list)
                {
                    LEVEL4 level4aux = db.LEVEL4.Find(item.ID_LEVEL4);
                    LEVEL2 level2aux = db.LEVEL2.Find(item.LEVEL3.ID_LEVEL2);
                    level4aux.TAG = level2aux.LEVEL1.NAME_LEVEL1 + " " +
                                     level2aux.NAME_LEVEL2 + " " +
                                     level4aux.LEVEL3.NAME_LEVEL3 + " " +
                                     level4aux.NAME_LEVEL4;
                    db.Entry(level4aux).State = EntityState.Modified;
                    
                }
                db.SaveChanges();
                db.Database.ExecuteSqlCommand("BEGIN LLENAR_MENU; END; ");
                return RedirectToAction("Index");
            }
            ViewBag.ID_LEVEL2 = new SelectList(db.LEVEL2, "ID_LEVEL2", "NAME_LEVEL2", lEVEL3.ID_LEVEL2);
            return View(lEVEL3);
        }

        // GET: LEVEL3/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LEVEL3 lEVEL3 = db.LEVEL3.Find(id);
            if (lEVEL3 == null)
            {
                return HttpNotFound();
            }
            return View(lEVEL3);
        }

        // POST: LEVEL3/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            LEVEL3 lEVEL3 = db.LEVEL3.Find(id);
            db.LEVEL3.Remove(lEVEL3);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                var lEVEL4 = db.LEVEL4.Include(l => l.LEVEL3).Where(c => c.LEVEL3.ID_LEVEL3 == lEVEL3.ID_LEVEL3);
                //return View(    lEVEL4.ToListAsync());
                ViewBag.Level3 = lEVEL3;
                return View("Level4PorBorrar", lEVEL4.ToList()); 
            }
            
            db.Database.ExecuteSqlCommand("BEGIN LLENAR_MENU; END; ");
            return RedirectToAction("Index");
        }
        public ActionResult Level4PorBorrar()
        {
            return View();
        }

        public ActionResult DeleteAll(decimal id)
        {
            LEVEL3 lEVEL3 = db.LEVEL3.Find(id);
            var lEVEL4 = db.LEVEL4.Where(c => c.LEVEL3.ID_LEVEL3 == lEVEL3.ID_LEVEL3);
            List<LEVEL4> recorrido = lEVEL4.ToList();
            foreach (var item in recorrido)
            {
                db.LEVEL4.Remove(item);
            }
            db.SaveChanges();
            db.LEVEL3.Remove(lEVEL3);
            db.SaveChanges();
            db.Database.ExecuteSqlCommand("BEGIN LLENAR_MENU; END; ");
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
