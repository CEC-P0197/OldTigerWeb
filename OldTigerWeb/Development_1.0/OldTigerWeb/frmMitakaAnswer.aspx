<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmMitakaAnswer.aspx.cs" Inherits="OldTigerWeb.frmMitakaAnswer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>過去トラ観たか回答</title>
    <link rel="stylesheet" href= "Content/OldTiger.css" />
</head>
<body>
    <form id="frmMitakaAnswer" runat="server">
        <table class="MitakaAnswerTable">
            <tr>
                <td style="text-align:center">
                    <p class="td_data24" style ="color:#000080;text-align:center" >過去トラ観たか回答</p>
                </td>
                <td>
                    <input type="button" value="閉じる" class="buttoncolor btnSmall" onclick="window.close();" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table class="MitakaAnswerUpperTable">
                        <tr>
                            <th colspan="2">回答内容</th>
                            <td colspan="6">
                                <asp:TextBox ID="TxtMitakaAnswer" runat="server" Text="" TextMode="MultiLine"
                                    style="ime-mode: active" MaxLength="1000" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table class="MitakaAnswerLowerTable">
                        <tr>
                            <th colspan="1">進捗</th>
                            <td colspan="3"><asp:Label ID="LblStatus" runat="server" Text="" /></td>
                            <th colspan="1">項目管理No</th>
                            <td colspan="3"><asp:Label ID="LblManageNo" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">項目</th>
                            <td colspan="7"><asp:Label ID="LblItemNo" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">FMC</th>
                            <td colspan="3"><asp:Label ID="LblFmc" runat="server" Text="" /></td>
                            <th colspan="1">仕向地</th>
                            <td colspan="3"><asp:Label ID="LblPlace" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">現象（内容）</th>
                            <td colspan="7"><asp:Label ID="LblPhenomenon" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">状況</th>
                            <td colspan="7"><asp:Label ID="LblSituation" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">原因</th>
                            <td colspan="7"><asp:Label ID="LblCause" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">対策</th>
                            <td colspan="7"><asp:Label ID="LblCounterMeasure" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="4">開発時の流出要因</th>
                            <th colspan="4">確認の観点</th>
                        </tr>
                        <tr>
                            <td colspan="4"><asp:Label ID="LblFactor" runat="server" Text="" /></td>
                            <td colspan="4"><asp:Label ID="LblPoint" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="4">再発防止対策（設計面）</th>
                            <th colspan="4">再発防止対策（評価面）</th>
                        </tr>
                        <tr>
                            <td colspan="4"><asp:Label ID="LblRelapseSekkei" runat="server" Text="" /></td>
                            <td colspan="4"><asp:Label ID="LblRelapseHyoka" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">重要度ランク</th>
                            <td colspan="3"><asp:Label ID="LblRank" runat="server" Text="" /></td>
                            <th colspan="1">RSC項目</th>
                            <td colspan="3"><asp:Label ID="LblRsc" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">システム(1)</th>
                            <td colspan="3"><asp:Label ID="LblSys1" runat="server" Text="" /></td>
                            <th colspan="1">現象</th>
                            <td colspan="3"><asp:Label ID="LblClassificationPhenomenon" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">部品(1)</th>
                            <td colspan="3"><asp:Label ID="LblPart1" runat="server" Text="" /></td>
                            <th colspan="1">原因</th>
                            <td colspan="3"><asp:Label ID="LblClassificationCause" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">子部品(1)</th>
                            <td colspan="3"><asp:Label ID="LblChildPart1" runat="server" Text="" /></td>
                            <th colspan="1">PUBY区分</th>
                            <td colspan="3"><asp:Label ID="LblPuBy" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">システム(2)</th>
                            <td colspan="3"><asp:Label ID="LblSys2" runat="server" Text="" /></td>
                            <th colspan="1">制御ユニット名称</th>
                            <td colspan="3"><asp:Label ID="LblControlUnit" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">部品(2)</th>
                            <td colspan="3"><asp:Label ID="LblPart2" runat="server" Text="" /></td>
                            <th colspan="1">制御系現象</th>
                            <td colspan="3"><asp:Label ID="LblControlPhenomenon" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">子部品(2)</th>
                            <td colspan="3"><asp:Label ID="LblChildPart2" runat="server" Text="" /></td>
                            <th colspan="1">制御系要因</th>
                            <td colspan="3"><asp:Label ID="LblControlFactor" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">BLK No</th>
                            <td colspan="3"><asp:Label ID="LblBlkNo" runat="server" Text="" /></td>
                            <th colspan="1">車型/特殊</th>
                            <td colspan="3"><asp:Label ID="LblSpCarType" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">部品番号(上5ｹﾀ)</th>
                            <td colspan="3"><asp:Label ID="LblPartNo5Len" runat="server" Text="" /></td>
                            <th colspan="1">重保/法規</th>
                            <td colspan="3"><asp:Label ID="LblLaw" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">EGTM形式</th>
                            <td colspan="3"><asp:Label ID="LblEgtm" runat="server" Text="" /></td>
                            <th colspan="1">外製</th>
                            <td colspan="3"><asp:Label ID="LblMadeOutSide" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">排気量</th>
                            <td colspan="3"><asp:Label ID="LblDisplacement" runat="server" Text="" /></td>
                            <th colspan="1">製造</th>
                            <td colspan="3"><asp:Label ID="LblProduct" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">設計</th>
                            <td colspan="7"><asp:Label ID="DepartmentSekkei" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">評価</th>
                            <td colspan="7"><asp:Label ID="DepartmentHyoka" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">設通No</th>
                            <td colspan="7"><asp:Label ID="LblEcsNo" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">資料No</th>
                            <td colspan="7"><asp:Label ID="LblDocumentNo" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">関連項目管理No</th>
                            <td colspan="7"><asp:Label ID="LblRelationManageNo" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">キーワード</th>
                            <td colspan="7"><asp:Label ID="LblKeyword" runat="server" Text="" /></td>
                        </tr>
                        <tr>
                            <th colspan="1">Reliability</th>
                            <td colspan="7"><asp:Label ID="LblReliability" runat="server" Text="" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

    <%--<p class="td_data24" style ="color:#000080" />　　　　　　　　　　　　　　　　ＦＭＣ・ｍｃフォロー情報回答--%>　　　　　　　　
<%--        <asp:Button ID="btnRegist" runat="server" text="登録"   Height="30px" Width="100px" 
            OnClientClick="if (confRegistCheck() == false) { return false;} else { return true;}" OnClick="btnRegist_Click" /></p>--%>
            
<%--      <asp:Panel ID="pnlDetail" runat="server">

        <table class="syosaiTable">
          <tbody>
            <tr>
                <th>ヒヤリング要望</th>
                <td><asp:Label ID="lblHearing" runat="server" Text="" Width="15px"  /></td>
            </tr>
            <tr>
                <th>進度</th>
                <td>
                    <asp:RadioButton ID="rdoSindo1" runat="server" GroupName="Sindo" Text="織込済" />
                    <asp:RadioButton ID="rdoSindo2" runat="server" GroupName="Sindo" Text="検討･調整中" />
                    <asp:RadioButton ID="rdoSindo3" runat="server" GroupName="Sindo" Text="未確認" />
                    <asp:RadioButton ID="rdoSindo4" runat="server" GroupName="Sindo" Text="適用外（理由を対応内容に記載し登録）" />
                </td>
            </tr>
            <tr>
                <th>回答内容<input type="button" name="btnOtherDept" value="他部署回答" style="height:30px;width:90px" onclick="openAnswerSubWindow()" /> 
                </th>
                <td><asp:TextBox ID="txtAnswer" runat="server" Text="" TextMode="MultiLine" style="ime-mode: active" Width="600px" Height="100px" MaxLength="1000" /></td>
            </tr>
          </tbody>
        </table>

        <table class="syosaiTable">
          <tbody>
            <tr>
                <th>項目管理No</th>
                <td><asp:Label ID="lblKOUMOKU_KANRI_NO" runat="server" Text="" /></td>
                <th>フォロー状況</th>
                <td><asp:Label ID="lblFOLLOW_INFO" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>項目</th>
                <td colspan="3"><asp:Label ID="lblKOUMOKU" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>FMC</th>
                <td><asp:Label ID="lblFUGO_NO" runat="server" Text="" /></td>
                <th>仕向地</th>
                <td><asp:Label ID="lblSIMUKECHI_NAME" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>現象（内容）</th>
                <td colspan="3"><asp:Label ID="lblGENSYO_NAIYO" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>状況</th>
                <td colspan="3"><asp:Label ID="lblJYOUKYO" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>原因</th>
                <td colspan="3"><asp:Label ID="lblGENIN" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>対策</th>
                <td colspan="3"><asp:Label ID="lblTAISAKU" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th colspan="2">開発時の流出要因</th>
                <th colspan="2">確認の観点</th>
            </tr>
            <tr>
                <td colspan="2"><asp:Label ID="lblKAIHATU_MIHAKKEN_RIYU" runat="server" Text="" /></td>
                <td colspan="2"><asp:Label ID="lblSQB_KANTEN" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th colspan="2">再発防止策（設計面）</th>
                <th colspan="2">再発防止策（評価面）</th>
            </tr>
            <tr>
                <td colspan="2"><asp:Label ID="lblSAIHATU_SEKKEI" runat="server" Text="" /></td>
                <td colspan="2"><asp:Label ID="lblSAIHATU_HYOUKA" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>重要度ランク</th>
                <td><asp:Label ID="lblRANK" runat="server" Text="" /></td>
                <th>RSC項目</th>
                <td><asp:Label ID="lblRSC" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>システム(1)</th>
                <td><asp:Label ID="lblSYSTEM_NAME1" runat="server" Text="" /></td>
                <th>現象</th>
                <td><asp:Label ID="lblBUNRUI_GENSYO_NAME" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>部品(1)</th>
                <td><asp:Label ID="lblBUHIN_NAME1" runat="server" Text="" /></td>
                <th>原因</th>
                <td><asp:Label ID="lblBUNRUI_CASE_NAME" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>子部品(1)</th>
                <td><asp:Label ID="lblKOBUHIN_NAME1" runat="server" Text="" /></td>
                <th>PUBY区分</th>
                <td><asp:Label ID="lblBY_PU" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>システム(2)</th>
                <td><asp:Label ID="lblSYSTEM_NAME2" runat="server" Text="" /></td>
                <th>制御ユニット名称</th>
                <td><asp:Label ID="lblSEIGYO_UNIT_NAME" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>部品(2)</th>
                <td><asp:Label ID="lblBUHIN_NAME2" runat="server" Text="" /></td>
                <th>制御系現象</th>
                <td><asp:Label ID="lblSEIGYO_GENSYO_NAME" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>子部品(2)</th>
                <td><asp:Label ID="lblKOBUHIN_NAME2" runat="server" Text="" /></td>
                <th>制御系要因</th>
                <td><asp:Label ID="lblSEIGYO_FACTOR_NAME" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>BLK No</th>
                <td><asp:Label ID="lblBLKNO" runat="server" Text="" /></td>
                <th>車型/特殊</th>
                <td><asp:Label ID="lblKATA_NAME" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>部品番号(上5ｹﾀ)</th>
                <td><asp:Label ID="lblBUHIN_BANGO" runat="server" Text="" /></td>
                <th>重保/法規</th>
                <td><asp:Label ID="lblJYUYO_HOUKI" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>EGTM形式</th>
                <td><asp:Label ID="lblEGTM_NAME" runat="server" Text="" /></td>
                <th>外製</th>
                <td><asp:Label ID="lblSYUMU_GAISEI" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>排気量</th>
                <td><asp:Label ID="lblHAIKI_NAME" runat="server" Text="" /></td>
                <th>製造</th>
                <td><asp:Label ID="lblSYUMU_SEIZO" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>設計</th>
                <td colspan="3"><asp:Label ID="lblBUSYO_SEKKEI" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>評価</th>
                <td colspan="3"><asp:Label ID="lblBUSYO_HYOUKA" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>設通No</th>
                <td colspan="3"><asp:Label ID="lblSETTU_NO" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>資料No</th>
                <td colspan="3"><asp:Label ID="lblSIRYOU_NO" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>関連資料</th>
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <th>関連項目管理No</th>
                <td colspan="3"><asp:Label ID="lblKANREN_KANRI_NO" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>キーワード</th>
                <td colspan="3"><asp:Label ID="lblKEYWORD" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>Reliability</th>
                <td colspan="3"><asp:Label ID="lblRELIABILITY" runat="server" Text="" /></td>
            </tr>
            <tr>
                <th>ｸﾚｰﾑ費/TOP40</th>
                <td><asp:Label ID="lblKRAME" runat="server" Text="" /></td>
                <th>リプロ/TOP20</th>
                <td><asp:Label ID="lblRIPRO" runat="server" Text="" /></td>
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
        <asp:HiddenField runat="server" ID ="hdnSystemNo"/>--%>

