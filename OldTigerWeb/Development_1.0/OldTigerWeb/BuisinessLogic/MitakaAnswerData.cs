using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using OldTigerWeb.DataAccess;

namespace OldTigerWeb.BuisinessLogic
{
    public class MitakaAnswerData:CommonPageLogic
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
        /// 過去トラ観たか回答情報フィールド
        /// </summary>
        private DataTable _MitakaAnswerData;
        /// <summary>
        /// 回答対象システムNoフィールド
        /// </summary>
        private int? _AnswerSystemNo;
        /// <summary>
        /// 回答対象部署コードフィールド
        /// </summary>
        private string _AnswerDepartmentCode;
        /// <summary>
        /// データ処理結果フィールド
        /// </summary>
        private List<string> _DataProcessResult;
        /// <summary>
        /// 編集フラグフィールド
        /// </summary>
        private bool _EditFlg;
        /// <summary>
        /// 埋め込みスクリプトフィールド
        /// </summary>
        private string _embeddedScript;
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
        /// 回答対象システムNo
        /// </summary>
        public string AnswerSystemNo
        {
            get
            {
                if (_AnswerSystemNo != null)
                    return _AnswerSystemNo.ToString();

                // フィールドが存在しない場合は、""を返却
                return "";
            }
            set
            {
                if ((value != null) && (value != _AnswerSystemNo.ToString()))
                {
                    int no;
                    if(int.TryParse(value,out no))
                    _AnswerSystemNo = no;
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
                if(_AnswerDepartmentCode != null)
                return _AnswerDepartmentCode;

                // フィールドが存在しない場合は、""を返却
                return "";
            }
            set
            {
                if ((value != null) && (value != _AnswerDepartmentCode))
                {
                    _AnswerDepartmentCode = value.Trim();
                }
            }
        }
        /// <summary>
        /// 過去トラ観たか回答リスト(全部署)
        /// </summary>
        public DataTable MitakaAnswerList
        {
            get
            {
                return _MitakaAnswerData.Copy();
            }
            set
            {
                if ((value != null) && !value.AsEnumerable()
                .SequenceEqual(_MitakaAnswerData.AsEnumerable()))
                {
                    // 過去トラ観たか回答情報フィールドでループ
                    for (int i = 0; i < _MitakaAnswerData.Rows.Count; i++)
                    {
                        // 取得値に過去トラ観たか回答情報フィールド対象行が存在しない場合
                        if (!value.AsEnumerable().Any(row =>
                        row["SYSTEM_NO"].ToString()
                        == _MitakaAnswerData.Rows[i]["SYSTEM_NO"].ToString().Trim() &&
                        row["ANSWER_DIVISION_CODE"].ToString()
                        == _MitakaAnswerData.Rows[i]["ANSWER_DIVISION_CODE"].ToString().Trim()))
                        {
                            // 対象データ削除
                            _MitakaAnswerData.Rows[i].Delete();
                            _EditFlg = true;
                        }
                    }

                    // 取得値でループ
                    for (int i = 0; i < value.Rows.Count; i++)
                    {
                       // 過去トラ観たか回答情報フィールドから取得値対象行と同一キーを持つデータ抽出
                        var filter = _MitakaAnswerData.AsEnumerable().Where(row =>
                                        row["SYSTEM_NO"].ToString()
                                        == value.Rows[i]["SYSTEM_NO"].ToString().Trim() &&
                                        row["ANSWER_DIVISION_CODE"].ToString()
                                        == value.Rows[i]["ANSWER_DIVISION_CODE"].ToString().Trim())
                                        .FirstOrDefault();

                        if (filter == null)　// 同一行が存在しない場合
                        {
                            // 取得値対象行を過去トラ観たか回答情報フィールドに追加（登録対象）
                            value.Rows[i]["EDIT_FLG"] = "1";
                            _MitakaAnswerData.ImportRow(value.Rows[i]);
                        }
                        else  // 同一行が存在する場合
                        {
                            // 入力項目が同一でない場合
                            if((filter["STATUS"].ToString() != value.Rows[i]["STATUS"].ToString().Trim()) &&
                                (filter["ANSWER_CONTENT"].ToString() != value.Rows[i]["ANSWER_CONTENT"].ToString().Trim()))
                            {
                                // 取得値対象行
                                filter["STATUS"] = value.Rows[i]["STATUS"].ToString().Trim();
                                filter["ANSWER_CONTENT"] = value.Rows[i]["ANSWER_CONTENT"].ToString().Trim();
                                filter["EDIT_FLG"] = "1";
                            }
                        }
                    }
                    // 過去トラ観たか回答情報フィールドに編集フラグ ＝ "1"のデータが存在する場合
                    if (!_MitakaAnswerData.AsEnumerable().Any(row => row["EDIT_FLG"].ToString() == "1"))
                        _EditFlg = true;
                }
            }
        }

        /// <summary>
        /// 過去トラ観たか回答リスト(指定部署)
        /// </summary>
        public DataTable MitakaAnswerDepartmentList
        {
            get
            {
                var filter = _MitakaAnswerData.Copy().AsEnumerable()
                    .Where(row => row["ANSWER_DIVISION_CODE"].ToString() == _AnswerDepartmentCode);

                if (filter != null)
                    if (filter.Count() != 0)
                        return filter.CopyToDataTable();

                return _MitakaAnswerData.Clone();

            }
        }
        /// <summary>
        /// 過去トラ観たか回答(回答単位)
        /// </summary>
        public DataRow MitakaAnswerTargetData
        {
            get
            {
                var filter = _MitakaAnswerData.Copy().AsEnumerable()
                    .Where(row => row["ANSWER_DIVISION_CODE"].ToString() == _AnswerDepartmentCode
                    && (int)row["SYSTEM_NO"] == _AnswerSystemNo
                    ).FirstOrDefault();

                if (filter != null)
                    return filter;

                // 新規行返却
                return _MitakaAnswerData.NewRow();
            }
            set
            {
                if (value != null)
                {
                    // 追加するシステムNO、回答対象部署コード設定
                    int? systemNo = (int)value["SYSTEM_NO"];
                    string divisionCode = value["ANSWER_DIVISION_CODE"].ToString();
                    // 取得値に要素が含まれない場合、プロパティより取得
                    if (systemNo == null && _AnswerSystemNo != null)
                        systemNo = _AnswerSystemNo;
                    if (divisionCode == null && divisionCode == "" &&
                        _AnswerDepartmentCode != null && _AnswerDepartmentCode == "")
                        divisionCode = _AnswerDepartmentCode;

                    // 設定値にNULLが含まれる場合は処理を抜ける
                    if (systemNo == null || divisionCode == null || divisionCode == "")
                        return;

                    // 取得値にて、過去トラ観たか回答情報フィールドをフィルターする
                    var filter = _MitakaAnswerData.AsEnumerable()
                        .Where(row => row["ANSWER_DIVISION_CODE"].ToString() == divisionCode
                        && (int)row["SYSTEM_NO"] == systemNo).FirstOrDefault();


                    if (filter != null) // 更新対象
                    {

                        if ((filter["STATUS"] != value["STATUS"])
                            || filter["ANSWER_CONTENT"] != value["ANSWER_CONTENT"])
                        {
                            filter["STATUS"] = value["STATUS"];
                            filter["ANSWER_CONTENT"] = value["ANSWER_CONTENT"];
                            filter["EDIT_FLG"] = "1";
                        }
                    }
                    else　// 登録対象
                    {
                        var dr = _MitakaAnswerData.NewRow();
                        dr["MITAKA_NO"] = value["MITAKA_NO"];
                        dr["SYSTEM_NO"] = value["SYSTEM_NO"];
                        dr["ANSWER_DIVISION_CODE"] = value["ANSWER_DIVISION_CODE"];
                        dr["STATUS"] = value["STATUS"];
                        dr["ANSWER_CONTENT"] = value["ANSWER_CONTENT"];
                        dr["EDIT_FLG"] = "1";
                        _MitakaAnswerData.Rows.Add(dr);
                    }
                }
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
                if ((value != null) && !value.SequenceEqual(_DataProcessResult))
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
        }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MitakaAnswerData():this(new DAMitakaData())
        {
            // 過去トラ観たか回答情報初期化
            initMitakaAnswerData();
        }
        /// <summary>
        /// コンストラクタ（テスト用）
        /// </summary>
        /// <param name="dbMitakaData"></param>
        public MitakaAnswerData(IDAMitakaData dbMitakaData)
        {
            _DbMitakaData = dbMitakaData;
            // 過去トラ観たか回答情報初期化
            initMitakaAnswerData();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <param name="systemNo">回答対象システムNo</param>
        /// <param name="departmentCode">回答対象部署コード</param>
        public MitakaAnswerData(string manageNo,    int? systemNo = null , string departmentCode = "") : this(new DAMitakaData())
        {
            // 初期値セット
            _ManageNo = manageNo;
            if (systemNo != null ) _AnswerSystemNo = systemNo;
            if (departmentCode != null && departmentCode != "")
                _AnswerDepartmentCode = departmentCode;
            // 過去トラ観たか回答情報取得
            getMitakaAnswerData();

        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <param name="systemNo">回答対象システムNo</param>
        /// <param name="departmentCode">回答対象部署コード</param>
        public MitakaAnswerData(IDAMitakaData dbMitakaData,string manageNo, int? systemNo = null, string departmentCode = "")
        {
            // 初期値セット
            if(dbMitakaData != null) _DbMitakaData = dbMitakaData;
            _ManageNo = manageNo;
            if (systemNo != null) _AnswerSystemNo = systemNo;
            if (departmentCode != null && departmentCode != "")
                _AnswerDepartmentCode = departmentCode;
            // 過去トラ観たか回答情報取得
            getMitakaAnswerData();
        }

        #endregion

        #region メソッド
        /// <summary>
        /// 過去トラ観たか回答情報初期化
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public void initMitakaAnswerData()
        {
            // 過去トラ観たか回答情報取得
            _MitakaAnswerData = _DbMitakaData.getMitakaAnswerData();
            // 編集フラグフィールドにfalseをセット
            _EditFlg = false;
        }

        /// <summary>
        /// 過去トラ観たか回答項目チェック
        /// </summary>
        /// <param name="pattern">処理区分</param>
        /// <returns>チェック結果</returns>
        public Boolean checkMitakaAnswerData(string pattern)
        {
            // 処理区分が「登録・更新」の場合
            if (pattern == "1")
            {
                if (_MitakaAnswerData.Rows.Count == 0)
                {
                    // 上記該当項目がある場合、チェックNGを返却
                    _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_ANSWER_POST_FAILURE);
                    return false;
                }

                if (_MitakaAnswerData.AsEnumerable().Any(row =>
                 (row.IsNull("MITAKA_NO")|| row["MITAKA_NO"].ToString() == "" ||
                 row.IsNull("SYSTEM_NO")||
                 row.IsNull("ANSWER_CONTENT") || row["ANSWER_CONTENT"].ToString() == "")
                 && row["EDIT_FLG"].ToString() == "1"))
                {

                    // 上記該当項目がある場合、チェックNGを返却
                    _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_ANSWER_POST_FAILURE);
                    return false;
                }
            }
            else if (pattern == "2") // 処理区分が「削除」の場合
            {
                if (_ManageNo == null || _ManageNo == "")
                {
                    // 上記該当項目がある場合、チェックNGを返却
                    _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_ANSWER_DELETE_FAILURE);
                    return false;
                }
            }
            else
                // チェック区分が例外の為、チェックNGを返却
                return false;
            
            // チェックOKを返却
            return true;
        }

        /// <summary>
        /// 過去トラ観たか回答情報取得
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public void getMitakaAnswerData()
        {
            // 過去トラ観たか回答情報取得
            _MitakaAnswerData = _DbMitakaData.getMitakaAnswerData(_ManageNo);
            // 編集フラグフィールドにfalseをセット
            _EditFlg = false;
        }

        /// <summary>
        /// 過去トラ観たか回答情報展開
        /// </summary>
        /// <param name="deployData">展開対象フィールド行</param>
        /// <returns>展開結果</returns>
        public Boolean deployMitakaAnswerData(DataRow deployData)
        {
            try
            {
                // 展開対象フィールド件数が1件以上無い場合
                if (deployData == null || deployData.ToString() == "")
                    return false;

                // 過去トラ観たか回答フィールド ＝ NULLの場合
                if (_MitakaAnswerData == null)
                    // 過去トラ観たか回答情報取得
                    getMitakaAnswerData();

                // 展開対象フィールド行．設計部署、展開対象フィールド行．評価部署で部署リスト作成
                List<string> dpList = new List<string>();
                for (int i = 0; i < deployData.ItemArray.Length; i++)
                    if (deployData.Table.Columns[i].ToString().StartsWith("BUSYO_"))
                        if (deployData.ItemArray[i] != null || deployData.ItemArray[i].ToString() != "")
                            dpList.Add(deployData.ItemArray[i].ToString());

                // 部署リストでループ
                for (int i = 0; i < dpList.Count; i++)
                    // 対象行が過去トラ観たか回答フィールドに存在しない場合
                    if (!_MitakaAnswerData.AsEnumerable().Any(row => row["ANSWER_DIVISION_CODE"].ToString() == dpList[i]
                        && row["SYSTEM_NO"] == deployData["ANSWER.SYSTEM_NO"]))
                    {
                        var dr = _MitakaAnswerData.NewRow();
                        dr["MITAKA_NO"] = deployData["MITAKA_NO"];
                        dr["SYSTEM_NO"] = deployData["SYSTEM_NO"];
                        dr["ANSWER_DIVISION_CODE"] = dpList[i];
                        dr["EDIT_FLG"] = "1";
                        _MitakaAnswerData.Rows.Add(dr);
                    }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 過去トラ観たか回答情報登録・更新
        /// </summary>
        /// <param name=""></param>
        /// <returns>登録・更新結果</returns>
        public Boolean postMitakaAnswerData()
        {
            // 管理番号が取得された際の設定
            for (int i = 0; i < _MitakaAnswerData.Rows.Count; i++)
                if (_MitakaAnswerData.Rows[i].IsNull("MITAKA_NO") ||
                    (_MitakaAnswerData.Rows[i]["MITAKA_NO"].ToString() == "" ))
                    if((_ManageNo != null) && (_ManageNo != "") )
                    _MitakaAnswerData.Rows[i]["MITAKA_NO"] = _ManageNo;

            // 過去トラ観たか回答項目チェック
            if (!checkMitakaAnswerData("1"))
                return false;

            // 過去トラ観たか回答情報テーブル登録・更新
            // 過去トラ観たか回答情報テーブル登録・更新の登録・更新結果が「登録・更新NG」の場合
            if (!postMitakaAnswerTableData())
            {
                // アラートメッセージ出力スクリプト取得
                _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_ANSWER_POST_FAILURE);
                return false;
            }

            // 過去トラ観たか回答情報テーブル登録・更新の登録・更新結果が「登録・更新OK」の場合
            // アラートメッセージ出力スクリプト取得
            _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_ANSWER_POST_SUCCESS);
            return true;
        }

        /// <summary>
        /// 過去トラ観たか情報削除
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>削除結果</returns>
        public Boolean deleteMitakaAnswerData()
        {
            // 過去トラ観たか回答項目チェック

            if (!checkMitakaAnswerData("2"))
                return false;

            // 過去トラ観たか回答情報テーブル削除

            // 過去トラ観たか回答情報テーブル削除の削除結果が「削除NG」の場合
            if (!deleteMitakaAnswerTableData())
            {
                // アラートメッセージ出力スクリプト取得
                _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_ANSWER_DELETE_FAILURE);
                return false;
            }

            // 過去トラ観たか回答情報テーブル削除の削除結果が「削除OK」の場合
            // アラートメッセージ出力スクリプト取得
            _embeddedScript = getScriptForAlertMessage(MessageConst.MITAKA_ANSWER_DELETE_SUCCESS);

            return true;
        }

        /// <summary>
        /// 過去トラ観たかテーブル回答情報登録
        /// </summary>
        /// <param name=""></param>
        /// <returns>登録結果</returns>
        private Boolean postMitakaAnswerTableData()
        {
            // 過去トラ観たか回答登録・更新・削除

            // 過去トラ観たか回答登録・更新・削除が「NG」の場合
            if (!_DbMitakaData.postMitakaAnswerData(_MitakaAnswerData))
            {
                // DAMitakaDataインスタンスフィールド.エラーメッセージをデータ処理結果フィールドに追加
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                return false;
            }
            else // 過去トラ観たか回答登録・更新・削除が「OK」の場合
                return true;
        }

        /// <summary>
        /// 過去トラ観たか回答テーブル情報削除
        /// </summary>
        /// <param name=""></param>
        /// <returns>削除結果</returns>
        public Boolean deleteMitakaAnswerTableData()
        {

            // 過去トラ観たか回答削除

            // 過去トラ観たか回答削除が「NG」の場合
            if (!_DbMitakaData.deleteMitakaAnswerData(_ManageNo))
            {
                // DAMitakaDataインスタンスフィールド.エラーメッセージをデータ処理結果フィールドに追加
                _DataProcessResult = _DbMitakaData.ErrorMessage;
                return false;
            }
            else // 過去トラ観たか回答削除が「OK」の場合
                return true;
        }

        #endregion

    }
}