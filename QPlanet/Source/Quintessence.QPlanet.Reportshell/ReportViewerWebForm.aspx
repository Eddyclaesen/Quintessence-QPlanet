<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewerWebForm.aspx.cs" Inherits="Quintessence.QPlanet.Reportshell.ReportViewerWebForm" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Style/Reset.css" rel="stylesheet" type="text/css"/>
    <link href="~/Style/Site.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%; height: 100%">
    <div>
        <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="StandardReportViewer" runat="server" Width="100%" Height="95%"></rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
