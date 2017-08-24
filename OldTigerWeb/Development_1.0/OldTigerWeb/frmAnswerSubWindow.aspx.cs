using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OldTigerWeb
{
    public partial class frmAnswerSubWindow : System.Web.UI.Page
    {
        public int gbStartPage = 0;
        public DataTable gbFollowDataOtherDept = null;
        public DataTable gbTenkaiKbn = null; // 2017/07/14 Add
        protected void Page_Load(object sender, EventArgs e)
        {
            // ポストバック時はリターン
            if (IsPostBack == true)
            {
                return;
            }

            CommonLogic bcom = new CommonLogic();

            try
            {
                Boolean bRet = false;

                string stFmcMc = "";
                string stKaihatsuId = "";
                string stByPu = "";
                string stEventNo = "";
                string stFollowNo = "";
                string stKaCode = "";
                string stSystemNo = "";

                // 引数:フォローキー情報
                try
                {
                    stFmcMc = Request.QueryString.Get(Def.DefPARA_FMCMC).Trim();
                    stKaihatsuId = Request.QueryString.Get(Def.DefPARA_KAIHATSUID).Trim();
                    stByPu = Request.QueryString.Get(Def.DefPARA_BYPU).Trim();
                    stEventNo = Request.QueryString.Get(Def.DefPARA_EVENTNO).Trim();
                    stFollowNo = Request.QueryString.Get(Def.DefPARA_FOLLOWNO).Trim();
                    stKaCode = Request.QueryString.Get(Def.DefPARA_KACODE).Trim();
                    stSystemNo = Request.QueryString.Get(Def.DefPARA_SYSTEMNO).Trim();
                    if (stFmcMc == "" || stFmcMc == null)
                    {
                        bRet = true;
                    }
                }
                catch (Exception )
                {
                    bRet = true;
                }

                // Windowsログイン・ユーザマスタチェック
                bRet = bcom.CheckUser();
                if (bRet)
                {
                    errorMessage(Def.DefMsg_USERERR);
                    return;
                }

                BuisinessLogic.BLAnswerSubWindow bAnswer = new BuisinessLogic.BLAnswerSubWindow();

                // 2017/07/14 Add Start
                // 展開区分取得
                gbTenkaiKbn = bAnswer.getTenkaiKbn(stFmcMc,
                            stKaihatsuId,
                            stByPu,
                            stEventNo);

                string stTenkaiKbn = "";
                stTenkaiKbn = gbTenkaiKbn.Rows[0]["TENKAI_KBN"].ToString();
                // 2017/07/14 Add End

                // 他部署フォローデータ取得
                gbFollowDataOtherDept = bAnswer.GetFollowDataOtherDept(stFmcMc,
                            stKaihatsuId,
                            stByPu,
                            stEventNo,
                            stFollowNo,
                            stKaCode,
                            stSystemNo,
                            stTenkaiKbn); // 2017/07/14 Add 引数に展開区分を追加
             }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmAnswerSubWindow", "Page_Load", ex, this.Response);
            }
        }

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

    }
}