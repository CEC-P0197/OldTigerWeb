using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Text;

namespace OldTigerWeb.DataAccess
{
    public class DASearch
    {
        #region キーワードリスト情報取得
        /// <summary>
        /// キーワードリスト情報取得
        /// </summary>
        /// <param name="">無し</param>
        /// <returns>処理結果情報</returns>
        public DataTable SelectKeyWordList()
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
                cmd.CommandText = "SELECT WORD_CHAR, WORD_KEY FROM M_KEYWORD ORDER BY SEQ";

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

        #region おすすめTOP10情報取得
        /// <summary>
        /// おすすめTOP10情報取得
        /// </summary>
        /// <param name="">無し</param>
        /// <returns>処理結果情報</returns>
        public DataTable SelectRecommendList()
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
                cmd.CommandText = "SELECT RECOMMEND_WORD FROM M_RECOMMEND ORDER BY SEQ";

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

        #region 部品・部位情報取得
        /// <summary>
        /// 部品・部位情報取得
        /// </summary>
        /// <param name="BY">BY</param>
        /// <param name="PU">PU</param>
        /// <param name="dtSystem">システム選択リスト</param>
        /// <returns>処理結果情報</returns>
        public DataTable SelectPartsList(String BY, String PU, ArrayList dtSystem)
        {
            DataTable result = new DataTable();

            // DBオープン
            DataAccess.Common.SqlCommon dbBase = new DataAccess.Common.SqlCommon();

            SqlConnection connDb = dbBase.dbOpen();

            try
            {
                String where = "";

                // SQL作成
                if (BY == "1" && PU == "1")
                {
                    where = "BY_PU IN ('BY','PU')";
                }
                else
                {
                    if (BY == "1" && PU == "0")
                    {
                        where = "BY_PU = 'BY'";
                    }
                    else
                    {
                        where = "BY_PU = 'PU'";
                    }
                }

                where += " AND RTRIM(BUHIN_NO) <> '' AND SYSTEM_NO IN (";

                String[] strArrayData;

                for (int i = 0; i < dtSystem.Count; i++)
                {
                    if (i != 0) where += ",";
                    strArrayData = dtSystem[i].ToString().Trim().Split(',');
                    where += "'" + strArrayData[0].ToString() + "'";
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connDb;
                cmd.CommandText = "SELECT ";
                cmd.CommandText += "CASE ";
                cmd.CommandText += "WHEN RTRIM(KOBUHIN_NO) = '' THEN";
                cmd.CommandText += "  RTRIM(SYSTEM_NO) + ',' + RTRIM(BUHIN_NO) + ',,' + RTRIM(SYSTEM_NAME) + '／' + RTRIM(BUHIN_NAME)";
                cmd.CommandText += "ELSE";
                // 20170304 START k-ohmatsuzawa　部品検索結果の"｜"を"／"に変更
                //cmd.CommandText += "  RTRIM(SYSTEM_NO) + ',' + RTRIM(BUHIN_NO) + ',' + RTRIM(KOBUHIN_NO) + ',' + RTRIM(SYSTEM_NAME) + '／' + RTRIM(BUHIN_NAME) + '｜' + RTRIM(KOBUHIN_NAME)";
                cmd.CommandText += "  RTRIM(SYSTEM_NO) + ',' + RTRIM(BUHIN_NO) + ',' + RTRIM(KOBUHIN_NO) + ',' + RTRIM(SYSTEM_NAME) + '／' + RTRIM(BUHIN_NAME) + '／' + RTRIM(KOBUHIN_NAME)";
                // 20170304 END k-ohmatsuzawa
                cmd.CommandText += "END AS ID,";
                cmd.CommandText += "CASE ";
                cmd.CommandText += "WHEN RTRIM(KOBUHIN_NO) = '' THEN";
                cmd.CommandText += "  RTRIM(SYSTEM_NAME) + '／' + RTRIM(BUHIN_NAME) ";
                cmd.CommandText += "ELSE";
                cmd.CommandText += "  RTRIM(SYSTEM_NAME) + '／' + RTRIM(BUHIN_NAME) + '／' + RTRIM(KOBUHIN_NAME) ";
                cmd.CommandText += "END AS NAME ";
                cmd.CommandText += "FROM M_PARTS ";
                cmd.CommandText += "WHERE " + where;
                cmd.CommandText += ") ORDER BY ID;";

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

        #region 各マスタ情報取得
        /// <summary>
        /// 各マスタ情報取得
        /// </summary>
        /// <param name="Type">種類</param>
        /// <param name="BYPU">部署表示用BY/PU</param>
        /// <returns>処理結果情報</returns>
        public DataTable SelectMasterList(String Type, String BYPU)
        {
            DataTable result = new DataTable();

            String strSql = "";

            switch (Type)
            {
                case "31":
                    // 部署・設計
                    strSql = "SELECT case when RTRIM(KA_CODE) = RTRIM(BU_CODE) then RTRIM(BU_CODE) else RTRIM(KA_CODE) end as ID,";
                    strSql += " case when RTRIM(KA_CODE) = RTRIM(BU_CODE) then RTRIM(BU_CODE) else RTRIM(BU_CODE) + '／' + RTRIM(KA_CODE) end as NAME";
                    strSql += " FROM M_BUSYO_SEKKEI WHERE BY_PU = '" + BYPU + "' ORDER BY SEQ";
                    break;
                case "32":
                    // 部署・評価
                    strSql = "SELECT case when RTRIM(KA_CODE) = RTRIM(BU_CODE) then RTRIM(BU_CODE) else RTRIM(KA_CODE) end as ID,";
                    strSql += " case when RTRIM(KA_CODE) = RTRIM(BU_CODE) then RTRIM(BU_CODE) else RTRIM(BU_CODE) + '／' + RTRIM(KA_CODE) end as NAME";
                    strSql += " FROM M_BUSYO_HYOUKA WHERE BY_PU = '" + BYPU + "' ORDER BY SEQ";
                    break;
                case Def.DefTYPE_PARTS:
                    // 部品・部位 システム
                    strSql = "SELECT ";
                    strSql += "DISTINCT RTRIM(SYSTEM_NO) AS SYSTEM_NO, ";
                    strSql += "RTRIM(SYSTEM_NO) + ',,,' + RTRIM(SYSTEM_NAME) AS ID, ";
                    strSql += "RTRIM(SYSTEM_NAME) AS NAME ";
                    strSql += "FROM M_PARTS ";
                    strSql += "ORDER BY SYSTEM_NO;";
                    break;
                case Def.DefTYPE_KAIHATU:
                    // 開発符号
                    strSql = "SELECT KAIHATU_ID + ',' + KAIHATU_FUGO AS ID, SYAKEI + '／' + KAIHATU_FUGO AS NAME";
                    strSql += " FROM M_DEVELOPMENTSIGN ";
                    strSql += " WHERE FMC_MC = '1' ORDER BY SEQ";
                    break;
                case Def.DefTYPE_GENSYO:
                case Def.DefTYPE_SGENSYO:
                    // 現象（分類）
                    // 現象（制御系）
                    strSql = "SELECT GENSYO_NO + ',' + GENSYO_NAME AS ID, GENSYO_NAME AS NAME";
                    strSql += " FROM M_GENSYO ORDER BY SEQ";
                    break;
                case Def.DefTYPE_GENIN:
                    // 原因（分類）
                    strSql = "SELECT CASE_NO + ',' + CASE_NAME AS ID, CASE_NAME AS NAME";
                    strSql += " FROM M_CASE ORDER BY SEQ";
                    break;
                case Def.DefTYPE_SYAKATA:
                    // 車型特殊
                    strSql = "SELECT KATA_NO + ',' + KATA_NAME AS ID, KATA_NAME AS NAME";
                    strSql += " FROM M_SYAKATA ORDER BY SEQ";
                    break;
                case Def.DefTYPE_SYOUIN:
                    // 要因（制御系）
                    strSql = "SELECT FACTOR_NO + ',' + FACTOR_NAME AS ID, FACTOR_NAME AS NAME";
                    strSql += " FROM M_FACTOR ORDER BY SEQ";
                    break;
                case Def.DefTYPE_EGTM:
                    // EGTM形式
                    strSql = "SELECT EGTM_NO + ',' + EGTM_NAME AS ID, EGTM_NAME AS NAME";
                    strSql += " FROM M_EGTM ORDER BY SEQ";
                    break;
                case Def.DefTYPE_TOP40:
                    // TOP40
                    strSql = "SELECT FOLLOW_NO AS ID, FOLLOW_NO AS NAME";
                    strSql += " FROM M_TOP40 ORDER BY SEQ";
                    break;
                case Def.DefTYPE_RIPRO20:
                    // リプロ20
                    strSql = "SELECT FOLLOW_NO AS ID, FOLLOW_NO AS NAME";
                    strSql += " FROM M_RIPRO20 ORDER BY SEQ";
                    break;
            }


            // DBオープン
            DataAccess.Common.SqlCommon dbBase = new DataAccess.Common.SqlCommon();

            SqlConnection connDb = dbBase.dbOpen();

            try
            {
                // SQL作成
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connDb;
                cmd.CommandText = strSql;

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