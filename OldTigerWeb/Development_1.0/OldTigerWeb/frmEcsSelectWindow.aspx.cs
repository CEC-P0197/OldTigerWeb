using System;
using System.Net;
using System.Data;
using OldTigerWeb.BuisinessLogic;

namespace OldTigerWeb
{
    public partial class frmEcsSelectWindow : System.Web.UI.Page
    {
        CommonLogic bcom = new CommonLogic();

        #region "フィールド"
        // 対象設筒番号保持フィールド（クエリパラメータで取得）
        private string _TargetEcsNo;
        // 図面選択リスト（指定した設通に含まれる図面×改訂番号のリスト）
        private DataTable _EcsContainDrawingList;
        // ログインユーザー
        public string sUser;
        // IPアドレス
        public string sIPAddress;
        
        /// <summary>
        /// 設通選択画面　ビジネスロジック
        /// </summary>
        private BLEcsSelectWindow  _blEcsSelectWindow;
        #endregion

        #region プロパティ
        /// <summary>
        /// 図面改訂リスト
        /// </summary>
        public  DataTable EcsContainList
        {
            get {return _EcsContainDrawingList; }
        }
        #endregion

        #region メソッド
        /// <summary>
        /// 初期表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // ユーザーIDの取得
            sUser = bcom.GetWindowsUser();

            // IPアドレスの取得する
            var xForwardedFor = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            string ipv4;
            if (String.IsNullOrEmpty(xForwardedFor) == false)
            {
                ipv4 = xForwardedFor.Split(',').GetValue(0).ToString().Trim();
            }
            else
            {
                ipv4 = Request.UserHostAddress;
            }
            if (ipv4 == "::1")
            {
                IPHostEntry ipentry = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in ipentry.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        ipv4 = ip.ToString();
                        break;
                    }
                }
            }
            //IPHostEntry ipentry = Dns.GetHostEntry(Dns.GetHostName());
            //string ipv4 = "";
            //foreach (IPAddress ip in ipentry.AddressList)
            //{
            //    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            //    {
            //        ipv4 = ip.ToString();
            //        break;
            //    }
            //}

            // IPアドレスの3桁ずつを999から減算し、12桁にまとめる
            string[] work = ipv4.Split('.');
            int baseInt = 999;
            sIPAddress = "";
            for (int i = 0; i < work.Length; ++i)
            {
                sIPAddress += baseInt - int.Parse(work[i]);
            }

            // クエリ取得
            _TargetEcsNo = Request.QueryString.Get("EcsNo");
            if (_TargetEcsNo != null)
            {
                _TargetEcsNo = _TargetEcsNo.Trim();
                _blEcsSelectWindow = new BLEcsSelectWindow(_TargetEcsNo);
            }

            // 設通シリーズ取得
            _EcsContainDrawingList = _blEcsSelectWindow.GetEcsContainDrawingList(_TargetEcsNo);

        }

        #endregion
    }
}