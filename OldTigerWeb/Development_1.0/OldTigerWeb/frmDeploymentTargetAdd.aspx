<%@Page Language="C#" AutoEventWireup="true" CodeBehind="frmDeploymentTargetAdd.aspx.cs" Inherits="OldTigerWeb.frmDeploymentTargetAdd"%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="Scripts/media/css/jquery.dataTables.css" />
    <link rel="stylesheet" href= "Content/themes/base/jquery-ui.css"/>
    <link rel="stylesheet" href="Content/OldTiger.css" />

    <title>展開対象設定</title>
</head>
<body class="SubWindowStyle">
    <form id="FormDeploymentTargetAdd" runat="server">
        <%--タイトル--%>
        <table class="SubWindowTitle">
            <tr>
                <td>
                    <div>
                        展開対象追加
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
                    【抽出条件】
                    <div class="DeploymentTargetAddDiv tableDiv">
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
                        </table>
                    </div>
                </td>
            </tr>
            <tr style="height:10vh">
                <td>
                    現在、展開対象外の過去トラ情報のみ表示しています。
                </td>
            </tr>
            <tr>
                <td>
                    <div class="DeploymentTargetAddTroubleListDiv tableDiv">
                        <%--過去トラ観たかリスト--%>
                        <table id="TroubleTable" class="tratbl">
                        <thead>
                            <tr>
                                <th rowspan="2">対象</th>
                                <th colspan="8">不具合項目</th>
                                <th colspan="2">再発防止</th>
                                <th rowspan="2">部署</th>
                                <th rowspan="2">資料No一覧</th>
                                <th rowspan="2">重要度<br/>ランク</th>
                                <th rowspan="2">再発<br/>案件</th>
                                <th rowspan="2">主査<br/>部署</th>
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
                        <input type="button" value="決定" class="buttoncolor btnSmall" onclick="OpenSubWindowMitakaDetailCreater();" />
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
            zeroRecord = " 追加対象となる過去トラ情報が存在しません。"
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

    function CloseDialog() {
        //returnValue = document.getElementById("hdnID").value + "|" + document.getElementById("hdnMail").value + "|" + document.getElementById("hdnName").value;
        close();
    }
    function OpenSubWindowMitakaDetailCreater() {

        var url = "frmMitakaDetailCreater";
        location.href = url;
    }
</script>
</html>
