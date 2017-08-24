using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace OldTigerWeb.BuisinessLogic
{

    /// <summary>
    /// 設通選択画面　ビジネスロジック
    /// </summary>
    public class BLEcsSelectWindow
    {
        #region "フィールド"
        /// <summary>
        /// 対象設通番号
        /// </summary>
        private string _EcsNo;

        private string _Api_BaseUrl = ConfigurationManager.AppSettings["ApiUri"];

        #endregion

        #region "コンストラクタ"
        public BLEcsSelectWindow(string ecsNo)
        {
            _EcsNo = ecsNo;
        }
        #endregion 

        #region "プロパティ"
        /// <summary>
        /// 検索対象となる設通番号の取得・設定を行う。
        /// </summary>
        public string EcsNo
        {
            get { return _EcsNo; }
            set
            {
                if (value != null && value != _EcsNo)
                {
                    _EcsNo = value.Trim();
                }
            }
        }
        #endregion

        #region "メソッド"
        public DataTable GetEcsContainDrawingList(string en = null)
        {
            DataTable result = new DataTable(); 

            try
            {
                string ecsSeries = "" ;

                // 設通番号取得
                if (en != null)
                {

                    string[] enArray = en.Split('-');
                    ecsSeries = enArray[0];
                    EcsNo = enArray[1];
                }

                #region 設通取得（設通より関連図面リストを使用する為）
                var ecsList = new List<EcsAPIViewModel>();
                GetApiRequestEcs(_EcsNo, ecsSeries, ref ecsList);
                if (ecsList == null)
                {
                    // 設通取得のエラーの為、上位クラスにnull返却
                    return null;
                }
                return DeploymentEcsDrawingList(ecsList);
                #endregion
            }
            catch
            {
                // エラーの場合、nullを返却
                return null;
            }
        }

        /// <summary>
        /// 設通リスト取得（API呼び出し）
        /// </summary>
        /// <returns></returns>
        private List<EcsListAPIViewModel> GetApiRequestEcsList()
        {
            try {
                // 設通シリーズ取得（APIリクエスト）
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(string.Format(Def.API_BASE_URL, _Api_BaseUrl));
                    client.DefaultRequestHeaders.Accept.Clear();
                    // json ではなく xml で応答を受け取る場合は application/xml
                    client.DefaultRequestHeaders.Accept
                        .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(Def.MEDIA_TYPE_JSON));
                    var url = String.Format(client.BaseAddress + string.Format(Def.API_ECSLIST_URL, _EcsNo));

                    HttpResponseMessage response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var content = response.Content.ReadAsAsync<List<EcsListAPIViewModel>>().Result;
                        return content;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch(Exception ex)
            {
                DebugParameter dp = new DebugParameter();
                dp.Rank = 9;
                dp.FileName = System.IO.Path.GetFileName(this.GetType().Assembly.Location);
                dp.ClassName = this.GetType().FullName;
                dp.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                dp.Title = "設通リスト取得時エラー";
                dp.Content = "";
                //CommonLogic.DebugProcess(dp);
                return null;
            }
            finally
            {

            }
        }

        /// <summary>
        /// 設通取得（API呼び出し）
        /// </summary>
        /// <param name="ecsNo"></param>
        /// <param name="ecsSeries"></param>
        /// <param name="ecsList"></param>
        private void GetApiRequestEcs(string ecsNo, string ecsSeries, ref List<EcsAPIViewModel> ecsList)
        {
            try
            {

                // 設通シリーズ取得（APIリクエスト）
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(string.Format(Def.API_BASE_URL, _Api_BaseUrl));
                    client.DefaultRequestHeaders.Accept.Clear();
                    // json ではなく xml で応答を受け取る場合は application/xml
                    client.DefaultRequestHeaders.Accept
                        .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(Def.MEDIA_TYPE_JSON));
                    var url = String.Format(client.BaseAddress + string.Format(Def.API_ECS_URL, ecsSeries, ecsNo));

                    HttpResponseMessage response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var content = response.Content.ReadAsAsync<List<EcsAPIViewModel>>().Result;
                        foreach (var data in content)
                        {
                            ecsList.Add(data);
                        }
                    }
                }
            }
            catch
            {
                // エラーの場合、nullを返却
                ecsList = null;
            }
        }

        /// <summary>
        /// 取得した設通リストを展開します。
        /// </summary>
        /// <param name="ecsList"></param>
        /// <returns></returns>
        private DataTable DeploymentEcsDrawingList(List<EcsAPIViewModel> ecsList)
        {
            // 返却用データテーブル定義
            DataTable result = new DataTable();
            result.Columns.Add("DrawingNo");
            result.Columns.Add("DrawingRevNo");

            List<EcsDrawingViewModel> ecsDrawingList = new List<EcsDrawingViewModel>();

            // 設通リストより図面リストのみ抽出
            foreach (var ecsData in ecsList)
            {
                foreach(var drawingData in ecsData.DrawingList[0])
                {
                    ecsDrawingList.Add(drawingData);
                }
            }


            // 図面リストにてデータテーブル作成
            foreach (var ecsDrawingData in ecsDrawingList)
            {
                DataRow dr = result.NewRow();
                dr["DrawingNo"] = ecsDrawingData.TitleDrawingNo;
                dr["DrawingRevNo"] = ecsDrawingData.DrowingRevisionNo;
                result.Rows.Add(dr);
            }

            return result;
        }
        #endregion
    }

    #region API用モデルクラス
    #region 設通リスト取得用クラスモデル
    /// <summary>
    /// 設計シリーズリスト
    /// </summary>
    public class EcsListAPIViewModel
    {
        #region フィールド
        private Collection<EcsSeriesViewModel> _EcsSeriesList = new Collection<EcsSeriesViewModel>();
        #endregion

        #region プロパティ
        /// <summary>
        /// 設計通知書番号
        /// </summary>
        public string EcsNo { get; set; }
        /// <summary>
        /// 設計通知書シリーズリスト
        /// </summary>
        public Collection<EcsSeriesViewModel> EcsSeriesList
        {
            get { return _EcsSeriesList; }
        }
        #endregion
    }
    /// <summary>
    /// 設通シリーズ（モデル）
    /// 設通シリーズリストで使用 
    public class EcsSeriesViewModel
    {
        #region プロパティ
        /// <summary>
        /// 設計通知書シリーズ
        /// </summary>
        public List<string> EcsSeriesList { get; set; }
        #endregion
    }
    #endregion

    #region 設通取得用モデルクラス
    /// <summary>
    // 設計通知書（モデル）
    /// </summary>
    public class EcsAPIViewModel
    {
        #region フィールド
        /// <summary>
        /// SameInstructionListプロパティで使用
        /// </summary>
        private Collection<List<SameInstructionViewModel>> _SameInstructionList = new Collection<List<SameInstructionViewModel>>();
        /// <summary>
        /// _DrawingListプロパティで使用
        /// </summary>
        private Collection<List<EcsDrawingViewModel>> _DrawingList = new Collection<List<EcsDrawingViewModel>>();
        #endregion
        #region プロパティ
        /// <summary>
        /// 設計通知書シリーズ
        /// </summary>
        public string EcsSeries { get; set; }
        /// <summary>
        /// 設計通知書番号
        /// </summary>
        public string EcsNo { get; set; }
        /// <summary>
        /// タイトル
        /// </summary>
        public string EcsTitle { get; set; }
        /// <summary>
        /// 設計通知書作成者ID
        /// </summary>
        public string EcsCreaterId { get; set; }
        /// <summary>
        /// 同指示設通リスト
        /// </summary>
        public Collection<List<SameInstructionViewModel>> SameInstructionList
        {
            get { return _SameInstructionList; }
        }
        /// <summary>
        /// 図面リスト
        /// </summary>
        public Collection<List<EcsDrawingViewModel>> DrawingList
        {
            get { return _DrawingList; }
        }
        #endregion
    }
    /// <summary>
    /// 同指示設通（モデル）
    /// 設計通知書で使用 
    /// </summary>
    public class SameInstructionViewModel
    {
        #region プロパティ
        /// <summary>
        /// 設計通知書シリーズ
        /// </summary>
        public string EcsSeries { get; set; }
        /// <summary>
        /// 設計通知書改訂番号
        /// </summary>
        public string EcsNo { get; set; }
        #endregion
    }
    // 
    /// <summary>
    /// 関連図面リスト（モデル） 
    /// 設計通知書で使用 
    /// </summary>
    public class EcsDrawingViewModel
    {
        #region　プロパティ
        /// <summary>
        /// タイトル図面番号
        /// </summary>
        public string TitleDrawingNo { get; set; }
        /// <summary>
        /// 図面改訂番号
        /// </summary>
        public string DrowingRevisionNo { get; set; }
        #endregion
    }
    #endregion

    #region 図面リスト取得用モデルクラス

    /// <summary>
    /// 図面改訂リストモデル
    /// </summary>
    public class DrawingListAPIViewModel
    {
        #region フィールド
        /// <summary>
        /// DrawingRevisionListプロパティで使用されます
        /// </summary>
        private Collection<DrawingRevisionNoListViewModel> _DrawingRevisionList = new Collection<DrawingRevisionNoListViewModel>();
        #endregion

        #region プロパティ
        /// <summary>
        /// タイトル図面番号
        /// </summary>
        public string TitleDrawingNo { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        public string DevelopCode { get; set; }
        /// <summary>
        /// 部品名称（図面）
        /// </summary>
        public string DrawingName { get; set; }
        /// <summary>
        /// 図面改訂番号リスト
        /// </summary>
        public Collection<DrawingRevisionNoListViewModel> DrawingRevisionList
        {
            get { return _DrawingRevisionList; }
        }
        #endregion
    }

    /// <summary>
    /// 図面改訂（モデル）
    /// 図面改訂リストで使用
    /// </summary>
    public class DrawingRevisionNoListViewModel
    {
        #region プロパティ
        /// <summary>
        /// 図面改訂番号
        /// </summary>
        public List<string> DrawingRevisionNo { get; set; }
        #endregion
    }
    #endregion
    #endregion
}


