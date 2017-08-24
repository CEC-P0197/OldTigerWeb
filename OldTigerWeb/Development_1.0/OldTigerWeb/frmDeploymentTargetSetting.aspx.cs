using System;
using System.Net;
using System.Data;
using OldTigerWeb.BuisinessLogic;

namespace OldTigerWeb
{
    public partial class frmDeploymentTargetSetting : System.Web.UI.Page
    {
        #region "フィールド"
        CommonLogic bcom = new CommonLogic();

        //BLExcel bex = new BLExcel();

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
        }
        /// <summary>
        /// PDFダウンロードボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDownloadPdf_Click(object sender, EventArgs e)
        {
            // EXCELファイル作成

            // PDF作成
            //bex.ExcelToPdf();
        }
        #endregion
        #endregion
    }
}