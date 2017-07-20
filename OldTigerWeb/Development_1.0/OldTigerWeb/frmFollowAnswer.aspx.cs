using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OldTigerWeb
{
    public partial class frmFollowAnswer : System.Web.UI.Page
    {
        public int gbStartPage = 0;
        public DataTable gbFollowData = null;
        public DataTable kaCodeFollowData = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            // ポストバック時、回答画面の戻り以外はリターン
            
            if (IsPostBack == true) {
                // 回答更新後のリフレッシュ
                if (hdSubmit.Value == "1")
                {
                    DispDataGet();  // 画面表示データ取得処理
                }
                return;
            }

            CommonLogic bcom = new CommonLogic();

            try
            {
                String[] strArrayData;

                pnlFollowAnswer.Visible = false;

                String paraEventCode = (String)Session[Const.Def.DefPARA_FOLLOW];

                Boolean result = false;
            
                ClientScriptManager csManager = Page.ClientScript;
                Type csType = this.GetType();
                ArrayList arrayMessage = new ArrayList();

                // フォロー情報キー情報
                if (paraEventCode == "" || paraEventCode == null)
                {
                    btnDisp.Enabled = false;    // 表示ボタン使用不可
                    arrayMessage.Add(Const.Def.DefMsg_URLERR);
                    bcom.ShowMessage(csType, csManager, arrayMessage);
                    return;
                }

                // Windowsログイン・ユーザマスタチェック
                result = bcom.CheckUser();
                if (result)
                {
                    btnDisp.Enabled = false;    // 表示ボタン使用不可
                    arrayMessage.Add(Const.Def.DefMsg_USERERR);
                    bcom.ShowMessage(csType, csManager, arrayMessage);
                    return;
                }

                // 作業対象部署にフォーカス
                txtKacode.Focus();
                // HiddenField　フィールドクリア
                hdPageNo.Value = "0";

                strArrayData = paraEventCode.Trim().Split(',');
                ViewState["FMC_mc"] = strArrayData[0];
                ViewState["KAIHATU_ID"] = strArrayData[1];
                ViewState["BY_PU"] = strArrayData[2];
                ViewState["EVENT_NO"] = strArrayData[3];
                ViewState["FullEventName"] = strArrayData[4];
                Session[Const.Def.DefPARA_EVENTNM] = strArrayData[4];
                ViewState["EventName"] = strArrayData[5];

                // フォロー対象部署オートコンプリート処理
                GetKaCodeData(ViewState["FMC_mc"].ToString(), ViewState["KAIHATU_ID"].ToString(),
                        ViewState["BY_PU"].ToString(), ViewState["EVENT_NO"].ToString());

                Session[Const.Def.DefPARA_FOLLOW] = "";         // セッションクリア

            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmFollowAnswer", "Page_Load", ex, this.Response);
            }
        }

        // 表示クリック
        protected void btn_Disp_Click(Object sender, EventArgs e)
        {

            CommonLogic bcom = new CommonLogic();

            try
            {
                gbStartPage = 0;

                hdKacode.Value = txtKacode.Text.ToString().Trim();

                DispDataGet();  // 画面表示データ取得処理
            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmFollowAnswer", "btn_Disp_Click", ex, this.Response);
            }
        }

        #region 固有関数

        #region 画面表示データ取得処理
        /// <summary>
        /// 画面表示データ取得処理
        /// </summary>
        protected void DispDataGet()
        {
            BuisinessLogic.BLFollowAnswer bLogic = new BuisinessLogic.BLFollowAnswer();

            gbFollowData = bLogic.GetFollowDataList(ViewState["FMC_mc"].ToString(), ViewState["KAIHATU_ID"].ToString(),
                        ViewState["BY_PU"].ToString(), ViewState["EVENT_NO"].ToString(), hdKacode.Value);

            if (gbFollowData.Rows.Count > 0)
            {
                int pgsu = gbFollowData.Rows.Count / Const.Def.DefFOLLOW_LINECNT;
                int amari = gbFollowData.Rows.Count % Const.Def.DefFOLLOW_LINECNT;
                int curpage = int.Parse(hdPageNo.Value.ToString());
                if (pgsu < 1)
                {
                    pgsu = 1;
                }
                else
                {
                    if (amari != 0) pgsu++;
                }
                if (pgsu - 1 < curpage)
                {
                    pgsu = curpage - 1;
                    hdPageNo.Value = pgsu.ToString();
                }
            }
            else
            {
                hdPageNo.Value = "0";
            }

            gbStartPage = int.Parse(hdPageNo.Value.ToString());

            pnlFollowAnswer.Visible = true;

        }
        #endregion

        #region メッセージ表示処理
        /// <summary>
        /// メッセージ表示処理
        /// </summary>
        protected void registMessage(String strMessage)
        {
            ClientScriptManager csManager = Page.ClientScript;
            Type csType = this.GetType();
            ArrayList arrayMessage = new ArrayList();

            CommonLogic bcom = new CommonLogic();

            arrayMessage.Add(strMessage);
            bcom.ShowMessage(csType, csManager, arrayMessage);
        }
        #endregion

        // 2017/07/14 Add Start
        #region フォロー対象部署オートコンプリート処理
        /// <summary>
        /// フォロー対象部署オートコンプリート処理
        /// </summary>
        /// <param name="FMC_mc">FMC/mc区分</param>
        /// <param name="kaihatu_id">開発符号</param>
        /// <param name="by_pu">BYPU区分</param>
        /// <param name="event_no">イベントNO</param>
        /// <returns>結果ステータス</returns>
        protected void GetKaCodeData(String FMC_mc, String kaihatu_id, String by_pu, String event_no)
        {
            BuisinessLogic.BLFollowAnswer bLogic = new BuisinessLogic.BLFollowAnswer();

            // AutoComplete の課コードリスト取得
            kaCodeFollowData = bLogic.GetKaCodeFollowDataList(FMC_mc, kaihatu_id, by_pu, event_no);

            string kaCodeInfo = "";

            for (int i = 0; i < kaCodeFollowData.Rows.Count; i++)
            {
                if (i > 0) kaCodeInfo += ",\n";

                kaCodeInfo += "{ label: '" + kaCodeFollowData.Rows[i]["KA_CODE"].ToString().Trim() + "', value: '" + kaCodeFollowData.Rows[i]["KA_CODE"].ToString().Trim() + "'}";
            }

            ViewState["KaCodeInfo"] = kaCodeInfo;

        }
        #endregion
        // 2017/07/14 Add End

        #endregion
    }
}