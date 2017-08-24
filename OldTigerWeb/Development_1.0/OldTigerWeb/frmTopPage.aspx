<%@ Page Language="C#" validateRequest="false" AutoEventWireup="true" CodeBehind="frmTopPage.aspx.cs" Inherits="OldTigerWeb.frmTopPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="0" />

    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>


    

    <%--<link rel="stylesheet" type="text/css" href="./style.css?var=20120829"/>--%>
    <title>ＴＯＰページ</title>
    <link rel="stylesheet" href= "Content/OldTiger.css" />
    <link rel="stylesheet" href= "Content/themes/base/jquery-ui.css"/>
    <link rel="stylesheet" href="Scripts/media/css/jquery.dataTables.css" />

    <style type="text/css">
        
       h4 { /* h1～h6を指定 http://www.aoiweb.com/aoi2/title_bar4.htm */
        font-size: 24px; /* 文字の大きさ */
        width : 741px; /* 幅 */
        height : 25px; /* 高さ */
        padding:3px 0px 0px 40px; /* ボックスの内側[上 右 下 左]の余白 */
        margin :0px 0px 5px 0px; /* ボックスの外側[上 右 下 左]の余白 */
        /*color: #0073a8;*/ /* 文字の色 */
        color: grey; /* 文字の色を黒⇒グレーに変更 2017.03.29 神田 */
        }      
        .center {
            text-align:center;
        }
        /*2017/02/12 takeshi.kanda 追加*/
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

    </style>

    <%--外部JS読込み--%>
    <script type="text/javascript" src="Scripts/jscommon.js"></script>

    <%--<script type="text/javascript" src="./javascript/menuCode.js?var=20120829"></script>--%>
    
    <base target="_self" />

    <script>
            window.onload = function () {
            $(function () {
                if (document.getElementById("hdnUserInfo").value != "0") {
                    dts('#tableCommon');
                    dts('#tableTopic');
                    dts('#tableSample');
                    $("#tabsDiv").fadeIn();
                }
                if (document.getElementById("hdnSqbFg").value == "1") {
                    $(document.getElementsByName("lnkForm")).fadeIn();
                    document.getElementById("btn_ChangeImage").style.display = "block";

                }

                if (document.getElementById("hdnUserInfo").value == "BY") {
                    document.getElementById("rdTopicTokyo").style.display = "none";
                    document.getElementById("titleTopicTokyo").style.display = "none";
                }
                else
                {
                    document.getElementById("rdTopicGunma").style.display = "none";
                    document.getElementById("titleTopicGunma").style.display = "none";
                }
                document.getElementById("txtTopicTokyo").style.display = "none";
                document.getElementById("txtTopicGunma").style.display = "none";
                <% if ( !disFlgTopicTokyo ) {%>
                document.getElementById("btnTopicTokyo").style.display = "none";
                <% }%>
                <% if ( !disFlgTopicGunma ) {%>
                document.getElementById("btnTopicGunma").style.display = "none";
                <% }%>

            });

            }
      
        function openChangeImage()
        {
            // 子画面サイズ
            var winWidth = "500px";
            var winHeight = "300px";

            // 子画面で開く 
            var url = "frmChangeImage.aspx";

            var options = "dialogWidth=" + winWidth + ";" +
                          "dialogHeight=" + winHeight + ";" +
                          "center=1;" +
                          "status=0;" +
                          "scroll=0;" +
                          "resizable=0  ;" +
                          "minimize=0;" +
                          "maximize=0;";

            var result = showModalDialog(url, window, options);
            document.getElementById("btnShow").style.display = "none";
            
            // 画面表示更新
            if (result == undefined) {
                document.getElementById("btnShow").click();
            }
            return;
          
        }


    </script>
</head>
<%-- <body background="./Images/TOP_Image.png"> --%>

    <body class ="cbody">
        <form id="TopPage" runat="server">
            <div style="width:100%;align-items:flex-end ;">
                <h4 >過去トラシステム</h4>
                <table style="width:100%">
                    <tr>
                    <td style ="text-align:right;">
                        <% if ((String)ViewState["HELP"] != "")
                            { %>
                        <%--<a href="javascript:DisplayForder('<%=(String)ViewState["HELP"]%>')">マニュアル・Ｑ＆Ａ</a>--%>
                        <a href="#" onclick="helpFileViewOpen('HELP');"style ="padding-right:10px">マニュアル</a>
                        <%
                            }
                            else
                            { %>
                                マニュアル
                        <% 
                            }
                        %>
                        <% if ((String)ViewState["QA"] != "")
                            { %>
                        <a href="##" onclick="helpFileViewOpen('QA');"style ="padding-right:10px">Ｑ＆Ａ</a>
                        <%
                            }
                            else
                            { %>
                                Ｑ＆Ａ
                        <% 
                            }
                        %>
                    </td> 
                    </tr>
                    <tr>
                    <tr>
<%--                    <td style ="text-align:right;">
                        <a href="#" Onclick="btn_btnDownloadUser_Click;">ユーザー登録用紙ダウンロード</a>
                    </td></tr>--%>
                    <td class="right" style="float:right;">
                        <asp:Button ID="btnDownloadUser" runat="server" text="新規ユーザー申請" class="buttoncolor" 
                            OnClick="btn_btnDownloadUser_Click" />
                    </td></tr>
                    <tr>
                    <td class="right" style="float:right;">
                        <input id="btn_ChangeImage" type="button" value="背景変更" class="buttoncolor" 
                            style="display:none;text-align :right ; " onclick="openChangeImage();" />
                    </td></tr>
                    <tr>
                    <td class="right" style="float:right;">
                        <asp:Button ID="btnShow" runat="server" text="表示更新" style="display:none;" />
                    </td>                        </tr>
                 </table>
                
                 <div id="btnGroupLink" class="center">
                        <asp:Button ID="btn_Search" runat="server" text="過去トラ検索" 
                            class="buttoncolor textcenter24" Height="50px" Width="250px" OnClick="btn_Search_Click" />
                        <br /><br />
                        <asp:Button ID="btn_Follow" runat="server" text="　ＦＭＣ･ｍｃ進捗　" 
                            class="buttoncolor textcenter24" Height="50px" Width="250px" OnClick="btn_Follow_Click" />
                        <br /><br />
                 </div>
                 <div id ="tabsDiv" runat ="server" style="display:none;clear:both;">
                        <div style="padding:0% 15%;">
                            <div id="divEditTitle" style="display:none;"><b>掲示板編集</b></div>
                            <div id="divSampleForm" style="padding:0px;display:none;background-color:white;border:solid;border-width:1px;border-color:lightgray;">
                                    <div style="width:100%;background-color:lightpink;border:solid;border-width:1px;border-color:lightgray;" class="tableFont"><span style="padding-left:10px;"><b> 掲示板サンプル</b></span></div>
                                    <div id="sampleDiv" runat="server" style="height:100%;" ></div>    
                            </div>
                            <div id="tabs" style="padding:0px;" >
                                <div id="tabs-1" style="padding: 0px;">
                                    <div style="width:50%;float:left;">
                                        <div style="width:100%;background-color:lightgreen;border:solid;border-width:1px;border-color:lightgray;" class="tableFont"><span style="padding-left:10px;"><b>技本共通のお知らせ</b></span></div>
                                        <div id="infoCommon" runat="server" style="background-color:white;border:solid;border-width:1px;border-color:lightgray;" class="bbTable"></div>
                                    </div>
                                    <div style="width:50%;float:right;" >                               
                                        <div style="width:100%;background-color:lightblue;border:solid;border-width:1px;border-color:lightgray;" class="tableFont"><span style="padding-left:10px;"><b id="titleTopics" runat="server">トピックス</b></span></div>    
                                        <div id="infoTopic" runat="server" style="background-color:white;border:solid;border-width:1px;border-color:lightgray;" class="bbTable"></div>
                                    </div>
                                </div>
                            </div>
                           
                            <div style="float:left;">
                                <input name="lnkSample" id="lnkSample" type="button" value="サンプル" style="display:none;" onclick="displaySampleForm();" />
                            </div>
                            <div style="float:right;">
                                <input name="lnkForm" type="button" value="掲示板編集" class="buttoncolor" style="display:none" onclick="displayForm();" />
                                <input id="btn_MitakaRegist" type="button" value="test" class="buttoncolor" 
                                    style="text-align :right ; " onclick="openSubWindowMitakaRegist();" />
                            </div>
                            <br/>
                            <div id="divForm" style="display:none;">    
                                <asp:Panel ID="pnlForm" runat="server" Width="100%" Height="200px">
                                    <table style="width:100%">
                                        <tr class="pnlInputBoxStyle">
                                            <td style="padding-left:10%;">編集する掲示板を選択してください。
                                            </td>
                                        </tr>
                                        <tr class="pnlInputBoxStyle">
                                            <td style="text-align:center">                                                
                                                <input type="radio" id="rdCommon" name="rdGroup" onclick="chengeTxtForm('common');" value="rdCommon" checked/><span id="titleTopicCommon">技本共通</span>
                                                <input type="radio" id="rdTopicTokyo" name="rdGroup" onclick="chengeTxtForm('topicTokyo');" value="rdTopicTokyo" /><span id="titleTopicTokyo">トピックス（東京）</span>
                                                <input type="radio" id="rdTopicGunma" name="rdGroup" onclick="chengeTxtForm('topicGunma');" value="rdTopicGunma"/><span id="titleTopicGunma">トピックス（群馬）</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCommon" runat="server" TextMode="MultiLine" CssClass="TxtInputBoxStyle"  />
                                                <asp:TextBox ID="txtTopicTokyo" runat="server" TextMode="MultiLine" CssClass="TxtInputBoxStyle" />
                                                <asp:TextBox ID="txtTopicGunma" runat="server" TextMode="MultiLine" CssClass="TxtInputBoxStyle" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                    <asp:Button ID="btnCommon" runat="server" Text="更新" OnClientClick="return checkTxt('common');" Height="30px" Width="80px" OnClick="btnCommon_click" />
                                                    <asp:Button ID="btnTopicTokyo" runat="server" Text="更新" OnClientClick="return checkTxt('topicTokyo');" Height="30px" Width="80px" OnClick="btnTopicTokyo_click" />
                                                    <asp:Button ID="btnTopicGunma" runat="server" Text="更新" OnClientClick="return checkTxt('topicGunma');" Height="30px" Width="80px" OnClick="btnTopicGunma_click" />
                                          </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>  
                    </div>           
                </div>
            </div>

            <asp:HiddenField runat="server" ID="hdnTabIndex" Value="0" />
            <asp:HiddenField runat="server" ID="hdnUserInfo" Value="0" />
            <asp:HiddenField runat="server" ID="hdnSqbFg" Value="0" />
            <asp:HiddenField runat="server" ID="hdnPageUrl" Value="" />

            <script src="Scripts/jquery-1.8.2.js"></script>
            <script src="Scripts/jquery-ui-1.8.24.js"></script>
            <script src="Scripts/jquery.anystretch.min.js"></script>
            <script src="Scripts/media/js/jquery.dataTables.min.js"></script>
            <script>
   
      


                $(document).ready(function () {
                    var timestamp = new Date().getTime();
                    $('.cbody').anystretch("./Images/TOP_Image.png?"+timestamp);
                });

                    function dts(id) {

                        var cbox = { "orderDataType": "dom-text" };
                        var colNum = $(id)[0].rows[0].cells.length;
                        var colSet = [cbox];
                        for (var i = 0 ; i < colNum - 1 ; i++) {
                            colSet.push(null);
                        }
                        $(id).DataTable(
                        {
                        "searching": false,     //フィルタを無効
                        "info": false,         //総件数表示を無効
                        "paging": false,       //ページングを無効
                        //"iDisplayLength":5,
                        "ordering": false,
                        "lengthChange": false,
                        ////"iDisplayLength": 8,
                        "bJQueryUI": true,
                        "bAutoWidth": true,
                        //"scrollX": null,
                        //"scrollY": 180,
                        ////"sDom": '<"top"f>rt<"bottom"pil><"clear">',
                        "oLanguage": 
                        {
                            "sProcessing": "処理中...",
                            "sLengthMenu": "_MENU_ 件表示",
                            "sZeroRecords": "掲示はありません。",
                            "sInfo": "_START_件～_END_件を表示（全_TOTAL_ 件中）",
                            "sInfoEmpty": " 0 件中 0 から 0 まで表示",
                            "sInfoFiltered": "（全 _MAX_ 件より抽出）",
                            "sInfoPostFix": "",
                            "sSearch": "画面絞込:",
                            "sUrl": "",
                            "oPaginate": 
                            {
                                "sFirst": "先頭",
                                "sPrevious": "前ページ",
                                "sNext": "次ページ",
                                "sLast": "最終"
                            }
                        }
                            
                            ,"Columns": colSet
                    }
                );
                    }
                    function chgTab(strIndex) {
                        var tabIndexNew = document.getElementById("hdnTabIndex").value;
                        tabIndexNew.value = strIndex;
                    }
                    function displayForm() {
                        if (document.getElementById("divForm").style.display == "none") {
                            document.getElementById("divForm").style.display = "block";
                            document.getElementById("divEditTitle").style.display = "block";
                            document.getElementById("lnkSample").style.display = "block";
                            document.getElementById("btnGroupLink").style.display = "none";
                            document.getElementById("tabs").style.display = "block";

                        }
                        else {
                            document.getElementById("divForm").style.display = "none";
                            document.getElementById("divEditTitle").style.display = "none";
                            document.getElementById("lnkSample").style.display = "none";
                            document.getElementById("btnGroupLink").style.display = "block";
                            document.getElementById("tabs").style.display = "block";
                            document.getElementById("divSampleForm").style.display = "none";
                        } 
                    }
                    function displaySampleForm() {
                        if (document.getElementById("divSampleForm").style.display == "none") {
                            document.getElementById("divSampleForm").style.display = "block";
                            document.getElementById("tabs").style.display = "none";
                        }
                        else {
                            document.getElementById("divSampleForm").style.display = "none";
                            document.getElementById("tabs").style.display = "block";
                        }
                    }
                    function refresh() {
                        var url = document.getElementById("hdnPageUrl").value;
                        location.href = url;
                    }
                    function chengeTxtForm(checked) {
                        if (checked == 'common') {
                            document.getElementById('txtCommon').style.display = "block";
                            document.getElementById('txtTopicTokyo').style.display = "none";
                            document.getElementById('txtTopicGunma').style.display = "none";
                            document.getElementById('btnCommon').style.display = "block";
                            document.getElementById('btnTopicTokyo').style.display = "none";
                            document.getElementById('btnTopicGunma').style.display = "none";
                        }
                        else if (checked == 'topicTokyo') {
                            document.getElementById('txtCommon').style.display = "none";
                            document.getElementById('txtTopicTokyo').style.display = "block";
                            document.getElementById('txtTopicGunma').style.display = "none";
                            document.getElementById('btnCommon').style.display = "none";
                            document.getElementById('btnTopicTokyo').style.display = "block";
                            document.getElementById('btnTopicGunma').style.display = "none";
                        }
                        else if (checked == 'topicGunma') {
                            document.getElementById('txtCommon').style.display = "none";
                            document.getElementById('txtTopicTokyo').style.display = "none";
                            document.getElementById('txtTopicGunma').style.display = "block";
                            document.getElementById('btnCommon').style.display = "none";
                            document.getElementById('btnTopicTokyo').style.display = "none";
                            document.getElementById('btnTopicGunma').style.display = "block";
                        }
                    }

                    function checkTxt(sight) {
                        var txtSight;
                        
                        if (sight == "common") {
                            txtSight = document.getElementById("txtCommon").innerText;
                        }
                        else if (sight == 'topicTokyo') {
                            txtSight = document.getElementById("txtTopicTokyo").innerText;
                        }
                        else if (sight == 'topicGunma') {
                            txtSight = document.getElementById("txtTopicGunma").innerText;
                        }

                        // 2017/02/19 t.kanda 日付チェック
                        var textArray = txtSight.split(/\r\n|\r|\n/);

                        for (var i = 0, len = textArray.length; i < len; i++) {
                            // 1行目は飛ばす
                            if (i != 0) {
                                var data
                                dataArray = textArray[i].split(',')
                                if (dataArray != "") {
                                    var datestr = dataArray[0].replace(/\s+$/, "");
                                    rc = ckDate(datestr)
                                    if (rc == false) {
                                        alert("項目１番は、日付を設定して下さい(YYYY/MM/DD)")
                                        return false;
                                    }
                                }
                            }
                        }
                        return true;
                    }


                    /**************************************************************** 
                    * 機　能： 入力された値が日付でYYYY/MM/DD形式になっているか調べる 
                    * 引　数： datestr　入力された値 
                    * 戻り値： 正：true　不正：false 
                    ****************************************************************/
                    function ckDate(datestr) {
                        // 正規表現による書式チェック 
                        if (!datestr.match(/^\d{4}\/\d{2}\/\d{2}$/)) {
                            return false;
                        }
                        var vYear = datestr.substr(0, 4) - 0;
                        var vMonth = datestr.substr(5, 2) - 1; // Javascriptは、0-11で表現 
                        var vDay = datestr.substr(8, 2) - 0;
                        // 月,日の妥当性チェック 
                        if (vMonth >= 0 && vMonth <= 11 && vDay >= 1 && vDay <= 31) {
                            var vDt = new Date(vYear, vMonth, vDay);
                            if (isNaN(vDt)) {
                                return false;
                            } else if (vDt.getFullYear() == vYear && vDt.getMonth() == vMonth && vDt.getDate() == vDay) {
                                return true;
                            } else {
                                return false;
                            }
                        } else {
                            return false;
                        }
                    }

                    // --------------------------------------------------
                    // 関数名   : helpオープン
                    // 概要     : ＨＥＬＰファイル
                    // 引数     : strUrl     : フォルダパス
                    // 戻り値   : なし
                    // --------------------------------------------------
                    function helpFileViewOpen(kbn) {
                        try {
                            url = ""
                            if (kbn == "HELP") {
                                var url = "./frmClientView.aspx?VIEWFILE_KBN=" + "HelpTop";
                            }
                            else {
                                var url = "./frmClientView.aspx?VIEWFILE_KBN=" + "QaTop";
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

                    function openSubWindowMitakaRegist() {
                        // 1. 画面のオープン
                        var url = "frmMitakaRegist.aspx";
                        var w = (screen.width - 1340) / 2;
                        var h = (screen.height - 700) / 2;
                        var features = "menubar=no,toolbar=no,location=no,resizable=no,scrollbars=yes,status=no,height=700,width=1340,left=" + w + ",top=" + h;
                        var frmMitakaRegistFlg = window.open(url, "frmMitakaRegist", features);
                    }

            </script>
        </form>
    </body>

</html>
