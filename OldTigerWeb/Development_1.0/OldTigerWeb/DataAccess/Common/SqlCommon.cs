using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace OldTigerWeb.DataAccess.Common
{
    public class SqlCommon
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public SqlCommon()
        {
        }

        #region データベースオープン
        public SqlConnection dbOpen()
        {
            try
            {
                // Web.Configよりデータベース接続文字列を取得
                string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["OldTigerConnectionString"].ConnectionString;

                // DB接続
                SqlConnection conn = new SqlConnection(connString);

                // DBオープン
                conn.Open();

                return conn;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ユーザ情報取得
        /// <summary>
        /// ユーザ情報取得
        /// </summary>
        /// <param name="user_id">ユーザID</param>
        /// <returns>ユーザ情報</returns>
        public DataTable SelectUser(String user_id)
        {
            DataTable work_t = new DataTable();

            StringBuilder sb = new StringBuilder();

            try
            {
                // SQL作成
                sb.AppendLine("SELECT ");
                sb.AppendLine("* ");
                sb.AppendLine("FROM ");
                sb.AppendLine("M_USER ");
                sb.AppendLine("where ");
                sb.AppendLine("user_id = '@userId'");
                sb= sb.Replace("@userId", user_id);

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

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        // 2017/07/14 Add Start
        #region 課コードから部コードを取得
        /// <summary>
        /// 課コードから部コードを取得
        /// </summary>
        /// <param name="Type">課・主査コード</param>
        /// <returns>部コード</returns>
        public DataTable selectBuCode(String ka_code)
        {
            DataTable result = new DataTable();

            // DBオープン
            DataAccess.Common.SqlCommon dbBase = new DataAccess.Common.SqlCommon();

            SqlConnection connDb = dbBase.dbOpen();

            try
            {
                // SQL作成
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connDb;
                cmd.CommandText = "SELECT DISTINCT(BU_CODE) AS BU_CODE ";
                cmd.CommandText += "FROM M_BUSYO_SEKKEI ";
                cmd.CommandText += "WHERE KA_CODE = '" + ka_code + "' ";
                cmd.CommandText += "UNION ";
                cmd.CommandText += "SELECT DISTINCT(BU_CODE) AS BU_CODE ";
                cmd.CommandText += "FROM M_BUSYO_HYOUKA ";
                cmd.CommandText += "WHERE KA_CODE = '" + ka_code + "' ";

                // コマンドを実行
                SqlDataReader reader = cmd.ExecuteReader();

                // SqlDataReader からデータを DataTable に読み込む
                result.Load(reader);

                reader.Close();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connDb.Close();
                connDb.Dispose();
                connDb = null;
            }
        }
        #endregion
        // 2017/07/14 Add End

        #region 過去トラ情報取得
        /// <summary>
        /// 過去トラ情報取得
        /// </summary>
        /// <param name="kanri_no">システム管理No</param>
        /// <returns>過去トラ情報</returns>
        public DataTable SelectTorableData(String kanri_no)
        {
            DataTable work_t = new DataTable();

            // DBオープン
            SqlConnection connDb = dbOpen();

            try
            {
                // SQL作成
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connDb;
                cmd.CommandText = "SELECT * FROM T_TROUBLE_DATA WHERE SYSTEM_NO = @kanri_no AND RANK <> 'X'";
                cmd.Parameters.AddWithValue("@kanri_no", kanri_no);

                // コマンドを実行
                SqlDataReader reader = cmd.ExecuteReader();

                // SqlDataReader からデータを DataTable に読み込む
                work_t.Load(reader);

                reader.Close();

                return work_t;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connDb.Close();
                connDb.Dispose();
                connDb = null;
            }
        }
        #endregion

        #region 検索ログ登録
        /// <summary>
        /// 検索ログ登録
        /// <param name="type">検索タイプ</param>
        /// <param name="word">検索ワード</param>
        /// <param name="kanri_no">システム管理No</param>
        /// <param name="date_time">システム日時</param>
        /// <returns>結果ステータス</returns>
        //20170201 機能改善 START
        //public Boolean InsertLogData(String userid, String type, String word, int kanri_no)
        public Boolean InsertLogData(String userid, String type, String word, int kanri_no, DateTime date_time)
        //20170201 機能改善 END
        {
            Boolean result = false;

            // DBオープン
            SqlConnection connDb = dbOpen();

            //トランザクションの開始
            SqlTransaction sqlTran= connDb.BeginTransaction();

            try
            {
                // SQL作成
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connDb;
                cmd.Transaction = sqlTran;
                //20170201 機能改善 START
                cmd.CommandText = "INSERT INTO T_LOG_DATA (DATE_TIME, USER_ID, SEARCH_TYPE, SYSTEM_NO, SEARCH_WORD)";
                //cmd.CommandText += " VALUES ( GETDATE(), '" + userid + "','" + type + "'," + kanri_no + ", @word )";
                cmd.CommandText += " VALUES (@date_time" + ",'" + userid + "','" + type + "'," + kanri_no + ", @word )";
                cmd.Parameters.AddWithValue("@date_time", date_time);
                //20170201 機能改善 END
                cmd.Parameters.AddWithValue("@word", word);

                // コマンドを実行
                if (cmd.ExecuteNonQuery() == 1)
                {
                    // コミット
                    sqlTran.Commit();
                }
                else
                {
                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlTran.Dispose();
                sqlTran = null;
                connDb.Close();
                connDb.Dispose();
                connDb = null;
            }
        }
        #endregion

        //20170201 機能改善 START
        #region 検索履歴履歴登録
        /// <summary>
        /// 検索履歴登録
        /// <param name="userid">ユーザID</param>
        /// <param name="date">システム日時</param>
        /// <param name="paraCondition">And・Or検索条件</param>
        /// <param name="paraType">種類</param>
        /// <param name="paraWord">検索文字</param>
        /// <param name="paraBusyo">設計部署</param>
        /// <param name="paraHyouka">評価部署</param>
        /// <param name="paraPartsS">部品・システム</param>
        /// <param name="paraPartsN">部品・絞込み</param>
        /// <param name="paraKaihatu">開発符号</param>
        /// <param name="paraGensyo">現象(分類)</param>
        /// <param name="paraGenin">原因(分類)</param>
        /// <param name="paraSyakata">車型特殊</param>
        /// <param name="paraSgensyo">現象(制御系)</param>
        /// <param name="paraSyouin">要因(制御系)</param>
        /// <param name="paraEgtm">EGTM形式</param>
        /// <returns>結果ステータス</returns>
        public Boolean InsertHistoryLogData(String userid, DateTime date, String paraCondition, String paraType, String paraWord, 
                                            String paraBusyo, String paraHyouka, String paraPartsS, String paraPartsN,
                                            String paraKaihatu, String paraGensyo, String paraGenin, String paraSyakata,
                                            String paraSgensyo, String paraSyouin, String paraEgtm)
        {
            Boolean result = false;
            String paraTop10 = null;

            // DBオープン
            SqlConnection connDb = dbOpen();

            //トランザクションの開始
            SqlTransaction sqlTran = connDb.BeginTransaction();

            try
            {
                if (paraType == Def.DefTYPE_TOP10)
                {
                    paraTop10 = paraWord;
                    paraWord = null;
                }
                // SQL作成
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connDb;
                cmd.Transaction = sqlTran;
                cmd.CommandText = "MERGE INTO T_HISTORY_DATA AS A ";
                cmd.CommandText += "    USING (SELECT @user_id AS USER_ID, @date_time AS DATE_TIME, @condition_flg AS CONDITION_FLG ";
                cmd.CommandText += ",@search_word AS SEARCH_WORD ";
                cmd.CommandText += ",@search_top10 AS SEARCH_TOP10 ";
                cmd.CommandText += ",@search_busyo AS SEARCH_BUSYO ";
                cmd.CommandText += ",@search_hyouka AS SEARCH_HYOUKA ";
                cmd.CommandText += ",@search_parts_s AS SEARCH_PARTS_S ";
                cmd.CommandText += ",@search_parts_n AS SEARCH_PARTS_N ";
                cmd.CommandText += ",@search_kaihatu AS SEARCH_KAIHATU ";
                cmd.CommandText += ",@search_gensyo AS SEARCH_GENSYO ";
                cmd.CommandText += ",@search_genin AS SEARCH_GENIN ";
                cmd.CommandText += ",@search_syakata AS SEARCH_SYAKATA ";
                cmd.CommandText += ",@search_sgensyo AS SEARCH_SGENSYO ";
                cmd.CommandText += ",@search_syouin AS SEARCH_SYOUIN ";
                cmd.CommandText += ",@search_egtm AS SEARCH_EGTM ";
                cmd.CommandText += "    ) AS B ";
                cmd.CommandText += "    ON ";
                cmd.CommandText += "    ( ";
                cmd.CommandText += "            A.USER_ID = B.USER_ID ";
                cmd.CommandText += "        AND A.CONDITION_FLG = B.CONDITION_FLG ";
                if (paraWord == null) { cmd.CommandText += "AND A.SEARCH_WORD IS NULL "; } else { cmd.CommandText += "AND A.SEARCH_WORD = B.SEARCH_WORD "; }
                if (paraTop10 == null) { cmd.CommandText += "AND A.SEARCH_TOP10 IS NULL "; } else { cmd.CommandText += "AND A.SEARCH_TOP10 = B.SEARCH_TOP10 "; }
                if (paraBusyo == null) { cmd.CommandText += "AND A.SEARCH_BUSYO IS NULL "; } else { cmd.CommandText += "AND A.SEARCH_BUSYO = B.SEARCH_BUSYO "; }
                if (paraHyouka == null) { cmd.CommandText += "AND A.SEARCH_HYOUKA IS NULL "; } else { cmd.CommandText += "AND A.SEARCH_HYOUKA = B.SEARCH_HYOUKA "; }
                if (paraPartsS == null) { cmd.CommandText += "AND A.SEARCH_PARTS_S IS NULL "; } else { cmd.CommandText += "AND A.SEARCH_PARTS_S = B.SEARCH_PARTS_S "; }
                if (paraPartsN == null) { cmd.CommandText += "AND A.SEARCH_PARTS_N IS NULL "; } else { cmd.CommandText += "AND A.SEARCH_PARTS_N = B.SEARCH_PARTS_N "; }
                if (paraKaihatu == null) { cmd.CommandText += "AND A.SEARCH_KAIHATU IS NULL "; } else { cmd.CommandText += "AND A.SEARCH_KAIHATU = B.SEARCH_KAIHATU "; }
                if (paraGensyo == null) { cmd.CommandText += "AND A.SEARCH_GENSYO IS NULL "; } else { cmd.CommandText += "AND A.SEARCH_GENSYO = B.SEARCH_GENSYO "; }
                if (paraGenin == null) { cmd.CommandText += "AND A.SEARCH_GENIN IS NULL "; } else { cmd.CommandText += "AND A.SEARCH_GENIN = B.SEARCH_GENIN "; }
                if (paraSyakata == null) { cmd.CommandText += "AND A.SEARCH_SYAKATA IS NULL "; } else { cmd.CommandText += "AND A.SEARCH_SYAKATA = B.SEARCH_SYAKATA "; }
                if (paraSgensyo == null) { cmd.CommandText += "AND A.SEARCH_SGENSYO IS NULL "; } else { cmd.CommandText += "AND A.SEARCH_SGENSYO = B.SEARCH_SGENSYO "; }
                if (paraSyouin == null) { cmd.CommandText += "AND A.SEARCH_SYOUIN IS NULL "; } else { cmd.CommandText += "AND A.SEARCH_SYOUIN = B.SEARCH_SYOUIN "; }
                if (paraEgtm == null) { cmd.CommandText += "AND A.SEARCH_EGTM IS NULL "; } else { cmd.CommandText += "AND A.SEARCH_EGTM = B.SEARCH_EGTM "; }
                cmd.CommandText += "    ) ";
                cmd.CommandText += "    WHEN MATCHED THEN ";
                cmd.CommandText += "        UPDATE SET ";
                cmd.CommandText += "             DATE_TIME = B.DATE_TIME ";
                cmd.CommandText += "            ,UPDATE_USER = B.USER_ID ";
                cmd.CommandText += "            ,UPDATE_YMD = B.DATE_TIME ";
                cmd.CommandText += "    WHEN NOT MATCHED THEN ";
                cmd.CommandText += "        INSERT (USER_ID, DATE_TIME, CONDITION_FLG ";
                cmd.CommandText += ",SEARCH_WORD ";
                cmd.CommandText += ",SEARCH_TOP10 ";
                cmd.CommandText += ",SEARCH_BUSYO ";
                cmd.CommandText += ",SEARCH_HYOUKA ";
                cmd.CommandText += ",SEARCH_PARTS_S ";
                cmd.CommandText += ",SEARCH_PARTS_N ";
                cmd.CommandText += ",SEARCH_KAIHATU ";
                cmd.CommandText += ",SEARCH_GENSYO ";
                cmd.CommandText += ",SEARCH_GENIN ";
                cmd.CommandText += ",SEARCH_SYAKATA ";
                cmd.CommandText += ",SEARCH_SGENSYO ";
                cmd.CommandText += ",SEARCH_SYOUIN ";
                cmd.CommandText += ",SEARCH_EGTM ";
                cmd.CommandText += "        ,INSERT_USER, INSERT_YMD, UPDATE_USER, UPDATE_YMD) ";
                cmd.CommandText += "        VALUES ";
                cmd.CommandText += "        ( ";
                cmd.CommandText += "             B.USER_ID ";
                cmd.CommandText += "            ,B.DATE_TIME ";
                cmd.CommandText += "            ,B.CONDITION_FLG ";
                cmd.CommandText += ",B.SEARCH_WORD ";
                cmd.CommandText += ",B.SEARCH_TOP10 ";
                cmd.CommandText += ",B.SEARCH_BUSYO ";
                cmd.CommandText += ",B.SEARCH_HYOUKA ";
                cmd.CommandText += ",B.SEARCH_PARTS_S ";
                cmd.CommandText += ",B.SEARCH_PARTS_N ";
                cmd.CommandText += ",B.SEARCH_KAIHATU ";
                cmd.CommandText += ",B.SEARCH_GENSYO ";
                cmd.CommandText += ",B.SEARCH_GENIN ";
                cmd.CommandText += ",B.SEARCH_SYAKATA ";
                cmd.CommandText += ",B.SEARCH_SGENSYO ";
                cmd.CommandText += ",B.SEARCH_SYOUIN ";
                cmd.CommandText += ",B.SEARCH_EGTM ";
                cmd.CommandText += "            ,B.USER_ID ";
                cmd.CommandText += "            ,B.DATE_TIME ";
                cmd.CommandText += "            ,B.USER_ID ";
                cmd.CommandText += "            ,B.DATE_TIME ";
                cmd.CommandText += "        ); ";
                cmd.Parameters.AddWithValue("@user_id", userid);
                cmd.Parameters.AddWithValue("@date_time", date);
                cmd.Parameters.AddWithValue("@condition_flg", paraCondition);
                cmd.Parameters.AddWithValue("@search_word", ((Object)paraWord) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@search_top10", ((Object)paraTop10) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@search_busyo", ((Object)paraBusyo) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@search_hyouka", ((Object)paraHyouka) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@search_parts_s", ((Object)paraPartsS) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@search_parts_n", ((Object)paraPartsN) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@search_kaihatu", ((Object)paraKaihatu) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@search_gensyo", ((Object)paraGensyo) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@search_genin", ((Object)paraGenin) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@search_syakata", ((Object)paraSyakata) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@search_sgensyo", ((Object)paraSgensyo) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@search_syouin", ((Object)paraSyouin) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@search_egtm", ((Object)paraEgtm) ?? DBNull.Value);

                // コマンドを実行
                if (cmd.ExecuteNonQuery() == 1)
                {
                    // コミット
                    sqlTran.Commit();
                }
                else
                {
                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlTran.Dispose();
                sqlTran = null;
                connDb.Close();
                connDb.Dispose();
                connDb = null;
            }
        }
        #endregion

        #region 検索タイプ別抽出
        /// <summary>
        /// 検索タイプ別抽出
        /// </summary>
        /// <param name="dtAllType">全種類</param>
        /// <param name="strType">検索タイプ</param>
        /// <returns>検索タイプ別抽出データ</returns>
        private DataRow[] setSelectType(DataTable dtAllType, string strType)
        {
            DataRow[] drSelectType = (
                from row in dtAllType.AsEnumerable()
                let columnID = row.Field<string>("TableType")
                where columnID == strType
                select row
            ).ToArray();
            return drSelectType;
        }
        #endregion


        //20170201 機能改善 END

        #region 検索履歴情報取得
        /// <summary>
        /// 検索履歴情報取得
        /// </summary>
        /// <param name="user_id">ユーザ</param>
        /// <returns>結果データテーブル</returns>
        /// <remarks></remarks>
        public DataTable SelectSearchHistoryList(String user_id)
        {
            DataTable work_t = new DataTable();

            // DBオープン
            SqlConnection connDb = dbOpen();

            try
            {
                // SQL作成
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connDb;
                cmd.CommandText = "SELECT TOP 5 ";
                cmd.CommandText += "USER_ID, DATE_TIME,";
                cmd.CommandText += " IIF(CONDITION_FLG='1','AND','OR') AS CONDITION_FLGM,";
                cmd.CommandText += " SEARCH_WORD,";
                cmd.CommandText += " SEARCH_BUSYO,";
                cmd.CommandText += " SEARCH_HYOUKA,";
                cmd.CommandText += " SEARCH_PARTS_S,";
                cmd.CommandText += " SEARCH_PARTS_N,";
                cmd.CommandText += " SEARCH_KAIHATU,";
                cmd.CommandText += " SEARCH_GENSYO,";
                cmd.CommandText += " SEARCH_GENIN,";
                cmd.CommandText += " SEARCH_SYAKATA,";
                cmd.CommandText += " SEARCH_SGENSYO,";
                cmd.CommandText += " SEARCH_SYOUIN,";
                // 20170304 START k-ohmatsuzawa TOP10検索が履歴に表示されるように修正
                //cmd.CommandText += " SEARCH_EGTM";
                cmd.CommandText += " SEARCH_EGTM,";
                cmd.CommandText += " SEARCH_TOP10";
                // 20170304 END k-ohmatsuzawa
                cmd.CommandText += "  FROM T_HISTORY_DATA";
                cmd.CommandText += " WHERE USER_ID  = '" + user_id + "' ORDER BY DATE_TIME DESC";
                cmd.Parameters.AddWithValue("@user_id", user_id);

                // コマンドを実行
                SqlDataReader reader = cmd.ExecuteReader();

                // SqlDataReader からデータを DataTable に読み込む
                work_t.Load(reader);

                reader.Close();

                return work_t;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connDb.Close();
                connDb.Dispose();
                connDb = null;
            }
        }
        #endregion

    }
}