using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GASTOSMVC.Models;

namespace GASTOSMVC.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            string WUser = Request.Cookies["IDUsr"].Value; string WGas = "0";
            string WDia1 = "00" + DateTime.Now.Day.ToString(); string nMes1 = "00" + DateTime.Now.Month.ToString();            
            string XWDia1 = WDia1.Substring(WDia1.Length - 2, 2); string XWMes1 = nMes1.Substring(nMes1.Length - 2, 2); string nYear = DateTime.Now.Year.ToString(); 
            var oLLenaComboFec = SPLLenaCombosFecha(WUser, WGas, XWDia1, XWMes1, nYear, XWDia1, XWMes1, nYear);
            return View(oLLenaComboFec);
        }
        public ActionResult Actualiza(FormCollection oForm)
        {           
            string WHFEsActualiza = oForm["HFEsActualiza"].ToString();
            string WUser = Request.Cookies["IDUsr"].Value; string WGas = oForm["WCBXGas"].ToString();
            string nDia1 = oForm["WDia1"].ToString(); string nMes1 = oForm["WCBXMes1"].ToString(); string nYear1 = oForm["WCBXYear1"].ToString();
            string nDia2 = oForm["WDia2"].ToString(); string nMes2 = oForm["WCBXMes2"].ToString(); string nYear2 = oForm["WCBXYear2"].ToString();            
            return View("Index", SPLLenaCombosFecha(WUser, WGas, nDia1, nMes1, nYear1, nDia2, nMes2, nYear2));
        }
        private object SPLLenaCombosFecha(string WUser, string WGas, string nDia1, string nMes1, string nYear1, string nDia2, string nMes2, string nYear2) {
            TransGastos oTransGastos = new TransGastos();
            UtilTransGastos utilTransGastos = new UtilTransGastos();
            string WYear = utilTransGastos.oYear();
            oTransGastos.WSemana = utilTransGastos.NumSemana().ToString();
            Combos oCBX = new Combos();
            oTransGastos.LCBXGas = oCBX.FNLLenaCB("0|1|-1", "Normal|Especial|Todos");
            oTransGastos.LCBXDia1 = oCBX.FNLLenaCB(utilTransGastos.aNDia, utilTransGastos.aDia); oTransGastos.LCBXMes1 = oCBX.FNLLenaCB(utilTransGastos.aNMes, utilTransGastos.aMes); oTransGastos.LCBXYear1 = oCBX.FNLLenaCB(WYear, WYear);
            oTransGastos.LCBXDia2 = oCBX.FNLLenaCB(utilTransGastos.aNDia, utilTransGastos.aDia); oTransGastos.LCBXMes2 = oCBX.FNLLenaCB(utilTransGastos.aNMes, utilTransGastos.aMes); oTransGastos.LCBXYear2 = oCBX.FNLLenaCB(WYear, WYear);
            oCBX = null;
            oTransGastos.WCBXGas = WGas;
            oTransGastos.WDia1 = nDia1; oTransGastos.WCBXMes1 = nMes1; oTransGastos.WCBXYear1 = nYear1;
            oTransGastos.WDia2 = nDia2; oTransGastos.WCBXMes2 = nMes2; oTransGastos.WCBXYear2 = nYear2;
            UtilFecha oFecha = new UtilFecha();
            string WFecIni = oFecha.FNDevFecha(nDia1, nMes1, nYear1);  string WFecFin = oFecha.FNDevFecha(nDia2, nMes2, nYear2);
            oTransGastos.WTotal = utilTransGastos.FNTotal(WUser, WFecIni, WFecFin, "0");
            oFecha = null;
            oTransGastos.IEnumTransGastos = utilTransGastos.LlenaTabla(WUser, WFecIni, WFecFin, WGas);
            return oTransGastos;
        }
        public ActionResult Borrar(string IDDelGas)
        {
            UtilTransGastos oUtilTransGastos = new UtilTransGastos();
            oUtilTransGastos.SPDelTransac(IDDelGas);
            oUtilTransGastos = null;
            return RedirectToAction("Index", "Main");
        }
    }
}