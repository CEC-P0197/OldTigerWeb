<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmFollowAnswer.aspx.cs" Inherits="OldTigerWeb.frmFollowAnswer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>ＦＭＣ・ｍｃフォロー情報</title>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>
    <link rel="stylesheet" href="Scripts/media/css/jquery.dataTables.css" />
    <link rel="stylesheet" href="Content/OldTiger.css" />
    <script type="text/javascript" src="Scripts/jscommon.js"></script>

    <style type="text/css">

        h4 { /* h1～h6を指定 http://www.aoiweb.com/aoi2/title_bar4.htm */
            /*background:url('Images/bar51.gif') no-repeat left top;*/ /* 画像を左上にひとつだけ配置 */
            font-size: 22px; /* 文字の大きさ */
            /*width : 741px; /* 幅 */
            height: 20px; /* 高さ */
            padding: 3px 0px 0px 40px; /* ボックスの内側[上 右 下 左]の余白 */
            margin: 0px 0px 5px 0px; /* ボックスの外側[上 右 下 左]の余白 */
            /*color: #0073a8; /* 文字の色 */
            color: gray; /* 文字の色を黒に変更 2017.03.29 神田 */
        }
        .div_60 {
            font-family: "ＭＳ ゴシック", "monospace";
            font-size: 10pt;
            width: 60px;
            height: 76px;
            overflow: hidden;
        }
        .div_80 {
            font-family: "ＭＳ ゴシック", "monospace";
            font-size: 10pt;
            width: 80px;
            height: 76px;
            overflow: hidden;
        }
        .div_90 {
            font-family: "ＭＳ ゴシック", "monospace";
            font-size: 10pt;
            width: 90px;
            height: 76px;
            overflow: hidden;
        }
        .div_100 {
            font-family: "ＭＳ ゴシック", "monospace";
            font-size: 10pt;
            width: 108px;
            height: 76px;
            overflow: hidden;
        }
        .div_200 {
            font-family: "ＭＳ ゴシック", "monospace";
            font-size: 10pt;
            width: 200px;
            height: 76px;
            overflow: hidden;
        }
        .div_180 {
            font-family: "ＭＳ ゴシック", "monospace";
            font-size: 10pt;
            width: 180px;
            height: 76px;
            overflow: hidden;
        }


        .buttoncolor:hover {
            /* 背景色を水色に指定 */
            background-color: #ccffff;
            border: 1px #6699ff solid;
        }
        .buttoncolor:focus {
            /* 背景色をリンク色に指定 */
            background-color: #6699ff;
            border: 1px #6699ff solid;
        }
    </style>
</head>
     
<body onload="loadHello()" >

    <%--<h4 class="auto-style7" >　　　　　　　過去トラシステム　　　　　　　　　FMC／MCフォロー情報</h4>--%>
    <h4 class="auto-style7" >過去トラシステム</h4>

    <form id="frmFollowAnswer" method="post" runat="server">
        <div style="height: 680px; width: 1310px;">

         <table align="center" width="100%">
            <tbody>
                <tr>
                  <td colspan="5" class="midasi">ＦＭＣ・ｍｃフォロー情報</td>
                </tr> 
                <tr>
		          <td width="30%" style ="padding-left :15px"><%=(String)ViewState["FullEventName"]%></td>
                  <td width="36%"></td>
                  <td width="18%" align="left">フォロー対象部署：<asp:TextBox ID="txtKacode" runat="server" Width="94px" Height="23px" MaxLength="10" /></td>
                  <td width="8%" align="left"><asp:Button ID="btnDisp" runat="server" Width="100px" Height="30px" Text="表示" Class="buttoncolor" OnClientClick="return confDispCheck();" OnClick="btn_Disp_Click" /></td>
                  <td width="8%" align="right"><asp:Button ID="btnClose" runat="server" Width="100px" Height="30px" Text="閉じる" Class="buttoncolor" OnClientClick="window.close();" /></td>
                </tr>
            </tbody>
        </table>

        <asp:Panel ID="pnlFollowAnswer" runat="server" style="display:none">

        <table class="anstbl" id="tratable">
            <thead>
                <tr>
                    <th width="25px"  height="60px" rowspan="2">No</th>
                    <th width="60px"  height="60px" rowspan="2">部品</th>
                    <th width="100px" height="60px" rowspan="2">現象</th>
                    <th width="100px" height="60px" rowspan="2">制御系要因</th>
                    <th               height="20px" colspan="2">不具合事例</th>
                    <th               height="20px" colspan="4">再発防止</th>
                    <th               height="20px" colspan="2">適用有無</th>
                    <th               height="20px" colspan="4"><%=(String)ViewState["EventName"]%></th>
                </tr>
                <tr>
                    <th width="80px"  height="40px">管理No</th>
                    <th width="100px" height="40px">原因</th>
                    <th width="100px" height="40px">開発時の<br />流出要因</th>
                    <th width="100px" height="40px">確認の<br />観点</th>
                    <th width="100px" height="40px">再発防止策<br />設計面</th>
                    <th width="100px" height="40px">再発防止策<br />評価面</th>
                    <th width="20px"  height="40px">S<br />Q<br />B</th>
                    <th width="20px"  height="40px">設計評価</th>
                    <th width="20px"  height="40px">ﾋﾔﾘﾝｸﾞ</th>
                    <th width="20px"  height="40px">進度</th>
                    <th width="200px" height="40px">対応内容</th>
                    <th width="20px"  height="40px">更新</th>
                </tr>
            </thead>
            <tbody>
<%
    if (gbFollowData != null)
    {
        for (int i = 0; i < gbFollowData.Rows.Count; i++ )
        {
%>
<%
            if (gbFollowData.Rows[i]["SINDO"].ToString() != "")
            {
%>
                <tr>
<%          }
            else
            {
%>
                <tr class="trNonAnswer">
<%
            }
%>
                    <td width="30px" style="text-align:center"><div class="div_30"><%=gbFollowData.Rows[i]["RANK"].ToString() %></div></td>
                    <td width="50px"><div class="div_50"><%=gbFollowData.Rows[i]["BUHIN_NAME"].ToString() %></div></td>
                    <td width="90px"><div class="div_50"><%=gbFollowData.Rows[i]["BUNRUI_GENSYO_NAME"].ToString() %></div></td>
                    <td width="50px"><div class="div_50"><%=gbFollowData.Rows[i]["SEIGYO_FACTOR_NAME"].ToString() %></div></td>
                    <td width="90px"><div class="div_90"><%=gbFollowData.Rows[i]["KOUMOKU"].ToString() %></div></td>
                    <td width="100px"><div class="div_100"><%=gbFollowData.Rows[i]["GENIN"].ToString() %></div></td>
                    <td width="100px"><div class="div_100"><%=gbFollowData.Rows[i]["KAIHATU_MIHAKKEN_RIYU"].ToString() %></div></td>
                    <td width="100px"><div class="div_100"><%=gbFollowData.Rows[i]["SQB_KANTEN"].ToString() %></div></td>
                    <td width="100px"><div class="div_100"><%=gbFollowData.Rows[i]["SAIHATU_SEKKEI"].ToString() %></div></td>
                    <td width="100px"><div class="div_100"><%=gbFollowData.Rows[i]["SAIHATU_HYOUKA"].ToString() %></div></td>

                    <td width="20px" align="center"><%=gbFollowData.Rows[i]["TEKIYO_SQB"].ToString() %></td>
                    <td width="20px" align="center"><%=gbFollowData.Rows[i]["TEKIYO_SEKKEI"].ToString() %></td>
                    <td width="20px" align="center"><%=gbFollowData.Rows[i]["HEARING"].ToString() %></td>
                    <td width="20px" align="center"><%=gbFollowData.Rows[i]["SINDO"].ToString() %></td>
                    <td width="180px"><div class="div_200"><%=gbFollowData.Rows[i]["TAIOU_NAIYO"].ToString() %></div></td>
                    <td width="20px" align="center"><a href="#" onclick="openAnswer('<%=gbFollowData.Rows[i]["FOLLOW_KEY"].ToString() %>');">編集</a></td>
                </tr>
<%
        }
    }
%>
            </tbody>
        </table>

        </asp:Panel>
        </div>

        <asp:HiddenField ID="hdPageNo" runat="server" />
        <asp:HiddenField ID="hdKacode" runat="server" />
        <asp:HiddenField ID="hdSubmit" runat="server" />

    </form>
        <%--外部JS読込み--%>
    <script src="Scripts/jquery-1.8.2.js"></script>
    <script src="Scripts/jquery-ui-1.8.24.js"></script>
    <script src="Scripts/media/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript">

        var parentWindowObject;
        window.onload = function () {
            parentWindowObject = window.opener;  //親画面定義
            parentWindowObject.document.getElementById('btnCloseWindow').click(); //親画面内ボタンクリック
        }

        $(document).ready(function () {
            if (document.getElementById("pnlFollowAnswer") != null) {
                document.getElementById("pnlFollowAnswer").style.display = 'block';
            }
            var oTable = $('#tratable').dataTable(
            {
                "bProcessing": true,
                "lengthChange": false,
                "searching": false,
                //"ordering": false,
                "iDisplayLength": 6,
                "columnDefs": [{
                    "targets": [0,1,2,3,4,5,6,7,8,9,10,11,12,14,15],
                    "orderable": false
                }],
                "aaSorting": [],
                "oLanguage": {
                    "sProcessing": "処理中...",
                    "sLengthMenu": "_MENU_ 件表示",
                    "sZeroRecords": "フォロー対象部署に該当する、フォロー回答情報はありませんでした。",
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

            oTable.fnPageChange(<%= gbStartPage %>);
        });

       // 2017/07/14 Add Start
       // --------------------------------------------------
       // 関数名   : フォロー対象部署オートコンプリート
       // 概要     : フォロー対象部署入力候補設定
       // 引数     : 
       // --------------------------------------------------
        $(function () {
            var inputAuxiliary = [
                <%=(string)ViewState["KaCodeInfo"]%>
            ];

            $("#txtKacode").autocomplete({
                source: inputAuxiliary
            });

        });
        // 2017/07/14 Add End

        // --------------------------------------------------
        // 関数名   : confDispCheck
        // 概要     : 表示ボタンチェック
        // 引数     : なし
        // 戻り値   : true、false
        // --------------------------------------------------
        // 表示ボタンチェック
        function confDispCheck() {
            var txtData = document.getElementById("<%= txtKacode.ClientID %>").value;
            var rc = false;

            txtData = $.trim(txtData);

            // 未入力は何もしない
            if (txtData == "") {
                alert("フォロー対象部署が指定されていません。");
                document.getElementById("<%= txtKacode.ClientID %>").focus();
                return rc;
            }
            // ロード画面表示
            parentWindowObject.document.getElementById('btnOpenWindow').click(); //親画面内ボタンクリック
            if (document.getElementById("pnlFollowAnswer") != null) {
                document.getElementById('pnlFollowAnswer').style.display = 'none';
            }
                return true;
        }

        // --------------------------------------------------
        // 関数名   : openAnswer
        // 概要     : 回答登録画面表示
        // 引数     : フォローキー情報
        // 戻り値   : true、false
        // --------------------------------------------------
        function openAnswer(followKey) {

            // 1. 画面のオープン
            var url = "frmAnswer.aspx?FOLLOW=" + followKey + "&EventName=<%=HttpUtility.UrlEncode((String)ViewState["FullEventName"])%>";
            var features = "dialogWidth=800px; dialogHeight=700px;status=no;scroll=yes;center=yes;";
            var returnFlg = window.showModalDialog(url, this, features);

            // 2. 戻り処理
            if (returnFlg == 1) {
                // 更新有無
                var hdSubmit = document.getElementById("<%= hdSubmit.ClientID %>");
                hdSubmit.value = returnFlg;

                // 現在ページ取得
                var hdPageNo = document.getElementById("<%= hdPageNo.ClientID %>");
                hdPageNo.value = $('#tratable').DataTable().page();

                frmFollowAnswer.submit();
            }

            return false;
        }

        $(function () {
            var dispval = document.getElementById("<%= btnDisp.ClientID %>");
            var closeval = document.getElementById("<%= btnClose.ClientID %>");

            $("input").keydown(function (e) {
                if ((e.which && e.which === 13) || (e.keyCode && e.keyCode === 13)) {
                    var value = document.activeElement;
                    if (dispval == value || closeval == value)
                    {
                        return true;
                    }
                    return false;
                } else {
                    return true;
                }
            });
        });

        function loadHello() {
            var hdSubmit = document.getElementById("<%= hdSubmit.ClientID %>");

            if (hdSubmit.value == "1")
            {
                hdSubmit.value = "0";
                alert("フォロー回答情報を更新しました。");
            }
        }

        /* IE8 用 */
        jQuery(function ($) { $("tr:nth-child(odd)").addClass('odd'); $("tr:nth-child(even)").addClass('even'); });

    </script>

</body>
</html>
