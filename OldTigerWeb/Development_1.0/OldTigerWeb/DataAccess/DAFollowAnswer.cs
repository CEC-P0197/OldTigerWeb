using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace OldTigerWeb.DataAccess
{
    public class DAFollowAnswer
    {
        #region フォロー回答情報一覧取得
        /// <summary>
        /// フォロー情報取得
        /// </summary>
        /// <param name="FMC_mc">FMC/mc区分</param>
        /// <param name="Type">開発符号</param>
        /// <param name="Type">BYPU区分</param>
        /// <param name="Type">イベントNO</param>
        /// <param name="Type">課・主査コード</param>
        /// <returns>取得結果情報</returns>
        public DataTable SelectFollowDataList(String FMC_mc, String kaihatu_id, String by_pu, String event_no, String ka_code)
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
                cmd.CommandText += "CONVERT(varchar,ROW_NUMBER() OVER(ORDER BY FLW.BY_PU ASC, FLW.FOLLOW_NO ASC, TRB.FOLLOW_EDA DESC)) ";
                cmd.CommandText += " + '<br>' +TRB.RANK + '<br>' + TRB.SAIHATU + '<br>' + TRB.RSC + '<br>' + ";
                cmd.CommandText += "CASE WHEN RTRIM(SYUMU_SEIZO) = '○' AND RTRIM(SYUMU_GAISEI) != '○' THEN '製造' ";
                cmd.CommandText += "    ELSE CASE WHEN RTRIM(SYUMU_SEIZO) != '○' AND RTRIM(SYUMU_GAISEI) = '○' THEN '外製' ";
                cmd.CommandText += "        ELSE '設計' ";
                cmd.CommandText += "    END ";
                cmd.CommandText += "END AS RANK, ";
                cmd.CommandText += "CASE WHEN RTRIM(TRB.BUHIN_NAME1) <> '' THEN RTRIM(TRB.BUHIN_NAME1) ";
                cmd.CommandText += "ELSE";
                cmd.CommandText += "  RTRIM(TRB.BUHIN_NAME2) ";
                cmd.CommandText += "END AS BUHIN_NAME, ";            // 部品名
                cmd.CommandText += "TRB.BUNRUI_GENSYO_NAME, ";       // マスタの現象
                cmd.CommandText += "TRB.SEIGYO_FACTOR_NAME, ";       // マスタの制御系要因
                cmd.CommandText += "'(' + TRB.KOUMOKU_KANRI_NO + ')<br>' + TRB.KOUMOKU AS KOUMOKU, ";   // 項目管理番号+項目名
                cmd.CommandText += "TRB.GENIN, ";                    // 原因
                cmd.CommandText += "TRB.KAIHATU_MIHAKKEN_RIYU, ";    // 開発時の流出要因
                cmd.CommandText += "TRB.SQB_KANTEN, ";               // 確認の観点
                cmd.CommandText += "TRB.SAIHATU_SEKKEI, ";           // （再発防止策）設計面
                cmd.CommandText += "TRB.SAIHATU_HYOUKA, ";           // （再発防止策）評価面
                cmd.CommandText += "FLW.TEKIYO_SQB, FLW.TEKIYO_SEKKEI, FLW.HEARING, FLW.SINDO, FLW.TAIOU_NAIYO, ";  // フォロー情報
                cmd.CommandText += "FLW.FMC_mc + ',' + FLW.KAIHATU_ID + ',' + FLW.BY_PU + ',' + FLW.EVENT_NO + ',' + FLW.FOLLOW_NO + ',' + FLW.KA_CODE + ',' + CONVERT(VARCHAR, FLW.SYSTEM_NO) AS FOLLOW_KEY ";
                cmd.CommandText += "FROM T_FOLLOW_DATA AS FLW ";
                cmd.CommandText += "INNER JOIN T_TROUBLE_DATA AS TRB ";
                //cmd.CommandText += "ON FLW.SYSTEM_NO = TRB.SYSTEM_NO AND TRB.SAIHATU <> '類似' ";
                cmd.CommandText += "ON FLW.SYSTEM_NO = TRB.SYSTEM_NO ";
                cmd.CommandText += "WHERE FLW.FMC_mc = '" + FMC_mc + "' AND FLW.KAIHATU_ID = '" + kaihatu_id + "' AND FLW.EVENT_NO = '" + event_no + "' AND FLW.KA_CODE = '" + ka_code + "' ";
                cmd.CommandText += " AND FLW.TEKIYO_SQB = '*' AND FLW.TEKIYO_SEKKEI = '*' ";    // 20160322 INS フォロー展開コピー対応
                cmd.CommandText += "ORDER BY FLW.BY_PU ASC, FLW.FOLLOW_NO ASC, TRB.FOLLOW_EDA DESC";

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

        // 2017/07/14 Add Start
        #region フォロー対象部署一覧取得
        /// <summary>
        /// フォロー対象部署一覧取得
        /// </summary>
        /// <param name="FMC_mc">FMC/mc区分</param>
        /// <param name="Type">開発符号</param>
        /// <param name="Type">BYPU区分</param>
        /// <param name="Type">イベントNO</param>
        /// <returns>取得結果情報</returns>
        public DataTable selectKaCodeFollowDataList(String FMC_mc, String kaihatu_id, String by_pu, String event_no)
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
                cmd.CommandText += "DISTINCT(KA_CODE) AS KA_CODE ";  // 課コード
                cmd.CommandText += "FROM T_FOLLOW_DATA AS FLW ";
                cmd.CommandText += "WHERE FLW.FMC_mc = '" + FMC_mc + "' AND FLW.KAIHATU_ID = '" + kaihatu_id + "' AND FLW.EVENT_NO = '" + event_no + "' ";
                cmd.CommandText += " AND FLW.TEKIYO_SQB = '*' AND FLW.TEKIYO_SEKKEI = '*' ";    // 20160322 INS フォロー展開コピー対応
                cmd.CommandText += "ORDER BY FLW.KA_CODE ASC";

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

    }
}