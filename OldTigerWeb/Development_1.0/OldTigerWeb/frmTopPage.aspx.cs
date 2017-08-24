using System;
using System.Collections.Generic;
using System.Configuration;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Data;
using System.Text;
using Microsoft.VisualBasic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;

namespace OldTigerWeb
{
    public partial class frmTopPage : System.Web.UI.Page
    {
        public string userSight;
        public bool disFlgCommon = false, disFlgTopicTokyo = false, disFlgTopicGunma = false;

        #region 初期表示
        /// <summary>
        /// 初期表示
        /// </summary>
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
                Boolean bRet = false;

                // Windowsログイン・ユーザマスタチェック
                bRet = bcom.CheckUser();
                if (bRet)
                {
                    btn_Search.Enabled = false;
                    btn_Follow.Enabled = false;
                  
                    //btn_ChangeImage.Enabled= false;
                    //btnShow.Style["visibility"] = "hidden";

                    ClientScriptManager csManager = Page.ClientScript;
                    Type csType = this.GetType();
                    ArrayList arrayMessage = new ArrayList();
                    arrayMessage.Add(Def.DefMsg_USERERR);
                    bcom.ShowMessage(csType, csManager, arrayMessage);
                }
                else
                {
                    btn_Search.Enabled = true;
                    btn_Follow.Enabled = true;
                    //btn_ChangeImage.Enabled = true;
                    // 2017.04.03 ta_kanda 追加 Start
                    String helpForder = bcom.GetHelpForder("TH");
                    String helpUrl = "file:";

                    if (helpForder.Trim() != "")
                    {
                        //if (bcom.CheckFolder(helpForder.Trim()) == 0)
                        if (bcom.CheckFile(helpForder.Trim()) == 0)
                        {
                            helpUrl += helpForder.Trim().Replace("\\", "/");
                            ViewState["HELP"] = helpUrl;
                        }
                    }
                    String qaForder = bcom.GetHelpForder("TQ");
                    String qaUrl = "file:";

                    if (qaForder.Trim() != "")
                    {
                        if (bcom.CheckFile(qaForder.Trim()) == 0)
                        {
                            qaUrl += qaForder.Trim().Replace("\\", "/");
                            ViewState["QA"] = qaUrl;
                        }
                    }
                    // 2017.04.03 ta_kanda 追加 End

                
                }

                // 掲示板テキストの取得
                readTxt("common");
                readTxt("sample");

                //ユーザーのサイト情報取得
                DataTable userInfo = bcom.GetUser();
                hdnPageUrl.Value = Request.Url.AbsoluteUri;

                if (userInfo.Rows.Count > 0)
                {

                    string userSight = userInfo.Rows[0]["BY_PU"].ToString();
                    hdnUserInfo.Value = userSight;
                    if (userSight == "BY")
                    {
                        // 掲示板テキストの取得
                        readTxt("topicGunma");
                    }
                    else
                    {
                        // 掲示板テキストの取得
                        readTxt("topicTokyo");
                        hdnTabIndex.Value = "1";
                    }
                    string sqbFg = userInfo.Rows[0]["SQB_FLG"].ToString();
                    if (sqbFg == "1")
                    {
                        hdnSqbFg.Value = "1";
                    }
                }
            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmTopPage", "Page_Load", ex, this.Response);

            }
        }
        #endregion

        #region 過去トラ検索ボタン押下
        /// <summary>
        /// 過去トラ検索ボタン押下
        /// </summary>
        protected void btn_Search_Click(object sender, EventArgs e)
        {
            CommonLogic bcom = new CommonLogic();

            try
            {
                Response.Redirect("frmSearch.aspx", false);
            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmTopPage", "btn_Search_Click", ex, this.Response);
            }
        }
        #endregion

        #region ＦＭＣ／ＭＣ進捗ボタン押下
        /// <summary>
        /// ＦＭＣ／ＭＣ進捗ボタン押下
        /// </summary>
        protected void btn_Follow_Click(object sender, EventArgs e)
        {
            CommonLogic bcom = new CommonLogic();

            try
            {
                Response.Redirect("frmFollow.aspx", false);
            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmTopPage", "btn_Follow_Click", ex, this.Response);
            }
        }
        #endregion

        #region 掲示板テキスト取得
        /// <summary>
        /// Web.Configより掲示板テキストフォルダパスを取得
        /// </summary>
        protected void readTxt(string sight)
        {
            CommonLogic bcom = new CommonLogic();
            
            try
            {
                String infoFilePath, txtSight,disSight ="",disFlg ="",text="";
                if (sight.Equals("common"))
                {
                    txtSight = "txtFileCommon";
                    disSight = "commonDisplayflg";
                }
                else if (sight.Equals("topicTokyo"))
                {
                    txtSight = "txtFileTopicTokyo";
                    disSight = "topicTokyoDisplayflg";
                }
                else if (sight.Equals("topicGunma"))
                {
                    txtSight = "txtFileTopicGunma";
                    disSight = "topicGunmaDisplayflg";
                }
                else
                {
                    txtSight = "txtSample";
                }

                if (disSight != "") {
                    disFlg = System.Web.Configuration.WebConfigurationManager.AppSettings[disSight];
                }

                int newCount = int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["bbNewDayCount"]);

                if ((disFlg == "1") || (sight.Equals("sample")))
                {
                    infoFilePath = System.Web.Configuration.WebConfigurationManager.AppSettings[txtSight];
                    string txtInfo = HttpContext.Current.Server.MapPath(infoFilePath);
                    //string text = System.IO.File.ReadAllText(@txtInfoGunma);
                    System.IO.StreamReader sr = new System.IO.StreamReader(@txtInfo, System.Text.Encoding.GetEncoding("shift_jis"));

                    //内容をすべて読み込む
                    text = sr.ReadToEnd();

                    //txtファイルを解放する
                    sr.Close();
                    sr.Dispose();
                }
                else
                {
                    text = "編集不可";
                }
                if (sight.Equals("common"))
                {
                    txtCommon.Text = text;
                    if (disFlg != "1")
                    {
                        txtCommon.ReadOnly = true;
                        btnCommon.Visible = false;
                        disFlgCommon = true;
                    }
                }
                else if (sight.Equals("topicTokyo"))
                {
                    titleTopics.InnerText = titleTopics.InnerText + "(東京)";
                    txtTopicTokyo.Text = text;
                    if (disFlg != "1")
                    {
                        txtTopicTokyo.ReadOnly= true;
                        btnTopicTokyo.Visible = false;
                        disFlgTopicTokyo = true;
                    }
                }
                else if (sight.Equals("topicGunma"))
                {
                    titleTopics.InnerText = titleTopics.InnerText + "(群馬)";
                    txtTopicGunma.Text = text;
                    if (disFlg != "1")
                    {
                        txtTopicGunma.ReadOnly = true;
                        btnTopicGunma.Visible = false;
                        disFlgTopicGunma = true;
                    }
                }


                string html = "";
                string strRows;                
                string[] strMoji;
                String[] textList = new String[]{"","",""};
                DateTime nowTime = DateTime.Now;
                DateTime writeDate;
                TimeSpan diff;
                int rowMaxLength = 2;
                if ((disFlg == "1") || (sight.Equals("sample")))
                {
                    text = text.Trim().ToString().Replace(Environment.NewLine, "\n");
                    textList = text.Trim().Split('\n');
                }

                html = html + "<div class='divTable divTabletable-responsive' style='width: 100%;height:100%;'>";
                
                if (sight.Equals("common"))
                {
                    html = html + "<table id='tableCommon' ";
                }
                else if (sight.Equals("topicTokyo"))
                {
                    html = html + "<table id='tableTopic' ";
                }
                else if (sight.Equals("topicGunma"))
                {
                    html = html + "<table id='tableTopic' ";
                }
                else
                {
                    html = html + "<table id='tableSample' ";
                }

                if (!sight.Equals("sample"))
                {
                    if (disFlg == "1") 
                    { 
                        html = html + "style='padding:0%;width:100%' class='table table-condensed display compact nowrap'>";
                        html = html + "<thead style='display:none;'>";
                        html = html + "<tr><th style='width: 20px;'></th><th style='width: 60px;'>掲載日時</th><th>掲載内容</th></tr>";
                    }
                    else
                    {
                        html = html + "style='padding:0%;width:100%' class='table table-condensed display compact nowrap'>";
                        html = html + "<thead style='display:none;'>";
                        html = html + "<tr><th></th></tr>";
                    }
                }
                else 
                {
                    html = html + "style='padding:0%;width:100%' class='table table-condensed display compact nowrap　cell-border'>";
                    html = html + "<thead style='display:none;'>";
                    html = html + "<tr><th>掲載日時</th><th>掲載内容</th></tr>";
                }
                html = html + "</thead>";
                html = html + "<tbody>";

                if ((disFlg != "1") &&(!sight.Equals("sample")))
                {
                    html = html + "<tr><th>" + System.Web.Configuration.WebConfigurationManager.AppSettings["nonDispMessage"] + "</th></tr>";
                }
                else if (textList.Length > 0)
                {
                    for (int i = 0; i < textList.Length; i++)
                    {
                        int changeSort = textList.Length - i - 1;
                        if ((textList[changeSort].Trim().ToString() != "") && (textList[changeSort].Trim().ToString().Substring(0, 4) != "<%--"))
                        {
                            html = html + "<tr>";   
                            strRows = textList[changeSort].Trim().ToString();
                            strMoji = strRows.Split(',');

                            for (int j = 0; j < rowMaxLength; j++)
                            {
                                if ((j == 0) && (!sight.Equals("sample")))
                                {

                                    string weekName = "";
                                    try 
                                    {
                                        // 2017/02/19 t.kanda 掲示板から日時を削除
                                        writeDate = DateTime.ParseExact(strMoji[j].Trim().ToString(), "yyyy/MM/dd", null);
                                        //writeDate = DateTime.ParseExact(strMoji[j].Trim().ToString(), "yyyy/MM/dd HH:mm", null);
                                        weekName = GetWeek(writeDate);
                                        diff = nowTime - writeDate;
                                        if (diff.Days < newCount)
                                        {
                                            html = html + "<th  style='width: 20px;'>" + "<img SRC='/Images/new.gif' border='0' WIDTH='20' HEIGHT='10'></th>";
                                            // 2017/02/19 t.kanda 掲示板から日時を削除
                                            html = html + "<th with style='width: 70px;text-align:left; color: blue;'>" + strMoji[j].Trim().ToString() + weekName + "</th>";
                                        }
                                        else
                                        {
                                            html = html + "<th style='width: 20px;'></th>";
                                            // 2017/02/19 t.kanda 掲示板から日時を削除
                                            html = html + "<th with style='width: 70px;text-align:left; '>" + strMoji[j].Trim().ToString() + weekName + "</th>";
                                        }
                                    }
                                    catch
                                    {
                                        html = html + "<th style='width: 20px;'></th>";
                                    }
                                    // 2017/02/19 t.kanda 掲示板から日時を削除
                                    //html = html + "<th with style='width: 70px;text-align:left; color: blue;'>" + strMoji[j].Trim().ToString() + weekName + "</th>";
                                }
                                else
                                {
                                    html = html + "<th style='text-align:left;'>" + strMoji[j].Trim().ToString() + "</th>";
                                }

                            }
                            html = html + "</tr>";
                        }
                    }
                }
                html = html + "</tbody>";
                html = html + "</table>";
                html = html + "</div>";

                if (sight.Equals("common"))
                {
                    infoCommon.InnerHtml = html;
                }
                else if (sight.Equals("topicTokyo"))
                {
                    infoTopic.InnerHtml = html;
                }
                else if (sight.Equals("topicGunma"))
                {
                    infoTopic.InnerHtml = html;
                }
                else
                {
                    sampleDiv.InnerHtml = html;
                }
            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmTopPage", "readTxt", ex, this.Response);
            }
        }
        #endregion
        
        #region 曜日取得処理
        /// <summary>
        /// 曜日取得処理
        /// </summary>
        protected string GetWeek(DateTime day)
        {
            string week="";
            switch (day.DayOfWeek){
                case DayOfWeek.Sunday:
                    week = "(日)";
                    break;
                case DayOfWeek.Monday:
                    week = "(月)";
                    break;
                case DayOfWeek.Tuesday:
                    week = "(火)";
                    break;
                case DayOfWeek.Wednesday:
                    week = "(水)";
                    break;
                case DayOfWeek.Thursday:
                    week = "(木)";
                    break;
                case DayOfWeek.Friday:
                    week = "(金)";
                    break;
                case DayOfWeek.Saturday:
                    week = "(土)";
                    break;
                default:
                    week = "";
                    break;
            }

            return week;
        }
        #endregion

        #region 共通掲示板編集ボタン押下
        /// <summary>
        /// 共通掲示板編集ボタン押下
        /// </summary>
        protected void btnCommon_click(object sender, EventArgs e)
        {
            CommonLogic bcom = new CommonLogic();

            try
            {
                saveTxt("txtFileCommon");
            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmTopPage", "btnCommon_click", ex, this.Response);
            }
        }
        #endregion

        #region 東京掲示板編集ボタン押下
        /// <summary>
        /// 東京掲示板編集ボタン押下
        /// </summary>
        protected void btnTopicTokyo_click(object sender, EventArgs e)
        {
            CommonLogic bcom = new CommonLogic();

            try
            {
                saveTxt("txtFileTopicTokyo");
            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmTopPage", "btnTopicTokyo_click", ex, this.Response);
            }
        }
        #endregion

        #region 群馬掲示板編集ボタン押下
        /// <summary>
        /// 群馬掲示板編集ボタン押下
        /// </summary>
        protected void btnTopicGunma_click(object sender, EventArgs e)
        {
            CommonLogic bcom = new CommonLogic();

            try
            {
                saveTxt("txtFileTopicGunma");
            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmTopPage", "btnTopicGunma_click", ex, this.Response);
            }
        }
        #endregion

        #region テキストファイル保存処理
        /// <summary>
        /// テキストファイル保存処理
        /// </summary>
        protected void saveTxt(string sight)
        {
            CommonLogic bcom = new CommonLogic();

            try
            {
                string txtSight = sight;
                string folderPath = System.Web.Configuration.WebConfigurationManager.AppSettings[txtSight];
                string filePath = HttpContext.Current.Server.MapPath(folderPath);
                System.Text.Encoding enc = System.Text.Encoding.GetEncoding("SHIFT_JIS");
                System.IO.StreamWriter sr = new System.IO.StreamWriter(filePath, false, enc);
                string upData;
                if (sight.Equals("txtFileCommon"))
                {
                    upData = txtCommon.Text.ToString();
                }
                else if (sight.Equals("txtFileTopicTokyo"))
                {
                    upData = txtTopicTokyo.Text.ToString();
                }
                else if (sight.Equals("txtFileTopicGunma"))
                {
                    upData = txtTopicGunma.Text.ToString();
                }
                else {
                    upData = "";
                }

                sr.Write(upData);
                sr.Close();

                System.Text.StringBuilder script = new System.Text.StringBuilder();
                script.Append("<script language='javascript'>");
                script.Append("window.alert('更新完了');");
                script.Append("var url = document.getElementById('hdnPageUrl').value;");
                script.Append("location.href = url;");
                script.Append("</script>");
                RegisterStartupScript("save complete bbord", script.ToString());
                //Response.Redirect(Request.FilePath, false);

            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmTopPage", "saveTxt", ex, this.Response);
            }
        }
        #endregion

        #region 背景変更ボタン押下
        /// <summary>
        /// 背景変更ボタン押下
        /// </summary>
        protected void btn_ChangeImage_Click(object sender, EventArgs e)
        {
            CommonLogic bcom = new CommonLogic();

            try
            {

                Response.Redirect("frmChangeImage.aspx", false);
                

                //frmChangeImage frmChangeImageObj; // 子ﾌｫｰﾑ
                //frmChangeImageObj = new frmChangeImage(); // 子ﾌｫｰﾑ生成
                //frmChangeImageObj.SetFocus(); // 子ﾌｫｰﾑを表示

            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmTopPage", "btn_ChangeImage_Click", ex, this.Response);
            }
        }
        #endregion
        
        #region 再描画ボタン押下処理
        /// <summary>
        /// 再描画ボタン押下処理
        /// </summary>
        protected void btnShow_Click(object sender, EventArgs e)
        {
            CommonLogic bcom = new CommonLogic();

            try
            {
                Response.Redirect("frmTopPage.aspx");

            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmTopPage", "btnShow_click", ex, this.Response);
            }
        }
        #endregion
        #region ユーザー登録用紙ダウンロード
        //20170318 機能改善 END
        /// <summary>
        /// ユーザー登録用紙ダウンロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 
        protected void btn_btnDownloadUser_Click(Object sender, EventArgs e)
        {
            CommonLogic bcom = new CommonLogic(); 

            string rootfilePath = System.Web.Configuration.WebConfigurationManager.AppSettings["UserEntryFormatPath"];
            string fileName = System.Web.Configuration.WebConfigurationManager.AppSettings["UserEntryFormat"];
            // ファイル絶対パス設定（拡張子無し）
            //string[] pathList = value.Split(',');
            string filePath = rootfilePath;
            string fileFullPath = "";
            //string fileExtension = "";
            string dlFileName = HttpUtility.UrlEncode(fileName);
            //ファイル存在チェック

            fileFullPath = filePath + fileName;
            if (bcom.CheckFile(fileFullPath.Trim()) == 0)
            {
                // Response情報クリア
                Response.ClearContent();
                // バファリング
                Response.Buffer = true;
                // HTTPヘッダー情報設定
                Response.AddHeader("Content-Disposition", string.Format ("attachment; filename={0}", dlFileName));
                Response.ContentType = "aapplication/msexcel";
                // ファイル書込(データによりResponse.WriteFile()、Response.Write()、Response.BinaryWrite()を使い分ける
                Response.WriteFile(fileFullPath);
                // フラッシュ
                Response.Flush();
                // レスポンス終了
                Response.End();
            }
        }
        #endregion
    }
}
    