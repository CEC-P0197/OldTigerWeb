<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmAnswer.aspx.cs" Inherits="OldTigerWeb.frmAnswer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=Edge" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>ＦＭＣ・ｍｃフォロー情報回答</title>

    <link rel="stylesheet" href= "Content/OldTiger.css" />
    <%--外部JS読込み--%>
    <script type="text/javascript" src="Scripts/jscommon.js"></script>
    <script src="Scripts/jquery-1.8.2.js"></script>
    <script src="Scripts/jquery-ui-1.8.24.js"></script>

    <script type="text/javascript">
        // --------------------------------------------------
        // 関数名   : DisplayForder
        // 概要     : フォルダを表示する
        // 引数     : strUrl     : フォルダパス
        // 戻り値   : なし
        // --------------------------------------------------
        function DisplayForder(url) {
            try {
                var url = "./frmDocument.aspx?fileNo=" + file;
                var w = (screen.width - 1200) / 2;
                var h = (screen.height - 690) / 2;
                var features = "menubar=no,toolbar=no,location=no,resizable=no,scrollbars=no,status=no,height=690,width=1200,left=" + w + ",top=" + h;
                //var windowStatus = 'toolbar=0,loction=0,directories=0,center=1,status=1,menubar=0,scrollbars=1,resizable=1,width=1200px,height=900px';
                //window.open('./frmDocument.aspx?fileNo=' + file, 'frmDocument', windowStatus);
                var returnFlg = window.open(url, 'frmDocument', features);
            }
            catch (e) {
                //alert("未存在 " + url);
            }
            //try {
            //    window.open(url);
            //}
            //catch (e) {
            //    //alert("未存在 " + url);
            //}
        }

        // --------------------------------------------------
        // 関数名   : confRegistCheck
        // 概要     : 登録チェック
        // 引数     : なし
        // 戻り値   : true、false
        // --------------------------------------------------
        function confRegistCheck() {
            var rdoSindo1 = document.getElementById("<%= rdoSindo1.ClientID %>");
            var rdoSindo2 = document.getElementById("<%= rdoSindo2.ClientID %>");
            var rdoSindo3 = document.getElementById("<%= rdoSindo3.ClientID %>");
            var rdoSindo4 = document.getElementById("<%= rdoSindo4.ClientID %>");
            var txtAnswer = document.getElementById("<%= txtAnswer.ClientID %>").value;
            var rc = false;

            if (!(rdoSindo1.checked || rdoSindo2.checked || rdoSindo3.checked || rdoSindo4.checked))
            {
                window.alert('進度を選択して下さい。');
                document.getElementById("rdoSindo1").focus();
                return rc;
            }

            // 回答内容未入力はエラー
            txtAnswer = $.trim(txtAnswer);
            if (txtAnswer == "") {
                alert("回答内容が未入力です。");
                document.getElementById("txtAnswer").focus();
                return rc;
            }

            return true;
        }

        // --------------------------------------------------
        // 関数名   : changeInput
        // 概要     : 入力変更マークON
        // 引数     : なし
        // 戻り値   : true、false
        // --------------------------------------------------
        //function changeInput($this) {
        //    edit_flg = 1;
        //}

        // --------------------------------------------------
        // 関数名   : confClose
        // 概要     : 画面クローズ
        // 引数     : 更新有無
        // --------------------------------------------------
        function confClose(flg) {
            window.returnValue = flg;
            self.close();
        }

        // --------------------------------------------------
        // 関数名   : openAnswerSubWindow
        // 概要     : 他部署回答一覧表示
        // 引数     : なし
        // 戻り値   : true、false
        // --------------------------------------------------
        function openAnswerSubWindow() {

            // 1. 画面のオープン
            var url = "frmAnswerSubWindow.aspx?FMCMC=" + document.getElementById('hdnFmcMc').value
            　　　　　+ "&KAIHATSUID=" + document.getElementById('hdnKaihatsuId').value
                      + "&BYPU=" + document.getElementById('hdnByPu').value
                      + "&EVENTNO=" + document.getElementById('hdnEventNo').value
                      + "&FOLLOWNO=" + document.getElementById('hdnFollowNo').value
                      + "&KACODE=" + document.getElementById('hdnKaCode').value
                      + "&SYSTEMNO=" + document.getElementById('hdnSystemNo').value;

            //var features = "dialogWidth=830px; dialogHeight=600px;status=no;scroll=yes;center=yes;resizable=yes;";
            var features = "dialogWidth=780px; dialogHeight=600px;status=no;scroll=yes;center=yes;resizable=yes;";
            //var features = "width=800px; height=650px";
            var returnFlg = window.showModelessDialog(url, this, features);
        }
    </script>

<base target="_self" />
</head>

<body style="overflow-x:hidden">


    <form id="frmAnswer" runat="server">
    
    <p class="td_data24" style ="color:#000080">　　　　　　　　　　　　　　　　ＦＭＣ・ｍｃフォロー情報回答　　　　　　　　
        <asp:Button ID="btnRegist" runat="server" text="登録"   Height="30px" Width="100px" 
            OnClientClick="if (confRegistCheck() == false) { return false;} else { return true;}" OnClick="btnRegist_Click" /></p>
    <p class="moji11">　</p>
    <p class="moji16">　　<asp:Label ID="lblFollowInfo" runat="server" Text="" />　　　フォロー対象課：<asp:Label ID="lblKacode" runat="server" Text="" /></p>
            
      <asp:Panel ID="pnlDetail" runat="server">

        <table align="center" valign="top" width="700px" class="syosaiTable">
          <tbody>
            <tr>
                <th width="110"  height="20">ヒヤリング要望</th>
                <td width="600" height="20"><asp:Label ID="lblHearing" runat="server" Text="" Width="15px"  /></td>
            </tr>
            <tr>
                <th width="110"  height="20">進度</th>
                <td width="600" height="20">
                    <asp:RadioButton ID="rdoSindo1" runat="server" GroupName="Sindo" Text="織込済" />
                    <asp:RadioButton ID="rdoSindo2" runat="server" GroupName="Sindo" Text="検討･調整中" />
                    <asp:RadioButton ID="rdoSindo3" runat="server" GroupName="Sindo" Text="未確認" />
                    <asp:RadioButton ID="rdoSindo4" runat="server" GroupName="Sindo" Text="適用外（理由を対応内容に記載し登録）" />
                </td>
            </tr>
            <tr>
                <th width="110">回答内容<input type="button" name="btnOtherDept" value="他部署回答" style="height:30px;width:90px" onclick="openAnswerSubWindow()" /> 
                </th>
                <td width="600" height="100"><asp:TextBox ID="txtAnswer" runat="server" Text="" TextMode="MultiLine" style="ime-mode: active" Width="600px" Height="100px" MaxLength="1000" /></td>
            </tr>
          </tbody>
        </table>
        <p align="right"></p>

        <table align="center" valign="top" width="700px" class="syosaiTable">
          <tbody>
            <tr>
                <th width="110">項目管理No</th>
                <td width="225"><asp:Label ID="lblKOUMOKU_KANRI_NO" runat="server" Text="" /></td>
                <th width="110">フォロー状況</th>
                <td width="225"><asp:Label ID="lblFOLLOW_INFO" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">項目</th>
                <td width="560" colspan="3"><asp:Label ID="lblKOUMOKU" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">FMC</th>
                <td width="225"><asp:Label ID="lblFUGO_NO" runat="server" Text="" /></td>
                <th width="110">仕向地</th>
                <td width="225"><asp:Label ID="lblSIMUKECHI_NAME" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">現象（内容）</th>
                <td width="560" colspan="3"><asp:Label ID="lblGENSYO_NAIYO" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">状況</th>
                <td width="560" colspan="3"><asp:Label ID="lblJYOUKYO" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">原因</th>
                <td width="560" colspan="3"><asp:Label ID="lblGENIN" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">対策</th>
                <td width="560" colspan="3"><asp:Label ID="lblTAISAKU" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="335" colspan="2">開発時の流出要因</th>
                <th width="335" colspan="2">確認の観点</th>
            </tr>
            <tr>
                <td width="335" colspan="2"><asp:Label ID="lblKAIHATU_MIHAKKEN_RIYU" runat="server" Text="" /></td>
                <td width="335" colspan="2"><asp:Label ID="lblSQB_KANTEN" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="335" colspan="2">再発防止策（設計面）</th>
                <th width="335" colspan="2">再発防止策（評価面）</th>
            </tr>
            <tr>
                <td width="335" colspan="2"><asp:Label ID="lblSAIHATU_SEKKEI" runat="server" Text="" /></td>
                <td width="335" colspan="2"><asp:Label ID="lblSAIHATU_HYOUKA" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">重要度ランク</th>
                <td width="225"><asp:Label ID="lblRANK" runat="server" Text="" /></td>
                <th width="110">RSC項目</th>
                <td width="225"><asp:Label ID="lblRSC" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">システム(1)</th>
                <td width="225"><asp:Label ID="lblSYSTEM_NAME1" runat="server" Text="" /></td>
                <th width="110">現象</th>
                <td width="225"><asp:Label ID="lblBUNRUI_GENSYO_NAME" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">部品(1)</th>
                <td width="225"><asp:Label ID="lblBUHIN_NAME1" runat="server" Text="" /></td>
                <th width="110">原因</th>
                <td width="225"><asp:Label ID="lblBUNRUI_CASE_NAME" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">子部品(1)</th>
                <td width="225"><asp:Label ID="lblKOBUHIN_NAME1" runat="server" Text="" /></td>
                <th width="110">PUBY区分</th>
                <td width="225"><asp:Label ID="lblBY_PU" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">システム(2)</th>
                <td width="225"><asp:Label ID="lblSYSTEM_NAME2" runat="server" Text="" /></td>
                <th width="110">制御ユニット名称</th>
                <td width="225"><asp:Label ID="lblSEIGYO_UNIT_NAME" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">部品(2)</th>
                <td width="225"><asp:Label ID="lblBUHIN_NAME2" runat="server" Text="" /></td>
                <th width="110">制御系現象</th>
                <td width="225"><asp:Label ID="lblSEIGYO_GENSYO_NAME" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">子部品(2)</th>
                <td width="225"><asp:Label ID="lblKOBUHIN_NAME2" runat="server" Text="" /></td>
                <th width="110">制御系要因</th>
                <td width="225"><asp:Label ID="lblSEIGYO_FACTOR_NAME" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">BLK No</th>
                <td width="225"><asp:Label ID="lblBLKNO" runat="server" Text="" /></td>
                <th width="110">車型/特殊</th>
                <td width="225"><asp:Label ID="lblKATA_NAME" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">部品番号(上5ｹﾀ)</th>
                <td width="225"><asp:Label ID="lblBUHIN_BANGO" runat="server" Text="" /></td>
                <th width="110">重保/法規</th>
                <td width="225"><asp:Label ID="lblJYUYO_HOUKI" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">EGTM形式</th>
                <td width="225"><asp:Label ID="lblEGTM_NAME" runat="server" Text="" /></td>
                <th width="110">外製</th>
                <td width="225"><asp:Label ID="lblSYUMU_GAISEI" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">排気量</th>
                <td width="225"><asp:Label ID="lblHAIKI_NAME" runat="server" Text="" /></td>
                <th width="110">製造</th>
                <td width="225"><asp:Label ID="lblSYUMU_SEIZO" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">設計</th>
                <td width="560" colspan="3"><asp:Label ID="lblBUSYO_SEKKEI" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">評価</th>
                <td width="560" colspan="3"><asp:Label ID="lblBUSYO_HYOUKA" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">設通No</th>
                <td width="560" colspan="3"><asp:Label ID="lblSETTU_NO" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">資料No</th>
                <td width="560" colspan="3"><asp:Label ID="lblSIRYOU_NO" runat="server" Text="" /></td>
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
                                <%--<a href="javascript:DisplayForder('<%= kanrenSiryo[i]%>')"><%= siryoName[siryoName.Length-1].Replace(".pdf","") %></a>--%>
                                <%= siryoName[siryoName.Length-1].Replace(".pdf","") %>
    
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
                <th width="110">関連項目管理No</th>
                <td width="560" colspan="3"><asp:Label ID="lblKANREN_KANRI_NO" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">キーワード</th>
                <td width="560" colspan="3"><asp:Label ID="lblKEYWORD" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">Reliability</th>
                <td width="560" colspan="3"><asp:Label ID="lblRELIABILITY" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th width="110">ｸﾚｰﾑ費/TOP40</th>
                <td width="225"><asp:Label ID="lblKRAME" runat="server" Text="" /></td>
                <th width="110">リプロ/TOP20</th>
                <td width="225"><asp:Label ID="lblRIPRO" runat="server" Text="" /></td>
            </tr>
            </tbody>
        </table>
      </asp:Panel>
        <asp:HiddenField runat="server" ID ="hdnFmcMc"/>
        <asp:HiddenField runat="server" ID ="hdnKaihatsuId"/>
        <asp:HiddenField runat="server" ID ="hdnByPu"/>
        <asp:HiddenField runat="server" ID ="hdnEventNo"/>
        <asp:HiddenField runat="server" ID ="hdnFollowNo"/>
        <asp:HiddenField runat="server" ID ="hdnKaCode"/>
        <asp:HiddenField runat="server" ID ="hdnSystemNo"/>

    </form>
</body>
</html>
