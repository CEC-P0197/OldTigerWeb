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
    public partial class frmMitakaAnswer : System.Web.UI.Page
    {
        #region "フィールド"
        CommonLogic bcom = new CommonLogic();

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
        #endregion
        #endregion
    }
}