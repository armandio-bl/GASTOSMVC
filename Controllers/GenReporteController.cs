using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GASTOSMVC.Models;


namespace GASTOSMVC.Controllers
{
    public class GenReporteController : Controller
    {
        // GET: GenReporte
        public ActionResult Index(FormCollection oForm)
        {
            string WUser = Request.Cookies["IDUsr"].Value; string WGas = oForm["WCbxGas"].ToString();            
            string nDia1 = oForm["ncbxDia1"].ToString(); string nMes1 = oForm["ncbxMes1"].ToString(); string nYear1 = oForm["ncbxYear1"].ToString();
            string nDia2 = oForm["ncbxDia2"].ToString(); string nMes2 = oForm["ncbxMes2"].ToString(); string nYear2 = oForm["ncbxYear2"].ToString();

            UtilFecha oFecha = new UtilFecha();
            string WFecIni = oFecha.FNDevFecha(nDia1, nMes1, nYear1); string WFecFin = oFecha.FNDevFecha(nDia2, nMes2, nYear2);
            oFecha = null;
            ROT oRuta = new ROT();
            UtlGenReporte oGenRep = new UtlGenReporte();
            oRuta.Ruta = oGenRep.FNGenReporte(WUser, WFecIni, WFecFin, WGas);
            return View(oRuta);
        }
    }
}