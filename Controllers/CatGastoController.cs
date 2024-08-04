using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GASTOSMVC.Models;

namespace GASTOSMVC.Controllers
{
    public class CatGastoController : Controller
    {
        // GET: CatGasto
        public ActionResult Index()
        {
            CatGastos oCatGasto = new CatGastos();
            Combos oCBX = new Combos();
            string WIDUsr = Request.Cookies["IDUsr"].Value;
            oCatGasto.LCBXGastos = oCBX.FNLLenaCombo("Gastos", "IDGastos", "SP_CatGastos", "@IDUsr", WIDUsr);
            oCBX = null;
            return View(oCatGasto);
        }
        [HttpPost]
        public ActionResult ConsCatGas(FormCollection oForm)
        {
            string WIDGas = oForm["HDcbxGas"].ToString();
            CatGastos oGastos = new CatGastos();
            UtilCatGastos oUtlGas = new UtilCatGastos();
            string WIDUsr = Request.Cookies["IDUsr"].Value;
            var PP = oUtlGas.FNConsCatGastos(WIDGas, WIDUsr);
            oGastos.WCBXGastos = PP.WCBXGastos;
            oGastos.Gastos = PP.Gastos;
            Combos oCBX = new Combos();
            oGastos.LCBXGastos = oCBX.FNLLenaCombo("Gastos", "IDGastos", "SP_CatGastos", "@IDUsr", WIDUsr);
            oCBX = null;
            return View("Index",oGastos);
        }
        [HttpPost]
        public ActionResult Guardar(FormCollection oForm) {
            string WIDGas = oForm["HDcbxGas"].ToString();
            string WGas = oForm["txtGas"].ToString();
            string WCBXGastos = oForm["WCBXGastos"].ToString();
            string WIDUsr = Request.Cookies["IDUsr"].Value;
            UtilCatGastos oUtlGas = new UtilCatGastos();
            oUtlGas.SPGuardar(WIDGas,WGas, WIDUsr);
            oUtlGas = null;
            return RedirectToAction("Index", "CatGasto");
        }

        [HttpPost]
        public ActionResult Borrar(FormCollection oForm)
        {
            string WIDGas = oForm["HDcbxGas"].ToString(); string WIDUsr = Request.Cookies["IDUsr"].Value;
            UtilCatGastos oUtlGas = new UtilCatGastos();
            oUtlGas.SPBorrar(WIDGas, WIDUsr);
            oUtlGas = null;
            return RedirectToAction("Index", "CatGasto");
        }

    }
}