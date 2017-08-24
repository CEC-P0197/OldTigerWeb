using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using OldTigerWeb.BuisinessLogic;

namespace OldTigerWeb.DataAccess
{
    public class DAMitakaData:IDAMitakaData
    {
        #region フィールド
        /// <summary>
        /// 処理日時フィールド
        /// </summary>
        private DateTime _ProccessDateTime;
        /// <summary>
        /// 処理判定フィールド
        /// </summary>
        private bool _ProccessSucccess;
        /// <summary>
        /// エラーメッセージフィールド
        /// </summary>
        private List<string> _ErrorMessage;
        /// <summary>
        /// ユーザーIDフィールド
        /// </summary>
        private string _userId;
        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DAMitakaData(string userId = null)
        {
            try
            {

                if (userId == null)
                {
                    _userId = new CommonLogic().GetWindowsUser();
                }
                else
                {
                    _userId = userId;
                }
                _ProccessSucccess = true;
                _ProccessDateTime = DateTime.Now;
                _ErrorMessage = new List<string>();
            }
            catch
            {
                _ProccessSucccess = false;
            }
        }
        #endregion

        #region プロパティ
        /// <summary>
        /// 処理判定
        /// </summary>
        public bool ProccessSuccess
        {
            get { return _ProccessSucccess; }
        }
        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public List<string> ErrorMessage
        {
            get { return _ErrorMessage; }
        }
        #endregion

        #region メソッド

        #region 管理番号リスト取得関連

        /// <summary>
        /// 最新管理番号取得
        /// </summary>
        /// <param name="divisionCode">課コード</param>
        /// <returns>最新管理番号</returns>
        public string getMaxManageNo(string divisionCode)
        {
            StringBuilder sb = new StringBuilder();
            string manageNo = "";
            try
            {
                // 課コード-システム処理日
                string divisionCodeProccessDateTime =
                string.Format(Def.DivisionDateTime, divisionCode, _ProccessDateTime.ToString("yyyyMMdd"));

            // 課コード+"-"+システム処理日+"-"+ローカル変数

                sb.AppendLine("SELECT ");
                sb.AppendLine("MITAKA_NO");
                sb.AppendLine("FROM T_MITAKA_HEADER ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO LIKE '@manageNo%' ");
                sb.AppendLine("ORDER BY MITAKA_NO DESC");
                sb = sb.Replace("@manageNo", string.Format("{0}-{1}", 
                    divisionCode, _ProccessDateTime.ToString("yyyyMMdd")));

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                // 最新管理番号作成
                if (dt.Rows.Count > 0)
                {
                    var manageNoSp =　dt.Rows[0]["MITAKA_NO"].ToString().Split('-');

                    manageNo =
                        string.Format(Def.DivisionDateTimeVariable,
                        divisionCode, _ProccessDateTime.ToString("yyyyMMdd"),
                        (int.Parse(manageNoSp.Last()) + 1).ToString("000"));
                }
                else if (dt.Rows.Count == 0)
                {
                    manageNo =
                        string.Format(Def.DivisionDateTimeVariable,
                        divisionCode, _ProccessDateTime.ToString("yyyyMMdd"), "001");
                }

                _ProccessSucccess = true;
                return manageNo;
            }
            catch(Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return manageNo;
            }
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="parent">過去トラ観たか情報検索クラス</param>
        /// <returns>管理番号リスト</returns>
        public DataTable getManageNoFromMitakaSearchData(MitakaSearchData parent)
        {
            StringBuilder sb = new StringBuilder();
            List<string> joinList = new List<string>();
            List<string> whereList = new List<string>();

            try
            {
                // 過去トラ観たか情報検索クラス.タイトル ≠ ""の場合
                if (parent.Title != "")
                {
                    whereList.Add("HEAD.TITLE LIKE '%@title%' ");
                }

                // 過去トラ観たか情報検索クラス.管理番号 ≠ ""の場合
                if (parent.ManageNo != "")
                {
                    whereList.Add("HEAD.MITAKA_NO = '@manageNo' ");
                }

                // 過去トラ観たか情報検索クラス.作成部署コード ≠ ""の場合
                if (parent.CreateDepartmentCode != "")
                {
                    whereList.Add("(HEAD.MANAGE_DIVISION_CODE1 LIKE '@createDivisionCode%' "+
                    "OR HEAD.MANAGE_DIVISION_CODE2 LIKE '@createDivisionCode%') ");
                }

                // 過去トラ観たか情報検索クラス.回答期間（カラ） ≠ DateTime.最少値の場合
                if (parent.AnswerStartDateTime != DateTime.MinValue)
                {
                    whereList.Add("(HEAD.START_YMD >= '@startDateTime' "+
                    "OR HEAD.END_YMD >= '@startDateTime') ");
                }

                // 過去トラ観たか情報検索クラス.回答期間（マデ） ≠ DateTime.最大値の場合
                if (parent.AnswerEndDateTime != DateTime.MaxValue)
                {
                    whereList.Add("(HEAD.START_YMD <= '@endDateTime' "+
                    "OR HEAD.END_YMD <= '@endDateTime') ");
                }

                // 過去トラ観たか情報検索クラス.状況 ≠ ""の場合
                if (parent.Status != "")
                {
                    whereList.Add("HEAD.STATUS = '@status' ");
                }

                // 過去トラ観たか情報検索クラス.回答区分 ≠ ""、又は、
                //   過去トラ観たか情報検索クラス.回答対象部署コード ≠ ""の場合
                if (parent.AnswerPattern != "" || parent.AnswerDepartmentCode != "")
                {
                    joinList.Add("INNER JOIN T_MITAKA_ANSWER ANSWER ");
                    joinList.Add("ON HEAD.MITAKA_NO = ANSWER.MITAKA_NO ");

                    // 過去トラ観たか情報検索クラス.回答区分 ≠ ""の場合
                    if (parent.AnswerPattern != "")
                    {
                        // 回答区分＝1(未回答有)の場合
                        if (parent.AnswerPattern == "1")
                        {
                            whereList.Add("ANSWER.STATUS = '' ");
                        }
                        // 回答区分＝2(回答済)の場合
                        if (parent.AnswerPattern == "2")
                        {
                            whereList.Add("ANSWER.STATUS <> '' ");
                        }
                        // 過去トラ観たか情報検索クラス.回答対象部署コード ≠ ""の場合
                        if (parent.AnswerDepartmentCode != "")
                        {
                            whereList.Add("ANSWER.ANSWER_DEVISION_CODE LIKE '@answerDivisionCode%' ");
                        }
                    }
                }

                // 過去トラ観たか情報検索クラス.開発符号 ≠ ""の場合
                if (parent.DevelopmentCode != "")
                {
                    joinList.Add("INNER JOIN T_MITAKA_DEVELOPMENTSIGN DEVELOP ");
                    joinList.Add("ON HEAD.MITAKA_NO = DEVELOP.MITAKA_NO ");

                    whereList.Add("DEVELOP.DEVELOPMENT_CODE LIKE '@developmentCode%' ");
                }

                // 過去トラ観たか情報検索クラス.機種 ≠ ""の場合
                if (parent.Model != "")
                {
                    joinList.Add("INNER JOIN T_MITAKA_MODEL MODEL ");
                    joinList.Add("ON HEAD.MITAKA_NO = MODEL.MITAKA_NO ");

                    whereList.Add("MODEL.MODEL LIKE '@model%' ");
                }

                // 過去トラ観たか情報検索クラス.BLK No ≠ ""の場合
                if (parent.BlockNo != "")
                {
                    joinList.Add("INNER JOIN T_MITAKA_BLK BLK ");
                    joinList.Add("ON HEAD.MITAKA_NO = BLK.MITAKA_NO ");

                    whereList.Add("BLK.BLK_No LIKE '@blockNo%' ");
                }

                // 過去トラ観たか情報検索クラス.タイトル品番 ≠ ""の場合
                if (parent.TitleDrawingNo != "")
                {
                    joinList.Add("INNER JOIN T_MITAKA_DRAWING DRAWING ");
                    joinList.Add("ON HEAD.MITAKA_NO = DRAWING.MITAKA_NO ");

                    whereList.Add("DRAWING.TITLE_DRAWING_NO LIKE '@titleDrawingNo%' ");
                }

                // 過去トラ観たか情報検索クラス.設通番号 ≠ ""の場合
                if (parent.EcsNo != "")
                {
                    joinList.Add("INNER JOIN T_MITAKA_ECS ECS ");
                    joinList.Add("ON HEAD.MITAKA_NO = ECS.MITAKA_NO ");

                    whereList.Add("ECS.ECS_NO LIKE '@ecsNo%' ");
                }

                // SQLクエリへJOIN、WHEREを追記する
                sb.AppendLine("SELECT ");
                sb.AppendLine("HEAD.MITAKA_NO");
                sb.AppendLine("FROM ");
                sb.AppendLine("T_MITAKA_HEADER HEAD ");

                for (int i = 0; i < joinList.Count; i++)
                {
                    sb.AppendLine(joinList[i]);
                }

                if (whereList.Count > 0)
                {
                    sb.AppendLine("WHERE ");

                    for (int i = 0; i < whereList.Count; i++)
                    {
                        if (i < whereList.Count - 1)
                        {
                            sb.AppendLine(whereList[i] + "AND");
                        }
                        else if (i == whereList.Count - 1)
                        {
                            sb.AppendLine(whereList[i]);
                        }
                    }
                }
                sb.AppendLine("ORDER BY HEAD.MITAKA_NO");

                // 検索条件の設定
                sb = sb
                    .Replace("@title", parent.Title)
                    .Replace("@manageNo", parent.ManageNo)
                    .Replace("@createDivisionCode", parent.CreateDepartmentCode)
                    .Replace("@startDateTime", parent.AnswerStartDateTime
                    .TimeStartDefault().ToString("yyyy-MM-dd hh:mm:ss"))
                    .Replace("@endDateTime", parent.AnswerEndDateTime
                    .TimeEndDefault().ToString("yyyy-MM-dd hh:mm:ss"))
                    .Replace("@status", parent.Status)
                    .Replace("@answerDivisionCode", parent.AnswerDepartmentCode)
                    .Replace("@developmentCode", parent.DevelopmentCode)
                    .Replace("@model", parent.Model)
                    .Replace("@blockNo", parent.BlockNo)
                    .Replace("@titleDrawingNo", parent.TitleDrawingNo)
                    .Replace("@ecsNo", parent.EcsNo);

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return dt;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return new DataTable();
            }
        }

        /// <summary>
        /// 管理番号リスト取得（所有）
        /// </summary>
        /// <returns>管理番号リスト</returns>
        public DataTable getManageNoFromRelationUser()
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("SELECT DISTINCT ");
                sb.AppendLine("HEAD.MITAKA_NO ");
                sb.AppendLine("FROM ");
                sb.AppendLine("(SELECT ");
                sb.AppendLine("HEAD.MITAKA_NO,HEAD.MANAGE_DIVISION_CODE1,HEAD.MANAGE_DIVISION_CODE2,");
                sb.AppendLine("RELATION.USER_ID ");
                sb.AppendLine("FROM ");
                sb.AppendLine("T_MITAKA_HEADER HEAD ");
                sb.AppendLine("INNER JOIN T_MITAKA_RELATION_USER RELATION ");
                sb.AppendLine("ON HEAD.MITAKA_NO = RELATION.MITAKA_NO ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("RELATION.RELATION_TYPE IN ('10','11','20')) HEAD ");
                sb.AppendLine("INNER JOIN (SELECT USER_ID,KA_CODE FROM M_USER WHERE USER_ID = '@userId') US ");
                sb.AppendLine("ON HEAD.MANAGE_DIVISION_CODE1 = US.KA_CODE OR ");
                sb.AppendLine("HEAD.MANAGE_DIVISION_CODE2 = US.KA_CODE OR ");
                sb.AppendLine("HEAD.USER_ID = US.USER_ID ");
                sb.AppendLine("ORDER BY HEAD.MITAKA_NO ");
                sb = sb.Replace("@userId", _userId);

                // SQL実行
                    DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return dt;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return new DataTable();
            }
        }
        #endregion

        #region 過去トラ観たかヘッダー関連
        /// <summary>
        /// 過去トラ観たかヘッダー取得
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>過去トラ観たかヘッダー</returns>
        public DataTable getMitakaHeaderData(string manageNo = null)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("SELECT ");
                sb.AppendLine("MITAKA_NO,MANAGE_DIVISION_CODE1,MANAGE_DIVISION_CODE2, ");
                sb.AppendLine("TITLE,PURPOSE,COMMENT,START_YMD,END_YMD,STATUS,STATUS_COMMENT, ");
                sb.AppendLine("INSERT_USER,UPDATE_USER,INSERT_YMD,UPDATE_YMD, ");
                sb.AppendLine("'' AS EDIT_FLG ");
                sb.AppendLine("FROM T_MITAKA_HEADER ");
                sb.AppendLine("WHERE ");

                if (manageNo != null)
                {
                    sb.AppendLine("MITAKA_NO = '@manageNo' ");
                }
                else
                {
                    sb.AppendLine("MITAKA_NO = null ");
                }
                
                sb.AppendLine("ORDER BY MITAKA_NO ");

                sb = sb.Replace("@manageNo", manageNo);

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                DataColumn[] stringColumns = dt.Columns.Cast<DataColumn>()
                    .Where(c => c.DataType == typeof(string))
                    .ToArray();

                foreach (DataRow row in dt.Rows)
                    foreach (DataColumn col in stringColumns)
                    {
                        if (row.Field<string>(col) == null) continue;
                        row.SetField<string>(col, row.Field<string>(col).Trim());
                    }

                _ProccessSucccess = true;
                return dt;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return new DataTable();
            }
        }

        /// <summary>
        /// 過去トラ観たかヘッダー存在チェック
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>存在判定結果</returns>
        public Boolean existMitakaHeaderData(string manageNo)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("SELECT COUNT(MITAKA_NO) AS COUNT ");
                sb.AppendLine("FROM T_MITAKA_HEADER ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' ");
                sb = sb.Replace("@manageNo", manageNo);

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                if (dt.Rows.Count >= 1)
                {
                    if((int)dt.Rows[0]["COUNT"]> 0)
                    {
                        _ProccessSucccess = true;
                        return true;
                    }
                    else
                    {
                        _ProccessSucccess = true;
                        return false;
                    }
                }
                else
                {
                    _ProccessSucccess = true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 過去トラ観たかヘッダー登録
        /// </summary>
        /// <param name="param">過去トラ観たかヘッダーフィールド</param>
        /// <returns>登録結果</returns>
        public Boolean insertMitakaHeaderData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("INSERT T_MITAKA_HEADER( ");
                sb.AppendLine("MITAKA_NO,MANAGE_DIVISION_CODE1,MANAGE_DIVISION_CODE2, ");
                sb.AppendLine("TITLE,PURPOSE,COMMENT,START_YMD,END_YMD,STATUS,STATUS_COMMENT, ");
                sb.AppendLine("INSERT_USER,UPDATE_USER,INSERT_YMD,UPDATE_YMD) ");
                sb.AppendLine("VALUES( ");
                sb.AppendLine("'@manageNo', '@manageDivisionCode1', '@manageDivisionCode2', ");
                sb.AppendLine("'@title', '@purpose', '@comment', '@answerStartDate',");
                sb.AppendLine("'@answerEndDate', '@status', '', ");
                sb.AppendLine("'@userId', '@userId','@nowDateTime', '@nowDateTime') ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@manageDivisionCode1", param["MANAGE_DIVISION_CODE1"].ToString());
                sb = sb.Replace("@manageDivisionCode2", param["MANAGE_DIVISION_CODE2"].ToString());
                sb = sb.Replace("@title", param["TITLE"].ToString());
                sb = sb.Replace("@purpose", param["PURPOSE"].ToString());
                sb = sb.Replace("@comment", param["COMMENT"].ToString());
                sb = sb.Replace("@answerStartDate", Def.SQL_DATETIME_MIN);
                sb = sb.Replace("@answerEndDate", DateTime.Parse(param["END_YMD"].ToString()).ToString("yyyy-MM-dd hh:mm:ss.fff"));
                sb = sb.Replace("@status", Def.MITAKA_STATUS_PREPARATION);
                sb = sb.Replace("@userId", _userId);
                sb = sb.Replace("@nowDateTime", _ProccessDateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"));

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 過去トラ観たかヘッダー更新
        /// </summary>
        /// <param name="param">過去トラ観たかヘッダーフィールド</param>
        /// <returns>更新結果</returns>
        public Boolean updateMitakaHeaderData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("UPDATE ");
                sb.AppendLine("T_MITAKA_HEADER ");
                sb.AppendLine("SET ");
                sb.AppendLine("MANAGE_DIVISION_CODE1 = '@manageDivisionCode1', ");
                sb.AppendLine("MANAGE_DIVISION_CODE2 = '@manageDivisionCode2', ");
                sb.AppendLine("TITLE = '@title', ");
                sb.AppendLine("PURPOSE = '@purpose', ");
                sb.AppendLine("COMMENT = '@comment', ");
                sb.AppendLine("START_YMD = '@answerStartDate', ");
                sb.AppendLine("END_YMD = '@answerEndDate', ");
                sb.AppendLine("STATUS = '@status', ");
                sb.AppendLine("STATUS_COMMENT = '@statComments', ");
                sb.AppendLine("UPDATE_USER = '@userId', ");
                sb.AppendLine("UPDATE_YMD = '@nowDateTime'");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' ");
                sb = sb.Replace("@manageDivisionCode1", param["MANAGE_DIVISION_CODE1"].ToString());
                sb = sb.Replace("@manageDivisionCode2", param["MANAGE_DIVISION_CODE2"].ToString());
                sb = sb.Replace("@title", param["TITLE"].ToString());
                sb = sb.Replace("@purpose", param["PURPOSE"].ToString());
                sb = sb.Replace("@comment", param["COMMENT"].ToString());

                DateTime startDate;
                if (DateTime.TryParse(param["START_YMD"].ToString(),out startDate))
                {
                    sb = sb.Replace("@answerStartDate", DateTime.Parse(param["START_YMD"].ToString()).ToString("yyyy-MM-dd hh:mm:ss.fff"));
                }
                else
                {
                    sb = sb.Replace("@answerStartDate", Def.SQL_DATETIME_MIN);
                }
                DateTime endDate;
                if (DateTime.TryParse(param["END_YMD"].ToString(), out endDate))
                {
                    sb = sb.Replace("@answerEndDate", DateTime.Parse(param["END_YMD"].ToString()).ToString("yyyy-MM-dd hh:mm:ss.fff"));
                }
                else
                {
                    sb = sb.Replace("@answerEndDate", Def.SQL_DATETIME_MAX);
                }

                sb = sb.Replace("@status", param["STATUS"].ToString());
                sb = sb.Replace("@statComments", param["STATUS_COMMENT"].ToString());
                sb = sb.Replace("@userId", _userId);
                sb = sb.Replace("@nowDateTime", _ProccessDateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"));
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 過去トラ観たかヘッダーステータス更新
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <param name="status">状況</param>
        /// <param name="reason">理由</param>
        /// <returns>更新結果(更新OK：True、更新NG：False</returns>
        public Boolean updateHeaderDataToStatus(string manageNo, string status,string reason = null)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("UPDATE");
                sb.AppendLine("T_MITAKA_HEADER ");
                sb.AppendLine("SET ");
                if (status == Def.MITAKA_STATUS_ANSWER)
                {
                    sb.AppendLine("START_YMD = '@nowDateTime', ");
                }
                sb.AppendLine("STATUS = '@status', ");

                if (reason != null)
                {
                    sb.AppendLine("STATUS_COMMENT = '@reason', ");
                }

                sb.AppendLine("UPDATE_USER = '@userId', ");
                sb.AppendLine("UPDATE_YMD = '@nowDateTime'");
                sb.AppendLine("WHERE");
                sb.AppendLine("MITAKA_NO = '@manageNo' ");

                sb = sb.Replace("@manageNo", manageNo);
                sb = sb.Replace("@status", status);
                sb = sb.Replace("@reason", reason);
                sb = sb.Replace("@userId", _userId);
                sb = sb.Replace("@nowDateTime", _ProccessDateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"));

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 過去トラ観たかヘッダー削除
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>削除結果</returns>
        public Boolean deleteMitakaHeaderData(string manageNo)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                // 過去トラ観たかヘッダー存在チェック

                if (!existMitakaHeaderData(manageNo))
                {
                    _ProccessSucccess = true;
                    return false;
                }

                sb.AppendLine("DELETE FROM ");
                sb.AppendLine("T_MITAKA_HEADER ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' ");
                sb = sb.Replace("@manageNo", manageNo);

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 過去トラ観たかヘッダー登録・更新
        /// </summary>
        /// <param name="param">過去トラ観たかヘッダーフィールド</param>
        /// <returns>登録・更新結果</returns>
        public Boolean postMitakaHeaderData(DataTable param)
        {
            try
            {
                // 過去トラ観たかヘッダーフィールド件数が1件以上無い場合
                if (param.Rows.Count == 0)
                {
                    _ProccessSucccess = false;  
                    return false;
                }
                for (int i = 0; i < param.Rows.Count; i++)
                {
                    if (param.Rows[i]["EDIT_FLG"].ToString() != "1")
                    {
                        continue;
                    }

                    // 過去トラ観たかヘッダー存在チェック
                    // 過去トラ観たかヘッダー存在チェックが「データ無し」の場合
                    if (!existMitakaHeaderData(param.Rows[i]["MITAKA_NO"].ToString()))
                    {
                        // 過去トラ観たかヘッダー登録
                        if (!insertMitakaHeaderData(param.Rows[i]))
                        {
                            _ProccessSucccess = false;
                            return false;
                        }
                    }
                    else // 過去トラ観たかヘッダー存在チェックが「データ有り」の場合
                    {
                        // 過去トラ観たかヘッダー更新
                        if (!updateMitakaHeaderData(param.Rows[i]))
                        {
                            _ProccessSucccess = false;
                            return false;
                        }
                    }
                }
                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        #endregion

        #region 関連ユーザー情報関連
        /// <summary>
        /// 関連ユーザー情報取得
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>関連ユーザー情報</returns>
        public DataTable getReLationUserData(string manageNo = null)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("SELECT ");
                sb.AppendLine("RELATION.MITAKA_NO,RELATION.RELATION_TYPE,RELATION.USER_ID, ");
                sb.AppendLine("RELATION.INSERT_USER,RELATION.UPDATE_USER,RELATION.INSERT_YMD,");
                sb.AppendLine("RELATION.UPDATE_YMD,'' AS EDIT_FLG, ");
                sb.AppendLine("US.USER_NAME,US.MAIL,US.BY_PU,US.BU_CODE,");
                sb.AppendLine("US.KA_CODE,US.SQB_FLG");
                sb.AppendLine("FROM ");
                sb.AppendLine("T_MITAKA_RELATION_USER RELATION ");
                sb.AppendLine("LEFT JOIN M_USER US ");
                sb.AppendLine("ON RELATION.USER_ID = US.USER_ID ");
                sb.AppendLine("WHERE ");

                if (manageNo != null)
                {
                    sb.AppendLine("RELATION.MITAKA_NO = '@manageNo' ");
                    sb = sb.Replace("@manageNo",manageNo);
                }
                else
                {
                    sb.AppendLine("RELATION.MITAKA_NO = null ");
                }

                sb.AppendLine("ORDER BY RELATION.MITAKA_NO,RELATION.INSERT_YMD,RELATION.RELATION_TYPE ");

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                DataColumn[] stringColumns = dt.Columns.Cast<DataColumn>()
                    .Where(c => c.DataType == typeof(string))
                    .ToArray();

                foreach (DataRow row in dt.Rows)
                    foreach (DataColumn col in stringColumns)
                    {
                        if (row.Field<string>(col) == null) continue;
                        row.SetField<string>(col, row.Field<string>(col).Trim());
                    }

                _ProccessSucccess = true;
                return dt;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return new DataTable();
            }
        }

        /// <summary>
        /// 関連ユーザー情報存在チェック
        /// </summary>
        /// <param name="param">関連ユーザー情報フィールド行</param>
        /// <returns>存在判定結果</returns>
        public Boolean existReLationUserData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("SELECT COUNT(MITAKA_NO) AS COUNT ");
                sb.AppendLine("FROM T_MITAKA_RELATION_USER ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' AND ");
                sb.AppendLine("RELATION_TYPE = '@type' AND ");
                sb.AppendLine("USER_ID = '@relationUserId' ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@type", param["RELATION_TYPE"].ToString());
                sb = sb.Replace("@relationUserId", param["USER_ID"].ToString());

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                if (dt.Rows.Count >= 1)
                {
                    if ((int)dt.Rows[0]["COUNT"] > 0)
                    {
                        _ProccessSucccess = true;
                        return true;
                    }
                    else
                    {
                        _ProccessSucccess = true;
                        return false;
                    }
                }
                else
                {
                    _ProccessSucccess = true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 関連ユーザー情報登録
        /// </summary>
        /// <param name="param">関連ユーザー情報フィールド行</param>
        /// <returns>登録結果</returns>
        public Boolean insertReLationUserData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("INSERT ");
                sb.AppendLine("T_MITAKA_RELATION_USER( ");
                sb.AppendLine("MITAKA_NO,RELATION_TYPE,USER_ID, ");
                sb.AppendLine("INSERT_USER,UPDATE_USER,INSERT_YMD,UPDATE_YMD) ");
                sb.AppendLine("VALUES( ");
                sb.AppendLine("'@manageNo', '@type', '@relationUserId', ");
                sb.AppendLine("'@userId', '@userId', '@nowDateTime', '@nowDateTime') ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@type", param["RELATION_TYPE"].ToString());
                sb = sb.Replace("@relationUserId", param["USER_ID"].ToString());
                sb = sb.Replace("@userId", _userId);
                sb = sb.Replace("@nowDateTime", _ProccessDateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"));

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 関連ユーザー情報削除（ユーザ単位）
        /// </summary>
        /// <param name="param">関連ユーザー情報フィールド行</param>
        /// <returns>削除結果</returns>
        public Boolean deleteReLationUserData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                // 関連ユーザー情報削除対象存在チェック
                if (!existReLationUserData(param))
                {
                    _ProccessSucccess = true;
                    return false;
                }

                sb.AppendLine("DELETE FROM ");
                sb.AppendLine("T_MITAKA_RELATION_USER ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' AND ");
                sb.AppendLine("RELATION_TYPE = '@type' AND ");
                sb.AppendLine("USER_ID = '@relationUserId' ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@type", param["RELATION_TYPE"].ToString());
                sb = sb.Replace("@relationUserId", param["USER_ID"].ToString());

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 関連ユーザー情報削除（過去トラ観たか情報単位）
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>削除結果</returns>
        public Boolean deleteReLationUserData(string manageNo)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("DELETE FROM ");
                sb.AppendLine("T_MITAKA_RELATION_USER ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' ");
                sb = sb.Replace("@manageNo", manageNo);

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 関連ユーザー情報登録・削除
        /// </summary>
        /// <param name="param">関連ユーザー情報フィールド</param>
        /// <returns>登録・削除結果</returns>
        public Boolean postReLationUserData(DataTable param)
        {
            try
            {
                // 関連ユーザー情報フィールド件数が1件以上無い場合
                if (param.Rows.Count == 0)
                {
                    _ProccessSucccess = false;
                    return false;
                }
                // 関連ユーザー情報取得
                var dt = getReLationUserData(param.Rows[0]["MITAKA_NO"].ToString());

                // 取得した関連ユーザー情報でループ
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // 関連ユーザー情報フィールドに対象の関連ユーザー情報が存在しない場合
                    if (!param.AsEnumerable().Any(
                        row => row["MITAKA_NO"] == dt.Rows[i]["MITAKA_NO"]
                         && row["RELATION_TYPE"] == dt.Rows[i]["RELATION_TYPE"]
                         && row["USER_ID"] == dt.Rows[i]["USER_ID"]))
                    {
                        // 関連ユーザー情報削除
                        if (!deleteReLationUserData(dt.Rows[i]))
                        {
                            _ProccessSucccess = false;
                            return false;
                        }
                    }
                }

                // 関連ユーザー情報フィールドでループ
                for (int i = 0; i < param.Rows.Count; i++)
                {
                    if (param.Rows[i]["EDIT_FLG"].ToString() != "1")
                    {
                        continue;
                    }

                    // 関連ユーザー情報フィールド対象行がDBに存在しない場合
                    if (!dt.AsEnumerable().Any(row => row["MITAKA_NO"] == param.Rows[i]["MITAKA_NO"]
                             && row["RELATION_TYPE"] == param.Rows[i]["RELATION_TYPE"]
                             && row["USER_ID"] == param.Rows[i]["USER_ID"])
                            )
                    {
                        // 関連ユーザー情報登録
                        if (!insertReLationUserData(param.Rows[i]))
                        {
                            _ProccessSucccess = false;
                            return false;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        #endregion

        #region 検索条件関連
        /// <summary>
        /// 検索条件取得
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>検索条件</returns>
        public DataTable getSearchParameterData(string manageNo = null)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("SELECT ");
                sb.AppendLine("MITAKA_NO,GROUP_ID,SEARCH_TYPE,SEARCH_CLASS,SEARCH_PARAMETER, ");
                sb.AppendLine("INSERT_USER,UPDATE_USER,INSERT_YMD,UPDATE_YMD, ");
                sb.AppendLine("'' AS EDIT_FLG ");
                sb.AppendLine("FROM ");
                sb.AppendLine("T_MITAKA_SEARCH_GROUP ");
                sb.AppendLine("WHERE ");

                if (manageNo != null)
                {
                    sb.AppendLine("MITAKA_NO = '@manageNo'");
                    sb = sb.Replace("@manageNo", manageNo);
                }
                else
                {
                    sb.AppendLine("MITAKA_NO = null ");
                }

                sb.AppendLine("ORDER BY MITAKA_NO,INSERT_YMD ");

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                DataColumn[] stringColumns = dt.Columns.Cast<DataColumn>()
                    .Where(c => c.DataType == typeof(string))
                    .ToArray();

                foreach (DataRow row in dt.Rows)
                    foreach (DataColumn col in stringColumns)
                    {
                        if (row.Field<string>(col) == null) continue;
                        row.SetField<string>(col, row.Field<string>(col).Trim());
                    }

                _ProccessSucccess = true;
                return dt;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return new DataTable();
            }
        }

        /// <summary>
        /// 検索条件存在チェック
        /// </summary>
        /// <param name="param">検索条件フィールド行</param>
        /// <returns>存在判定結果</returns>
        public Boolean existSearchParameterData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("SELECT ");
                sb.AppendLine("COUNT(MITAKA_NO) AS COUNT ");
                sb.AppendLine("FROM ");
                sb.AppendLine("T_MITAKA_SEARCH_GROUP ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' AND ");
                sb.AppendLine("GROUP_ID = '@groupId' AND ");
                sb.AppendLine("SEARCH_TYPE = '@type' AND ");
                sb.AppendLine("SEARCH_CLASS = '@class' ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@groupId", param["GROUP_ID"].ToString());
                sb = sb.Replace("@type", param["SEARCH_TYPE"].ToString());
                sb = sb.Replace("@class", param["SEARCH_CLASS"].ToString());

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                if (dt.Rows.Count >= 1)
                {
                    if ((int)dt.Rows[0]["COUNT"] > 0)
                    {
                        _ProccessSucccess = true;
                        return true;
                    }
                    else
                    {
                        _ProccessSucccess = true;
                        return false;
                    }
                }
                else
                {
                    _ProccessSucccess = true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 検索条件登録
        /// </summary>
        /// <param name="param">検索条件フィールド行</param>
        /// <returns>登録結果</returns>
        public Boolean insertSearchParameterData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("INSERT ");
                sb.AppendLine("T_MITAKA_SEARCH_GROUP( ");
                sb.AppendLine("MITAKA_NO,GROUP_ID,SEARCH_TYPE,SEARCH_CLASS,SEARCH_PARAMETER, ");
                sb.AppendLine("INSERT_USER,UPDATE_USER,INSERT_YMD,UPDATE_YMD) ");
                sb.AppendLine("VALUES( ");
                sb.AppendLine("'@manageNo','@groupId', '@type', '@class', '@param',");
                sb.AppendLine("'@userId','@userId', '@nowDateTime', '@nowDateTime') ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@groupId", _ProccessDateTime.ToString("yyyyMMddhhmmss"));
                sb = sb.Replace("@type", param["SEARCH_TYPE"].ToString());
                sb = sb.Replace("@class", param["SEARCH_CLASS"].ToString());
                sb = sb.Replace("@param", param["SEARCH_PARAMETER"].ToString());
                sb = sb.Replace("@userId", _userId);
                sb = sb.Replace("@nowDateTime", _ProccessDateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"));

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 検索条件削除（検索条件単位）
        /// </summary>
        /// <param name="param">検索条件フィールド行</param>
        /// <returns>削除結果</returns>
        public Boolean deleteSearchParameterData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                // 検索条件削除対象存在チェック
                if (!existSearchParameterData(param))
                {
                    _ProccessSucccess = true;
                    return false;
                }

                sb.AppendLine("DELETE FROM ");
                sb.AppendLine("T_MITAKA_SEARCH_GROUP ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' AND ");
                sb.AppendLine("GROUP_ID = '@groupId' AND ");
                sb.AppendLine("SEARCH_TYPE = '@type' AND ");
                sb.AppendLine("SEARCH_CLASS = '@class' ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@groupId", param["GROUP_ID"].ToString());
                sb = sb.Replace("@type", param["SEARCH_TYPE"].ToString());
                sb = sb.Replace("@class", param["SEARCH_CLASS"].ToString());

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 検索条件削除（過去トラ観たか情報単位）
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>削除結果</returns>
        public Boolean deleteSearchParameterData(string manageNo)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("DELETE FROM ");
                sb.AppendLine("T_MITAKA_SEARCH_GROUP ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' ");
                sb = sb.Replace("@manageNo", manageNo);

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 検索条件登録・削除
        /// </summary>
        /// <param name="param">検索条件フィールド</param>
        /// <returns>登録・削除結果</returns>
        public Boolean postSearchParameterData(DataTable param)
        {
            try
            {
                // 検索条件フィールド件数が1件以上無い場合
                if (param.Rows.Count == 0)
                {
                    _ProccessSucccess = true;
                    return false;
                }
                // 検索条件取得
                var dt = getSearchParameterData(param.Rows[0]["MITAKA_NO"].ToString());

                // 取得した検索条件でループ
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string dtCount = "";
                    // 検索条件フィールドに対象の検索条件が存在しない場合
                    if (param.AsEnumerable().Any(
                        row => row["MITAKA_NO"] == dt.Rows[i]["MITAKA_NO"]
                        && row["GROUP_ID"] == dt.Rows[i]["GROUP_ID"]
                        && row["SEARCH_TYPE"] == dt.Rows[i]["SEARCH_TYPE"]
                        && row["SEARCH_CLASS"] == dt.Rows[i]["SEARCH_CLASS"]
                        ))
                    {
                        // 検索条件削除
                        if (!deleteSearchParameterData(dt.Rows[i]))
                        {
                            _ProccessSucccess = false;
                            return false;
                        }
                    }
                }

                // 検索条件フィールドでループ
                for (int i = 0; i < param.Rows.Count; i++)
                {
                    if (param.Rows[i]["EDIT_FLG"].ToString() != "1")
                    {
                        continue;
                    }
                    // 関連ユーザー情報フィールド対象行がDBに存在しない場合
                    if (!dt.AsEnumerable().Any(
                    row => row["MITAKA_NO"] == param.Rows[i]["MITAKA_NO"]
                    && row["GROUP_ID"] == param.Rows[i]["GROUP_ID"]
                    && row["SEARCH_TYPE"] == param.Rows[i]["SEARCH_TYPE"]
                    && row["SEARCH_CLASS"] == param.Rows[i]["SEARCH_CLASS"]
                    ))
                    {
                        // 検索条件登録
                        if (!insertSearchParameterData(param.Rows[i]))
                        {
                            _ProccessSucccess = false;
                            return false;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        #endregion

        #region 展開対象関連
        /// <summary>
        /// 展開対象情報取得
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>展開対象情報</returns>
        public DataTable getDeploymentTroubleData(string manageNo = null)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("SELECT ");
                sb.AppendLine("TARGET.MITAKA_NO,TARGET.SYSTEM_NO,TARGET.TARGET_FLG, ");
                sb.AppendLine("TARGET.INSERT_USER,TARGET.UPDATE_USER,TARGET.INSERT_YMD,TARGET.UPDATE_YMD, ");
                sb.AppendLine("TROUBLE.BY_PU,TROUBLE.KOUMOKU_KANRI_NO,TROUBLE.KOUMOKU,TROUBLE.RANK,TROUBLE.SAIHATU,TROUBLE.RSC, ");
                sb.AppendLine("CASE WHEN RTRIM(TROUBLE.BUHIN_NAME1) <> '' THEN RTRIM(TROUBLE.BUHIN_NAME1) ");
                sb.AppendLine("ELSE RTRIM(TROUBLE.BUHIN_NAME2) END AS BUHIN_NAME, ");
                sb.AppendLine("TROUBLE.FUGO_NAME1,TROUBLE.FUGO_NAME2,TROUBLE.FUGO_NAME3, ");
                sb.AppendLine("TROUBLE.FUGO_NAME4,TROUBLE.FUGO_NAME5, ");
                sb.AppendLine("TROUBLE.GENIN,TROUBLE.TAISAKU,TROUBLE.KAIHATU_MIHAKKEN_RIYU,TROUBLE.SQB_KANTEN,");
                sb.AppendLine("TROUBLE.SAIHATU_SEKKEI,TROUBLE.SAIHATU_HYOUKA,TROUBLE.SIRYOU_NO1,TROUBLE.SIRYOU_NO2, ");
                sb.AppendLine("TROUBLE.SETTU_NO1,TROUBLE.SETTU_NO2,TROUBLE.SETTU_NO3,TROUBLE.SETTU_NO4,SETTU_NO5, ");
                sb.AppendLine("TROUBLE.BUSYO_SEKKEI1,TROUBLE.BUSYO_SEKKEI2,TROUBLE.BUSYO_SEKKEI3,TROUBLE.BUSYO_SEKKEI4, ");
                sb.AppendLine("TROUBLE.BUSYO_SEKKEI5,TROUBLE.BUSYO_SEKKEI6,TROUBLE.BUSYO_SEKKEI7,TROUBLE.BUSYO_SEKKEI8, ");
                sb.AppendLine("TROUBLE.BUSYO_SEKKEI9,TROUBLE.BUSYO_SEKKEI10, ");
                sb.AppendLine("TROUBLE.BUSYO_HYOUKA1,TROUBLE.BUSYO_HYOUKA2,TROUBLE.BUSYO_HYOUKA3,TROUBLE.BUSYO_HYOUKA4, ");
                sb.AppendLine("TROUBLE.BUSYO_HYOUKA5,TROUBLE.BUSYO_HYOUKA6,TROUBLE.BUSYO_HYOUKA7,TROUBLE.BUSYO_HYOUKA8, ");
                sb.AppendLine("TROUBLE.BUSYO_HYOUKA9,TROUBLE.BUSYO_HYOUKA10, ");
                sb.AppendLine("'' AS EDIT_FLG ");
                sb.AppendLine("FROM ");
                sb.AppendLine("T_MITAKA_TROUBLE_DATA TARGET ");
                sb.AppendLine("LEFT JOIN T_TROUBLE_DATA TROUBLE ");
                sb.AppendLine("ON TARGET.SYSTEM_NO = TROUBLE.SYSTEM_NO ");
                sb.AppendLine("WHERE ");

                if (manageNo != null)
                {
                    sb.AppendLine("MITAKA_NO = '@manageNo' ");
                    sb = sb.Replace("@manageNo", manageNo);
                }
                else
                {
                    sb.AppendLine("MITAKA_NO = null ");
                }

                sb.AppendLine("ORDER BY MITAKA_NO,INSERT_YMD,SYSTEM_NO ");

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                DataColumn[] stringColumns = dt.Columns.Cast<DataColumn>()
                    .Where(c => c.DataType == typeof(string))
                    .ToArray();

                foreach (DataRow row in dt.Rows)
                    foreach (DataColumn col in stringColumns)
                    {
                        if (row.Field<string>(col) == null) continue;
                        row.SetField<string>(col, row.Field<string>(col).Trim());
                    }

                _ProccessSucccess = true;
                return dt;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return new DataTable();
            }
        }

        /// <summary>
        /// 展開対象情報存在チェック
        /// </summary>
        /// <param name="param">展開対象情報フィールド行</param>
        /// <returns>存在判定結果</returns>
        public Boolean existDeploymentTroubleData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("SELECT");
                sb.AppendLine("COUNT(MITAKA_NO) AS COUNT");
                sb.AppendLine("FROM ");
                sb.AppendLine("MITAKA_TROUBLE_DATA ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' AND ");
                sb.AppendLine("SYSTEM_NO = '@systemNo' ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@systemNo", param["SYSTEM_NO"].ToString());

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                if (dt.Rows.Count >= 1)
                {
                    if ((int)dt.Rows[0]["COUNT"] > 0)
                    {
                        _ProccessSucccess = true;
                        return true;
                    }
                    else
                    {
                        _ProccessSucccess = true;
                        return false;
                    }
                }
                else
                {
                    _ProccessSucccess = true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 展開対象情報登録
        /// </summary>
        /// <param name="param">展開対象情報フィールド行</param>
        /// <returns>登録結果</returns>
        public Boolean insertDeploymentTroubleData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("INSERT ");
                sb.AppendLine("T_MITAKA_TROUBLE_DATA( ");
                sb.AppendLine("MITAKA_NO,SYSTEM_NO,TARGET_FLG, ");
                sb.AppendLine("INSERT_USER,UPDATE_USER,INSERT_YMD,UPDATE_YMD) ");
                sb.AppendLine("VALUES( ");
                sb.AppendLine("'@manageNo', @systemNo, '@targetFlg', ");
                sb.AppendLine("'@userId', '@userId','@nowDateTime', '@nowDateTime') ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@systemNo", param["SYSTEM_NO"].ToString());
                sb = sb.Replace("@targetFlg", param["TARGET_FLG"].ToString());
                sb = sb.Replace("@userId", _userId);
                sb = sb.Replace("@nowDateTime", _ProccessDateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"));
                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch(Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 展開対象情報更新
        /// </summary>
        /// <param name="param">展開対象情報フィールド行</param>
        /// <returns>更新結果</returns>
        public Boolean updateDeploymentTroubleData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("UPDATE ");
                sb.AppendLine("T_MITAKA_TROUBLE_DATA ");
                sb.AppendLine("SET ");
                sb.AppendLine("TARGET_FLG = '@targetFlg', ");
                sb.AppendLine("UPDATE_USER = '@userId', ");
                sb.AppendLine("UPDATE_YMD = '@nowDateTime'");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' AND ");
                sb.AppendLine("SYSTEM_NO = '@systemNo' ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@systemNo", param["SYSTEM_NO"].ToString());
                sb = sb.Replace("@targetFlg", param["TARGET_FLG"].ToString());
                sb = sb.Replace("@userId", _userId);
                sb = sb.Replace("@nowDateTime", _ProccessDateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"));

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 展開対象情報削除（システムNo単位）
        /// </summary>
        /// <param name="param">展開対象情報フィールド行</param>
        /// <returns>削除結果</returns>
        public Boolean deleteDeploymentTroubleData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                // 展開対象情報削除対象存在チェック
                if (!existDeploymentTroubleData(param))
                {
                    _ProccessSucccess = true;
                    return false;
                }

                sb.AppendLine("DELETE FROM ");
                sb.AppendLine("T_MITAKA_TROUBLE_DATA ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' AND ");
                sb.AppendLine("SYSTEM_NO = '@systemNo' ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@systemNo", param["SYSTEM_NO"].ToString());

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 展開対象情報削除（過去トラ観たか情報単位）
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>削除結果</returns>
        public Boolean deleteDeploymentTroubleData(string manageNo)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("DELETE FROM ");
                sb.AppendLine("T_MITAKA_TROUBLE_DATA ");
                sb.AppendLine("WHERE  ");
                sb.AppendLine("MITAKA_NO = '@manageNo' ");
                sb = sb.Replace("@manageNo", manageNo);

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 展開対象情報登録・更新・削除
        /// </summary>
        /// <param name="param">展開対象情報フィールド</param>
        /// <returns>登録・更新・削除結果</returns>
        public Boolean postDeploymentTroubleData(DataTable param)
        {

            try
            {
                // 展開対象情報フィールド件数が1件以上無い場合
                if (param.Rows.Count == 0)
                {
                    _ProccessSucccess = false;
                    return false;
                }
                else
                {
                    // 展開対象情報取得
                    var dt = getDeploymentTroubleData(param.Rows[0]["MITAKA_NO"].ToString());

                    // 取得した展開対象情報でループ
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string dtCount = "";
                        dtCount = param.AsEnumerable().Where(row => row["MITAKA_NO"] == dt.Rows[i]["MITAKA_NO"]
                            && row["MITAKA_NO"] == dt.Rows[i]["MITAKA_NO"]).ToString();

                        // 展開対象対象フィールドに対象の検索条件が存在しない場合
                        if (dtCount.Count() == 0)
                        {

                            if (!deleteDeploymentTroubleData(dt.Rows[i]))
                            {
                                return false;
                            }
                        }
                    }

                    // 展開対象情報フィールドでループ
                    for (int i = 0; i < param.Rows.Count; i++)
                    {
                        if (param.Rows[i]["EDIT_FLG"].ToString() != "1")
                        {
                            continue;
                        }
                        // 展開対象情報登録対象存在チェック
                        // 展開対象情報存在チェックが「データ無し」の場合
                        if (!existDeploymentTroubleData(param.Rows[i]))
                        {
                            // 展開対象情報登録
                            if (!insertDeploymentTroubleData(param.Rows[i]))
                            {
                                _ProccessSucccess = false;
                                return false;
                            }
                        }
                        else // 展開対象情報存在チェックが「データ有り」の場合
                        {
                            // 展開対象情報更新
                            if (!updateDeploymentTroubleData(param.Rows[i]))
                            {
                                _ProccessSucccess = false;
                                return false;
                            }
                        }
                    }
                    _ProccessSucccess = true;
                    return true;
                }
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        #endregion

        #region タイトル品番情報関連
        /// <summary>
        /// タイトル品番情報取得
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>タイトル品番情報</returns>
        public DataTable getTitleDrawingData(string manageNo = null)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("SELECT ");
                sb.AppendLine("MITAKA_NO,TITLE_DRAWING_NO, ");
                sb.AppendLine("INSERT_USER,UPDATE_USER,INSERT_YMD,UPDATE_YMD, ");
                sb.AppendLine("'' AS EDIT_FLG ");
                sb.AppendLine("FROM ");
                sb.AppendLine("T_MITAKA_DRAWING ");
                sb.AppendLine("WHERE ");

                if (manageNo != null)
                {
                    sb.AppendLine("MITAKA_NO = '@manageNo' ");
                    sb = sb.Replace("@manageNo", manageNo);
                }
                else
                {
                    sb.AppendLine("MITAKA_NO = null ");
                }

                sb.AppendLine("ORDER BY MITAKA_NO,INSERT_YMD ");

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                DataColumn[] stringColumns = dt.Columns.Cast<DataColumn>()
                    .Where(c => c.DataType == typeof(string))
                    .ToArray();

                foreach (DataRow row in dt.Rows)
                    foreach (DataColumn col in stringColumns)
                    {
                        if (row.Field<string>(col) == null) continue;
                        row.SetField<string>(col, row.Field<string>(col).Trim());
                    }

                _ProccessSucccess = true;
                return dt;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return new DataTable();
            }
        }

        /// <summary>
        /// タイトル品番情報存在チェック
        /// </summary>
        /// <param name="param">タイトル品番情報フィールド行</param>
        /// <returns>存在判定結果</returns>
        public Boolean existTitleDrawingData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("SELECT ");
                sb.AppendLine("COUNT(MITAKA_NO) AS COUNT");
                sb.AppendLine("FROM ");
                sb.AppendLine("T_MITAKA_DRAWING ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' AND ");
                sb.AppendLine("TITLE_DRAWING_NO = '@titleDrawingNo' ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@titleDrawingNo", param["TITLE_DRAWING_NO"].ToString());

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                if (dt.Rows.Count >= 1)
                {
                    if ((int)dt.Rows[0]["COUNT"] > 0)
                    {
                        _ProccessSucccess = true;
                        return true;
                    }
                    else
                    {
                        _ProccessSucccess = true;
                        return false;
                    }
                }
                else
                {
                    _ProccessSucccess = true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// タイトル品番情報登録
        /// </summary>
        /// <param name="param">タイトル品番情報フィールド行</param>
        /// <returns>登録結果</returns>
        public Boolean insertTitleDrawingData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("INSERT ");
                sb.AppendLine("T_MITAKA_DRAWING( ");
                sb.AppendLine("MITAKA_NO,TITLE_DRAWING_NO, ");
                sb.AppendLine("INSERT_USER,UPDATE_USER,INSERT_YMD,UPDATE_YMD) ");
                sb.AppendLine("VALUES( ");
                sb.AppendLine("'@manageNo', '@titleDrawingNo', ");
                sb.AppendLine("'@userId', '@userId', '@nowDateTime', '@nowDateTime') ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@titleDrawingNo", param["TITLE_DRAWING_NO"].ToString());
                sb = sb.Replace("@userId", _userId);
                sb = sb.Replace("@nowDateTime", _ProccessDateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"));

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// タイトル品番情報削除（タイトル品番単位）
        /// </summary>
        /// <param name="param">タイトル品番情報フィールド行</param>
        /// <returns>削除結果</returns>
        public Boolean deleteTitleDrawingData(DataRow param)
        {
            bool existFlg = true;
            StringBuilder sb = new StringBuilder();

            try
            {
                // タイトル品番情報削除対象存在チェック
                existFlg = existTitleDrawingData(param);

                // タイトル品番情報存在チェックが「データ無し」の場合
                if (existFlg == false)
                {
                    _ProccessSucccess = true;
                    return false;
                }
                else
                {
                    sb.AppendLine("DELETE FROM T_MITAKA_DRAWING ");
                    sb.AppendLine("WHERE ");
                    sb.AppendLine("MITAKA_NO = '@manageNo' AND ");
                    sb.AppendLine("TITLE_DRAWING_NO = '@titleDrawingNo' ");
                    sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                    sb = sb.Replace("@titleDrawingNo", param["TITLE_DRAWING_NO"].ToString());

                    // SQL実行
                    DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                    _ProccessSucccess = true;
                    return true;
                }
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// タイトル品番情報削除（過去トラ観たか情報単位）
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>削除結果</returns>
        public Boolean deleteTitleDrawingData(string manageNo)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("DELETE FROM ");
                sb.AppendLine("T_MITAKA_DRAWING ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' ");
                sb = sb.Replace("@manageNo", manageNo);

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// タイトル品番情報登録・削除
        /// </summary>
        /// <param name="param">タイトル品番情報フィールド</param>
        /// <returns>登録・削除結果</returns>
        public Boolean postTitleDrawingData(DataTable param)
        {
            try
            {
                // タイトル品番情報フィールド件数が1件以上無い場合
                if (param.Rows.Count == 0)
                {
                    _ProccessSucccess = true;
                    return false;
                }
                // タイトル品番情報取得
                 var dt = getTitleDrawingData(param.Rows[0]["MITAKA_NO"].ToString());

                // 取得したタイトル品番情報でループ
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // タイトル品番情報フィールドに対象のタイトル品番情報が存在しない場合
                    if (!param.AsEnumerable().Any(row => row["MITAKA_NO"] == dt.Rows[i]["MITAKA_NO"]
                        && row["TITLLE_DRAWING_NO"] == dt.Rows[i]["TITLLE_DRAWING_NO"]))
                    {
                        // タイトル品番情報削除
                        if (!deleteTitleDrawingData(dt.Rows[i]))
                        {
                            _ProccessSucccess = false;
                            return false;
                        }
                    }
                }
                // タイトル品番情報フィールドでループ
                for (int i = 0; i < param.Rows.Count; i++)
                {
                    if (param.Rows[i]["EDIT_FLG"].ToString() != "1")
                    {
                        continue;
                    }
                    // タイトル品番情報登録対象存在チェック
                    // タイトル品番情報存在チェックが「データ無し」の場合
                    if (!existTitleDrawingData(param.Rows[i]))
                    {
                        // タイトル品番情報登録
                        if (!insertTitleDrawingData(param.Rows[i]))
                        {
                            _ProccessSucccess = false;
                            return false;
                        }
                    }
                    // タイトル品番情報存在チェックが「データ有り」の場合
                    else
                    {
                        continue;
                    }
                }

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        #endregion

        #region 機種情報関連
        /// <summary>
        /// 機種情報取得
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>機種情報</returns>
        public DataTable getModelData(string manageNo = null)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("SELECT ");
                sb.AppendLine("MITAKA_NO,MODEL, ");
                sb.AppendLine("INSERT_USER,UPDATE_USER,INSERT_YMD,UPDATE_YMD, ");
                sb.AppendLine("'' AS EDIT_FLG ");
                sb.AppendLine("FROM ");
                sb.AppendLine("T_MITAKA_MODEL ");
                sb.AppendLine("WHERE ");

                if (manageNo != null)
                {
                    sb.AppendLine("MITAKA_NO = '@manageNo' ");
                    sb = sb.Replace("@manageNo", manageNo);
                }
                else
                {
                    sb.AppendLine("MITAKA_NO = null ");
                }

                sb.AppendLine("ORDER BY MITAKA_NO,INSERT_YMD ");

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                DataColumn[] stringColumns = dt.Columns.Cast<DataColumn>()
                    .Where(c => c.DataType == typeof(string))
                    .ToArray();

                foreach (DataRow row in dt.Rows)
                    foreach (DataColumn col in stringColumns)
                    {
                        if (row.Field<string>(col) == null) continue;
                        row.SetField<string>(col, row.Field<string>(col).Trim());
                    }

                _ProccessSucccess = true;
                return dt;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return new DataTable();
            }
        }

        /// <summary>
        /// 機種情報存在チェック
        /// </summary>
        /// <param name="param">機種情報フィールド行</param>
        /// <returns>存在判定結果</returns>
        public Boolean existModelData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("SELECT ");
                sb.AppendLine("COUNT(MITAKA_NO) AS COUNT");
                sb.AppendLine("FROM T_MITAKA_MODEL ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' AND ");
                sb.AppendLine("MODEL = '@model' ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@model", param["MODEL"].ToString());

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                if (dt.Rows.Count >= 1)
                {
                    if ((int)dt.Rows[0]["COUNT"] > 0)
                    {
                        _ProccessSucccess = true;
                        return true;
                    }
                    else
                    {
                        _ProccessSucccess = true;
                        return false;
                    }
                }
                else
                {
                    _ProccessSucccess = true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 機種情報登録
        /// </summary>
        /// <param name="param">機種情報フィールド行</param>
        /// <returns>登録結果</returns>
        public Boolean insertModelData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("INSERT ");
                sb.AppendLine("T_MITAKA_MODEL( ");
                sb.AppendLine("MITAKA_NO,MODEL, ");
                sb.AppendLine("INSERT_USER,UPDATE_USER,INSERT_YMD,UPDATE_YMD) ");
                sb.AppendLine("VALUES( ");
                sb.AppendLine("'@manageNo', '@model', ");
                sb.AppendLine("'@userId', '@userId', '@nowDateTime', '@nowDateTime') ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@model", param["MODEL"].ToString());
                sb = sb.Replace("@userId", _userId);
                sb = sb.Replace("@nowDateTime", _ProccessDateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"));

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 機種情報削除（機種単位）
        /// </summary>
        /// <param name="param">機種情報フィールド行</param>
        /// <returns>削除結果</returns>
        public Boolean deleteModelData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                // 機種情報削除対象存在チェック
                // 機種情報存在チェックが「データ無し」の場合
                if (!existModelData(param))
                {
                    _ProccessSucccess = true;
                    return false;
                }
                else
                {
                    sb.AppendLine("DELETE FROM ");
                    sb.AppendLine("T_MITAKA_MODEL ");
                    sb.AppendLine("WHERE ");
                    sb.AppendLine("MITAKA_NO = '@manageNo' AND ");
                    sb.AppendLine("MODEL = '@model' ");
                    sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                    sb = sb.Replace("@model", param["MODEL"].ToString());

                    // SQL実行
                    DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                    _ProccessSucccess = true;
                    return true;
                }
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 機種情報削除（過去トラ観たか情報単位）
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>削除結果</returns>
        public Boolean deleteModelData(string manageNo)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("DELETE FROM ");
                sb.AppendLine("T_MITAKA_MODEL ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' ");
                sb = sb.Replace("@manageNo", manageNo);

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 機種情報登録・削除
        /// </summary>
        /// <param name="param">機種情報フィールド</param>
        /// <returns>登録・削除結果</returns>
        public Boolean postModelData(DataTable param)
        { 

            try
            {
                // 機種情報フィールド件数が1件以上無い場合
                if (param.Rows.Count == 0)
                {
                    _ProccessSucccess = false;
                    return false;
                }
                // 機種情報取得
                var dt = getModelData(param.Rows[0]["MITAKA_NO"].ToString());

                // 取得した機種情報でループ
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    // 機種情報フィールドに対象の機種情報が存在しない場合
                    if (!param.AsEnumerable().Any(row => row["MITAKA_NO"] == dt.Rows[i]["MITAKA_NO"]
                        && row["MODEL"] == dt.Rows[i]["MODEL"]))
                    {
                        // 機種情報削除
                        if (!deleteModelData(dt.Rows[i]))
                        {
                            _ProccessSucccess = false;
                            return false;
                        }
                    }
                }

                // 機種情報フィールドでループ
                for (int i = 0; i < param.Rows.Count; i++)
                {
                    if (param.Rows[i]["EDIT_FLG"].ToString() != "1")
                    {
                        continue;
                    }

                    // 機種情報登録対象存在チェック
                    // 機種情報存在チェックが「データ無し」の場合
                    if (!existModelData(param.Rows[i]))
                    {
                        // 機種情報登録
                        if (!insertModelData(param.Rows[i]))
                        {
                            _ProccessSucccess = false;
                            return false;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        #endregion

        #region BLK情報関連
        /// <summary>
        /// BLK情報取得
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>BLK情報</returns>
        public DataTable getBlockData(string manageNo = null)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("SELECT ");
                sb.AppendLine("MITAKA_NO,BLK_NO, ");
                sb.AppendLine("INSERT_USER,UPDATE_USER,INSERT_YMD,UPDATE_YMD, ");
                sb.AppendLine("'' AS EDIT_FLG ");
                sb.AppendLine("FROM ");
                sb.AppendLine("T_MITAKA_BLK ");
                sb.AppendLine("WHERE ");

                if (manageNo != null)
                {
                    sb.AppendLine("MITAKA_NO = '@manageNo' ");
                    sb = sb.Replace("@manageNo",manageNo);
                }
                else
                {
                    sb.AppendLine("MITAKA_NO = null ");
                }

                sb.AppendLine("ORDER BY MITAKA_NO,INSERT_YMD ");

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                DataColumn[] stringColumns = dt.Columns.Cast<DataColumn>()
                    .Where(c => c.DataType == typeof(string))
                    .ToArray();

                foreach (DataRow row in dt.Rows)
                    foreach (DataColumn col in stringColumns)
                    {
                        if (row.Field<string>(col) == null) continue;
                        row.SetField<string>(col, row.Field<string>(col).Trim());
                    }

                _ProccessSucccess = true;
                return dt;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return new DataTable();
            }
        }

        /// <summary>
        /// BLK情報存在チェック
        /// </summary>
        /// <param name="param">BLK情報フィールド行</param>
        /// <returns>存在判定結果</returns>
        public Boolean existBlockData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("SELECT ");
                sb.AppendLine("COUNT(MITAKA_NO) AS COUNT ");
                sb.AppendLine("FROM ");
                sb.AppendLine("T_MITAKA_BLK ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' AND ");
                sb.AppendLine("BLK_NO = '@blockNo' ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@blockNo", param["BLK_NO"].ToString());

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                if (dt.Rows.Count >= 1)
                {
                    if ((int)dt.Rows[0]["COUNT"] > 0)
                    {
                        _ProccessSucccess = true;
                        return true;
                    }
                    else
                    {
                        _ProccessSucccess = true;
                        return false;
                    }
                }
                else
                {
                    _ProccessSucccess = true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// BLK情報登録
        /// </summary>
        /// <param name="param">BLK情報フィールド行</param>
        /// <returns>登録結果</returns>
        public Boolean insertBlockData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("INSERT ");
                sb.AppendLine("T_MITAKA_BLK( ");
                sb.AppendLine("MITAKA_NO,BLK_NO, ");
                sb.AppendLine("INSERT_USER,UPDATE_USER,INSERT_YMD,UPDATE_YMD) ");
                sb.AppendLine("VALUES( ");
                sb.AppendLine("'@manageNo', '@blockNo', ");
                sb.AppendLine("'@userId', '@userId', '@nowDateTime','@nowDateTime') ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@blockNo", param["BLK_NO"].ToString());
                sb = sb.Replace("@userId", _userId);
                sb = sb.Replace("@nowDateTime", _ProccessDateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"));

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// BLK情報削除（BLK NO単位）
        /// </summary>
        /// <param name="param">BLK情報フィールド行</param>
        /// <returns>削除結果</returns>
        public Boolean deleteBlockData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                // BLK情報削除対象存在チェック
                // BLK情報存在チェックが「データ無し」の場合
                if (!existBlockData(param))
                {
                    _ProccessSucccess = true;
                    return false;
                }

                sb.AppendLine("DELETE FROM ");
                sb.AppendLine("T_MITAKA_BLK ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' AND ");
                sb.AppendLine("BLK_NO = '@blockNo' ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@blockNo", param["BLK_NO"].ToString());

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// BLK情報削除（過去トラ観たか情報単位）
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>削除結果</returns>
        public Boolean deleteBlockData(string manageNo)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("DELETE FROM ");
                sb.AppendLine("T_MITAKA_BLK ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' ");
                sb = sb.Replace("@manageNo", manageNo);

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// BLK情報登録・削除
        /// </summary>
        /// <param name="param">BLK情報フィールド</param>
        /// <returns>登録・削除結果</returns>
        public Boolean postBlockData(DataTable param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                // BLK情報フィールド件数が1件以上無い場合
                if (param.Rows.Count == 0)
                {
                    _ProccessSucccess = true;
                    return false;
                }
                // BLK情報取得
                var dt = getBlockData(param.Rows[0]["MITAKA_NO"].ToString());

                // 取得したBLK情報でループ
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // BLK情報フィールドに対象のBLK情報が存在しない場合
                    if (!param.AsEnumerable().Any(row => row["MITAKA_NO"] == dt.Rows[i]["MITAKA_NO"]
                        && row["BLK_NO"] == dt.Rows[i]["BLK_NO"]))
                    {
                        // BLK情報削除
                        if (!deleteBlockData(dt.Rows[i]))
                        {
                            _ProccessSucccess = false;
                            return false;
                        }
                    }
                }

                // BLK情報フィールドでループ
                for (int i = 0; i < param.Rows.Count; i++)
                {
                    if (param.Rows[i]["EDIT_FLG"].ToString() != "1")
                    {
                        continue;
                    }

                    // BLK情報登録対象存在チェック
                    // BLK情報存在チェックが「データ無し」の場合
                    if (!existBlockData(param.Rows[i]))
                    {
                        // BLK情報登録
                        if (!insertBlockData(param.Rows[i]))
                        {
                            _ProccessSucccess = false;
                            return false;
                        }
                    }
                    // BLK情報存在チェックが「データ有り」の場合
                    else
                    {
                        continue;
                    }
                }

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        #endregion

        #region 開発符号情報関連
        /// <summary>
        /// 開発符号情報取得
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>開発符号情報</returns>
        public DataTable getDevelopmentCodeData(string manageNo = null)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("SELECT ");
                sb.AppendLine("MITAKA_NO,DEVELOPMENT_CODE, ");
                sb.AppendLine("INSERT_USER,UPDATE_USER,INSERT_YMD,UPDATE_YMD, ");
                sb.AppendLine("'' AS EDIT_FLG ");
                sb.AppendLine("FROM ");
                sb.AppendLine("T_MITAKA_DEVELOPMENTSIGN ");
                sb.AppendLine("WHERE ");

                if (manageNo != null)
                {
                    sb.AppendLine("MITAKA_NO = '@manageNo' ");
                    sb = sb.Replace("@manageNo", manageNo);
                }
                else
                {
                    sb.AppendLine("MITAKA_NO = null ");
                }

                sb.AppendLine("ORDER BY MITAKA_NO,INSERT_YMD ");

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                DataColumn[] stringColumns = dt.Columns.Cast<DataColumn>()
                    .Where(c => c.DataType == typeof(string))
                    .ToArray();

                foreach (DataRow row in dt.Rows)
                    foreach (DataColumn col in stringColumns)
                    {
                        if (row.Field<string>(col) == null) continue;
                        row.SetField<string>(col, row.Field<string>(col).Trim());
                    }

                _ProccessSucccess = true;
                return dt;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return new DataTable();
            }
        }

        /// <summary>
        /// 開発符号情報存在チェック
        /// </summary>
        /// <param name="param">開発符号情報フィールド行</param>
        /// <returns>存在判定結果</returns>
        public Boolean existDevelopmentCodeData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("SELECT ");
                sb.AppendLine("COUNT(MITAKA_NO) AS COUNT ");
                sb.AppendLine("FROM ");
                sb.AppendLine("T_MITAKA_DEVELOPMENTSIGN ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' AND ");
                sb.AppendLine("DEVELOPMENT_CODE = '@developmentCode' ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@developmentCode", param["DEVELOPMENT_CODE"].ToString());

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                if (dt.Rows.Count >= 1)
                {
                    if ((int)dt.Rows[0]["COUNT"] > 0)
                    {
                        _ProccessSucccess = true;
                        return true;
                    }
                    else
                    {
                        _ProccessSucccess = true;
                        return false;
                    }
                }
                else
                {
                    _ProccessSucccess = true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 開発符号情報登録
        /// </summary>
        /// <param name="param">開発符号情報フィールド行</param>
        /// <returns>登録結果</returns>
        public Boolean insertDevelopmentCodeData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("INSERT ");
                sb.AppendLine("T_MITAKA_DEVELOPMENTSIGN( ");
                sb.AppendLine("MITAKA_NO,DEVELOPMENT_CODE, ");
                sb.AppendLine("INSERT_USER,UPDATE_USER,INSERT_YMD,UPDATE_YMD) ");
                sb.AppendLine("VALUES( ");
                sb.AppendLine("'@manageNo', '@DevelopmentCode', ");
                sb.AppendLine("'@userId', '@userId', '@nowDateTime', '@nowDateTime') ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@DevelopmentCode", param["DEVELOPMENT_CODE"].ToString());
                sb = sb.Replace("@userId", _userId);
                sb = sb.Replace("@nowDateTime", _ProccessDateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"));

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 開発符号情報削除（開発符号単位）
        /// </summary>
        /// <param name="param">開発符号情報フィールド行</param>
        /// <returns>削除結果</returns>
        public Boolean deleteDevelopmentCodeData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                // 開発符号情報削除対象存在チェック
                // 開発符号情報存在チェックが「データ無し」の場合
                if (!existDevelopmentCodeData(param))
                {
                    _ProccessSucccess = true;
                    return false;
                }
                sb.AppendLine("DELETE FROM ");
                sb.AppendLine("T_MITAKA_DEVELOPMENTSIGN ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' AND ");
                sb.AppendLine("DEVELOPMENT_CODE = '@developmentCode' ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@developmentCode", param["DEVELOPMENT_CODE"].ToString());

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 開発符号情報削除（過去トラ観たか情報単位）
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>削除結果</returns>
        public Boolean deleteDevelopmentCodeData(string manageNo)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("DELETE FROM ");
                sb.AppendLine("T_MITAKA_DEVELOPMENTSIGN ");
                sb.AppendLine("WHERE  ");
                sb.AppendLine("MITAKA_NO = '@manageNo' ");
                sb = sb.Replace("@manageNo", manageNo);

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 開発符号情報登録・削除
        /// </summary>
        /// <param name="param">開発符号情報フィールド</param>
        /// <returns>登録・削除結果</returns>
        public Boolean postDevelopmentCodeData(DataTable param)
        {

            try
            {
                // 開発符号情報フィールド件数が1件以上無い場合
                if (param.Rows.Count == 0)
                {
                    _ProccessSucccess = true;
                    return false;
                }
                else
                {
                    // 開発符号情報取得
                    var dt = getDevelopmentCodeData(param.Rows[0]["MITAKA_NO"].ToString());

                    // 取得した開発符号情報でループ
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // 開発符号情報フィールドに対象の開発符号情報が存在しない場合
                        if (!param.AsEnumerable().Any(row => row["MITAKA_NO"] == dt.Rows[i]["MITAKA_NO"]
                            && row["DEVELOPMENT_CODE"] == dt.Rows[i]["DEVELOPMENT_CODE"]))
                        {
                            // 開発符号情報削除
                            if (!deleteDevelopmentCodeData(dt.Rows[i]))
                            {
                                _ProccessSucccess = false;
                                return false;
                            }
                        }
                    }

                    // 開発符号情報フィールドでループ
                    for (int i = 0; i < param.Rows.Count; i++)
                    {
                        if (param.Rows[i]["EDIT_FLG"].ToString() != "1")
                        {
                            continue;
                        }

                        // 開発符号情報登録対象存在チェック
                        // 開発符号情報存在チェックが「データ無し」の場合
                        if (!existDevelopmentCodeData(param.Rows[i]))
                        {
                            // 開発符号情報登録
                            if (!insertDevelopmentCodeData(param.Rows[i]))
                            {
                                _ProccessSucccess = false;
                                return false;
                            }
                        }
                        // 開発符号情報存在チェックが「データ有り」の場合
                        else
                        {
                            continue;
                        }
                    }

                    _ProccessSucccess = true;
                    return true;
                }
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        #endregion

        #region 設通情報関連
        /// <summary>
        /// 設通情報取得
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>設通情報</returns>
        public DataTable getEcsData(string manageNo = null)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("SELECT ");
                sb.AppendLine("MITAKA_NO,ECS_NO, ");
                sb.AppendLine("INSERT_USER,UPDATE_USER,INSERT_YMD,UPDATE_YMD, ");
                sb.AppendLine("'' AS EDIT_FLG ");
                sb.AppendLine("FROM ");
                sb.AppendLine("T_MITAKA_ECS ");
                sb.AppendLine("WHERE ");

                if (manageNo != null)
                {
                    sb.AppendLine("MITAKA_NO = '@manageNo' ");
                    sb = sb.Replace("@manageNo", manageNo);
                }
                else
                {
                    sb.AppendLine("MITAKA_NO = null ");
                }

                sb.AppendLine("ORDER BY MITAKA_NO,INSERT_YMD ");

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                DataColumn[] stringColumns = dt.Columns.Cast<DataColumn>()
                    .Where(c => c.DataType == typeof(string))
                    .ToArray();

                foreach (DataRow row in dt.Rows)
                    foreach (DataColumn col in stringColumns)
                    {
                        if (row.Field<string>(col) == null) continue;
                        row.SetField<string>(col, row.Field<string>(col).Trim());
                    }

                _ProccessSucccess = true;
                return dt;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return new DataTable();
            }
        }

        /// <summary>
        /// 設通情報存在チェック
        /// </summary>
        /// <param name="param">設通情報フィールド行</param>
        /// <returns>存在判定結果</returns>
        public Boolean existEcsData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("SELECT ");
                sb.AppendLine("COUNT(MITAKA_NO) AS COUNT ");
                sb.AppendLine("FROM ");
                sb.AppendLine("T_MITAKA_ECS ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' AND ");
                sb.AppendLine("ECS_NO = '@ecsNo' ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@ecsNo", param["ECS_NO"].ToString());


                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                if (dt.Rows.Count >= 1)
                {
                    if ((int)dt.Rows[0]["COUNT"] > 0)
                    {
                        _ProccessSucccess = true;
                        return true;
                    }
                    else
                    {
                        _ProccessSucccess = true;
                        return false;
                    }
                }
                else
                {
                    _ProccessSucccess = true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 設通情報登録
        /// </summary>
        /// <param name="param">設通情報フィールド行</param>
        /// <returns>登録結果</returns>
        public Boolean insertEcsData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("INSERT ");
                sb.AppendLine("T_MITAKA_ECS( ");
                sb.AppendLine("MITAKA_NO,ECS_NO, ");
                sb.AppendLine("INSERT_USER,UPDATE_USER,INSERT_YMD,UPDATE_YMD) ");
                sb.AppendLine("VALUES( ");
                sb.AppendLine("'@manageNo', '@ecsNo', ");
                sb.AppendLine("'@userId', '@userId', '@nowDateTime','@nowDateTime') ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@ecsNo", param["ECS_NO"].ToString());
                sb = sb.Replace("@userId", _userId);
                sb = sb.Replace("@nowDateTime", _ProccessDateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"));

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 設通情報削除（設通No単位）
        /// </summary>
        /// <param name="param">設通情報フィールド行</param>
        /// <returns>削除結果</returns>
        public Boolean deleteEcsData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                // 設通情報削除対象存在チェック
                // 設通情報存在チェックが「データ無し」の場合
                if (!existEcsData(param))
                {
                    _ProccessSucccess = true;
                    return false;
                }
                else
                {
                    sb.AppendLine("DELETE FROM ");
                    sb.AppendLine("T_MITAKA_ECS ");
                    sb.AppendLine("WHERE ");
                    sb.AppendLine("MITAKA_NO = '@manageNo' AND ");
                    sb.AppendLine("ECS_NO = '@ecsNo' ");
                    sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                    sb = sb.Replace("@ecsNo", param["ECS_NO"].ToString());

                    // SQL実行
                    DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                    _ProccessSucccess = true;
                    return true;
                }
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 設通情報削除（過去トラ観たか情報単位）
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>削除結果</returns>
        public Boolean deleteEcsData(string manageNo)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("DELETE FROM ");
                sb.AppendLine("T_MITAKA_ECS ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' ");
                sb = sb.Replace("@manageNo", manageNo);

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 設通情報登録・削除
        /// </summary>
        /// <param name="param">設通情報フィールド</param>
        /// <returns>登録・削除結果</returns>
        public Boolean postEcsData(DataTable param)
        {
            try
            {
                // 設通情報フィールド件数が1件以上無い場合
                if (param.Rows.Count == 0)
                {
                    _ProccessSucccess = true;
                    return false;
                }
                // 設通情報取得
                var dt = getEcsData(param.Rows[0]["MITAKA_NO"].ToString());

                // 取得した設通情報でループ
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    // 設通情報フィールドに対象の設通情報が存在しない場合
                    if (!param.AsEnumerable().Any(row => row["MITAKA_NO"] == dt.Rows[i]["MITAKA_NO"]
                        && row["ECS_NO"] == dt.Rows[i]["ECS_NO"]))
                    {
                        // 設通情報削除
                        if (!deleteEcsData(dt.Rows[i]))
                        {
                            _ProccessSucccess = false;
                            return false;
                        }
                    }
                }

                // 設通情報フィールドでループ
                for (int i = 0; i < param.Rows.Count; i++)
                {
                    if (param.Rows[i]["EDIT_FLG"].ToString() != "1")
                    {
                        continue;
                    }
                        // 設通情報存在チェック
                    // 設通情報存在チェックが「データ無し」の場合
                    if (!existEcsData(param.Rows[i]))
                    {
                        // 設通情報登録
                        if (!insertEcsData(param.Rows[i]))
                        {
                            _ProccessSucccess = false;
                            return false;
                        }
                    }
                    // 設通情報存在チェックが「データ有り」の場合
                    else
                    {
                        continue;
                    }
                }

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        #endregion

        #region 過去トラ観たか回答関連
        /// <summary>
        /// 過去トラ観たか回答情報取得
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>過去トラ観たか回答情報</returns>
        public DataTable getMitakaAnswerData(string manageNo = null)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("SELECT ");
                sb.AppendLine("ANSWER.MITAKA_NO,ANSWER.SYSTEM_NO,ANSWER.ANSWER_DIVISION_CODE, ");
                sb.AppendLine("ANSWER.STATUS,ANSWER.ANSWER_CONTENT, ");
                sb.AppendLine("TARGET.TARGET_FLG,'' AS EDIT_FLG, ");
                sb.AppendLine("ANSWER.INSERT_USER,ANSWER.UPDATE_USER,ANSWER.INSERT_YMD,ANSWER.UPDATE_YMD, ");
                sb.AppendLine("TROUBLE.BY_PU,TROUBLE.KOUMOKU_KANRI_NO,TROUBLE.KOUMOKU,TROUBLE.RANK,TROUBLE.SAIHATU,TROUBLE.RSC, ");
                sb.AppendLine("CASE WHEN RTRIM(TROUBLE.BUHIN_NAME1) <> '' THEN RTRIM(TROUBLE.BUHIN_NAME1) ");
                sb.AppendLine("ELSE RTRIM(TROUBLE.BUHIN_NAME2) END AS BUHIN_NAME, ");
                sb.AppendLine("TROUBLE.FUGO_NAME1,TROUBLE.FUGO_NAME2,TROUBLE.FUGO_NAME3,TROUBLE.FUGO_NAME4,TROUBLE.FUGO_NAME5, ");
                sb.AppendLine("TROUBLE.GENIN,TROUBLE.TAISAKU,TROUBLE.KAIHATU_MIHAKKEN_RIYU,TROUBLE.SQB_KANTEN, ");
                sb.AppendLine("TROUBLE.SAIHATU_SEKKEI,TROUBLE.SAIHATU_HYOUKA,TROUBLE.SIRYOU_NO1,TROUBLE.SIRYOU_NO2, ");
                sb.AppendLine("TROUBLE.SETTU_NO1,TROUBLE.SETTU_NO2,TROUBLE.SETTU_NO3,TROUBLE.SETTU_NO4,SETTU_NO5, ");
                sb.AppendLine("TROUBLE.BUSYO_SEKKEI1,TROUBLE.BUSYO_SEKKEI2,TROUBLE.BUSYO_SEKKEI3,TROUBLE.BUSYO_SEKKEI4, ");
                sb.AppendLine("TROUBLE.BUSYO_SEKKEI5,TROUBLE.BUSYO_SEKKEI6,TROUBLE.BUSYO_SEKKEI7,TROUBLE.BUSYO_SEKKEI8, ");
                sb.AppendLine("TROUBLE.BUSYO_SEKKEI9,TROUBLE.BUSYO_SEKKEI10, ");
                sb.AppendLine("TROUBLE.BUSYO_HYOUKA1,TROUBLE.BUSYO_HYOUKA2,TROUBLE.BUSYO_HYOUKA3,TROUBLE.BUSYO_HYOUKA4, ");
                sb.AppendLine("TROUBLE.BUSYO_HYOUKA5,TROUBLE.BUSYO_HYOUKA6,TROUBLE.BUSYO_HYOUKA7,TROUBLE.BUSYO_HYOUKA8, ");
                sb.AppendLine("TROUBLE.BUSYO_HYOUKA9,TROUBLE.BUSYO_HYOUKA10");
                sb.AppendLine("FROM ");
                sb.AppendLine("T_MITAKA_ANSWER ANSWER ");
                sb.AppendLine("LEFT JOIN T_TROUBLE_DATA TROUBLE ");
                sb.AppendLine("ON ANSWER.SYSTEM_NO = TROUBLE.SYSTEM_NO ");
                sb.AppendLine("LEFT JOIN T_MITAKA_TROUBLE_DATA TARGET ");
                sb.AppendLine("ON ANSWER.SYSTEM_NO = TARGET.SYSTEM_NO ");
                sb.AppendLine("WHERE ");

                if (manageNo != null)
                {
                    sb.AppendLine("ANSWER.MITAKA_NO = '@manageNo' ");
                    sb= sb.Replace("@manageNo",manageNo);
                }
                else
                {
                    sb.AppendLine("ANSWER.MITAKA_NO = null ");
                }

                sb.AppendLine("ORDER BY ANSWER.MITAKA_NO,INSERT_YMD,ANSWER.SYSTEM_NO,ANSWER.ANSWER_DIVISION_CODE ");

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                DataColumn[] stringColumns = dt.Columns.Cast<DataColumn>()
                   .Where(c => c.DataType == typeof(string))
                   .ToArray();

                foreach (DataRow row in dt.Rows)
                    foreach (DataColumn col in stringColumns)
                    {
                        if (row.Field<string>(col) == null) continue;
                        row.SetField<string>(col, row.Field<string>(col).Trim());
                    }

                _ProccessSucccess = true;
                return dt;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return new DataTable();
            }
        }

        /// <summary>
        /// 過去トラ観たか回答情報存在チェック
        /// </summary>
        /// <param name="param">過去トラ観たか回答情報フィールド行</param>
        /// <returns>存在判定結果</returns>
        public Boolean existMitakaAnswerData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("SELECT ");
                sb.AppendLine("COUNT(MITAKA_NO) AS COUNT ");
                sb.AppendLine("FROM ");
                sb.AppendLine("T_MITAKA_ANSWER ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' AND ");
                sb.AppendLine("SYSTEM_NO = '@systemNo' AND ");
                sb.AppendLine("ANSWER_DIVISION_CODE = '@answerDivisionCode' ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@systemNo", param["SYSTEM_NO"].ToString());
                sb = sb.Replace("@answerDivisionCode", param["ANSWER_DIVISION_CODE"].ToString());

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                if (dt.Rows.Count >= 1)
                {
                    if ((int)dt.Rows[0]["COUNT"] > 0)
                    {
                        _ProccessSucccess = true;
                        return true;
                    }
                    else
                    {
                        _ProccessSucccess = true;
                        return false;
                    }
                }
                else
                {
                    _ProccessSucccess = true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 過去トラ観たか回答情報登録
        /// </summary>
        /// <param name="param">過去トラ観たか回答情報フィールド行</param>
        /// <returns>登録結果</returns>
        public Boolean insertMitakaAnswerData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("INSERT ");
                sb.AppendLine("T_MITAKA_ANSWER( ");
                sb.AppendLine("MITAKA_NO,SYSTEM_NO,ANSWER_DIVISION_CODE,STATUS,ANSWER_CONTENT, ");
                sb.AppendLine("INSERT_USER,UPDATE_USER,INSERT_YMD,UPDATE_YMD) ");
                sb.AppendLine("VALUES( ");
                sb.AppendLine("'@manageNo', '@systemNo', '@answerDivisionCode','','', ");
                sb.AppendLine("'@userId', '@userId', '@nowDateTime', '@nowDateTime') ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@systemNo", param["SYSTEM_NO"].ToString());
                sb = sb.Replace("@answerDivisionCode", param["ANSWER_DIVISION_CODE"].ToString());
                sb = sb.Replace("@userId", _userId);
                sb = sb.Replace("@nowDateTime", _ProccessDateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"));

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 過去トラ観たか回答情報更新
        /// </summary>
        /// <param name="param">過去トラ観たか回答情報フィールド行</param>
        /// <returns>更新結果</returns>
        public Boolean updateMitakaAnswerData(DataRow param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("UPDATE ");
                sb.AppendLine("T_MITAKA_ANSWER ");
                sb.AppendLine("SET ");
                sb.AppendLine("STATUS = '@status', ");
                sb.AppendLine("ANSWER_CONTENT = '@answerContent', ");
                sb.AppendLine("UPDATE_USER = '@userId', ");
                sb.AppendLine("UPDATE_YMD = '@nowDateTime'");
                sb.AppendLine("WHERE MITAKA_NO = '@manageNo' AND ");
                sb.AppendLine("SYSTEM_NO = '@systemNo' AND ");
                sb.AppendLine("ANSWER_DIVISION_CODE = '@answerDivisionCode' ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@systemNo", param["SYSTEM_NO"].ToString());
                sb = sb.Replace("@answerDivisionCode", param["ANSWER_DIVISION_CODE"].ToString());
                sb = sb.Replace("@status", param["STATUS"].ToString());
                sb = sb.Replace("@answerContent", param["ANSWER_CONTENT"].ToString());
                sb = sb.Replace("@userId", _userId);
                sb = sb.Replace("@nowDateTime", _ProccessDateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"));

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 過去トラ観たか回答情報削除
        /// </summary>
        /// <param name="param">過去トラ観たか回答情報フィールド行</param>
        /// <returns>削除結果</returns>
        public Boolean deleteMitakaAnswerData(DataRow param)
        {
            bool existFlg = true;
            StringBuilder sb = new StringBuilder();

            try
            {
                // 過去トラ観たか回答情報削除対象存在チェック
                if (!existMitakaAnswerData(param))
                {
                    _ProccessSucccess = true;
                    return false;
                }
                sb.AppendLine("DELETE FROM ");
                sb.AppendLine("T_MITAKA_ANSWER ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' AND ");
                sb.AppendLine("SYTEM_NO = '@systemNo' AND ");
                sb.AppendLine("ANSWER_DIVISION_CODE = '@answerDivisionCode' ");
                sb = sb.Replace("@manageNo", param["MITAKA_NO"].ToString());
                sb = sb.Replace("@systemNo", param["SYSTEM_NO"].ToString());
                sb = sb.Replace("@answerDivisionCode", param["ANSWER_DIVISION_CODE"].ToString());

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 過去トラ観たか回答情報削除
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <returns>削除結果</returns>
        public Boolean deleteMitakaAnswerData(string manageNo)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("DELETE FROM ");
                sb.AppendLine("T_MITAKA_ANSWER ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("MITAKA_NO = '@manageNo' ");
                sb = sb.Replace("@manageNo", manageNo);

                // SQL実行
                DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 過去トラ観たか回答情報登録・更新・削除
        /// </summary>
        /// <param name="param">過去トラ観たか回答情報フィールド</param>
        /// <returns>登録・更新・削除結果</returns>
        public Boolean postMitakaAnswerData(DataTable param)
        {
            try
            {
                // 過去トラ観たか回答情報フィールド件数が1件以上無い場合
                if (param.Rows.Count == 0)
                {
                    _ProccessSucccess = false;
                    return false;
                }

                // 過去トラ観たか回答情報取得
                var dt = getMitakaAnswerData(param.Rows[0]["MITAKA_NO"].ToString());

                // 取得した過去トラ観たか回答情報でループ
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // 過去トラ観たか回答フィールドに対象の検索条件が存在しない場合
                    if (!param.AsEnumerable().Any(row => row["MITAKA_NO"] == dt.Rows[i]["MITAKA_NO"]
                        && row["SYSTEM_NO"] == dt.Rows[i]["SYSTEM_NO"]))
                    {
                        // 過去トラ観たか回答情報削除
                        if (!deleteMitakaAnswerData(dt.Rows[i]))
                        {
                            _ProccessSucccess = false;
                            return false;
                        }
                    }
                }

                // 過去トラ観たか回答情報フィールドでループ
                for (int i = 0; i < param.Rows.Count; i++)
                {
                    if (param.Rows[i]["EDIT_FLG"].ToString() != "1")
                    {
                        continue;
                    }
                    // 過去トラ観たか回答情報登録対象存在チェック
                    // 過去トラ観たか回答情報存在チェックが「データ無し」の場合
                    if (!existMitakaAnswerData(param.Rows[i]))
                    {
                        // 過去トラ観たか回答情報登録
                        // 過去トラ観たか回答情報登録の登録結果が「登録NG」の場合
                        if (!insertMitakaAnswerData(param.Rows[i]))
                        {
                            _ProccessSucccess = false;
                            return false;
                        }
                    }
                    // 過去トラ観たか回答情報存在チェックが「データ有り」の場合
                    else
                    {
                        // 過去トラ観たか回答情報更新
                        // 過去トラ観たか回答情報更新の更新結果が「更新NG」の場合
                        if (!updateMitakaAnswerData(param.Rows[i]))
                        {
                            _ProccessSucccess = false;
                            return false;
                        }
                    }
                }

                _ProccessSucccess = true;
                return true;
            }
            catch (Exception ex)
            {
                _ProccessSucccess = false;
                _ErrorMessage.Add(ex.Message);
                return false;
            }
        }

        #endregion

        #endregion
    }
}