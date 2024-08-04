using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GASTOSMVC.Models;

namespace GASTOSMVC.Controllers
{
    public class AdmonController : Controller
    {
        // GET: Admon
        public ActionResult Index()
        {
            Admon oAdmon = new Admon();
            UtilAdmon oUltlAdmn = new UtilAdmon();
            string WIDUsr = Request.Cookies["IDUsr"].Value;
            Combos oCBX = new Combos();
            oAdmon.LCBXUsr = oCBX.FNLLenaCombo("NomCom", "IDUsr", "SP_User", "@IDUsr", WIDUsr);
            oAdmon.IDUsr = 0;
            oAdmon.Nombre = ""; oAdmon.Paterno = ""; oAdmon.Materno = "";
            oAdmon.Usuario = ""; oAdmon.Password = "";
            oCBX = null;
            return View(oAdmon);
        }

        [HttpPost]
        public ActionResult ConsAdmon(FormCollection oForm)
        {
            string WIDAdmon = oForm["HDcbxAdmon"].ToString();
            Admon oAdmon = new Admon();
            UtilAdmon oUtlAdmn = new UtilAdmon();
            var oAdm = oUtlAdmn.FNConsAdmon(WIDAdmon);
            oAdmon.IDUsr = oAdm.IDUsr;
            oAdmon.Nombre = oAdm.Nombre; oAdmon.Paterno = oAdm.Paterno;  oAdmon.Materno = oAdm.Materno;
            oAdmon.Usuario = oAdm.Usuario; oAdmon.Password = oAdm.Password;
            Combos oCBX = new Combos();
            oAdmon.LCBXUsr = oCBX.FNLLenaCombo("NomCom", "IDUsr", "SP_User", "@IDUsr", "1");
            oCBX = null;
            return View("Index", oAdmon);
        }
        [HttpPost]
        public ActionResult Guardar(FormCollection oForm)
        {
            string WIDUsr = oForm["HDcbxAdmon"].ToString();
            string WNom = oForm["txtNom"].ToString(); string WPat = oForm["txtPaterno"].ToString(); string WMat = oForm["txtMaterno"].ToString();
            string WUsr = oForm["txtUsuario"].ToString(); string WPass = oForm["txtContra"].ToString();

            UtilAdmon OUtilAdmon = new UtilAdmon();
            OUtilAdmon.SPGuardar(WNom, WPat, WMat, WUsr, WPass, WIDUsr);
            OUtilAdmon = null;
            return RedirectToAction("Index", "Admon");
        }

        public ActionResult Borrar(FormCollection oForm)
        {
            string WIDUsr = oForm["HDcbxAdmon"].ToString();
            UtilAdmon oUtlAdmon = new UtilAdmon();
            oUtlAdmon.SPBorrar(WIDUsr); 
            oUtlAdmon = null;
            return RedirectToAction("Index", "Admon");
        }
    }
}