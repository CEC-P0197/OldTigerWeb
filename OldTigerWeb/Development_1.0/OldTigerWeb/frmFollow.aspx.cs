using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using OldTigerWeb.Const;

namespace OldTigerWeb
{
    public partial class frmFollow : System.Web.UI.Page
    {
        #region "フィールド"
        CommonLogic bcom = new CommonLogic();
        CommonPageLogic cPageLogic = new CommonPageLogic();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            // ポストバック時はリターン
            if (IsPostBack == true)
            {
                return;
            }

            ViewState["HELP"] = "";
            ViewState["QA"] = "";

            CommonLogic bcom = new CommonLogic();

            try
            {
                BuisinessLogic.BLSearch bLogic = new BuisinessLogic.BLSearch();

                Boolean bRet = false;

                // 画面表示処理
                initialDisp();

                // Windowsログイン・ユーザマスタチェック
                bRet = bcom.CheckUser();
                if (bRet)
                {
                    lnkTop.Enabled = false;
                    btn_Kaito.Enabled = false;
                    btn_Download.Enabled = false;

                    ClientScriptManager csManager = Page.ClientScript;
                    Type csType = this.GetType();
                    ArrayList arrayMessage = new ArrayList();
                    arrayMessage.Add(Const.Def.DefMsg_USERERR);
                    bcom.ShowMessage(csType, csManager, arrayMessage);
                }
                else
                {
                    // 2017.04.04 ta_kanda マニュアルとＱＡを分離 Start
                    String helpForder = bcom.GetHelpForder("FH");
                    String helpUrl = "file:";

                    if (helpForder.Trim() != "")
                    {
                        if (bcom.CheckFile(helpForder.Trim()) == 0)
                        {
                            helpUrl += helpForder.Trim().Replace("\\", "/");
                            ViewState["HELP"] = helpUrl;
                        }
                    }
                    String qaForder = bcom.GetHelpForder("FQ");
                    String qaUrl = "file:";

                    if (qaForder.Trim() != "")
                    {
                        if (bcom.CheckFile(qaForder.Trim()) == 0)
                        {
                            qaUrl += qaForder.Trim().Replace("\\", "/");
                            ViewState["QA"] = qaUrl;
                        }
                    }
                    // 2017.04.04 ta_kanda マニュアルとＱＡを分離 End

                    DataTable work_t = bcom.GetUser();

                    if (work_t.Rows.Count > 0)
                    {
                        ViewState["MailAddr"] = bcom.GetMailAddress(work_t.Rows[0]["BY_PU"].ToString());
                    }
                }
            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmFollow", "Page_Load", ex, this.Response);
            }
        }

        // ＴＯＰページへクリック
        protected void lnkTop_Click(Object sender, EventArgs e)
        {
            CommonLogic bcom = new CommonLogic();

            try
            {
                Response.Redirect("frmTopPage.aspx", false);
            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmFollow", "lnkTop_Click", ex, this.Response);
            }
        }

        // 回答ボタンクリック
        protected void btn_Kaito_Click(Object sender, EventArgs e)
        {
            CommonLogic bcom = new CommonLogic();

            try
            {
                string event_code = "";

                if (ckBoxFmc.SelectedIndex > -1)
                {
                    event_code = ckBoxFmc.SelectedItem.Value;
                }
                else
                {
                    if (ckBoxmc.SelectedIndex > -1)
                    {
                        event_code = ckBoxmc.SelectedItem.Value;
                    }
                }

                Session[Const.Def.DefPARA_FOLLOW] = event_code;

                openWindow();       // フォロー情報回答画面オープン
            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmFollow", "btn_Kaito_Click", ex, this.Response);
            }
        }

        // ダウンロードボタンクリック
        protected void btn_Download_Click(Object sender, EventArgs e)
        {
            CommonLogic bcom = new CommonLogic();

            try
            {
                string event_code = "";

                if (ckBoxFmc.SelectedIndex > -1)
                {
                    event_code = ckBoxFmc.SelectedItem.Value;
                }
                else
                {
                    if (ckBoxmc.SelectedIndex > -1)
                    {
                        event_code = ckBoxmc.SelectedItem.Value;
                    }
                    else
                    {
                        if (ckBoxOverFmc.SelectedIndex > -1)
                        {
                            event_code = ckBoxOverFmc.SelectedItem.Value;
                        }
                        else
                        {
                            if (ckBoxOvermc.SelectedIndex > -1)
                            {
                                event_code = ckBoxOvermc.SelectedItem.Value;
                            }
                        }
                    }
                }

                BuisinessLogic.BLFollow bLogic = new BuisinessLogic.BLFollow();

                DataTable followList = null;
                String paraTitle = "";
                String[] strArrayData;

                strArrayData = event_code.Trim().Split(',');

                // 2017/07/14 Add Start
                // 部展開の場合、入力された課コードから部コードを取得
                DataTable buCode = null;

                if (strArrayData[6] == Const.Def.BuTenkai)
                {
                    buCode = bcom.GetBuCode(txtKacode.Text.Trim());

                    if (buCode.Rows.Count > 0)
                    {
                        txtKacode.Text = buCode.Rows[0]["BU_CODE"].ToString();
                    }
                }
                // 2017/07/14 Add End

                // 過去トラ一覧リスト取得
                followList = bLogic.GetDownloadList(strArrayData[0], strArrayData[1],
                                        strArrayData[2], strArrayData[3], txtKacode.Text.Trim());

                paraTitle = "《 " + strArrayData[4] + " 》段階結果（ " + txtKacode.Text.Trim() + " ）";

                // TemplateのFileInfo EXCELテンプレートフルパス
                FileInfo template = new FileInfo(@bcom.GetExcelTemplate("F"));

                // 作成EXCELのFileInfo
                FileInfo newFile = new FileInfo(Const.Def.DefFollowExcelName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");

                // EXCEL作成
                using (ExcelPackage excelPkg = new ExcelPackage(newFile, template))
                {
                    ExcelWorksheet worksheet = null;
                    worksheet = excelPkg.Workbook.Worksheets.Where(s => s.Name == Const.Def.DefFollowWorksheetName).FirstOrDefault();

                    //処理を記述
                    bLogic.CreateFollowList(worksheet, paraTitle, strArrayData[5], followList);

                    // ダウンロード処理
                    String fn = Const.Def.DefFollowExcelName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", String.Format("attachment; filename=" + HttpUtility.UrlDecode(fn)));
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.BinaryWrite(excelPkg.GetAsByteArray());
                    Response.Flush();
                    Response.End();
                }


            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmFollow", "btn_Download_Click", ex, this.Response);
            }
        }

        #region 固有関数

        #region 画面表示処理
        /// <summary>
        /// 画面表示処理
        /// </summary>
        protected void initialDisp()
        {
            BuisinessLogic.BLFollow bLogic = new BuisinessLogic.BLFollow();

            DataTable result = null;

            // イベント期間内・ＦＭＣ
            result = bLogic.GetFollowList(Const.Def.DefTYPE_FMC, Const.Def.DefTYPE_Now);

            ckBoxFmc.DataSource = result;
            ckBoxFmc.DataBind();

            // イベント期間内・ｍｃ
            result = bLogic.GetFollowList(Const.Def.DefTYPE_mc, Const.Def.DefTYPE_Now);

            ckBoxmc.DataSource = result;
            ckBoxmc.DataBind();

            // イベント期間外・ＦＭＣ
            result = bLogic.GetFollowList(Const.Def.DefTYPE_FMC, Const.Def.DefTYPE_Old);

            ckBoxOverFmc.DataSource = result;
            ckBoxOverFmc.DataBind();

            // イベント期間外・ｍｃ
            result = bLogic.GetFollowList(Const.Def.DefTYPE_mc, Const.Def.DefTYPE_Old);

            ckBoxOvermc.DataSource = result;
            ckBoxOvermc.DataBind();

        }
        #endregion

        #region FMC／MCフォロー情報回答画面表示
        /// <summary>
        /// Window Open 処理
        /// </summary>
        protected void openWindow()
        {
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;

            string strScr = cPageLogic.openWindow(Def.DefPageId_FollowAnswer);

            cs.RegisterStartupScript(cstype, "OpenSubWindow", strScr);
        }

        #endregion

        #endregion
    }
}