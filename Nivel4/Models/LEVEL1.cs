//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nivel4.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LEVEL1
    {
        public LEVEL1()
        {
            this.LEVEL2 = new HashSet<LEVEL2>();
        }
    
        public decimal ID_LEVEL1 { get; set; }
        public string NAME_LEVEL1 { get; set; }
    
        public virtual ICollection<LEVEL2> LEVEL2 { get; set; }
    }
}