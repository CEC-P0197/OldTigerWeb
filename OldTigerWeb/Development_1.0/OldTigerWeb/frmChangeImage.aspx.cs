using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualBasic;

using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;

namespace OldTigerWeb
{
    public partial class frmChangeImage : System.Web.UI.Page
    {
        #region 画面ロード処理
        /// <summary>
        /// 画面ロード処理
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // ポストバック時はリターン
            if (IsPostBack == true)
            {
                return;
            }

            CommonLogic bcom = new CommonLogic();
            ListBox1.ClearSelection();

            try
            {
                String infoFilePath, fldSight = "";

                fldSight = "fldChangeImage";

                infoFilePath = System.Web.Configuration.WebConfigurationManager.AppSettings[fldSight];
                string txtInfo = HttpContext.Current.Server.MapPath(infoFilePath);

                //ChangeImageフォルダ内のファイルをすべて取得する
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(txtInfo);
                IEnumerable<System.IO.FileInfo> files =
                    di.EnumerateFiles("*", System.IO.SearchOption.AllDirectories);

                //ファイル名を列挙する
                foreach (System.IO.FileInfo f in files)
                {
                    ListBox1.Items. Add(new ListItem(f.Name,f.FullName));
                }
            }

            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmTopPage", "btn_ChangeImage_Click", ex, this.Response);
            }
          
        }
        #endregion

        #region インデックス変更処理
        /// <summary>
        /// インデックス変更処理
        /// </summary>
        public void ListBox1_SelectedIndexChanged(object sender, System.EventArgs e) 
        {
            CommonLogic bcom = new CommonLogic();
            //Get the currently selected item in the ListBox.
            string curItem = ListBox1.SelectedValue;


            try
            {
                String infoImagePath, fldSight = "";

                fldSight = "fileImages";

                infoImagePath = System.Web.Configuration.WebConfigurationManager.AppSettings[fldSight];
                string txtInfo = HttpContext.Current.Server.MapPath(infoImagePath);

                string strImagePath = txtInfo;

                File.Copy(curItem, strImagePath, true);

                this.Dispose();

                //自分自身のフォームを閉じる
                System.Text.StringBuilder script = new System.Text.StringBuilder();
                script.Append("<script language='javascript'>\n");
                script.Append("self.opener = self;\n");
                script.Append("self.close();\n");
                script.Append("</script>\n");
                // 
                // JavaScriptを登録する。 
                // 
                this.Page.RegisterClientScriptBlock("SetText", script.ToString());

            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmTopPage", "btn_ChangeImage_Click", ex, this.Response);
            }
            this.Dispose();
          
        }
        #endregion

        #region キャンセルボタン押下処理
        /// <summary>
        /// キャンセルボタン押下処理
        /// </summary>
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            //自分自身のフォームを閉じる
            System.Text.StringBuilder script = new System.Text.StringBuilder();
            script.Append("<script language='javascript'>\n");
            script.Append("self.opener = self;\n");
            script.Append("self.close();\n");
            script.Append("</script>\n");
            // 
            // JavaScriptを登録する。 
            // 
            this.Page.RegisterClientScriptBlock("SetText", script.ToString());
        }
        #endregion
    }
}

    