using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using OldTigerWeb.DataAccess;

namespace OldTigerWeb.BuisinessLogic
{
    public class MitakaSearchData
    {
        #region フィールド
        /// <summary>
        /// DAMitakaDataインスタンスフィールド（リポジトリクラス）
        /// </summary>
        private IDAMitakaData _DbMitakaData;
        /// <summary>
        /// ユーザーIDフィールド
        /// </summary>
        private string _Owner;
        /// <summary>
        /// タイトルフィールド
        /// </summary>
        private string _Title;
        /// <summary>
        /// 管理番号フィールド
        /// </summary>
        private string _ManageNo;
        /// <summary>
        /// 作成部署コードフィールド
        /// </summary>
        private string _CreateDepartmentCode;
        /// <summary>
        /// 回答対象部署コードフィールド
        /// </summary>
        private string _AnswerDepartmentCode;
        /// <summary>
        /// 回答期間（カラ）フィールド
        /// </summary>
        private DateTime _AnswerStartDateTime;
        /// <summary>
        /// 回答期間（マデ）フィールド
        /// </summary>
        private DateTime _AnswerEndDateTime;
        /// <summary>
        /// 状況フィールド
        /// </summary>
        private string _Status;
        /// <summary>
        /// 状況リストフィールド
        /// </summary>
        private Dictionary<string,string> _StatusList;
        /// <summary>
        /// 回答区分フィールド
        /// </summary>
        private string _AnswerPattern;
        /// <summary>
        /// 回答区分リストフィールド
        /// </summary>
        private Dictionary<string, string> _AnswerPatternList;
        /// <summary>
        /// 開発符号フィールド
        /// </summary>
        private string _DevelopmentCode;
        /// <summary>
        /// 機種フィールド
        /// </summary>
        private string _Model;
        /// <summary>
        /// BLK Noフィールド
        /// </summary>
        private string _BlockNo;
        /// <summary>
        /// タイトル品番フィールド
        /// </summary>
        private string _TitleDrawingNo;
        /// <summary>
        /// 設通番号フィールド
        /// </summary>
        private string _EcsNo;
        /// <summary>
        /// 検索結果リストフィールド
        /// </summary>
        private List<MitakaData> _SearchResultList;
        /// <summary>
        /// 埋め込みスクリプトフィールド
        /// </summary>
        private string _EmbeddedScript;
        #endregion

        #region プロパティ
        /// <summary>
        /// 所有者
        /// </summary>
        public string Owner
        {
            get
            {
                if (_Owner != null)
                    return _Owner;

                return "";
            }
        }
        /// <summary>
        /// タイトル
        /// </summary>
        public string Title
        {
            get
            {
                if (_Title != null)
                    return _Title;

                return "";
            }
            set
            {
                if (value != null && value != _Title)
                {
                    _Title = value.Trim();
                }
            }
        }
        /// <summary>
        /// 管理番号
        /// </summary>
        public string ManageNo
        {
            get
            {
                if (_ManageNo != null)
                    return _ManageNo;

                return "";
            }
            set
            {
                if (value != null && value != _ManageNo)
                {
                    _ManageNo = value.Trim();
                }
            }
        }
        /// <summary>
        /// 作成部署コード
        /// </summary>
        public string CreateDepartmentCode
        {
            get
            {
                if (_CreateDepartmentCode != null)
                    return _CreateDepartmentCode;

                return "";
            }
            set
            {
                if (value != null && value != _CreateDepartmentCode)
                {
                    _CreateDepartmentCode = value.Trim();
                }
            }
        }
        /// <summary>
        /// 回答対象部署コード
        /// </summary>
        public string AnswerDepartmentCode
        {
            get
            {
                if (_AnswerDepartmentCode != null)
                    return _AnswerDepartmentCode;

                return "";
            }
            set
            {
                if (value != null && value != _AnswerDepartmentCode)
                    _AnswerDepartmentCode = value.Trim();
            }
        }
        /// <summary>
        /// 回答期間（カラ）
        /// </summary>
        public DateTime AnswerStartDateTime
        {
            get
            {
                if (_AnswerStartDateTime != null)
                    return _AnswerStartDateTime;

                return DateTime.Parse(Def.SQL_DATETIME_MIN);
            }
        }
        /// <summary>
        /// 回答期間（カラ） 表示用
        /// </summary>
        public string AnswerStartDateTimeDisp
        {
            get
            {
                if (_AnswerStartDateTime != null)
                    if (_AnswerStartDateTime != DateTime.Parse(Def.SQL_DATETIME_MIN))
                        return _AnswerStartDateTime.ToString("yyyyMM");

                return "";
            }
            set
            {
                if (value != null)
                {
                    DateTime startDate = value.StringToDateTimeOfMonthsAndYear().BeginOfMonth();

                    if ((startDate != _AnswerStartDateTime) &&
                        (startDate != DateTime.Parse(Def.SQL_DATETIME_MIN)))
                        _AnswerStartDateTime = startDate;
                }
            }
        }
        /// <summary>
        /// 回答期間（マデ）
        /// </summary>
        public DateTime AnswerEndDateTime
        {
            get
            {
                if (_AnswerEndDateTime!= null)
                    return _AnswerEndDateTime;

                return DateTime.Parse(Def.SQL_DATETIME_MAX);
            }
        }
        /// <summary>
        /// 回答期間（マデ） 表示用
        /// </summary>
        public string AnswerEndDateTimeDisp
        {
            get
            {
                if (_AnswerEndDateTime != null)
                    if (_AnswerEndDateTime != DateTime.Parse(Def.SQL_DATETIME_MAX))
                        return _AnswerEndDateTime.ToString("yyyyMM");

                return "";
            }
            set
            {
                if (value != null)
                {
                    DateTime endDate = value.StringToDateTimeOfMonthsAndYear().EndOfMonth();

                    if ((endDate != _AnswerEndDateTime) &&
                        (endDate != DateTime.Parse(Def.SQL_DATETIME_MAX)))
                        _AnswerEndDateTime = endDate;
                }
            }
        }

        /// <summary>
        /// 状況
        /// </summary>
        public string Status
        {
            get
            {
                if (_Status != null)
                    return _Status;
                
                return "";
            }
            set
            {
                if (value != _Status)
                    switch (value)
                    {
                        case Def.MITAKA_STATUS_PREPARATION:
                        case Def.MITAKA_STATUS_ANSWER:
                        case Def.MITAKA_STATUS_CONFIRMED:
                        case Def.MITAKA_STATUS_CANCEL:
                            _Status = value;
                            break;
                        default:
                            _Status = "";
                            break;
                    }
            }
        }
        /// <summary>
        /// 状況リスト
        /// </summary>
        public Dictionary<string,string> StatusList
        {
            get
            {
                Dictionary<string, string> returnList = new Dictionary<string, string>();
                returnList.Add("", "");
                returnList.Add(Def.MITAKA_STATUS_PREPARATION, "回答準備");
                returnList.Add(Def.MITAKA_STATUS_ANSWER, "回答中");
                returnList.Add(Def.MITAKA_STATUS_CONFIRMED, "完了");
                returnList.Add(Def.MITAKA_STATUS_CANCEL, "取消");
                return returnList;
            }
        }

        /// <summary>
        /// 回答区分
        /// </summary>
        public string AnswerPattern
        {
            get
            {
                if (_AnswerPattern != null)
                    return _AnswerPattern;

                return "";
            }
            set
            {
                if (value != _AnswerPattern)
                    switch (value)
                    {
                        case "1":
                        case "2":
                            _AnswerPattern = value;
                            break;
                        default:
                            _AnswerPattern = "";
                            break;
                    }
            }
        }

        /// <summary>
        /// 回答区分リスト
        /// </summary>
        public Dictionary<string,string> AnswerPatternList
        {
            get
            {
                Dictionary<string, string> returnList = new Dictionary<string, string>();
                returnList.Add("", "");
                returnList.Add("1", "未回答有");
                returnList.Add("2", "回答済");
                return returnList;
            }
        }
        /// <summary>
        /// 開発符号
        /// </summary>
        public string DevelopmentCode
        {
            get
            {
                if (_DevelopmentCode != null)
                    return _DevelopmentCode;

                return "";
            }
            set
            {
                if (value != null && value != _DevelopmentCode)
                    _DevelopmentCode = value;
            }
        }
        /// <summary>
        /// 機種
        /// </summary>
        public string Model
        {
            get
            {
                if (_Model != null)
                    return _Model;

                return "";
            }
            set
            {
                if (value != null && value != _Model)
                    _Model = value;
            }
        }
        /// <summary>
        /// BLK No
        /// </summary>
        public string BlockNo
        {
            get
            {
                return _BlockNo;
            }
            set
            {
                if (value != null && value != _BlockNo)
                {
                    _BlockNo = value;
                }
            }
        }
        /// <summary>
        /// タイトル品番
        /// </summary>
        public string TitleDrawingNo
        {
            get
            {
                return _TitleDrawingNo;
            }
            set
            {
                if (value != null && value != _TitleDrawingNo)
                {
                    _TitleDrawingNo = value;
                }
            }
        }
        /// <summary>
        /// 設通番号
        /// </summary>
        public string EcsNo
        {
            get
            {
                return _EcsNo;
            }
            set
            {
                if (value != null && value != _EcsNo)
                {
                    _EcsNo = value;
                }
            }
        }
        /// <summary>
        /// 検索結果リスト
            /// </summary>
        public List<MitakaData> SearchResultList
        {
            get
            {
                return _SearchResultList;
            }
            set
            {
                if (value != null && value != _SearchResultList)
                {
                    _SearchResultList = value;
                }
            }
        }
        /// <summary>
        /// 埋め込みスクリプト
        /// </summary>
        public string EmbeddedScript
        {
            get
            {
                return _EmbeddedScript;
            }
            set
            {
                if (value != null && value != _EmbeddedScript)
                {
                    _EmbeddedScript = value;
                }
            }
        }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MitakaSearchData(string userId)
        {
            _Owner = userId;

            _DbMitakaData = new DAMitakaData(_Owner);

            // 過去トラ観たか情報検索初期化
            initMitakaSearchData();
        }

        public MitakaSearchData(IDAMitakaData dbMitakaData, string userId)
        {
            _Owner = userId;

            _DbMitakaData = dbMitakaData;

            // 過去トラ観たか情報検索初期化
            initMitakaSearchData();
        }

        #endregion

        #region メソッド
        /// <summary>
        /// 過去トラ観たか情報検索初期化
        /// </summary>
        public void initMitakaSearchData()
        {
            _Title = "";
            _ManageNo = "";
            _CreateDepartmentCode = "";
            _AnswerDepartmentCode = "";
            _AnswerStartDateTime = DateTime.Parse(Def.SQL_DATETIME_MIN);
            _AnswerEndDateTime = DateTime.Parse(Def.SQL_DATETIME_MAX);
            _Status = "";
            _AnswerPattern = "";
            _DevelopmentCode = "";
            _Model = "";
            _BlockNo = "";
            _TitleDrawingNo = "";
            _EcsNo = "";
            _SearchResultList = new List<MitakaData>();
            _EmbeddedScript = "";
        }
        /// <summary>
        /// 過去トラ観たか情報検索（検索条件指定）
        /// </summary>
        public void searchMitakaDataCondition()
        {
            // 管理番号取得（過去トラ観たか検索）
            DataTable manageNoList = _DbMitakaData.getManageNoFromMitakaSearchData(this);

            // 管理番号でループ
            for ( int i = 0; i < manageNoList.Rows.Count; i++)
            {
                // 過去トラ観たか情報インスタンス定義（引数に管理番号、ユーザーIDをセット）
                MitakaData mitakaData = 
                    new MitakaData((string)manageNoList.Rows[i]["MITAKA_NO"], _Owner);

                // 検索結果フィールドに過去トラ観たか情報インスタンスを追加
                SearchResultList.Add(mitakaData);
            }
        }
        /// <summary>
        /// 過去トラ観たか情報検索（所有）
        /// </summary>
        public void searchMitakaDataMine()
        {
            // 管理番号リスト取得（所有）
            DataTable manageNoList = _DbMitakaData.getManageNoFromRelationUser();

            // 管理番号でループ
            for (int i = 0; i < manageNoList.Rows.Count; i++)
            {
                // 過去トラ観たか情報インスタンス定義（引数に管理番号、ユーザーIDをセット）
                MitakaData mitakaData =
                    new MitakaData((string)manageNoList.Rows[i]["MITAKA_NO"], _Owner);

                // 検索結果フィールドに過去トラ観たか情報インスタンスを追加
                SearchResultList.Add(mitakaData);
            }
        }
        #endregion
    }
}