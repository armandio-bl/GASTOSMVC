using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GASTOSMVC.Models;

namespace GASTOSMVC.Controllers
{
    public class TransaccionController : Controller
    {
        // GET: Transaccion
        public ActionResult Index()
        {
            Transaccion oTrans = new Transaccion();
            string WDia1 = "00" + DateTime.Now.Day.ToString(); string nMes1 = "00" + DateTime.Now.Month.ToString();
            string XWDia1 = WDia1.Substring(WDia1.Length - 2, 2); string XWMes1 = nMes1.Substring(nMes1.Length - 2, 2); string nYear = DateTime.Now.Year.ToString();
            var oTras = LLenaCombos(oTrans);
            oTrans.WCBXGas = "0";
            oTrans.WDia1 = XWDia1; oTrans.WCBXMes1 = XWMes1; oTrans.WCBXYear1 = nYear; 
            return View(oTrans);
        }
        public ActionResult Act(int nIDGas)
        {
            Transaccion oTrans = new Transaccion();
            UtilTransaccion oUtilTrans = new UtilTransaccion();
            oTrans.IDGas = nIDGas;
            var oTras = LLenaCombos(oTrans);
            var rTrans = oUtilTrans.FNTransaccion(nIDGas.ToString());

            oTrans.WCBXGas = rTrans.WCBXGas;
            oTrans.WDia1 = rTrans.WDia1;
            oTrans.WCBXMes1 = rTrans.WCBXMes1; oTrans.WCBXYear1 = rTrans.WCBXYear1; oTrans.WCBXGastos = rTrans.WCBXGastos;
            oTrans.dGastos = rTrans.dGastos;

            return View("Index",oTrans);
        }
        [HttpPost]
        public ActionResult Guardar(FormCollection oForm) {
            string WIDGas = oForm["HDIDGas"].ToString();
            string WCBXGastos = oForm["WCBXGastos"].ToString();
            string WDia1 = oForm["WDia1"].ToString();
            string WCBXMes1 = oForm["WCBXMes1"].ToString();
            string WCBXYear1 = oForm["WCBXYear1"].ToString();
            string WGas = oForm["txtGas"].ToString();
            string WCBXGas = oForm["WCBXGas"].ToString();
            string WIDUsr = Request.Cookies["IDUsr"].Value;
            UtilTransaccion oGuardar = new UtilTransaccion();
            UtilFecha oFec = new UtilFecha();
            string WFecha = oFec.FNDevFecha(WDia1, WCBXMes1, WCBXYear1);
            oGuardar.SPGuardaTransaccion(WCBXGastos, WCBXGas, WFecha,WGas, WIDUsr, WIDGas);
            oGuardar = null;
            return RedirectToAction("Index", "Main");
        }

        private object LLenaCombos(Transaccion oTrans) {
            UtilTransGastos utilTransGastos = new UtilTransGastos();
            string WYear = utilTransGastos.oYear(); string WIDUsr = Request.Cookies["IDUsr"].Value;
            Combos oCBX = new Combos();
            oTrans.LCBXGas = oCBX.FNLLenaCB("0|1|-1", "Normal|Especial|Todos");
            oTrans.LCBXDia1 = oCBX.FNLLenaCB(utilTransGastos.aNDia, utilTransGastos.aDia); oTrans.LCBXMes1 = oCBX.FNLLenaCB(utilTransGastos.aNMes, utilTransGastos.aMes); oTrans.LCBXYear1 = oCBX.FNLLenaCB(WYear, WYear);
            oTrans.LCBXGastos = oCBX.FNLLenaCombo("Gastos", "IDGastos", "SP_CatGastos", "@IDUsr", WIDUsr);
            oCBX = null; utilTransGastos = null;
            return oTrans;
        }
    }
}