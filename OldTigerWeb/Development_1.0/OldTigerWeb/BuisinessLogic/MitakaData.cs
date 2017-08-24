using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using OldTigerWeb.DataAccess;
using OldTigerWeb.DataAccess.Common;

namespace OldTigerWeb.BuisinessLogic
{
    //public class MitakaData: CommonPageLogic
    public class MitakaData: CommonPageLogic
    {
        #region フィールド   
        /// <summary>
        /// DAMitakaDataインスタンスフィールド
        /// </summary>
        private IDAMitakaData _DbMitakaData;

        /// <summary>
        /// 管理番号フィールド
        /// </summary>
        private string _ManageNo;
        /// <summary>
        /// 所有者フィールド
        /// </summary>
        private string _Owner;
        /// <summary>
        /// 過去トラ観たかヘッダーフィールド
        /// </summary>
        private DataTable _MitakaHeaderData;
        /// <summary>
        /// 検索条件フィールド
        /// </summary>
        private DataTable _SearchParameterData;
        /// <summary>
        /// 関連ユーザー情報フィールド
        /// </summary>
        private DataTable _ReLationUserData;
        /// <summary>
        /// 展開対象フィールド
        /// </summary>
        private DataTable _DeploymentTroubleData;
        /// <summary>
        /// タイトル品番情報フィールド
        /// </summary>
        private DataTable _TitleDrawingData;
        /// <summary>
        /// 機種フィールド
        /// </summary>
        private DataTable _ModelData;
        /// <summary>
        /// BLK情報フィールド
        /// </summary>
        private DataTable _BlockData;
        /// <summary>
        /// 開発符号情報フィールド
        /// </summary>
        private DataTable _DevelopmentCodeData;
        /// <summary>
        /// 設通情報フィールド
        /// </summary>
        private DataTable _EcsData;
        /// <summary>
        /// 過去トラ観たか回答情報インスタンスフィールド
        /// </summary>
        private MitakaAnswerData _MitakaAnswerData;
        /// <summary>
        /// データ処理結果フィールド
        /// </summary>
        private List<string> _DataProcessResult;
        /// <summary>
        /// 編集フラグフィールド
        /// </summary>
        private bool _EditFlg;
        /// <summary>
        /// 過去トラ観たか権限フィールド
        /// </summary>
        private string _MitakaAuthority;
        /// <summary>
        /// 埋め込みスクリプトフィールド
        /// </summary>
        private string _embeddedScript;
        /// <summary>
        /// テスト実行フラグ
        /// </summary>
        private bool _test;

        #endregion
        #region プロパティ
        /// <summary>
        /// 管理番号
        /// </summary>
        public string ManageNo
        {
            get
            {
                if (_ManageNo != null)
                        return
                            _ManageNo;

                // フィールドが存在しない場合は、""を返却
                return "";
            }
        }
        /// <summary>
        /// 所有者
        /// </summary>
        public string Owner
        {
            get
            {
                if (_Owner != null)
                        return
                            _Owner;

                // フィールドが存在しない場合は、""を返却
                return "";
            }
        }
        /// <summary>
        /// タイトル
        /// </summary>
        public new string Title
        {
            get
            {
                if (_MitakaHeaderData != null)
                    if (_MitakaHeaderData.Rows.Count != 0)
                        if (!_MitakaHeaderData.AsEnumerable()
                           .FirstOrDefault().IsNull("TITLE"))
                            return
                               (string)_MitakaHeaderData.AsEnumerable()
                           .FirstOrDefault()["TITLE"];

                // フィールドが存在しない場合は、""を返却
                return "";
            }
            set
            {
                if (value != null)
                {
                    // フィールドが0件の場合、行追加
                    if (_MitakaHeaderData.Rows.Count == 0)
                        _MitakaHeaderData.Rows.Add(_MitakaHeaderData.NewRow());
                    else if (value == _MitakaHeaderData.AsEnumerable().FirstOrDefault()["TITLE"].ToString())
                        return;

                    // 項目セット
                    _MitakaHeaderData.AsEnumerable().FirstOrDefault()["TITLE"] = value;
                    _MitakaHeaderData.AsEnumerable().FirstOrDefault()["EDIT_FLG"] = "1";
                    _EditFlg = true;
                }
            }
        }
        /// <summary>
        /// 目的
        /// </summary>
        public string Purpose
        {
            get
            {
                if (_MitakaHeaderData != null)
                    if (_MitakaHeaderData.Rows.Count != 0)
                        if (!_MitakaHeaderData.AsEnumerable()
                           .FirstOrDefault().IsNull("PURPOSE"))
                            return
                               (string)_MitakaHeaderData.AsEnumerable()
                           .FirstOrDefault()["PURPOSE"];

                // フィールドが存在しない場合は、""を返却
                return "";
            }
            set
            {
                if (value != null) 
                {

                    // フィールドが0件の場合、行追加
                    if (_MitakaHeaderData.Rows.Count == 0)
                        _MitakaHeaderData.Rows.Add(_MitakaHeaderData.NewRow());
                    else if (value == _MitakaHeaderData.AsEnumerable().FirstOrDefault()["PURPOSE"].ToString())
                        return;

                    // 項目セット
                    _MitakaHeaderData.AsEnumerable().FirstOrDefault()["PURPOSE"] = value;
                    _MitakaHeaderData.AsEnumerable().FirstOrDefault()["EDIT_FLG"] = "1";
                    _EditFlg = true;
                }
            }
        }
        /// <summary>
        /// コメント
        /// </summary>
        public string Comment
        {
            get
            {
                if (_MitakaHeaderData != null)
                    if (_MitakaHeaderData.Rows.Count != 0)
                        if (!_MitakaHeaderData.AsEnumerable()
                           .FirstOrDefault().IsNull("COMMENT"))
                            return
                               (string)_MitakaHeaderData.AsEnumerable()
                           .FirstOrDefault()["COMMENT"];

                // フィールドが存在しない場合は、""を返却
                return "";
            }
            set
            {
                if (value != null )
                {
                    // フィールドが0件の場合、行追加
                    if (_MitakaHeaderData.Rows.Count == 0)
                        _MitakaHeaderData.Rows.Add(_MitakaHeaderData.NewRow());
                    else if (value == _MitakaHeaderData.AsEnumerable().FirstOrDefault()["COMMENT"].ToString())
                        return;

                    // 項目セット
                    _MitakaHeaderData.AsEnumerable().FirstOrDefault()["COMMENT"] = value;
                    _MitakaHeaderData.AsEnumerable().FirstOrDefault()["EDIT_FLG"] = "1";
                    _EditFlg = true;
                }
            }
        }
        /// <summary>
        /// 回答開始日時
        /// </summary>
        public string StartDateTime
        {
            get
            {
                DateTime date;

                if (_MitakaHeaderData != null)
                    if (_MitakaHeaderData.Rows.Count != 0)
                        if (!_MitakaHeaderData.AsEnumerable()
                           .FirstOrDefault().IsNull("START_YMD"))
                            if (DateTime.TryParse(_MitakaHeaderData.AsEnumerable().FirstOrDefault()["START_YMD"].ToString(), out date))
                            return
                               date.ToString("yyyy/MM/dd");

                // フィールドが存在しない場合は、""を返却
                return "";
            }
            set
            {
                if (value != null)
                {
                    DateTime dt;

                    // 取得値がフォーマット変換(string(yyyy / MM / dd)→datetime)できた場合（TryParse使用）
                    if (DateTime.TryParseExact(value.Replace("/", ""), "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out dt))
                    //if (DateTime.TryParse(value,"", out parm))
                    {
                        // フィールドが0件の場合、行追加
                        if (_MitakaHeaderData.Rows.Count == 0)
                            _MitakaHeaderData.Rows.Add(_MitakaHeaderData.NewRow());
                        else
                            if (!_MitakaHeaderData.AsEnumerable().FirstOrDefault().IsNull("START_YMD") )
                            if (dt == (DateTime)_MitakaHeaderData.AsEnumerable()
                                .FirstOrDefault()["START_YMD"])
                                return;

                        // 項目セット
                        _MitakaHeaderData.AsEnumerable().FirstOrDefault()["START_YMD"] = dt;
                        _MitakaHeaderData.AsEnumerable().FirstOrDefault()["EDIT_FLG"] = "1";
                        _EditFlg = true;
                    }
                }
            }
        }
        /// <summary>
        /// 回答終了日時
        /// </summary>
        public string EndDateTime
        {
            get
            {
                DateTime date;

                if (_MitakaHeaderData != null)
                    if (_MitakaHeaderData.Rows.Count != 0)
                        if (!_MitakaHeaderData.AsEnumerable()
                           .FirstOrDefault().IsNull("END_YMD"))
                            if (DateTime.TryParse(_MitakaHeaderData.AsEnumerable().FirstOrDefault()["END_YMD"].ToString(), out date))
                                return
                                   date.ToString("yyyy/MM/dd");

                // フィールドが存在しない場合は、""を返却
                return "";
            }
            set
            {
                if (value != null)
                {
                    DateTime dt;

                    // 取得値がフォーマット変換(string(yyyy / MM / dd)→datetime)できた場合（TryParse使用）
                    if (DateTime.TryParseExact(value.Replace("/", ""), "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out dt))
                    //if (DateTime.TryParse(value,"", out parm))
                    {
                        // フィールドが0件の場合、行追加
                        if (_MitakaHeaderData.Rows.Count == 0)
                            _MitakaHeaderData.Rows.Add(_MitakaHeaderData.NewRow());
                        else
                            if (!_MitakaHeaderData.AsEnumerable().FirstOrDefault().IsNull("END_YMD"))
                            if (dt == (DateTime)_MitakaHeaderData.AsEnumerable()
                                .FirstOrDefault()["END_YMD"])
                                return;


                        // 項目セット
                        _MitakaHeaderData.AsEnumerable().FirstOrDefault()["END_YMD"] = dt;
                        _MitakaHeaderData.AsEnumerable().FirstOrDefault()["EDIT_FLG"] = "1";
                        _EditFlg = true;
                    }
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
                if (_MitakaHeaderData != null)
                    if (_MitakaHeaderData.Rows.Count != 0)
                        if (!_MitakaHeaderData.AsEnumerable()
                           .FirstOrDefault().IsNull("STATUS"))
                            return
                               (string)_MitakaHeaderData.AsEnumerable()
                           .FirstOrDefault()["STATUS"];

                // フィールドが存在しない場合は、""を返却
                return "";
            }
            //set 
            //{
            //    if (value != null) 
            //    {
            //        string status = "";

            //        switch (value)
            //        {
            //            case "10": // 回答準備
            //            case "20": // 回答中
            //            case "30": // 完了
            //            case "99": // 取消
            //                status = value;
            //                break;
            //            default:
            //                return;
            //        }

            //        // フィールドが0件の場合、行追加
            //        if (_MitakaHeaderData.Rows.Count == 0)
            //            _MitakaHeaderData.Rows.Add(_MitakaHeaderData.NewRow());
            //        else if (value == _MitakaHeaderData.AsEnumerable().FirstOrDefault()["STATUS"].ToString())
            //            return;

            //        // 項目セット
            //        _MitakaHeaderData.AsEnumerable().FirstOrDefault()["STATUS"] = status;
            //        _MitakaHeaderData.AsEnumerable().FirstOrDefault()["EDIT_FLG"] = "1";
            //        _EditFlg = true;

            //    }
            //}
        }
        /// <summary>
        /// 状況（表示用）
        /// </summary>
        public string StatusDisp
        {
            get
            {
                if (_MitakaHeaderData != null)
                    if (_MitakaHeaderData.Rows.Count != 0)
                        if (!_MitakaHeaderData.AsEnumerable()
                           .FirstOrDefault().IsNull("STATUS"))

                            switch
                               ((string)_MitakaHeaderData.AsEnumerable()
                           .FirstOrDefault()["STATUS"])
                            {
                                case "10": // 回答準備
                                    return "回答準備";
                                case "20": // 回答中
                                    return "回答中";
                                case "30": // 完了
                                    return "完了";
                                case "99": // 取消
                                    return "取消";
                                default: // 上記以外
                                    return "";
                            }
                return "";
            }
        }
        /// <summary>
        /// 状況コメント
        /// </summary>
        public string StatusComment
        {
            get
            {
                if (_MitakaHeaderData != null)
                    if (_MitakaHeaderData.Rows.Count != 0)
                        if (!_MitakaHeaderData.AsEnumerable()
                           .FirstOrDefault().IsNull("STATUS_COMMENT"))
                            return
                              (string)_MitakaHeaderData.AsEnumerable()
                          .FirstOrDefault()["STATUS_COMMENT"];

                // フィールドが存在しない場合は、""を返却
                return "";
            }
            set
            {
                if (value != null)
                {
                    // フィールドが0件の場合、行追加
                    if (_MitakaHeaderData.Rows.Count == 0)
                        _MitakaHeaderData.Rows.Add(_MitakaHeaderData.NewRow());
                    else if (value == _MitakaHeaderData.AsEnumerable().FirstOrDefault()["STATUS_COMMENT"].ToString())
                        return;

                    _MitakaHeaderData.AsEnumerable().FirstOrDefault()["STATUS_COMMENT"] = value;
                    _MitakaHeaderData.AsEnumerable().FirstOrDefault()["EDIT_FLG"] = "1";
                    _EditFlg = true;
                }
            }
        }
        /// <summary>
        /// 作成者
        /// </summary>
        public string InsertUser
        {
            get
            {
                if (_MitakaHeaderData != null)
                    if (_MitakaHeaderData.Rows.Count != 0)
                        if (!_MitakaHeaderData.AsEnumerable()
                           .FirstOrDefault().IsNull("INSERT_USER"))
                            return
                               (string)_MitakaHeaderData.AsEnumerable()
                           .FirstOrDefault()["INSERT_USER"];

                // フィールドが存在しない場合は、""を返却
                return "";
            }
        }
        /// <summary>
        /// 作成者名
        /// </summary>
        public string InsertUserName
        {
            get
            {
                if (InsertUser != "")
                    return (string)new SqlCommon().SelectUser(InsertUser)
                        .AsEnumerable().FirstOrDefault()["USER_NAME"];

                // 存在しない場合は、""を返却
                return "";
            }
        }
        /// <summary>
        /// 更新者
        /// </summary>
        public string UpdateUser
        {
            get
            {
                if (_MitakaHeaderData != null)
                    if (_MitakaHeaderData.Rows.Count != 0)
                        return
                           (string)_MitakaHeaderData.AsEnumerable()
                           .FirstOrDefault()["UPDATE_USER"];

                // フィールドが存在しない場合は、""を返却
                return "";
            }
        }
        /// <summary>
        /// 更新者名
        /// </summary>
        public string UpdateUserName
        {
            get
            {
                if (UpdateUser != "")
                    return (string)new SqlCommon().SelectUser(UpdateUser)
                    .AsEnumerable().FirstOrDefault()["USER_NAME"];

                // 存在しない場合は、""を返却
                return "";
            }
        }
        /// <summary>
        /// 作成日時
        /// </summary>
        public string InsertDate
        {
            get
            {
                DateTime date;

                if (_MitakaHeaderData != null)
                    if (_MitakaHeaderData.Rows.Count != 0)
                        if (!_MitakaHeaderData.AsEnumerable()
                           .FirstOrDefault().IsNull("INSERT_YMD"))
                            if (DateTime.TryParse(_MitakaHeaderData.AsEnumerable().FirstOrDefault()["INSERT_YMD"].ToString(), out date))
                                return
                                   date.ToString("yyyy/MM/dd");

                // フィールドが存在しない場合は、""を返却
                return "";
            }
        }
        /// <summary>
        /// 更新日時
        /// </summary>
        public string UpdateDate
        {
            get
            {
                DateTime date;

                if (_MitakaHeaderData != null)
                    if (_MitakaHeaderData.Rows.Count != 0)
                        if (!_MitakaHeaderData.AsEnumerable()
                           .FirstOrDefault().IsNull("UPDATE_YMD"))
                            if (DateTime.TryParse(_MitakaHeaderData.AsEnumerable().FirstOrDefault()["UPDATE_YMD"].ToString(), out date))
                                return
                                      date.ToString("yyyy/MM/dd");

                // フィールドが存在しない場合は、""を返却
                return "";
            }
        }
        /// <summary>
        /// 過去トラ検索条件
        /// </summary>
        public DataTable SearchParameterList
        {
            get
            {
                return _SearchParameterData.Copy();
            }
        }
        /// <summary>
        /// 関連ユーザー情報
        /// </summary>
        public DataTable ReLationUserList
        {
            get
            {
                return _ReLationUserData.Copy();
            }
        }
        /// <summary>
        /// 作成者（主）
        /// </summary>
        public DataRow CreateMainUser
        {
            get
            {
                var filter = _ReLationUserData.AsEnumerable()
                            .Where(row => row["RELATION_TYPE"].ToString() == Def.MITAKA_RELATION_TYPE_CREATER_MAIN)
                            .FirstOrDefault();

                if (filter != null)
                    return filter;

                // 新規行返却
                return _ReLationUserData.NewRow();
            }
            set
            {
                try
                {
                    if (value != null)
                    {

                        var filter = _ReLationUserData.AsEnumerable()
                            .Where(row => row["RELATION_TYPE"].ToString() == Def.MITAKA_RELATION_TYPE_CREATER_MAIN);

                        if (filter.Count() > 0)
                        {
                            if (filter.FirstOrDefault()["USER_ID"] != value["USER_ID"])
                            {
                                _ReLationUserData.AsEnumerable()
                                .Where(row => row["RELATION_TYPE"].ToString() == Def.MITAKA_RELATION_TYPE_CREATER_MAIN)
                                .FirstOrDefault().Delete();
                            }
                            else
                                return;
                        }

                        var dr = _ReLationUserData.NewRow();
                        dr["MITAKA_NO"] = _ManageNo;
                        dr["RELATION_TYPE"] = Def.MITAKA_RELATION_TYPE_CREATER_MAIN;
                        dr["USER_ID"] = value["USER_ID"];
                        dr["EDIT_FLG"] = "1";
                        _ReLationUserData.Rows.Add(dr);
                        _EditFlg = true;

                        var parm = new SqlCommon().SelectUser((string)value["USER_ID"])
                            .AsEnumerable().FirstOrDefault();


                        // フィールドが0件の場合、行追加
                        if (_MitakaHeaderData.Rows.Count == 0)
                            _MitakaHeaderData.Rows.Add(_MitakaHeaderData.NewRow());

                        // ユーザー情報が取得できなかった場合
                        if (parm == null)
                        {
                            _MitakaHeaderData.AsEnumerable().FirstOrDefault()["MANAGE_DIVISION_CODE1"] = "";
                        }

                        else if (parm["KA_CODE"] != _MitakaHeaderData.AsEnumerable().FirstOrDefault()["MANAGE_DIVISION_CODE1"])
                        {
                            _MitakaHeaderData.AsEnumerable().FirstOrDefault()["MANAGE_DIVISION_CODE1"] = parm["KA_CODE"];
                        }
                        _MitakaHeaderData.AsEnumerable().FirstOrDefault()["EDIT_FLG"] = "1";
                        _EditFlg = true;
                    }
                    else
                    {
                        _ReLationUserData.AsEnumerable()
                            .Where(row => row["RELATION_TYPE"].ToString() == Def.MITAKA_RELATION_TYPE_CREATER_MAIN)
                            .FirstOrDefault().Delete();

                        // フィールドが0件の場合、行追加
                        if (_MitakaHeaderData.Rows.Count != 0)
                            _MitakaHeaderData.Rows.Add(_MitakaHeaderData.NewRow());

                        _MitakaHeaderData.AsEnumerable().FirstOrDefault()["MANAGE_DIVISION_CODE1"] = "";
                        _MitakaHeaderData.AsEnumerable().FirstOrDefault()["EDIT_FLG"] = "1";
                        _EditFlg = true;
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// 作成者（副）
        /// </summary>
        public DataRow CreateSubUser
        {
            get
            {
                var filter = _ReLationUserData.Copy().AsEnumerable()
                            .Where(row => row["RELATION_TYPE"].ToString() == Def.MITAKA_RELATION_TYPE_CREATER_SUB)
                            .FirstOrDefault();

                if (filter != null)
                    return filter;

                // 新規行返却
                return _ReLationUserData.NewRow();
            }
            set
            {
                try
                {
                    if (value != null)
                    {

                        var filter = _ReLationUserData.AsEnumerable()
                            .Where(row => row["RELATION_TYPE"].ToString() == Def.MITAKA_RELATION_TYPE_CREATER_SUB);

                        if (filter.Count() > 0)
                        {
                            if (filter.FirstOrDefault()["USER_ID"] != value["USER_ID"])
                            {
                                _ReLationUserData.AsEnumerable()
                                .Where(row => row["RELATION_TYPE"].ToString() == Def.MITAKA_RELATION_TYPE_CREATER_SUB)
                                .FirstOrDefault().Delete();
                            }
                            else
                                return;
                        }

                        var dr = _ReLationUserData.NewRow();
                        dr["MITAKA_NO"] = _ManageNo;
                        dr["RELATION_TYPE"] = Def.MITAKA_RELATION_TYPE_CREATER_SUB;
                        dr["USER_ID"] = value["USER_ID"];
                        dr["EDIT_FLG"] = "1";
                        _ReLationUserData.Rows.Add(dr);
                        _EditFlg = true;

                        var parm = new SqlCommon().SelectUser((string)value["USER_ID"])
                            .AsEnumerable().FirstOrDefault();

                        // フィールドが0件の場合、行追加
                        if (_MitakaHeaderData.Rows.Count == 0)
                            _MitakaHeaderData.Rows.Add(_MitakaHeaderData.NewRow());

                        // ユーザー情報が取得できなかった場合
                        if (parm == null)
                        {
                            _MitakaHeaderData.AsEnumerable().FirstOrDefault()["MANAGE_DIVISION_CODE2"] = "";
                        }

                        else if (parm["KA_CODE"] != _MitakaHeaderData.AsEnumerable().FirstOrDefault()["MANAGE_DIVISION_CODE1"])
                        {
                            _MitakaHeaderData.AsEnumerable().FirstOrDefault()["MANAGE_DIVISION_CODE2"] = parm["KA_CODE"];
                        }
                        _MitakaHeaderData.AsEnumerable().FirstOrDefault()["EDIT_FLG"] = "1";
                        _EditFlg = true;
                    }
                    else
                    {
                        _ReLationUserData.AsEnumerable()
                            .Where(row => row["RELATION_TYPE"].ToString() == Def.MITAKA_RELATION_TYPE_CREATER_SUB)
                            .FirstOrDefault().Delete();

                        // フィールドが0件の場合、行追加
                        if (_MitakaHeaderData.Rows.Count != 0)
                            _MitakaHeaderData.Rows.Add(_MitakaHeaderData.NewRow());

                        _MitakaHeaderData.AsEnumerable().FirstOrDefault()["MANAGE_DIVISION_CODE2"] = "";
                        _MitakaHeaderData.AsEnumerable().FirstOrDefault()["EDIT_FLG"] = "1";
                        _EditFlg = true;
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// 点検者
        /// </summary>
        public DataRow InspectionUser
        {
            get
            {
                var filter = _ReLationUserData.Copy().AsEnumerable()
                            .Where(row => row["RELATION_TYPE"].ToString() == Def.MITAKA_RELATION_TYPE_INSPECTOR)
                            .FirstOrDefault();

                if (filter != null)
                    return filter;

                // 新規行返却
                return _ReLationUserData.NewRow();
            }
            set
            {
                try
                {
                    if (value != null)
                    {
                        var filter = _ReLationUserData.AsEnumerable()
                            .Where(row => row["RELATION_TYPE"].ToString() == Def.MITAKA_RELATION_TYPE_INSPECTOR);

                        if (filter.Count() > 0)
                        {
                            if (filter.FirstOrDefault()["USER_ID"] != value["USER_ID"])
                            {
                                _ReLationUserData.AsEnumerable()
                                .Where(row => row["RELATION_TYPE"].ToString() == Def.MITAKA_RELATION_TYPE_INSPECTOR)
                                .FirstOrDefault().Delete();
                            }
                            else
                                return;
                        }

                        var dr = _ReLationUserData.NewRow();
                        dr["MITAKA_NO"] = _ManageNo;
                        dr["RELATION_TYPE"] = Def.MITAKA_RELATION_TYPE_INSPECTOR;
                        dr["USER_ID"] = value["USER_ID"];
                        dr["EDIT_FLG"] = "1";
                        _ReLationUserData.Rows.Add(dr);
                        _EditFlg = true;
                    }
                    else
                    {
                        _ReLationUserData.AsEnumerable()
                            .Where(row => row["RELATION_TYPE"].ToString() == Def.MITAKA_RELATION_TYPE_INSPECTOR)
                            .FirstOrDefault().Delete();

                        _EditFlg = true;
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// 管理部署（主）コード
        /// </summary>
        public string ManageMainDivisionCode
        {
            get
            {
                if (_MitakaHeaderData != null)
                    if (_MitakaHeaderData.Rows.Count != 0)
                        return
                           (string)_MitakaHeaderData.AsEnumerable()
                           .FirstOrDefault()["MANAGE_DIVISION_CODE1"];

                // フィールドが存在しない場合は、""を返却
                return "";
            }
        }
        /// <summary>
        /// 管理部署（副）コード
        /// </summary>
        public string ManageSubDivisionCode
        {
            get
            {
                if (_MitakaHeaderData != null)
                    if (_MitakaHeaderData.Rows.Count != 0)
                        return
                            (string)_MitakaHeaderData.AsEnumerable()
                            .FirstOrDefault()["MANAGE_DIVISION_CODE2"];

                // フィールドが存在しない場合は、""を返却
                return "";
            }
        }
        /// <summary>
        /// 回答依頼先情報リスト
        /// </summary>
        public DataTable ConfirmationRequestList
        {
            get
            {
                var filter = _ReLationUserData.Copy().AsEnumerable()
                            .Where(row => row["RELATION_TYPE"].ToString() == Def.MITAKA_RELATION_TYPE_REQUEST);

                if (filter != null)
                    if (filter.Count() != 0)
                        return filter.CopyToDataTable();

                // 構造体のみ返却
                return _ReLationUserData.Clone();

            }
            set
            {
                if (value != null)
                {
                    var filterOfRowField = _ReLationUserData.AsEnumerable()
                             .Where(row => row["RELATION_TYPE"].ToString()
                             == Def.MITAKA_RELATION_TYPE_REQUEST);

                    DataTable filterOfField;

                    if (filterOfRowField.Count() > 0)
                        filterOfField = filterOfRowField.CopyToDataTable();
                    else
                        filterOfField = _ReLationUserData.Clone();

                    for (int i = 0; i < filterOfField.Rows.Count; i++)
                    {
                        if (value.AsEnumerable().All(row => row["USER_ID"]
                             != filterOfField.Rows[i]["USER_ID"]))
                        {
                            _ReLationUserData.AsEnumerable()
                                .Where(row => row["RELATION_TYPE"].ToString() == "30" &&
                                row["USER_ID"] == filterOfField.Rows[i]["USER_ID"])
                                .FirstOrDefault().Delete();
                            _EditFlg = true;
                        }
                    }

                    for (int i = 0; i < value.Rows.Count; i++)
                    {
                        if (filterOfField.AsEnumerable().All(row => row["USER_ID"]
                             != value.Rows[i]["USER_ID"]))
                        {
                            var dr = _ReLationUserData.NewRow();
                            dr["MITAKA_NO"] = _ManageNo;
                            dr["RELATION_TYPE"] = Def.MITAKA_RELATION_TYPE_REQUEST;
                            dr["USER_ID"] = value.Rows[i]["USER_ID"];
                            dr["EDIT_FLG"] = "1";
                            _ReLationUserData.Rows.Add(dr);
                        }
                    }
                    if (!(_ReLationUserData.AsEnumerable().All(row => row["EDIT_FLG"].ToString() != "1")))
                    {
                        _EditFlg = true;
                    }
                }

            }
        }
        /// <summary>
        /// 展開対象情報
        /// </summary>
        public DataTable DeploymentTroubleList
        {
            get
            {
                return _DeploymentTroubleData.Copy();
            }
            set
            {
                if (value != null)
                {
                    for (int i = 0; i < value.Rows.Count; i++)
                    {
                        var filter = _DeploymentTroubleData.AsEnumerable()
                            .Where(row => row["SYSTEM_NO"] == value.Rows[i]["SYSTEM_NO"]);

                        if (filter.Count() > 0)
                        {
                            if (filter.FirstOrDefault()["TARGET_FLG"] != value.Rows[i]["TARGET_FLG"])
                            {
                                filter.FirstOrDefault()["TARGET_FLG"] = value.Rows[i]["TARGET_FLG"];
                                filter.FirstOrDefault()["EDIT_FLG"] = "1";
                            }
                        }
                        else
                        {
                            var dr = _DeploymentTroubleData.NewRow();
                            dr["MITAKA_NO"] = _ManageNo;
                            dr["SYSTEM_NO"] = value.Rows[i]["SYSTEM_NO"];
                            dr["TARGET_FLG"] = value.Rows[i]["TARGET_FLG"];
                            dr["EDIT_FLG"] = "1";
                            _DeploymentTroubleData.Rows.Add(dr);
                            // 過去トラ観たか回答展開
                        }
                    }
                    if (!(_DeploymentTroubleData.AsEnumerable().All(row => row["EDIT_FLG"].ToString() != "1")))
                    {
                        _EditFlg = true;
                    }

                }
            }
        }
        /// <summary>
        /// 展開対象最新リスト（最新）
        /// </summary>
        public DataTable DeploymentTroubleLatestList
        {
            get
            {
                var filter = _DeploymentTroubleData.Copy().AsEnumerable();

                var result = filter.Where(row => row["INSERT_YMD"].ToString() ==
                filter.Max(m => m["INSERT_YMD"].ToString()));

                if (result != null)
                if (result.Count()!=0  )
                        return result.CopyToDataTable();

                return _DeploymentTroubleData.Clone();
            }
        }
        /// <summary>
        /// タイトル品番情報
        /// </summary>
        public DataTable TitleDrawingList
        {
            get
            {
                return _TitleDrawingData.Copy();
            }
            set
            {
                if (value != null)
                {
                    for (int i = 0; i < _TitleDrawingData.Rows.Count; i++)
                    {
                        if (value.AsEnumerable().All(row => row["TITLE_DRAWING_NO"]
                             != _TitleDrawingData.Rows[i]["TITLE_DRAWING_NO"]))
                        {
                            _TitleDrawingData.AsEnumerable().Where(row =>
                                row["TITLE_DRAWING_NO"] == _TitleDrawingData.Rows[i]["TITLE_DRAWING_NO"])
                                .FirstOrDefault().Delete();
                            _EditFlg = true;
                        }
                    }

                    for (int i = 0; i < value.Rows.Count; i++)
                    {
                        if (_TitleDrawingData.AsEnumerable().All(row => row["TITLE_DRAWING_NO"]
                             != value.Rows[i]["TITLE_DRAWING_NO"]))
                        {
                            var dr = _TitleDrawingData.NewRow();
                            dr["MITAKA_NO"] = _ManageNo;
                            dr["TITLE_DRAWING_NO"] = value.Rows[i]["TITLE_DRAWING_NO"];
                            dr["EDIT_FLG"] = "1";
                            _TitleDrawingData.Rows.Add(dr);
                        }
                    }
                    if (!(_TitleDrawingData.AsEnumerable().All(row => row["EDIT_FLG"].ToString() != "1")))
                    {
                        _EditFlg = true;
                    }
                }
            }
        }
        /// <summary>
        /// 機種情報リスト
        /// </summary>
        public DataTable ModelList
        {
            get
            {
                return _ModelData.Copy();
            }
            set
            {
                if (value != null)
                {
                    for (int i = 0; i < _ModelData.Rows.Count; i++)
                    {
                        if (value.AsEnumerable().All(row => row["MODEL"]
                             != _ModelData.Rows[i]["MODEL"]))
                        {
                            _ModelData.AsEnumerable().Where(row =>
                            row["MODEL"] == _ModelData.Rows[i]["MODEL"])
                            .FirstOrDefault().Delete();
                            _EditFlg = true;
                        }
                    }

                    for (int i = 0; i < value.Rows.Count; i++)
                    {
                        if (_ModelData.AsEnumerable().All(row => row["MODEL"]
                             != value.Rows[i]["MODEL"]))
                        {
                            var dr = _ModelData.NewRow();
                            dr["MITAKA_NO"] = _ManageNo;
                            dr["MODEL"] = value.Rows[i]["MODEL"];
                            dr["EDIT_FLG"] = "1";
                            _ModelData.Rows.Add(dr);
                        }
                    }
                    if (!(_ModelData.AsEnumerable().All(row => row["EDIT_FLG"].ToString() != "1")))
                    {
                        _EditFlg = true;
                    }
                }
            }
        }
        /// <summary>
        /// BLK情報リスト
        /// </summary>
        public DataTable BlockList
        {
            get
            {
                return _BlockData.Copy();
            }
            set
            {
                if (value != null)
                {
                    for (int i = 0; i < _BlockData.Rows.Count; i++)
                    {
                        if (value.AsEnumerable().All(row => row["BLK_NO"]
                             != _BlockData.Rows[i]["BLK_NO"]))
                        {
                            _ModelData.AsEnumerable().Where(row =>
                            row["BLK_NO"] == _BlockData.Rows[i]["BLK_NO"])
                            .FirstOrDefault().Delete();
                            _EditFlg = true;
                        }
                    }

                    for (int i = 0; i < value.Rows.Count; i++)
                    {
                        if (_BlockData.AsEnumerable().All(row => row["BLK_NO"]
                             != value.Rows[i]["BLK_NO"]))
                        {
                            var dr = _BlockData.NewRow();
                            dr["MITAKA_NO"] = _ManageNo;
                            dr["BLK_NO"] = value.Rows[i]["BLK_NO"];
                            dr["EDIT_FLG"] = "1";
                            _BlockData.Rows.Add(dr);
                        }
                    }
                    if (!(_BlockData.AsEnumerable().All(row => row["EDIT_FLG"].ToString() != "1")))
                    {
                        _EditFlg = true;
                    }
                }
            }
        }
        /// <summary>
        /// 開発符号情報リスト
        /// </summary>
        public DataTable DevelopmentCodeList
        {
            get
            {
                return _DevelopmentCodeData.Copy();
            }
            set
            {
                if (value != null)
                {
                    for (int i = 0; i < _DevelopmentCodeData.Rows.Count; i++)
                    {
                        if (value.AsEnumerable().All(row => row["DEVELOPMENT_CODE"]
                             != _DevelopmentCodeData.Rows[i]["DEVELOPMENT_CODE"]))
                        {
                            _DevelopmentCodeData.AsEnumerable().Where(row =>
                        row["DEVELOPMENT_CODE"] == _DevelopmentCodeData.Rows[i]["DEVELOPMENT_CODE"])
                        .FirstOrDefault().Delete();
                            _EditFlg = true;
                        }
                    }
                    for (int i = 0; i < value.Rows.Count; i++)
                    {
                        if (_DevelopmentCodeData.AsEnumerable().All(row => row["DEVELOPMENT_CODE"]
                             != value.Rows[i]["DEVELOPMENT_CODE"]))
                        {
                            var dr = _DevelopmentCodeData.NewRow();
                            dr["MITAKA_NO"] = _ManageNo;
                            dr["DEVELOPMENT_CODE"] = value.Rows[i]["DEVELOPMENT_CODE"];
                            dr["EDIT_FLG"] = "1";
                            _DevelopmentCodeData.Rows.Add(dr);
                        }
                    }
                    if (!(_DevelopmentCodeData.AsEnumerable().All(row => row["EDIT_FLG"].ToString() != "1")))
                    {
                        _EditFlg = true;
                    }
                }
            }
        }
        /// <summary>
        /// 設通情報リスト
        /// </summary>
        public DataTable EcsList
        {
            get
            {
                return _EcsData.Copy();
            }
            set
            {
                if (value != null)
                {
                    for (int i = 0; i < _EcsData.Rows.Count; i++)
                    {
                        if (value.AsEnumerable().All(row => row["ECS_NO"]
                             != _EcsData.Rows[i]["ECS_NO"]))
                        {
                            _EcsData.AsEnumerable().Where(row =>
                        row["ECS_NO"] == _EcsData.Rows[i]["ECS_NO"])
                        .FirstOrDefault().Delete();
                            _EditFlg = true;
                        }
                    }
                    for (int i = 0; i < value.Rows.Count; i++)
                    {
                        if (_EcsData.AsEnumerable().All(row => row["ECS_NO"]
                             != value.Rows[i]["ECS_NO"]))
                        {
                            var dr = _EcsData.NewRow();
                            dr["MITAKA_NO"] = _ManageNo;
                            dr["ECS_NO"] = value.Rows[i]["ECS_NO"];
                            dr["EDIT_FLG"] = "1";
                            _EcsData.Rows.Add(dr);
                        }
                    }
                    if (!(_EcsData.AsEnumerable().All(row => row["EDIT_FLG"].ToString() != "1")))
                    {
                        _EditFlg = true;
                    }
                }
            }
        }
        /// <summary>
        /// 過去トラ観たか回答情報
        /// </summary>
        public MitakaAnswerData MitakaAnswerData
        {
            get
            {
                return _MitakaAnswerData;
            }
        }
        /// <summary>
        /// データ処理結果
        /// </summary>
        public List<string> DataProcessResult
        {
            get
            {
                if (_DataProcessResult != null)
                return _DataProcessResult;

                // デフォルト返却
                return 
                    new List<string>();
            }
            set
            {
                if ((value != null) && (value != _DataProcessResult))
                {
                    _DataProcessResult = value;
                }
            }
        }
        /// <summary>
        /// 編集フラグ
        /// </summary>
        public bool EditFlg
        {
            get
            {
                    return _EditFlg;
            }
        }
        /// <summary>
        /// 過去トラ観たか権限
        /// </summary>
        public string MitakaAuthority
        {
            get
            {
                if (_MitakaAuthority != null)
                    return _MitakaAuthority;

                return "";
            }
        }
        /// <summary>
        /// 埋め込みスクリプト
        /// </summary>
        public string embeddedScript
        {
            get
            {
                if (_embeddedScript != null)
                    return _embeddedScript;

                return "";

            }
            set
            {
                if ((value != null) && (value != embeddedScript))
                {
                    embeddedScript = value;
                }
            }
        }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="test">ユーザーID</param>
        public MitakaData(string userId = null,bool test = false) 
        {
            if (test)
                _test = test;

            if (userId != null)
                _Owner = userId;

            _DbMitakaData = new DAMitakaData(_Owner);

            // 過去トラ観たか情報初期化
            initMitakaData();
            // 展開対象追加
            if(!test)
            addDeploymentTroubleData("DeployAdd");
        }
        /// <summary>
        /// コンストラクタ（テスト用）
        /// </summary>
        /// <param name="dbMitakaData"></param>
        public MitakaData(IDAMitakaData dbMitakaData,bool test = false)
        {
            if (test)
                _test = test;

            _DbMitakaData = dbMitakaData;

            // 過去トラ観たか情報初期化
            initMitakaData(dbMitakaData);
            // 展開対象追加
            if(!test)
                addDeploymentTroubleData("DeployAdd");
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="manageNo"></param>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <param name="dbMitakaData"></param>
        public MitakaData(string manageNo, string userId, string request = "",bool test = false)
        {
            if (test)
                _test = test;

            if (userId != null)
                _Owner = userId;

            _DbMitakaData = new DAMitakaData(_Owner);

            if ((manageNo == null) || (userId == null))
            {
                // 過去トラ観たか情報初期化
                initMitakaData();
            }
            else
            {
                _ManageNo = manageNo;

                // 過去トラ観たか情報権限取得
                getMitakaAuthority(test);

                if (request == "HeadOnly ")
                {
                    // 過去トラ観たか情報取得（ヘッダーのみ）
                    getMitakaData();
                }
                else
                {
                    // 過去トラ観たか情報取得
                    getMitakaData(true);
                }

                switch (request)
                {
                    case "DeployAdd":
                    case "DeployUpdate":
                        // 展開対象追加
                        if (!test && request == "DeployAdd")
                        addDeploymentTroubleData(request);

                        break;
                    default:
                        break;
                }

            }
        }
        #endregion
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="manageNo"></param>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <param name="dbMitakaData"></param>

        public MitakaData(IDAMitakaData dbMitakaData, string manageNo, string userId, string request = "",bool test =false)
        {
            if (test)
                _test = test;

            _DbMitakaData = dbMitakaData;

            if ((manageNo == null) || (userId == null))
            {
                // 過去トラ観たか情報初期化
                initMitakaData();
            }
            else
            {
                _ManageNo = manageNo;
                _Owner = userId;

                // 過去トラ観たか情報権限取得
                getMitakaAuthority(test);

                if (request == "HeadOnly ")
                {
                    // 過去トラ観たか情報取得（ヘッダーのみ）
                    getMitakaData();
                }
                else
                {
                    // 過去トラ観たか情報取得
                    getMitakaData(true);
                    getMitakaAnswerData(dbMitakaData);
                }

                switch (request)
                {
                    case "DeployAdd":
                    case "DeployUpdate":
                        // 展開対象追加
                        if (!test && request == "DeployAdd")
                            addDeploymentTroubleData(request);

                        break;
                    default:
                        break;
                }

            }
        }

        #region メソッド

        /// <summary>
        /// 過去トラ観たか情報初期化
        /// </summary>
        public void initMitakaData(IDAMitakaData dbMitakaData = null)
        {
            // 過去トラ観たかヘッダー初期化
            _MitakaHeaderData = _DbMitakaData.getMitakaHeaderData();
            // 関連ユーザー情報初期化
            _ReLationUserData = _DbMitakaData.getReLationUserData();
            // 検索条件初期化
            _SearchParameterData = _DbMitakaData.getSearchParameterData();
            // 展開対象初期化
            _DeploymentTroubleData = _DbMitakaData.getDeploymentTroubleData();
            // タイトル品番情報初期化
            _TitleDrawingData = _DbMitakaData.getTitleDrawingData();
            // 機種情報初期化
            _ModelData = _DbMitakaData.getModelData();
            // BLK情報初期化
            _BlockData = _DbMitakaData.getBlockData();
            // 開発符号情報初期化
            _DevelopmentCodeData = _DbMitakaData.getDevelopmentCodeData();
            // 設通情報初期化
            _EcsData = _DbMitakaData.getEcsData();
            // 過去トラ観たか情報インスタンス作成
            if (dbMitakaData == null)
                _MitakaAnswerData = new MitakaAnswerData();
            else
                _MitakaAnswerData = new MitakaAnswerData(dbMitakaData);
            // 編集フラグ初期化
            _EditFlg = false;
        }

        /// <summary>
        /// 過去トラ観たか権限取得
        /// </summary>
        private void getMitakaAuthority(bool test = false)
        {

            // テスト時は、定数設定している権限を返却する
            if (test)
            {
                _MitakaAuthority = Def.DefSETTING_DEFAULT_MITAKA_AUTHORITY;
                return;
            }

            // ユーザー情報取得
            var userData = new SqlCommon().SelectUser(_Owner).AsEnumerable().FirstOrDefault();

            if (userData == null)
            {
                _MitakaAuthority = Def.MITAKA_AUTHORITY_USER; // 過去トラ利用者
                return;
            }

            // ログインユーザー権限判定
            if (userData["SQB_FLG"].ToString() == "1")
            {
                _MitakaAuthority = Def.MITAKA_AUTHORITY_SQB; // SQBユーザー
            }
            else if (userData["USER_ID"] == InspectionUser["USER_ID"])
            {
                _MitakaAuthority = Def.MITAKA_AUTHORITY_INSPECTOR; // 点検者
            }
            else if (userData["USER_ID"] == CreateMainUser["USER_ID"])
            {
                _MitakaAuthority = Def.MITAKA_AUTHORITY_CREATER_MAIN; // 作成者（主）
            }
            else if (userData["USER_ID"] == CreateSubUser["USER_ID"])
            {
                _MitakaAuthority = Def.MITAKA_AUTHORITY_CREATER_SUB; // 作成者（副）
            }
            else if (userData["KA_CODE"].ToString() == ManageMainDivisionCode)
            {
                _MitakaAuthority = Def.MITAKA_AUTHORITY_DIVISION_MAIN; // 管理部署（主）ユーザー
            }
            else if (userData["KA_CODE"].ToString() == ManageSubDivisionCode)
            {
                _MitakaAuthority = Def.MITAKA_AUTHORITY_DIVISION_SUB; // 管理部署（副）ユーザー
            }
            else if (ConfirmationRequestList.AsEnumerable()
                .All(row => row["USER_ID"] == userData["USER_ID"]))
            {
                _MitakaAuthority = Def.MITAKA_AUTHORITY_REQUESTER; // 回答依頼先ユーザー
            }
            else
            {
                _MitakaAuthority = Def.MITAKA_AUTHORITY_USER; // 過去トラ利用者
            }
        }

        /// <summary>   
        /// 過去トラ観たか項目チェック
        /// </summary>
        /// <param name="pattern">処理区分(登録・更新："1",削除："2")</param>
        /// <returns>チェック結果(チェックOK：True,チェックNG：False)</returns>
        public bool checkMitakaData(string pattern)
        {
            switch (pattern)
            {

                case "1": //登録・更新
                    // 未入力チェック
                    if (
                        Title == "" ||
                        Purpose == "" ||
                        EndDateTime == "" 
                        )
                    {
                        // 未入力項目有り（「チェックNG」返却」）
                        return false;
                    }

                    if (CreateMainUser.IsNull("USER_ID") || CreateMainUser["USER_ID"].ToString() == "" ||
                        InspectionUser.IsNull("USER_ID") || InspectionUser["USER_ID"].ToString() == "")
                        // 未入力項目有り（「チェックNG」返却」）
                        return false;

                    break;

                case "2": //削除

                    // 未入力チェック
                    if (ManageNo == "")
                    {
                        // 未入力項目有り（「チェックNG」返却」）
                        return false;
                    }
                    break;


                default:
                    // 上記に該当するパターンが存在しない（「チェックNG」返却」）
                    return false;
            }

            // 全項目チェックエラー無し（「チェックOK」返却）
            return true;
        }

        /// <summary>
        /// 過去トラ観たか情報取得
        /// </summary>
        /// <param name="getAll">ALL取得フラグ</param>
        public void getMitakaData(bool getAll = false)
        {
            // 管理番号が例外の場合
            if ((_ManageNo == null) || (_ManageNo == ""))
            {
                // 処理終了
                return;
            }

            // 過去トラ観たかヘッダー取得
            _MitakaHeaderData = _DbMitakaData.getMitakaHeaderData(_ManageNo);
            // 関連ユーザー情報取得
            _ReLationUserData = _DbMitakaData.getReLationUserData(_ManageNo);
            // 関連ユーザー情報取得
            _ReLationUserData = _DbMitakaData.getReLationUserData(_ManageNo);

            // ALL取得フラグがFalseの場合
            if (!getAll)
            {
                // 処理終了
                return;
            }

            // 検索条件取得
            _SearchParameterData = _DbMitakaData.getSearchParameterData(_ManageNo);
            // 展開対象情報取得
            _DeploymentTroubleData = _DbMitakaData.getDeploymentTroubleData(_ManageNo);
            // タイトル品番情報取得
            _TitleDrawingData = _DbMitakaData.getTitleDrawingData(_ManageNo);
            // 機種情報取得
            _ModelData = _DbMitakaData.getModelData(_ManageNo);
            // BLK情報取得
            _BlockData = _DbMitakaData.getBlockData(_ManageNo);
            // 開発符号情報取得
            _DevelopmentCodeData = _DbMitakaData.getDevelopmentCodeData(_ManageNo);
            // 設通情報取得
            _EcsData = _DbMitakaData.getEcsData(_ManageNo);
            // 過去トラ観たか回答情報取得
            _MitakaAnswerData = new MitakaAnswerData(_ManageNo);
            // 編集フラグ初期化
            _EditFlg = false;
        }

        /// <summary>
        /// 過去トラ観たか情報登録・更新
        /// </summary>
        /// <returns>登録・更新結果(登録・更新OK：True,登録・更新NG：False)</returns>
        public Boolean postMitakaData()
        {
            if (ManageNo != "") // 更新
            {

                // 権限チェック
                switch (MitakaAuthority)
                {
                    case "10":
                    case "20":
                    case "30":
                    case "31":
                    case "40":
                    case "41":
                        break;
                    default:
                        // 登録・更新失敗（「登録・更新NG」返却）
                        _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_POST_FAILURE);
                        return false;
                }
            }

            // 入力値チェック
            if (!checkMitakaData("1"))
            {
                // 登録・更新失敗（「登録・更新NG」返却）
                _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_POST_FAILURE);
                return false;
            }

            // 過去トラ観たか情報テーブル登録
            if (!postMitakaTableData())
            {
                // 登録・更新失敗（「登録・更新NG」返却）
                _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_POST_FAILURE);
                return false;
            }

            // 登録・更新失敗（「登録・更新NG」返却）
            _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_POST_SUCCESS);
            return true;
        }
        /// <summary>
        /// 過去トラ観たか情報削除
        /// </summary>
        /// <returns>削除結果(削除OK：True,削除NG：False)</returns>
        public Boolean deleteMitakaData()
        {
            if ((_ManageNo == null) || (_ManageNo == ""))
            {
                // 削除失敗（「削除NG」返却）
                _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_DELETE_FAILURE);
                return false;
            }

            // 権限チェック
            switch (MitakaAuthority)
            {
                case "10":
                case "20":
                case "30":
                    break;
                default:
                    _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_DELETE_FAILURE);
                    // 削除失敗（「削除NG」返却）
                    return false;
            }

            // 入力値チェック
            if (!checkMitakaData("2"))
            {
                // 削除失敗（「削除NG」返却）
                _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_DELETE_FAILURE);
                return false;
            }

            // 過去トラ観たか情報テーブル削除
            if (!deleteMitakaTableData())
            {
                // 削除失敗（「削除NG」返却）
                _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_DELETE_FAILURE);
                return false;
            }

            // 削除成功（「削除OK」返却）
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(getScriptForAlertMessage(MessageConst.MITAKA_DELETE_SUCCESS));
            sb.AppendLine(getScriptForCloseWindow());
            _embeddedScript = sb.ToString();
            return true;
        }


        /// <summary>
        /// 過去トラ観たかテーブル情報登録・更新
        /// </summary>
        /// <returns>登録結果(登録OK：True,登録NG：False)</returns>
        private Boolean postMitakaTableData()
        {
            if ((_ManageNo == null) || (_ManageNo == "")) // 新規登録
            {
                // 最新管理番号取得
                _ManageNo = _DbMitakaData.getMaxManageNo(CreateMainUser["KA_CODE"].ToString());
                    _MitakaHeaderData.AsEnumerable().FirstOrDefault()["MITAKA_NO"] = _ManageNo;
                    _MitakaHeaderData.AsEnumerable().FirstOrDefault()["EDIT_FLG"] = "1";
            }
            // 過去トラ観たかヘッダー登録・更新
            if (!(_DbMitakaData.postMitakaHeaderData(_MitakaHeaderData))) // 「登録・更新NG」の場合
            {
                // エラーメッセージをセット
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                // 登録・更新失敗（「登録・更新NG」返却）
                return false;
            }
            // 検索条件登録・更新
            if (!(_DbMitakaData.postSearchParameterData(_SearchParameterData))) // 「登録・更新NG」の場合
            {
                // エラーメッセージをセット
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                // 登録失敗（「登録NG」返却）
                return false;
            }
            // 関連ユーザー情報登録・更新
            if (!(_DbMitakaData.postReLationUserData(_ReLationUserData))) // 「登録・更新NG」の場合
            {
                // エラーメッセージをセット
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                // 登録失敗（「登録NG」返却）
                return false;
            }
            // 展開対象情報登録・更新
            if (!(_DbMitakaData.postDeploymentTroubleData(_DeploymentTroubleData))) // 「登録・更新NG」の場合
            {
                // エラーメッセージをセット
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                // 登録失敗（「登録NG」返却）
                return false;
            }
            // 過去トラ観たか回答情報登録・削除
            if (!(_MitakaAnswerData.postMitakaAnswerData())) // 「登録・削除NG」の場合
            {
                // エラーメッセージをセット
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                // 登録失敗（「登録NG」返却）
                return false;
            }
            // タイトル品番情報登録・削除
            if (!(_DbMitakaData.postTitleDrawingData(_TitleDrawingData))) // 「登録・更新NG」の場合
            {
                // エラーメッセージをセット
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                // 登録失敗（「登録NG」返却）
                return false;
            }
            // 機種情報登録・削除
            if (!(_DbMitakaData.postModelData(_ModelData))) // 「登録・更新NG」の場合
            {
                // エラーメッセージをセット
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                // 登録失敗（「登録NG」返却）
                return false;
            }
            // BLK情報登録・削除
            if (!(_DbMitakaData.postBlockData(_BlockData))) // 「登録・更新NG」の場合
            {
                // エラーメッセージをセット
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                // 登録失敗（「登録NG」返却）
                return false;
            }
            // 開発符号情報登録・削除
            if (!(_DbMitakaData.postDevelopmentCodeData(_DevelopmentCodeData))) // 「登録・更新NG」の場合
            {
                // エラーメッセージをセット
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                // 登録失敗（「登録NG」返却）
                return false;
            }
            // 設通情報登録・削除
            if (!(_DbMitakaData.postEcsData(_EcsData))) // 「登録・更新NG」の場合
            {
                // エラーメッセージをセット
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                // 登録失敗（「登録NG」返却）
                return false;
            }
            // 登録成功（「登録OK」返却）
            return true;
        }

        /// <summary>
        /// 過去トラ観たかテーブル情報削除
        /// </summary>
        /// <returns>削除結果(削除OK：True,削除NG：False)</returns>
        private Boolean deleteMitakaTableData()
        {
            if ((_ManageNo == null) || (_ManageNo == "")) // 新規削除
            {
                // 削除失敗（「削除NG」返却）
                return false;
            }
            // 過去トラ観たかヘッダー削除
            if (!(_DbMitakaData.deleteMitakaHeaderData(_ManageNo))) // 「削除NG」の場合
            {
                // エラーメッセージをセット
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                // 削除・更新失敗（「削除NG」返却）
                return false;
            }
            // 検索条件削除
            if (!(_DbMitakaData.deleteSearchParameterData(_ManageNo))) // 「削除NG」の場合
            {
                // エラーメッセージをセット
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                // 削除失敗（「削除NG」返却）
                return false;
            }
            // 関連ユーザー情報削除
            if (!(_DbMitakaData.deleteReLationUserData(_ManageNo))) // 「削除NG」の場合
            {
                // エラーメッセージをセット
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                // 削除失敗（「削除NG」返却）
                return false;
            }
            // 展開対象情報削除
            if (!(_DbMitakaData.deleteDeploymentTroubleData(_ManageNo))) // 「削除NG」の場合
            {
                // エラーメッセージをセット
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                // 削除失敗（「削除NG」返却）
                return false;
            }
            if (_MitakaAnswerData == null)
                getMitakaAnswerData();

            // 過去トラ観たか回答情報削除
            if (!(_MitakaAnswerData.deleteMitakaAnswerData())) // 「削除NG」の場合
            {
                // エラーメッセージをセット
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                // 削除失敗（「削除NG」返却）
                return false;
            }
            // タイトル品番情報削除
            if (!(_DbMitakaData.deleteTitleDrawingData(_ManageNo))) // 「削除NG」の場合
            {
                // エラーメッセージをセット
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                // 削除失敗（「削除NG」返却）
                return false;
            }
            // 機種情報削除
            if (!(_DbMitakaData.deleteModelData(_ManageNo))) // 「削除NG」の場合
            {
                // エラーメッセージをセット
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                // 削除失敗（「削除NG」返却）
                return false;
            }
            // BLK情報削除
            if (!(_DbMitakaData.deleteBlockData(_ManageNo))) // 「削除NG」の場合
            {
                // エラーメッセージをセット
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                // 削除失敗（「削除NG」返却）
                return false;
            }
            // 開発符号情報削除
            if (!(_DbMitakaData.deleteDevelopmentCodeData(_ManageNo))) // 「削除NG」の場合
            {
                // エラーメッセージをセット
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                // 削除失敗（「削除NG」返却）
                return false;
            }
            // 設通情報削除
            if (!(_DbMitakaData.deleteEcsData(_ManageNo))) // 「削除NG」の場合
            {
                // エラーメッセージをセット
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                // 削除失敗（「削除NG」返却）
                return false;
            }
            // 削除成功（「削除OK」返却）
            return true;
        }

        /// <summary>
        /// 過去トラ観たか情報点検依頼
        /// </summary>
        /// <returns>更新結果(更新OK：True、更新NG：False)</returns>
        public Boolean requestMitakaData()
        {
            // 権限チェック
            switch (MitakaAuthority)
            {
                case Def.MITAKA_AUTHORITY_SQB:
                case Def.MITAKA_AUTHORITY_INSPECTOR:
                case Def.MITAKA_AUTHORITY_CREATER_MAIN:
                case Def.MITAKA_AUTHORITY_CREATER_SUB:
                case Def.MITAKA_AUTHORITY_DIVISION_MAIN:
                case Def.MITAKA_AUTHORITY_DIVISION_SUB:
                    break;
                default:
                    // 更新失敗（「更新NG」返却）
                    _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_REQUEST_FAILURE);
                    return false;
            }

            // 管理番号フィールドが""でない場合
            if ((_ManageNo == null) || (_ManageNo == "")) 
            {
                // 更新失敗（「更新NG」返却）
                _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_REQUEST_FAILURE);
                return false;
            }

            // 状況 ≠ 回答中の場合
            if (Status != Def.MITAKA_STATUS_ANSWER)
            {
                // 過去トラ観たかヘッダー更新（回答開始）
                if (!_DbMitakaData.updateHeaderDataToStatus(_ManageNo, Def.MITAKA_STATUS_ANSWER))
                {
                    // 更新失敗（「更新NG」返却）
                    _DataProcessResult = _DbMitakaData.ErrorMessage;
                    getScriptForAlertMessage(MessageConst.MITAKA_REQUEST_FAILURE);
                    return false;
                };

                // 更新成功（「更新OK」返却）
                getScriptForAlertMessage(MessageConst.MITAKA_REQUEST_SUCCESS);

                // 過去トラ観たかヘッダー再取得
                getMitakaData();
            }
            // 点検依頼メール作成スクリプト取得
            _embeddedScript = 
                _embeddedScript + createMailForMitakaData(Def.MITAKA_PATTERN_REQUEST);

            // 更新成功（「更新OK」返却）
            return true;
        }


        /// <summary>
        /// 過去トラ観たか情報取消
        /// </summary>
        /// <returns>更新結果(更新OK：True、更新NG：False)</returns>
        public Boolean cancelMitakaData()
        {
            // 権限チェック
            switch (MitakaAuthority)
            {
                case Def.MITAKA_AUTHORITY_SQB:
                case Def.MITAKA_AUTHORITY_INSPECTOR:
                case Def.MITAKA_AUTHORITY_CREATER_MAIN:
                    break;
                default:
                    // 更新失敗（「更新NG」返却）
                    _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_CANCEL_FAILURE);
                    return false;
            }

            // 管理番号フィールドが""でない場合
            if ((_ManageNo == null) || (_ManageNo == ""))
            {
                // 更新失敗（「更新NG」返却）
                _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_CANCEL_FAILURE);
                return false;
            }

            // 状況 ≠ 取消の場合
            if (Status != Def.MITAKA_STATUS_CANCEL)
            {
                // 過去トラ観たかヘッダー更新（取消）
                if (!_DbMitakaData.updateHeaderDataToStatus(_ManageNo, Def.MITAKA_STATUS_CANCEL,StatusComment))
                {
                    // 更新失敗（「更新NG」返却）
                    _DataProcessResult = _DbMitakaData.ErrorMessage;
                    getScriptForAlertMessage(MessageConst.MITAKA_CANCEL_FAILURE);
                    return false;
                };

                // 更新成功（「更新OK」返却）
                getScriptForAlertMessage(MessageConst.MITAKA_CANCEL_SUCCESS);

                // 過去トラ観たかヘッダー再取得
                getMitakaData();
            }
            // 点検中止メール作成スクリプト取得
            _embeddedScript =
                _embeddedScript + createMailForMitakaData(Def.MITAKA_PATTERN_CANCEL);

            // 更新成功（「更新OK」返却）
            return true;
        }
        /// <summary>
        /// 過去トラ観たか情報確認完了
        /// </summary>
        /// <returns>更新結果(更新OK：True、更新NG：False)</returns>
        public Boolean confirmedMitakaData()
        {
            // 権限チェック
            switch (MitakaAuthority)
            {
                case Def.MITAKA_AUTHORITY_SQB:
                case Def.MITAKA_AUTHORITY_INSPECTOR:
                case Def.MITAKA_AUTHORITY_CREATER_MAIN:
                    break;
                default:
                    // 更新失敗（「更新NG」返却）
                    _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_CONFIRMED_FAILURE);
                    return false;
            }

            // 管理番号フィールドが""でない場合
            if ((_ManageNo == null) || (_ManageNo == ""))
            {
                // 更新失敗（「更新NG」返却）
                _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_CONFIRMED_FAILURE);
                return false;
            }

            // 状況 ≠ 完了の場合
            if (Status != Def.MITAKA_STATUS_CONFIRMED)
            {
                // 過去トラ観たかヘッダー更新（完了）
                if (!_DbMitakaData.updateHeaderDataToStatus(_ManageNo, Def.MITAKA_STATUS_CONFIRMED))
                {
                    // 更新失敗（「更新NG」返却）
                    _DataProcessResult = _DbMitakaData.ErrorMessage;
                    getScriptForAlertMessage(MessageConst.MITAKA_CONFIRMED_FAILURE);
                    return false;
                };

                // 更新成功（「更新OK」返却）
                getScriptForAlertMessage(MessageConst.MITAKA_CONFIRMED_SUCCESS);

                // 過去トラ観たかヘッダー再取得
                getMitakaData();
            }
            // 点検完了メール作成スクリプト取得
            _embeddedScript =
                _embeddedScript + createMailForMitakaData(Def.MITAKA_PATTERN_CONFIRMED);

            // 更新成功（「更新OK」返却）
            return true;
        }

        /// <summary>
        /// 過去トラ観たか情報完了取消
        /// </summary>
        /// <returns>更新結果(更新OK：True、更新NG：False)</returns>
        public Boolean cancellConfirmedMitakaData()
        {
            // 権限チェック
            switch (MitakaAuthority)
            {
                case Def.MITAKA_AUTHORITY_SQB:
                case Def.MITAKA_AUTHORITY_INSPECTOR:
                case Def.MITAKA_AUTHORITY_CREATER_MAIN:
                    break;
                default:
                    // 更新失敗（「更新NG」返却）
                    _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_CONFIRMED_CANCEL_FAILURE);
                    return false;
            }

            // 管理番号フィールドが""でない場合
            if ((_ManageNo == null) || (_ManageNo == ""))
            {
                // 更新失敗（「更新NG」返却）
                _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_CONFIRMED_CANCEL_FAILURE);
                return false;
            }

            // 状況 ≠ 完了の場合
            if (Status == Def.MITAKA_STATUS_CONFIRMED)
            {
                // 過去トラ観たかヘッダー更新（完了取消）
                if (!_DbMitakaData.updateHeaderDataToStatus(_ManageNo, Def.MITAKA_STATUS_ANSWER))
                {
                    // 更新失敗（「更新NG」返却）
                    _DataProcessResult = _DbMitakaData.ErrorMessage;
                    getScriptForAlertMessage(MessageConst.MITAKA_CONFIRMED_CANCEL_FAILURE);
                    return false;
                };

                // 更新成功（「更新OK」返却）
                getScriptForAlertMessage(MessageConst.MITAKA_CONFIRMED_CANCEL_SUCCESS);

                // 過去トラ観たかヘッダー再取得
                getMitakaData();
            }

            // 更新成功（「更新OK」返却）
            return true;
        }

        /// <summary>
        /// 過去トラ観たか回答情報取得
        /// </summary>
        public void getMitakaAnswerData(IDAMitakaData _dbMitakaData = null)
        {
            if (_dbMitakaData == null)
                _MitakaAnswerData = new MitakaAnswerData(_ManageNo);
            else
                _MitakaAnswerData = new MitakaAnswerData(_dbMitakaData, _ManageNo);
        }

        /// <summary>
        /// 展開対象情報追加
        /// </summary>
        /// <param name="request">リクエスト("DeployAdd"：展開対象追加、"DeployUpdate"：展開対象最新化)</param>
        private void addDeploymentTroubleData(string request)
        {
            TroubleData troubleData = new TroubleData();
            string keyword;
            string keywordCondition;
            DataTable cotegory;
            string cotegoryCondition;
            DataTable troubleList = new DataTable();

            if (request == Def.MITAKA_DEPLOY_ADD) // 展開対象追加
            {
                // 検索条件取得（セッション）
                getTrobuleSearchParameterFromSession(out keyword, out keywordCondition, out cotegory, out cotegoryCondition);

                if (!((keyword != null) && (keywordCondition != null)) || ((cotegory != null) && (cotegoryCondition != null)))
                {
                    return;
                }

                // 過去トラ情報検索
                troubleList = troubleData.getTroubleList(keyword, keywordCondition, cotegory, cotegoryCondition);
            }
            else if (request == Def.MITAKA_DEPLOY_UPDATE) // 展開対象最新化
            {

                // 検索グループリスト取得
                var searchGroupList = _SearchParameterData.AsEnumerable().Select(row =>
                    row["GROUP_ID"].ToString()).Distinct().ToList();

                for (int i = 0; i < searchGroupList.Count; i++)
                {
                    keyword = null;
                    keywordCondition = null;
                    cotegory = null;
                    cotegoryCondition = null;

                    // 検索条件抽出
                    var searchGroupData = _SearchParameterData.AsEnumerable()
                        .Where(row => row["GROUP_ID"].ToString() == searchGroupList[i]).CopyToDataTable();

                    for (int j = 0; j < searchGroupData.Rows.Count; j++)
                    {
                        if (searchGroupData.Rows[j]["SEARCH_TYPE"].ToString() == Def.DefTYPE_WORD)
                        {
                            keywordCondition = (string)searchGroupData.Rows[j]["SEARCH_CLASS"];
                            keyword = (string)searchGroupData.Rows[j]["SEARCH_PARAMETER"];
                        }
                        else if (searchGroupData.Rows[j]["SEARCH_CLASS"].ToString() == Def.DefTYPE_AND)
                        {
                            DataTable cotegoyTable = troubleData.getCotegoryDataTable();

                            // カテゴリ条件フォーマット変換(string → DataTable)
                            if (searchGroupData.Rows[j]["SEARCH_PARAMETER"].ToString() != "")
                            {
                                char listSplit = char.Parse("<->");
                                string[] searchList = searchGroupData.Rows[j]["SEARCH_PARAMETER"].ToString().Split(listSplit);

                                for (int k = 0; k < searchList.Count(); k++)
                                {
                                    char rowSplit = char.Parse("<>");
                                    string[] searchRow = searchList[k].Split(rowSplit);

                                    if (searchRow.Count() == 2)
                                    {
                                        DataRow dr = cotegoyTable.NewRow();
                                        dr["TableType"] = searchRow[0];     //タイプ
                                        dr["ItemValue1"] = searchRow[1];    //検索文字列
                                        cotegoyTable.Rows.Add(dr);
                                    }
                                }
                            }

                            cotegoryCondition = (string)searchGroupData.Rows[j]["SEARCH_CLASS"];
                            cotegory = cotegoyTable;
                        }

                        //過去トラ情報検索
                        var searchResult = troubleData.getTroubleList(keyword, keywordCondition, cotegory, cotegoryCondition);

                        for (int k = 0; k < searchResult.Rows.Count; k++)
                            troubleList.Rows.Add(searchResult.Rows[k]);
                    }
                }
            }

            // 検索結果リストの重複削除
            if(troubleList.Rows.Count != 0)
                troubleList = troubleList.AsEnumerable().Distinct().CopyToDataTable();

            if (troubleList.Rows.Count == 0)
                return;

            // 過去トラ観たか回答情報インスタンス定義
            if (_MitakaAnswerData == null)
                _MitakaAnswerData = new MitakaAnswerData(_ManageNo);

            for (int i = 0; i < troubleList.Rows.Count; i++)
            {
                // 対象行が展開対象情報に含まれているかチェック
                if (!_DeploymentTroubleData.AsEnumerable()
                    .Any(row => row["SYSTEM_NO"] == troubleList.Rows[i]["SYSTEM_NO"]))
                {
                    // 対象行を展開対象情報フィールドに追加
                    _DeploymentTroubleData.Rows.Add(troubleList.Rows[i]);
                    // 過去トラ観たか回答情報展開
                    _MitakaAnswerData.deployMitakaAnswerData(troubleList.Rows[i]);
                }

            }
        }


        /// <summary>
        /// 過去トラ観たか情報メール作成
        /// </summary>
        /// <param name="pattern">区分("request"：点検依頼、"cancel"：取消、"complete"：確認完了)</param>
        /// <returns></returns>
        private string createMailForMitakaData(string pattern)
        {
            List<string> toList = new List<string>();
            List<string> ccList = new List<string>();
            string subject = "";
            string body = "";
            StringBuilder sb = new StringBuilder();
            bool notExist = false;

            // 宛先作成
            if (ConfirmationRequestList.AsEnumerable().Any(row => row.IsNull("MAIL")))
                notExist = true;

            toList = ConfirmationRequestList.AsEnumerable()
                .Where(row => !row.IsNull("MAIL")).Select(field => (string)field["MAIL"]).ToList();


            // Cc作成
            if (!CreateMainUser.IsNull("MAIL"))
                ccList.Add((string)CreateMainUser["MAIL"]); // 作成者（主）
            else
                notExist = true;

            if (!CreateSubUser.IsNull("MAIL"))
                ccList.Add((string)CreateSubUser["MAIL"]); // 作成者（副）
            else
                notExist = true;

            if (!InspectionUser.IsNull("MAIL"))
                ccList.Add((string)InspectionUser["MAIL"]); // 点検者
            else
                notExist = true;

            if (notExist)
                sb.AppendLine(getScriptForAlertMessage(
                    MessageConst.MITAKA_REQUEST_FAILURE_NOT_MAILADDRESS));

            // 件名・本文作成
            switch (pattern)
            {
                case Def.MITAKA_PATTERN_REQUEST:
                    subject = string.Format(Def.SCRIPT_MAIL_FORMAT_SUBJECT,
                        Def.SCRIPT_MAIL_SUBJECT_REQUEST, ManageNo, Title);

                    body = string.Format(Def.SCRIPT_MAIL_FORMAT_BODY_REQUEST,
                        MessageConst.MAIL_BODY_MITAKA_REQUEST,
                        Title, Purpose, EndDateTime,
                        Def.SCRIPT_MAIL_BODY_URL,
                        Def.SCRIPT_MAIL_BODY_WARNING
                        );

                    break;
                case Def.MITAKA_PATTERN_CONFIRMED:
                    subject = string.Format(Def.SCRIPT_MAIL_FORMAT_SUBJECT,
                        Def.SCRIPT_MAIL_SUBJECT_COMPLETE, ManageNo, Title);

                    body = string.Format(Def.SCRIPT_MAIL_FORMAT_BODY_COMPLETE,
                        MessageConst.MAIL_BODY_MITAKA_COMPLETE,
                        Title, Def.SCRIPT_MAIL_BODY_WARNING
                        );
                    break;
                case Def.MITAKA_PATTERN_CANCEL:
                    subject = string.Format(Def.SCRIPT_MAIL_FORMAT_SUBJECT,
                        Def.SCRIPT_MAIL_SUBJECT_CANCEL, ManageNo, Title);

                    body = string.Format(Def.SCRIPT_MAIL_FORMAT_BODY_COMPLETE,
                        MessageConst.MAIL_BODY_MITAKA_COMPLETE,
                        Title, StatusComment, Def.SCRIPT_MAIL_BODY_WARNING
                        );
                    break;

                default:
                    break;

            }

            // メール作成スクリプト取得
            return getScriptForCreatingMail(toList, ccList, subject, body);
        }

        #endregion
    }
}