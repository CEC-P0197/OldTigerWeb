<%@Page Language="C#" AutoEventWireup="true" CodeBehind="frmUserSearch.aspx.cs" Inherits="OldTigerWeb.frmUserSearch"%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href= "Content/themes/base/jquery-ui.css"/>
    <link rel="stylesheet" href="Scripts/media/css/jquery.dataTables.css" />
    <link rel="stylesheet" href="Content/OldTiger.css" />

    <title>ユーザー検索</title>
</head>
<%--<body onblur="focus()" class="SubWindowStyle">--%>
<body class="SubWindowStyle">
    <form id="FormfrmUserSearch" runat="server">
        <%--タイトル--%>
        <table class="SubWindowTitle">
            <tr>
                <td>
                    <div>
                        ユーザー検索
                    </div>
                </td>
                <td>
                    <input type="button" value="閉じる" class="buttoncolor btnSmall" onclick="window.close();" />
                </td>
            </tr>
        </table>
        <br/>
        <%--データ部--%>
        <table class="UserSearchTable">
            <tr>
                <td colspan="3">
                    <%--検索条件入力テーブル--%>
                    <table class="UserSearchCriteriaTable">
                        <tr>
                            <td class="SubWindowTableHeader">
                                部略称
                            </td>
                            <td>
                                <asp:TextBox ID="TxtDepartment" placeholder="部署略称" runat="server" />
                            </td>
                            <td class="SubWindowTableHeader">
                                課略称
                            </td>
                            <td>
                                <asp:TextBox ID="TxtDivision" placeholder="課略称" runat="server" />
                            </td>
                            <td colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="SubWindowTableHeader">
                                ユーザー名
                            </td>
                            <td>
                                <asp:TextBox ID="TxtUserName" placeholder="ユーザー名" runat="server" />
                            </td>
                            <td class="SubWindowTableHeader">
                                メールアドレス
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="TxtAddress" placeholder="メールアドレス" runat="server" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="BtnSearch" runat="server" Text="検索" Class="buttoncolor btnSmallLeft" OnClick="Btn_Search_Click" />
                            </td>
                            <td colspan="7">

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="UserSearchArea">
            <tr>
                <td class="tdSearchArea">
                    ≪検索結果リスト≫
                    <%--検索結果テーブル--%>
                    <div class="UserSearchResultListDiv tableDiv">
                        <table id="SearchResultTable" class="tratbl">
                            <thead>
                                <tr>
                                    <th class="tdUserId">ユーザーID</th>
                                    <th class="tdDepartment">部</th>
                                    <th class="tdDivision">課</th>
                                    <th class="tdUserName">ユーザー名</th>
                                    <th class="tdAddress"> メールアドレス</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </td>
                <td class="tdSelectedArea">
                    ≪選択リスト≫
                    <%--選択済テーブル--%>
                    <div class="UserSearchResultListDiv tableDiv">
                        <table id="SelectedTable" class="tratbl">
                            <thead>
                                <tr>
                                    <th class="tdUserId">ユーザーID</th>
                                    <th class="tdDepartment">部</th>
                                    <th class="tdDivision">課</th>
                                    <th class="tdUserName">ユーザー名</th>  
                                    <th>メールアドレス</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <table class="SubWindowFooter">
            <tr>
                <td>
                    <%--<asp:Button ID="btnSubmit" runat="server" Text="決定" Class="buttoncolor btnSmallLeft" OnClick="Btn_Search_Click" />--%>
                    <input type="button" value="決定" class="buttoncolor btnSmallLeft" onclick="Submit();" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HdnFlg" runat="server" Value="init"/>
        <asp:HiddenField ID="HdnSelected" runat="server" />
    </form>


<script src="Scripts/jquery-1.8.2.js"></script>
<script src="Scripts/jquery-ui-1.8.24.js"></script>
<script src="Scripts/media/js/jquery.dataTables.min.js"></script>
<script type="text/javascript">
    var searchTable;
    var selectedTable;
    var parentWindowObject;


    $(document).ready(function () {

        if(document.getElementById("HdnFlg").value == "init")
        {
            //親画面定義
            parentWindowObject = window.opener;  
            //親画面取得
            document.getElementById('HdnSelected').value = 
            parentWindowObject.document.getElementById('Hdn<%=QueryString%>').value;
            document.getElementById("HdnFlg").value = "";
        }

        // DataTables作成
        CreateDataTables("SearchResultTable");
        CreateDataTables("SelectedTable");
        createSearchResultTable();
        createSelectedTable();
    })
    function CreateDataTables(id) {
        var table;
        var zeroRecord;

        if (id == "SearchResultTable") {
            zeroRecord = " 検索条件に当てはまるユーザーが存在しません。"
        }
        else {
            zeroRecord = " 選択中のユーザーが存在しません。"
        }

        table = $('#' + id).DataTable(
        {
            "searching": false,     //フィルタを無効
            "info": false,         //総件数表示を無効
            "paging": false,       //ページングを無効
            "ordering": false,     //ソートを無効
            "bProcessing": true,
            "lengthChange": false,
            "bAutoWidth": false,
            "scrollY": 330,
            "scrollX": null,
            "iDisplayLength": 6,
            "columnDefs": [
                        {
                            "targets": [ 0 ],
                            "visible": false,
                            "searchable": false
                        }],
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

        if (id == "SearchResultTable") {
            searchTable = table;
        }
        else {
            selectedTable = table;
        }
        //searchTable.$("tr").click(function () {
        //    var tr = table.$("tr");
        //    alert("ok");
        //    tr.removeClass("highlight-active");
        //    $(this).addClass("highlight-active");
        //});
    }

    // 検索結果リストの行選択
    $('#SearchResultTable tbody').on('click', 'tr', function () {

        var key = $(this)[0].children[3].innerText;

        if ($(this).find('.dataTables_empty').length == 0) {

            if ($(this).hasClass('highlight-active')) {

                //var allData = $.fn.dataTable('#SelectedTable').dataTableSettings[1].aiDisplayMaster;
                var allData = $('#SelectedTable').dataTable().fnGetData();
                var matchData = allData.filter(function (item, index) {
                    if (item[3] == key)
                    return true;
                });

                if (matchData.length == 0)
                {
                    selectedTable.row.add([
                        $(this)[0].children[0].innerText,
                        $(this)[0].children[1].innerText,
                        $(this)[0].children[2].innerText,
                        $(this)[0].children[3].innerText,
                        $(this)[0].children[4].innerText,
                    ]).draw();

                    // 選択保持リスト作成
                    CreateHiddenList();
                }
                else {
                    alert("既に選択しています");
                }
            }
            else {
                searchTable.$('tr.highlight-active').removeClass('highlight-active');
                $(this).addClass('highlight-active');
            }

        }
    });

    // 選択済リストの行選択
    $('#SelectedTable tbody').on('click', 'tr', function () {
        if ($(this).find('.dataTables_empty').length == 0) {

            if ($(this).hasClass('highlight-active')) {

                selectedTable.rows('.highlight-active').remove().draw();

                // 選択保持リスト作成
                CreateHiddenList();
            }
            else {
                searchTable.$('tr.highlight-active').removeClass('highlight-active');
                $(this).addClass('highlight-active');
            }
        }
    });

        // 決定ボタン押下
    function Submit() {
        var hdnData = document.getElementById('HdnSelected').value;
        var list = hdnData.split("<->");
        
        var sendData = "";

        for (var i = 0; i < list.length; i++) {
            if (sendData != "") {
                sendData = sendData + "／";
            }

            var listRow = list[i].split(",");

            sendData = sendData + listRow[2] + "(" + listRow[0] + "・" + listRow[1] + ")";
        }


        parentWindowObject = window.opener;  //親画面定義
        parentWindowObject.document.getElementById('Txt<%=QueryString%>').value = sendData;
        parentWindowObject.document.getElementById('Hdn<%=QueryString%>').value
            = document.getElementById('HdnSelected').value; //親画面設定
        close();
    }

    function createSearchResultTable() {

<%
    for (int i = 0; i < SearchResultList.Rows.Count; i++) {
%>
        searchTable.row.add([
                    "<%=SearchResultList.Rows[i]["USER_ID"]%>",
                    "<%=SearchResultList.Rows[i]["BU_CODE"]%>",
                    "<%=SearchResultList.Rows[i]["KA_CODE"]%>",
                    "<%=SearchResultList.Rows[i]["USER_NAME"]%>",
                    "<%=SearchResultList.Rows[i]["MAIL"]%>"
        ]).draw();
<%}%>

    }


    function createSelectedTable() {
        
        var selected = document.getElementById('HdnSelected').value; //親画面設定

        var table = selected.split('<->');
        
        for (var i=0;i < table.length;i++)
        {
            var colmun = table[i].split('<>');

            if(colmun.length >= 5)
 
                selectedTable.row.add([
                            colmun[0],
                            colmun[1],
                            colmun[2],
                            colmun[3]
                ]).draw();
        }
    }

    // 選択保持リスト作成
    function CreateHiddenList() {
        var allData = $('#SelectedTable').dataTable().fnGetData();

        var selectedData;

        for (var i = 0; i < allData.length ; i++) {

            if (selectedData != undefined) {
                selectedData = selectedData + "<->";
            }
            else {
                selectedData = "";
            }

            var selectedRow = "";

            for (var j = 0; j < allData[i].length ; j++) {

                if (selectedRow != "") {
                    selectedRow = selectedRow + "<>" + allData[i][j];
                }
                else {
                    selectedRow = allData[i][j];
                }
            }
            selectedData = selectedData + selectedRow;
        }
        document.getElementById('HdnSelected').value = selectedData;
    }

    function CloseDialog() {
        close();
    }
</script>
</body>
</html>
