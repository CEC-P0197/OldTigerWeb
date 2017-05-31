using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IGC.Ben.Api.Core;
using IGC.Ben.Api.Core.Cache;

namespace OldTigerWeb
{
    public partial class frmClientView : System.Web.UI.Page
    {
        public string title="", filePath = "";
        public string sessionId;
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
                // パラメータ取得
                // HelpSerch:過去トラ検索用Helpファイル
                var fileKbn = "";
                fileKbn = Request.QueryString.Get("VIEWFILE_KBN").Trim();

                if (filePath == "" || filePath == null)
                {
                    bRet = true;
                }

                ClientScriptManager csManager = Page.ClientScript;
                Type csType = this.GetType();
                ArrayList arrayMessage = new ArrayList();

                // Windowsログイン・ユーザマスタチェック
                bRet = bcom.CheckUser();
                if (bRet)
                {
                    arrayMessage.Add(Const.Def.DefMsg_USERERR);
                    bcom.ShowMessage(csType, csManager, arrayMessage);
                    return;
                }

                // ファイルパス取得
                switch (fileKbn)
                {
                    case "HelpSerch":
                        filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["HelpSerch"];
                        break;
                    case "HelpFollow":
                        filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["HelpFollow"];
                        break;
                    // 2017.04.03 ta_kanda 追加 Start
                    case "HelpTop":
                        filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["HelpTop"];
                        break;
                    case "QaTop":
                        filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["QaTop"];
                        break;
                    // 2017.04.03 ta_kanda 追加 End
                    // 2017.04.04 ta_kanda 追加 Start
                    case "QaSerch":
                        filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["QaSerch"];
                        break;
                    case "QaFollow":
                        filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["QaFollow"];
                        break;
                    // 2017.04.04 ta_kanda 追加 End
                }
 
                string[] strTitle = filePath.Split('/');
                title = strTitle[strTitle.Length - 1].Trim();

                // ファイル存在チェック
                if (!File.Exists(filePath))
                {
                    arrayMessage.Add(Const.Def.DefMsg_FILE_NOTFOUND);
                    bcom.ShowMessage(csType, csManager, arrayMessage);
                    return;
                }

                // Bravaセッション取得
                BravaConnection conn = Providers.Connection.Create("http://" + System.Web.Configuration.WebConfigurationManager.AppSettings["Brava"]);
                sessionId = conn.GetSessionID();
                Page.DataBind();
            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmClientView", "Page_Load", ex, this.Response);
            }
        }
    }
}