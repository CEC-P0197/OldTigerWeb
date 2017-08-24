using System;
using System.Net;
using System.Data;
using OldTigerWeb.BuisinessLogic;
using System.Web.UI;
using System.Collections;
using OldTigerWeb.DataAccess.Common;
using System.Collections.Generic;

namespace OldTigerWeb
{
    public partial class frmMitakaSelect : System.Web.UI.Page
    {
        #region "フィールド"
        CommonLogic bcom = new CommonLogic();
        CommonPageLogic cPageLogic = new CommonPageLogic();
        /// <summary>
        /// ユーザー情報フィールド
        /// </summary>
        private DataTable _UserMater;
        /// <summary>
        /// 過去トラ観たか管理Noフィールド
        /// </summary>
        private string _QueryManagementNo;
        /// <summary>
        /// 過去トラ観たか情報検索フィールド
        /// </summary>
        public MitakaSearchData _MitakaSearchData;
        /// <summary>
        /// 過去トラ観たか検索結果リストフィールド
        /// </summary>
        //public List<MitakaData> _SearchResultList;
        /// <summary>
        /// 過去トラ観たか情報フィールド
        /// </summary>
        public MitakaData _MitakaData;
        /// <summary>
        /// 過去トラ観たか回答情報フィールド
        /// </summary>
        public MitakaAnswerData _MitakaAnswerData;


        #endregion

        #region "プロパティ"
        /// <summary>
        /// 検索結果リスト
        /// </summary>
        //public List<MitakaData> SearchResultList
        //{
        //    get
        //    {
        //        return _SearchResultList;
        //    }
        //    set
        //    {
        //        if (value != null && value != _SearchResultList)
        //        {
        //            _SearchResultList = value;
        //        }
        //    }
        //}

        #endregion


        #region "メソッド"

        #region "イベント処理"

        protected void Page_Load(object sender, EventArgs e)
        {
            Boolean bRet = false;
            ClientScriptManager csManager = Page.ClientScript;
            Type csType = this.GetType();
            ArrayList arrayMessage = new ArrayList();

            // Windowsログイン・ユーザマスタチェック
            bRet = bcom.CheckUser();
            if (bRet)
            {
                arrayMessage.Add(Def.DefMsg_USERERR);
                bcom.ShowMessage(csType, csManager, arrayMessage);
                return;
            }

            // ログインユーザーID取得
            string sUser = bcom.GetWindowsUser();

            // ユーザー情報取得
            _UserMater = new SqlCommon().SelectUser(sUser);

            // クエリパラメータから管理番号を取得
            if (IsPostBack != true)
            {
                // クエリパラメータを取得する（過去トラ観たか管理No）
                if (Request.QueryString.Get("ManageNo") != null)
                {
                    _QueryManagementNo = Request.QueryString.Get("ManageNo");
                }

            }
            // 管理番号が取得できなかった場合
            if (_QueryManagementNo == null)
            {
                // 過去トラ観たか情報取得  初期処理
                //getMitakaSearchData();
            }

            // 過去トラ観たか情報取得
            getMitakaSearchData(sUser);

        }
        #endregion

        #region "画面固有処理"

        /// <summary>
        /// 過去トラ観たか情報検索取得  初期処理
        /// </summary>
        private void getMitakaSearchData()
        {
            // 初期処理
            ViewState["SEARCH_KEYWORD"] = ""; // 検索キーワード
            ViewState["SEARCH_CATEGORY"] = ""; // 検索カテゴリ
        }

        /// <summary>
        /// 過去トラ観たか情報検索取得
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        private void getMitakaSearchData(string userId)
        {
            // 過去トラ観たか情報検索インスタンス定義（初期化）
            // 過去トラ観たか情報検索 コンストラクタ
            //_MitakaData = new MitakaData(manageNo, userId);
            _MitakaSearchData = new MitakaSearchData(userId);

            // 観たか回答情報
            //_MitakaAnswerData = _MitakaData.MitakaAnswerData;

            ViewState["SEARCH_KEYWORD"] = Session[Def.DefPARA_WORD]; // 検索キーワード(キーワード検索)
            ViewState["SEARCH_CATEGORY"] = Session[Def.DefPARA_TABLE]; // 検索カテゴリ(カテゴリ検索)

            // 過去トラ観たかヘッダー情報
            _MitakaSearchData.searchMitakaDataMine();

        }


        #endregion

        #endregion
    }
}