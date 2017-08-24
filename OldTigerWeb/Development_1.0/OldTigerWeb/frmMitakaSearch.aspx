<%@Page Language="C#" AutoEventWireup="true" CodeBehind="frmMitakaSearch.aspx.cs" Inherits="OldTigerWeb.frmMitakaSearch"%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="Scripts/media/css/jquery.dataTables.css" />
    <link rel="stylesheet" href= "Content/themes/base/jquery-ui.css"/>
    <link rel="stylesheet" href="Content/OldTiger.css" />

    <title>過去トラ観たか検索</title>
</head>
<body class="SubWindowStyle">
    <form id="FormMitakaSearch" runat="server">
        <%--タイトル--%>
        <table class="SubWindowTitle">
            <tr>
                <td>
                    <div>
                        過去トラ観たか検索
                    </div>
                </td>
                <td>
                    <input type="button" value="閉じる" class="buttoncolor btnSmall" onclick="window.close();" />
                </td>
            </tr>
        </table>
        <br/>
        <table>
            <tr>
                <td>
                    <%--過去トラ観たかヘッダー--%>
                    <table class="MitakaSearchTable">
                        <tr>
                            <td colspan="7">
                                【検索条件】
                            </td>
                        </tr>
                        <tr>
                            <td class="SubWindowTableHeader">
                                開発符号
                            </td>
                            <td>
                                <asp:TextBox ID="TxtDevelopCode" placeholder="開発符号" runat="server" />
                            </td>
                            <td class="SubWindowTableHeader">
                                機種
                            </td>
                            <td>
                                <asp:TextBox ID="TxtModel" placeholder="機種" runat="server" />
                            </td>
                            <td class="SubWindowTableHeader">
                                部署
                            </td>
                            <td>
                                <asp:TextBox ID="TxtDepartmentCode" placeholder="部署コード" runat="server" ReadOnly="true"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="SubWindowTableHeader">
                                BLK No
                            </td>
                            <td>
                                <asp:TextBox ID="TxtBlkNo" placeholder="BLK No." runat="server" />
                            </td>
                            <td class="SubWindowTableHeader">
                                タイトル品番
                            </td>
                            <td>
                                <asp:TextBox ID="TxtTitlePartNo" placeholder="タイトル品番" runat="server" />
                            </td>
                            <td class="SubWindowTableHeader">
                                設通番号
                            </td>
                            <td>
                                <asp:TextBox ID="TxtEcsNo" placeholder="設通番号" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="button" value="検索" class="buttoncolor btnSmallLeft" onclick="window.close();" />
                </td>
            </tr>
            <tr></tr>
            <tr>
                <td>
                    <div class="MitakaSearchInstListDiv">
                        <%--過去トラ観たかリスト--%>
                        <table class="tratbl">
                        <thead>
                            <tr>
                                <th>No.</th>
                                <th colspan="3">室</th>
                                <th colspan="6">タイトル</th>
                                <th colspan="4"></th>
                            </tr>
                        </thead>
                    </table>
                    </div>
                </td>
            </tr>
            <tr>
            </tr>
        </table>
        <table class="MitakaSearchFooter">
            <tr>
                <td>
                    <input type="button" value="詳細表示" class="buttoncolor btnSmall" onclick="OpenSubWindowMitakaDetailAnswer();" />
                </td>
            </tr>
        </table>

    </form>


</body>
<script src="Scripts/jquery-1.8.2.js"></script>
<script src="Scripts/jquery-ui-1.8.24.js"></script>
<script>

    function checkSelectedRow() {
        //alert("表示対象を選択してください。");
    }
    function OpenSubWindowMitakaDetailAnswer() {
        // 1. 画面のオープン
        var url = "frmMitakaDetailAnswer.aspx";
        var w = (screen.width - 1340) / 2;
        var h = (screen.height - 700) / 2;
        var features = "menubar=no,toolbar=no,location=no,resizable=no,scrollbars=yes,status=no,height=700,width=1340,left=" + w + ",top=" + h;
        var frmUserSearchFlg = window.open(url, "FormfrmUserSearch", features);
    }

</script>
</html>
