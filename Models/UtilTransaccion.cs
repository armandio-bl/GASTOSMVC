using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GASTOSMVC.Models
{
    public class UtilTransaccion
    {
        public Transaccion FNTransaccion(string IDGas) {
            Transaccion oTrans = new Transaccion();
            DataLayer DL = new DataLayer();
            var rTran = DL.SPRdrSPX("SP_TransacID", 0, "@ID", IDGas);
            if (rTran.Read())
            {
                DateTime sFecha = DateTime.Parse(rTran["Fecha"].ToString()); string WFecDia = "00" + sFecha.Day.ToString(); string WFecMes = "00" + sFecha.Month.ToString();
                oTrans.WCBXGastos = rTran["IDGastos"].ToString();
                oTrans.WDia1 = WFecDia.Substring(WFecDia.Length - 2, 2);
                oTrans.WCBXMes1 = WFecMes.Substring(WFecMes.Length - 2, 2);
                oTrans.WCBXYear1 = sFecha.Year.ToString();
                oTrans.WCBXGas = rTran["GastoEsp"].ToString();
                oTrans.dGastos = decimal.Parse(rTran["Gasto"].ToString());
            }
            rTran.Close(); rTran.Dispose(); rTran = null;
            DL.Dispose(); DL = null;
            return oTrans;
        }
        public void SPGuardaTransaccion(string WIDGastos, string WCBXGastos, string WFecha, string WGas, string WIDUsr, string nID)
        {
            DataLayer DL = new DataLayer();
            if (nID == "0")
            {
                var PP = DL.SPRdrSPX("spInsertGasto", 1, "@Fecha|@nIDGasto|@WGas|@EsGasEsp|@IDUsr", WFecha, WIDGastos, WGas, WCBXGastos, WIDUsr);
            }
            else
            {
                var PP = DL.SPRdrSPX("spUpdateGasto", 1, "@Fecha|@nIDGasto|@WGas|@EsGasEsp|@IDUsr|@ID", WFecha, WIDGastos, WGas, WCBXGastos, WIDUsr, nID);
            }
            DL.Dispose(); DL = null;
        }

    }
}