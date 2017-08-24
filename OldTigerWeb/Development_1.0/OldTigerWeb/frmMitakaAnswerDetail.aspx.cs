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
    public partial class frmMitakaAnswerDetail : System.Web.UI.Page
    {
        #region "フィールド"
        CommonLogic bcom = new CommonLogic();
        public MitakaAnswerData _MitakaAnswerData;

        public MitakaData _MitakaData;
        #endregion

        #region "メソッド"

        #region "イベント処理"
        /// <summary>
        /// ページ読み込み処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                
                string strManageNo = ""; // 管理番号
                string strAnswerSystemNo = ""; // 回答対象システムNo
                string strAnswerDepartmentCode = ""; // 回答対象部署コード

                ClientScriptManager csManager = Page.ClientScript;
                Type csType = this.GetType();
                ArrayList arrayMessage = new ArrayList();

                // 観たか管理番号、回答対象システムNo、回答対象部署コード
                try
                {
                    strManageNo = Request.QueryString.Get("ManageNo").Trim();
                    strAnswerSystemNo = Request.QueryString.Get("SystemNo").Trim();
                    strAnswerDepartmentCode = Request.QueryString.Get("DepartmentCode").Trim();
                    if (strManageNo == "" || strManageNo == null) 
                    {
                        bRet = true;
                    }
                    if (strAnswerSystemNo == "" || strAnswerSystemNo == null)
                    {
                        bRet = true;
                    }
                    if (strAnswerDepartmentCode == "" || strAnswerDepartmentCode == null)
                    {
                        bRet = true;
                    }
                }
                catch (Exception)
                {
                    bRet = true;
                }

                if (bRet == true)
                {
                    //DetailDiv.Visible = false; // 画面詳細項目を表示しない

                    arrayMessage.Add(Def.DefMsg_URLERR);
                    bcom.ShowMessage(csType, csManager, arrayMessage);
                    return;
                }

                // Windowsログイン・ユーザマスタチェック
                bRet = bcom.CheckUser();
                if (bRet)
                {
                    //DetailDiv.Visible = false; // 画面詳細項目を表示しない

                    arrayMessage.Add(Def.DefMsg_USERERR);
                    bcom.ShowMessage(csType, csManager, arrayMessage);
                    return;
                }

                // 過去トラ観たか回答情報 コンストラクタ
                // 過去トラ観たか回答、過去トラ情報のデータ取得
                _MitakaAnswerData = new MitakaAnswerData(strManageNo, strAnswerSystemNo, strAnswerDepartmentCode);

                // ユーザID取得
                string sUser = bcom.GetWindowsUser();

                // 過去トラ観たか情報 コンストラクタ
                // 過去トラ観たかヘッダーのデータ取得
                _MitakaData = new MitakaData(strManageNo, sUser, "HeadOnly");

                // 画面表示処理
                initialDisp();

            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmDetail", "Page_Load", ex, this.Response);
            }
        }

        /// <summary>
        /// 保存処理（保存ボタン押下時）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(Object sender, EventArgs e)
        {
            String strStatus = ""; // 進捗状況
            
            if (RdoUnconfirmed.Checked)
            {
                strStatus = "10";
            }
            else if (RdoPending.Checked)
            {
                strStatus = "20";
            }
            else if (RdoConfirmed.Checked)
            {
                strStatus = "90";
            }
            else if (RdoNotApply.Checked)
            {
                strStatus = "99";
            }

            var answer = _MitakaAnswerData.MitakaAnswerTargetData;
            answer["STATUS"] = strStatus; // 進捗状況
            answer["ANSWER_CONTENT"] = TxtMitakaAnswer.Text; // 回答内容

            // 過去トラ観たか回答情報 過去トラ観たか回答情報登録・更新
            _MitakaAnswerData.postMitakaAnswerData();
        }
        #endregion

        #region "画面固有処理"
        /// <summary>
        /// 画面表示処理
        /// </summary>
        protected void initialDisp()
        {
            // 観たかヘッダー．タイトル
            ViewState["TITLE"] = _MitakaData.Title;

            // 観たかヘッダー．状況
            ViewState["STATUS"] = _MitakaData.Status;

            // 回答対象部署
            ViewState["ANSWER_DEVISION_CODE"] = _MitakaAnswerData.MitakaAnswerTargetData["ANSWER_DEVISION_CODE"].ToString();

            ViewState["KOUMOKU_KANRI_NO"] = _MitakaAnswerData.MitakaAnswerTargetData["KOUMOKU_KANRI_NO"].ToString(); // 項目管理No
            ViewState["KOUMOKU"] = _MitakaAnswerData.MitakaAnswerTargetData["KOUMOKU"].ToString(); // 項目
            // FMC 開発符号名(1)～(5)
            string stWord = _MitakaAnswerData.MitakaAnswerTargetData["FUGO_NAME1"].ToString().Trim();

            if (!(_MitakaAnswerData.MitakaAnswerTargetData["FUGO_NAME2"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["FUGO_NAME2"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["FUGO_NAME2"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["FUGO_NAME3"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["FUGO_NAME3"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["FUGO_NAME3"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["FUGO_NAME4"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["FUGO_NAME4"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["FUGO_NAME4"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["FUGO_NAME5"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["FUGO_NAME5"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["FUGO_NAME5"].ToString().Trim();
            }
            ViewState["FUGO_NAME"] = stWord;
            ViewState["SIMUKECHI_NAME"] = _MitakaAnswerData.MitakaAnswerTargetData["SIMUKECHI_NAME"].ToString(); // 仕向地
            ViewState["GENSYO_NAIYO"] = _MitakaAnswerData.MitakaAnswerTargetData["GENSYO_NAIYO"].ToString().Replace("\r\n", "<BR>"); // 現象（内容）
            ViewState["JYOUKYO"] = _MitakaAnswerData.MitakaAnswerTargetData["JYOUKYO"].ToString().Replace("\r\n", "<BR>"); // 状況
            ViewState["GENIN"] = _MitakaAnswerData.MitakaAnswerTargetData["GENIN"].ToString().Replace("\r\n", "<BR>"); // 原因
            ViewState["TAISAKU"] = _MitakaAnswerData.MitakaAnswerTargetData["TAISAKU"].ToString().Replace("\r\n", "<BR>"); // 対策
            ViewState["KAIHATU_MIHAKKEN_RIYU"] = _MitakaAnswerData.MitakaAnswerTargetData["KAIHATU_MIHAKKEN_RIYU"].ToString().Replace("\r\n", "<BR>"); // 開発時の流出要因
            ViewState["SQB_KANTEN"] = _MitakaAnswerData.MitakaAnswerTargetData["SQB_KANTEN"].ToString().Replace("\r\n", "<BR>"); // 確認の観点
            ViewState["SAIHATU_SEKKEI"] = _MitakaAnswerData.MitakaAnswerTargetData["SAIHATU_SEKKEI"].ToString().Replace("\r\n", "<BR>"); // 再発防止策(設計面）
            ViewState["SAIHATU_HYOUKA"] = _MitakaAnswerData.MitakaAnswerTargetData["SAIHATU_HYOUKA"].ToString().Replace("\r\n", "<BR>"); // 再発防止策(評価面）
            ViewState["RANK"] = _MitakaAnswerData.MitakaAnswerTargetData["RANK"].ToString(); // 重要度ランク
            // RSC項目
            switch (_MitakaAnswerData.MitakaAnswerTargetData["RSC"].ToString().Trim())
            {
                case "R":
                    ViewState["RSC"] = "ﾘｺｰﾙ";
                    break;
                case "SC":
                    ViewState["RSC"] = "ｻｰﾋﾞｽｷｬﾝﾍﾟｰﾝ";
                    break;
                case "安":
                    ViewState["RSC"] = "安全部会止り";
                    break;
            }
            ViewState["SYSTEM_NAME1"] = _MitakaAnswerData.MitakaAnswerTargetData["SYSTEM_NAME1"].ToString(); // システム(1)
            ViewState["BUNRUI_GENSYO_NAME"] = _MitakaAnswerData.MitakaAnswerTargetData["BUNRUI_GENSYO_NAME"].ToString(); // 現象
            ViewState["BUHIN_NAME1"] = _MitakaAnswerData.MitakaAnswerTargetData["BUHIN_NAME1"].ToString();            // 部品(1)
            ViewState["BUNRUI_CASE_NAME"] = _MitakaAnswerData.MitakaAnswerTargetData["BUNRUI_CASE_NAME"].ToString();  // 原因
            ViewState["KOBUHIN_NAME1"] = _MitakaAnswerData.MitakaAnswerTargetData["KOBUHIN_NAME1"].ToString();        // 子部品(1)
            ViewState["BY_PU"] = _MitakaAnswerData.MitakaAnswerTargetData["BY_PU"].ToString();                        // PUBY区分
            ViewState["SYSTEM_NAME2"] = _MitakaAnswerData.MitakaAnswerTargetData["SYSTEM_NAME2"].ToString();          // システム(2)
            ViewState["SEIGYO_UNIT_NAME"] = _MitakaAnswerData.MitakaAnswerTargetData["SEIGYO_UNIT_NAME"].ToString();  // 制御ユニット名称
            ViewState["BUHIN_NAME2"] = _MitakaAnswerData.MitakaAnswerTargetData["BUHIN_NAME2"].ToString();                // 部品(2)
            ViewState["SEIGYO_GENSYO_NAME"] = _MitakaAnswerData.MitakaAnswerTargetData["SEIGYO_GENSYO_NAME"].ToString();  // 制御系現象
            ViewState["KOBUHIN_NAME2"] = _MitakaAnswerData.MitakaAnswerTargetData["KOBUHIN_NAME2"].ToString();            // 子部品(2)
            ViewState["SEIGYO_FACTOR_NAME"] = _MitakaAnswerData.MitakaAnswerTargetData["SEIGYO_FACTOR_NAME"].ToString();  // 制御系要因
            // BLK No.
            stWord = _MitakaAnswerData.MitakaAnswerTargetData["BLKNO1"].ToString().Trim();
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BLKNO2"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BLKNO2"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BLKNO2"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BLKNO3"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BLKNO3"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BLKNO3"].ToString().Trim();
            }
            ViewState["BLKNO"] = stWord.Trim();
            ViewState["KATA_NAME"] = _MitakaAnswerData.MitakaAnswerTargetData["KATA_NAME"].ToString(); // 車型/特殊
            // 部品番号(上5ｹﾀ)
            stWord = _MitakaAnswerData.MitakaAnswerTargetData["BUHIN_BANGO1"].ToString().Trim();
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUHIN_BANGO2"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUHIN_BANGO2"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUHIN_BANGO2"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUHIN_BANGO3"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUHIN_BANGO3"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUHIN_BANGO3"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUHIN_BANGO4"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUHIN_BANGO4"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUHIN_BANGO4"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUHIN_BANGO5"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUHIN_BANGO5"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUHIN_BANGO5"].ToString().Trim();
            }
            ViewState["BUHIN_BANGO"] = stWord.Trim();
            // 重保/法規
            switch (_MitakaAnswerData.MitakaAnswerTargetData["JYUYO_HOUKI"].ToString().Trim())
            {
                case "重保":
                    ViewState["JYUYO_HOUKI"] = "重要保安部品（重点的に管理している部品）";
                    break;
                case "重品":
                    ViewState["JYUYO_HOUKI"] = "重要品質";
                    break;
                case "法規":
                    ViewState["JYUYO_HOUKI"] = "各国の車両の環境や安全に対する規定（にかかわる案件か）";
                    break;
            }
            ViewState["EGTM_NAME"] = _MitakaAnswerData.MitakaAnswerTargetData["EGTM_NAME"].ToString().Trim();    // EGTM形式
            // 外製主務
            if (_MitakaAnswerData.MitakaAnswerTargetData["SYUMU_GAISEI"].ToString().Trim() == "○")
            {
                ViewState["SYUMU_GAISEI"] = "外製主務";
            }
            ViewState["HAIKI_NAME"] = _MitakaAnswerData.MitakaAnswerTargetData["HAIKI_NAME"].ToString().Trim();  // 排気量
            // 製造主務
            if (_MitakaAnswerData.MitakaAnswerTargetData["SYUMU_SEIZO"].ToString().Trim() == "○")
            {
                ViewState["SYUMU_SEIZO"] = "製造主務";
            }
            // 設計部署
            stWord = _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI1"].ToString().Trim();
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI2"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI2"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI2"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI3"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI3"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI3"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI4"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI4"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI4"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI5"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI5"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI5"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI6"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI6"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI6"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI7"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI7"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI7"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI8"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI8"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI8"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI9"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI9"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI9"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI10"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI10"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_SEKKEI10"].ToString().Trim();
            }
            ViewState["BUSYO_SEKKEI"] = stWord.Trim();
            // 評価部署
            stWord = _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA1"].ToString().Trim();
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA2"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA2"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA2"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA3"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA3"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA3"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA4"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA4"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA4"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA5"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA5"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA5"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA6"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA6"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA6"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA7"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA7"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA7"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA8"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA8"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA8"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA9"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA9"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA9"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA10"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA10"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["BUSYO_HYOUKA10"].ToString().Trim();
            }
            ViewState["BUSYO_HYOUKA"] = stWord.Trim();
            // 設通No.
            stWord = "";
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["SETTU_NO1"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["SETTU_NO1"].ToString().Trim() == null))
            {
                stWord = (_MitakaAnswerData.MitakaAnswerTargetData["SETTU_NO1"].ToString().Trim());
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["SETTU_NO2"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["SETTU_NO2"].ToString().Trim() == null))
            {
                stWord += ", " + (_MitakaAnswerData.MitakaAnswerTargetData["SETTU_NO2"].ToString().Trim());
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["SETTU_NO3"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["SETTU_NO3"].ToString().Trim() == null))
            {
                stWord += ", " + (_MitakaAnswerData.MitakaAnswerTargetData["SETTU_NO3"].ToString().Trim());
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["SETTU_NO4"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["SETTU_NO4"].ToString().Trim() == null))
            {
                stWord += ", " + (_MitakaAnswerData.MitakaAnswerTargetData["SETTU_NO4"].ToString().Trim());
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["SETTU_NO5"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["SETTU_NO5"].ToString().Trim() == null))
            {
                stWord += ", " + (_MitakaAnswerData.MitakaAnswerTargetData["SETTU_NO5"].ToString().Trim());
            }
            ViewState["SETTU_NO"] = stWord.Trim();
            // 資料No.
            stWord = _MitakaAnswerData.MitakaAnswerTargetData["SIRYOU_NO1"].ToString().Trim();

            if (!(_MitakaAnswerData.MitakaAnswerTargetData["SIRYOU_NO2"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["SIRYOU_NO2"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["SIRYOU_NO2"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["SIRYOU_NO3"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["SIRYOU_NO3"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["SIRYOU_NO3"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["SIRYOU_NO4"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["SIRYOU_NO4"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["SIRYOU_NO4"].ToString().Trim();
            }
            if (!(_MitakaAnswerData.MitakaAnswerTargetData["SIRYOU_NO5"].ToString().Trim() == "" || 
                _MitakaAnswerData.MitakaAnswerTargetData["SIRYOU_NO5"].ToString().Trim() == null))
            {
                stWord += ", " + _MitakaAnswerData.MitakaAnswerTargetData["SIRYOU_NO5"].ToString().Trim();
            }
            ViewState["SIRYOU_NO"] = stWord.Trim();
            ViewState["KANREN_KANRI_NO"] = _MitakaAnswerData.MitakaAnswerTargetData["KANREN_KANRI_NO"].ToString().Trim(); // 関連項目管理No
            ViewState["KEYWORD"] = _MitakaAnswerData.MitakaAnswerTargetData["KEYWORD"].ToString().Trim(); // キーワード
            if (_MitakaAnswerData.MitakaAnswerTargetData["RELIABILITY"].ToString().Trim() == "米R")
            {
                ViewState["RELIABILITY"] = "対象"; // Reliabilty
            }
        }
        #endregion

        #endregion
    }
}