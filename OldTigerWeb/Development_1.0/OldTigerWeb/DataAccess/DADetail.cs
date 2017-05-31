using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace OldTigerWeb.DataAccess
{
    public class DADetail
    {
        #region TOP40マスタ存在チェック
        /// <summary>
        /// TOP40マスタ存在チェック
        /// </summary>
        /// <param name="FollowNo">フォローNo</param>
        /// <returns>処理結果情報</returns>
        public Boolean SelectTop40(String followNo)
        {
            DataTable wkTable = new DataTable();
            Boolean result = false;

            // DBオープン
            DataAccess.Common.SqlCommon dbBase = new DataAccess.Common.SqlCommon();

            SqlConnection connDb = dbBase.dbOpen();

            try
            {
                // SQL作成
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connDb;
                cmd.CommandText = "SELECT FOLLOW_NO FROM M_TOP40 WHERE FOLLOW_NO = @follow_no";
                cmd.Parameters.AddWithValue("@follow_no", followNo);

                // コマンドを実行
                SqlDataReader reader = cmd.ExecuteReader();

                // SqlDataReader からデータを DataTable に読み込む
                wkTable.Load(reader);

                reader.Close();

                if (wkTable.Rows.Count > 0)
                {
                    result = true;      // 存在
                }
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
                wkTable.Dispose();
            }
        }

        #endregion

        #region リプロ20マスタ存在チェック
        /// <summary>
        /// リプロ20マスタ存在チェック
        /// </summary>
        /// <param name="FollowNo">フォローNo</param>
        /// <returns>処理結果情報</returns>
        public Boolean SelectRipro20(String followNo)
        {
            DataTable wkTable = new DataTable();
            Boolean result = false;

            // DBオープン
            DataAccess.Common.SqlCommon dbBase = new DataAccess.Common.SqlCommon();

            SqlConnection connDb = dbBase.dbOpen();

            try
            {
                // SQL作成
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connDb;
                cmd.CommandText = "SELECT FOLLOW_NO FROM M_RIPRO20 WHERE FOLLOW_NO = @follow_no";
                cmd.Parameters.AddWithValue("@follow_no", followNo);

                // コマンドを実行
                SqlDataReader reader = cmd.ExecuteReader();

                // SqlDataReader からデータを DataTable に読み込む
                wkTable.Load(reader);

                reader.Close();

                if (wkTable.Rows.Count > 0)
                {
                    result = true;      // 存在
                }
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
                wkTable.Dispose();
            }
        }

        #endregion
    }
}