using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Web.UI;
using OldTigerWeb.BuisinessLogic;
using System.Collections.Generic;
using System.Configuration;

namespace OldTigerWeb
{
    public partial class frmDetail : System.Web.UI.Page
    {
        CommonLogic bcom = new CommonLogic();
        BLDetail bdetail = new BLDetail();

        public string[] kanrenSiryo,kanrenSiryoName;
        public string kanrenSiryoPath;
        public List<string> ecsList;
        public string _ecsSubSysPath = ConfigurationManager.AppSettings["EcsSubSysPath"];

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

            try
            {
                Boolean bRet = false;
                DataTable dtTrableData = null;

                string stKanriNo = "";

                ClientScriptManager csManager = Page.ClientScript;
                Type csType = this.GetType();
                ArrayList arrayMessage = new ArrayList();
                Boolean result = false;

                // 引数:システム管理No
                try
                {
                    stKanriNo = Request.QueryString.Get(Def.DefPARA_KANRINO).Trim();
                    if (stKanriNo == "" || stKanriNo == null)
                    {
                        bRet = true;
                    }
                }
                catch (Exception )
                {
                    bRet = true;
                }

                if (bRet == true)
                {
                    DetailDiv.Visible = false;

                    arrayMessage.Add(Def.DefMsg_URLERR);
                    bcom.ShowMessage(csType, csManager, arrayMessage);
                    return;
                }

                // Windowsログイン・ユーザマスタチェック
                bRet = bcom.CheckUser();
                if (bRet)
                {
                    DetailDiv.Visible = false;

                    arrayMessage.Add(Def.DefMsg_USERERR);
                    bcom.ShowMessage(csType, csManager, arrayMessage);
                    return;
                }

                // 検索ログ登録
                //20170201 機能改善 START
                //result = bcom.RegistLogData(Def.DefTYPE_DETAIL, "", int.Parse(stKanriNo));
                result = bcom.RegistLogData(Def.DefTYPE_DETAIL, "", int.Parse(stKanriNo), DateTime.Now);
                //20170201 機能改善 END

                // 過去トラデータ取得
                dtTrableData = bcom.GetTroubleData(stKanriNo);
                if (dtTrableData.Rows.Count == 0)
                {
                    DetailDiv.Visible = false;

                    arrayMessage.Add(Def.DefMsg_DATA_NOTFOUND);
                    bcom.ShowMessage(csType, csManager, arrayMessage);
                    return;
                }

                // 画面表示処理
                initialDisp(dtTrableData, bcom.GetLinkForder());

            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmDetail", "Page_Load", ex, this.Response);
            }
        }
        #endregion

        #region 固有関数

        #region 画面表示処理
        /// <summary>
        /// 画面表示処理
        /// </summary>
        private void initialDisp(DataTable dtTrableData, String strLinkDir)
        {
            // 画面項目編集
            lblKOUMOKU_KANRI_NO.Text = dtTrableData.Rows[0]["KOUMOKU_KANRI_NO"].ToString().Trim();  // 項目管理№
            lblFOLLOW_INFO.Text = dtTrableData.Rows[0]["FOLLOW_INFO"].ToString().Trim();            // フォロー状況
            lblKOUMOKU.Text = dtTrableData.Rows[0]["KOUMOKU"].ToString().Trim();                    // 項目

            // 開発符号
            string stWord = dtTrableData.Rows[0]["FUGO_NAME1"].ToString().Trim();

            if (!(dtTrableData.Rows[0]["FUGO_NAME2"].ToString().Trim() == "" || dtTrableData.Rows[0]["FUGO_NAME2"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["FUGO_NAME2"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["FUGO_NAME3"].ToString().Trim() == "" || dtTrableData.Rows[0]["FUGO_NAME3"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["FUGO_NAME3"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["FUGO_NAME4"].ToString().Trim() == "" || dtTrableData.Rows[0]["FUGO_NAME4"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["FUGO_NAME4"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["FUGO_NAME5"].ToString().Trim() == "" || dtTrableData.Rows[0]["FUGO_NAME5"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["FUGO_NAME5"].ToString().Trim();
            }

            lblFUGO_NO.Text = stWord.Trim();

            lblSIMUKECHI_NAME.Text = dtTrableData.Rows[0]["SIMUKECHI_NAME"].ToString().Trim();      // 仕向地
            lblGENSYO_NAIYO.Text = dtTrableData.Rows[0]["GENSYO_NAIYO"].ToString().Trim().Replace("\r\n", "<BR>");  // 現象（内容）
            lblJYOUKYO.Text = dtTrableData.Rows[0]["JYOUKYO"].ToString().Trim().Replace("\r\n", "<BR>");    // 状況
            lblGENIN.Text = dtTrableData.Rows[0]["GENIN"].ToString().Trim().Replace("\r\n", "<BR>");        // 原因
            lblTAISAKU.Text = dtTrableData.Rows[0]["TAISAKU"].ToString().Trim().Replace("\r\n", "<BR>");    // 対策
            // 未発見理由
            lblKAIHATU_MIHAKKEN_RIYU.Text = dtTrableData.Rows[0]["KAIHATU_MIHAKKEN_RIYU"].ToString().Trim().Replace("\r\n", "<BR>");
            lblSQB_KANTEN.Text = dtTrableData.Rows[0]["SQB_KANTEN"].ToString().Trim().Replace("\r\n", "<BR>");  // SQB観点

            lblSAIHATU_SEKKEI.Text = dtTrableData.Rows[0]["SAIHATU_SEKKEI"].ToString().Trim().Replace("\r\n", "<BR>");  // 再発・設計
            lblSAIHATU_HYOUKA.Text = dtTrableData.Rows[0]["SAIHATU_HYOUKA"].ToString().Trim().Replace("\r\n", "<BR>");  // 再発・評価

            // 重要度ランク
            lblRANK.Text = dtTrableData.Rows[0]["RANK"].ToString().Trim();

            // RSC項目
            switch (dtTrableData.Rows[0]["RSC"].ToString().Trim())
            {
                case "R":
                    lblRSC.Text = "ﾘｺｰﾙ";
                    break;
                case "SC":
                    lblRSC.Text = "ｻｰﾋﾞｽｷｬﾝﾍﾟｰﾝ";
                    break;
                case "安":
                    lblRSC.Text = "安全部会止り";
                    break;
            }

            lblSYSTEM_NAME1.Text = dtTrableData.Rows[0]["SYSTEM_NAME1"].ToString().Trim();          // システム(1)
            lblBUNRUI_GENSYO_NAME.Text = dtTrableData.Rows[0]["BUNRUI_GENSYO_NAME"].ToString().Trim();  // 現象
            lblBUHIN_NAME1.Text = dtTrableData.Rows[0]["BUHIN_NAME1"].ToString().Trim();            // 部品(1)
            lblBUNRUI_CASE_NAME.Text = dtTrableData.Rows[0]["BUNRUI_CASE_NAME"].ToString().Trim();  // 原因
            lblKOBUHIN_NAME1.Text = dtTrableData.Rows[0]["KOBUHIN_NAME1"].ToString().Trim();        // 子部品(1)
            lblBY_PU.Text = dtTrableData.Rows[0]["BY_PU"].ToString().Trim();                        // PUBY区分
            lblSYSTEM_NAME2.Text = dtTrableData.Rows[0]["SYSTEM_NAME2"].ToString().Trim();          // システム(2)
            lblSEIGYO_UNIT_NAME.Text = dtTrableData.Rows[0]["SEIGYO_UNIT_NAME"].ToString().Trim();  // 制御ユニット名称

            lblBUHIN_NAME2.Text = dtTrableData.Rows[0]["BUHIN_NAME2"].ToString().Trim();                // 部品(2)
            lblSEIGYO_GENSYO_NAME.Text = dtTrableData.Rows[0]["SEIGYO_GENSYO_NAME"].ToString().Trim();  // 制御系現象

            lblKOBUHIN_NAME2.Text = dtTrableData.Rows[0]["KOBUHIN_NAME2"].ToString().Trim();            // 子部品(2)
            lblSEIGYO_FACTOR_NAME.Text = dtTrableData.Rows[0]["SEIGYO_FACTOR_NAME"].ToString().Trim();  // 制御系要因

            // BLK No.
            stWord = dtTrableData.Rows[0]["BLKNO1"].ToString().Trim();
            if (!(dtTrableData.Rows[0]["BLKNO2"].ToString().Trim() == "" || dtTrableData.Rows[0]["BLKNO2"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BLKNO2"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["BLKNO3"].ToString().Trim() == "" || dtTrableData.Rows[0]["BLKNO3"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BLKNO3"].ToString().Trim();
            }
            lblBLKNO.Text = stWord.Trim();


            lblKATA_NAME.Text = dtTrableData.Rows[0]["KATA_NAME"].ToString().Trim();            // 車型/特殊
            
            // 部品番号(上5ｹﾀ)
            stWord = dtTrableData.Rows[0]["BUHIN_BANGO1"].ToString().Trim();
            if (!(dtTrableData.Rows[0]["BUHIN_BANGO2"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUHIN_BANGO2"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUHIN_BANGO2"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["BUHIN_BANGO3"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUHIN_BANGO3"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUHIN_BANGO3"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["BUHIN_BANGO4"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUHIN_BANGO4"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUHIN_BANGO4"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["BUHIN_BANGO5"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUHIN_BANGO5"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUHIN_BANGO5"].ToString().Trim();
            }
            lblBUHIN_BANGO.Text = stWord.Trim();

            // 重保/法規
            switch (dtTrableData.Rows[0]["JYUYO_HOUKI"].ToString().Trim())
            {
                case "重保":
                    lblJYUYO_HOUKI.Text = "重要保安部品（重点的に管理している部品）";
                    break;
                case "重品":
                    lblJYUYO_HOUKI.Text = "重要品質";
                    break;
                case "法規":
                    lblJYUYO_HOUKI.Text = "各国の車両の環境や安全に対する規定（にかかわる案件か）";
                    break;
            }

            lblEGTM_NAME.Text = dtTrableData.Rows[0]["EGTM_NAME"].ToString().Trim();    // EG/TM形式
            
            // 外製主務
            if (dtTrableData.Rows[0]["SYUMU_GAISEI"].ToString().Trim() == "○")
            {
                lblSYUMU_GAISEI.Text = "外製主務";
            }

            lblHAIKI_NAME.Text = dtTrableData.Rows[0]["HAIKI_NAME"].ToString().Trim();  // 排気量

            // 製造主務
            if (dtTrableData.Rows[0]["SYUMU_SEIZO"].ToString().Trim() == "○")
            {
                lblSYUMU_SEIZO.Text = "製造主務";
            }

            // 設計部署
            stWord = dtTrableData.Rows[0]["BUSYO_SEKKEI1"].ToString().Trim();
            if (!(dtTrableData.Rows[0]["BUSYO_SEKKEI2"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUSYO_SEKKEI2"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUSYO_SEKKEI2"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["BUSYO_SEKKEI3"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUSYO_SEKKEI3"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUSYO_SEKKEI3"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["BUSYO_SEKKEI4"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUSYO_SEKKEI4"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUSYO_SEKKEI4"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["BUSYO_SEKKEI5"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUSYO_SEKKEI5"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUSYO_SEKKEI5"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["BUSYO_SEKKEI6"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUSYO_SEKKEI6"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUSYO_SEKKEI6"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["BUSYO_SEKKEI7"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUSYO_SEKKEI7"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUSYO_SEKKEI7"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["BUSYO_SEKKEI8"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUSYO_SEKKEI8"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUSYO_SEKKEI8"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["BUSYO_SEKKEI9"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUSYO_SEKKEI9"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUSYO_SEKKEI9"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["BUSYO_SEKKEI10"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUSYO_SEKKEI10"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUSYO_SEKKEI10"].ToString().Trim();
            }
            lblBUSYO_SEKKEI.Text = stWord.Trim();

            // 評価部署
            stWord = dtTrableData.Rows[0]["BUSYO_HYOUKA1"].ToString().Trim();
            if (!(dtTrableData.Rows[0]["BUSYO_HYOUKA2"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUSYO_HYOUKA2"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUSYO_HYOUKA2"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["BUSYO_HYOUKA3"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUSYO_HYOUKA3"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUSYO_HYOUKA3"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["BUSYO_HYOUKA4"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUSYO_HYOUKA4"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUSYO_HYOUKA4"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["BUSYO_HYOUKA5"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUSYO_HYOUKA5"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUSYO_HYOUKA5"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["BUSYO_HYOUKA6"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUSYO_HYOUKA6"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUSYO_HYOUKA6"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["BUSYO_HYOUKA7"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUSYO_HYOUKA7"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUSYO_HYOUKA7"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["BUSYO_HYOUKA8"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUSYO_HYOUKA8"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUSYO_HYOUKA8"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["BUSYO_HYOUKA9"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUSYO_HYOUKA9"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUSYO_HYOUKA9"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["BUSYO_HYOUKA10"].ToString().Trim() == "" || dtTrableData.Rows[0]["BUSYO_HYOUKA10"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["BUSYO_HYOUKA10"].ToString().Trim();
            }
            lblBUSYO_HYOUKA.Text = stWord.Trim();

            // 設通No.
            ecsList = new List<string>();
            if (!(dtTrableData.Rows[0]["SETTU_NO1"].ToString().Trim() == "" || dtTrableData.Rows[0]["SETTU_NO1"].ToString().Trim() == null))
            { 
                ecsList.Add(dtTrableData.Rows[0]["SETTU_NO1"].ToString().Trim());
            }
            if (!(dtTrableData.Rows[0]["SETTU_NO2"].ToString().Trim() == "" || dtTrableData.Rows[0]["SETTU_NO2"].ToString().Trim() == null))
            {
                ecsList.Add(dtTrableData.Rows[0]["SETTU_NO2"].ToString().Trim());
            }
            if (!(dtTrableData.Rows[0]["SETTU_NO3"].ToString().Trim() == "" || dtTrableData.Rows[0]["SETTU_NO3"].ToString().Trim() == null))
            {
                ecsList.Add(dtTrableData.Rows[0]["SETTU_NO3"].ToString().Trim());
            }
            if (!(dtTrableData.Rows[0]["SETTU_NO4"].ToString().Trim() == "" || dtTrableData.Rows[0]["SETTU_NO4"].ToString().Trim() == null))
            {
                ecsList.Add(dtTrableData.Rows[0]["SETTU_NO4"].ToString().Trim());
            }
            if (!(dtTrableData.Rows[0]["SETTU_NO5"].ToString().Trim() == "" || dtTrableData.Rows[0]["SETTU_NO5"].ToString().Trim() == null))
            {
               ecsList.Add(dtTrableData.Rows[0]["SETTU_NO5"].ToString().Trim());
            }

            // 資料No.
            stWord = dtTrableData.Rows[0]["SIRYOU_NO1"].ToString().Trim();

            if (!(dtTrableData.Rows[0]["SIRYOU_NO2"].ToString().Trim() == "" || dtTrableData.Rows[0]["SIRYOU_NO2"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["SIRYOU_NO2"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["SIRYOU_NO3"].ToString().Trim() == "" || dtTrableData.Rows[0]["SIRYOU_NO3"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["SIRYOU_NO3"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["SIRYOU_NO4"].ToString().Trim() == "" || dtTrableData.Rows[0]["SIRYOU_NO4"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["SIRYOU_NO4"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["SIRYOU_NO5"].ToString().Trim() == "" || dtTrableData.Rows[0]["SIRYOU_NO5"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["SIRYOU_NO5"].ToString().Trim();
            }
            lblSIRYOU_NO.Text = stWord.Trim();

            ViewState["LINK_OK"] = "NG";
            ViewState["KOUMOKU_KANRI_NO"] = ""; // 項目管理№

            // 関連資料
            // 2017/02/14 Kanda フォルダーOPENから対象のファイルリスト出力に変更
            if (dtTrableData.Rows[0]["LINK_FOLDER_PATH"].ToString().Trim() != "")
            {
                ViewState["SIRYO_DIR"] = strLinkDir + dtTrableData.Rows[0]["LINK_FOLDER_PATH"].ToString().Trim();   // 関連資料

                if (bcom.CheckFolder((String)ViewState["SIRYO_DIR"].ToString().Trim()) == 0)
                {
                    ViewState["LINK_OK"] = "OK";
                    kanrenSiryoPath = strLinkDir;
                    ViewState["SIRYO_DIR"] = "file:" + (String)ViewState["SIRYO_DIR"].ToString().Trim().Replace("\\", "/");
                    ViewState["KOUMOKU_KANRI_NO"] = dtTrableData.Rows[0]["KOUMOKU_KANRI_NO"].ToString().Trim();     // 項目管理№

                    kanrenSiryo = Directory.GetFiles(kanrenSiryoPath + dtTrableData.Rows[0]["LINK_FOLDER_PATH"], Def.DefPDF_Asterisk + Def.DefPDF_ExtensionPDF);
                    kanrenSiryoName = Directory.GetFiles(kanrenSiryoPath + dtTrableData.Rows[0]["LINK_FOLDER_PATH"], Def.DefPDF_Asterisk + Def.DefPDF_ExtensionPDF);

                    for (int i = 0; i < kanrenSiryo.Length; i++) {
                        kanrenSiryo[i] = kanrenSiryo[i].Replace(kanrenSiryoPath, "");
                        kanrenSiryoName[i] = kanrenSiryoName[i].Replace(kanrenSiryoPath, "");
                        kanrenSiryo[i] = kanrenSiryo[i].Replace("\\", "<>");
                    }
                }
            }

            lblKANREN_KANRI_NO.Text = dtTrableData.Rows[0]["KANREN_KANRI_NO"].ToString().Trim();    // 関連項目管理№
            lblKEYWORD.Text = dtTrableData.Rows[0]["KEYWORD"].ToString().Trim();                    // キーワード

            if (dtTrableData.Rows[0]["RELIABILITY"].ToString().Trim() == "米R" )
            {
                lblRELIABILITY.Text = "対象"; // RELIABILITY
            }

            lblKRAME.Text = "";             // ｸﾚｰﾑ費/TOP40
            lblRIPRO.Text = "";             // リプロ/TOP20

            if (!(dtTrableData.Rows[0]["FOLLOW_NO"].ToString().Trim() == "" || dtTrableData.Rows[0]["FOLLOW_NO"].ToString().Trim() == null))
            {
                // TOP40チェック
                if (  bdetail.ChkTOP40(dtTrableData.Rows[0]["FOLLOW_NO"].ToString().Trim()))
                {
                    lblKRAME.Text = "対象";
                }

                // リプロ20チェック
                if (  bdetail.ChkRipro20(dtTrableData.Rows[0]["FOLLOW_NO"].ToString().Trim()))
                {
                    lblRIPRO.Text = "対象";
                }
            }
        }

        #endregion
        #endregion
    }
}