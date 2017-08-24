<%@Page Language="C#" AutoEventWireup="true" CodeBehind="frmMitakaDetailCreater.aspx.cs" Inherits="OldTigerWeb.frmMitakaDetailCreater"%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="Scripts/media/css/jquery.dataTables.css" />
    <link rel="stylesheet" href= "Content/themes/base/jquery-ui.css"/>
    <link rel="stylesheet" href="Content/OldTiger.css" />

    <title>過去トラ観たか詳細</title>
</head>
<body class="SubWindowStyle">
    <form id="FormMitakaDetail" runat="server">
        <%--タイトル--%>
        <table class="SubWindowTitle">
            <tr>
                <td>
                    <div>
                        過去トラ観たか詳細
                    </div>
                </td>
                <td>
                    <input type="button" value="閉じる" class="buttoncolor btnSmall" onclick="window.close();" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <%--過去トラ観たかヘッダー--%>
                    <table class="MitakaDetailTable">
                        <tr>
                            <th class="SubWindowTableHeader">タイトル</th>
                            <td colspan="9">
                                <asp:Label ID="LblTitle" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <th class="SubWindowTableHeader">
                                作成者
                            </th>
                            <td>
                                <asp:Label ID="LblCreateUser" runat="server" Text="" />
                            </td>
                            <th class="SubWindowTableHeader">
                                観たか管理番号
                            </th>
                            <td>
                                <asp:Label ID="LblManageNo" runat="server" Text="" />
                            </td>
                            <th class="SubWindowTableHeader">
                                状況
                            </th>
                            <td>
                                <asp:Label ID="LblStatus" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <th class="SubWindowTableHeader">
                                展開先
                            </th>
                            <td colspan="9">
                                <asp:Label ID="DevelopmentDepartment" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <th class="SubWindowTableHeader">
                                BLK No
                            </th>
                            <td colspan="9">
                                <asp:Label ID="LblBlckNo" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <th class="SubWindowTableHeader">
                                設通番号
                            </th>
                            <td colspan="9">
                                <asp:Label ID="LblEcsNo" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <th class="SubWindowTableHeader">
                                タイトル品番
                            </th>
                            <td colspan="9">
                                <asp:Label ID="LblTitleDrawingNo" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <th class="SubWindowTableHeader">
                                コメント
                            </th>
                            <td colspan="9">
                                <asp:Label ID="LblComment" runat="server" Text="" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    ≪抽出条件≫
                    <div class="MitakaDetailSearchDiv tableDiv">
                        <table id="SearchParameterTable" class="tratbl">
                            <thead>
                                <tr>
                                    <th>
                                        検索区分
                                    </th>
                                    <th>
                                        検索キーワード
                                    </th>
                                    <th>
                                        検索カテゴリ
                                    </th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    ≪回答リスト≫
                    <div class="MitakaDetailTroubleListDiv tableDiv">
                        <%--過去トラ観たかリスト--%>
                        <table id="TroubleTable" class="tratbl">
                            <thead>
                                <tr>
                                    <th rowspan="2">No.</th>
                                    <th colspan="8">不具合項目</th>
                                    <th colspan="2">再発防止</th>
                                    <th rowspan="2">部署</th>
                                    <th rowspan="2">資料No一覧</th>
                                    <th rowspan="2">重要度<br/>ランク</th>
                                    <th rowspan="2">再発<br/>案件</th>
                                    <th rowspan="2">主査<br/>部署</th>
                                    <th rowspan="2">確認結果</th>
                                </tr>
                                <tr>
                                    <th>進捗</th>
                                    <th>項目管理No</th>
                                    <th>項目</th>
                                    <th>FMC</th>
                                    <th>現象</th>
                                    <th>状況</th>
                                    <th>原因</th>
                                    <th>対策</th>
                                    <th>開発時の<br />流出要因</th>
                                    <th>確認の<br />視点</th>
                                </tr>
                            </thead>
                            <%--<tbody></tbody>--%>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="button" value="観たか編集" class="buttoncolor btnSmallLeft" onclick="jumpFrmEdit();" />
                    <input type="button" value="追加確認" class="buttoncolor btnSmallLeft" onclick="jumpFrmDevelopmentTargetAdd();" />
                    <input type="button" value="回答確認" class="buttoncolor btnSmall" onclick="openDialogMitakaConfirmation();" />
                    <input type="button" value="点検完了" class="buttoncolor btnSmall" onclick="CompleteMitaka()" />
                    <input type="button" value="点検依頼" class="buttoncolor btnSmall" onclick="CreateSendMail()" />
                    <%--<input type="button" value="PDFﾀﾞｳﾝﾛｰﾄﾞ" class="buttoncolor btnSubmit" onclick="window.close();" />--%>
                    <asp:Button ID="btnDownloadPdf" runat="server" Text="リスト印刷" CssClass="buttoncolor btnSmall" 
                        OnClick="btnDownloadPdf_Click"/>
                </td>
            </tr>
        </table>
        <table class="SubWindowFooter">
            <tr>
                <td>
                </td>
            </tr>
        </table>

    </form>


</body>
<script src="Scripts/jquery-1.8.2.js"></script>
<script src="Scripts/jquery-ui-1.8.24.js"></script>
<script src="Scripts/media/js/jquery.dataTables.min.js"></script>
<script type="text/javascript">

    var searchPrmTable;
    var troubleTable;

    $(document).ready(function () {
        CreateDataTables("SearchParameterTable");
        CreateDataTables("TroubleTable");
    })

    function CreateDataTables(id) {
        var table;
        var zeroRecord;
        var size;

        if (id == "SearchParameterTable") {
            zeroRecord = " 検索条件が存在しません。"
            size = 50;
        }
        else {
            zeroRecord = " 回答対象の過去トラ情報が存在しません。"
            size = 230;
        }

        table = $('#' + id).DataTable(
        {
            "searching": false,     //フィルタを無効
            "info": false,         //総件数表示を無効
            "paging": false,       //ページングを無効
            "ordering": false,     //ソートを無効
            "bProcessing": false,
            "lengthChange": false,
            "bAutoWidth": false,
            "scrollY": size,
            "scrollX": null,
            "iDisplayLength": 6,
            "oLanguage": {
                "sProcessing": "処理中...",
                "sLengthMenu": "_MENU_ 件表示",
                "sZeroRecords": zeroRecord,
                "sInfo": "_START_件～_END_件を表示（全_TOTAL_ 件中）",
                "sInfoEmpty": " 0 件中 0 から 0 まで表示",
                "sInfoFiltered": "（全 _MAX_ 件より抽出）",
                "sInfoPostFix": "",
                "sSearch": "全体検索：　",
                "sUrl": "",
                "oPaginate": {
                    "sFirst": "先頭",
                    "sPrevious": "前ページ",
                    "sNext": "次ページ",
                    "sLast": "最終"
                }
            }
        });

        if (id == "SearchParameterTable") {
            searchPrmTable = table;
        }
        else {
            troubleTable = table;
        }
    }


    function jumpFrmEdit() {
        // 1. 画面のオープン
        if (confirm("過去トラ観たか編集を行います。") == true)
        {
            var url = "frmMitakaRegist.aspx?ManageNo=Test";
            location.href = url;
        }
    }

    function jumpFrmDevelopmentTargetAdd() {
        // 1. 画面のオープン
        if (confirm("過去トラ追加を行います。") == true) {
            var url = "frmDeploymentTargetAdd.aspx?ManageNo=Test";
            location.href = url;
        }
    }

    function CreateSendMail(){
        location.href = 'mailto:' + 'k-kato@cec-ltd.co.jp' + '?cc=' + 'k-kato@cec-ltd.co.jp' + '&subject=' + '【件名】テスト' + '&body=' + '【本文】テスト１' + '%0D%0A' + "【本文】テスト２";
    }

    function openDialogMitakaConfirmation() {
        var timestamp = new Date().getTime();

        // 1. 画面のオープン
        var url = "frmMitakaConfirmation.aspx?" + timestamp;

        var w = "1340px";
        var h = "700px";
        var features = "dialogWidth=" + w + ";dialogHeight=\
                " + h + ";center=1;status=1;scroll=1;resizable=1;\
                minimize=0;maximize=0;";

        var returnValue = showModalDialog(url, window, features);

        // 帰り値が存在する場合（回答が行われた場合）
        if (returnValue != undefined) {

        }
    }



    function CompleteMitaka() {
        // 1. 画面のオープン
        if (confirm("過去トラ観たかをクローズします。") == true) {
            var url = "frmMitakaDetailAnswer.aspx?ManageNo=Test";
            location.href = url;
            //window.close();
        }
    }
</script>
</html>
