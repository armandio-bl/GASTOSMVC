using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GASTOSMVC.Models
{
    public class Asign
    {
        public string WIDUsr { get; set; }
        public string  WIDMenu { get; set; }
        public string Menu { get; set; }
        public List<SelectListItem> LCBXUser { get; set; }
        public List<SelectListItem> LCBXMenu { get; set; }
        public IEnumerable<Asign> IEnumAsig { get; set; }

    }
}