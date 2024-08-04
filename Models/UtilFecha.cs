using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GASTOSMVC.Models
{
    public class UtilFecha
    {
        public string FNDevFecha(string WDia, string WMes, string WYear)
        {
            DataLayer oRuta = new DataLayer();
            string WRuta = HttpContext.Current.Server.MapPath(oRuta.RUTAFILEDB);
            int nEsServer = int.Parse(oRuta.FNXML(WRuta, "EsSERVER"));
            oRuta.Dispose(); oRuta = null;
            string WFecha = "";
            if (nEsServer == 0)
            { //LOCAL
                WFecha = WYear + "-" +  WMes.Substring(WMes.Length - 2, 2) + "-" + WDia.Substring(WDia.Length - 2, 2);
            }
            else
            { //SERVER

                WFecha = WYear + "/" + WMes.Substring(WMes.Length - 2, 2) + "/" + WDia.Substring(WDia.Length - 2, 2);

            }
            return WFecha;
        }
    }
}