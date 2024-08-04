using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace GASTOSMVC.Models
{
    public class DataLayer
    {
        public string RUTAFILEDB = "/mvcgas/FILE-CN";
        bool disposed = false; public SqlConnection DB = null; public SqlCommand cmd = null; public SqlDataReader RD = null;
        public SqlCommand FNCMD(string SQL)
        {
            string WRuta = HttpContext.Current.Server.MapPath(RUTAFILEDB); string WDB = FNXML(WRuta, "Contra");
            DB = new SqlConnection(WDB); DB.Open(); cmd = DB.CreateCommand(); cmd.CommandText = SQL;
            return cmd;
        }
        public string FNXML(string WRuta, string WTag)
        {
            XmlDocument xDoc = new XmlDocument(); string WMain = WRuta + "\\BYLO.xml"; xDoc.Load(WMain); string WDat = "";
            XmlNodeList xPersonas = xDoc.GetElementsByTagName("Pass"); XmlNodeList xLista = ((XmlElement)xPersonas[0]).GetElementsByTagName(WTag);
            foreach (XmlElement nodo in xLista) WDat = nodo.InnerText;
            xLista = null; xPersonas = null; xDoc = null;
            return WDat;
        }
        public SqlDataReader SPRdrSPX(string WSP, int nData, string WPar, params string[] oVal)
        {
            char CLim = '|'; cmd = FNCMD(WSP); cmd.CommandType = CommandType.StoredProcedure; string[] oPars = WPar.Split(CLim);
            for (int K = 0; K <= oVal.Length - 1; K++) cmd.Parameters.AddWithValue(oPars[K], oVal[K]);
            Array.Clear(oPars, 0, oPars.Length); oPars = null; Array.Clear(oVal, 0, oVal.Length); oVal = null;
            if (nData == 1)
            {
                cmd.ExecuteNonQuery(); return null;
            }
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public SqlDataAdapter SPRdrSP(string WSP, string WPar, params string[] oVal)
        {
            char CLim = '|'; cmd = FNCMD(WSP); cmd.CommandType = CommandType.StoredProcedure; string[] oPars = WPar.Split(CLim);
            for (int K = 0; K <= oVal.Length - 1; K++) cmd.Parameters.AddWithValue(oPars[K], oVal[K]);
            Array.Clear(oPars, 0, oPars.Length); oPars = null; Array.Clear(oVal, 0, oVal.Length); oVal = null;
            var Adapter = new SqlDataAdapter(cmd);            
            return Adapter;
        }

        public DataSet LLenaTabla(string WSP, string WPar, params string[] oVal)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter oADPT = SPRdrSP(WSP, WPar, oVal);
            oADPT.Fill(ds); oADPT.Dispose(); oADPT = null;
            return ds;
        }
        private int Asc(string WLet)
        {
            if (WLet.Length == 0) return 0;
            int nAsc = System.Convert.ToChar(WLet);
            return nAsc;
        }
        private String Hex(string WHnd)
        {
            int nWord = Convert.ToInt32(WHnd); string WHex = String.Format("{0:X}", nWord);
            return WHex;
        }
        public string FNCdHx(string WHnd)
        {
            String Wrd = ""; String WCar = ""; int nKey = 31; char CPad = '0';
            for (int J = 0; J <= WHnd.Length - 1; J++)
            {
                WCar = WHnd.Substring(J, 1); int nAsc = Asc(WCar) * nKey; string WHex = Hex(nAsc.ToString());
                Wrd = Wrd + WHex.PadLeft(4, CPad);
            }
            return Wrd;
        }
        public string FNDcHx(string WHnd)
        {
            String Wrd = ""; String WCar = ""; int nKey = 31; int J = 0;
            while (J < WHnd.Length)
            {
                WCar = WHnd.Substring(J, 4); string XCar = WCar; int nHNum = Int32.Parse(XCar, System.Globalization.NumberStyles.HexNumber); int nChr = nHNum / nKey; Wrd = Wrd + Convert.ToChar(nChr);
                J = J + 4;
            }
            return Wrd;
        }
        public void Dispose()
        {
            Dispose(true); GC.SuppressFinalize(this); GC.Collect(); GC.WaitForPendingFinalizers();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)// Free any other managed objects here.
            {
            }// Free any unmanaged objects here.
            RUTAFILEDB = "";
            disposed = true;
            if (DB != null) { DB.Close(); DB.Dispose(); DB = null; }
            if (RD != null) { RD.Close(); RD.Dispose(); RD = null; }
            if (cmd != null) { cmd.Dispose(); cmd = null; }
        }
        ~DataLayer()
        {
            Dispose(false);
        }

    }
}