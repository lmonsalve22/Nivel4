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
            return View(db.LEVEL1.OrderBy(c => c.ID_LEVEL1).ToList());
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
            lEVEL1.ID_LEVEL1 = db.LEVEL1.Count() + 1;
            if (ModelState.IsValid)
            {
                db.LEVEL1.Add(lEVEL1);
                db.SaveChanges();
                db.Database.ExecuteSqlCommand("BEGIN LLENAR_MENU; END; ");
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
                var list = db.LEVEL4.Where(c => c.LEVEL3.LEVEL2.LEVEL1.ID_LEVEL1 == lEVEL1.ID_LEVEL1);
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
                db.Database.ExecuteSqlCommand("BEGIN LLENAR_MENU; END; ");
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
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                var lEVEL2 = db.LEVEL2.Include(l => l.LEVEL1).Where(c => c.LEVEL1.ID_LEVEL1 == lEVEL1.ID_LEVEL1);
                //return View(    lEVEL4.ToListAsync());
                ViewBag.Level1 = lEVEL1;
                return View("Level2PorBorrar", lEVEL2.ToList());
            }
            //db.Database.ExecuteSqlCommand("BEGIN LLENAR_MENU; END; ");
            if (!Ordenador.GenerarMenuDinamico())
            {
                return View("ErrorPage");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Level2PorBorrar()
        {
            return View();
        }

        public ActionResult DeleteAll(decimal id)
        {
            LEVEL1 lEVEL1 = db.LEVEL1.Find(id);
            var lEVEL2 = db.LEVEL2.Where(c => c.LEVEL1.ID_LEVEL1 == lEVEL1.ID_LEVEL1);
            List<LEVEL2> recorrido = lEVEL2.ToList();
            foreach (var item in recorrido)
            {
                var lEVEL3 = db.LEVEL3.Where(c => c.LEVEL2.ID_LEVEL2 == item.ID_LEVEL2);
                List<LEVEL3> recorridoLevel3 = lEVEL3.ToList();
                foreach (var itemLevel3 in recorridoLevel3)
                {
                    var lEVEL4 = db.LEVEL4.Where(c => c.LEVEL3.ID_LEVEL3 == itemLevel3.ID_LEVEL3);
                    List<LEVEL4> recorridoLevel4 = lEVEL4.ToList();
                    foreach (var itemLevel4 in recorridoLevel4)
                    {
                        db.LEVEL4.Remove(itemLevel4);
                    }
                    db.SaveChanges();
                    db.LEVEL3.Remove(itemLevel3);
                }
                db.SaveChanges();
                db.LEVEL2.Remove(item);
            }
            db.SaveChanges();
            db.LEVEL1.Remove(lEVEL1);
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
