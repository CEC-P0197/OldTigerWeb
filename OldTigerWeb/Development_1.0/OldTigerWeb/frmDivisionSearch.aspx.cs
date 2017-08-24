using System;
using System.Net;
using System.Data;
using System.Collections.Generic;
using OldTigerWeb.BuisinessLogic;
using OldTigerWeb.DataAccess;

namespace OldTigerWeb
{
    public partial class frmDivisionSearch : System.Web.UI.Page
    {
        #region "フィールド"
        /// <summary>
        /// 共通ロジックフィールド
        /// </summary>
        private CommonLogic _BLCommon;
        /// <summary>
        /// 部署検索ビジネスロジックフィールド
        /// </summary>
        private BLDivisionSearch _BlDivisionSearch;
        /// <summary>
        /// 部署検索DBフィールド
        /// </summary>
        private DADivisionSearch _DbDivisionSearch;
        /// <summary>
        /// 検索結果フィールド
        /// </summary>
        private DataTable _SearchResultList;
        #endregion


        #region プロパティ
        /// <summary>
        /// クエリパラメータ
        /// </summary>
        public string QueryString { get; set; }
        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResultList
        {
            get
            {
                if (_SearchResultList != null)
                    return _SearchResultList;

                return _DbDivisionSearch.getDivisionInfo();
            }
        }
        #endregion

        #region コンストラクタ
        public frmDivisionSearch()
        {
            _BLCommon = new CommonLogic();
            _BlDivisionSearch = new BLDivisionSearch();
            _DbDivisionSearch = new DADivisionSearch();
        }

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
            // クエリパラメータから管理番号を取得
            //if (IsPostBack != true)
            //{
            // クエリパラメータを取得する（ID取得）
            if (Request.QueryString.Get("target") != null)
            {
                QueryString = Request.QueryString.Get("target");
            }
            //}


            //// ユーザー情報リスト取得
            //var list = _QueryString.Replace("<->", "?").Split('?');

            //List<string> parm = new List<string>();

            //for ( int i = 0; i < list.Length;i++)
            //{
            //    var child = list[i].Replace("<>", "?").Split('?');
            //    if (child[0] != "")
            //        parm.Add(child[0]);
            //}
            //var dt = new DAUserSearch().getUserInfo(parm);

            // ユーザー情報設定（HiddenField）
        }



        /// <summary>
        /// 検索ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            if (_BlDivisionSearch == null)
            {
                _BlDivisionSearch = new BLDivisionSearch();
            }

            // 検索条件取得
            _BlDivisionSearch.SearchPrmDivision = TxtDivision.Text;
            _BlDivisionSearch.SearchPrmDepartment = TxtDepartment.Text;

            // 検索処理（フィールドセット）
            _BlDivisionSearch.getDivisionInfo();
            _SearchResultList = _BlDivisionSearch.SearchResult;
        }
        #endregion
        #endregion
    }
}