using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GASTOSMVC.Models;

namespace GASTOSMVC.Controllers
{
    public class ROTController : Controller
    {
        // GET: ROT
        public ActionResult Index()
        {
            ROT oROT = new ROT();
            string WIDUsr = Request.Cookies["IDUsr"].Value;
            oROT.WGIDUsr = WIDUsr; oROT.FNUser();
            oROT.IEnumMenu =  oROT.LlenaTabla(WIDUsr);
            return View(oROT);
        }
    }
}