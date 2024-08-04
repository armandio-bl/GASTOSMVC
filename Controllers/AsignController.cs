using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GASTOSMVC.Models;

namespace GASTOSMVC.Controllers
{
    public class AsignController : Controller
    {
        // GET: Asign
        public ActionResult Index()
        {
            UtilAsign oUtlAsign = new UtilAsign();
            Asign oAsig = new Asign();
            string WUser = Request.Cookies["IDUsr"].Value;
            Combos oCBX = new Combos();
            oAsig.LCBXUser = oCBX.FNLLenaCombo("NomCom", "IDUsr", "SP_User", "@IDUsr", "1");  oAsig.LCBXMenu = oCBX.FNLLenaCombo("Menu", "IDMenu", "SP_Menu", "@IDMenu", "1");
            oCBX = null;
            oAsig.IEnumAsig = oUtlAsign.LlenaTabla("0");
            oUtlAsign = null;
            return View(oAsig);
        }
        [HttpPost]
        public ActionResult ConsAsig(FormCollection oForm)
        {
            string WIDUsr = oForm["HDcbxUsr"].ToString();
            UtilAsign oUtlAsign = new UtilAsign();            
            Asign oAsig = FNAsignar(WIDUsr);
            return View("Index",oAsig);
        }

        [HttpPost]
        public ActionResult Guardar(FormCollection oForm)
        {
            string WIDUsr = oForm["WIDUsr"].ToString(); string WIDMenu = oForm["WIDMenu"].ToString();
            UtilAsign oUtlAsig = new UtilAsign();
            oUtlAsig.Guardar(WIDUsr, WIDMenu);
            Asign oAsig = FNAsignar(WIDUsr);
            return View("Index", oAsig);
        }
        public ActionResult Borrar(string IDDelUsr, string IDMenu)
        {
            UtilAsign oUtlAsig = new UtilAsign();
            oUtlAsig.Borrar(IDDelUsr, IDMenu);
            Asign oAsig = FNAsignar(IDDelUsr);
            return View("Index",oAsig);
        }
        public Asign FNAsignar(string WIDUsr) {            
            Asign oAsig = new Asign();
            UtilAsign oUtlAsig = new UtilAsign();
            Combos oCBX = new Combos();
            oAsig.LCBXUser = oCBX.FNLLenaCombo("NomCom", "IDUsr", "SP_User", "@IDUsr", "1"); oAsig.LCBXMenu = oCBX.FNLLenaCombo("Menu", "IDMenu", "SP_Menu_Falta", "@IDUsr", WIDUsr);
            oCBX = null;
            oAsig.WIDUsr = WIDUsr;
            oAsig.IEnumAsig = oUtlAsig.LlenaTabla(WIDUsr);
            oUtlAsig = null;
            return oAsig;
        }
    }
}