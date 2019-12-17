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
            var lEVEL2 = db.LEVEL2.Include(l => l.LEVEL1).OrderBy(c => c.ID_LEVEL2);
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
            lEVEL2.ID_LEVEL2 = db.LEVEL2.Count() + 1;
            if (ModelState.IsValid)
            {
                db.LEVEL2.Add(lEVEL2);
                db.SaveChanges();
                if (!Ordenador.GenerarMenuDinamico())
                {
                    return View("ErrorPage");
                }
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
                var list = db.LEVEL4.Where(c => c.LEVEL3.LEVEL2.ID_LEVEL2 == lEVEL2.ID_LEVEL2);
                foreach (var item in list)
                {
                    LEVEL4 level4aux = db.LEVEL4.Find(item.ID_LEVEL4);
                    LEVEL2 level2aux = db.LEVEL2.Find(item.LEVEL3.ID_LEVEL2);
                    LEVEL1 level1aux = db.LEVEL1.Find(level2aux.ID_LEVEL1);
                    level4aux.TAG = level2aux.LEVEL1.NAME_LEVEL1 + " " +
                                     level2aux.NAME_LEVEL2 + " " +
                                     level4aux.LEVEL3.NAME_LEVEL3 + " " +
                                     level4aux.NAME_LEVEL4;
                    db.Entry(level4aux).State = EntityState.Modified;

                }
                db.SaveChanges();
                if (!Ordenador.GenerarMenuDinamico())
                {
                    return View("ErrorPage");
                }
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
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                var lEVEL3 = db.LEVEL3.Include(l => l.LEVEL2).Where(c => c.LEVEL2.ID_LEVEL2 == lEVEL2.ID_LEVEL2);
                //return View(    lEVEL4.ToListAsync());
                ViewBag.Level2 = lEVEL2;
                return View("Level3PorBorrar", lEVEL3.ToList());
            }
            if (!Ordenador.GenerarMenuDinamico())
            {
                return View("ErrorPage");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Level3PorBorrar()
        {
            return View();
        }

        public ActionResult DeleteAll(decimal id)
        {
            LEVEL2 lEVEL2 = db.LEVEL2.Find(id);
            var lEVEL3 = db.LEVEL3.Where(c => c.LEVEL2.ID_LEVEL2 == lEVEL2.ID_LEVEL2);
            List<LEVEL3> recorrido = lEVEL3.ToList();
            foreach (var item in recorrido)
            {
                var lEVEL4 = db.LEVEL4.Where(c => c.LEVEL3.ID_LEVEL3 == item.ID_LEVEL3);
                List<LEVEL4> recorridoLevel4 = lEVEL4.ToList();
                foreach (var itemLevel4 in recorridoLevel4)
                {
                    db.LEVEL4.Remove(itemLevel4);
                }
                db.SaveChanges();
                db.LEVEL3.Remove(item);
            }
            db.SaveChanges();
            db.LEVEL2.Remove(lEVEL2);
            db.SaveChanges();
            if (!Ordenador.GenerarMenuDinamico())
            {
                return View("ErrorPage");
            }
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
