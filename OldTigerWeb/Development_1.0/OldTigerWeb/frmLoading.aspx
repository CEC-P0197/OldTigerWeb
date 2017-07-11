<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLoading.aspx.cs" Inherits="OldTigerWeb.frmLoading" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%--外部JS読込み--%>
<%--    <script type="text/javascript" src="Scripts/jscommon.js"></script>
    <script src="Scripts/jquery-1.8.2.js"></script>
    <script src="Scripts/jquery-ui-1.8.24.js"></script>--%>

    <title>Loading...</title>

    <%--<link rel="stylesheet" href="Content/OldTiger.css" />--%>
    <style type="text/css">
        .style { /* h1～h6を指定 http://www.aoiweb.com/aoi2/title_bar4.htm */
            /*background: url('Images/bar51.gif') no-repeat left top;*/ /* 画像を左上にひとつだけ配置 　削除　2017.03.29 神田*/
            /*font-size: 22px;*/ /* 文字の大きさ */
            /*height: 30px;*/ /* 高さ */
            /*height: 10px;*/ /* 高さ */
            /*padding: 3px 0px 0px 40px;*/ /* ボックスの内側[上 右 下 左]の余白 */
            /*padding: 0px 0px 0px 0px;*/ /* ボックスの内側[-上 右 下 左]の余白 　変更　2017.03．29 神田*/
            /*margin: 8px 0px 5px 0px;*/ /* ボックスの外側[上 右 下 左]の余白 */
            /*margin: 0px 0px 0px 0px;*/ /* ボックスの外
                側[上 右 下 左]の余白 */
            /*color: #0073a8;*/ /* 文字の色 */
            text-align:center;
            color: gray; /* 文字の色を黒⇒グレーに変更 2017.03.29 神田 */
        }
        .dispLoading {
            background-image: url("./Images/load.gif")!important;
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-position: center center;
        }
    </style>
</head>
<body onblur="focus()" style="height: 100vh; width: 100vw">
    <form id="Loading" runat="server">
        <div class="dispLoading style" style="height: 100vh; width: 100vw">少々お待ちください</div>
    </form>
</body>
</html>
