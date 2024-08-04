using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GASTOSMVC.Models
{
    public class Transaccion
    {
        public int IDGas { get; set; }
        public string WCBXGas { get; set; }
        public string WCBXGastos { get; set; }
        public string WDia1 { get; set; }
        public string WCBXMes1 { get; set; }
        public string WCBXYear1 { get; set; }
        public decimal dGastos { get; set; }
        public List<SelectListItem> LCBXGas { get; set; }
        public List<SelectListItem> LCBXDia1 { get; set; }
        public List<SelectListItem> LCBXMes1 { get; set; }
        public List<SelectListItem> LCBXYear1 { get; set; }
        public List<SelectListItem> LCBXGastos { get; set; }
    }
}