using System;
using System.Linq;
using System.Data;
using System.Collections;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using OldTigerWeb.BuisinessLogic;

namespace OldTigerWeb
{
    public partial class frmSearch : System.Web.UI.Page
    {
        #region フィールド
                    CommonLogic bcom = new CommonLogic();
                    CommonPageLogic cPageLogic = new CommonPageLogic();
        #endregion

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
  
            pnlCategoryBusyo.Visible = false;

            ViewState["MailAddr"] = "";
            ViewState["HELP"] = "";
            ViewState["QA"] = "";

            txtSearch.Focus();            

            try
            {
                Boolean bRet = false;

                // 画面表示処理
                initialDisp();

                // Windowsログイン・ユーザマスタチェック
                bRet = bcom.CheckUser();
                if (bRet)
                {
                    lnkTop.Enabled = false;
                    btnSearch.Enabled = false;
                    
                    lnkTop01.Enabled = false;
                    lnkTop02.Enabled = false;
                    lnkTop03.Enabled = false;
                    lnkTop04.Enabled = false;
                    lnkTop05.Enabled = false;
                    lnkTop06.Enabled = false;
                    lnkTop07.Enabled = false;
                    lnkTop08.Enabled = false;
                    lnkTop09.Enabled = false;
                    lnkTop10.Enabled = false;

                    //btnFollow.Enabled = false;　2017.03.29 神田 画面から削除

                    ClientScriptManager csManager = Page.ClientScript;
                    Type csType = this.GetType();
                    ArrayList arrayMessage = new ArrayList();
                    arrayMessage.Add(Def.DefMsg_USERERR);
                    bcom.ShowMessage(csType, csManager, arrayMessage);
                    

                }
                else
                {
                    // 2017.04.04 ta_Kanda マニュアルとQAを分離 Start
                    String helpForder = bcom.GetHelpForder("SH");
                    String helpUrl = "file:";
                    if (helpForder.Trim() != "")
                    {
                        if (bcom.CheckFile(helpForder.Trim()) == 0)
                        {
                            helpUrl += helpForder.Trim().Replace("\\", "/");
                            ViewState["HELP"] = helpUrl;
                        }
                    }
                    String qaForder = bcom.GetHelpForder("SQ");
                    String qaUrl = "file:";

                    if (qaForder.Trim() != "")
                    {
                        if (bcom.CheckFile(qaForder.Trim()) == 0)
                        {
                            qaUrl += qaForder.Trim().Replace("\\", "/");
                            ViewState["QA"] = qaUrl;
                        }
                    }
                    // 2017.04.04 ta_Kanda マニュアルとQAを分離 End

                    DataTable work_t = bcom.GetUser();

                    if (work_t.Rows.Count > 0)
                    {
                        ViewState["MailAddr"] = bcom.GetMailAddress(work_t.Rows[0]["BY_PU"].ToString());
                    }
                }

                //String BYPU = "BY";
                CategoryProc("31", "BY");   // BY部署・設計
                CategoryProc("32", "BY");   // BY部署・評価

                CategoryProc("31", "PU");   // PU部署・設計
                CategoryProc("32", "PU");   // PU部署・評価

                CategoryProc(Def.DefTYPE_PARTS, "");      // 部品

                ckBoxBuhin.DataSource = "";
                ckBoxBuhin.DataBind();

                CategoryProc(Def.DefTYPE_KAIHATU, "");    // 開発符号

                CategoryProc(Def.DefTYPE_GENSYO, "");     // 現象（分類）

                CategoryProc(Def.DefTYPE_GENIN, "");      // 原因（分類）

                CategoryProc(Def.DefTYPE_SYAKATA, "");    // 車型特殊

                CategoryProc(Def.DefTYPE_SGENSYO, "");    // 現象（制御系）

                CategoryProc(Def.DefTYPE_SYOUIN, "");     // 要因（制御系）

                CategoryProc(Def.DefTYPE_EGTM, "");       // EGTM形式

                //CategoryProc(Def.DefTYPE_TOP40, "");      // ＴＯＰ４０

                //CategoryProc(Def.DefTYPE_RIPRO20, "");    // リプロ２０

                ViewState[Def.DefPARA_CONDITION_FLG] = Def.DefTYPE_OR;

                //検索履歴画面表示処理
                SearchHistory();

            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmSearch", "Page_Load", ex, this.Response);
            }
        }
        #endregion

        #region カテゴリデータテーブル設定
        /// <summary>
        /// カテゴリデータテーブル設定
        /// </summary>
        protected void CreateCategoryDatatable(DataTable result)
        {
            // 部署
            CreateCategoryTable(Def.DefTYPE_BUSYO, ckBoxBusyoSekkeiPu, result);
            CreateCategoryTable(Def.DefTYPE_HYOUKA, ckBoxBusyoHyoukaPu, result);
            CreateCategoryTable(Def.DefTYPE_BUSYO, ckBoxBusyoSekkeiBy, result);
            CreateCategoryTable(Def.DefTYPE_HYOUKA, ckBoxBusyoHyoukaBy, result);

            if (IsCreateCategory(ckBoxBuhin) == true)
            {
                // 部品
                CreateCategoryTable(Def.DefTYPE_PARTS, ckBoxBuhin, result);
            }
            else
            {
                // システム
                CreateCategoryTable(Def.DefTYPE_PARTS, ckBoxSystem, result);
            }

            // 開発符号
            CreateCategoryTable(Def.DefTYPE_KAIHATU, ckBoxMst01, result);
            // 現象（分類）
            CreateCategoryTable(Def.DefTYPE_GENSYO, ckBoxMst02, result);
            // 原因（分類）
            CreateCategoryTable(Def.DefTYPE_GENIN, ckBoxMst03, result);
            // 車型特殊
            CreateCategoryTable(Def.DefTYPE_SYAKATA, ckBoxMst04, result);
            // 現象（制御系）
            CreateCategoryTable(Def.DefTYPE_SGENSYO, ckBoxMst05, result);
            // 要因（制御系）
            CreateCategoryTable(Def.DefTYPE_SYOUIN, ckBoxMst06, result);
            // EGTM形式
            CreateCategoryTable(Def.DefTYPE_EGTM, ckBoxMst07, result);
            //データテーブル作成結果をセッションに入れる
            Session[Def.DefPARA_TABLE] = result;
        }
        #endregion

        #region カテゴリ選択テーブル作成
        /// <summary>
        /// カテゴリ選択テーブル作成
        /// </summary>
        protected void CreateCategoryTable(string Type, CheckBoxList ckBoxList, DataTable dt)
        {
            foreach (ListItem mstItem in ckBoxList.Items)
            {
                if (mstItem.Selected)
                {
                    DataRow dr = dt.NewRow();
                    dr["TableType"] = Type;             //タイプ
                    dr["ItemValue1"] = mstItem.Value;   //検索文字列
                    dt.Rows.Add(dr);                    //行追加
                }
            }
        }
        #endregion

        #region カテゴリ部品選択有無チェック
        /// <summary>
        /// カテゴリ部品選択有無チェック
        /// </summary>
        protected bool IsCreateCategory(CheckBoxList ckBoxList)
        {
            bool CreateCategoryB = false;
       
            foreach (ListItem mstItem in ckBoxList.Items)
            {
                if (mstItem.Selected)
                {
                    CreateCategoryB = true;
                }
            }
            return CreateCategoryB;
        }
        #endregion

        #region 検索ボタンクリック
        /// <summary>
        /// 検索ボタンクリック
        /// </summary>
        protected void btn_Search_Click(object sender, EventArgs e)
        {
            SessionClear();
            createSearchParam(true);
        }
        #endregion

        #region カテゴリAND検索ボタンクリック
        /// <summary>
        /// カテゴリAND検索ボタンクリック
        /// </summary>
        protected void btn_CategorySearchAND_Click(object sender, EventArgs e)
        {
            SessionClear();
            createSearchParam(false, -1, Def.DefTYPE_AND);
        }
        #endregion

        #region カテゴリOR検索ボタンクリック
        /// <summary>
        /// カテゴリOR検索ボタンクリック
        /// </summary>
        // 20170719 Add Start
        protected void btn_CategorySearchOR_Click(object sender, EventArgs e)
        {
            SessionClear();
            createSearchParam(false, -1, Def.DefTYPE_OR);
        }
        // 20170719 Add End
        #endregion

        #region 検索処理メイン
        /// <summary>
        /// 検索処理メイン　引数 searchKbn true:キーワード検索　false:カテゴリ検索
        /// </summary>  
        /// <param name="searchKbn">検索区分 true:キーワード検索　false:カテゴリ検索</param>
        /// <param name="intHistoryRow">履歴</param>
        /// <param name="strCategorySearch">カテゴリ検索 1:AND、2:OR</param> // 20170719 Add
        //private void createSearchParam(Boolean searchKbn = false, int intHistoryRow = -1)
        private void createSearchParam( Boolean searchKbn = false, int intHistoryRow = -1, string strCategorySearch = "0")
        {
            try
            {
                String[] strArrayData = null;
                String strMoji = "";
                String strWord = null;
                // カテゴリ検索パラメータ
                DataTable categolyParam = new TroubleData().getCotegoryDataTable();

                if (Session[Def.DefPARA_WORD] == null)
                {
                    // 初期設定　検索パラメータ
                    Session[Def.DefPARA_CONDITION_FLG] = (String)ViewState[Def.DefPARA_CONDITION_FLG];
                    //Session[Def.DefPARA_WORD] = strWord;
                }

                Session[Def.DefPARA_TABLE] = categolyParam;

                Session[Def.DefPARA_CATEGORY_CONDITION_FLG] = strCategorySearch;


                if (searchKbn == true)
                { 
                    //20170201 機能改善 START
                    Session[Def.DefPARA_CONDITION_FLG] = (String)ViewState[Def.DefPARA_CONDITION_FLG];
                    strMoji = txtSearch.Text.Trim();
                    if (strMoji.Length > 0)
                    {
                        //20170201 機能改善 END
                        Session[Def.DefPARA_TYPE] = Def.DefTYPE_WORD;   // 文字列検索
                        strMoji = txtSearch.Text.Trim();
                        // 全角スペースをを半角に置換 20160311
                        strMoji = strMoji.Replace("　", " ");
                        // 文字数チェック
                        strArrayData = strMoji.Split(' ');
                        int i = 0;
                        int iWord = 0;
                        for (i = 0; i < strArrayData.Count(); i++)
                        {
                            if (strArrayData[i].ToString().Trim() != "")
                            {
                                if (i != 0) { strWord += " "; }
                                strWord += strArrayData[i];
                                iWord++;
                            }
                            if (iWord > 3) break;
                        }
                        txtSearch.Text = strWord.Trim();

                        Session[Def.DefPARA_WORD] = strWord.Trim();    // 検索文字列

                    }
                    if (intHistoryRow > -1)
                    {
                        CreateHistoryPrm(intHistoryRow);
                    }
                }
                else
                { 
                    // カテゴリ選択データテーブル
                    CreateCategoryDatatable(categolyParam);
                }

                // 過去トラ検索結果画面オープン
                openWindow();
            }

            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmSearch", "btn_Search_Click", ex, this.Response);
            }
        }

        //20170201 機能改善 START
        #region 履歴カテゴリ選択テーブル作成
        /// <summary>
        /// 検索履歴パラメータ作成（カテゴリ）
        /// </summary>
        /// <param name="intHistoryRow"></param>
        /// <param name="categolyParam"></param>
        private void CreateHistoryPrm(int intHistoryRow)
        {

            DataTable categolyParam = new DataTable();
            categolyParam.Columns.Add("TableType", typeof(string));
            categolyParam.Columns.Add("ItemValue1", typeof(String));

            // 履歴カテゴリ選択テーブル作成
            CreateHistoryCategoryDatatable(intHistoryRow, Def.DefSEARCH_PARTS_N, Def.DefTYPE_PARTS, categolyParam);
            if (categolyParam.Rows.Count == 0)
            {
                CreateHistoryCategoryDatatable(intHistoryRow, Def.DefSEARCH_PARTS_S, Def.DefTYPE_PARTS, categolyParam);
            }
            CreateHistoryCategoryDatatable(intHistoryRow, Def.DefSEARCH_BUSYO, Def.DefTYPE_BUSYO, categolyParam);
            CreateHistoryCategoryDatatable(intHistoryRow, Def.DefSEARCH_HYOUKA, Def.DefTYPE_HYOUKA, categolyParam);
            CreateHistoryCategoryDatatable(intHistoryRow, Def.DefSEARCH_KAIHATU, Def.DefTYPE_KAIHATU, categolyParam);
            CreateHistoryCategoryDatatable(intHistoryRow, Def.DefSEARCH_GENSYO, Def.DefTYPE_GENSYO, categolyParam);
            CreateHistoryCategoryDatatable(intHistoryRow, Def.DefSEARCH_GENIN, Def.DefTYPE_GENIN, categolyParam);
            CreateHistoryCategoryDatatable(intHistoryRow, Def.DefSEARCH_SYAKATA, Def.DefTYPE_SYAKATA, categolyParam);
            CreateHistoryCategoryDatatable(intHistoryRow, Def.DefSEARCH_SGENSYO, Def.DefTYPE_SGENSYO, categolyParam);
            CreateHistoryCategoryDatatable(intHistoryRow, Def.DefSEARCH_SYOUIN, Def.DefTYPE_SYOUIN, categolyParam);
            CreateHistoryCategoryDatatable(intHistoryRow, Def.DefSEARCH_EGTM, Def.DefTYPE_EGTM, categolyParam);

            Session[Def.DefPARA_TABLE] = categolyParam;
        }
        #endregion

        /// <summary>
        /// 履歴カテゴリ選択テーブル作成
        /// </summary>
        protected void CreateHistoryCategoryDatatable(int intHistoryRow, int intItemRow, string Type, DataTable dt)
        {
            BuisinessLogic.BLTroubleList bLogic = new BuisinessLogic.BLTroubleList();
            DataTable dtCategoryName = null;
            String strCategoryName = "";
            string[] stArrayData = ((System.Data.DataTable)(ViewState[Def.DefHISTORY])).Rows[intHistoryRow].ItemArray[intItemRow].ToString().Split(',');

            foreach (String stData in stArrayData)
            {
                if (!String.IsNullOrEmpty(stData))
                {
                    dtCategoryName = bLogic.GetCategoryName(Type, stData);
                    if (dtCategoryName.Rows.Count > 0)
                    {
                        strCategoryName = dtCategoryName.Rows[0].ItemArray[0].ToString();
                    }

                    DataRow dr = dt.NewRow();
                    dr["TableType"] = Type;
                    if (intItemRow == Def.DefSEARCH_PARTS_N)
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder(stData);
                        //カンマを挿入する
                        sb.Insert(2, ",");
                        sb.Insert(5, ",");
                        string strWork = sb.ToString();
                        dr["ItemValue1"] = strWork + "," + strCategoryName;
                    }
                    else if (intItemRow == Def.DefSEARCH_PARTS_S)
                    {
                        dr["ItemValue1"] = stData + ",,," + strCategoryName;
                    }
                    else if(intItemRow == Def.DefSEARCH_BUSYO || intItemRow == Def.DefSEARCH_HYOUKA)
                    {
                        dr["ItemValue1"] = stData;
                    }
                    else
                    {
                        dr["ItemValue1"] = stData + "," + strCategoryName;
                    }
                    dt.Rows.Add(dr);    //行追加
                }
            }
        }
        #endregion
        #region カテゴリクリアボタンクリック
        /// <summary>
        /// カテゴリクリアボタンクリック
        /// </summary>
        protected void btn_CategoryClear_Click(object sender, EventArgs e)
        {
            ckBoxBuhin.DataSource = "";
            ckBoxBuhin.DataBind();
        }
        #endregion

        #region ＴＯＰページへクリック
        /// <summary>
        /// ＴＯＰページへクリック
        /// </summary>
        protected void lnkTop_Click(Object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("frmTopPage.aspx", false);
            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmSearch", "lnkTop_Click", ex, this.Response);
            }
        }
        #endregion

        #region ＦＭＣ・ｍｃ進捗ボタンクリック
        /// <summary>
        /// ＦＭＣ・ｍｃ進捗ボタンクリック
        /// </summary>
        protected void btn_Follow_Click(Object sender, EventArgs e)
        {
            
            try
            {
                Response.Redirect("frmFollow.aspx", false);
            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmSearch", "btn_Follow_Click", ex, this.Response);
            }
        }
        #endregion

        #region おすすめリンク
        /// <summary>
        /// おすすめ０１～１０リンククリック
        /// </summary>
        protected void lnkTopTen_Click(Object sender, CommandEventArgs e)
        {
            try
            {
                //20170313 START k-ohmatsuzawa
                SessionClear();
                //20170313 END k-ohmatsuzawa
                // 2017.02.22 ta_kanda 条件
                Session[Def.DefPARA_CONDITION_FLG] = Def.DefTYPE_OR;　　// 条件
                Session[Def.DefPARA_TYPE] = Def.DefTYPE_TOP10;  　　　　// キーワード検索
                Session[Def.DefPARA_WORD] = e.CommandName;            　　　　// 検索文字列
                // 2017.02.22 ta_kanda カテゴリ検索パラメータ
                DataTable categolyParam = new DataTable();
                categolyParam.Columns.Add("TableType", typeof(string));
                categolyParam.Columns.Add("ItemValue1", typeof(String));
                Session[Def.DefPARA_TABLE] = categolyParam;

                openWindow();                           // 過去トラ検索結果画面オープン
            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmSearch", "lnkTop01_Click", ex, this.Response);
            }
        }
        #endregion

        #region カテゴリ検索
        /// <summary>
        /// カテゴリ検索・部品部位絞込み
        /// </summary>
        protected void btn_Filter(Object sender, EventArgs e)
        {
            

            DataTable dtPartList = null;

            try
            {
                String strBY = "1";
                String strPU = "1";

                BuisinessLogic.BLSearch bLogic = new BuisinessLogic.BLSearch();

                ArrayList arrWork = new ArrayList();

                foreach (ListItem sysItem in ckBoxSystem.Items)
                {
                    if (sysItem.Selected)
                    {
                        arrWork.Add(sysItem.Value);
                    }
                }

                dtPartList = bLogic.GetPartsList(strBY, strPU, arrWork);

                ckBoxBuhin.DataSource = dtPartList;
                ckBoxBuhin.DataBind();
                ckBoxBuhin.Focus();

            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmSearch", "btn_Filter", ex, this.Response);
            }

        }

        #endregion

        #region 固有関数

        #region 画面表示処理
        /// <summary>
        /// 画面表示処理
        /// </summary>
        protected void initialDisp()
        {
            DateTime dtNow = DateTime.Now;

            lblTop10.Text = dtNow.Year + " 年 " + dtNow.Month + " 月 検索ワードTOP10";
            //lblTop10.Text = dtNow.Year + " 年 " + dtNow.Month + " 月 の検索キーワードＴＯＰ１０";

            BuisinessLogic.BLSearch bLogic = new BuisinessLogic.BLSearch();

            lnkTop01.Text = "";
            lnkTop02.Text = "";
            lnkTop03.Text = "";
            lnkTop04.Text = "";
            lnkTop05.Text = "";
            lnkTop06.Text = "";
            lnkTop07.Text = "";
            lnkTop08.Text = "";
            lnkTop09.Text = "";
            lnkTop10.Text = "";

            DataTable dtKeyword = null;
            DataTable dtRecommend = null;

            // AutoComplete のキーワードリスト取得
            dtKeyword = bLogic.GetKeyWordList();

            string jisyoInfo = "";

            for (int i = 0; i < dtKeyword.Rows.Count; i++)
             {
                if (i > 0) jisyoInfo += ",\n";

                jisyoInfo += "{ label: '" + dtKeyword.Rows[i]["WORD_KEY"].ToString().Trim() + "　" + dtKeyword.Rows[i]["WORD_CHAR"].ToString().Trim() + "', value: '" + dtKeyword.Rows[i]["WORD_KEY"].ToString().Trim() + "'}";
            }

            ViewState["JisyoInfo"] = jisyoInfo;

            // おすすめＴＯＰ１０のデータ取得
            dtRecommend = bLogic.GetRecommendList();

            for (int i = 0; i < 10; i++)
            {
                if (dtRecommend.Rows.Count < i + 1)
                {
                    break;
                }
                else
                {
                    var toptenWord = dtRecommend.Rows[i]["RECOMMEND_WORD"].ToString().Trim();

                    switch(i)
                    {
                      case 0:
                        lnkTop01.Text =toptenWord;
                        lnkTop01.CommandName = toptenWord;
                        break;
                      case 1:
                        lnkTop02.Text =toptenWord;
                        lnkTop02.CommandName = toptenWord;
                        break;
                      case 2:
                        lnkTop03.Text =toptenWord;
                        lnkTop03.CommandName = toptenWord;
                          break;
                      case 3:
                        lnkTop04.Text =toptenWord;
                        lnkTop04.CommandName = toptenWord;
                        break;
                      case 4:
                        lnkTop05.Text =toptenWord;
                        lnkTop05.CommandName = toptenWord;
                        break;
                      case 5:
                        lnkTop06.Text =toptenWord;
                        lnkTop06.CommandName = toptenWord;
                          break;
                      case 6:
                        lnkTop07.Text =toptenWord;
                        lnkTop07.CommandName = toptenWord;
                        break;
                      case 7:
                        lnkTop08.Text =toptenWord;
                        lnkTop08.CommandName = toptenWord;
                        break;
                      case 8:
                        lnkTop09.Text =toptenWord;
                        lnkTop09.CommandName = toptenWord;
                        break;
                      case 9:
                        lnkTop10.Text =toptenWord;
                        lnkTop10.CommandName = toptenWord;
                        break;
                    }
                }
            }
        }
        #endregion

        #region 過去トラカテゴリ検索
        /// <summary>
        /// 過去トラカテゴリ検索表示処理
        /// </summary>
        protected void CategoryProc(String Type, String BYPU)
        {
            //------ TYPE:検索タイプ
            // 1;文字列検索
            // 2:TOP10検索
            // 3:部署
            // 4:部品
            // 5:開発符号
            // 6:現象（分類）
            // 7:原因（分類）
            // 8:車型特殊
            // 9:現象（制御系）
            // 10:要因（制御系）
            // 11:EGTM形式
            // 12:TOP40
            // 13:リプロ20

            switch(Type)
            {
                case "31":
                case "32":
                    // 部署
                    ViewState[Def.DefPARA_TYPE] = Def.DefTYPE_BUSYO;
                    ViewState[Def.DefPARA_CATNM] = "";
                    pnlCategoryBusyo.Visible = true;
                    break;
                case Def.DefTYPE_PARTS:
                    // 部品・部位
                    ViewState[Def.DefPARA_TYPE] = Def.DefTYPE_PARTS;
                    ViewState[Def.DefPARA_CATNM] = Def.DefTYPENM_PARTS;
                    //pnlCategoryBuhin.Visible = true;
                    break;
                case Def.DefTYPE_KAIHATU:
                    // 開発符号def.
                    ViewState[Def.DefPARA_TYPE] = Def.DefTYPE_KAIHATU;
                    ViewState[Def.DefPARA_CATNM] = Def.DefTYPENM_KAIHATU;
                    //pnlCategoryMst.Visible = true;
                    break;
                case Def.DefTYPE_GENSYO:
                    // 現象（分類）
                    ViewState[Def.DefPARA_TYPE] = Def.DefTYPE_GENSYO;
                    ViewState[Def.DefPARA_CATNM] = Def.DefTYPENM_GENSYO;
                    //pnlCategoryMst.Visible = true;
                    break;
                case Def.DefTYPE_GENIN:
                    // 原因（分類）
                    ViewState[Def.DefPARA_TYPE] = Def.DefTYPE_GENIN;
                    ViewState[Def.DefPARA_CATNM] = Def.DefTYPENM_GENIN;
                    //pnlCategoryMst.Visible = true;
                    break;
                case Def.DefTYPE_SYAKATA:
                    // 車型特殊
                    ViewState[Def.DefPARA_TYPE] = Def.DefTYPE_SYAKATA;
                    ViewState[Def.DefPARA_CATNM] = Def.DefTYPENM_SYAKATA;
                    //pnlCategoryMst.Visible = true;
                    break;
                case Def.DefTYPE_SGENSYO:
                    // 現象（制御系）
                    ViewState[Def.DefPARA_TYPE] = Def.DefTYPE_SGENSYO;
                    ViewState[Def.DefPARA_CATNM] = Def.DefTYPENM_SGENSYO;
                    //pnlCategoryMst.Visible = true;
                    break;
                case Def.DefTYPE_SYOUIN:
                    // 要因（制御系）
                    ViewState[Def.DefPARA_TYPE] = Def.DefTYPE_SYOUIN;
                    ViewState[Def.DefPARA_CATNM] = Def.DefTYPENM_SYOUIN;
                    //pnlCategoryMst.Visible = true;
                    break;
                case Def.DefTYPE_EGTM:
                    // EGTM形式
                    ViewState[Def.DefPARA_TYPE] = Def.DefTYPE_EGTM;
                    ViewState[Def.DefPARA_CATNM] = Def.DefTYPENM_EGTM;
                    //pnlCategoryMst.Visible = true;
                    break;
                case Def.DefTYPE_TOP40:
                    // TOP40
                    ViewState[Def.DefPARA_TYPE] = Def.DefTYPE_TOP40;
                    ViewState[Def.DefPARA_CATNM] = Def.DefTYPENM_TOP40;
                    //pnlCategoryMst.Visible = true;
                    break;
                case Def.DefTYPE_RIPRO20:
                    // リプロ20
                    ViewState[Def.DefPARA_TYPE] = Def.DefTYPE_RIPRO20;
                    ViewState[Def.DefPARA_CATNM] = Def.DefTYPENM_RIPRO20;
                    //pnlCategoryMst.Visible = true;
                    break;
            }

            BuisinessLogic.BLSearch bLogic = new BuisinessLogic.BLSearch();

            DataTable result = null;

            result = bLogic.GetMasterList(Type, BYPU);

            //if (((Type == "31")
            //  && (BYPU == "BY")))
            //{
            //    if (result.Rows.Count != 0)
            //    {
            //        ckBoxBusyoSekkeiPu.DataSource = result;
            //        ckBoxBusyoSekkeiPu.DataBind();
            //    }
            //}

            //if (((Type == "32")
            //  && (BYPU == "BY")))
            //{
            //    if (result.Rows.Count != 0)
            //    {
            //        ckBoxBusyoHyoukaPu.DataSource = result;
            //        ckBoxBusyoHyoukaPu.DataBind();
            //    }
            //}

            switch (Type)
            {

                // 部署設計
                case "31":
                    if (result.Rows.Count != 0)
                    {
                        if (BYPU == "BY")
                        {
                            ckBoxBusyoSekkeiBy.DataSource = result;
                            ckBoxBusyoSekkeiBy.DataBind();
                        }
                        {
                            ckBoxBusyoSekkeiPu.DataSource = result;
                            ckBoxBusyoSekkeiPu.DataBind();
                        }
                    }
                    break;
                // 部署評価
                case "32":
                    if (result.Rows.Count != 0)
                    {
                        if (BYPU == "BY")
                        {
                            ckBoxBusyoHyoukaBy.DataSource = result;
                            ckBoxBusyoHyoukaBy.DataBind();
                        }
                        {
                            ckBoxBusyoHyoukaPu.DataSource = result;
                            ckBoxBusyoHyoukaPu.DataBind();
                        }
                    }
                    break;
                case Def.DefTYPE_PARTS:
                    if (result.Rows.Count != 0)
                    {
                        // 部品・部位
                        ckBoxSystem.DataSource = result;
                        ckBoxSystem.DataBind();
                    }
                    break;
                case Def.DefTYPE_KAIHATU:
                    if (result.Rows.Count != 0)
                    {
                        // 開発符号def.
                        ckBoxMst01.DataSource = result;
                        ckBoxMst01.DataBind();
                    }
                    break;
                case Def.DefTYPE_GENSYO:
                    if (result.Rows.Count != 0)
                    {
                        // 現象（分類）
                        ckBoxMst02.DataSource = result;
                        ckBoxMst02.DataBind();
                    }
                    break;
                case Def.DefTYPE_GENIN:
                    if (result.Rows.Count != 0)
                    {
                        // 原因（分類）
                        ckBoxMst03.DataSource = result;
                        ckBoxMst03.DataBind();
                    }
                    break;
                case Def.DefTYPE_SYAKATA:
                    if (result.Rows.Count != 0)
                    {
                        // 車型特殊
                        ckBoxMst04.DataSource = result;
                        ckBoxMst04.DataBind();
                    }
                    break;
                case Def.DefTYPE_SGENSYO:
                    if (result.Rows.Count != 0)
                    {
                        // 現象（制御系）
                        ckBoxMst05.DataSource = result;
                        ckBoxMst05.DataBind();
                    }
                    break;
                case Def.DefTYPE_SYOUIN:
                    if (result.Rows.Count != 0)
                    {
                        // 要因（制御系）
                        ckBoxMst06.DataSource = result;
                        ckBoxMst06.DataBind();
                    }
                    break;
                case Def.DefTYPE_EGTM:
                    if (result.Rows.Count != 0)
                    {
                        // EGTM形式
                        ckBoxMst07.DataSource = result;
                        ckBoxMst07.DataBind();
                    }
                    break;
                //case Def.DefTYPE_TOP40:
                //    if (result.Rows.Count != 0)
                //    {
                //        // TOP40
                //        ckBoxMst08.DataSource = result;
                //        ckBoxMst08.DataBind();
                //    }
                //    break;
                //case Def.DefTYPE_RIPRO20:
                //    // リプロ20
                //    ckBoxMst09.DataSource = result;
                //    ckBoxMst09.DataBind();
                //    break;
            }

        }
        #endregion

        #region 過去トラ検索結果画面表示
        /// <summary>
        /// Window Open 処理
        /// </summary>
        protected void openWindow()
        {
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;

            string strScr = cPageLogic.getScriptForOpenWindow(Def.DefPageId_TroubleList);

            cs.RegisterStartupScript(cstype, "OpenSubWindow", strScr);
        }

        #endregion

        #region 検索履歴画面表示処理
        /// <summary>
        /// 検索履歴画面表示処理
        /// </summary>
        protected void SearchHistory()
        {

            

            lnkHistory01.Text = "";
            lnkHistory02.Text = "";
            lnkHistory03.Text = "";
            lnkHistory04.Text = "";
            lnkHistory05.Text = "";
            //lnkHistory06.Text = "";
            //lnkHistory07.Text = "";
            //lnkHistory08.Text = "";
            //lnkHistory09.Text = "";
            //lnkHistory10.Text = "";

            String lnkCategory = "";

            DataTable dtHistory = null;
            String user_id = "";

            // リスト取得
            dtHistory = bcom.GetSearchHistoryList(user_id);

            //20170201 機能改善 START
            
            ViewState[Def.DefHISTORY] = dtHistory;

            Session[Def.DefHISTORY] = dtHistory;
            //20170201 機能改善 END

            // 最新の検索履歴5件のデータ取得
            for (int i = 0; i < 5; i++)
            {
                lnkCategory = "";
                if (dtHistory.Rows.Count < i + 1)
                {
                    break;
                }
                else
                {
                    switch (i)
                    {
                        case 0:
                            if (DBNull.Value.Equals(dtHistory.Rows[i]["SEARCH_WORD"]))
                            {
                                lblConFlg01.Text = i + 1 + ". ";

                            }
                            else
                            {
                                lblConFlg01.Text = i + 1 + ". " + dtHistory.Rows[i]["CONDITION_FLGM"].ToString().Trim();
                            }
                            lnkCategory = EditLinkCategory(dtHistory, i);
                            lnkHistory01.Text = lnkCategory;
                            break;
                        case 1:
                            if (DBNull.Value.Equals(dtHistory.Rows[i]["SEARCH_WORD"]))
                            {
                                lblConFlg02.Text = i + 1 + ". ";

                            }
                            else
                            {
                                lblConFlg02.Text = i + 1 + ". " + dtHistory.Rows[i]["CONDITION_FLGM"].ToString().Trim();
                            }
                            lnkCategory = EditLinkCategory(dtHistory, i);
                            lnkHistory02.Text = lnkCategory;
                            break;
                        case 2:
                            if (DBNull.Value.Equals(dtHistory.Rows[i]["SEARCH_WORD"]))
                            {
                                lblConFlg03.Text = i + 1 + ". ";

                            }
                            else
                            {
                                lblConFlg03.Text = i + 1 + ". " + dtHistory.Rows[i]["CONDITION_FLGM"].ToString().Trim();
                            }
                            lnkCategory = EditLinkCategory(dtHistory, i);
                            lnkHistory03.Text = lnkCategory;
                            break;
                        case 3:
                            if (DBNull.Value.Equals(dtHistory.Rows[i]["SEARCH_WORD"]))
                            {
                                lblConFlg04.Text = i + 1 + ". ";

                            }
                            else
                            {
                                lblConFlg04.Text = i + 1 + ". " + dtHistory.Rows[i]["CONDITION_FLGM"].ToString().Trim();
                            }
                            lnkCategory = EditLinkCategory(dtHistory, i);
                            lnkHistory04.Text = lnkCategory;
                            break;
                        case 4:
                            if (DBNull.Value.Equals(dtHistory.Rows[i]["SEARCH_WORD"]))
                            {
                                lblConFlg05.Text = i + 1 + ". ";

                            }
                            else
                            {
                                lblConFlg05.Text = i + 1 + ". " + dtHistory.Rows[i]["CONDITION_FLGM"].ToString().Trim();
                            }
                            lnkCategory = EditLinkCategory(dtHistory, i);
                            lnkHistory05.Text = lnkCategory;
                            break;
                        //case 5:
                        //    lblConFlg06.Text = "6. " + dtHistory.Rows[i]["CONDITION_FLGM"].ToString().Trim();
                        //    lnkCategory = EditLinkCategory(dtHistory, i);
                        //    lnkHistory06.Text = lnkCategory;
                        //    break;
                        //case 6:
                        //    lblConFlg07.Text = "7. " + dtHistory.Rows[i]["CONDITION_FLGM"].ToString().Trim();
                        //    lnkCategory = EditLinkCategory(dtHistory, i);
                        //    lnkHistory07.Text = lnkCategory;
                        //    break;
                        //case 7:
                        //    lblConFlg08.Text = "8. " + dtHistory.Rows[i]["CONDITION_FLGM"].ToString().Trim();
                        //    lnkCategory = EditLinkCategory(dtHistory, i);
                        //    lnkHistory08.Text = lnkCategory;
                        //    break;
                        //case 8:
                        //    lblConFlg09.Text = "9. " + dtHistory.Rows[i]["CONDITION_FLGM"].ToString().Trim();
                        //    lnkCategory = EditLinkCategory(dtHistory, i);
                        //    lnkHistory09.Text = lnkCategory;
                        //    break;
                        //case 9:
                        //    lblConFlg10.Text = "10. " + dtHistory.Rows[i]["CONDITION_FLGM"].ToString().Trim();
                        //    lnkCategory = EditLinkCategory(dtHistory, i);
                        //    lnkHistory10.Text = lnkCategory;
                        //    break;
                    }
                }
            }
        }

        #endregion

        //20170306 START k-ohmatsuzawa
        #region セッション領域のクリア処理
        /// <summary>
        /// セッション領域のクリア処理
        /// </summary>
        protected void SessionClear()
        {
            Session[Def.DefPARA_TYPE] = null;     // 検索タイプ
            Session[Def.DefPARA_WORD] = null;     // 文字列検索
            Session[Def.DefPARA_TABLE] = null;            // Datatable
            Session[Def.DefPARA_CONDITION_FLG] = null;    // And・Or検索条件
            Session[Def.DefPARA_ARRY] = null;     // 各マスタor設計部署
            Session[Def.DefPARA_CATEGORY_CONDITION_FLG] = null;    // カテゴリ用検索 And・Or検索条件 // 20170719 Add
        }
        #endregion
        //20170306 END k-ohmatsuzawa

        #endregion

        #region カテゴリリンク作成処理
        protected string EditLinkCategory(DataTable dtHistory,int i)
        {
            StringBuilder sb = new StringBuilder();// 検索履歴表示用のカテゴリ文言

            if (!DBNull.Value.Equals(dtHistory.Rows[i]["SEARCH_WORD"]))
            {
                //sb.Append("(ｷｰﾜｰﾄﾞ)");
                sb.Append(dtHistory.Rows[i]["SEARCH_WORD"].ToString().Trim());
                sb.Append("<br />");
            }
            else
            {
                //sb.Append("<br />");
            }
            if (!DBNull.Value.Equals(dtHistory.Rows[i]["SEARCH_BUSYO"]) || !DBNull.Value.Equals(dtHistory.Rows[i]["SEARCH_HYOUKA"]))
            {
                sb.Append("(ｶﾃｺﾞﾘ)部署");
                sb.Append("<br />");
            }
            if (!DBNull.Value.Equals(dtHistory.Rows[i]["SEARCH_PARTS_S"])){
                sb.Append("(ｶﾃｺﾞﾘ)部品・システム");
                sb.Append("<br />");
            }
            if (!DBNull.Value.Equals(dtHistory.Rows[i]["SEARCH_PARTS_N"])){
                sb.Append("(ｶﾃｺﾞﾘ)部品・絞込み");
                sb.Append("<br />");
            }
            if (!DBNull.Value.Equals(dtHistory.Rows[i]["SEARCH_KAIHATU"])){
                sb.Append("(ｶﾃｺﾞﾘ)開発符号");
                sb.Append("<br />");
            }
            if (!DBNull.Value.Equals(dtHistory.Rows[i]["SEARCH_GENSYO"])){
                sb.Append("(ｶﾃｺﾞﾘ)現象(分類)");
                sb.Append("<br />");
            }
            if (!DBNull.Value.Equals(dtHistory.Rows[i]["SEARCH_GENIN"])){
                sb.Append("(ｶﾃｺﾞﾘ)原因(分類)");
                sb.Append("<br />");
            }
            if (!DBNull.Value.Equals(dtHistory.Rows[i]["SEARCH_SYAKATA"])){
                sb.Append("(ｶﾃｺﾞﾘ)車両特殊");
                sb.Append("<br />");
            }
            if (!DBNull.Value.Equals(dtHistory.Rows[i]["SEARCH_SGENSYO"])){
                sb.Append("(ｶﾃｺﾞﾘ)現象(制御系)");
                sb.Append("<br />");
            }
            if (!DBNull.Value.Equals(dtHistory.Rows[i]["SEARCH_SYOUIN"])){
                sb.Append("(ｶﾃｺﾞﾘ)要因(制御系)");
                sb.Append("<br />");
            }
            if (!DBNull.Value.Equals(dtHistory.Rows[i]["SEARCH_EGTM"])){
                sb.Append("(ｶﾃｺﾞﾘ)EGTM形式");
                sb.Append("<br />");
            }
            // 20170304 START k-ohmatsuzawa TOP10検索が履歴に表示されるように修正
            if (!DBNull.Value.Equals(dtHistory.Rows[i]["SEARCH_TOP10"]))
            {
                //sb.Append("(TOP10)");
                sb.Append(dtHistory.Rows[i]["SEARCH_TOP10"].ToString().Trim());
                sb.Append("<br />");
            }
            // 20170304 END k-ohmatsuzawa

            return sb.ToString();
        }
        #endregion

        #region クリアボタン処理
        /// <summary>
        /// クリアボタン処理
        /// </summary> 
        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            

            try
            {
                txtSearch.Text = null;
            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmSearch", "btn_Clear_Ok", ex, this.Response);
            }
        }
        #endregion

        #region 履歴検索処理
        /// <summary>
        /// 検索履歴ボタンクリック
        /// </summary> 
        protected void lnkHistory_Click(object sender, CommandEventArgs e)
        {
            try
            {
                //20170313 START k-ohmatsuzawa
                SessionClear();
                //20170313 END k-ohmatsuzawa
                var dt = (DataTable)Session[Def.DefHISTORY];

                var i = int.Parse(e.CommandName);

                // キーワード設定
                //20170313 START k-ohmatsuzawa 不具合を修正
                ///if (dt.Rows[0]["CONDITION_FLGM"].Equals("AND"))
                if (dt.Rows[i]["CONDITION_FLGM"].Equals("AND"))
                //20170313 END k-ohmatsuzawa
                {
                    Session[Def.DefPARA_CONDITION_FLG] = Def.DefTYPE_AND;
                }
                else
                {
                    Session[Def.DefPARA_CONDITION_FLG] = Def.DefTYPE_OR;
                }

                // 検索内容設定
                if (dt.Rows[i]["SEARCH_WORD"].ToString() != "")
                {
                    // キーワード検索設定
                    Session[Def.DefPARA_TYPE] = Def.DefTYPE_WORD;   // 文字列検索
                    Session[Def.DefPARA_WORD] = dt.Rows[i]["SEARCH_WORD"].ToString();
                }
                else if(dt.Rows[i]["SEARCH_TOP10"].ToString() != "")
                {
                    // TOP10検索設定
                    Session[Def.DefPARA_TYPE] = Def.DefTYPE_TOP10;   // TOP10検索
                    Session[Def.DefPARA_WORD] = dt.Rows[i]["SEARCH_TOP10"].ToString();
                }
                // カテゴリ検索設定（履歴用）
                CreateHistoryPrm(i);
                //20170304 END k-ohmatsuzawa

                // 検索結果画面オープン
                openWindow();
            }

            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmSearch", "lnkHistory01_Click", ex, this.Response);
            }
        }
        #endregion

        #region 検索条件指示（OR、AND)

        #region 検索条件「ＯＲ」ボタンクリック
        /// <summary>
        /// 「ＯＲ」ボタンクリック
        /// </summary> 
        protected void btn_OR_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.BackColor = System.Drawing.ColorTranslator.FromHtml("#99FF99");

                ViewState[Def.DefPARA_CONDITION_FLG] = Def.DefTYPE_OR;


            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmSearch", "btn_OR_Click", ex, this.Response);
            }
        }
        #endregion

        #region 検索条件「ＡＮＤ」ボタンクリック
        /// <summary>
        /// 「ＡＮＤ」ボタンクリック
        /// </summary> 
        protected void btn_AND_Click(object sender, EventArgs e)
        {
            

            try
            {
                txtSearch.BackColor = System.Drawing.ColorTranslator.FromHtml("#66FFFF");

                ViewState[Def.DefPARA_CONDITION_FLG] = Def.DefTYPE_AND;


            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmSearch", "btn_AND_Click", ex, this.Response);
            }
        }
        #endregion
        #endregion
    }
 }

