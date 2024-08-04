using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GASTOSMVC.Models;

namespace GASTOSMVC.Controllers
{
    public class AccController : Controller
    {
        // GET: Acc
        public ActionResult Index()
        {
            Usuario oUser = new Usuario();
            oUser.nPaso = 0;
            return View(oUser);
        }

        [HttpPost]
        public ActionResult Valida(FormCollection oCollec)
        {
            AccValida oMnVal = new AccValida();
            string WUser = oCollec["txtUsr"].ToString(); string WContra = oCollec["txtContra"].ToString();
            int nValida = oMnVal.Validar(WUser, WContra); oMnVal = null;
            Response.Cookies["IDUsr"].Value = nValida.ToString(); oCollec = null;
            if (nValida == 0)
            {
                Usuario oValida = new Usuario(); oValida.nPaso = 1;
                return View("Index", oValida);
            }
            else
            {
                return RedirectToAction("Index", "ROT");
            }
        }

    }
}