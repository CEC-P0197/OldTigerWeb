using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace OldTigerWeb.DataAccess
{
    public class DAFollow
    {
        #region フォロー情報取得
        /// <summary>
        /// フォロー情報取得
        /// </summary>
        /// <param name="FMC_mc">FMC/mc 1:FMC、2:mc</param>
        /// <param name="Type">種類 1:現在、2:過去</param>
        /// <returns>取得結果情報</returns>
        public DataTable SelectFollowList(String FMC_mc, String Type)
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
                cmd.CommandText = "SELECT RTRIM(a.FMC_mc) + ',' + RTRIM(a.KAIHATU_ID) + ','";
                cmd.CommandText += " + RTRIM(a.BY_PU) + ',' + RTRIM(a.EVENT_NO) + ',' + ";
                cmd.CommandText += "RTRIM(a.BY_PU) + '-' + ";
                cmd.CommandText += "CASE WHEN RTRIM(a.FMC_mc) = '1' THEN 'FMC' ";
                cmd.CommandText += "ELSE 'mc' END + '-' + ";
                cmd.CommandText += "RTRIM(b.KAIHATU_FUGO) + '-' + RTRIM(c.EVENT_NAME) + '-'";
                cmd.CommandText += " + CONVERT(VARCHAR, a.YMD_KAISI,111) + '～'";
                cmd.CommandText += " + CONVERT(VARCHAR, a.YMD_END,111) + ',' + ";
                cmd.CommandText += "RTRIM(c.EVENT_NAME) + ',' + a.TENKAI_KBN AS EVENT_CODE, "; // 2017/07/14 Add TENKAI_KBN追加
                cmd.CommandText += "RTRIM(a.BY_PU) + '-' + RTRIM(b.KAIHATU_FUGO) + '-' + RTRIM(c.EVENT_NAME) + '-'";
                cmd.CommandText += " + CONVERT(VARCHAR, a.YMD_KAISI,111) + '～'";
                cmd.CommandText += " + CONVERT(VARCHAR, a.YMD_END,111) AS FOLLOW_NAME ";
                cmd.CommandText += ", b.SEQ AS FUGO_SEQ, c.SEQ AS EVENT_SEQ ";
                cmd.CommandText += "FROM T_EVENT_DATA AS a ";
                cmd.CommandText += "LEFT JOIN M_DEVELOPMENTSIGN AS b ";
                cmd.CommandText += "ON a.KAIHATU_ID = b.KAIHATU_ID ";
                cmd.CommandText += "LEFT JOIN M_EVENT AS c ";
                cmd.CommandText += "ON a.EVENT_NO = c.EVENT_NO ";
                cmd.CommandText += "WHERE a.FMC_mc = '" + FMC_mc + "' AND ";

                // 現在情報取得
                if (Type == Const.Def.DefTYPE_Now)
                {
                    cmd.CommandText += "CONVERT(VARCHAR, a.YMD_KAISI,111) <= CONVERT(VARCHAR,CURRENT_TIMESTAMP,111) AND ";
                    cmd.CommandText += "CONVERT(VARCHAR, a.YMD_END,111) >= CONVERT(VARCHAR,CURRENT_TIMESTAMP,111) ";
                }
                // 過去情報取得
                else
                {
                    cmd.CommandText += "CONVERT(VARCHAR, a.YMD_END,111) < CONVERT(VARCHAR,CURRENT_TIMESTAMP,111) ";
                }

                cmd.CommandText += "ORDER BY a.BY_PU, FUGO_SEQ, EVENT_SEQ, a.YMD_KAISI";

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

        #region フォローダウンロード情報取得
        /// <summary>
        /// フォローダウンロード情報取得
        /// </summary>
        /// <param name="FMC_mc">FMC/mc区分</param>
        /// <param name="Type">開発符号</param>
        /// <param name="Type">BYPU区分</param>
        /// <param name="Type">イベントNO</param>
        /// <param name="Type">課・主査コード</param>
        /// <returns>取得結果情報</returns>
        public DataTable SelectFollowDownList(String FMC_mc, String kaihatu_id, String by_pu, String event_no, String ka_code)
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
                cmd.CommandText = "SELECT CONVERT(varchar,ROW_NUMBER() OVER(ORDER BY K.BY_PU ASC, K.FOLLOW_NO ASC, K.FOLLOW_EDA DESC, K.KA_CODE  ASC, SYSTEM_NO ASC)) AS ROWID,";
                cmd.CommandText += " K.RANK, K.SAIHATU, K.RSC,";    // ランク、再発、RSC
                cmd.CommandText += "CASE WHEN RTRIM(SYUMU_SEIZO) = '○' AND RTRIM(SYUMU_GAISEI) != '○' THEN '製造' ";
                cmd.CommandText += "    ELSE CASE WHEN RTRIM(SYUMU_SEIZO) != '○' AND RTRIM(SYUMU_GAISEI) = '○' THEN '外製' ";
                cmd.CommandText += "        ELSE '設計' ";
                cmd.CommandText += "    END ";
                cmd.CommandText += "END AS SYUMU, ";                // 主務部署
                cmd.CommandText += "CASE WHEN RTRIM(K.BUHIN_NAME1) <> '' THEN RTRIM(K.BUHIN_NAME1) ";
                cmd.CommandText += "ELSE";
                cmd.CommandText += "  RTRIM(K.BUHIN_NAME2) ";
                cmd.CommandText += "END AS BUHIN_NAME, ";           // 部品名
                cmd.CommandText += "K.BUNRUI_GENSYO_NAME, ";        // マスタの現象
                cmd.CommandText += "K.SEIGYO_FACTOR_NAME, ";        // マスタの制御系要因
                cmd.CommandText += "K.KOUMOKU_KANRI_NO, K.KOUMOKU, ";   // 項目管理番号、項目
                cmd.CommandText += "K.GENIN, K.FOLLOW_INFO, ";      // 原因、進捗
                cmd.CommandText += "K.TAISAKU, ";                   // 対策
                cmd.CommandText += "K.KAIHATU_MIHAKKEN_RIYU, ";     // 開発時の流出要因
                cmd.CommandText += "K.SQB_KANTEN, ";                // 確認の観点
                cmd.CommandText += "K.SAIHATU_SEKKEI, ";            // （再発防止策）設計面
                cmd.CommandText += "K.SAIHATU_HYOUKA, ";            // （再発防止策）評価面
                cmd.CommandText += "K.SIRYOU_NO1, K.SIRYOU_NO2, K.SIRYOU_NO3, K.SIRYOU_NO4, K.SIRYOU_NO5, K.KANREN_KANRI_NO, "; // 資料№一覧、関連管理No
                cmd.CommandText += "K.KA_CODE, K.SORT_KA_CODE, ";   // フォロー情報・課コード
                cmd.CommandText += "K.TEKIYO_SQB, K.TEKIYO_SEKKEI, K.HEARING, K.SINDO, K.TAIOU_NAIYO, ";      // フォロー情報
                cmd.CommandText += "K.FOLLOW_NO, CONVERT(VARCHAR, K.SYSTEM_NO) AS SYSTEM_NO ";  
                cmd.CommandText += "FROM (";

                cmd.CommandText += "SELECT TRB.RANK, TRB.SAIHATU, TRB.RSC,TRB.SYUMU_SEIZO, TRB.SYUMU_GAISEI, TRB.BUHIN_NAME1, TRB.BUHIN_NAME2, TRB.BUNRUI_GENSYO_NAME, TRB.SEIGYO_FACTOR_NAME, TRB.KOUMOKU_KANRI_NO, TRB.KOUMOKU, TRB.GENIN, TRB.FOLLOW_INFO, TRB.TAISAKU, TRB.KAIHATU_MIHAKKEN_RIYU, TRB.SQB_KANTEN, TRB.SAIHATU_SEKKEI, TRB.SAIHATU_HYOUKA, TRB.SIRYOU_NO1, TRB.SIRYOU_NO2, TRB.SIRYOU_NO3, TRB.SIRYOU_NO4, TRB.SIRYOU_NO5, TRB.KANREN_KANRI_NO, FLW.KA_CODE, FLW.KA_CODE AS SORT_KA_CODE, FLW.TEKIYO_SQB, FLW.TEKIYO_SEKKEI, FLW.HEARING, FLW.SINDO, FLW.TAIOU_NAIYO, FLW.FOLLOW_NO, FLW.FOLLOW_EDA, FLW.BY_PU, CONVERT(VARCHAR, FLW.SYSTEM_NO) AS SYSTEM_NO ";
                cmd.CommandText += "FROM T_FOLLOW_DATA AS FLW ";
                cmd.CommandText += "INNER JOIN T_TROUBLE_DATA AS TRB ";
                //cmd.CommandText += "ON FLW.SYSTEM_NO = TRB.SYSTEM_NO AND TRB.SAIHATU <> '類似' ";
                cmd.CommandText += "ON FLW.SYSTEM_NO = TRB.SYSTEM_NO ";
                cmd.CommandText += "WHERE FLW.FMC_mc = '" + FMC_mc + "' AND FLW.KAIHATU_ID = '" + kaihatu_id + "' AND FLW.EVENT_NO = '" + event_no + "' ";
                cmd.CommandText += " AND FLW.TEKIYO_SQB = '*' AND FLW.TEKIYO_SEKKEI = '*' ";    // 20160322 INS フォロー展開コピー対応

                if (ka_code != "ALL")       // 個別課指定
                {
                    cmd.CommandText += "AND FLW.KA_CODE = '" + ka_code + "' ";
                }

                cmd.CommandText += "UNION ";
                cmd.CommandText += "SELECT TB2.RANK, TB2.SAIHATU, TB2.RSC,TB2.SYUMU_SEIZO, TB2.SYUMU_GAISEI, TB2.BUHIN_NAME1, TB2.BUHIN_NAME2, TB2.BUNRUI_GENSYO_NAME, TB2.SEIGYO_FACTOR_NAME, TB2.KOUMOKU_KANRI_NO, TB2.KOUMOKU, TB2.GENIN, TB2.FOLLOW_INFO, TB2.TAISAKU, TB2.KAIHATU_MIHAKKEN_RIYU, TB2.SQB_KANTEN, TB2.SAIHATU_SEKKEI, TB2.SAIHATU_HYOUKA, TB2.SIRYOU_NO1, TB2.SIRYOU_NO2, TB2.SIRYOU_NO3, TB2.SIRYOU_NO4, TB2.SIRYOU_NO5, TB2.KANREN_KANRI_NO, '' AS KA_CODE ,'ZZZZZ' AS SORT_KA_CODE, '' AS TEKIYO_SQB, '' AS TEKIYO_SEKKEI, '' AS HEARING, '' AS SINDO, '' AS TAIOU_NAIYO, TB2.FOLLOW_NO, TB2.FOLLOW_EDA, TB2.BY_PU, CONVERT(VARCHAR, TB2.SYSTEM_NO) AS SYSTEM_NO ";
                cmd.CommandText += "FROM T_TROUBLE_DATA AS TB2 ";
                cmd.CommandText += "INNER JOIN (";
                cmd.CommandText += "SELECT FOLLOW_NO, FOLLOW_EDA FROM T_FOLLOW_DATA ";
                cmd.CommandText += "WHERE  FMC_mc = '" + FMC_mc + "' AND KAIHATU_ID = '" + kaihatu_id + "' AND EVENT_NO = '" + event_no + "' ";
                cmd.CommandText += " AND TEKIYO_SQB = '*' AND TEKIYO_SEKKEI = '*' ";    // 20160322 INS フォロー展開コピー対応

                if (ka_code != "ALL")       // 個別課指定
                {
                    cmd.CommandText += "AND KA_CODE = '" + ka_code + "' ";
                }
                cmd.CommandText += "GROUP BY FOLLOW_NO ,FOLLOW_EDA) AS FL2 ";
                cmd.CommandText += "ON FL2.FOLLOW_NO = TB2.FOLLOW_NO AND FL2.FOLLOW_EDA > TB2.FOLLOW_EDA";
                cmd.CommandText += ") AS K ";
                //cmd.CommandText += "ORDER BY K.FOLLOW_NO, K.FOLLOW_EDA DESC, K.SORT_KA_CODE";
                cmd.CommandText += "ORDER BY K.BY_PU ASC, K.FOLLOW_NO ASC, K.FOLLOW_EDA DESC, K.KA_CODE  ASC, SYSTEM_NO ASC ";
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