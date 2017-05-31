<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmChangeImage.aspx.cs" Inherits="OldTigerWeb.frmChangeImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>背景変更画面リスト</title>
    
    <style type="text/css">
    h1 { /* h1～h6を指定 http://www.aoiweb.com/aoi2/title_bar4.htm */
        /*background: url('Images/bar51.gif') no-repeat left top; /* 画像を左上にひとつだけ配置 */*/
        font-size: 24px; /* 文字の大きさ */
        width: 600px; /* 幅 */
        height: 40px; /* 高さ */
        padding: 3px 0px 0px 40px; /* ボックスの内側[上 右 下 左]の余白 */
        margin: 10px 0px 5px 0px; /* ボックスの外側[上 右 下 左]の余白 */
        /*color: #0073a8;*/ /* 文字の色 */
        color: #000000; /* 文字の色を黒に変更 2017.03.29 神田 */
    }

    .TableStyle {
        width:100%;
    	font-size: 14px;
	    font-family: "ＭＳ ゴシック", "monospace";
    }

    .divCancelBtn {
        text-align: right;
        padding:1em;
    }

    .buttoncolor:hover { 
        /* 背景色を水色に指定 */ 
        background-color: #ccffff;
        border:1px #6699ff solid;
    }

    .buttoncolor:focus { 
        /* 背景色をリンク色に指定 */ 
        background-color: #6699ff;
        border:1px #6699ff solid;
    }
 
    </style>

    <link rel="stylesheet" href="Scripts/media/css/jquery.dataTables.css" />
    <link rel="stylesheet" href= "Content/themes/base/jquery-ui.css"/>
    <link rel="stylesheet" href="Content/OldTiger.css" />

    <%--外部JS読込み--%>
    <script type="text/javascript" src="Scripts/JSCOMMON.JS"></script>
    <script src="Scripts/jquery-1.8.2.js"></script>
    <script src="Scripts/jquery-ui-1.8.24.js"></script>
    <base target="_self" />
    </head>
    <body>
    <%--<h1 class="auto-style7" style ="width:100%" >過去トラシステム</h1>--%>
    <%--<h1 class="midasi" style ="width:100%" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ＴＯＰページ背景指示画面</h1>--%>
    <form id="frmChangeImage"  runat="server">
    <div>
        <table class ="TableStyle">
            <tbody>
                <tr>
                    <td colspan ="3" class="midasi" >ＴＯＰページ背景指示画面</td>
                <tr/>
                <tr>
                    <td></td>
                    <td colspan ="2" style ="color:blue  "> 
                       &nbsp;※変更ファイルをクリックすることで、背景が変更されます
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="true" 
                            style="border-style: solid; width:100%; border-color: #C0C0C0;text-align: center" 
                            Rows="10" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged">
                        </asp:ListBox>
                    </td>
                    <td></td>
                </tr>
            </tbody>
        </table>
    </div>
        <%--<p>--%>
<%--        <asp:ListBox ID="ListBox1" runat="server" Width="550px" AutoPostBack="true" 
            style="border-style: solid; width:90%; border-color: #C0C0C0;text-align: center" 
            Rows="20" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged">
        </asp:ListBox>--%>
        <%--</p>--%>
<%--    </div>
    
        <div class="moji12">※変更ファイルをクリックすることで、背景が変更されます</div>
        </br>    
    <div>--%>
    <div class ="divCancelBtn">
         <asp:Button ID="btn_Cancel" runat="server" Text="キャンセル"  
             class="buttoncolor textcenter14" Height="25px" Width="100px" text-align="right" OnClick="btn_Cancel_Click" />
    </div>
     </form>
</body>
</html>
