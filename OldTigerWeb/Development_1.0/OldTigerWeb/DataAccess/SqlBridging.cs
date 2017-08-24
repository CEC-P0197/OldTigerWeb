using System.Data;
using System.Configuration;
using System.Data.SqlClient;
namespace OldTigerWeb.DataAccess
{ 
    public class SqlBridging : SqlService
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks></remarks>
        public SqlBridging()
        {
            GetConnection();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks></remarks>
        public SqlBridging(bool userTran)
        {
            GetConnection();
            this.Open();
        }

        /// <summary>
        /// 接続先を取得します。
        /// </summary>
        /// <remarks></remarks>
        public void GetConnection()
        {
            try
            {
                this.ConnectionString = ConfigurationManager.ConnectionStrings["OldTigerConnectionString"].ConnectionString;
            }
            catch 
            {
                if (this.ConnectionString == null)
                {
                    this.ConnectionString = Def.DefSETTING_DEFAULT_CONNECTION;
                }
                //AppException appException = new AppException();
                //// ＤＢ処理エラー
                //appException.ErrorCode = MessageCodeMSGCD_E0002;
                //throw appException;
            }
        }
    }
}