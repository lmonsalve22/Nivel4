//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdministradorNivel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MENU006
    {
        public MENU006()
        {
            this.MENU0061 = new HashSet<MENU006>();
        }
    
        public decimal ID { get; set; }
        public string NAME { get; set; }
        public Nullable<decimal> PARENTID { get; set; }
    
        public virtual ICollection<MENU006> MENU0061 { get; set; }
        public virtual MENU006 MENU0062 { get; set; }
    }
}
