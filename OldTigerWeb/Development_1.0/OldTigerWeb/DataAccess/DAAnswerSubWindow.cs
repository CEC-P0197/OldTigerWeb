using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace OldTigerWeb.DataAccess
{
    public class DAAnswerSubWindow
    {
        #region フォロー回答詳細情報取得
        /// <summary>
        /// フォロー情報取得
        /// </summary>
        /// <param name="FMC_mc">FMC/mc区分</param>
        /// <param name="kaihatu_id">開発符号</param>
        /// <param name="by_pu">BYPU区分</param>
        /// <param name="event_no">イベントNO</param>
        /// <param name="follow_no">フォロー管理No</param>
        /// <param name="ka_code">課・主査コード</param>
        /// <param name="system_no">システム管理番号</param>
        /// <returns>取得結果情報</returns>
        public DataTable SelectFollowDataOtherDept(String FMC_mc, String kaihatu_id, String by_pu, String event_no,
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
                cmd.CommandText += "FLW.KA_CODE, FLW.SINDO, FLW.TAIOU_NAIYO ";
                cmd.CommandText += "FROM T_FOLLOW_DATA AS FLW ";
                cmd.CommandText += "LEFT OUTER JOIN M_BUSYO_SEKKEI AS SKK ";
                cmd.CommandText += "ON FLW.KA_CODE = SKK.KA_CODE ";
                cmd.CommandText += "LEFT OUTER JOIN M_BUSYO_HYOUKA AS HYK ";
                cmd.CommandText += "ON FLW.KA_CODE = HYK.KA_CODE ";
                cmd.CommandText += "WHERE FLW.FMC_mc = '" + FMC_mc + "' AND FLW.KAIHATU_ID = '" + kaihatu_id + "' AND FLW.BY_PU = '" + by_pu + "' AND FLW.EVENT_NO = '" + event_no + "' AND FLW.FOLLOW_NO = '" + follow_no + "' ";
                cmd.CommandText += "AND FLW.KA_CODE <> '" + ka_code + "' AND FLW.SYSTEM_NO = '" + system_no + "' ";
                cmd.CommandText += "ORDER BY  CASE WHEN SKK.KA_CODE IS NULL THEN 1 ELSE 0 END ";
                cmd.CommandText += ",FLW.KA_CODE ";
                cmd.CommandText += ",CASE WHEN HYK.KA_CODE IS NULL THEN 1 ELSE 0 END ";
                cmd.CommandText += ",HYK.KA_CODE;";
                
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


    }
}