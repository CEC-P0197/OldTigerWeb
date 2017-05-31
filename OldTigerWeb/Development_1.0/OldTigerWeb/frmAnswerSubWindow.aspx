<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmAnswerSubWindow.aspx.cs" Inherits="OldTigerWeb.frmAnswerSubWindow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=Edge" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>ＦＭＣ・ｍｃ他部署回答一覧</title>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>
    <link rel="stylesheet" href="Scripts/media/css/jquery.dataTables.css" />
    <link rel="stylesheet" href= "Content/OldTiger.css" />
    <%--外部JS読込み--%>
    <script type="text/javascript" src="Scripts/jscommon.js"></script>
    <script src="Scripts/jquery-1.8.2.js"></script>
    <script src="Scripts/jquery-ui-1.8.24.js"></script>

     
<base target="_self" />
    <style type="text/css">

       .div_60 {
            font-family: "ＭＳ ゴシック", "monospace";
            width: 60px;
        }
        .div_110 {
            font-family: "ＭＳ ゴシック", "monospace";
            width: 110px;
        }

        .div_610 {
            font-family: "ＭＳ ゴシック", "monospace";
            width: 610px;
        }

    </style>
</head>

<body style="overflow-x:hidden;">


    <form id="frmAnswerSubWindow" runat="server">
    <div style="padding-left:10px;padding-right :10px">
    <%--<table  style="width:780px;">--%>
    <table  style="width:100%;padding-top :20px;padding-bottom :10px">
        <tr>
            <td></td>
            <td class="midasi" style ="padding-left :20px">ＦＭＣ・ｍｃ他部署回答一覧
                <%--<p class="td_data24">FMC／MC他部署回答一覧</p>--%>
                <%--<p class="midasi">FMC／MC他部署回答一覧</p>--%>
            </td>
            <td>
                <%--<div style="text-align: right" >--%> 
                    <asp:Button ID="Button1" runat="server"  Text="閉じる" style="float:right; Width:100px; Height:30px"
                        Class="buttoncolor" OnClientClick="window.close();" />
                <%--</div>--%>
            </td>
        </tr>
    </table>  

    
        <%--<div style="width:780px" >--%>               
        <table style="padding-left :10px;padding-right :10px" class="anstbl" id="tratable" >
            <thead>
                <tr>
                    <th class="div_60">課</th>
                    <th class="div_110">進度</th>
                    <th class="div_610">回答内容</th>
                </tr>
            </thead>    
            <tbody>
            
<%
    if ( gbFollowDataOtherDept!= null )
    {
        for (int i = 0; i < gbFollowDataOtherDept.Rows.Count; i++ )
        {
%>
                <tr>
                    <td class="div_60"><%=gbFollowDataOtherDept.Rows[i]["KA_CODE"].ToString() %></td>
<%
            if ( gbFollowDataOtherDept.Rows[i]["SINDO"].ToString() == "済" )
            {
%>
                    <td class="div_110">織込済</td>
<%
            }
            else if ( gbFollowDataOtherDept.Rows[i]["SINDO"].ToString() == "△" )
            {
%>
                    <td class="div_110">検討･調整中</td>
<%
            }
            else if ( gbFollowDataOtherDept.Rows[i]["SINDO"].ToString() == "×" )
            {
%>
                     <td class="div_110">未確認</td>
<%
            }
            else if ( gbFollowDataOtherDept.Rows[i]["SINDO"].ToString() == "－" )
            {
%>
                     <td class="div_110">適用外</td>
<%
            }
            else
            {
%>
                    <td class="div_110"></td>
<%
            }
%>          
                    <td class="div_610"><%=gbFollowDataOtherDept.Rows[i]["TAIOU_NAIYO"].ToString() %></td>
                </tr>
                
<%
        }
    }
%>
            </tbody>
        </table>
    </div>
    </div>
    <script src="Scripts/media/js/jquery.dataTables.min.js"></script>
     <script type="text/javascript">
         $(document).ready(function () {
             CreateDataTables('#tratable');
         });

         function CreateDataTables(tableID) {
             var oTable = $(tableID).DataTable({

                 "searching": false,     //フィルタを有効
                 "paging": false,
                 "lengthChange": false,
                 "aaSorting": [],
                 "sDom": '<"top">rt<"bottom"><"clear">',
                 "scrollY": 500,
                 "sScrollX": null,
                 "bSort": false,
                 "bAutoWidth": true,
                 "oLanguage": {
                     "sProcessing": "処理中...",
                     "sLengthMenu": "_MENU_ 件表示",
                     "sZeroRecords": "該当するフォロー回答情報はありません。",
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
    </script>
        </form>
   
        </body>
</html>
