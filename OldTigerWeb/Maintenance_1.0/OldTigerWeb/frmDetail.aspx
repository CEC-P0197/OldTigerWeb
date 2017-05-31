<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmDetail.aspx.cs" Inherits="OldTigerWeb.frmDetail" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>過去トラ項目詳細</title>
    <link rel="stylesheet" href= "Content/OldTiger.css" />
</head>
<body>
    <form name="frmDetail" runat="server">
    <div style="width:100%">
        <table style="width:100%">
            <tr>
                <td class="midasi" style="padding-left :70px ">過去トラ項目詳細</td>
                <td style="text-align:right;width :100px"> 
                   <asp:Button ID="Button1" runat="server" Text="閉じる" Height="30px" Width="100px" OnClientClick="window.close();" />
                </td>
            </tr>
        </table>
    </div>
    <div id="DetailDiv" class="DetailDivStyle" runat="server">
        <table class="DetailTableStyle">
            <tr>
                <th>項目管理No</th>
                <td>
                    <asp:Label ID="lblKOUMOKU_KANRI_NO" runat="server" Text="" /></td>
                <th>フォロー状況</th>
                <td>
                    <asp:Label ID="lblFOLLOW_INFO" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>項目</th>
                <td colspan="3">
                    <asp:Label ID="lblKOUMOKU" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>FMC</th>
                <td>
                    <asp:Label ID="lblFUGO_NO" runat="server" Text="" /></td>
                <th>仕向地</th>
                <td>
                    <asp:Label ID="lblSIMUKECHI_NAME" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>現象（内容）</th>
                <td colspan="3">
                    <asp:Label ID="lblGENSYO_NAIYO" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>状況</th>
                <td colspan="3">
                    <asp:Label ID="lblJYOUKYO" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>原因</th>
                <td colspan="3">
                    <asp:Label ID="lblGENIN" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>対策</th>
                <td colspan="3">
                    <asp:Label ID="lblTAISAKU" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th colspan="2">開発時の流出要因</th>
                <th colspan="2">確認の観点</th>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblKAIHATU_MIHAKKEN_RIYU" runat="server" Text="" /></td>
                <td colspan="2">
                    <asp:Label ID="lblSQB_KANTEN" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th colspan="2">再発防止策（設計面）</th>
                <th colspan="2">再発防止策（評価面）</th>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblSAIHATU_SEKKEI" runat="server" Text="" /></td>
                <td colspan="2">
                    <asp:Label ID="lblSAIHATU_HYOUKA" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>重要度ランク</th>
                <td>
                    <asp:Label ID="lblRANK" runat="server" Text="" /></td>
                <th>RSC項目</th>
                <td>
                    <asp:Label ID="lblRSC" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>システム(1)</th>
                <td>
                    <asp:Label ID="lblSYSTEM_NAME1" runat="server" Text="" /></td>
                <th>現象</th>
                <td>
                    <asp:Label ID="lblBUNRUI_GENSYO_NAME" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>部品(1)</th>
                <td>
                    <asp:Label ID="lblBUHIN_NAME1" runat="server" Text="" /></td>
                <th>原因</th>
                <td>
                    <asp:Label ID="lblBUNRUI_CASE_NAME" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>子部品(1)</th>
                <td>
                    <asp:Label ID="lblKOBUHIN_NAME1" runat="server" Text="" /></td>
                <th>PUBY区分</th>
                <td>
                    <asp:Label ID="lblBY_PU" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>システム(2)</th>
                <td>
                    <asp:Label ID="lblSYSTEM_NAME2" runat="server" Text="" /></td>
                <th>制御ユニット名称</th>
                <td>
                    <asp:Label ID="lblSEIGYO_UNIT_NAME" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>部品(2)</th>
                <td>
                    <asp:Label ID="lblBUHIN_NAME2" runat="server" Text="" /></td>
                <th>制御系現象</th>
                <td>
                    <asp:Label ID="lblSEIGYO_GENSYO_NAME" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>子部品(2)</th>
                <td>
                    <asp:Label ID="lblKOBUHIN_NAME2" runat="server" Text="" /></td>
                <th>制御系要因</th>
                <td>
                    <asp:Label ID="lblSEIGYO_FACTOR_NAME" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>BLK No</th>
                <td>
                    <asp:Label ID="lblBLKNO" runat="server" Text="" /></td>
                <th>車型/特殊</th>
                <td>
                    <asp:Label ID="lblKATA_NAME" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>部品番号(上5ｹﾀ)</th>
                <td>
                    <asp:Label ID="lblBUHIN_BANGO" runat="server" Text="" /></td>
                <th>重保/法規</th>
                <td>
                    <asp:Label ID="lblJYUYO_HOUKI" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>EGTM形式</th>
                <td>
                    <asp:Label ID="lblEGTM_NAME" runat="server" Text="" /></td>
                <th>外製</th>
                <td>
                    <asp:Label ID="lblSYUMU_GAISEI" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>排気量</th>
                <td>
                    <asp:Label ID="lblHAIKI_NAME" runat="server" Text="" /></td>
                <th>製造</th>
                <td>
                    <asp:Label ID="lblSYUMU_SEIZO" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>設計</th>
                <td colspan="3">
                    <asp:Label ID="lblBUSYO_SEKKEI" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>評価</th>
                <td colspan="3">
                    <asp:Label ID="lblBUSYO_HYOUKA" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>設通No</th>
                <td colspan="3">
                    <% 
                        if (ecsList != null && ecsList.Count() != 0)
                        {

                            for (int i = 0; i < ecsList.Count(); i++)
                            {
                                if (i != 0)
                                {%>
                                        ,
                                    <% } %>

                    <a href="javascript:DisplayEcsWindow('<%= ecsList[i]%>')"><%= ecsList[i]%></a>
                    <%  
                            }
                        }
                    %>
                </td>
            </tr>
            <tr>
                <th>資料No</th>
                <td colspan="3">
                    <asp:Label ID="lblSIRYOU_NO" runat="server" Text="" />
                </td>
            </tr>
            <tr>
                <th>関連資料</th>
                <td colspan="3">
                    <% 
                        if (kanrenSiryoName != null)
                        { 
                            for (int i = 0; i < kanrenSiryoName.Length; i++)
                            {
                                string [] siryoName = kanrenSiryoName[i].Split('\\');
                                
                                if (i != 0)
                                {%>
                                    ,
                                <% } %>
                                <a href="javascript:DisplayForder('<%= kanrenSiryo[i]%>')"><%= siryoName[siryoName.Length-1].Replace(".pdf","") %></a>
    
                    <%  
                                if (i == kanrenSiryo.Length-1)
                                {
                                    break;
                                }
                            }
                        }
                    %>
                </td>
            </tr>
            <tr>
                <th>関連項目管理No</th>
                <td colspan="3">
                    <asp:Label ID="lblKANREN_KANRI_NO" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>キーワード</th>
                <td colspan="3">
                    <asp:Label ID="lblKEYWORD" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>Reliability</th>
                <td colspan="3">
                    <asp:Label ID="lblRELIABILITY" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>ｸﾚｰﾑ費/TOP40</th>
                <td>
                    <asp:Label ID="lblKRAME" runat="server" Text="" /></td>
                <th>リプロ/TOP20</th>
                <td>
                    <asp:Label ID="lblRIPRO" runat="server" Text="" /></td>
            </tr>
        </table>
    </div>
        <asp:HiddenField runat="server" ID="hdnKaknriNo" Value ="" />
        <asp:HiddenField runat="server" ID="hdnFolderPath" Value ="<%= kanrenSiryoPath%>" />

            <%--外部JS読込み--%>
            <script type="text/javascript" src="Scripts/jscommon.js"></script>
            <script type="text/javascript">

                // --------------------------------------------------
                // 関数名   : DisplayForder
                // 概要     : フォルダを表示する
                // 引数     : strUrl     : フォルダパス
                // 戻り値   : なし
                // --------------------------------------------------
                function DisplayForder(file) {
                    try {
                        var url = "./frmDocument.aspx?fileNo=" + file;
                        var w = (screen.width - 780) / 2;   // 2017.04.10 ta_kanda サイズ変更
                        //var w = (screen.width - 1200) / 2;
                        var h = (screen.height - 690) / 2;
                        var features = "menubar=no,toolbar=no,location=no,resizable=no,scrollbars=no,status=no,height=690,width=780,left=" + w + ",top=" + h;
                        //var windowStatus = 'toolbar=0,loction=0,directories=0,center=1,status=1,menubar=0,scrollbars=1,resizable=1,width=1200px,height=900px';
                        //window.open('./frmDocument.aspx?fileNo=' + file, 'frmDocument', windowStatus);
                        var returnFlg = window.open(url, 'frmDocument', features);
                    }
                    catch (e) {
                        //alert("未存在 " + url);
                    }
                }
                // --------------------------------------------------
                // 関数名   : DisplayEcsWindow
                // 概要     : 設通SYS呼出し用の図面改訂画面を表示する
                // 引数     : strUrl     : フォルダパス
                // 戻り値   : なし
                // --------------------------------------------------
                function DisplayEcsWindow(ecsNo) {
                    // モーダルウィンドウで開く 
                    var url = "frmEcsSelectWindow?EcsNo=" + ecsNo;

                    var date = Date.now;
                    var winWidth = "450px";
                    var winHeight = "500px";
                    var options = "dialogWidth=" + winWidth + ";" +
                                  "dialogHeight=" + winHeight + ";" +
                                  "center=1;" +
                                  "status=1;" +
                                  "scroll=0;" +
                                  "resizable=1;" +
                                  "minimize=0;" +
                                  "maximize=0;" + date;

                    // 図面選択サブウィンドウより図面番号取得
                    var result = showModalDialog(url, window, options);

                    // 設通サブシステムオープン
                    if (result != undefined && result.length > 0) {
                        var windowStatus =
                            'toolbar=0,loction=0,directories=0,status=1,menubar=0,scrollbars=1,resizable=1,width=1200px,height=1000px';
                        window.open('http://<%=_ecsSubSysPath%>' + result, windowStatus);
                    }
                }
    </script>

    </form>
</body>
</html>
