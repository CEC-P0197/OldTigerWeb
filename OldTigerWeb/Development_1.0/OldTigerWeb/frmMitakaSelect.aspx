<%@Page Language="C#" AutoEventWireup="true" CodeBehind="frmMitakaSelect.aspx.cs" Inherits="OldTigerWeb.frmMitakaSelect"%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="Scripts/media/css/jquery.dataTables.css" />
    <link rel="stylesheet" href= "Content/themes/base/jquery-ui.css"/>
    <link rel="stylesheet" href="Content/OldTiger.css" />

    <title>過去トラ観たか選択</title>
</head>
<body class="SubWindowStyle">
    <form id="FormMitakaSelect" runat="server">
        <%--タイトル--%>
        <table class="SubWindowTitle">
            <tr>
                <td>
                    <div>
                        過去トラ観たか選択
                    </div>
                </td>
                <td>
                    <input type="button" value="閉じる" class="buttoncolor btnSmall" onclick="window.close();" />
                </td>
            </tr>
        </table>
        <br/>
        <table class="MitakaSelectTable">
            <tr>
                <td>
                    <%--過去トラ観たかヘッダー--%>
                    <table class="MitakaSelectTable">
                        <tr>
                            <td colspan="5">
                                【追加条件】
                            </td>
                        </tr>
                        <tr>
                            <td class="SubWindowTableHeader">
                                検索キーワード
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="TxtKeyword" placeholder="キーワード" value="" runat="server" ReadOnly="true"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="SubWindowTableHeader">
                                検索カテゴリ
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="TxtCategory"  placeholder="カテゴリ" value="" runat="server" ReadOnly="true" TextMode="MultiLine" MaxLength="200" CssClass="MultiLineTxt" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr></tr>
            <tr>
                <td>
                        <%--過去トラ観たかリスト--%>
                    <div class="tableDiv">
                        <%--<table  id="TroubleTable" class="tableDiv tratbl">--%>
                        <table border="1" class="tableDiv tratbl" id="HeaderTable" style="width:100%;">
                            <thead>
                                <tr>
                                    <th>管理番号</th>
                                    <th>タイトル</th>
                                    <th>回答期間</th>
                                    <th>状況</th>
                                    <th>作成部署</th>
                                    <th>回答対象部署</th>
                                </tr>
                            </thead>
                            <tbody>
                            <%
                            if(_MitakaSearchData.SearchResultList.Count > 0)
                            {
                                for (int i = 0; i < _MitakaSearchData.SearchResultList.Count; i++){
                            %>
                                <tr>
                                    <td class="tdStyle"><div class="divHidden"><%=_MitakaSearchData.SearchResultList[i].ManageNo.ToString() %></div></td>
                                    <td class="tdStyle"><div class="divHidden"><%=_MitakaSearchData.SearchResultList[i].Title.ToString() %></div></td>
                                    <td class="tdStyle"><div class="divHidden">
                                    <% if (_MitakaSearchData.SearchResultList[i].Status == "20" || _MitakaSearchData.SearchResultList[i].Status == "30")
                                    { %>
                                        <%=_MitakaSearchData.SearchResultList[i].StartDateTime.ToString() + "～" + 
                                                                                        _MitakaSearchData.SearchResultList[i].EndDateTime.ToString()    %>
                                    <%}
                                     else
                                    { %>
                                        <%=""%>
                                    <%}%>
                                    </div></td>
                                    <td class="tdStyle"><div class="divHidden"><%=_MitakaSearchData.SearchResultList[i].Status.ToString() %></div></td>
                                    <td class="tdStyle"><div class="divHidden">
                                    <% if (_MitakaSearchData.SearchResultList[i].ManageMainDivisionCode == _MitakaSearchData.SearchResultList[i].ManageSubDivisionCode)
                                    { %>
                                            <%=_MitakaSearchData.SearchResultList[i].ManageMainDivisionCode.ToString() %>
                                    <%}
                                     else if (_MitakaSearchData.SearchResultList[i].ManageMainDivisionCode != _MitakaSearchData.SearchResultList[i].ManageSubDivisionCode)
                                    { %>
                                            <%=_MitakaSearchData.SearchResultList[i].ManageMainDivisionCode.ToString() + "," +
                                                                                       _MitakaSearchData.SearchResultList[i].ManageSubDivisionCode.ToString() %>
                                    <%}%>
                                    </div></td>
                                    <td class="tdStyle"><div class="divHidden">
                                        <%=_MitakaSearchData.SearchResultList[i].MitakaAnswerData.MitakaAnswerList.Rows[0]["ANSWER_DIVISION_CODE"].ToString() %>
                                    </div></td>
                                </tr>
                            <%  
                                }
                            }%>
                            </tbody>
                        </table>
                    </div>
                    <input type="button" value="追加" class="buttoncolor btnSmall" onclick="selectedAdd();" />
                </td>
            </tr>
            <tr>
            </tr>
        </table>
<%--        <table class="MitakaSelectFooter">
            <tr>
                <td>
                </td>
            </tr>
        </table>--%>

    </form>


</body>
<script src="Scripts/jquery-1.8.2.js"></script>
<script src="Scripts/jquery-ui-1.8.24.js"></script>
<script src="Scripts/media/js/jquery.dataTables.min.js"></script>
<script type="text/javascript">

    //var troubleTable;
    var headerTable;
    var selectedManageNo;

    $(document).ready(function () {
        //CreateDataTables("TroubleTable");
        CreateDataTables("HeaderTable");

        document.getElementById("TxtKeyword").value = "<%= Session[Def.DefPARA_WORD]%>";
        document.getElementById("TxtCategory").value = "<%= Session[Def.DefSERCH_WORD]%>";
    })

    function CreateDataTables(id) {
        var table;
        var zeroRecord;
        var size;

        //if (id == "TroubleTable") {
        //if (id == "HeaderTable") {
        //    zeroRecord = " 追加対象となる過去トラ観たかが存在しません。"
        //    size = 400;
        //}

        zeroRecord = "";
        size = 100;

        table = $('#' + id).DataTable(
        {
            "searching": false,     //フィルタを無効
            "info": false,         //総件数表示を無効
            "paging": false,       //ページングを無効
            "ordering": true,     //ソートを有効
            "bProcessing": false,
            "lengthChange": false,
            "bAutoWidth": true,
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

        if (id == "HeaderTable") {
            headerTable = table;
        }

        headerTable.$("tr").click(function () {
            var tr = table.$("tr");
            //alert("ok");
            tr.removeClass("highlight-active");
            $(this).addClass("highlight-active");
            selectedManageNo = $(this)[0].children[0].innerText;
        });
    }

    function selectedAdd() {

        var url = "frmMitakaRegist?ManageNo=" + selectedManageNo;
        location.href = url;
    }


    function OpenSubWindowMitakaRegist() {

        var url = "frmMitakaRegist?ManageNo=Test&AddFlg=Test";
        location.href = url;
    }

</script>
</html>
