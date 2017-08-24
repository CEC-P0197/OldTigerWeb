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
    public class BLUserSearch
    {
        #region "フィールド"
        /// <summary>
        /// DBインスタンスフィールド
        /// </summary>
        private DAUserSearch _db;
        /// <summary>
        /// 検索条件_部略称フィールド
        /// </summary>
        private string _SearchPrmDepartment;
        /// <summary>
        /// 検索条件_課略称フィールド
        /// </summary>
        private string _SearchPrmDivision;
        /// <summary>
        /// 検索条件_ユーザー名フィールド
        /// </summary>
        private string _SearchPrmUserName;
        /// <summary>
        /// 検索条件_メールアドレスフィールド
        /// </summary>
        private string _SearchPrmMailAddress;
        /// <summary>
        /// 検索結果フィールド
        /// </summary>
        private DataTable _SearchResult;

        #endregion

        #region "コンストラクタ"
        public BLUserSearch()
        {
            _db = new DAUserSearch() ;
        }

        #endregion

        #region "プロパティ"
        /// <summary>
        /// 検索条件_部略称
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
        /// 検索条件_課略称
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
        /// 検索条件_ユーザー名
        /// </summary>
        public string SearchPrmUserName
        {
            get
            {
                if (_SearchPrmUserName != null)
                    return _SearchPrmUserName;

                return "";
            }
            set
            {
                if (value != null)
                    if (!object.ReferenceEquals(value, _SearchPrmUserName))
                        _SearchPrmUserName = value;
            }
        }
        /// <summary>
        /// 検索条件_メールアドレスプロパティ
        /// </summary>
        public string SearchPrmMailAddress
        {
            get
            {
                if (_SearchPrmUserName != null)
                    return _SearchPrmMailAddress;

                return "";
            }
            set
            {
                if (value != null)
                    if (!object.ReferenceEquals(value, _SearchPrmMailAddress))
                        _SearchPrmMailAddress = value;
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

                return _db.getUserInfo();
            }
        }

#endregion


        #region "メソッド"
        /// <summary>
        /// ユーザー検索処理（検索条件はフィールドで定義）
        /// </summary>
        /// <returns></returns>
        public bool getUserInfo()
        {
            // 検索条件チェック
            if (!checkSearchParameter())
                return false;

            // 検索結果を返却
            _SearchResult = _db.getUserInfo(this);

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
            if ((SearchPrmDepartment == "") && (SearchPrmDivision == "") &&
                (SearchPrmUserName == "") && (SearchPrmMailAddress == ""))
                // チェックNG
                return false;

            // チェックOK
            return true;
        }

        #endregion
    }
}