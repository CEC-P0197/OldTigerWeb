using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Linq;
using System.Web;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Web.UI;
using OldTigerWeb.DataAccess;

namespace OldTigerWeb.BuisinessLogic
{
    public class BLDivisionSearch
    {
        #region "フィールド"
        /// <summary>
        /// DBインスタンスフィールド
        /// </summary>
        private DADivisionSearch _db;
        /// <summary>
        /// 検索条件_課・主査フィールド
        /// </summary>
        private string _SearchPrmDivision;
        /// <summary>
        /// 検索条件_部フィールド
        /// </summary>
        private string _SearchPrmDepartment;
        /// <summary>
        /// 検索結果フィールド
        /// </summary>
        private DataTable _SearchResult;

        #endregion

        #region "コンストラクタ"
        public BLDivisionSearch()
        {
            _db = new DADivisionSearch() ;
        }

        #endregion

        #region "プロパティ"
        /// <summary>
        /// 検索条件_課・主査
        /// </summary>
        public string SearchPrmDivision
        {
            get
            {
                if (_SearchPrmDivision != null)
                    return _SearchPrmDivision;

                return "";
            }
            set
            {
                if (value != null)
                    if (!object.ReferenceEquals(value, _SearchPrmDivision))
                        _SearchPrmDivision = value;
            }
        }
        /// <summary>
        /// 検索条件_部
        /// </summary>
        public string SearchPrmDepartment
        {
            get
            {
                if (_SearchPrmDepartment != null)
                    return _SearchPrmDepartment;

                return "";
            }
            set
            {
                if (value != null)
                    if (!object.ReferenceEquals(value, _SearchPrmDepartment))
                        _SearchPrmDepartment = value;
            }
        }
        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult
        {
            get
            {
                if (_SearchResult != null)
                    return _SearchResult;

                return _db.getDivisionInfo();
            }
        }

#endregion


        #region "メソッド"
        /// <summary>
        /// 部署検索処理（検索条件はフィールドで定義）
        /// </summary>
        /// <returns></returns>
        public bool getDivisionInfo()
        {
            // 検索条件チェック
            if (!checkSearchParameter())
                return false;

            // 検索結果を返却
            _SearchResult = _db.getDivisionInfo(this);

            // 検索結果がNULLの場合
            if (_SearchResult == null)
                return false;

            return true;
        }

        /// <summary>
        /// 検索条件チェック
        /// </summary>
        /// <returns></returns>
        public bool checkSearchParameter()
        {
            // 未入力チェック（検索条件が1件も設定されていない場合
            if ((SearchPrmDivision == "") && (SearchPrmDepartment == ""))
                // チェックNG
                return false;

            // チェックOK
            return true;
        }

        #endregion
    }
}