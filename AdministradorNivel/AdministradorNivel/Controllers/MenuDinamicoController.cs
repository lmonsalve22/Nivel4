using AdministradorNivel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AdministradorNivel.Controllers
{
    public class MenuDinamicoController : Controller
    {
        private Entities db = new Entities();
        // GET: MenuDinamico
        public PartialViewResult VistaParcial()
        {
            //var listaMenu = db.MENU006.ToList();
            var listaMenu = db.MENU006.Where(x => x.PARENTID == null).ToList();
            return PartialView("VistaParcial", listaMenu);
        }
    }
}