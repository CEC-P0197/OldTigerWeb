using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Text;
using System.IO;
using System.Configuration;
using OldTigerWeb.DataAccess.Common;

// デバッグパラメータ用モデル
public class DebugParameter
{
    public int? Rank;
    public string FileName;
    public string ClassName;
    public string MethodName;
    public string Title;
    public string Content;
}

public class CommonLogic
    {
        // エラーログ設定
        private  string _ErrorLogDir =  HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["LogDir"]);
        private string _ErrorLogFile = ConfigurationManager.AppSettings["LogFile"];

        // デバッグログ設定
        private string _DebugLogDir = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["DebugLogDir"]);
        private string _DebugLogFile = ConfigurationManager.AppSettings["DebugLogFile"];

        // デバッグ判定設定
        private string _DebugFlg = ConfigurationManager.AppSettings["DebugFlg"];
        private string _DebugDefaultFlg = ConfigurationManager.AppSettings["DebugDefaultFlg"];
        // デバッグログファイルパス
        private string _DebugFileFullPath;

        /// <summary>
        /// コンストラクタ
        /// </summary>
    public CommonLogic()
        {
        }

        /// <summary>
        /// Script（ダイアログ表示）登録
        /// </summary>
        /// <param name="csType">インスタンスType</param>
        /// <param name="csManager">ClientScriptManagerクラス</param>
        /// <param name="arrayMessage">メッセージ文字列</param>
        public void ShowMessage(
            Type csType,
            ClientScriptManager csManager,
            ArrayList arrayMessage)
        {
            try
            {
                string csPopUp = "PopUpScript";
                string getString = "";

                // Scriptの登録
                if (!csManager.IsStartupScriptRegistered(csType, csPopUp))
                {
                    // 1) Script文字列の作成
                    foreach (string arrayString in arrayMessage)
                    {
                        getString = getString + arrayString.ToString() + "\\n";
                    }

                    // 2) Scriptの登録
                    csManager.RegisterStartupScript(csType, csPopUp,
                        "DisplayMessage('" + getString + "'); ", true);
                }

                // リターン
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// エラーログ出力処理
        /// </summary>
        /// <param name="errorForm">エラーフォーム名</param>
        /// <param name="errorPlace">エラーメソッド名</param>
        /// <param name="errorText">Exceptionクラス</param>
        /// <returns>なし</returns>
        public void ErrorProcess(string errorForm, string errorPlace, Exception errorEx, HttpResponse response)
        {
            try
            {
                // システム日付取得
                DateTime systemDate = DateTime.Now;


                string logFileName = systemDate.ToString("yyyyMMdd") + "_" + _ErrorLogFile.Trim();

                StringBuilder logString = new StringBuilder();

                // 出力文字列に追加
                // 日付
                logString.Append(systemDate.ToString("yyyy/MM/dd HH:mm:ss") + " ");
                // エラーフォーム名
                logString.Append(errorForm + " ");
                // メソッド名
                logString.Append(errorPlace + " ");
                // エラー詳細
                logString.Append(errorEx.ToString() + " ");

                // 出力文字列から改行コードを削除
                logString.Replace("\n\r", "");
                logString.Replace("\r", "");
                logString.Replace("\n", "");

                // フォルダの存在チェック
                if (!Directory.Exists(_ErrorLogDir))
                {
                    // 存在しない場合、ディレクトリを作成
                    Directory.CreateDirectory(_ErrorLogDir);
                }

                // ファイルの存在チェック
                if (!File.Exists(_ErrorLogDir + logFileName))
                {
                    // 存在しない場合、ファイルを作成
                    FileStream fs = File.Create(_ErrorLogDir + logFileName);
                    fs.Close();
                }

                // ログ出力
                StreamWriter logFile = new StreamWriter(_ErrorLogDir + logFileName, true, Encoding.GetEncoding("Shift_JIS"));

                // ログの書き込み
                logFile.WriteLine(logString.ToString());

                // ログファイルのクローズ
                logFile.Close();

                // コンテンツのクリア
                response.ClearContent();

                StringBuilder errExString = new StringBuilder();
                errExString.Append(errorEx.ToString() + " ");

                // 出力文字列から改行コードを削除
                errExString.Replace("\n\r", "");
                errExString.Replace("\r", "");
                errExString.Replace("\n", "");
                errExString.Replace("&", "");

                // エラーページ表示
                response.Redirect("frmErrorPage.aspx?form_id=" + errorForm + "&place=" + errorPlace + "&ex=" + errExString.ToString(), false);

                // リターン
                return;
            }
            catch (Exception ex)
            {
                string exstr = ex.ToString();
                return;
            }
        }

    /// <summary>
    /// デバッグログ出力処理
    /// </summary>
    /// <param name="dp"></param>
    // デバッグログ使用例
    //　・Rankには任意の数値を指定
    //　　（基準値<=Rankの場合、出力。NULLの場合、デフォルト値がセットされる）
    //　・Titleには任意の文字列を指定
    //　・Contentには任意の文字列を指定
    //↓サンプルソース
    //DebugParameter dp = new DebugParameter();
    //        dp.Rank = 9;
    //        dp.FileName = System.IO.Path.GetFileName(this.GetType().Assembly.Location);
    //        dp.ClassName = this.GetType().FullName;
    //        dp.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
    //        dp.Title = "fileFullPath:";
    //        dp.Content = ""; 
    //CommonLogic.DebugProcess(dp);
    public void DebugProcess(DebugParameter dp)
    {
        // 出力基準となるデバッグフラグを設定
        // 存在しない場合、デフォルトで設定した値、又は0を設定
        int debugFlg;
        if (string.IsNullOrEmpty(_DebugFlg))
        {
            if (string.IsNullOrEmpty(_DebugDefaultFlg))
            {
                debugFlg = 0;
            }
            else
            {
                debugFlg = int.Parse(_DebugDefaultFlg);
            }
        }
        else
        {
            debugFlg = int.Parse(_DebugFlg);
        }

        // ログインユーザー取得
        string[] domainloginName = System.Web.HttpContext.Current.User.Identity.Name.Split('\\');
        string loginName = domainloginName[domainloginName.GetUpperBound(0)];


        // デバッグログデフォルト値設定
        if (dp.Rank == null)
        {
            dp.Rank = int.Parse(_DebugDefaultFlg);
        }

        if (dp.Rank <= debugFlg)
        {
            DateTime systemDate = DateTime.Now;
            // ログ名称用日付文字列
            string logDate = systemDate.ToString("yyyyMMdd").Trim();
            // デバッグログフォルダパス
            string debugLogFolder = _DebugLogDir;
            // デバッグログファイルパス
            string debugLogFile =_DebugLogFile;
            // タイトル
            string Title = dp.Title;
            // コンテンツ
            string content = dp.Content;
            // ログテキスト用ビルダー
            StringBuilder logContent = new StringBuilder();

            try
            {
                // デバッグログ用フォルダ有無確認
                if (Directory.Exists(debugLogFolder) == false)
                {
                    /// デバッグログ用フォルダ作成
                    Directory.CreateDirectory(debugLogFolder);
                }

                // ログ書き込みテキスト作成
                // 日付
                logContent.Append(systemDate.ToString("yyyy/MM/dd HH:mm:ss") + ",");
                // ログインユーザーID
                logContent.Append(loginName + ",");
                // エラーファイル名
                logContent.Append(dp.FileName + ",");
                // エラークラス名
                logContent.Append(dp.ClassName + ",");
                // メソッド名
                logContent.Append(dp.MethodName + ",");
                // タイトル
                logContent.Append(dp.Title + ",");
                // 内容
                logContent.Append(dp.Content + " ");

                // 出力文字列から改行コードを削除
                //logContent.Replace("\n\r", "");
                //logContent.Replace("\r", "");
                //logContent.Replace("\n", "");

                _DebugFileFullPath = debugLogFolder + logDate + "_" + debugLogFile;
                // ファイルの存在チェック
                if (!System.IO.File.Exists(_DebugFileFullPath))
                {
                    // 存在しない場合、ファイルを作成
                    FileStream fs = System.IO.File.Create(_DebugFileFullPath);
                    fs.Close();
                }
            }
           catch
            {
                throw;
            }

            // ログ出力
            StreamWriter logFile = new StreamWriter(_DebugFileFullPath, true, Encoding.GetEncoding("Shift_JIS"));

            try
            {

                // ログの書き込み
                logFile.WriteLine(logContent.ToString());

                // ログファイルのクローズ
            }
            finally
            {
                logFile.Close();
                logFile.Dispose();
            }
        }
    }
    
    /// <summary>
    /// Excelテンプレート名取得
    /// <param name="ExcelType">エクセルタイプ K:過去トラ F:フォロー回答</param>
    /// <returns>Excelテンプレート名</returns>
    public string GetExcelTemplate(String ExcelType)
        {
            // Web.Configよりログディレクトリ名を取得
            String excelFilePath =  HttpContext.Current.Server.MapPath(System.Web.Configuration.WebConfigurationManager.AppSettings["ExcelTempDir"]);

            if (ExcelType == "K")
            {
                // Excelテンプレート・過去トラ
                excelFilePath += System.Web.Configuration.WebConfigurationManager.AppSettings["KakotoraTemplate"];
            }
            else
            {
                // Excelテンプレート・フォロー回答
                excelFilePath += System.Web.Configuration.WebConfigurationManager.AppSettings["FollowTemplate"];
            }
            return excelFilePath;
        }

        /// <summary>
        /// 問合せ先メールアドレス取得
        /// <param name="BYPU">BY_PU区分</param>
        /// <returns>メールアドレス</returns>
        public string GetMailAddress(String BYPU)
        {
            // Web.Configよりメールアドレスを取得
            String mailAddr = "";

            if (BYPU == "BY")
            {
                // メールアドレス・BY
                mailAddr = System.Web.Configuration.WebConfigurationManager.AppSettings["MailAddrBY"];
            }
            if (BYPU == "PU")
            {
                // メールアドレス・PU
                mailAddr = System.Web.Configuration.WebConfigurationManager.AppSettings["MailAddrPU"];
            }
            return mailAddr;
        }

        /// <summary>
        /// ヘルプ表示先取得
        /// <param name="SF">S:検索、F:フォロー</param>
        /// <returns>ヘルプ表示先</returns>
        public string GetHelpForder(String SF)
        {
            // Web.Configよりヘルプ表示先を取得
            String helpFolder = "";

            if (SF == "SH")
            {
                // 過去トラ検索-ヘルプ（マニュアル）
                helpFolder = System.Web.Configuration.WebConfigurationManager.AppSettings["HelpSerch"];
            }
            // 2017.04.03 ta_kanda 追加　Start
            if (SF == "SQ")
            {
                // 過去トラ検索-Ｑ＆Ａ
                helpFolder = System.Web.Configuration.WebConfigurationManager.AppSettings["QaSerch"];
            }
            if (SF == "FH")
            {
                // フォロー-ヘルプ（マニュアル）
                helpFolder = System.Web.Configuration.WebConfigurationManager.AppSettings["HelpFollow"];
            }
            // 2017.04.03 ta_kanda 追加　Start
            if (SF == "FQ")
            {
                // フォロー-Ｑ＆Ａ
                helpFolder = System.Web.Configuration.WebConfigurationManager.AppSettings["QaFollow"];
            }
            // 2017.04.03 ta_kanda 追加　Start
            if (SF == "TH")
            {
                // ＴＯＰ-ヘルプ（マニュアル）
                helpFolder = System.Web.Configuration.WebConfigurationManager.AppSettings["HelpTop"];
            }
            if (SF == "TQ")
            {
                // ＴＯＰ-Ｑ＆Ａ
                helpFolder = System.Web.Configuration.WebConfigurationManager.AppSettings["QaTop"];
            }
            // 2017.04.03 ta_kanda 追加　End
            return helpFolder;
        }

        /// <summary>
        /// 参照先メインフォルダ取得
        /// <returns>参照先メインフォルダ</returns>
        public string GetLinkForder()
        {
            // Web.Configより参照先メインフォルダを取得
            String strLinkForder = "";

            strLinkForder = System.Web.Configuration.WebConfigurationManager.AppSettings["LinkForder"];

            return strLinkForder;
        }

        /// <summary>
        /// フォルダの存在チェック・参照権限チェック
        /// </summary>
        /// <returns>結果ステータス</returns>
        /// <remarks></remarks>
        public int CheckFolder(String FilePath)
        {
            int result = 0;

            if (FilePath == null || FilePath == "") return 1;

            // フォルダの存在チェック
            if (!Directory.Exists(FilePath))
            {
                // 存在しないか参照権限がない場合
                return 1;
            }

            return result;
        }

    /// <summary>
        /// ファイルの存在チェック・参照権限チェック
        /// </summary>
        /// <returns>結果ステータス</returns>
        /// <remarks></remarks>
        public int CheckFile(String FilePath)
        {
            int result = 0;

            if (FilePath == null || FilePath == "") return 1;

            // フォルダの存在チェック
            if (!System.IO.File.Exists(FilePath))
            {
                // 存在しないか参照権限がない場合
                return 1;
            }

            return result;
        }

        /// <summary>
        /// Windowsログイン・ユーザマスタチェック
        /// </summary>
        /// <returns>結果ステータス</returns>
        /// <remarks></remarks>
        public Boolean CheckUser()
        {
            Boolean bRet = false;
            DataTable result = null;

            // Windows Login チェック
            string sUser = GetWindowsUser();

             // データアクセス作成
            SqlCommon dac = new SqlCommon();

            // ＳＱＬ実行
            result = dac.SelectUser(sUser);

            // 存在しない場合、使用不可
            if (result.Rows.Count == 0)
            {
                bRet = true;
            }

            result.Dispose();

            return bRet;
        }

        /// <summary>
        /// ユーザマスタ取得
        /// </summary>
        /// <returns>結果データテーブル</returns>
        /// <remarks></remarks>
        public DataTable GetUser()
        {
            DataTable result = null;

            // Windows User
            string sUser = GetWindowsUser();

            // データアクセス作成
            SqlCommon dac = new SqlCommon();

            // ＳＱＬ実行
            result = dac.SelectUser(sUser);

            return result;
        }

        /// <summary>
        /// Windowsログインユーザ取得
        /// </summary>
        /// <returns>WindowsユーザID</returns>
        /// <remarks></remarks>
        public String GetWindowsUser()
        {
            String user_id = "";

            // Windows Login 取得
            String sUser = HttpContext.Current.Request.ServerVariables["LOGON_USER"];

            if (!(sUser.Trim() == "" || sUser == null))
            {
                // ドメインがある場合を考慮
                String[] strArrayData = sUser.Trim().Split('\\');

                user_id = strArrayData[strArrayData.Count() - 1];
            }

            return user_id;
        }

        /// <summary>
        /// 検索日時取得
        /// </summary>
        /// <returns>検索日時</returns>
        /// <remarks></remarks>
        public String Getdate()
        {
            String date = "";

            // 検索日時 取得
            date = DateTime.Now.ToString("yyyy/MM/dd hh:ss:ttt");


            return date;
        }

        /// 過去トラ情報取得
        /// </summary>
        /// <param name="kanri_no">システム管理No</param>
        /// <returns>結果データテーブル</returns>
        /// <remarks></remarks>
        public DataTable GetTroubleData(String kanri_no)
        {
            DataTable result = null;

            // データアクセス作成
            SqlCommon dac = new SqlCommon();

            // ＳＱＬ実行
            result = dac.SelectTorableData(kanri_no);

            return result;
        }

        /// 検索ログ登録
        /// </summary>
        /// <param name="type">検索タイプ</param>
        /// <param name="word">検索ワード</param>
        /// <param name="kanri_no">システム管理No</param>
        /// <param name="date_time">システム日時</param>
        /// <returns>結果ステータス</returns>
        /// <remarks></remarks>
        //20170201 機能改善 START
        //public Boolean RegistLogData(String type, String word, int kanri_no)
        public Boolean RegistLogData(String type, String word, int kanri_no, DateTime date_time)
        //20170201 機能改善 END
        {
            Boolean result = false;
            string sUser = GetWindowsUser();

            // データアクセス作成
            SqlCommon dac = new SqlCommon();

            // ＳＱＬ実行
            //20170201 機能改善 START
            //result = dac.InsertLogData(sUser, type, word, kanri_no);
            result = dac.InsertLogData(sUser, type, word, kanri_no, date_time);
            //20170201 機能改善 END

            return result;
        }

        /// 検索ログ登録
        /// </summary>
        /// <param name="paraType">種類</param>
        /// <param name="paraWord">検索文字</param>
        /// <param name="paraTable">カテゴリデータテーブル</param>
        /// <param name="paraCondition">And・Or検索条件</param>
        /// <returns>結果ステータス</returns>
        /// <remarks></remarks>
        public Boolean RegistHistoryLogData(String paraCondition, String paraType, String paraWord,  
                                            String paraBusyo, String paraHyouka, String paraPartsS, String paraPartsN,
                                            String paraKaihatu, String paraGensyo, String paraGenin, String paraSyakata,
                                            String paraSgensyo, String paraSyouin, String paraEgtm)
        {
            Boolean result = false;
            string user = GetWindowsUser();
            DateTime date = DateTime.Now;


            // データアクセス作成
            SqlCommon dac = new SqlCommon();

            // ＳＱＬ実行
            result = dac.InsertHistoryLogData(user, date, paraCondition, paraType, paraWord, paraBusyo, paraHyouka, paraPartsS, paraPartsN, paraKaihatu, paraGensyo, paraGenin, paraSyakata, paraSgensyo, paraSyouin, paraEgtm);

            return result;
        }

        /// <summary>
        /// 検索履歴情報取得
        /// </summary>
        /// <param name="user_id">ユーザ</param>
        /// <returns>結果データテーブル</returns>
        /// <remarks></remarks>
        public DataTable GetSearchHistoryList(String user_id)
        {

            DataTable result = null;
            string sUser = GetWindowsUser();


            // データアクセス作成
            SqlCommon dac = new SqlCommon();



            // ＳＱＬ実行
            result = dac.SelectSearchHistoryList(sUser);

            return result;
        }


    }
