<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VISOR.aspx.cs" Inherits="GASTOSMVC.VISOR.VISOR" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
		<div>     
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="411px" Width="816px"></rsweb:ReportViewer>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
		</div>
    </form>
</body>
</html>
