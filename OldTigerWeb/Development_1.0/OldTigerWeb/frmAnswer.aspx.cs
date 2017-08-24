using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Web.UI;
using OldTigerWeb.BuisinessLogic;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace OldTigerWeb
{
    public partial class frmAnswer : System.Web.UI.Page
    {
        CommonLogic bcom = new CommonLogic();
        BLDetail bdetail = new BLDetail();
 
        public string[] kanrenSiryo, kanrenSiryoName;
        public string kanrenSiryoPath;

        protected void Page_Load(object sender, EventArgs e)
        {
            // ポストバック時はリターン
            if (IsPostBack == true)
            {
                return;
            }

            //CommonLogic bcom = new CommonLogic();

            try
            {
                Boolean bRet = false;
                DataTable dtFollowData = null;

                string stFollowKey = "";
                string stEventName = "";

                String[] strArrayData;

                // 引数:フォローキー情報
                try
                {
                    stFollowKey = Request.QueryString.Get(Def.DefPARA_FOLLOW).Trim();
                    stEventName = Request.QueryString.Get(Def.DefPARA_EVENTNM).Trim();
                    if (stFollowKey == "" || stFollowKey == null)
                    {
                        bRet = true;
                    }
                    else
                    {
                        strArrayData = stFollowKey.Trim().Split(',');
                        if(strArrayData.Length == 7 )
                        {
                            ViewState["FMC_mc"] = strArrayData[0];
                            hdnFmcMc.Value = strArrayData[0];
                            ViewState["KAIHATU_ID"] = strArrayData[1];
                            hdnKaihatsuId.Value = strArrayData[1];
                            ViewState["BY_PU"] = strArrayData[2];
                            hdnByPu.Value = strArrayData[2];
                            ViewState["EVENT_NO"] = strArrayData[3];
                            hdnEventNo.Value = strArrayData[3];
                            ViewState["FOLLOW_NO"] = strArrayData[4];
                            hdnFollowNo.Value = strArrayData[4];
                            ViewState["KA_CODE"] = strArrayData[5];
                            hdnKaCode.Value = strArrayData[5];
                            ViewState["SYSTEM_NO"] = strArrayData[6];
                            hdnSystemNo.Value = strArrayData[6];
                        }
                        else { bRet = true; }
                    }
                }
                catch (Exception )
                {
                    bRet = true;
                }

                if (bRet == true)
                {
                    btnRegist.Enabled = false;
                    pnlDetail.Visible = false;

                    errorMessage(Def.DefMsg_URLERR);
                    return;
                }

                // Windowsログイン・ユーザマスタチェック
                bRet = bcom.CheckUser();
                if (bRet)
                {
                    pnlDetail.Visible = false;
                    btnRegist.Enabled = false;

                    errorMessage(Def.DefMsg_USERERR);
                    return;
                }

                // フォローデータ取得
                BuisinessLogic.BLAnswer bAnswer = new BuisinessLogic.BLAnswer();

                dtFollowData = bAnswer.GetFollowData(ViewState["FMC_mc"].ToString(),
                            ViewState["KAIHATU_ID"].ToString(),
                            ViewState["BY_PU"].ToString(), ViewState["EVENT_NO"].ToString(),
                            ViewState["FOLLOW_NO"].ToString(), ViewState["KA_CODE"].ToString(),
                            ViewState["SYSTEM_NO"].ToString() );
                if (dtFollowData.Rows.Count == 0)
                {
                    pnlDetail.Visible = false;
                    btnRegist.Enabled = false;

                    errorMessage(Def.DefMsg_DATA_NOTFOUND);
                    return;
                }

                // 画面表示処理
                initialDisp(dtFollowData, bcom.GetLinkForder());

                lblKacode.Text = ViewState["KA_CODE"].ToString();

                lblFollowInfo.Text = stEventName;

                txtAnswer.Focus();

                ViewState["SHARED_YMD"] = dtFollowData.Rows[0]["SHARED_YMD"].ToString().Trim(); // 更新日時
            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmAnswer", "Page_Load", ex, this.Response);
            }
        }

        // フォロー回答登録処理
        protected void btnRegist_Click(object sender, EventArgs e)
        {
            CommonLogic bcom = new CommonLogic();

            try
            {
                Boolean result = false;
                String strSindo;
                // 2016.04.20 Kanda 適用有無設計の更新を廃止
                // String strHyouka = "*";
                DataTable dtFollowData = null;

                BuisinessLogic.BLAnswer bAnswer = new BuisinessLogic.BLAnswer();

                dtFollowData = bAnswer.GetFollowData(ViewState["FMC_mc"].ToString(),
                            ViewState["KAIHATU_ID"].ToString(),
                            ViewState["BY_PU"].ToString(), ViewState["EVENT_NO"].ToString(),
                            ViewState["FOLLOW_NO"].ToString(), ViewState["KA_CODE"].ToString(),
                            ViewState["SYSTEM_NO"].ToString());
                if (dtFollowData.Rows.Count == 0)
                {
                    errorMessage(Def.DefMsg_KAITO_NOTFOUND);

                    return;
                }

                // 更新日時の排他チェック
                if (ViewState["SHARED_YMD"].ToString() != dtFollowData.Rows[0]["SHARED_YMD"].ToString())
                {
                    errorMessage(Def.DefMsg_KAITO_EDITED);

                    return;
                }

                if (rdoSindo1.Checked == true )
                {
                    strSindo = "済";
                }
                else if (rdoSindo2.Checked == true)
                {
                    strSindo = "△";
                }
                else if (rdoSindo3.Checked == true)
                {
                    strSindo = "×";
                }
                else
                {
                    strSindo = "－";
                    // 2016.04.20 Kanda 適用有無設計の更新を廃止
                    // strHyouka = " ";
                }

                // フォローデータ更新
                // 2016.04.20 Kanda 適用有無設計の更新を廃止
                // result = bAnswer.registFollowData(ViewState["FMC_mc"].ToString(),
                //             ViewState["KAIHATU_ID"].ToString(),
                //             ViewState["BY_PU"].ToString(), ViewState["EVENT_NO"].ToString(),
                //             ViewState["FOLLOW_NO"].ToString(), ViewState["KA_CODE"].ToString(),
                //             ViewState["SYSTEM_NO"].ToString(), strHyouka, strSindo,
                //             txtAnswer.Text.Trim(), bcom.GetWindowsUser()
                //             );
                result = bAnswer.registFollowData(ViewState["FMC_mc"].ToString(),
                            ViewState["KAIHATU_ID"].ToString(),
                            ViewState["BY_PU"].ToString(), ViewState["EVENT_NO"].ToString(),
                            ViewState["FOLLOW_NO"].ToString(), ViewState["KA_CODE"].ToString(),
                            ViewState["SYSTEM_NO"].ToString(),  strSindo,
                            txtAnswer.Text.Trim(), bcom.GetWindowsUser()
                            );

                // 画面終了
                endDisp("1");

            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmAnswer", "btnRegist_Click", ex, this.Response);
            }
        }

        #region 固有関数

        #region 画面表示処理
        /// <summary>
        /// 画面表示処理
        /// </summary>
        protected void initialDisp(DataTable dtTrableData, String strLinkDir)
        {
            //BuisinessLogic.BLDetail bdetail = new BuisinessLogic.BLDetail();
            //CommonLogic bCom = new CommonLogic();

            // 画面項目編集
            // ================================= 回答情報
            lblHearing.Text = dtTrableData.Rows[0]["HEARING"].ToString().Trim();                    // ヒヤリング要望

            switch (dtTrableData.Rows[0]["SINDO"].ToString().Trim())                                // 進度
            {
                case "済":
                    rdoSindo1.Checked = true;
                    break;
                case "△":
                    rdoSindo2.Checked = true;
                    break;
                case "×":
                    rdoSindo3.Checked = true;
                    break;
                case "－":
                    rdoSindo4.Checked = true;
                    break;
            }

            txtAnswer.Text = dtTrableData.Rows[0]["TAIOU_NAIYO"].ToString().Trim();                 // 対応内容

            // ================================= 過去トラ情報
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
            stWord = dtTrableData.Rows[0]["SETTU_NO1"].ToString().Trim();
            if (!(dtTrableData.Rows[0]["SETTU_NO2"].ToString().Trim() == "" || dtTrableData.Rows[0]["SETTU_NO2"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["SETTU_NO2"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["SETTU_NO3"].ToString().Trim() == "" || dtTrableData.Rows[0]["SETTU_NO3"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["SETTU_NO3"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["SETTU_NO4"].ToString().Trim() == "" || dtTrableData.Rows[0]["SETTU_NO4"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["SETTU_NO4"].ToString().Trim();
            }
            if (!(dtTrableData.Rows[0]["SETTU_NO5"].ToString().Trim() == "" || dtTrableData.Rows[0]["SETTU_NO5"].ToString().Trim() == null))
            {
                stWord += ", " + dtTrableData.Rows[0]["SETTU_NO5"].ToString().Trim();
            }
            lblSETTU_NO.Text = stWord.Trim();

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

            //if (dtTrableData.Rows[0]["LINK_FOLDER_PATH"].ToString().Trim() != "")
            //{
            //    ViewState["SIRYO_DIR"] = strLinkDir + dtTrableData.Rows[0]["LINK_FOLDER_PATH"].ToString().Trim();   // 関連資料

            //    if (bCom.CheckFolder((String)ViewState["SIRYO_DIR"].ToString().Trim()) == 0)
            //    {
            //        ViewState["LINK_OK"] = "OK";
            //        ViewState["SIRYO_DIR"] = "file:" + (String)ViewState["SIRYO_DIR"].ToString().Trim().Replace("\\", "/");
            //        ViewState["KOUMOKU_KANRI_NO"] = dtTrableData.Rows[0]["KOUMOKU_KANRI_NO"].ToString().Trim();     // 項目管理№
            //    }
            //}
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

                    for (int i = 0; i < kanrenSiryo.Length; i++)
                    {
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

        #region メッセージ表示処理
        /// <summary>
        /// メッセージ表示処理
        /// </summary>
        protected void errorMessage(String strMessage)
        {
            ClientScriptManager csManager = Page.ClientScript;
            Type csType = this.GetType();
            ArrayList arrayMessage = new ArrayList();

            CommonLogic bcom = new CommonLogic();

            arrayMessage.Add(strMessage);
            bcom.ShowMessage(csType, csManager, arrayMessage);
        }
        #endregion

        #region 画面終了処理
        /// <summary>
        /// 画面終了処理
        /// </summary>
        protected void endDisp(String strStat)
        {
            string csPopUp = "PopUpScript";
            string getString = "";

            ClientScriptManager csManager = Page.ClientScript;
            Type csType = this.GetType();
            ArrayList arrayMessage = new ArrayList();
            // Scriptの登録
            if (!csManager.IsStartupScriptRegistered(csType, csPopUp))
            {
                // 1) Script文字列の作成
                foreach (string arrayString in arrayMessage)
                {
                    getString = getString + arrayString.ToString() + "\\n";
                }

                // 2) Scriptの登録
                csManager.RegisterStartupScript(csType, csPopUp,
                    "confClose('" + strStat + "'); ", true);
            }
        }
        #endregion
        #endregion
    }
}