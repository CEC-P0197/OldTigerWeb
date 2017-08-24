<%@Page Language="C#" AutoEventWireup="true" CodeBehind="frmMitakaRegist.aspx.cs" Inherits="OldTigerWeb.frmMitakaRegist"%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="Scripts/media/css/jquery.dataTables.css" />
    <link rel="stylesheet" href= "Content/themes/base/jquery-ui.css"/>
    <link rel="stylesheet" href="Content/OldTiger.css" />

    <title>過去トラ観たか新規登録</title>
</head>
<body class="SubWindowStyle">
    <form id="FormMitakaRegist" runat="server">
        <%--タイトル--%>
        <table class="SubWindowTitle">
            <tr>
                <td>
                    <div>
                        <asp:Label runat="server" ID="PageTitle"></asp:Label>
                    </div>
                </td>
                <td>
                    <table border="1" class="">
                        <tr>
                            <td>管理番号</td>
                            <td>最終更新者</td>
                            <td>最終更新日時</td>
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="TxtTlManageNo" runat="server" CssClass="ReadOnlyTxt" Text="" ReadOnly="true" /></td>
                            <td><asp:TextBox ID="TxtTlLastUpdateUser" runat="server" CssClass="ReadOnlyTxt" Text="" ReadOnly="true" /></td>
                            <td><asp:TextBox ID="TxtTlLastUpdateYMD" runat="server" CssClass="ReadOnlyTxt" Text="" ReadOnly="true" /></td>
                        </tr>
                    </table>
                </td>
                <td>
                    <%--<input type="button" value="閉じる" class="buttoncolor btnSmall" onclick="btn_Close_Click" />--%>
                    <asp:Button ID="BtnClose" runat="server" Width="100px" Height="35px" Text="閉じる" CssClass="buttoncolor btnClose" onclick="btn_Close_Click" />
                </td>
            </tr>
        </table>
        <br/>
        <%--データ部--%>
        <table border="1" class="" style="width:100%;">
            <tr>
                <td colspan="1" class="SubWindowTableHeader">タイトル</td>
                <td colspan="5">
                    <%--<asp:TextBox ID="TxtTitle" placeholder="50文字以内で入力してください" runat="server" TextMode="MultiLine" MaxLength="50" CssClass="MultiLineTxt"/>--%>
                    <asp:TextBox ID="TxtTitle" placeholder="" runat="server" Columns="180"/>
                </td>
            </tr>
        </table>
        <div id="divForm" style="display:block;">
        <table border="1" class="" style="width:100%;">
            <tr>
                <td colspan="1" class="SubWindowTableHeader">目的</td>
                <td colspan="5">
                    <%--<asp:TextBox ID="TxtPurpose" placeholder="50文字以内で入力してください" runat="server" TextMode="MultiLine" MaxLength="50" CssClass="MultiLineTxt"/>--%>
                    <asp:TextBox ID="TxtPurpose" placeholder="" runat="server" Columns="180"/>
                </td>
            </tr>
            <tr>
                <td colspan="1" class="SubWindowTableHeader">状況</td>
                <td colspan="1">
                    <asp:TextBox ID="TxtSituation" runat="server" Text="" ReadOnly="true" CssClass="ReadOnlyTxt" Columns="40"/>
                </td>
                <td colspan="1"class="SubWindowTableHeader">回答開始日時</td>
                <td colspan="1">
                    <asp:TextBox ID="TxtAnswerStartYMD" runat="server" Text="" ReadOnly="true" CssClass="ReadOnlyTxt" Columns="40"/>
                </td>
                <td colspan="1"class="SubWindowTableHeader">回答期限</td>
                <td colspan="1">
                    <asp:TextBox ID="TxtAnswerLimit" runat="server" Text="" ReadOnly="true" CssClass="ReadOnlyTxt" Columns="40"/>
                </td>
            </tr>
            <tr>
                <td colspan="1" class="SubWindowTableHeader">作成者(正)</td>
                <td colspan="1">
                    <asp:TextBox ID="TxtUserMain" runat="server" ReadOnly="true" CssClass="ReadOnlyTxt" Columns="40"/>
                    <input id="BtnTxtUserMain" type="button" value=">>" class="buttoncolor" onclick="openSubWindowUserSearch();" />
                </td>
                <td colspan="1" class="SubWindowTableHeader">作成者(副)</td>
                <td colspan="1">
                    <asp:TextBox ID="TxtUserSub" runat="server" ReadOnly="true" CssClass="ReadOnlyTxt" Columns="40"/>
                    <input id="BtnTxtUserSub" type="button" value=">>" class="buttoncolor" onclick="openSubWindowUserSearch();" />
                </td>
                <td colspan="1" class="SubWindowTableHeader">点検者</td>
                <td colspan="1">
                    <asp:TextBox ID="TxtCheckUser" runat="server" ReadOnly="true" CssClass="ReadOnlyTxt" Columns="40"/>
                    <input id="BtnTxtCheckUser" type="button" value=">>" class="buttoncolor" onclick="openSubWindowUserSearch();" />
                </td>
            </tr>
            <tr>
                <td colspan="1" class="SubWindowTableHeader">回答依頼先</td>
                <td colspan="5">
                    <asp:TextBox ID="TxtAnswerRequest" runat="server" ReadOnly="true" CssClass="ReadOnlyTxt" Columns="180"/>
                    <input id="BtnTxtAnswerRequest" type="button" value=">>" class="buttoncolor" onclick="openSubWindowUserSearch('AnswerRequest');" />
                </td>
            </tr>
            <tr>
                <td colspan="1" class="SubWindowTableHeader">開発符号</td>
                <td colspan="2">
                    <asp:TextBox ID="TxtDevelopCode" runat="server" ReadOnly="true" CssClass="ReadOnlyTxt" Columns="60" />
                    <input id="BtnTxtDevelopCode" type="button" value=">>" class="buttoncolor" onclick="openSubWindowUserSearch();" />
                </td>
                <td colspan="1" class="SubWindowTableHeader">機種</td>
                <td colspan="2">
                    <asp:TextBox ID="TxtModel" runat="server" ReadOnly="true" CssClass="ReadOnlyTxt" Columns="60" />
                    <input id="BtnTxtModel" type="button" value=">>" class="buttoncolor" onclick="openSubWindowUserSearch();" />
                </td>
            </tr>
            <tr>
                <td colspan="1" class="SubWindowTableHeader">設通番号</td>
                <td colspan="5">
                    <asp:TextBox ID="TxtEcsNo" runat="server" ReadOnly="true" CssClass="ReadOnlyTxt" Columns="180"/>
                    <input id="BtnTxtEcsNo" type="button" value=">>" class="buttoncolor" onclick="openSubWindowUserSearch();" />
                </td>
            </tr>
            <tr>
                <td colspan="1" class="SubWindowTableHeader">BLK No</td>
                <td colspan="2">
                    <asp:TextBox ID="TxtBlkNo" runat="server" ReadOnly="true" CssClass="ReadOnlyTxt" Columns="60" />
                    <input id="BtnTxtBlkNo" type="button" value=">>" class="buttoncolor" onclick="openSubWindowUserSearch();" />
                </td>
                <td colspan="1" class="SubWindowTableHeader">タイトル品番</td>
                <td colspan="2">
                    <asp:TextBox ID="TxtTitleDrawingNo" runat="server" ReadOnly="true" CssClass="ReadOnlyTxt" Columns="60" />
                    <input id="BtnTxtTitleDrawingNo" type="button" value=">>" class="buttoncolor" onclick="openSubWindowUserSearch();" />
                </td>
            </tr>
            <tr>
                <td colspan="1" class="SubWindowTableHeader">過去トラ確認<br />結果まとめ</td>
                <%--<td colspan="12"　rowspan="3">--%>
                <td colspan="5">
                    <%--<asp:TextBox ID="TxtSummary" placeholder="120文字以内で入力してください" runat="server"  Text="" TextMode="MultiLine" MaxLength="120" CssClass="MultiLineTxt" />--%>
                    <asp:TextBox ID="TxtSummary" placeholder="" runat="server"  Text="" Columns="180" />
                </td>
            </tr>
        </table>
        <table border="1" class="SearchTable" style="width:100%;">
            <tr>
                <td class="SubWindowTableHeader">抽出条件</td>
            </tr>
            <tr>
                <td colspan="12">
                    <div class="MitakaRegistSearchDiv tableDiv">
                        <table id="SearchParameterTable" class="tratbl">
                            <thead>
                                <tr>
                                    <th>検索区分</th>
                                    <th>検索キーワード</th>
                                    <th>検索カテゴリ</th>
                                </tr>
                            </thead>
                            <tbody>
                            <%
                            if(_SearchCondition != null)
                            {
                                for (int i = 0; i < _SearchCondition.Rows.Count; i++){
                            %>
                                <tr>
                                    <td class="tdStyle"><div class="divHidden"><%=_SearchCondition.Rows[i]["SEARCH_CLASS"].ToString() %></div></td>
                                    <td class="tdStyle"><div class="divHidden"><%=_SearchCondition.Rows[i]["SEARCH_TYPE"].ToString() %></div></td>
                                    <td class="tdStyle"><div class="divHidden"><%=_SearchCondition.Rows[i]["SEARCH_PARAMETER"].ToString() %></div></td>
                                </tr>
                            <%  
                                }
                            }%>
                            </tbody>
                        </table>
                    </div>
                </td>
            </tr>
         </table>
         </div>
         <div style="float:right;">
             <input name="lnkForm" type="button" value="▲" class="buttoncolor" style="display:block" onclick="displayForm();" />
         </div> 
         <br/>
         <%--<div class="divStyle">--%>
        <table class="tratbl noneStyle" id="" style="width:100%;">
            <tr>
                <td class="SubWindowTableHeader">回答リスト</td>
            </tr>
        </table>
            <table border="1" class="tratbl" id="troubleList" style="width:100%;">
            <%--<table border="1" class="tratbl noneStyle" id="troubleList">--%>
  <%--              <tr>
                    <td class="SubWindowTableHeader">回答リスト</td>
                </tr>--%>
                    <thead>
                        <tr>
                            <th rowspan="2" class="div_36 thBaseColor">対象<br />項目</th>
                            <th rowspan="2" class="div_36 thBaseColor">BYPU<br/>区分</th>
                            <th rowspan="2" class="div_36 thBaseColor">管理<br/>No</th>
                            <th rowspan="2" class="div_36 thBaseColor">重要度<br/>ランク</th>
                            <th colspan="4" class ="thTroubleColor">不具合項目</th>
                            <th colspan="3" class ="thRecurColor">再発防止項目</th>
                            <th rowspan="2" class="div_50 thBaseColor" >資料No一覧<br/>＆<br/>設通番号</th>
                            <th colspan="2" class="thDepartColor" >部署</th>
                            <th rowspan="2" class="div_42 thBaseColor">回答対象部署</th>
                        </tr>
                        <tr>
                            <th class="div_50 thTroubleColor">部品</th>
                            <th class="div_42 thTroubleColor">F<br />M<br />C</th>
                            <th class="div_90 thTroubleColor">原因</th>
                            <th class="div_90 thTroubleColor">対策</th>
                            <th class="div_90 thRecurColor">確認の<br />観点</th>
                            <th class="div_90 thRecurColor">再発防止策<br />設計面</th>
                            <th class="div_90 thRecurColor">再発防止策<br />評価面</th>
                            <th class="div_90 thDepartColor">設計部署</th>
                            <th class="div_90 thDepartColor">評価部署</th>
                        </tr>
                    </thead>  
                    <tbody>
                    <%
                    if(_TroubleData != null)
                    {
                        for (int i = 0; i < _TroubleData.Rows.Count; i++){
                    %>
                        <tr>
                            <td class="tdStyle"><div class="divHidden"><input runat="server" type="checkbox" name="chkLineNm" id="chkLineID" /></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=_TroubleData.Rows[i]["BY_PU"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=_TroubleData.Rows[i]["KOUMOKU_KANRI_NO"].ToString() + _TroubleData.Rows[i]["KOUMOKU"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=_TroubleData.Rows[i]["RANK"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=_TroubleData.Rows[i]["FUGO_NAME1"].ToString() + "," + 
                                                                               _TroubleData.Rows[i]["FUGO_NAME2"].ToString() + "," + 
                                                                               _TroubleData.Rows[i]["FUGO_NAME3"].ToString() + "," + 
                                                                               _TroubleData.Rows[i]["FUGO_NAME4"].ToString() + "," + 
                                                                               _TroubleData.Rows[i]["FUGO_NAME5"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=_TroubleData.Rows[i]["GENIN"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=_TroubleData.Rows[i]["TAISAKU"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=_TroubleData.Rows[i]["SQB_KANTEN"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=_TroubleData.Rows[i]["SAIHATU_SEKKEI"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=_TroubleData.Rows[i]["SAIHATU_HYOUKA"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=_TroubleData.Rows[i]["SETTU_NO1"].ToString() + "," + 
                                                                               _TroubleData.Rows[i]["SETTU_NO2"].ToString() + "," + 
                                                                               _TroubleData.Rows[i]["SETTU_NO3"].ToString() + "," + 
                                                                               _TroubleData.Rows[i]["SETTU_NO4"].ToString() + "," + 
                                                                               _TroubleData.Rows[i]["SETTU_NO5"].ToString() + "," + 
                                                                               _TroubleData.Rows[i]["SIRYOU_NO1"].ToString() + "," + 
                                                                               _TroubleData.Rows[i]["SIRYOU_NO2"].ToString() + "," + 
                                                                               _TroubleData.Rows[i]["SIRYOU_NO3"].ToString() + "," + 
                                                                               _TroubleData.Rows[i]["SIRYOU_NO4"].ToString() + "," + 
                                                                               _TroubleData.Rows[i]["SIRYOU_NO5"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=_TroubleData.Rows[i]["BUSYO_SEKKEI_R"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><%=_TroubleData.Rows[i]["BUSYO_HYOUKA_R"].ToString() %></div></td>
                            <td class="tdStyle"><div class="divHidden"><a href="#" onclick="openDetail('<%=_TroubleData.Rows[i]["BUSYO_KAITOU_R"].ToString()%>');">
                                                                               <%=_TroubleData.Rows[i]["SYSTEM_NO"].ToString() %></a></div></td>
                        </tr>
                    <%  
                        }
                    }%>
                    </tbody>
            </table>
            <%--</div>--%>
      
        <table class="SubWindowFooter">
            <tr>
                <td>
                    <asp:Button ID="BtnNewPreserve" runat="server" Width="100px" Height="35px" Text="新規保存" CssClass="buttoncolor btnSmallLeft" onclick="btn_Preserve_Click" OnClientClick="return confPreserveCheck();"/>
                    <asp:Button ID="BtnPreserve" runat="server" Width="100px" Height="35px" Text="保存" CssClass="buttoncolor btnSmallLeft" onclick="btn_Preserve_Click" OnClientClick="confPreserveCheck();"/>
                    <asp:Button ID="BtnCheckPrint" runat="server" Width="100px" Height="35px" Text="点検印刷" CssClass="buttoncolor btnSmallLeft" onclick="btn_CheckPrint_Click" />
                    <asp:Button ID="BtnAnswerRequest" runat="server" Width="100px" Height="35px" Text="回答依頼" CssClass="buttoncolor btnSmall" onclick="btn_AnswerRequest_Click" OnClientClick="confPreserveCheck();"/>
                    <asp:Button ID="BtnAnswerConfirm" runat="server" Width="100px" Height="35px" Text="回答/確認" CssClass="buttoncolor btnSmall" onclick="btn_AnswerConfirm_Click" OnClientClick="confPreserveCheck();"/>
                    <asp:Button ID="BtnConfirmFinish" runat="server" Width="100px" Height="35px" Text="確認完了" CssClass="buttoncolor btnSmall" onclick="btn_ConfirmFinish_Click" />
                    <asp:Button ID="BtnDelete" runat="server" Width="100px" Height="35px" Text="削除" CssClass="buttoncolor btnSmall" onclick="btn_Delete_Click" />
                    <asp:Button ID="BtnFinishCancel" runat="server" Width="100px" Height="35px" Text="完了取消" CssClass="buttoncolor btnSmall" onclick="btn_FinishCancel_Click" />
                </td>
            </tr>
        </table>
        <asp:HiddenField runat="server" ID="HdnManageNo" Value="" />
        <asp:HiddenField runat="server" ID="HdnUserMain" Value="" />
        <asp:HiddenField runat="server" ID="HdnUserSub" Value="" />
        <asp:HiddenField runat="server" ID="HdnAnswerRequest" Value="" />
        <asp:HiddenField runat="server" ID="HdnDevelopCode" Value="" />
        <asp:HiddenField runat="server" ID="HdnModel" Value="" />
        <asp:HiddenField runat="server" ID="HdnBlkNo" Value="" />
        <asp:HiddenField runat="server" ID="HdnTitleDrawingNo" Value="" />
        <asp:HiddenField runat="server" ID="HdnEcsNo" Value="" />
        <asp:HiddenField runat="server" ID="HdnDeploymentTarget" Value="" />
    </form>

</body>
<script src="Scripts/jquery-1.8.2.js"></script>
<script src="Scripts/jquery-ui-1.8.24.js"></script>
<script src="Scripts/media/js/jquery.dataTables.min.js"></script>
<script type="text/javascript">
    var _SearchPrmTable;
    var allData;

    $(document).ready(function () {

        $('#TxtTlManageNo').val("<%= ViewState["MITAKA_NO"]%>"); // 管理番号
        $('#TxtTlLastUpdateUser').val("<%= ViewState["LAST_UPDATE_USER"]%>"); // 最終更新者
        $('#TxtTlLastUpdateYMD').val("<%= ViewState["LAST_UPDATE_YMD"]%>"); // 最終更新日時
        $('#TxtTitle').val("<%= ViewState["TITLE"]%>"); // タイトル
        $('#TxtPurpose').val("<%= ViewState["PURPOSE"]%>"); // 目的
        $('#TxtSituation').val("<%= ViewState["STATUS"]%>"); // 状況
        $('#TxtAnswerStartYMD').val("<%= ViewState["START_YMD"]%>"); // 回答開始日時
        $('#TxtAnswerLimit').val("<%= ViewState["END_YMD"]%>"); // 回答期限
        $('#TxtUserMain').val("<%= ViewState["USER_MAIN"]%>"); // 作成者(正)
        $('#TxtUserSub').val("<%= ViewState["USER_SUB"]%>"); // 作成者(副)
        $('#TxtCheckUser').val("<%= ViewState["CHECK_USER"]%>"); // 点検者
        $('#TxtAnswerRequest').val("<%= ViewState["ANSWER_REQUEST_USER"]%>"); // 回答依頼先
        $('#TxtDevelopCode').val("<%= ViewState["DEVELOPMENT_CODE"]%>"); // 開発符号
        $('#TxtModel').val("<%= ViewState["MODEL"]%>"); // 機種
        $('#TxtEcsNo').val("<%= ViewState["ECS_NO"]%>"); // 設通番号
        $('#TxtBlkNo').val("<%= ViewState["BLK_NO"]%>"); // BLK No
        $('#TxtTitleDrawingNo').val("<%= ViewState["TITLE_DRAWING_NO"]%>"); // タイトル品番
        $('#TxtSummary').val("<%= ViewState["COMMENT"]%>"); // 過去トラ確認結果まとめ

        // 隠し項目
        $('#HdnUserMain').val("<%= ViewState["HDN_USER_MAIN"]%>"); // 作成者（正）
        $('#HdnUserSub').val("<%= ViewState["HDN_USER_SUB"]%>"); // 作成者（副）
        $('#HdnAnswerRequest').val("<%= ViewState["HDN_ANSWER_REQUEST_USER"]%>"); // 回答依頼先
        $('#HdnDevelopCode').val("<%= ViewState["HDN_DEVELOPMENT_CODE"]%>"); // 開発符号
        $('#HdnModel').val("<%= ViewState["HDN_MODEL"]%>"); // 機種
        $('#HdnBlkNo').val("<%= ViewState["HDN_BLK_NO"]%>"); // BLK No
        $('#HdnTitleDrawingNo').val("<%= ViewState["HDN_TITLE_DRAWING_NO"]%>"); // タイトル品番
        $('#HdnEcsNo').val("<%= ViewState["HDN_ECS_NO"]%>"); // 設通番号
        $('#HdnDeploymentTarget').val("<%= ViewState["HDN_DEPLOYMENT_TARGET"]%>"); // 展開対象


        if($("#HdnManageNo").val() != "")
        {
            document.title = "過去トラ観たか編集";

            //document.getElementById("TxtInstPrimaryKey").value = "過去トラ観たか管理番号";

            //document.getElementById("BtnRegist").value = "更新";
            //document.getElementById("BtnDelete").style.display = "block";
        }

        CreateDataTables("SearchParameterTable");

        CreateDataTableBody("troubleList");

        // troubleListの全てのデータを取得(画面に表示されていないデータも含む)　※取得するのはインデックス
        allData = $.fn.dataTable.settings[1].aiDisplayMaster;

        $(allData).each(function () {
            // DataTablesから行を取得
            var rowNum = parseInt(this.toFixed());
            //var data = $('#troubleList').DataTable().fnGetNodes(rowNum);

            // data.childNodes[0].childNodes[1]　等で値を取得

        });

        // ボタン　表示コントロール
        if ($('#TxtSituation').val() == "") // 新規登録
        {
            $('#BtnPreserve').hide();
            $('#BtnCheckPrint').hide();
            $('#BtnAnswerRequest').hide();
            $('#BtnAnswerConfirm').hide();
            $('#BtnConfirmFinish').hide();
            $('#BtnDelete').hide();
            $('#BtnFinishCancel').hide();
        }
        else if ($('#TxtSituation').val() == "10"
             || $('#TxtSituation').val() == "20") // "10"(回答準備)or"20"(回答中)
        {
            $('#BtnNewPreserve').hide();
            $('#BtnFinishCancel').hide();
        }
        else if ($('#TxtSituation').val() == "30"
             || $('#TxtSituation').val() == "99") // "30"(確認完了)or"99"(取消)
        {
            $('#BtnNewPreserve').hide();
            $('#BtnPreserve').hide();
            $('#BtnCheckPrint').hide();
            $('#BtnAnswerRequest').hide();
            $('#BtnAnswerConfirm').hide();
            $('#BtnConfirmFinish').hide();
            $('#BtnDelete').hide();
            $('#BtnFinishCancel').hide();
        }

        // 過去トラ観たか情報　使用コントロール
        if ($('#TxtSituation').val() == ""        // 新規登録
             || $('#TxtSituation').val() == "10"
             || $('#TxtSituation').val() == "20") // "10"(回答準備)or"20"(回答中)
        {
            
        }
        else if ($('#TxtSituation').val() == "30"
             || $('#TxtSituation').val() == "99") // "30"(確認完了)or"99"(取消)
        {
            var txtTitleElement = document.getElementById("TxtTitle");
            txtTitleElement.readOnly = true;

            var txtPurposeElement = document.getElementById("TxtPurpose");
            txtPurposeElement.readOnly = true;

            var txtSummaryElement = document.getElementById("TxtSummary");
            txtSummaryElement.readOnly = true;

            $('BtnTxtUserMain').hide();
            $('BtnTxtUserSub').hide();
            $('BtnTxtCheckUser').hide();
            $('BtnTxtAnswerRequest').hide();
            $('BtnTxtDevelopCode').hide();
            $('BtnTxtModel').hide();
            $('BtnTxtEcsNo').hide();
            $('BtnTxtBlkNo').hide();
            $('BtnTxtTitleDrawingNo').hide();
        }

    });

    function displayForm() {
        if (document.getElementById("divForm").style.display == "none") {
            document.getElementById("divForm").style.display = "block";
            
            $.fn.dataTable.settings[0]._iDisplayLength = 3;
        }
        else {
            document.getElementById("divForm").style.display = "none";
            
            $.fn.dataTable.settings[0]._iDisplayLength = 8;
        }
        //$("#troubleList").dataTable().fnDraw();
    }

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
            _SearchPrmTable = table;
        }
    }

    function CreateDataTableBody(id) {
        var table;
        var zeroRecord;
        var size;

        if (id == "troubleList") {
            zeroRecord = " データが存在しません。"
            size = 100;
        }
        //else {
        //    zeroRecord = " 追加対象となる過去トラ情報が存在しません。"
        //    size = 230;
        //}

        table = $('#' + id).DataTable(
        {
            "searching": false,     //フィルタを無効
            "info": true,         //総件数表示を有効
            "paging": true,       //ページングを有効
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
                "sZeroRecords": "",
                "sInfo": "_START_件～_END_件を表示（全_TOTAL_ 件中）",
                "sInfoEmpty": " 0 件中 0 から 0 まで表示",
                "sInfoFiltered": "（全 _MAX_ 件より抽出）",
                //"sInfoPostFix": "",
                //"sSearch": "全体検索：　",
                //"sUrl": "",
                "oPaginate": {
                    "sFirst": "先頭",
                    "sPrevious": "前ページ",
                    "sNext": "次ページ",
                    "sLast": "最終"
                }
            }
        });

        //if (id == "SearchParameterTable") {
        //    _SearchPrmTable = table;
        //}
    }

    // --------------------------------------------------
    // 関数名   : confPreserveCheck
    // 概要     : 必須入力、文字数チェック
    // 引数     : なし
    // 戻り値   : true、false
    // --------------------------------------------------
    function confPreserveCheck() {
        var txtTitleElement = document.getElementById("TxtTitle").value;
        var txtPurposeElement = document.getElementById("TxtPurpose").value;
        var txtAnswerLimitElement = document.getElementById("TxtAnswerLimit").value;
        var txtUserMainElement = document.getElementById("TxtUserMain").value;
        var txtSummaryElement = document.getElementById("TxtSummary").value;
        var txtCheckUserElement = document.getElementById("TxtCheckUser").value;
 
        txtTitleElement = $.trim(txtTitleElement);
        txtPurposeElement = $.trim(txtPurposeElement);
        txtAnswerLimitElement = $.trim(txtAnswerLimitElement);
        txtUserMainElement = $.trim(txtUserMainElement);
        txtSummaryElement = $.trim(txtSummaryElement);
        txtCheckUserElement = $.trim(txtCheckUserElement);
 
        // 必須入力チェック
        if (txtTitleElement == "")
        {
            alert("タイトルは必須入力です。");
            return false;
        }

        if (txtPurposeElement == "") {
            alert("目的は必須入力です。");
            return false;
        }

        //if (txtAnswerLimitElement == "") {
        //alert("回答期限は必須入力です。");
        //    return false;
        //}

        //if (txtUserMainElement == "") {
        //alert("作成者（正）は必須入力です。");
        //    return false;
        //}

        //if (txtCheckUserElement == "") {
        //alert("点検者は必須入力です。");
        //    return false;
        //}
        
        // 文字数チェック
        if (txtTitleElement != "" && txtTitleElement.length > 50)
        {
            alert("タイトルの文字数は最大で全角50文字です。");
            return false;
        }

        if (txtPurposeElement != "" && txtPurposeElement.length > 50) {
            alert("目的の文字数は最大で全角50文字です。");
            return false;
        }

        if (txtSummaryElement != "" && txtSummaryElement.length > 50) {
            alert("過去トラ確認結果まとめの文字数は最大で全角120文字です。");
            return false;
        }
                
        return true;
    }

    
    function openSubWindowMitakaDetail() {
        // 1. 画面のオープン
        //var url = "frmMitakaDetail.aspx";
        //var w = (screen.width - 1340) / 2;
        //var h = (screen.height - 700) / 2;
        //var features = "menubar=no,toolbar=no,location=no,resizable=no,scrollbars=yes,status=no,height=700,width=1340,left=" + w + ",top=" + h;
        //var frmWatchInstRegistFlg = window.open(url, "frmMitakaRegist", features);

        if($("#HdnManageNo").val() == "")
        {

            if (confirm("過去トラ観たか登録を行います。") == true) {
                var url = "frmMitakaDetailCreater.aspx?ManageNo=Test";
                location.href = url;
            }
        }
        else
        {
            if (confirm("過去トラ観たか更新を行います。") == true) {
                var url = "frmMitakaDetailCreater.aspx?ManageNo=Test";
                location.href = url;
            }
        }
    }
    function openSubWindowUserSearch(target) {
        // 1. 画面のオープン
        var url = "frmUserSearch.aspx?target=" +target;
        var w = (screen.width - 1340) / 2;
        var h = (screen.height - 700) / 2;
        var features = "menubar=no,toolbar=no,location=no,resizable=no,scrollbars=yes,status=no,height=700,width=1340,left=" + w + ",top=" + h;
        var frmUserSearchFlg = window.open(url, "FormfrmUserSearch", features);
    }


    //function openDialogUserSelect() {
        //var timestamp = new Date().getTime();

        //// 1. 画面のオープン
        //var url = "frmUserSearch.aspx?" + timestamp;

        //var w = "1340px";
        //var h = "700px";
        //var features = "dialogWidth=" + w + ";dialogHeight=\
        //        " + h + ";center=1;status=1;scroll=1;resizable=1;\
        //        minimize=0;maximize=0;";

        //var returnValue = showModalDialog(url, "FormfrmUserSearch", features);

        // 帰り値が存在する場合（ユーザー選択が行われた場合）
        //if (returnValue != undefined) {

        //}
    //} 
    function openDialogDeploymentTargetSetting() {
        var timestamp = new Date().getTime();

        // 1. 画面のオープン
        var url = "frmDeploymentTargetSetting.aspx?" + timestamp;

        var w = "1340px";
        var h = "700px";
        var features = "dialogWidth=" + w + ";dialogHeight=\
                " + h + ";center=1;status=1;scroll=1;resizable=1;\
                minimize=0;maximize=0;";

        var returnValue = showModalDialog(url, window, features);

        // 帰り値が存在する場合（展開対象設定が行われた場合）
        if (returnValue != undefined) {

        }
    }
    

</script>
</html>
