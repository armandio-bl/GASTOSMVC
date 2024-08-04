using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace GASTOSMVC.Models
{
    public class UtlGenReporte
    {
        public string FNGenReporte(string WIDUsr, string WFecIni, string WFecFin, string WTipoGasto ) {
            ProcBorrarPDF();
            DataLayer DL = new DataLayer();
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            LocalReport rv = viewer.LocalReport;
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1"; rds.Value = DL.LLenaTabla("SP_REP_GASTOS", "@IDUsr|@FecIni|@FecFin|@nTipoGasto", WIDUsr, WFecIni, WFecFin, WTipoGasto).Tables[0];
            string WRuta = System.Web.HttpContext.Current.Server.MapPath("REPORT") + "\\" + "Report1.rdlc";
            rv.DataSources.Clear(); rv.DataSources.Add(rds); rv.ReportPath = WRuta;
            string WIDC = DateTime.Now.Millisecond.ToString() + ".pdf";
            string WRutaPDF = System.Web.HttpContext.Current.Server.MapPath("PDFFile") + "\\REPALUM" + WIDC;
            Warning[] warnings; string[] streamIds; string mimeType = string.Empty; string encoding = string.Empty; string extension = string.Empty;
            string deviceInfo = @"<DeviceInfo><OutputFormat>PDF</OutputFormat><PageWidth>8.5in</PageWidth><PageHeight>11in</PageHeight><MarginTop>0.25in</MarginTop><MarginLeft>0.25in</MarginLeft><MarginRight>0.25in</MarginRight><MarginBottom>0.25in</MarginBottom><EmbedFonts>None</EmbedFonts></DeviceInfo>";
            byte[] Bytes = viewer.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamIds, out warnings);
            FileStream fs = new FileStream(WRutaPDF, FileMode.Create);
            fs.Write(Bytes, 0, Bytes.Length); fs.Close(); fs = null;
            MemoryStream ms = new MemoryStream(Bytes);
            BinaryWriter BW = new BinaryWriter(ms);
            BW.Flush(); BW.Close(); BW.Dispose(); BW = null;
            Array.Clear(Bytes, 0, Bytes.Length); Bytes = null;
            DL.Dispose(); DL = null;
            viewer.Dispose(); viewer = null;
            rv.Dispose(); rv = null; rds = null;
            return "PDFFile/REPALUM" + WIDC;
        }
        private void ProcBorrarPDF()
        {
            string WRuta = System.Web.HttpContext.Current.Server.MapPath("PDFFile"); string[] oFile = null; 
            oFile = Directory.GetFiles(WRuta);
            foreach (var WFiles in oFile)
            {
                var oFileInfo = new FileInfo(WFiles);
                if (oFileInfo.Extension == ".pdf" & oFileInfo.LastWriteTime < DateTime.Today) File.Delete(WRuta + "/" + oFileInfo.Name);
                oFileInfo = null;
            }
            Array.Clear(oFile, 0, oFile.Length); oFile = null;
        }
    }
}