using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AdministradorNivel.Models;

namespace AdministradorNivel.Models
{
    public class Ordenador
    {
        static private Entities db = new Entities();
        static public bool GenerarMenuDinamico()
        {
            bool salida = true;
            try
            {
                db.Database.ExecuteSqlCommand("BEGIN LLENAR_MENU; END; ");
            }
            catch (Exception)
            {

                salida = false;
            }
            return salida;
        }
        
        static public void OrdenarLevel()
        {
            decimal contador = db.LEVEL1.Count();
            decimal maximo = db.LEVEL1.Max(c => c.ID_LEVEL1);
            if (db.LEVEL1.Count() != db.LEVEL1.Max(c => c.ID_LEVEL1))
            {
                var lista = db.LEVEL1.OrderBy(c => c.ID_LEVEL1);                
                int i = 1;
                foreach (var item in lista)
                {
                    if (item.ID_LEVEL1 != i)
                    {
                        LEVEL1 aux = new LEVEL1();
                        aux.ID_LEVEL1 = i;
                        db.LEVEL1.Add(aux);
                        db.SaveChanges();
                    }
                    i++;
                }
            }
        }
        
    }
}