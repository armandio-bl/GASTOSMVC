using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GASTOSMVC.Models
{
    public class AccValida
    {

        public int Validar(string WUsuario, string WContra)
        {
            int nEsPaso = 0;
            DataLayer OT = new DataLayer(); 
            var oUser = OT.SPRdrSPX("spGetUsuario", 0, "@Usuario|@Contra", OT.FNCdHx(WUsuario), OT.FNCdHx(WContra));
            if (oUser.Read()) nEsPaso = int.Parse(oUser["IDUsr"].ToString());
            oUser.Close(); oUser.Dispose(); oUser = null;
            OT.Dispose(); OT = null;
            return nEsPaso;
        }

    }
}