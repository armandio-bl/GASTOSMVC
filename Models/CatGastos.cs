using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GASTOSMVC.Models
{
    public class CatGastos
    {
        public int IDGas { get; set; }
        public string WCBXGastos { get; set; }
        public List<SelectListItem> LCBXGastos { get; set; }
        public string Gastos { get; set; }
    }
}