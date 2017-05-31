<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmTroubleList.aspx.cs" Inherits="OldTigerWeb.frmTroubleList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>--%>
    <link rel="stylesheet" href="Scripts/media/css/jquery.dataTables.css" />
    <link rel="stylesheet" href= "Content/themes/base/jquery-ui.css"/>
    <link rel="stylesheet" href="Content/OldTiger.css" />

    <%--外部JS読込み--%>
    <script type="text/javascript" src="Scripts/JSCOMMON.JS"></script>
    <script src="Scripts/jquery-1.8.2.js"></script>
    <script src="Scripts/jquery-ui-1.8.24.js"></script>
    <%-- 20170201 機能改善 START --%>
    <script src="Scripts/media/js/jquery.dataTables.min.js"></script>
	<%--<script src="Scripts/jquery.highlight-5.js"></script>--%>
    <%-- 20170201 機能改善 END --%>   

    <title>過去トラ検索結果</title>

    <%--<script type="text/javascript">--
        //// --------------------------------------------------
        //// 関数名   : DisplayForder
        //// 概要     : フォルダを表示する
        //// 引数     : strUrl     : フォルダパス
        //// 戻り値   : なし
        //// --------------------------------------------------
        //function DisplayForder(url) {
        //    try {
        //        window.open(url);
        //    }
        //    catch (e) {
        //        //alert("未存在 " + url);
        //    }
        //}

        //// --------------------------------------------------
        //// 関数名   : Window Open
        //// 概要     : 詳細画面表示
        //// 引数     : システム管理番号
        //// 戻り値   : true、false
        //// --------------------------------------------------
        //function openDetail(sysKanriNo) {
        //    // 1. 画面のオープン
        //    var url = "frmDetail.aspx?KANRINO=" + sysKanriNo;
        //    var w = (screen.width - 780) / 2;
        //    var h = (screen.height - 700) / 2;
        //    alert("A");
        //    alert(sysKanriNo);
        //    var features = "menubar=no,toolbar=no,location=no,resizable=no,scrollbars=yes,status=no,height=700,width=780,left=" + w + ",top=" + h;
        //    var returnFlg = window.open(url, sysKanriNo, features);

        //    // 3. エラー時処理
        //    // 1) 別画面1画面目
        //    if (returnFlg == 1) {

        //    }
        //    return true;
        //}

        //$(function () {
        //    $("input").keydown(function (e) {
        //        if ((e.which && e.which === 13) || (e.keyCode && e.keyCode === 13)) {
        //            var value = document.activeElement;
        //            alert(value);
        //            return false;
        //        } else {
        //            return true;
        //        }
        //    });
        //});

        /* IE8 用 */
        <%--jQuery(function ($) { $("tr:nth-child(odd)").addClass('odd'); $("tr:nth-child(even)").addClass('even'); });--

   --</script>--%>

    <style type="text/css">
    h4 { /* h1～h6を指定 http://www.aoiweb.com/aoi2/title_bar4.htm */
        /*background: url('Images/bar51.gif') no-repeat left top;*/ /* 画像を左上にひとつだけ配置 　削除　2017.03.29 神田*/
        font-size: 22px; /* 文字の大きさ */
        width: 1300px; /* 幅 */
        /*height: 30px;*/ /* 高さ */
        height: 10px; /* 高さ */
        /*padding: 3px 0px 0px 40px;*/ /* ボックスの内側[上 右 下 左]の余白 */
        padding: 0px 0px 0px 40px; /* ボックスの内側[上 右 下 左]の余白 　変更　2017.03．29 神田*/
        /*margin: 8px 0px 5px 0px;*/ /* ボックスの外側[上 右 下 左]の余白 */
        margin: 0px 0px 0px 0px; /* ボックスの外側[上 右 下 左]の余白 */
        /*color: #0073a8;*/ /* 文字の色 */
        color: gray ; /* 文字の色を黒⇒グレーに変更 2017.03.29 神田 */
    }
    .div_30,
    .div_36,
    .div_42,
    .div_80,
    .div_90,
    .tdStyle,
    .divHidden
    .common_td {
        font-family: "ＭＳ ゴシック", "monospace";
        font-size: 10pt;
    }

       *,
    *:first-letter 
    {
        text-shadow:none !important;
        opacity:1.0  !important;
    }

    div:first-letter 
    {
        font-size: 100% !important;
    }

    td:first-letter 
    {
        font-size: 100% !important;
    }

    th:first-letter 
    {
        font-size: 100% !important;
    }

    .tdStyle,
    .divHidden {
        height: 53px;
    }
    .divHidden {
        overflow: hidden;
    }
    .div_30 {
        width: 30px;
        height: 53px;
    }
    .div_36 {
        width: 30px;
        height: 53px;
    }
    .div_42 {
        width: 42px;
        height: 53px;
    }
    .div_50 {
        width:48px;
        height:53px;
    }
    .div_80 {
        width: 90px;
        height: 53px;
    }
    .div_90 {
        width: 110px;
        height: 53px;
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
    .divStyle {
        width:1330px;
    }

    .thBaseColor{
        background-color : #008000 !important;
    }
    .thTroubleColor{
        background-color : #008000 !important;
    }
    .thRecurColor{
        background-color : #008000 !important;
    }

    </style>

</head>
<body style ="margin:0 auto; padding-left :5px;">
   <%--<h4 class="auto-style7" >過去トラシステム　　　　　　　　　　　　　　　過去トラ検索結果</h4>　2017.03.29 神田 --%>
   <h4 >過去トラシステム</h4>
    <form id="frmTroubleList" runat="server">
        <div class="divStyle" style="height: 640px;height: 1330px;">
            <table style="width:100%;" >
                <tbody>
                    <tr>
                        <td colspan="5" class="midasi">過去トラ検索結果</td>
                    </tr>
                    <tr>
		              <td>検索条件：　<asp:Label ID="lblType" runat="server" /></td>
                      <td></td> 
<%-- 20170201 機能改善 START --%>
                      <td style="text-align:right"> 
                        <asp:Button ID="btnOR" runat="server" text="OR" Width="50px" Height="25px" onclick="btn_OR_Click" BackColor="#99FF99"/> 
                        <asp:Button ID="btnAND" runat="server" text="AND" Width="50px" Height="25px" onclick="btn_AND_Click" BackColor="#66FFFF"/>
                        <asp:TextBox ID="txtSearch" placeholder="検索キーワードは空白区切りで４つまで入力可" runat="server" class="moji18" style="ime-mode: active" Columns="170" Width="660px" Height="25px" BackColor ="#99FF99"/>
                      </td>
<%-- 20170201 機能改善 END --%>
                      <td style="text-align:left; padding-left:0px">
<%-- 20170201 機能改善 START --%>
                        <asp:Button ID="btnSearch" runat="server" Width="100px" Height="35px" Text="検索" CssClass="buttoncolor" OnClick="btn_Search_Click" 
                            OnClientClick="return confSearchCheck();" /> 
<%-- 20170201 機能改善 END --%>
                       </td>
                       <td style="text-align:right">
                        <asp:Button ID="btnExcel" runat="server" Width="128px" Height="35px" Text="EXCELダウンロード" CssClass="buttoncolor" 
                            OnClick="btn_Excel_Click"/>
                        <asp:Button ID="btnClose" runat="server" Width="100px" Height="35px" Text="閉じる" CssClass="buttoncolor" 
                            OnClientClick="window.close();" />
                      </td>
                    </tr>
<%-- 20170201 機能改善 START --%>
                    <tr>
                  　　<td  colspan="4">カテゴリ：　<asp:Label ID="lblCategory" runat="server" /></td>
                      <td></td>
                    </tr>
<%-- 20170201 機能改善 END --%>
                </tbody>
            </table>
            <div class="divStyle">
                <table border="1" class="tratbl" id="troubleList" >
                    <thead>
                        <tr>
                            <th rowspan="2" class="div_36 thBaseColor"><br /><br />No</th>
                            <th rowspan="2" class="div_36 thBaseColor"><br /><br />ﾗﾝｸ</th>
                            <th colspan="8" class ="thTroubleColor">不具合項目</th>
                            <th colspan="4" class ="thRecurColor">再発防止</th>
                            <th rowspan="2" class="div_42 thBaseColor" ><br /><br />部署</th>
                            <th rowspan="2" class="div_50 thBaseColor" >資料<br />No<br />一覧</th>
                            <th rowspan="2" class="div_42 thBaseColor">関<br />連<br />資<br />料</th>
                        </tr>
                        <tr>
                            <th class="div_50 thTroubleColor">BYPU<br/>区分</th>
                            <th class="div_50 thTroubleColor">管理<br />No</th>
                            <th class="div_80 thTroubleColor">項目</th>
                            <th class="div_42 thTroubleColor">F<br />M<br />C</th>
                            <th class="div_90 thTroubleColor">現象<br />（内容）</th>
                            <th class="div_90 thTroubleColor">状況</th>
                            <th class="div_90 thTroubleColor">原因</th>
                            <th class="div_90 thTroubleColor">対策</th>
                            <th class="div_90 thRecurColor">開発時の<br />流出要因</th>
                            <th class="div_90 thRecurColor">確認の<br />観点</th>
                            <th class="div_90 thRecurColor">再発防止策<br />設計面</th>
                            <th class="div_90 thRecurColor">再発防止策<br />評価面</th>
                        </tr>
                    </thead>
<%--                <tfoot>
                        <tr>
                            <th>No</th>
                            <th>ﾗﾝｸ</th>
                            <th>BYPU区分</th>
                            <th>管理<br />No</th>
                            <th>項目</th>
                            <th>F<br />M<br />C</th>
                            <th>現象<br />（内容）</th>
                            <th>状況</th>
                            <th>原因</th>
                            <th>対策</th>
                            <th>開発時の<br />流出要因</th>
                            <th>確認の<br />観点</th>
                            <th>再発防止策<br />設計面</th>
                            <th>再発防止策<br />評価面</th>
                            <th>部署</th>
                            <th>資料No一覧</th>
                            <th>関連資料</th>
                        </tr>
                    </tfoot>--%>
                    <tbody>
                    <%
                        if (userSight == "BY") {editDt = troubleListBY;}
                        else{editDt = troubleListPU;}
                                   
                        if (editDt != null){
                            for (int i = 0; i < editDt.Rows.Count; i++){
                                rowCount++;
                    %>
                        <tr>
                            <%--<td style="text-align:right"><%=editDt.Rows[i]["ROWID"].ToString() %></td>--%>
                            <td class="tdStyle" style="text-align:right"><%=rowCount %></td>
                            <td class="tdStyle" style="text-align:center;font-size:10px;"><div><%=editDt.Rows[i]["SYUMU"].ToString() %></div></td>
                            <%--<td><%=troubleListBY.Rows[i]["FOLLOW_INFO"].ToString() %></td>--%>
                            <td class="tdStyle" style="text-align:center"><%=editDt.Rows[i]["BY_PU"].ToString() %></td>
                            <td class="tdStyle"><a href="#" onclick="openDetail('<%=editDt.Rows[i]["SYSTEM_NO"].ToString() %>');">
                                <%=editDt.Rows[i]["KOUMOKU_KANRI_NO"].ToString() %></a></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["KOUMOKU"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["FUGO_NAME"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["GENSYO_NAIYO"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["JYOUKYO"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["GENIN"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["TAISAKU"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["KAIHATU_MIHAKKEN_RIYU"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["SQB_KANTEN"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["SAIHATU_SEKKEI"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["SAIHATU_HYOUKA"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["BUSYO_CODE"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["SIRYOU_NO"].ToString() %></div></td>
                            <%if (editDt.Rows[i]["LINK_FOLDER_PATH"].ToString() == ""){%>
                            <td  class="tdStyle" style="text-align:center"></td>
                            <%}else{%>
<%-- 20170201 機能改善 START --%>
                             
<%--                                <td  class="tdStyle" style="text-align:center"><a href="javascript:DisplayForder('<%=editDt.Rows[i]["LINK_FOLDER_PATH"].ToString().Trim() %>')">資料</a></td> --%>
                                <td style="text-align:center">●</td>
<%-- 20170201 機能改善 END --%>
<%--                                <td style="text-align:center">有</td>--%>
                            <%}%>
                        </tr>
                    <%  }    }%>
                    <% 
                        if (userSight == "BY") {editDt = troubleListPU;}
                        else{editDt = troubleListBY;}
                                   
                        if (editDt != null){
                            for (int i = 0; i < editDt.Rows.Count; i++){
                                rowCount++;           
                            %>
                        <tr>
                            <%--<td style="text-align:right"><%=editDt.Rows[i]["ROWID"].ToString() %></td>--%>
                            <td class="tdStyle" style="text-align:right"><%=rowCount %></td>
                            <td class="tdStyle" style="text-align:center;font-size:10px;"><div><%=editDt.Rows[i]["SYUMU"].ToString() %></div></td>
                            <%--<td><%=troubleListBY.Rows[i]["FOLLOW_INFO"].ToString() %></td>--%>
                            <td class="tdStyle" style="text-align:center"><%=editDt.Rows[i]["BY_PU"].ToString() %></td>
                            <td class="tdStyle"><a href="#" onclick="openDetail('<%=editDt.Rows[i]["SYSTEM_NO"].ToString() %>');">
                                <%=editDt.Rows[i]["KOUMOKU_KANRI_NO"].ToString() %></a></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["KOUMOKU"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["FUGO_NAME"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["GENSYO_NAIYO"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["JYOUKYO"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["GENIN"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["TAISAKU"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["KAIHATU_MIHAKKEN_RIYU"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["SQB_KANTEN"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["SAIHATU_SEKKEI"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["SAIHATU_HYOUKA"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["BUSYO_CODE"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=editDt.Rows[i]["SIRYOU_NO"].ToString() %></div></td>
                            <%if (editDt.Rows[i]["LINK_FOLDER_PATH"].ToString() == ""){%>
                            <td  class="tdStyle" style="text-align:center"></td>
                            <%}else{%>
<%-- 20170201 機能改善 START --%>
                             
<%--                                <td  class="tdStyle" style="text-align:center"><a href="javascript:DisplayForder('<%=editDt.Rows[i]["LINK_FOLDER_PATH"].ToString().Trim() %>')">資料</a></td> --%>
                                <td style="text-align:center">●</td>
<%-- 20170201 機能改善 END --%>
                                <%--<td style="text-align:center">有</td>--%>
                                    <%}%>
                        </tr>
                    <%  }    }%>
                    </tbody>
                </table>
            </div>
        </div>

    <script type="text/javascript">
        $(document).ready(function () {
            CreateDataTables('#troubleList');
        });

        function CreateDataTables(tableID) {
            //$(tableID + ' tfoot th').each(function () {
            //    var title = jQuery.trim($(this).text());
            //    $(this).html('<input style="width:100%" type="text" placeholder="' + title + '" name="' + title +　'" />' );
            //});
            //テキストボックスソート用
            //$.fn.dataTable.ext.order['dom-text'] = function (settings, col) {
            //    return this.api().column(col, { order: 'index' }).nodes().map(function (td, i) {
            //        return $('input', td).val();
            //    });
            //}

            //var cbox = { "orderDataType": "dom-text" };
            //var colNum = $(tableID)[0].rows[0].cells.length;
            //var colSet = [cbox];
            //for (var i = 0 ; i < colNum - 1 ; i++) {
            //    colSet.push(null);
            //}

            var oTable = $(tableID).DataTable({
                //initComplete: function() {
                //    var r = $(tableID+' tfoot tr');   
                //    r.find('th').each(function() {
                //        $(this).css('padding', 8);
                //    });
                //    $(tableID+' thead').append(r);
                //    $('#search_0').css('text-align', 'center');
                //},
                //20170201 機能改善 START
                //"searching": true,     //フィルタを有効
                "searching": false,     //フィルタを無効
<%--                <% if(saveFlg) {%>
                "stateSave": true,
                <%} else {%>
                "stateSave": false,
                <% saveFlg = false; }%>--%>
                //20170201 機能改善 END
                "lengthChange": false,
                "iDisplayLength": 8,
                "aaSorting":[],
                //"sDom": '<"top">rt<"bottom"lip><"clear">',
                "bAutoWidth": false,
                "oLanguage": {
                    "sProcessing": "処理中...",
                    "sLengthMenu": "_MENU_ 件表示",
                    "sZeroRecords": "検索対象の過去トラ情報は見つかりませんでした。",
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
                //, "Columns": colSet
            });
            ////フィルタを設定
            //oTable.columns().every(function () {
            //    var that = this;
            //    $('input', this.footer()).on('keyup change blur input', function () {
            //        if (that.search() !== this.value) {
            //            that
            //                .search(this.value)
            //                .draw();
            //        }
            //    });
            //});
        }

        //20170201 機能改善 START
        // --------------------------------------------------
        // 関数名   : confSearchCheck
        // 概要     : 文字列検索チェック
        // 引数     : なし
        // 戻り値   : true、false
        // --------------------------------------------------
        function confSearchCheck() {
            var txtData = document.getElementById("<%= txtSearch.ClientID %>").value;
            var lblData = document.getElementById("<%= lblCategory.ClientID %>").innerText;
            var wkData = "";
            var url = "";
            var rc = false;

            txtData = $.trim(txtData);
            lblData = $.trim(lblData);

            // 未入力は何もしない
            if (txtData == "" && lblData == "")
            {
                return rc;
            }

            // 全角スペースをを半角に置換
            while (txtData.indexOf("　", 0) != -1) {
                txtData = txtData.replace("　", " ");
            }

            // 文字数チェック
            var wkData = txtData.split(" ");
            txtData = "";
            var iCnt = 0;

            for (var i = 0; i < wkData.length; i++) {
                if ($.trim(wkData[i]) != "") {
                    if (iCnt != 0)
                    {
                        txtData += " ";
                    }
                    txtData += wkData[i];
                    iCnt++;
                }
            }

            if (iCnt > 4)
            {
                if (confirm("キーワードの指定は４つまで有効ですが、よろしいですか？"))
                {
                    return true;
                }
                return rc;
            }

            return true;
        }
        // 入力候補設定
        $(function () {
            var inputauxiliary = [
                <%=(string)ViewState["JisyoInfo"]%>
            ];

            $("#txtSearch").autocomplete({
                source: inputauxiliary
            });
        });
        // ハイライト
        $("#troubleList").bind("draw", HighLight());
<%--        function HightLight() {
            $("#troubleList").removeHighlight();                                                    //ハイライト消去
            var words = [];                                                                         //複数語句を格納するための変数(配列)
            words = "<%=(string)ViewState["HightLightWord"] + (string)ViewState["HightLight"]%>"    //カテゴリの値
            .replace(/^\s+|\s+$/g, "")                                                              //前後の空白を除去(gオプションは繰り返し)
            .replace(/\s+/g, " ")                                                                   //連続する空白を1つに
            .split(" ");                                                                            //空白で分割して配列にして代入
            for (i in words) {                                                                      //要素数ぶんループ(iには添字が入る)
                if (words[i] != "") {                                                               //空文字であれば処理しない
                    $("#troubleList td").highlight(words[i]);                                       //#target内のテキストに対し、処理中の語句でハイライト
                }
            }
        }--%>
        function HighLight() {
            var words = [];                                                                         //複数語句を格納するための変数(配列)
            words = "<%=(string)ViewState["HightLightWord"] + (string)ViewState["HightLight"]%>"    //カテゴリの値
            .replace(/^\s+|\s+$/g, "")                                                              //前後の空白を除去(gオプションは繰り返し)
            .replace(/\s+/g, " ")                                                                   //連続する空白を1つに
            .split(" ");                                                                            //空白で分割して配列にして代入
            for (i in words) {                                                                      //要素数ぶんループ(iには添字が入る)
                if (words[i] != "") {                                                               //空文字であれば処理しない
                    $('#troubleList').each(function () {
                        $(this).find('td').each(function () {
                            if ($(this).text().match(new RegExp(words[i], "g"))) {
                                $(this).attr('style', 'background-color:yellow');
                            }
                        });
                    });
                }
            }
        }

        //20170201 機能改善 END
        // --------------------------------------------------
        // 関数名   : DisplayForder
        // 概要     : フォルダを表示する
        // 引数     : strUrl     : フォルダパス
        // 戻り値   : なし
        // --------------------------------------------------
        function DisplayForder(url) {
            try {
                window.open(url);
            }
            catch (e) {
                //alert("未存在 " + url);
            }
        }
        // --------------------------------------------------
        // 関数名   : Window Open
        // 概要     : 詳細画面表示
        // 引数     : システム管理番号
        // 戻り値   : true、false
        // --------------------------------------------------
        function openDetail(sysKanriNo) {
            // 1. 画面のオープン
            var url = "frmDetail.aspx?KANRINO=" + sysKanriNo;
            var w = (screen.width - 780) / 2;
            var h = (screen.height - 700) / 2;
            var features = "menubar=no,toolbar=no,location=no,resizable=no,scrollbars=yes,status=no,height=700,width=780,left=" + w + ",top=" + h;
            var returnFlg = window.open(url, sysKanriNo, features);

            // 3. エラー時処理
            // 1) 別画面1画面目
            if (returnFlg == 1) {

            }
            return true;
        }

        $(function () {
            $("input").keydown(function (e) {
                if ((e.which && e.which === 13) || (e.keyCode && e.keyCode === 13)) {
                    $('#btnSearch').click();
                    return false;
                } else {
                    return true;
                }
            });
        });
        /* IE8 用 */
        jQuery(function ($) { $("tr:nth-child(odd)").addClass('odd'); $("tr:nth-child(even)").addClass('even'); });


    </script>
    </form>
</body>
</html>
