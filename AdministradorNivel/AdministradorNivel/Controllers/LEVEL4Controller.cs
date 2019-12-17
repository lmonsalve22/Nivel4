using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AdministradorNivel.Models;

namespace AdministradorNivel.Controllers
{
    public class LEVEL4Controller : Controller
    {
        private Entities db = new Entities();

        // GET: LEVEL4
        public async Task<ActionResult> Index()
        {
            var lEVEL4 = db.LEVEL4.Include(l => l.LEVEL3).OrderBy(c => c.ID_LEVEL4);
            return View(await lEVEL4.ToListAsync());
        }

        // GET: LEVEL4/Details/5
        public async Task<ActionResult> Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LEVEL4 lEVEL4 = await db.LEVEL4.FindAsync(id);
            if (lEVEL4 == null)
            {
                return HttpNotFound();
            }
            return View(lEVEL4);
        }

        // GET: LEVEL4/Create
        public ActionResult Create()
        {
            ViewBag.ID_LEVEL3 = new SelectList(db.LEVEL3, "ID_LEVEL3", "NAME_LEVEL3");
            return View();
        }

        // POST: LEVEL4/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "ID_LEVEL4,ID_LEVEL3,NAME_LEVEL4,VINCULOPOWERBI,TAG")] LEVEL4 lEVEL4)
        public ActionResult Create([Bind(Include = "ID_LEVEL4,ID_LEVEL3,NAME_LEVEL4,VINCULOPOWERBI,TAG")] LEVEL4 lEVEL4)
        {
            //lEVEL4.ID_LEVEL4 = db.LEVEL4.Max(c => c.ID_LEVEL4) + 1;
            lEVEL4.ID_LEVEL4 = db.LEVEL4.Count() + 1;
            LEVEL3 level3aux = db.LEVEL3.Find(lEVEL4.ID_LEVEL3);
            lEVEL4.TAG = level3aux.LEVEL2.LEVEL1.NAME_LEVEL1 + " " +
                         level3aux.LEVEL2.NAME_LEVEL2 + " " +
                         level3aux.NAME_LEVEL3 + " " +
                         lEVEL4.NAME_LEVEL4;
            if (ModelState.IsValid)
            {
                db.LEVEL4.Add(lEVEL4);
                if (!Ordenador.GenerarMenuDinamico())
                {
                    return View("ErrorPage");
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_LEVEL3 = new SelectList(db.LEVEL3, "ID_LEVEL3", "NAME_LEVEL3", lEVEL4.ID_LEVEL3);
            return View(lEVEL4);
        }

        // GET: LEVEL4/Edit/5
        public async Task<ActionResult> Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LEVEL4 lEVEL4 = await db.LEVEL4.FindAsync(id);
            if (lEVEL4 == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_LEVEL3 = new SelectList(db.LEVEL3, "ID_LEVEL3", "NAME_LEVEL3", lEVEL4.ID_LEVEL3);
            return View(lEVEL4);
        }

        // POST: LEVEL4/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "ID_LEVEL4,ID_LEVEL3,NAME_LEVEL4,VINCULOPOWERBI,TAG")] LEVEL4 lEVEL4)
        public ActionResult Edit([Bind(Include = "ID_LEVEL4,ID_LEVEL3,NAME_LEVEL4,VINCULOPOWERBI,TAG")] LEVEL4 lEVEL4)
        {
            if (ModelState.IsValid)
            {
                LEVEL3 level3aux = db.LEVEL3.Find(lEVEL4.ID_LEVEL3);
                lEVEL4.TAG = level3aux.LEVEL2.LEVEL1.NAME_LEVEL1 + " " +
                             level3aux.LEVEL2.NAME_LEVEL2 + " " +
                             level3aux.NAME_LEVEL3 + " " +
                             lEVEL4.NAME_LEVEL4;
                db.Entry(lEVEL4).State = EntityState.Modified;
                db.SaveChanges();
                if (!Ordenador.GenerarMenuDinamico())
                {
                    return View("ErrorPage");
                }

                return RedirectToAction("Index");
            }
            ViewBag.ID_LEVEL3 = new SelectList(db.LEVEL3, "ID_LEVEL3", "NAME_LEVEL3", lEVEL4.ID_LEVEL3);
            return View(lEVEL4);
        }

        // GET: LEVEL4/Delete/5
        public async Task<ActionResult> Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LEVEL4 lEVEL4 = await db.LEVEL4.FindAsync(id);
            if (lEVEL4 == null)
            {
                return HttpNotFound();
            }
            return View(lEVEL4);
        }

        // POST: LEVEL4/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(decimal id)
        public ActionResult DeleteConfirmed(decimal id)
        {
            LEVEL4 lEVEL4 = db.LEVEL4.Find(id);
            db.LEVEL4.Remove(lEVEL4);
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
