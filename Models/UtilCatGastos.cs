using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GASTOSMVC.Models
{
    public class UtilCatGastos
    {
        public CatGastos FNConsCatGastos(string IDGas, string WIDUsr)
        {
            CatGastos oCatGasto = new CatGastos();
            DataLayer DL = new DataLayer();
            var rTran = DL.SPRdrSPX("SP_CatGastosID", 0, "@IDGastos|@IDUsr", IDGas, WIDUsr);
            if (rTran.Read())
            {
                oCatGasto.WCBXGastos = rTran["IDGastos"].ToString();
                oCatGasto.IDGas = int.Parse(rTran["IDGastos"].ToString());
                oCatGasto.Gastos = rTran["Gastos"].ToString();
            }
            rTran.Close(); rTran.Dispose(); rTran = null;
            DL.Dispose(); DL = null;
            return oCatGasto;
        }
        public void SPGuardar(string nID, string WGas, string WIDUsr) {
            DataLayer DL = new DataLayer();
            if (nID == "0")
            {
                var PP = DL.SPRdrSPX("spInsertCatGasto", 1, "@WGas|@IDUsr", WGas, WIDUsr);
            }
            else
            {
                var PP = DL.SPRdrSPX("spUpdateCatGasto", 1, "@WGas|@IDUsr|@ID", WGas, WIDUsr, nID);
            }
            DL.Dispose(); DL = null;
        }

        public void SPBorrar(string nID, string WIDUsr)
        {
            DataLayer DL = new DataLayer();          
            var PP = DL.SPRdrSPX("SP_CatGastoDELID", 1, "@IDUsr|@ID", WIDUsr, nID);          
            DL.Dispose(); DL = null;
        }

    }
}