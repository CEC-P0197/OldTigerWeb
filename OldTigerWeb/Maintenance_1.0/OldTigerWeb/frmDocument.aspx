<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmDocument.aspx.cs" Inherits="OldTigerWeb.frmDocument" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <%--外部JS読込み--%>
    <script type="text/javascript" src="Scripts/jscommon.js"></script>
    <script src="Scripts/jquery-1.8.2.js"></script>
    <script src="Scripts/jquery-ui-1.8.24.js"></script>

    <title> <%= title.Replace(".pdf","")%></title>

    <link rel="stylesheet" href= "Content/OldTiger.css" />

   <style type="text/css">
        @media print{
            .pdf {display:none !important;}
            .kinshi {display:block !important}
        }
    </style>

</head>
<body style="height: 100%;">
    <form id="Document" runat="server">
    <div style="width:100vh">
        <div　class="kinshi" style="display:none">
        </div>
        <div class="pdf">
            <object
            classid="clsid:F01E06A7-2439-44D9-9759-F2A73A0DD34A"
            codebase="http://<%= ConfigurationManager.AppSettings["Brava"] %>/BravaSDK/ActiveX/viewer/client/BravaClientXWrapper.cab#version=8,3,2,93"
            codebaseURL="http://<%= ConfigurationManager.AppSettings["Brava"] %>/BravaSDK/ActiveX/viewer/client/BravaClientXWrapper.cab#version=8,3,2,93"
            style="width: 100%; height: 100vh;">        
            <param name="BravaXParams" value="
                IntegrationURL=http://<%= ConfigurationManager.AppSettings["Brava"] %>/BravaSDK/ActiveX/viewer/client/generic.bin&#10;
                IntegrationVersion=8.3.2.93&#10;
                ServerProperties=methodurl,http://<%= ConfigurationManager.AppSettings["Brava"] %>/BravaServer/Properties&#10;
                EnablePrinting=False&#10;
                DisableClipboardImages=True&#10;
                EnableSaveView=False&#10;
                EnableMarkup=False&#10; マークアップの無効化
                EnableMeasurement=False&#10; 計測の無効化
                EnableCopyText=False&#10;
                EnableRightMouseButtonMenu=False&#10;
                SessionId=<%=sessionId%>&#10;
                DocID=<%=filePath%>&#10;">
            </object>
        </div>
    </div>
        <input type="hidden" name ="folderPath" value="" />
        <input type="hidden" name ="file" value="" />
    </form>
</body>
</html>
