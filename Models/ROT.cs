using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GASTOSMVC.Models
{
    public class ROT
    {
        public IEnumerable<ROT> IEnumMenu { get; set; }
        public string Ruta { get; set; }
        public string Img { get; set; }
        public string WGIDUsr = ""; public string WNomCom = ""; public string WSP = "";
        public string FNUser()
        {
            DataLayer OT = new DataLayer();  string WIDUser = WGIDUsr;
            var oNomCom =  OT.SPRdrSPX("SP_UsersID", 0, "@ID", WIDUser);
            if (oNomCom.Read()) WNomCom = oNomCom["Nombre"].ToString() + " " + oNomCom["Paterno"].ToString() + " " + oNomCom["Materno"].ToString();
            oNomCom.Close(); oNomCom.Dispose(); oNomCom = null; OT = null;
            return WNomCom != "" ? WNomCom : "";
        }

        public List<ROT> LlenaTabla(string WUser)
        {
            List<ROT> oMenu = new List<ROT>();
            DataLayer DL = new DataLayer();
            var oTT = DL.LLenaTabla("SP_MenuXUserID", "@IDUsr", WUser);
            for (int K = 0; K < oTT.Tables[0].Rows.Count; K++)
            {
                oMenu.Add(new ROT
                {
                    Ruta = oTT.Tables[0].Rows[K].ItemArray[1].ToString(),
                    Img = oTT.Tables[0].Rows[K].ItemArray[2].ToString(),
                });
            }
            oTT.Dispose(); oTT = null; DL.Dispose(); DL = null;
            return oMenu;
        }


    }
}