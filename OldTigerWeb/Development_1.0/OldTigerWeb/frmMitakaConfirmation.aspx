<%@Page Language="C#" AutoEventWireup="true" CodeBehind="frmMitakaConfirmation.aspx.cs" Inherits="OldTigerWeb.frmMitakaConfirmation"%>
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
                        過去トラ観たか回答一覧
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
                     <div class="MitakaConfirmationTroubleListDiv tableDiv">
                        <%--過去トラ観たかリスト--%>
                        <table id="TroubleTable" class="tratbl tableDiv">
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
                    </table>
                    </div>
               </td>
            </tr>
            <tr>
                <td>
                    <%--<input type="button" value="検索" class="buttoncolor btnSmallLeft" onclick="window.close();" />--%>
                </td>
            </tr>
            <tr></tr>
            <tr>
                <td>
                    <div class="MitakaConfirmationInstListDiv tableDiv">
                        <%--過去トラ観たかリスト--%>
                        <table id="AnswerTable" class="tratbl">
                        <thead>
                            <tr>
                                <th class="tdDepartment">部</th>
                                <th class="tdDivision">課</th>
                                <th class="tdUserName">担当者</th>
                                <th class="tdAnswer">回答内容</th>
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
                    <%--<input type="button" value="一覧へ" class="buttoncolor btnSmall" onclick="checkSelectedRow();" />--%>
                </td>
            </tr>
        </table>

    </form>


</body>
<script src="Scripts/jquery-1.8.2.js"></script>
<script src="Scripts/jquery-ui-1.8.24.js"></script>
<script src="Scripts/media/js/jquery.dataTables.min.js"></script>
<script type="text/javascript">

    var troubleTable;
    var answerTable;

    $(document).ready(function () {
        CreateDataTables("TroubleTable");
        CreateDataTables("AnswerTable");
    })

    function CreateDataTables(id) {
        var table;
        var zeroRecord;
        var size;

        if (id == "TroubleTable") {
            zeroRecord = " 対象となる過去トラ情報が存在しません。"
            size = 150;
        }
        else {
            zeroRecord = " 該当する回答情報が存在しません。"
            size = 330;
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

        if (id == "TroubleTable") {
            searchPrmTable = table;
        }
        else {
            troubleTable = table;
        }
    }


</script>
</html>
