using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GASTOSMVC.Models
{
    public class UtilTransGastos
    {
        public string aNDia = "01|02|03|04|05|06|07|08|09|10|11|12|13|14|15|16|17|18|19|20|21|22|23|24|25|26|27|28|29|30|31";
        public string aDia = "01|02|03|04|05|06|07|08|09|10|11|12|13|14|15|16|17|18|19|20|21|22|23|24|25|26|27|28|29|30|31";
        public string aNMes = "01|02|03|04|05|06|07|08|09|10|11|12";
        public string aMes = "Enero|Febrero|Marzo|Abril|Mayo|Junio|Julio|Agosto|Septiembre|Octubre|Noviembre|Diciembre";
        public List<TransGastos> LlenaTabla(string WUser, string WFecIni, string WFecFin, string nGasto)
        {
            List<TransGastos> oAlumno = new List<TransGastos>();
            DataLayer DL = new DataLayer();
            var oTT = DL.LLenaTabla("SP_TransGastos", "@IDUsr|@FecIni|@FecFin|@nTipoGasto", WUser, WFecIni, WFecFin, nGasto);
            for (int K = 0; K < oTT.Tables[0].Rows.Count; K++)
            {
                oAlumno.Add(new TransGastos { IDGas = int.Parse(oTT.Tables[0].Rows[K].ItemArray[0].ToString()), Fecha = DateTime.Parse(oTT.Tables[0].Rows[K].ItemArray[1].ToString()),
                    Gasto = decimal.Parse(oTT.Tables[0].Rows[K].ItemArray[3].ToString()), Gastos = oTT.Tables[0].Rows[K].ItemArray[2].ToString() });
            }
            oTT.Dispose(); oTT = null; DL.Dispose(); DL = null;
            return oAlumno;
        }
        public string oYear() {
            string WYear = "";
            for (int K = 2015; K <= DateTime.Now.Year; K++) WYear += K.ToString() + "|";
            return WYear.Substring(0,WYear.Length-1);
        }
        public void SPDelTransac(string nIDelGas) {
            DataLayer DL = new DataLayer();
            var oDL = DL.SPRdrSPX("spDeleteGasto", 1, "@ID", nIDelGas);
            DL.Dispose(); DL = null;
        }
        public int NumSemana()
        {
            CultureInfo cul = CultureInfo.CurrentCulture;
            int PP = cul.Calendar.GetWeekOfYear(DateTime.Now.Date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            int nNumSem = cul.Calendar.GetWeekOfYear(DateTime.Now.Date, CalendarWeekRule.FirstDay, DayOfWeek.Monday); cul = null;
            return nNumSem;
        }
        public string FNTotal(string WIDUsr, string WFec1, string WFec2, string nTipoGasto)
        {
            string WTotal = ""; DataLayer DL = new DataLayer();
            var oTal =  DL.SPRdrSPX("SP_TransacTOTAL",0, "@IDUsr|@FecIni|@FecFin|@GastoEsp", WIDUsr, WFec1, WFec2, nTipoGasto);
            if (oTal.Read()) WTotal = oTal["Gasto"].ToString();
            oTal.Close(); oTal.Dispose(); oTal = null; DL.Dispose(); DL = null;
            return WTotal != "" ? WTotal : "0";
        }

    }
}