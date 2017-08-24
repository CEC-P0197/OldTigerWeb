using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using OldTigerWeb.BuisinessLogic;

namespace OldTigerWeb.DataAccess
{
    public class DADivisionSearch
    {
        #region "フィールド"
        /// <summary>
        /// SQLコネクションフィールド
        /// </summary>
        private SqlConnection Conn;
        /// <summary>
        /// SQLコマンドフィールド
        /// </summary>
        private SqlCommand cmd;
        #endregion

        #region "コンストラクタ"
        public DADivisionSearch()
        {
        }

        #endregion

        #region "メソッド"
        /// <summary>
        /// 部署情報取得（検索条件指定）
        /// </summary>
        /// <param name="structure">Trueの場合、構造体のみ返却</param>
        /// <returns></returns>
        public DataTable getDivisionInfo()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbwork = new StringBuilder();

            sb.AppendLine("SELECT ");
            sb.AppendLine("* ");
            sb.AppendLine("FROM ");
            sb.AppendLine("M_BUSYO_SEKKEI ");
            sb.AppendLine("WHERE ");
            sb.AppendLine("KA_CODE = null");
            sb.AppendLine("AND ");
            sb.AppendLine("BU_CODE = null");
            sb.AppendLine("AND ");
            sb.AppendLine("BY_PU = null");

            // SQL実行
            DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

            return dt;
        }


        /// <summary>
        /// 部署情報取得（検索条件指定）
        /// </summary>
        /// <param name="structure">Trueの場合、構造体のみ返却</param>
        /// <returns></returns>
        public DataTable getDivisionInfo(BLDivisionSearch parent)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbwork = new StringBuilder();
            StringBuilder sbwork2 = new StringBuilder();

            sb.AppendLine("SELECT ");
            sb.AppendLine("* ");
            sb.AppendLine("FROM ");
            sb.AppendLine("M_BUSYO_SEKKEI ");
            sb.AppendLine("WHERE ");

            if (parent.SearchPrmDivision != "")
            {
                sbwork.AppendLine("KA_CODE LIKE '@division%' ");
                sbwork = sbwork.Replace("@division", parent.SearchPrmDivision);
            }
            if (parent.SearchPrmDepartment != "")
            {
                if (sbwork.ToString() != "")
                {
                    sbwork.AppendLine("AND ");
                }
                sbwork.AppendLine("BU_CODE LIKE '@department%' ");
                sbwork = sbwork.Replace("@department", parent.SearchPrmDepartment);
            }

            sb.AppendLine(sbwork.ToString());

            sb.AppendLine("UNION ");
            sb.AppendLine("SELECT ");
            sb.AppendLine("* ");
            sb.AppendLine("FROM ");
            sb.AppendLine("M_BUSYO_HYOUKA ");
            sb.AppendLine("WHERE ");

            if (parent.SearchPrmDivision != "")
            {
                sbwork2.AppendLine("KA_CODE LIKE '@division%' ");
                sbwork2 = sbwork2.Replace("@division", parent.SearchPrmDivision);
            }
            if (parent.SearchPrmDepartment != "")
            {
                if (sbwork2.ToString() != "")
                {
                    sbwork2.AppendLine("AND ");
                }
                sbwork2.AppendLine("BU_CODE LIKE '@department%' ");
                sbwork2 = sbwork2.Replace("@department", parent.SearchPrmDepartment);
            }

            sb.AppendLine(sbwork2.ToString());

            // SQL実行
            DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

            DataColumn[] stringColumns =
                dt.Columns.Cast<DataColumn>()
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

        /// <summary>
        /// ユーザー情報取得（複数ユーザーiD指定）
        /// </summary>
        /// <param name="structure">Trueの場合、構造体のみ返却</param>
        /// <returns></returns>
        //public DataTable getUserInfo(List<string> userIdList)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    StringBuilder sbwork = new StringBuilder();

        //    sb.AppendLine("SELECT ");
        //    sb.AppendLine("* ");
        //    sb.AppendLine("FROM ");
        //    sb.AppendLine("M_USER ");
        //    sb.AppendLine("WHERE ");

        //    var parm = "";
        //    for(int i = 0;i < userIdList.Count; i++)
        //    {
        //        userIdList.CombineFromStringSingleQuotes(ref parm);
        //    }

        //    sb.AppendLine("USER_ID IN (@param)");
        //    sb = sb.Replace("@param", parm);

        //    sb.AppendLine(sbwork.ToString());

        //    // SQL実行
        //    DataTable dt = new SqlBridging().ExecuteReader(sb.ToString());

        //    DataColumn[] stringColumns =
        //        dt.Columns.Cast<DataColumn>()
        //            .Where(c => c.DataType == typeof(string))
        //            .ToArray();

        //    foreach (DataRow row in dt.Rows)
        //        foreach (DataColumn col in stringColumns)
        //        {
        //            if (row.Field<string>(col) == null) continue;
        //            row.SetField<string>(col, row.Field<string>(col).Trim());
        //        }

        //    return dt;
        //}



        #endregion

    }
}