using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GASTOSMVC.Models
{
    public class UtilAsign
    {
        public List<Asign> LlenaTabla(string WUser)
        {
            List<Asign> oAsign = new List<Asign>();
            DataLayer DL = new DataLayer();
            var oTT = DL.LLenaTabla("SPLLenaAsign", "@IDUsr", WUser);
            for (int K = 0; K < oTT.Tables[0].Rows.Count; K++)
            {
                oAsign.Add(new Asign
                {
                    WIDMenu = oTT.Tables[0].Rows[K].ItemArray[0].ToString(),
                    Menu = oTT.Tables[0].Rows[K].ItemArray[1].ToString(),
                    WIDUsr = oTT.Tables[0].Rows[K].ItemArray[2].ToString(),
                });
            }
            oTT.Dispose(); oTT = null; DL.Dispose(); DL = null;
            return oAsign;
        }
        public void Guardar(string IDUsr, string IDMenu) 
        {
            DataLayer DL = new DataLayer();
            var oLAsig = DL.SPRdrSPX("SPAccesoINS", 1, "@IDUsr|@IDMenu", IDUsr, IDMenu);
            DL.Dispose(); DL = null;
        }
        public void Borrar(string IDUsr, string IDMenu)
        {
            DataLayer DL = new DataLayer();
            var oLAsig = DL.SPRdrSPX("SPAccesoDEL", 1, "@IDUsr|@IDMenu", IDUsr, IDMenu);
            DL.Dispose(); DL = null;
        }
    }
}