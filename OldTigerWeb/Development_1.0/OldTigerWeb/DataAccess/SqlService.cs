using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Transactions;

namespace OldTigerWeb.DataAccess
{
    public class SqlService
    {

        /// <summary>
        /// 接続文字列
        /// </summary>
        /// <remarks></remarks>
        private string _cn;

        /// <summary>
        /// Sqlコマンド
        /// </summary>
        /// <remarks></remarks>
        private SqlCommand _sqlCmd = new SqlCommand();

        /// <summary>
        /// コネクション
        /// </summary>
        /// <remarks></remarks>
        private SqlConnection _sqlCn = new SqlConnection();

        /// <summary>
        /// トランザクション
        /// </summary>
        /// <remarks></remarks>
        private SqlTransaction _sqlTran;

        /// <summary>
        /// 接続文字列の取得、または設定を行います。
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string ConnectionString
        {
            get { return _cn; }
            set { _cn = value; }
        }

        /// <summary>
        /// 接続を開きます。
        /// </summary>
        /// <remarks></remarks>
        public void Open()
        {
            try
            {
                if (_sqlCn.State != ConnectionState.Open)
                {
                    _sqlCn.ConnectionString = this.ConnectionString;
                    _sqlCn.Open();
                }

                if (_sqlTran == null)
                {
                    _sqlTran = _sqlCn.BeginTransaction();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {

                _sqlCn.Close();
                _sqlCn.Dispose();
            }
        }

        /// <summary>
        /// 解放処理を行います。
        /// </summary>
        /// <remarks></remarks>
        public void Dispose()
        {
            try
            {
                if ((_sqlCmd != null))
                {
                    _sqlCmd.Dispose();
                    _sqlCmd = null;
                }

                if ((_sqlTran != null))
                {
                    _sqlTran.Dispose();
                    _sqlTran = null;
                }

                if ((_sqlCn != null))
                {
                    if (_sqlCn.State == ConnectionState.Open)
                    {
                        _sqlCn.Close();
                        _sqlCn.Dispose();
                    }

                    _sqlCn = null;
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// コミットを行います。
        /// </summary>
        /// <remarks></remarks>
        public void Commit()
        {
            if ((_sqlTran != null))
            {
                _sqlTran.Commit();
            }
        }

        /// <summary>
        /// ロールバックを行います。
        /// </summary>
        /// <remarks></remarks>
        public void Rollback()
        {
            if ((_sqlTran != null))
            {
                _sqlTran.Rollback();
            }
        }

        /// <summary>
        /// Sqlコマンドにパラメータを追加します。
        /// </summary>
        /// <param name="query">実行するクエリを指定します。</param>
        /// <param name="collection">実行するクエリのパラメータを指定します。</param>
        /// <param name="sqlCn">コネクションを指定します。</param>
        /// <returns>Sqlコマンドを返します。</returns>
        private SqlCommand GetSqlCommand(string query, SqlParamCollection collection, SqlConnection sqlCn)
        {
            string queryString = query;
            SqlCommand sqlCmd = new SqlCommand(queryString, sqlCn, _sqlTran);

            if ((collection != null))
            {
                for (int i = 0; i < collection.Count; i++)
                {
                    SqlParam param = collection[i];
                    sqlCmd.Parameters.Add(param.ParamName, param.SqlDbType, param.Size);
                    sqlCmd.Parameters[param.ParamName].Value = param.ParamValue;
                }
            }

            return sqlCmd;
        }

        /// <summary>
        /// Sqlコマンドを取得します。
        /// </summary>
        /// <returns>Sqlコマンドを返します。</returns>
        public SqlCommand GetSqlCommand(string query)
        {
            string queryString = query;
            SqlCommand sqlCmd = new SqlCommand();

                //DBへアクセスする
                if (_sqlCn.State != ConnectionState.Open)
                {
                    _sqlCn.ConnectionString = this.ConnectionString;
                    _sqlCn.Open();
                }

                sqlCmd = new SqlCommand(queryString, _sqlCn);
                return sqlCmd;
        }

        /// <summary>
        /// データベースへアクセスし、DataTableを返します。
        /// </summary>
        /// <param name="query"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public DataTable ExecuteReader(string query)
        {
            //返却用DataTable
            DataTable dt = new DataTable();
            try
            {
                //DBへアクセスする
                if (_sqlCn.State != ConnectionState.Open)
                {
                    _sqlCn.ConnectionString = this.ConnectionString;
                    _sqlCn.Open();
                }
                SqlCommand sqlCmd = GetSqlCommand(query);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd);

                //CommonLogic bcom = new CommonLogic();
                //DebugParameter dp = new DebugParameter();
                //dp.Rank = 9;
                //dp.FileName = System.IO.Path.GetFileName(this.GetType().Assembly.Location);
                //dp.ClassName = this.GetType().FullName;
                //dp.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                //dp.Title = "";
                //dp.Content = query;
                //bcom.DebugProcess(dp);

                sqlAdapter.Fill(dt);
                sqlAdapter.Dispose();
                sqlCmd.Dispose();


            }
            catch (Exception ex)
            {

            }
            finally
            {
                _sqlCn.Close();
                _sqlCn.Dispose();
            }
            return dt;
        }

        /// <summary>
        /// データベースへアクセスし、DataTableを返します。
        /// </summary>
        /// <param name="query">実行するクエリを指定します。</param>
        /// <param name="collection">実行するクエリのパラメータを指定します。</param>
        /// <returns>取得データ</returns>
        public DataTable ExecuteReader(string query, SqlParamCollection collection = null)
        {

            //返却用DataTable
            DataTable dt = new DataTable();

            try
            {
                //DBへアクセスする
                if (_sqlCn.State != ConnectionState.Open)
                {
                    _sqlCn.ConnectionString = this.ConnectionString;
                    _sqlCn.Open();
                }

                SqlCommand sqlCmd = GetSqlCommand(query, collection, _sqlCn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd);

                sqlAdapter.Fill(dt);
                sqlAdapter.Dispose();
                sqlCmd.Dispose();

            }
            finally
            {

                _sqlCn.Close();
                GC.SuppressFinalize(this);
                _sqlCn.Dispose();
            }
            return dt;
        }

        /// <summary>
        /// データベースへアクセスし、DataTableを返します。
        /// </summary>
        /// <param name="sqlCmd">実行するSQLコマンドを指定します。</param>
        /// <returns>取得データ</returns>
        public DataTable ExecuteReader(SqlCommand sqlCmd)
        {

            //返却用DataTable
            DataTable dt = new DataTable();

            try
            {
                //DBへアクセスする
                if (_sqlCn.State != ConnectionState.Open)
                {
                    _sqlCn.ConnectionString = this.ConnectionString;
                    _sqlCn.Open();
                }

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd);

                sqlAdapter.Fill(dt);
                sqlAdapter.Dispose();
                sqlCmd.Dispose();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                _sqlCn.Close();
                _sqlCn.Dispose();
            }
            return dt;
        }

        /// <summary>
        /// データベースへアクセスし、queryを実行します。
        /// </summary>
        /// <param name="query">実行するクエリを指定します。</param>
        /// <param name="collection">実行するクエリのパラメータを指定します。</param>
        /// <returns>実行結果の件数</returns>
        public int ExecuteNonQuery(string query, SqlParamCollection collection = null)
        {
            //返却件数
            int result = 0;

            try
            {
                //トランザクションを開く
                using (TransactionScope tScope = new TransactionScope())
                {

                    //DBへアクセスする
                    if (_sqlCn.State != ConnectionState.Open)
                    {
                        _sqlCn.ConnectionString = this.ConnectionString;
                        _sqlCn.Open();
                    }

                    SqlCommand sqlCmd = GetSqlCommand(query, collection, _sqlCn);
                    result = sqlCmd.ExecuteNonQuery();

                    //コミット
                    tScope.Complete();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                _sqlCn.Close();
                _sqlCn.Dispose();
            }
            return result;
        }

        /// <summary>
        /// データベースへアクセスし、queryを実行します。
        /// </summary>
        /// <param name="sqlCmd">実行するSQLコマンドを指定します。</param>
        /// <returns>実行結果の件数</returns>
        public int ExecuteNonQuery(SqlCommand sqlCmd)
        {
            //返却件数
            int result = 0;

            try
            {
                //トランザクションを開く
                using (TransactionScope tScope = new TransactionScope())
                {
                    //DBへアクセスする
                    if (_sqlCn.State != ConnectionState.Open)
                    {
                        _sqlCn.ConnectionString = this.ConnectionString;
                        _sqlCn.Open();
                    }

                    result = sqlCmd.ExecuteNonQuery();

                    //コミット
                    tScope.Complete();
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                _sqlCn.Close();
                _sqlCn.Dispose();
            }

            return result;
        }

        /// <summary>
        /// データベースへアクセスし、queryを実行します。
        /// </summary>
        /// <param name="query">実行するクエリを指定します。</param>
        /// <param name="collection">実行するクエリのパラメータを指定します。</param>
        /// <returns>実行結果の件数</returns>
        public int ExecuteNonQueryTran(string query, SqlParamCollection collection = null)
        {

            //返却件数
            int result = 0;

            try
            {
                SqlCommand sqlCmd = GetSqlCommand(query, collection, _sqlCn);
                result = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }

            return result;

        }

        /// <summary>
        /// データベースへアクセスし、queryを実行します。
        /// </summary>
        /// <param name="sqlCmd">実行するSQLコマンドを指定します。</param>
        /// <returns>実行結果の件数</returns>
        public int ExecuteNonQueryTran(SqlCommand sqlCmd)
        {

            //返却件数
            int result = 0;

            try
            {
                result = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// データが存在するか調べます。（true(データ有／false(データ無))）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsExists(string query, SqlParamCollection collection = null)
        {
                DataTable dt = this.ExecuteReader(query, collection);
                return !(dt.Rows.Count == 0);
        }


        /// <summary>
        /// データが存在するか調べます。（true(データ有／false(データ無))）
        /// </summary>
        /// <param name="sqlCmd">実行するSQLコマンドを指定します。</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsExists(SqlCommand sqlCmd)
        {
            try
            {
                DataTable dt = this.ExecuteReader(sqlCmd);
                return !(dt.Rows.Count == 0);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
