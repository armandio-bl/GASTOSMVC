using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GASTOSMVC.Models
{
    public class Admon
    {
        public int IDUsr { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public List<SelectListItem> LCBXUsr { get; set; }
    }
}