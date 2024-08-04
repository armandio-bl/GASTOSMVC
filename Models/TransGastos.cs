using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GASTOSMVC.Models
{
    public class TransGastos
    {
        public int IDGas { get; set; }
        public DateTime Fecha { get; set; }
        public string Gastos { get; set; }
        public decimal Gasto { get; set; }
        public string WCBXGas { get; set; }
        public string WDia1 { get; set; }
        public string WDia2 { get; set; }        
        public string WCBXMes1 { get; set; }
        public string WCBXMes2 { get; set; }
        public string WCBXYear1 { get; set; }
        public string WCBXYear2 { get; set; }
        public string WSemana { get; set; }
        public string WTotal { get; set; }
        public List<SelectListItem> LCBXGas { get; set; }
        public List<SelectListItem> LCBXDia1 { get; set; }
        public List<SelectListItem> LCBXDia2 { get; set; }
        public List<SelectListItem> LCBXMes1 { get; set; }
        public List<SelectListItem> LCBXMes2 { get; set; }
        public List<SelectListItem> LCBXYear1 { get; set; }
        public List<SelectListItem> LCBXYear2 { get; set; }
        public IEnumerable<TransGastos> IEnumTransGastos { get; set; }
    }
}