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
    public partial class frmDocument : System.Web.UI.Page
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
                string prm = Request.QueryString.Get(Const.Def.DefPDF_FileNo).Trim().Replace("<>","\\");
                filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["LinkForder"] + prm;
                string[] strTitle = filePath.Split('\\');
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
                bcom.ErrorProcess("frmDetail", "Page_Load", ex, this.Response);
            }
        }
    }
}