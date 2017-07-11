<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmFollow.aspx.cs" Inherits="OldTigerWeb.frmFollow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>ＦＭＣ・ｍｃ進捗</title>

    <script type="text/javascript" src="Scripts/jscommon.js"></script>
    <link rel="stylesheet" href= "Content/OldTiger.css" />

    <style type="text/css">
        h4 { /* h1～h6を指定 http://www.aoiweb.com/aoi2/title_bar4.htm */
            font-size: 24px; /* 文字の大きさ */
            width: 741px; /* 幅 */
            height: 25px; /* 高さ */
            padding: 3px 0px 0px 40px; /* ボックスの内側[上 右 下 左]の余白 */
            margin: 0px 0px 5px 0px; /* ボックスの外側[上 右 下 左]の余白 */
            /*color: #0073a8;*/ /* 文字の色 */
            color: gray ; /* 文字の色を黒に変更 2017.03.29 神田 */
        }
    </style>
</head>
<body background="./Images/BackImage.gif">

    <div style="height: 640px; width: 1300px">

    <%--<h4 class="auto-style7" >　　　　　　　過去トラシステム</h4>--%>
    <h4 class="auto-style7" >過去トラシステム</h4>

    <form id="frmFollow" runat="server">
    <table align="center" valign="top" style="width :100%; padding-left :40px">
      <tbody>
        <tr>
            <td>
                <asp:LinkButton id="lnkTop" Text="TOPページ" OnClick="lnkTop_Click" runat="server"/>
            </td>
		    <%--<td width="45%" class="midasi">ＦＭＣ・ｍｃ進捗</td>--%>
		    <td class="midasi" style ="width:50%">ＦＭＣ・ｍｃ進捗</td>
            <td>
            <%--<td width="10%"  align="right" class="moji24">--%>
            <% if ( (String)ViewState["HELP"] != "" )
               { %>   
                <%--<A href="javascript:DisplayForder('<%=(String)ViewState["HELP"]%>')">マニュアル・Ｑ＆Ａ</A>--%>
                <a href="##" onclick="helpFileViewOpen('HELP');" style ="padding-right:10px">マニュアル</a>
            <%
                }
               else
               { %>
                  マニュアル
            <% 
               }
                 %>

            <% if ( (String)ViewState["QA"] != "" )
               { %>   
                <a href="##" onclick="helpFileViewOpen('QA');" style ="padding-right:10px">Ｑ＆Ａ</a>
            <%
                }
               else
               { %>
                  Ｑ＆Ａ
            <% 
               }
                 %>

            <%--</td>--%>
            <%--<td style ="text-align:center;">--%>
                <a href="mailto:<%= (String)ViewState["MailAddr"] %>">&nbsp;&nbsp;問合せ・ご要望</a>
            </td>
            <%--            <td width="15%" align="right" Class="moji24"><asp:LinkButton id="lnkTop" Text="TOPページへ" OnClick="lnkTop_Click" runat="server"/></td>--%>
            <%--<td width="10%" align="right" Class="moji24"></td>--%>
            <td>
            </td>
        </tr>
        </tbody>
    </table>

    <table align="center" valign="top" width="80%" >
    <tbody>
      <tr>
        <td valign="top"  align="center" width="12%" Class="moji18">フォロー中　</td>
        <td width="60%">
            <table align="center" valign="top" width="100%" style="border:1px solid #000080;">
            <tbody>
              <tr valign="top">
                  <td width="15%" Class="moji18" align="center">ＦＭＣ</td>
                  <td>
                    <div width="65%" class="follow" >
                        <asp:CheckBoxList id="ckBoxFmc" runat="server" DataTextField="FOLLOW_NAME" DataValueField="EVENT_CODE">
                        </asp:CheckBoxList>
                    </div>
                  </td>
                </tr>
                <tr valign="top">
                  <td width="15%" Class="moji18" align="center">ｍｃ</td>
                  <td width="65%">
                    <div class="follow" >
                        <asp:CheckBoxList id="ckBoxmc" runat="server" DataTextField="FOLLOW_NAME" DataValueField="EVENT_CODE">
                        </asp:CheckBoxList>
                    </div>
                  </td>
                </tr>
            </tbody>
            </table>
        </td>
        <td valign="bottom" width="20%">
          <table align="left">
            <tbody>
              <tr>
                <td><asp:Button ID="btn_Kaito" runat="server" Width="120px" Height="32px" Text="回答" Style="font-size: 16pt" OnClick="btn_Kaito_Click" OnClientClick="return SelectCheck('1');" /></td>
             </tr>
            </tbody>
          </table>

      </tr>

      <tr>
        <td colspan="3"></td>
      </tr>

      <tr>
        <td valign="top" align="center" width="12%" Class="moji18">フォロー終了</td>
        <td width="60%">
            <table align="center" valign="top" width="100%" style="border:1px solid #000080;">
            <tbody>
              <tr valign="top">
                  <td width="15%" align="center" Class="moji18">ＦＭＣ</td>
                  <td>
                    <div width="65%" class="follow">
                        <asp:CheckBoxList id="ckBoxOverFmc" runat="server" DataTextField="FOLLOW_NAME" DataValueField="EVENT_CODE">
                        </asp:CheckBoxList>
                    </div>
                  </td>
                </tr>
                <tr valign="top">
                  <td width="15%" align="center" Class="moji18">ｍｃ</td>
                  <td width="65%">
                    <div class="follow" >
                        <asp:CheckBoxList id="ckBoxOvermc" runat="server" DataTextField="FOLLOW_NAME" DataValueField="EVENT_CODE">
                        </asp:CheckBoxList>
                    </div>
                  </td>
                </tr>
            </tbody>
            </table>
        </td>
        <td valign="bottom" width="20%">
          <table align="left">
            <tbody>
             <tr>
               <td Class="komoji">&nbsp;</td>
             </tr>
             <tr>
                <td><asp:Button ID="btn_Download" runat="server" Width="120px" Height="32px" Text="EXCELダウンロード" OnClick="btn_Download_Click" OnClientClick="return SelectCheck('2');" /></td>
             </tr>
             <tr>
               <td Class="komoji">EXCELダウンロードの</td>
             </tr>
             <tr>
               <td Class="komoji">フォロー対象部署指定：</td>
             </tr>
             <tr>
               <td><asp:TextBox ID="txtKacode" runat="server" Width="100px" Height="20px" MaxLength="10" /></td>
             </tr>
             <tr>
               <td Class="komoji">ALL：全部署指定</td>
             </tr>
             <tr>
               <td Class="komoji">以外は個別部署</td>
             </tr>
            </tbody>
          </table>
        </td>
      </tr>
    </tbody>
    </table>
        <input type="button" id="btnOpenWindow" style="visibility:hidden" onclick="openWindowLoading();" />
        <input type="button" id="btnCloseWindow" style="visibility:hidden" onclick="closeWindowLoading();" />
    </div>
        <script src="Scripts/jquery-1.8.2.js"></script>
        <script src="Scripts/jquery-ui-1.8.24.js"></script>
        <script src="Scripts/loading.js"></script>
        <script type="text/javascript">
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
            // 関数名   : SelectCheck
            // 概要     : ＦＭＣ／ｍｃ選択チェック
            // 引数     : mode 1:回答 2:EXCELダウンロード
            // 戻り値   : true、false
            // --------------------------------------------------
           function SelectCheck(mode) {
               var iMode1 = 0;
               var iFound = 0;
               var rc = false;

               var chkFmc     = document.getElementById("<%= ckBoxFmc.ClientID %>");
               var chkMc      = document.getElementById("<%= ckBoxmc.ClientID %>");
               var chkOverFmc = document.getElementById("<%= ckBoxOverFmc.ClientID %>");
               var chkOverMc  = document.getElementById("<%= ckBoxOvermc.ClientID %>");
               var txtKacode  = document.getElementById("<%= txtKacode.ClientID %>").value;

               var txtKacode  =
               //if (txtKacode.trim() == "" && mode == "2") {
               txtKacode  = $.trim(txtKacode);
               if (txtKacode == ""  && mode == "2") {
                   alert("ダウンロードするフォロー対象部署を指定して下さい。");
                   document.getElementById("<%= txtKacode.ClientID %>").focus();
                   return rc;
               }

               if (chkFmc != null) {
                   var a = chkFmc.getElementsByTagName("input");

                   for (var i = 0; i < a.length; i++) {
                       var e = a[i];
                       if (e.type != "checkbox") {
                           continue;
                       }

                       if (e.checked) {
                           iMode1++;
                           iFound++;
                           if (iFound == 2) {
                               break;
                           }
                       }
                   }
               }

               if (iFound != 2)
               {
                   if (chkMc != null) {
                       var b = chkMc.getElementsByTagName("input");

                       for (var i = 0; i < b.length; i++) {
                           var f = b[i];
                           if (f.type != "checkbox") {
                               continue;
                           }

                           if (f.checked) {
                               iMode1++;
                               iFound++;
                               if (iFound > 2) {
                                   break;
                               }
                           }
                       }
                   }
                }

               if (iFound != 2) {
                   if (chkOverFmc != null) {
                       var c = chkOverFmc.getElementsByTagName("input");

                       for (var i = 0; i < c.length; i++) {
                           var g = c[i];
                           if (g.type != "checkbox") {
                               continue;
                           }

                           if (g.checked) {
                               iFound++;
                               if (iFound > 2) {
                                   break;
                               }
                           }
                       }
                   }
               }

               if (iFound != 2) {
                   if (chkOverMc != null) {
                       var d = chkOverMc.getElementsByTagName("input");

                       for (var i = 0; i < d.length; i++) {
                           var h = d[i];
                           if (h.type != "checkbox") {
                               continue;
                           }

                           if (h.checked) {
                               iFound++;
                               if (iFound > 2) {
                                   break;
                               }
                           }
                       }
                   }
               }

                // チェックボックス選択チェック
                if (mode == "1") {      // イベント期間内・回答
                    if (iMode1 == 0) {
                        alert("回答するイベントを選択して下さい。");
                        return rc;
                    }

                    if (iMode1 > 1) {
                        alert("回答するイベントが複数選択されています。");
                        return rc;
                    }
                }
                else
                {                       // イベント期間内/外・ダウンロード
                    if (iFound == 0) {
                        alert("ダウンロードするイベントを選択して下さい。");
                        return rc;
                    }

                    if (iFound > 1) {
                        alert("ダウンロードするイベントが複数選択されています。");
                        return rc;
                    }
                }

                return true;
            }
           // --------------------------------------------------
           // 関数名   : helpオープン
           // 概要     : ＨＥＬＰファイル
           // 引数     : strUrl     : フォルダパス
           // 戻り値   : なし
           // --------------------------------------------------
           function helpFileViewOpen(kbn) {
               try {
                   url=""
                   if (kbn == "HELP") {
                       var url = "./frmClientView.aspx?VIEWFILE_KBN=" + "HelpFollow";
                   }
                   else {
                       var url = "./frmClientView.aspx?VIEWFILE_KBN=" + "QaFollow";
                   }
                   var w = (screen.width - 780) / 2;   // 2017.04.10 ta_kanda サイズ変更
                   //var w = (screen.width - 1200) / 2;
                   var h = (screen.height - 690) / 2;
                   var features = "menubar=no,toolbar=no,location=no,resizable=no,scrollbars=no,status=no,height=690,width=780,left=" + w + ",top=" + h;
                   var returnFlg = window.open(url, 'frmClientView', features);
               }
               catch (e) {
                   //alert("未存在 " + url);
               }
           }
        </script>

    </form>
</body>
</html>
