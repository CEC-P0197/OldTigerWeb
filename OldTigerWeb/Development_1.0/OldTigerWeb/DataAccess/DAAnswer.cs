using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace OldTigerWeb.DataAccess
{
    public class DAAnswer
    {
        #region フォロー回答詳細情報取得
        /// <summary>
        /// フォロー情報取得
        /// </summary>
        /// <param name="FMC_mc">FMC/mc区分</param>
        /// <param name="kaihatu_id">開発符号</param>
        /// <param name="by_pu">BYPU区分</param>
        /// <param name="event_no">イベントNO</param>
        /// <param name="Type">フォロー管理No</param>
        /// <param name="ka_code">課・主査コード</param>
        /// <param name="Type">システム管理番号</param>
        /// <returns>取得結果情報</returns>
        public DataTable SelectFollowData(String FMC_mc, String kaihatu_id, String by_pu, String event_no,
            String follow_no, String ka_code, String system_no)
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
                cmd.CommandText = "SELECT ";
                cmd.CommandText += "TRB.* , ";
                cmd.CommandText += "FLW.HEARING, FLW.SINDO, FLW.TAIOU_NAIYO, FORMAT(FLW.UPDATE_YMD, 'yyyyMMddHHmmss') AS SHARED_YMD ";
                cmd.CommandText += "FROM T_FOLLOW_DATA AS FLW ";
                cmd.CommandText += "INNER JOIN T_TROUBLE_DATA AS TRB ";
                cmd.CommandText += "ON FLW.SYSTEM_NO = TRB.SYSTEM_NO ";
                cmd.CommandText += "WHERE FLW.FMC_mc = '" + FMC_mc + "' AND FLW.KAIHATU_ID = '" + kaihatu_id + "' AND FLW.EVENT_NO = '" + event_no + "' AND FLW.FOLLOW_NO = '" + follow_no + "' ";
                cmd.CommandText += "AND FLW.KA_CODE = '" + ka_code + "' AND FLW.SYSTEM_NO = " + system_no;

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

        #region 回答情報更新
        /// <summary>
        /// 回答情報更新
        /// </summary>
        /// <param name="FMC_mc">FMC/mc 1:FMC、2:mc</param>
        /// <param name="KAIHATU_ID">開発符号</param>
        /// <param name="BY_PU">BYPU区分</param>
        /// <param name="EVENT_NO">イベントNO</param>
        /// <param name="FOLLOW_NO">フォロー管理No</param>
        /// <param name="KA_CODE">課・主査コード</param>
        /// <param name="SYSTEM_NO">システム管理番号</param>
        /// <param name="SINDO">進度</param>
        /// <param name="TAIOU_NAIYO">対応内容</param>
        /// <returns>更新結果</returns>
        public Boolean UpdateFollowData(String FMC_mc,   String KAIHATU_ID, String BY_PU,
                                        String EVENT_NO, String FOLLOW_NO,  String KA_CODE,     String SYSTEM_NO,
                                        String SINDO,    String TAIOU_NAIYO, String UserId)
        {
            Boolean result = false;

            // DBオープン
            DataAccess.Common.SqlCommon dbBase = new DataAccess.Common.SqlCommon();

            SqlConnection connDb = dbBase.dbOpen();

            //トランザクションの開始
            SqlTransaction sqlTran = connDb.BeginTransaction();

            try
            {
                // SQL作成
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connDb;
                cmd.Transaction = sqlTran;
                // 2016.04.20 Kanda 適用有無設計の更新を廃止
                // cmd.CommandText = "UPDATE T_FOLLOW_DATA SET TEKIYO_SEKKEI = @TEKIYO, SINDO = @SINDO, TAIOU_NAIYO = @TAIOU_NAIYO ,";
                cmd.CommandText = "UPDATE T_FOLLOW_DATA SET  SINDO = @SINDO, TAIOU_NAIYO = @TAIOU_NAIYO ,";
                cmd.CommandText += "UPDATE_USER = '" + UserId + "', UPDATE_YMD = getdate() ";
                cmd.CommandText += "WHERE FMC_mc = '" + FMC_mc + "' AND KAIHATU_ID = '" + KAIHATU_ID + "' AND ";
                cmd.CommandText += "BY_PU = '" + BY_PU + "' AND EVENT_NO = '" + EVENT_NO + "' AND ";
                cmd.CommandText += "FOLLOW_NO = '" + FOLLOW_NO + "' AND KA_CODE = '" + KA_CODE + "' ";
                cmd.CommandText += "AND SYSTEM_NO = " + SYSTEM_NO;
                // 2016.04.20 Kanda 適用有無設計の更新を廃止
                // cmd.Parameters.AddWithValue("@TEKIYO", TEKIYO);
                cmd.Parameters.AddWithValue("@SINDO", SINDO);
                cmd.Parameters.AddWithValue("@TAIOU_NAIYO", TAIOU_NAIYO);

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

    }
}