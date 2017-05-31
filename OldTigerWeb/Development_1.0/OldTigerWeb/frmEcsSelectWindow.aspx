<%@Page Language="C#" AutoEventWireup="true" CodeBehind="frmEcsSelectWindow.aspx.cs" Inherits="OldTigerWeb.frmEcsSelectWindow"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="Scripts/media/css/jquery.dataTables.css" />
    <link rel="stylesheet" href= "Content/themes/base/jquery-ui.css"/>
    <link rel="stylesheet" href="Content/OldTiger.css" />

    <title>図面改訂選択</title>
</head>
<body>
    <form id="FormEcsSelectWindow" runat="server">
        <div class="ecsDrawingSelectDivStyle">
            <p>表示対象の図面を選択してください</p>
            <table id ="EcsDrawingList" class="ecsDrawingSelectTableStyle tratbl" >
                <thead>
                    <tr>
                       <th class="ecsDrawingSelectTdDrawingNoStyle">図面番号</th>
                       <th class="ecsDrawingSelectTdDrawingRevNoStyle ">図面改訂番号</th>
                    </tr>
                </thead>
                <tbody>
                    <%if (EcsContainList != null && EcsContainList.Rows.Count > 0)%>
                    <%{%>
                        <%for (var i=0;i<EcsContainList.Rows.Count;i++) %>
                        <%{ %>
                        <tr>
                                <td class="ecsDrawingSelectTdDrawingNoStyle"><a href="#" onclick="SendDrawingData('<%=EcsContainList.Rows[i]["DrawingNo"] %>')"><%=EcsContainList.Rows[i]["DrawingNo"]%></a></td>
                                <td class="ecsDrawingSelectTdDrawingRevNoStyle "><%=EcsContainList.Rows[i]["DrawingRevNo"]%></td>
                        </tr>
                        <%} %>
                    <% }%>
                </tbody>
            </table>
        </div>
    </form>

    <script src="Scripts/jquery-1.8.2.js"></script>
    <script src="Scripts/jquery-ui-1.8.24.js"></script>
    <script src="Scripts/media/js/jquery.dataTables.min.js"></script>

    <script>
        // グローバル変数定義
        var oTable;

        // 画面読込時処理
        $(document).ready(function () {
            CreateDataTables('EcsDrawingList');
        });

        // DataTables作成(JQuery)
        function CreateDataTables(tableID) {
            oTable = $('#' + tableID).DataTable({
                //"searching": true,     //フィルタを有効
                "searching": false,     //フィルタを無効
                "lengthChange": false,
                "iDisplayLength": 8,
                "aaSorting":[],
                "sDom": '<"top">rt<"bottom"lip><"clear">',
                "sDom": '<"top">rt<"bottom"><"clear">',
                "bAutoWidth": false,
                "oLanguage": {
                    "sProcessing": "処理中...",
                    "sLengthMenu": "_MENU_ 件表示",
                    "sZeroRecords": "対象となる図面情報が取得できません。",
                    "sInfo": "_START_件～_END_件を表示（全_TOTAL_ 件中）",
                    "sInfoEmpty": " 0 件中 0 から 0 まで表示",
                    "sInfoFiltered": "（全 _MAX_ 件より抽出）",
                    "sInfoPostFix": "",
                    "sSearch": "画面絞込:",
                    "sUrl": "",
                    "oPaginate": {
                        "sFirst": "先頭",
                        "sPrevious": "前ページ",
                        "sNext": "次ページ",
                        "sLast": "最終"
                    }
                }
            });
        }

        // 親画面へのクエリ文字列送信、画面クローズ処理
        function SendDrawingData(drawingNo)
        {
            // 送信するクエリ文字列を作成
            var str1 = rightPad("<%= sUser %>", 10, " ")  // ユーザーID
            var str2 = rightPad(drawingNo, 13, " ")     // 図面番号
            var str3 = "<%= sIPAddress %>"              // IPアドレス

            returnValue = encodeURIComponent(str1) + encodeURIComponent(str2) + "1" + str3;
            close();
        }

        // 半角スペースで右埋め
        function rightPad(str, len, chr) {
            if (!chr || chr.length == 0) {
                return str;
            } else if (chr.length > 1) {
                chr = chr.charAt(0);
            }

            for (; str.length < len; str = str + chr);
            return str;
        }

    </script>
</body>
</html>
