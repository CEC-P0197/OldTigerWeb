<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmErrorPage.aspx.cs" Inherits="OldTigerWeb.frmErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>過去トラ管理：エラーページ</title>
    <link rel="stylesheet" href= "Content/OldTiger.css" />

</head>
<body>
    <form id="frmErrPage" runat="server">
    <div align="center">
        <div>
            <h1>＜＜＜　　過去トラ管理：エラーページ　　＞＞＞</h1>
        </div>
        <br />
        <br />
        <div align="center">
            <label id="Label1">●===============================　　　予期せぬエラーが発生しました　　　===============================●</label>
        </div>
        <br />
        <br />
        <br />

        <div align="center">
            <label id="Label2">■　画面ＩＤ：</label>
            <asp:Label id="lblFormId" runat="server"></asp:Label>
        </div>
        <br />
        <br />
        <div align="center">
            <label id="Label3">■　イベント：</label>
            <asp:Label id="lblEvent" runat="server"></asp:Label>
        </div>
        <br />
        <br />
        <div align="center">
            <label id="Label4">■　エラーメッセージ：</label>
            <asp:Label id="lblEx" runat="server"></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
