using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using GASTOSMVC.Models;

namespace GASTOSMVC.VISOR
{
    public partial class VISOR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataLayer DL = new DataLayer();            
            string WIDUsr = Request.Cookies["IDUsr"].Value;
            string ncbxGas = Request.QueryString["ncbxGas"];
            string ncbxDia1 = Request.QueryString["ncbxDia1"]; string cbxMes1 = Request.QueryString["cbxMes1"]; string ncbxYear1 = Request.QueryString["ncbxYear1"];
            string ncbxDia2 = Request.QueryString["ncbxDia2"]; string cbxMes2 = Request.QueryString["cbxMes2"]; string ncbxYear2 = Request.QueryString["ncbxYear2"];
            UtilFecha UFecha = new UtilFecha();
            string FIni = UFecha.FNDevFecha(ncbxDia1, cbxMes1, ncbxYear1); string FFin = UFecha.FNDevFecha(ncbxDia2, cbxMes2, ncbxYear2);
            UFecha = null;
            ReportViewer1.ProcessingMode = ProcessingMode.Local; ReportViewer1.AsyncRendering = false;
            LocalReport rv = ReportViewer1.LocalReport;
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1"; var SP = DL.LLenaTabla("SP_REP_GASTOS", "@IDUsr|@FecIni|@FecFin|@nTipoGasto", WIDUsr, FIni, FFin, ncbxGas).Tables[0]; rds.Value = SP;
            string WRuta = MapPath("../REPORT") + "\\" + "Report1.rdlc";
            rv.DataSources.Clear(); rv.DataSources.Add(rds); rv.ReportPath = WRuta; rv.Refresh();
            SP.Dispose(); DL.Dispose(); DL = null;
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            ReportViewer1.Dispose(); ReportViewer1 = null;
        }
    }
}