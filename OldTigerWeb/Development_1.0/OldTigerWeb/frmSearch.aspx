<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmSearch.aspx.cs" Inherits="OldTigerWeb.frmSearch" %>

<!DOCtype html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta http-equiv="Content-type" content="text/html; charset=utf-8"/>
    <title>過去トラ検索</title>

    <link rel="stylesheet" href= "Content/OldTiger.css" />
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>--%>
    <link rel="stylesheet" href= "Content/themes/base/jquery-ui.css"/>
    <script type="text/javascript" src="Scripts/JSCOMMON.js"></script>

    <style type="text/css">
        h4 { /* h1～h6を指定 http://www.aoiweb.com/aoi2/title_bar4.htm */
            font-size: 24px; /* 文字の大きさ */
            width: 600px; /* 幅 */
            height: 25px; /* 高さ */
            padding: 3px 0px 0px 40px; /* ボックスの内側[上 右 下 左]の余白 */
            margin: 0px 0px 5px 0px; /* ボックスの外側[上 右 下 左]の余白 */
            /*color: #0073a8;*/ /* 文字の色 */
            color: grey ; /* 文字の色を黒に変更 2017.03.29 神田 */
        } 
    </style>

</head>

<body style="background-image: url(./Images/BackImage.gif)">

<%--<div style="height: 640px; width: 100%; " >--%>
<div style="height: 640px; width: 1280px;" >

    <h4>過去トラシステム</h4>
    <%--<h4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;過去トラシステム</h4>--%>

    <form id="frmSearch" runat="server">
        <%-- ヘッダーエリア --%>
        <table style ="padding-left :40px">
                <tr>
                    <td>
                        <asp:LinkButton ID="lnkTop" Text="TOPページ" OnClick="lnkTop_Click" runat="server" />
                    </td>
                    <td class="midasi" style ="text-align:right;padding-right :150px;">過去トラ検索</td>
                    <td></td>
                    <td style ="text-align:right;">
                        <% if ((String)ViewState["HELP"] != "")
                            { %>
                        <%--<a href="javascript:DisplayForder('<%=(String)ViewState["HELP"]%>')">マニュアル・Ｑ＆Ａ</a>--%>
                        <a href="##" onclick="helpFileViewOpen('HELP');"style ="padding-right:10px">マニュアル</a>
                        <%
                            }
                            else
                            { %>
                                マニュアル
                        <% 
                            }
                        %>
                    </td>
                    <td style ="text-align:right;">
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
                    <td style ="text-align:center;">
                        <a href="mailto:<%= (String)ViewState["MailAddr"] %>">問合せ・ご要望</a>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="3">
                        <asp:Button ID="btnOR" runat="server" Text="OR" Width="50px" OnClick="btn_OR_Click" BackColor="#99FF99" />
                        <asp:Button ID="btnAND" runat="server" Text="AND" Width="50px" OnClick="btn_AND_Click" BackColor="#66FFFF" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:TextBox ID="txtSearch" placeholder="検索キーワードは空白区切りで４つまで入力可"
                            runat="server" class="moji18" Style="ime-mode: active" Columns="170" Width="98%" Height="25px" BackColor="#99FF99" />
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="ｷｰﾜｰﾄﾞ検索" Width="100px"
                            Class="buttoncolor" Style="font-size: 12pt" OnClick="btn_Search_Click" OnClientClick="return confSearchCheck();" /></td>
                    <td>
                        <asp:Button ID="btnClearKeyWord" runat="server" Text="ｷｰﾜｰﾄﾞｸﾘｱ" Width="100px"
                            Class="buttoncolor " Style="font-size: 12pt" OnClick="btn_Clear_Click" /></td>
                </tr>
        </table>
        <%-- 検索条件エリア --%>
        <table style ="margin :0 auto ;">
                <tr>
                    <%-- カテゴリ検索 --%>
                    <td class="categoryField">
                        <%--タブのインデックスをhiddenフィールドで保持--%>
                        <asp:HiddenField runat="server" ID="hdnTabIndex" Value="0" />
                        <div id="tabs" class="tabsGroup">
                            <ul>
                                <li id="tabPU"><a href="#lnkKategori03PU" class ="tabcolor" onclick="javascript:chgTab('0');">部署（ＰＵ）</a></li>
                                <li id="tabBY"><a href="#lnkKategori03BY" class ="tabcolor" onclick="javascript:chgTab('1');">部署（ＢＹ）</a></li>
                                <li id="tabMt01"><a href="#lnkKategori04" class ="tabcolor" onclick="javascript:chgTab('2');">部品・部位</a></li>
                                <li id="tabMt02"><a href="#lnkKategori05" class ="tabcolor" onclick="javascript:chgTab('3');">開発符号</a></li>
                                <li id="tabMt03"><a href="#lnkKategori06" class ="tabcolor" onclick="javascript:chgTab('4');">現象（分類）</a></li>
                                <li id="tabMt04"><a href="#lnkKategori07" class ="tabcolor" onclick="javascript:chgTab('5');">原因（分類）</a></li>
                                <li id="tabMt05"><a href="#lnkKategori08" class ="tabcolor" onclick="javascript:chgTab('6');">車型特殊</a></li>
                                <li id="tabMt06"><a href="#lnkKategori09" class ="tabcolor" onclick="javascript:chgTab('7');">現象（制御系）</a></li>
                                <li id="tabMt07"><a href="#lnkKategori10" class ="tabcolor" onclick="javascript:chgTab('8');">要因（制御系）</a></li>
                                <li id="tabMt08"><a href="#lnkKategori11" class ="tabcolor" onclick="javascript:chgTab('9');">ＥＧＴＭ形式</a></li>
                            </ul>

                            <%-- 部署（ＰＵ） --%>
                            <div id="lnkKategori03PU" class ="lnkKategori">
                                <asp:Panel ID="pnlCategoryBusyo" runat="server" class="categoryTab">
                                    <table>
                                            <tr>
                                                <td></td>
                                                <td>設計部署
                                                    <input type="checkbox" onclick="this.blur(); this.focus(); TabAllPU();"
                                                        onchange="sekkeiChange(this.checked?'ON':'OFF','PU')" />
                                                    <label>&nbsp;全選択・全解除</label>
                                                </td>
                                                <td></td>
                                                <td>評価部署
                                                    <input type="checkbox" onclick="this.blur(); this.focus(); TabAllPU();"
                                                        onchange="hyoukaChange(this.checked?'ON':'OFF','PU')"/>
                                                    <label>&nbsp;全選択・全解除</label>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <div class="categoryBusyo">
                                                        <asp:CheckBoxList ID="ckBoxBusyoSekkeiPu" runat="server" DataTextField="NAME" DataValueField="ID"
                                                            onclick="selectedCheckBoxItem(this.id + '<->ckBoxBusyoHyoukaPu','lnkKategori03PU')">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                                <td></td>
                                                <td>
                                                    <div class="categoryBusyo">
                                                        <asp:CheckBoxList ID="ckBoxBusyoHyoukaPu" runat="server" DataTextField="NAME" DataValueField="ID"
                                                            onclick="selectedCheckBoxItem(this.id +'<->ckBoxBusyoSekkeiPu','lnkKategori03PU')">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                              </tr>
                                    </table>
                                </asp:Panel>
                            </div>

                            <%-- 部署（ＢＹ） --%>
                            <div id="lnkKategori03BY" class ="lnkKategori">
                                <asp:Panel ID="pnlCategoryBusyo1" runat="server" class="categoryTab">
                                    <table>
                                            <tr>
                                                <td></td>
                                                <td>設計部署
                                                <input type="checkbox" onclick="this.blur(); this.focus(); TabAllBY();" 
                                                    onchange="sekkeiChange(this.checked?'ON':'OFF','BY')"/>
                                                    <label>&nbsp;全選択・全解除</label></td>
                                                <td></td>
                                                <td>評価部署
                                                <input type="checkbox" onclick="this.blur(); this.focus(); TabAllBY();" 
                                                    onchange="hyoukaChange(this.checked?'ON':'OFF','BY')"/>
                                                    <label>&nbsp;全選択・全解除</label></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <div class="categoryBusyo">
                                                        <asp:CheckBoxList ID="ckBoxBusyoSekkeiBy" runat="server" DataTextField="NAME" DataValueField="ID"
                                                            onclick="selectedCheckBoxItem(this.id +'<->ckBoxBusyoHyoukaBy','lnkKategori03BY')">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                                <td></td>
                                                <td>
                                                    <div class="categoryBusyo">
                                                        <asp:CheckBoxList ID="ckBoxBusyoHyoukaBy" runat="server" DataTextField="NAME" DataValueField="ID"
                                                            onclick="selectedCheckBoxItem(this.id +'<->ckBoxBusyoSekkeiBy','lnkKategori03BY')">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                                <td></td>
                                            </tr>
                                    </table>
                                </asp:Panel>
                            </div>

                            <%-- 部品・部位 --%>
                            <div id="lnkKategori04" class ="lnkKategori">
                                <asp:Panel ID="pnlCategoryMst01" runat="server" class="categoryTab">
                                    <table>
                                            <tr>
                                                <td></td>
                                                <td style="padding-top:5px;">システム</td>
                                                <td></td>
                                                <td>部品部位
                                                    <input type="checkbox" id="ckBoxBuhinAll" onclick="this.blur(); this.focus(); TabAllBuhin();" 
                                                        onchange="buhinChange(this.checked?'ON':'OFF')"/>
                                                        <label>&nbsp;全選択・全解除</label>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <div class="categorySystem">
                                                                    <asp:CheckBoxList ID="ckBoxSystem" runat="server" DataTextField="NAME" DataValueField="ID"
                                                                        onclick="selectedCheckBoxItem(this.id,'lnkKategori04')">
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnFilter" runat="server" Text="絞込み" Width="100px" Class="buttoncolor"
                                                                    OnClick="btn_Filter" OnClientClick="return systemSelectCheck();" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td></td>
                                                <td>
                                                     <div class="categoryBuhin">
                                                          <asp:CheckBoxList ID="ckBoxBuhin" runat="server" DataTextField="NAME" DataValueField="ID">
                                                          </asp:CheckBoxList>
                                                     </div>
                                                </td>
                                                <td></td>
                                            </tr>
                                    </table>
                                </asp:Panel>
                            </div>

                            <%-- 開発符号 --%>
                            <div id="lnkKategori05" class ="lnkKategori">
                                <asp:Panel ID="pnlCategoryMst02" runat="server" class="categoryTab">
                                    <table>
                                            <tr>
                                                <td></td>
                                                <td style="padding-top:5px;">開発符号</td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <div class="categoryMst">
                                                        <asp:CheckBoxList ID="ckBoxMst01" runat="server" DataTextField="NAME" DataValueField="ID"
                                                            onclick="selectedCheckBoxItem(this.id,'lnkKategori05')">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                                <td></td>
                                            </tr>
                                    </table>
                                </asp:Panel>
                            </div>

                            <%-- 現象（分類） --%>
                            <div id="lnkKategori06" class="lnkKategori">
                                <asp:Panel ID="pnlCategoryMst03" runat="server" class="categoryTab">
                                    <table>
                                            <tr>
                                                <td></td>
                                                <td style="padding-top:5px;">現象（分類）</td>
                                                <td></td>
                                            </tr>                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <div class="categoryMst">
                                                        <%--<div class="categoryMst" style="height: 292px">--%>
                                                        <asp:CheckBoxList ID="ckBoxMst02" runat="server" DataTextField="NAME" DataValueField="ID"
                                                            onclick="selectedCheckBoxItem(this.id,'lnkKategori06')">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                                <td></td>
                                            </tr>
                                    </table>
                                </asp:Panel>
                            </div>

                            <%-- 原因（分類） --%>
                            <div id="lnkKategori07" class="lnkKategori">
                                <asp:Panel ID="pnlCategoryMst04" runat="server" class="categoryTab">
                                    <table>
                                            <tr>
                                                <td></td>
                                                <td style="padding-top:5px;">原因（分類）</td>
                                                <td></td>
                                            </tr> 
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <div class="categoryMst">
                                                        <asp:CheckBoxList ID="ckBoxMst03" runat="server" DataTextField="NAME" DataValueField="ID"
                                                            onclick="selectedCheckBoxItem(this.id,'lnkKategori07')">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                                <td></td>
                                            </tr>
                                    </table>
                                </asp:Panel>
                            </div>

                            <%-- 車型特殊 --%>
                            <div id="lnkKategori08" class="lnkKategori">
                                <asp:Panel ID="pnlCategoryMst05" runat="server" class="categoryTab">
                                    <table>
                                            <tr>
                                                <td></td>
                                                <td style="padding-top:5px;">車型特殊</td>
                                                <td></td>
                                            </tr> 
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <div class="categoryMst">
                                                        <asp:CheckBoxList ID="ckBoxMst04" runat="server" DataTextField="NAME" DataValueField="ID"
                                                            onclick="selectedCheckBoxItem(this.id,'lnkKategori08')">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                                <td></td>
                                            </tr>
                                    </table>
                                </asp:Panel>
                            </div>

                            <%-- 現象（制御系） --%>
                            <div id="lnkKategori09" class="lnkKategori">
                                <asp:Panel ID="pnlCategoryMst06" runat="server" class="categoryTab">
                                    <table>
                                            <tr>
                                                <td></td>
                                                <td style="padding-top:5px;">現象（制御系）</td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <div class="categoryMst">
                                                        <asp:CheckBoxList ID="ckBoxMst05" runat="server" DataTextField="NAME" DataValueField="ID"
                                                            onclick="selectedCheckBoxItem(this.id,'lnkKategori09')">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                                <td></td>
                                            </tr>
                                    </table>
                                </asp:Panel>
                            </div>

                            <%-- 要因（制御系） --%>
                            <div id="lnkKategori10" class="lnkKategori">
                                <asp:Panel ID="pnlCategoryMst07" runat="server" class="categoryTab">
                                    <table>
                                            <tr>
                                                <td></td>
                                                <td style="padding-top:5px;">要因（制御系）</td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <div class="categoryMst">
                                                        <asp:CheckBoxList ID="ckBoxMst06" runat="server" DataTextField="NAME" DataValueField="ID"
                                                            onclick="selectedCheckBoxItem(this.id,'lnkKategori10')">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                                <td></td>
                                            </tr>
                                    </table>
                                </asp:Panel>
                            </div>

                            <%-- ＥＧＴＭ形式 --%>
                            <div id="lnkKategori11" class="lnkKategori">
                                <asp:Panel ID="pnlCategoryMst08" runat="server" class="categoryTab">
                                    <table>
                                            <tr>
                                                <td></td>
                                                <td style="padding-top:5px;">ＥＧＴＭ形式</td>
                                                <td></td>
                                            </tr>                                           
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <div class="categoryMst">
                                                        <asp:CheckBoxList ID="ckBoxMst07" runat="server" DataTextField="NAME" DataValueField="ID"
                                                            onclick="selectedCheckBoxItem(this.id,'lnkKategori11')">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                                <td></td>
                                            </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        
                        	<%--<div>
	                        <asp:Button ID="btnCategoryClear" runat="server" text="ｶﾃｺﾞﾘｸﾘｱ" Width="100px"
	                                 Class="buttoncolor " Style="font-size: 12pt; float: right;" onclick="btn_CategoryClear_Click" OnClientClick="return CategoryAllClear();"/>
	                        <asp:Button ID="btnCategorySerch" runat="server" text="ｶﾃｺﾞﾘ検索"  Width="100px" 
	                               Class="buttoncolor" Style="font-size: 12pt;float: right;" onclick="btn_CategorySearch_Click" OnClientClick="return confSearchCategoryCheck();" />
                            </div>--%>
                            <div>
                                <asp:Button ID="btnCategoryClear" runat="server" text="ｶﾃｺﾞﾘｸﾘｱ" Width="100px"
                                         Class="buttoncolor " Style="font-size: 12pt; float: right;" onclick="btn_CategoryClear_Click" OnClientClick="return CategoryAllClear();"/>
                                <asp:Button ID="btnCategorySerchAND" runat="server" text="ｶﾃｺﾞﾘAND検索" 
                                       Class="buttoncolor" Style="font-size: 12pt;float: right;" BackColor="#66FFFF" onclick="btn_CategorySearchAND_Click" OnClientClick="return confSearchCategoryCheck();" />
                                <asp:Button ID="btnCategorySerchOR" runat="server" text="ｶﾃｺﾞﾘOR検索"
                                       Class="buttoncolor" Style="font-size: 12pt;float: right;" BackColor="#99FF99" onclick="btn_CategorySearchOR_Click" OnClientClick="return confSearchCategoryCheck();" />
                            </div>
                        </div>
                    </td>

                    <%-- TOP10タグ --%>
                    <td class="serchTop10">
                        <table>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblTop10" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td >&nbsp;1.</td>
                                <td >
                                    <asp:LinkButton ID="lnkTop01" runat="server" OnCommand="lnkTopTen_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td >&nbsp;2.</td>
                                <td >
                                    <asp:LinkButton ID="lnkTop02" runat="server" OnCommand="lnkTopTen_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td >&nbsp;3.</td>
                                <td >
                                    <asp:LinkButton ID="lnkTop03" runat="server" OnCommand="lnkTopTen_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td >&nbsp;4.</td>
                                <td >
                                    <asp:LinkButton ID="lnkTop04" runat="server" OnCommand="lnkTopTen_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td >&nbsp;5.</td>
                                <td >
                                    <asp:LinkButton ID="lnkTop05" runat="server" OnCommand="lnkTopTen_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td >&nbsp;6.</td>
                                <td >
                                    <asp:LinkButton ID="lnkTop06" runat="server" OnCommand="lnkTopTen_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td >&nbsp;7.</td>
                                <td >
                                    <asp:LinkButton ID="lnkTop07" runat="server" OnCommand="lnkTopTen_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td >&nbsp;8.</td>
                                <td >
                                    <asp:LinkButton ID="lnkTop08" runat="server" OnCommand="lnkTopTen_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td >&nbsp;9.</td>
                                <td >
                                    <asp:LinkButton ID="lnkTop09" runat="server" OnCommand="lnkTopTen_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td >10.</td>
                                <td >
                                    <asp:LinkButton ID="lnkTop10" runat="server" OnCommand="lnkTopTen_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <%-- 検索履歴 --%>
                    <td class="serchkHistory">
                        <table>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label1" runat="server" >検索キーワード履歴</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <asp:Label ID="lblConFlg01" runat="server" Width="55px" /></td>
                                <td >
                                    <asp:LinkButton ID="lnkHistory01" CommandName="0" runat="server" OnCommand="lnkHistory_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <asp:Label ID="lblConFlg02" runat="server" Width="55px" /></td>
                                <td >
                                    <asp:LinkButton ID="lnkHistory02" CommandName="1" runat="server" OnCommand="lnkHistory_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <asp:Label ID="lblConFlg03" runat="server" Width="55px" /></td>
                                <td >
                                    <asp:LinkButton ID="lnkHistory03"  CommandName="2" runat="server" OnCommand="lnkHistory_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <asp:Label ID="lblConFlg04" runat="server" Width="55px" /></td>
                                <td >
                                    <asp:LinkButton ID="lnkHistory04" CommandName="3" runat="server" OnCommand="lnkHistory_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <asp:Label ID="lblConFlg05" runat="server" Width="55px" /></td>
                                <td >
                                    <asp:LinkButton ID="lnkHistory05"  CommandName="4" runat="server" OnCommand="lnkHistory_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
        </table> 
        <input type="button" id="btnOpenWindow" style="visibility:hidden" onclick="openWindowLoading();" />
        <input type="button" id="btnCloseWindow" style="visibility:hidden" onclick="closeWindowLoading();" />
    </form> 
</div>
</body>

    <%--外部JS読込み--%>
    <script src="Scripts/jquery-1.8.2.js"></script>
    <script src="Scripts/jquery-ui-1.8.24.js"></script>
    <script src="Scripts/loading.js"></script>
    <script type="text/javascript">

       var tabIndex;
       var frmLoading;
        //タブ設定
        $(document).ready(function () {
            
            selectedCheckBoxItem('ckBoxBusyoSekkeiPu<->ckBoxBusyoHyoukaPu','lnkKategori03PU');   // 部署（ＰＵ）
            selectedCheckBoxItem('ckBoxBusyoSekkeiBy<->ckBoxBusyoHyoukaBy', 'lnkKategori03BY');   // 部署（ＢＹ）
            selectedCheckBoxItem('ckBoxSystem','lnkKategori04');                                // 部品・部位
            selectedCheckBoxItem('ckBoxMst01','lnkKategori05');                                 // 開発符号
            selectedCheckBoxItem('ckBoxMst02','lnkKategori06');                                 // 現象（分類）
            selectedCheckBoxItem('ckBoxMst03','lnkKategori07');                                 // 原因（分類）
            selectedCheckBoxItem('ckBoxMst04','lnkKategori08');                                 // 車両特殊
            selectedCheckBoxItem('ckBoxMst05','lnkKategori09');                                 // 現象（制御系）
            selectedCheckBoxItem('ckBoxMst06','lnkKategori10');                                 // 要因（制御系）
            selectedCheckBoxItem('ckBoxMst07','lnkKategori11');                                 // EGTM形式
          
            $('#tabs').tabs({ selected: tabIndex });
            
        })

        //--------------------------------------------------
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
        // 関数名   : confSearchCheck
        // 概要     : 文字列検索チェック
        // 引数     : なし
        // 戻り値   : true、false
        // --------------------------------------------------
        function confSearchCheck() {
            var txtData = document.getElementById("<%= txtSearch.ClientID %>").value;
            var wkData = "";
            var url = "";
            var rc = false;

            txtData = $.trim(txtData);

            // 未入力は何もしない
            if (txtData == "")
            {
                return rc;
            }

            // 全角スペースをを半角に置換 20160311
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

       // --------------------------------------------------
       // 関数名   : confSearchCategoryCheck
       // 概要     : カテゴリ検索チェック
       // 引数     : なし
       // 戻り値   : true、false
       // --------------------------------------------------
       function confSearchCategoryCheck() {

           var elem
           var iCount = 0;

           //　部署PU　設計
           elem = document.getElementById("<%= ckBoxBusyoSekkeiPu.ClientID %>")
           iCount = categorySelectCount(elem)
           if (iCount != 0) {
               return true;
           }
           //　部署PU　評価
           elem = document.getElementById("<%= ckBoxBusyoHyoukaPu.ClientID %>")
           iCount = categorySelectCount(elem)
           if (iCount != 0) {
               return true;
           }
           //　部署BY　設計
           elem = document.getElementById("<%= ckBoxBusyoSekkeiBy.ClientID %>")
           iCount = categorySelectCount(elem)
           if (iCount != 0) {
               return true;
           }
           //　部署BY　評価
           elem = document.getElementById("<%= ckBoxBusyoHyoukaBy.ClientID %>")
           iCount = categorySelectCount(elem)
           if (iCount != 0) {
               return true;
           }
           //　システム
           elem = document.getElementById("<%= ckBoxSystem.ClientID %>")
           iCount = categorySelectCount(elem)
           if (iCount != 0) {
               return true;
           }
           //　開発符号
           elem = document.getElementById("<%= ckBoxMst01.ClientID %>")
           iCount = categorySelectCount(elem)
           if (iCount != 0) {
               return true;
           }
           //　現象（分類）
           elem = document.getElementById("<%= ckBoxMst02.ClientID %>")
           iCount = categorySelectCount(elem)
           if (iCount != 0) {
               return true;
           }
           //　原因（分類）
           elem = document.getElementById("<%= ckBoxMst03.ClientID %>")
           iCount = categorySelectCount(elem)
           if (iCount != 0) {
               return true;
           }
           //　車型特殊
           elem = document.getElementById("<%= ckBoxMst04.ClientID %>")
           iCount = categorySelectCount(elem)
           if (iCount != 0) {
               return true;
           }
           //　現象（制御系）
           elem = document.getElementById("<%= ckBoxMst05.ClientID %>")
           iCount = categorySelectCount(elem)
           if (iCount != 0) {
               return true;
           }
           //　要因（制御系）
           elem = document.getElementById("<%= ckBoxMst06.ClientID %>")
           iCount = categorySelectCount(elem)
           if (iCount != 0) {
               return true;
           }
           //　EGTM形式
           elem = document.getElementById("<%= ckBoxMst07.ClientID %>")
           iCount = categorySelectCount(elem)
           if (iCount != 0) {
               return true;
           }
           //　部品  部品部位非表示の対応
           elem = document.getElementById("<%= ckBoxBuhin.ClientID %>")
           if (elem != undefined) {
               CategoryClear(document.getElementById("<%= ckBoxBuhin.ClientID %>"));
               iCount = categorySelectCount(elem)
               if (iCount != 0) {
                   return true;
               }

           }
           // チェックボックス選択チェック
           alert("カテゴリが選択されていません。");
           return false;
       }

        // --------------------------------------------------
        // 関数名   : mstSelectCheck
        // 概要     : カテゴリ選択チェック
        // 引数     : なし
        // 戻り値   : true、false
       // -------------------------------------------------- 
       function categorySelectCount(elem)  {
           var iCount = 0;
           var a = elem.getElementsByTagName("input");

           for (var i = 0; i < a.length; i++) {
               var c = a[i];
               if (c.type != "checkbox") {
                   continue;
               }

               if (c.checked) {
                   iCount++;
               }
           }

           return iCount;
       }

        // --------------------------------------------------
        // 関数名   : systemSelectCheck
        // 概要     : カテゴリ・システム選択チェック
        // 引数     : なし
        // 戻り値   : true、false
        // --------------------------------------------------
        function systemSelectCheck() {
            var chkData = document.getElementById("<%= ckBoxSystem.ClientID %>");
            var iFound = 0;
            var rc = false;

            var a = chkData.getElementsByTagName("input");

            for (var i = 0; i < a.length; i++) {
                var c = a[i];
                if (c.type != "checkbox") {
                    continue;
                }

                if (c.checked) {
                    iFound++;
                    break;
                }
            }

            // チェックボックス選択チェック
            if (iFound == 0) {
                alert("システムが選択されていません。");
                return rc;
            }
       
                return true;
           
        }


        // --------------------------------------------------
        // 関数名   : sekkeiChange
        // 概要     : カテゴリ・設計部署 全選択・全解除
        // 引数     : mode　ON 全選択、OFF 全解除
        // --------------------------------------------------
       function sekkeiChange(mode, puby) {
            var flag = false;

            if (mode == "ON") flag = true;
                if (puby == 'BY') {
                    var elem = document.getElementById("<%= ckBoxBusyoSekkeiBy.ClientID %>");
                }
                else {
                    var elem = document.getElementById("<%= ckBoxBusyoSekkeiPu.ClientID %>");
                }
                var a = elem.getElementsByTagName("input");

                    for (var i = 0; i < a.length; i++) {
                        var c = a[i];
                        if (c.type == "checkbox") {
                            c.checked = flag;
                        }
                    }
        }

        // --------------------------------------------------
        // 関数名   : hyoukaChange
        // 概要     : カテゴリ・評価部署 全選択・全解除
        // 引数     : mode　ON 全選択、OFF 全解除
        // --------------------------------------------------
       function hyoukaChange(mode, puby) {
            var flag = false;

            if (mode == "ON") flag = true;
                if (puby == 'BY'){
                    var elem = document.getElementById("<%= ckBoxBusyoHyoukaBy.ClientID %>");
                }
                else{
                    var elem = document.getElementById("<%= ckBoxBusyoHyoukaPu.ClientID %>");
                }

           var a = elem.getElementsByTagName("input");

            for (var i = 0; i < a.length; i++) {
                var c = a[i];
                if (c.type == "checkbox") {
                    c.checked = flag;
                }
            }
        }

        // --------------------------------------------------
        // 関数名   : buhinChange
        // 概要     : カテゴリ・部品 全選択・全解除
        // 引数     : mode　ON 全選択、OFF 全解除
        // --------------------------------------------------
       function buhinChange(mode) {

               var flag = false;

                if (mode == "ON") flag = true;

                var elem = document.getElementById("<%= ckBoxBuhin.ClientID %>");

                if (elem == null) {
                    document.getElementById("ckBoxBuhinAll").checked = false;
                    return;
                }
                else {
                    var a = elem.getElementsByTagName("input");
                }

                for (var i = 0; i < a.length; i++) {
                    var c = a[i];
                    if (c.type == "checkbox") {
                        c.checked = flag;
                    }
                }
           
        }
       // --------------------------------------------------
       // 関数名   : 
       // 概要     : 入力候補設定
       // 引数     : 
       // --------------------------------------------------
        $(function () {
            var inputauxiliary = [
                <%=(string)ViewState["JisyoInfo"]%>
            ];

            $("#txtSearch").autocomplete({
                source: inputauxiliary
            });

        });

       // --------------------------------------------------
       // 関数名   : 
       // 概要     : インデックス変更処理
       // 引数     : 
       // --------------------------------------------------
       //画面描画時にhiddenフィールドに格納した値を読み込み（グローバル変数として記載）
       var tabIndex = document.getElementById("hdnTabIndex").value;

       function chgTab(strIndex) {
            var tabIndexNew = document.getElementById("hdnTabIndex");
            tabIndexNew.value = strIndex;
            //alert(strIndex);
        }

       // --------------------------------------------------
       // 関数名   : TabAllPU
       // 概要     : 部署（PU）全解除・全選択　タブカラー変更
       // 引数     : 
       // --------------------------------------------------
       function TabAllPU() {
        selectedCheckBoxItem('ckBoxBusyoSekkeiPu<->ckBoxBusyoHyoukaPu', 'lnkKategori03PU');
       }


       // --------------------------------------------------
       // 関数名   : TabAllBY
       // 概要     : 部署（BY）全解除・全選択　タブカラー変更
       // 引数     : 
       // --------------------------------------------------
       function TabAllBY() {
        selectedCheckBoxItem('ckBoxBusyoSekkeiBy<->ckBoxBusyoHyoukaBy', 'lnkKategori03BY');
       }

       // --------------------------------------------------
       // 関数名   : TabAllBuhin
       // 概要     : 部品（Buhin）全解除・全選択　タブカラー変更
       // 引数     : 
       // --------------------------------------------------
       function TabAllBuhin() {
           selectedCheckBoxItem('ckBoxSystem<->ckBoxbuhin', 'lnkKategori04');
       }
  
       // --------------------------------------------------
       // 関数名   : selectedCheckBoxItem
       // 概要     : カテゴリ検索・タブ色変更処理
       // 引数     : ID, Href
       // --------------------------------------------------
        function selectedCheckBoxItem(ID, Href) {

            var flg = false;
            var idList = ID.split('<->');

            for (var j=0;j<idList.length;j++)
            {
                var r = document.getElementById(idList[j]);

                var rc = false;

                if (r == null) {
                    return rc;
                }

                var a = r.getElementsByTagName("input");
                for (var i = 0; i < a.length; i++) {
                    var c = a[i];
                    if (c.type != "checkbox") {
                        continue;
                    }
                    if (c.checked) {
                        flg = true;
                    }
                }
            }
            colorCheckItem(flg,Href);
        }

        // --------------------------------------------------
        // 関数名   : colorCheckItem
        // 概要     : タブカラー変更処理
        // 引数     : 
        // --------------------------------------------------
        function colorCheckItem(flg, Href) {

            if (flg) {

                $("a[ href='#" + Href + "']").addClass("Tab_ColorAdd");
            }
            else {
                $("a[ href='#" + Href + "']").removeClass("Tab_ColorAdd");
            }
        }

        // --------------------------------------------------
        // 関数名   : CategoryAllClear
        // 概要     : カテゴリチェッククリア
        // 引数     : ID, Href
        // --------------------------------------------------
        function CategoryAllClear() {

            CategoryClear(document.getElementById("<%= ckBoxBusyoSekkeiPu.ClientID %>")); //　部署PU　設計 
            CategoryClear(document.getElementById("<%= ckBoxBusyoHyoukaPu.ClientID %>")); //　部署PU　評価
            CategoryClear(document.getElementById("<%= ckBoxBusyoSekkeiBy.ClientID %>")); //　部署BY　設計
            CategoryClear(document.getElementById("<%= ckBoxBusyoHyoukaBy.ClientID %>")); //　部署BY　評価
            CategoryClear(document.getElementById("<%= ckBoxSystem.ClientID %>"));        //　システム
            CategoryClear(document.getElementById("<%= ckBoxMst01.ClientID %>"));         //　開発符号
            CategoryClear(document.getElementById("<%= ckBoxMst02.ClientID %>"));         //　現象（分類）
            CategoryClear(document.getElementById("<%= ckBoxMst03.ClientID %>"));         //　原因（分類）
            CategoryClear(document.getElementById("<%= ckBoxMst04.ClientID %>"));         //　車型特殊
            CategoryClear(document.getElementById("<%= ckBoxMst05.ClientID %>"));         //　現象（制御系）
            CategoryClear(document.getElementById("<%= ckBoxMst06.ClientID %>"));         //　要因（制御系）
            CategoryClear(document.getElementById("<%= ckBoxMst07.ClientID %>"));         //　EGTM形式
            //　部品  部品部位非表示の対応
            elem = document.getElementById("<%= ckBoxBuhin.ClientID %>")
            if (elem!= undefined){
                // クリア
                CategoryClear(document.getElementById("<%= ckBoxBuhin.ClientID %>")); 
            }

        }

        // --------------------------------------------------
        // 関数名   : CategoryClear
        // 概要     : カテゴリチェッククリア
        // 引数     : ID, Href
        // --------------------------------------------------
       function CategoryClear(elem) {

            var a = elem.getElementsByTagName("input");

            for (var i = 0; i < a.length; i++) {
                var c = a[i];
                if (c.type == "checkbox") {
                    c.checked = false;
                }
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
               url=""
                if (kbn == "HELP"){
                    var url = "./frmClientView.aspx?VIEWFILE_KBN=" + "HelpSerch";
                }
                else
                {
                    var url = "./frmClientView.aspx?VIEWFILE_KBN=" + "QaSerch";
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

       function OpenSubWindowMitakaSearch() {
           // 1. 画面のオープン
           var url = "frmMitakaSearch.aspx";
           var w = (screen.width - 1000) / 2;
           var h = (screen.height - 700) / 2;
           var features = "menubar=no,toolbar=no,location=no,resizable=no,scrollbars=yes,status=no,height=700,width=1340,left=" + w + ",top=" + h;
           var frmWatchInstSearchFlg = window.open(url, "frmMitakaSearch", features);
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
        
    </script>
    </html>
