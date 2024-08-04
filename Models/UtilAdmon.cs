using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GASTOSMVC.Models
{
    public class UtilAdmon
    {
        public Admon FNConsAdmon(string WIDUsr)
        {
            Admon oAdmon = new Admon();
            DataLayer DL = new DataLayer();
            var rAdmon= DL.SPRdrSPX("SP_UsersID", 0, "@ID", WIDUsr);
            if (rAdmon.Read())
            {
                oAdmon.IDUsr = int.Parse(rAdmon["IDUsr"].ToString());
                oAdmon.Nombre = rAdmon["Nombre"].ToString();
                oAdmon.Paterno = rAdmon["Paterno"].ToString();
                oAdmon.Materno = rAdmon["Materno"].ToString();
                oAdmon.Usuario = DL.FNDcHx(rAdmon["Usuario"].ToString());
                oAdmon.Password = DL.FNDcHx(rAdmon["Password"].ToString());
            }
            rAdmon.Close(); rAdmon.Dispose(); rAdmon = null;
            DL.Dispose(); DL = null;
            return oAdmon;
        }

        public void SPGuardar(string WNombre, string WPaterno, string WMaterno, string WUsuario, string WContra, string WIDUsr)
        {
            DataLayer DL = new DataLayer();
            if (WIDUsr == "0")
            {
                var PP = DL.SPRdrSPX("SP_UsersINS", 1, "@Nombre|@Paterno|@Materno|@Usuario|@Contra", WNombre, WPaterno, WMaterno, DL.FNCdHx(WUsuario), DL.FNCdHx(WContra));
            }
            else
            {
                var PP = DL.SPRdrSPX("SP_UsersUPD", 1, "@Nombre|@Paterno|@Materno|@Usuario|@Contra|@ID", WNombre, WPaterno, WMaterno, DL.FNCdHx(WUsuario), DL.FNCdHx(WContra), WIDUsr);
            }
            DL.Dispose(); DL = null;
        }
        public void SPBorrar(string WIDUsr)
        {
            DataLayer DL = new DataLayer();
            var PP = DL.SPRdrSPX("SP_UsersDELID", 1, "@ID", WIDUsr);
            DL.Dispose(); DL = null;
        }
    }
}