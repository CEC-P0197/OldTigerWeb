using System;
using System.Net;
using System.Data;
using OldTigerWeb.BuisinessLogic;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using OldTigerWeb.DataAccess.Common;

//using OldTigerWeb.Common;

namespace OldTigerWeb
{
    public partial class frmMitakaRegist : System.Web.UI.Page
    {
        #region "フィールド"
        CommonLogic bcom = new CommonLogic();
        CommonPageLogic cPageLogic = new CommonPageLogic();
        /// <summary>
        /// 過去トラ観たか情報フィールド
        /// </summary>
        public MitakaData _MitakaData;
        /// <summary>
        /// ユーザー情報フィールド
        /// </summary>
        private DataTable _UserMater;
        /// <summary>
        /// 観たか検索条件フィールド
        /// </summary>
        public DataTable _SearchCondition;
        /// <summary>
        /// 過去トラ情報フィールド
        /// </summary>
        public DataTable _TroubleData;
        /// <summary>
        /// 過去トラ観たか状況フィールド
        /// </summary>
        public string _Status;
        /// <summary>
        /// 過去トラ観たか管理Noフィールド
        /// </summary>
        private string _QueryManagementNo;
        /// <summary>
        /// 過去トラ観たか回答情報フィールド
        /// </summary>
        public MitakaAnswerData _MitakaAnswerData;
        /// <summary>
        /// 検索条件リストフィールド
        /// </summary>
        //private List<SearchParameter> _SearchParameterList;
        /// <summary>
        /// 開発符号リストフィールド
        /// </summary>
        private List<string> _DevelopmentCodeList;
        /// <summary>
        /// 作成者IDフィールド
        /// </summary>
        private string _CreateUserId;
        /// <summary>
        /// 編集者IDフィールド
        /// </summary>
        private string _EditUserId;
        /// <summary>
        /// 自部署展開先メンバーリストフィールド
        /// </summary>
        private Dictionary<string,string> _DeploymentOwnMenberList;
        /// <summary>
        /// 関連部署展開先メンバーリストフィールド
        /// </summary>
        private Dictionary<string,string> _DeploymentRelationMenberList;
        /// <summary>
        /// タイトルフィールド
        /// </summary>
        private string _InfoTitle;
        /// <summary>
        /// コメントフィールド
        /// </summary>
        private string _Comment;
        /// <summary>
        /// BLK Noリストフィールド
        /// </summary>
        private List<string> _BlkNoList;
        /// <summary>
        /// タイトル品番リストフィールド
        /// </summary>
        private List<string> _TitleDrawingNoList;
        /// <summary>
        /// 設通番号リストフィールド
        /// </summary>
        private List<string> _EcsNoList;
        /// <summary>
        /// 展開対象リストフィールド
        /// </summary>
        private Dictionary<string,string> _DeploymentTargetList;
        /// <summary>
        /// 設計部署リストフィールド
        /// </summary>
        private List<string> _SekkeiDepartList;
        /// <summary>
        /// 評価部署リストフィールド
        /// </summary>
        private List<string> _HyoukaDepartList;
        /// <summary>
        /// 回答対象部署リストフィールド
        /// </summary>
        private List<string> _AnswerTargetDepartList;
        #endregion

        #region "プロパティ"
        /// <summary>
        /// ユーザID（ユーザマスタ）
        /// </summary>
        //private string sUser = "";
        /// <summary>
        /// 過去トラ観たか管理Noプロパティ
        /// </summary>
        public string ManageNo
        {
            get
            {
                return _QueryManagementNo;
            }
            set
            {
                if (value != null && value != _QueryManagementNo)
                {
                    _QueryManagementNo = value.Trim();
                }
            }
        }
        ///// <summary>
        ///// 検索条件リストプロパティ
        ///// </summary>
        //public List<SearchParameter> SearchParameterList
        //{
        //    get
        //    {
        //        return _SearchParameterList;
        //    }
        //    set
        //    {
        //        if (value != null && value != _SearchParameterList)
        //        {
        //            _SearchParameterList = value;
        //        }
        //    }
        //}
        /// <summary>
        /// 開発符号リストプロパティ
        /// </summary>
        public List<string> DevelopmentCodeList
        {
            get
            {
                return _DevelopmentCodeList;
            }
            set
            {
                if (value != null && value != _DevelopmentCodeList)
                {
                    _DevelopmentCodeList = value;
                }
            }
        }
        /// <summary>
        /// 作成者プロパティ
        /// </summary>
        public string CreateUser
        {
            get
            {
                return _CreateUserId;
            }
            set
            {
                if (value != null && value != _CreateUserId)
                {
                    _CreateUserId = value.Trim();
                }
            }
        }
        /// <summary>
        /// 編集者プロパティ
        /// </summary>
        public string EditUser
        {
            get
            {
                return _EditUserId;
            }
            set
            {
                if (value != null && value != _EditUserId)
                {
                    _EditUserId = value.Trim();
                }
            }
        }
        /// <summary>
        /// 自部署展開メンバーリストプロパティ
        /// </summary>
        public Dictionary<string,string> EditDeploymentOwnMenberListUser
        {
            get
            {
                return _DeploymentOwnMenberList;
            }
            set
            {
                if (value != null && value != _DeploymentOwnMenberList)
                {
                    _DeploymentOwnMenberList = value;
                }
            }
        }
        /// <summary>
        /// 関連部署展開メンバーリストプロパティ
        /// </summary>
        public Dictionary<string,string> DeploymentRelationMenberList
        {
            get
            {
                return _DeploymentRelationMenberList;
            }
            set
            {
                if (value != null && value != _DeploymentRelationMenberList)
                {
                    _DeploymentRelationMenberList = value;
                }
            }
        }
        /// <summary>
        /// タイトルプロパティ
        /// </summary>
        public string InfoTitle
        {
            get
            {
                return _InfoTitle;
            }
            set
            {
                if (value != null && value != _InfoTitle)
                {
                    _InfoTitle = value.Trim();
                }
            }
        }
        /// <summary>
        /// コメントプロパティ
        /// </summary>
        public string Comment
        {
            get
            {
                return _Comment;
            }
            set
            {
                if (value != null && value != _Comment)
                {
                    _Comment = value.Trim();
                }
            }
        }
        /// <summary>
        /// BLK Noプロパティ
        /// </summary>
        public List<string> BlkNoList
        {
            get
            {
                return _BlkNoList;
            }
            set
            {
                if (value != null && value != _BlkNoList)
                {
                    _BlkNoList = value;
                }
            }
        }
        /// <summary>
        /// タイトル品番プロパティ
        /// </summary>
        public List<string> TitleDrawingNoList
        {
            get
            {
                return _TitleDrawingNoList;
            }
            set
            {
                if (value != null && value != _TitleDrawingNoList)
                {
                    _TitleDrawingNoList = value;
                }
            }
        }
        /// <summary>
        /// 設通番号プロパティ
        /// </summary>
        public List<string> EcsNoList
        {
            get
            {
                return _EcsNoList;
            }
            set
            {
                if (value != null && value != _EcsNoList)
                {
                    _EcsNoList = value;
                }
            }
        }
        /// <summary>
        /// 展開対象リストプロパティ
        /// </summary>
        public Dictionary<string,string> DeploymentTargetList
        {
            get
            {
                return _DeploymentTargetList;
            }
            set
            {
                if (value != null && value != _DeploymentTargetList)
                {
                    _DeploymentTargetList = value;
                }
            }
        }
        /// <summary>
        /// 設計部署プロパティ
        /// </summary>
        public List<string> SekkeiDepartList
        {
            get
            {
                return _SekkeiDepartList;
            }
            set
            {
                if (value != null && value != _SekkeiDepartList)
                {
                    _SekkeiDepartList = value;
                }
            }
        }
        /// <summary>
        /// 評価部署プロパティ
        /// </summary>
        public List<string> HyoukaDepartList
        {
            get
            {
                return _HyoukaDepartList;
            }
            set
            {
                if (value != null && value != _HyoukaDepartList)
                {
                    _HyoukaDepartList = value;
                }
            }
        }
        /// <summary>
        /// 回答対象部署プロパティ
        /// </summary>
        public List<string> AnswerTargetDepartList
        {
            get
            {
                return _AnswerTargetDepartList;
            }
            set
            {
                if (value != null && value != _AnswerTargetDepartList)
                {
                    _AnswerTargetDepartList = value;
                }
            }
        }
        #endregion

        #region "メソッド"

        #region "イベント処理"
        /// <summary>
        /// ページ読み込み処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Boolean bRet = false;
            ClientScriptManager csManager = Page.ClientScript;
            Type csType = this.GetType();
            ArrayList arrayMessage = new ArrayList();

            // Windowsログイン・ユーザマスタチェック
            bRet = bcom.CheckUser();
            if (bRet)
            {
                //DetailDiv.Visible = false; // 画面詳細項目を表示しない

                arrayMessage.Add(Def.DefMsg_USERERR);
                bcom.ShowMessage(csType, csManager, arrayMessage);
                return;
            }

            // ログインユーザーID取得
            string sUser = bcom.GetWindowsUser();

            // ユーザー情報取得
            _UserMater = new SqlCommon().SelectUser(sUser);

            // クエリパラメータから管理番号を取得
            if (IsPostBack != true)
            {
                // クエリパラメータを取得する（過去トラ観たか管理No）
                if(Request.QueryString.Get("ManageNo") != null)
                {
                    _QueryManagementNo = Request.QueryString.Get("ManageNo");
                }

            }
            // 管理番号が取得できなかった場合
            if (_QueryManagementNo == null)
            {
                // 過去トラ観たか新規登録
                PageTitle.Text = "過去トラ観たか新規登録";

                // 過去トラ観たか情報取得  初期処理
                getMitakaData();
                
                //_ParamKeyword = (String)Session[Def.DefPARA_WORD];    // 文字列検索
                //_ParamCotegoryTable = (DataTable)Session[Def.DefPARA_TABLE];   // カテゴリ検索
                //_ParamSearchCondition = (String)Session[Def.DefPARA_CONDITION_FLG];  // AND/OR検索条件
            }
            else
            {
                // 過去トラ観たか編集
                PageTitle.Text = "過去トラ観たか編集";
                HdnManageNo.Value = _QueryManagementNo;
                
                // 過去トラ観たか情報取得
                getMitakaData(_QueryManagementNo, sUser);

                _TroubleData.Columns.Add("BUSYO_SEKKEI");
                _TroubleData.Columns.Add("BUSYO_HYOUKA");
                _TroubleData.Columns.Add("BUSYO_KAITOU");

                
                List<string> SekkeiDepartList = new List<string>();
                List<string> HyoukaDepartList = new List<string>();
                List<string> AnswerTargetDepartList = new List<string>();

                for (int i = 0; i < _TroubleData.Rows.Count; i++)
                {
                    for (int j = 0; j < _TroubleData.Rows[i].ItemArray.Length; j++)
                    {
                        // 設計部署をリストに格納
                        if (_TroubleData.Rows[i].Table.Columns[j].ToString().StartsWith("BUSYO_SEKKEI"))
                        {
                            if (_TroubleData.Rows[i].ItemArray[j].ToString().Length > 0)
                            {
                                SekkeiDepartList.Add(_TroubleData.Rows[i].ItemArray[j].ToString());
                            }
                        }
                        // 評価部署をリストに格納
                        if (_TroubleData.Rows[i].Table.Columns[j].ToString().StartsWith("BUSYO_HYOUKA"))
                        {
                            if (_TroubleData.Rows[i].ItemArray[j].ToString().Length > 0)
                            {
                                HyoukaDepartList.Add(_TroubleData.Rows[i].ItemArray[j].ToString());
                            }
                        }
                        // 回答対象部署をリストに格納
                        if (_TroubleData.Rows[i].Table.Columns[j].ToString().StartsWith("BUSYO_"))
                        {
                            if(_TroubleData.Rows[i].ItemArray[j].ToString().Length > 0)
                            {
                                AnswerTargetDepartList.Add(_TroubleData.Rows[i].ItemArray[j].ToString());
                            }
                        }
                    }

                    string skkei = "";
                    if (SekkeiDepartList.Count > 0)
                    {
                        SekkeiDepartList.CombineFromString(ref skkei);
                    }

                    string hyouka = "";
                    if (HyoukaDepartList.Count > 0)
                    {
                        HyoukaDepartList.CombineFromString(ref hyouka);
                    }

                    string kaitou = "";
                    if (AnswerTargetDepartList.Count > 0)
                    {
                        AnswerTargetDepartList.CombineFromString(ref kaitou);
                    }

                    _TroubleData.Columns.Add("BUSYO_SEKKEI_R");
                    _TroubleData.Rows[i]["BUSYO_SEKKEI_R"] = skkei;
                    _TroubleData.Columns.Add("BUSYO_HYOUKA_R");
                    _TroubleData.Rows[i]["BUSYO_HYOUKA_R"] = hyouka;
                    _TroubleData.Columns.Add("BUSYO_KAITOU_R");
                    _TroubleData.Rows[i]["BUSYO_KAITOU_R"] = kaitou;
                }
            }
        }

        /// <summary>
        /// 保存処理（新規保存、保存ボタン押下時）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Preserve_Click(Object sender, EventArgs e)
        {
            // 過去トラ観たか情報登録
            postMitakaData();
        }

        /// <summary>
        /// 削除処理（削除ボタン押下時）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Delete_Click(Object sender, EventArgs e)
        {
            // 過去トラ観たか情報削除
            deleteMitakaData();
        }

        /// <summary>
        /// 回答依頼処理（回答依頼ボタン押下時）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_AnswerRequest_Click(Object sender, EventArgs e)
        {
            // 過去トラ観たか情報回答依頼
            requestMitakaData();
        }

        /// <summary>
        /// 確認完了処理（確認完了ボタン押下時）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_ConfirmFinish_Click(Object sender, EventArgs e)
        {
            // 過去トラ観たか情報確認完了
            confirmedMitakaData();
        }

        /// <summary>
        /// 完了取消処理（完了取消ボタン押下時）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_FinishCancel_Click(Object sender, EventArgs e)
        {
            // 過去トラ観たか情報完了取消
            cancelConfirmedMitakaData();
        }

        /// <summary>
        /// 回答確認処理（回答確認ボタン押下時）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_AnswerConfirm_Click(Object sender, EventArgs e)
        {
            //cPageLogic;
            //getScriptForOpenMitatkaSearchがない
        }

        /// <summary>
        /// 画面クローズ処理（閉じるボタン押下時）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Close_Click(Object sender, EventArgs e)
        {
            // 画面クローズ処理
            closeMitakaRegist();
        }


        /// <summary>
        /// 点検印刷処理（点検印刷ボタン押下時）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_CheckPrint_Click(Object sender, EventArgs e)
        {
            // 点検印刷処理

        }







        #endregion

        #region "画面固有処理"
        /// <summary>
        /// 過去トラ観たか情報取得  初期処理
        /// </summary>
        private void getMitakaData()
        {
            // 初期処理
            // 過去トラ観たか情報インスタンス定義（初期化）
            // 過去トラ観たか情報 コンストラクタ
            _MitakaData = new MitakaData();

            ViewState["MITAKA_NO"] = ""; // 管理番号
            ViewState["LAST_UPDATE_USER"] = ""; // 最終更新者
            ViewState["LAST_UPDATE_YMD"] = ""; // 最終更新日時

            ViewState["TITLE"] = ""; // タイトル
            ViewState["PURPOSE"] = ""; // 目的
            ViewState["STATUS"] = ""; // 状況
            ViewState["START_YMD"] = ""; // 回答開始日時
            ViewState["END_YMD"] = ""; // 回答期限
            ViewState["USER_MAIN"] = ""; // 作成者(正)
            ViewState["USER_SUB"] = ""; // 作成者(副)
            ViewState["CHECK_USER"] = ""; // 点検者
            ViewState["ANSWER_REQUEST_USER"] = ""; // 回答依頼先
            ViewState["DEVELOPMENT_CODE"] = ""; // 開発符号
            ViewState["MODEL"] = ""; // 機種
            ViewState["ECS_NO"] = ""; // 設通番号
            ViewState["BLK_NO"] = ""; // BLK No
            ViewState["TITLE_DRAWING_NO"] = ""; // タイトル品番
            ViewState["COMMENT"] = ""; // 過去トラ確認結果まとめ
            ViewState["HDN_USER_MAIN"] = ""; // 作成者（正）（隠し項目）
            ViewState["HDN_USER_SUB"] = ""; // 作成者（副）（隠し項目）
            ViewState["HDN_ANSWER_REQUEST_USER"] = ""; // 回答依頼先（隠し項目）
            ViewState["HDN_DEVELOPMENT_CODE"] = ""; // 開発符号（隠し項目）
            ViewState["HDN_MODEL"] = ""; // 機種（隠し項目）
            ViewState["HDN_BLK_NO"] = ""; // BLK No（隠し項目）
            ViewState["HDN_TITLE_DRAWING_NO"] = ""; // タイトル品番（隠し項目）
            ViewState["HDN_ECS_NO"] = ""; // 設通番号（隠し項目）
            ViewState["HDN_DEPLOYMENT_TARGET"] = ""; // 展開対象（隠し項目）

            HdnUserMain.Value = ""; // 作成者（正）（隠し項目）
            HdnUserSub.Value = ""; // 作成者（副）（隠し項目）
            HdnAnswerRequest.Value = ""; // 回答依頼先（隠し項目）
            HdnDevelopCode.Value = ""; // 開発符号（隠し項目）
            HdnModel.Value = ""; // 機種（隠し項目）
            HdnBlkNo.Value = ""; // BLK No（隠し項目）
            HdnTitleDrawingNo.Value = ""; // タイトル品番（隠し項目）
            HdnEcsNo.Value = ""; // 設通番号（隠し項目）
            HdnDeploymentTarget.Value = ""; // 展開対象（隠し項目）
        }
        /// <summary>
        /// 過去トラ観たか情報取得
        /// </summary>
        /// <param name="manageNo">管理番号</param>
        /// <param name="userId">ユーザーID</param>
        private void getMitakaData(string manageNo, string userId)
        {
            // 過去トラ観たか情報インスタンス定義（初期化）
            // 過去トラ観たか情報 コンストラクタ
            _MitakaData = new MitakaData(manageNo, userId);

            ViewState["MITAKA_NO"] = _MitakaData.ManageNo; // 管理番号
            ViewState["LAST_UPDATE_USER"] = _MitakaData.UpdateUserName; // 最終更新者
            ViewState["LAST_UPDATE_YMD"] = _MitakaData.UpdateDate; // 最終更新日時

            ViewState["TITLE"] = _MitakaData.Title; // タイトル
            ViewState["PURPOSE"] = _MitakaData.Purpose; // 目的
            ViewState["STATUS"] = _MitakaData.Status; // 状況
            ViewState["START_YMD"] = _MitakaData.StartDateTime; // 回答開始日時
            ViewState["END_YMD"] = _MitakaData.EndDateTime; // 回答期限
            ViewState["USER_MAIN"] = _MitakaData.CreateMainUser["USER_ID"].ToString(); // 作成者(正)
            ViewState["USER_SUB"] = _MitakaData.CreateSubUser["USER_ID"].ToString(); // 作成者(副)
            ViewState["CHECK_USER"] = _MitakaData.InspectionUser["USER_ID"].ToString(); // 点検者
            // 回答依頼先
            if (_MitakaData.ConfirmationRequestList.Rows.Count == 0)
            {
                ViewState["ANSWER_REQUEST_USER"] = "";
            }
            for (int i = 0; i < _MitakaData.ConfirmationRequestList.Rows.Count; i++)
            {
                if(i == 0)
                {
                    ViewState["ANSWER_REQUEST_USER"] = _MitakaData.ConfirmationRequestList.Rows[i]["USER_ID"].ToString();
                }
                else
                {
                    ViewState["ANSWER_REQUEST_USER"] += "," + _MitakaData.ConfirmationRequestList.Rows[i]["USER_ID"].ToString();
                }
            }
            // 開発符号
            if (_MitakaData.DevelopmentCodeList.Rows.Count == 0)
            {
                ViewState["DEVELOPMENT_CODE"] = "";
            }
            for (int i = 0; i < _MitakaData.DevelopmentCodeList.Rows.Count; i++)
            {
                if (i == 0)
                {
                    ViewState["DEVELOPMENT_CODE"] = _MitakaData.DevelopmentCodeList.Rows[i]["DEVELOPMENT_CODE"].ToString();
                }
                else
                {
                    ViewState["DEVELOPMENT_CODE"] += "," + _MitakaData.DevelopmentCodeList.Rows[i]["DEVELOPMENT_CODE"].ToString();
                }
            }
            // 機種
            if (_MitakaData.ModelList.Rows.Count == 0)
            {
                ViewState["MODEL"] = "";
            }
            for (int i = 0; i < _MitakaData.ModelList.Rows.Count; i++)
            {
                if (i == 0)
                {
                    ViewState["MODEL"] = _MitakaData.ModelList.Rows[i]["MODEL"].ToString();
                }
                else
                {
                    ViewState["MODEL"] += "," + _MitakaData.ModelList.Rows[i]["MODEL"].ToString();
                }
            }
            // 設通番号
            if (_MitakaData.EcsList.Rows.Count == 0)
            {
                ViewState["ECS_NO"] = "";
            }
            for (int i = 0; i < _MitakaData.EcsList.Rows.Count; i++)
            {
                if (i == 0)
                {
                    ViewState["ECS_NO"] = _MitakaData.EcsList.Rows[i]["ECS_NO"].ToString();
                }
                else
                {
                    ViewState["ECS_NO"] += "," + _MitakaData.EcsList.Rows[i]["ECS_NO"].ToString();
                }
            }
            // BLK No
            if (_MitakaData.BlockList.Rows.Count == 0)
            {
                ViewState["BLK_NO"] = "";
            }
            for (int i = 0; i < _MitakaData.BlockList.Rows.Count; i++)
            {
                if (i == 0)
                {
                    ViewState["BLK_NO"] = _MitakaData.BlockList.Rows[i]["BLK_NO"].ToString();
                }
                else
                {
                    ViewState["BLK_NO"] += "," + _MitakaData.BlockList.Rows[i]["BLK_NO"].ToString();
                }
            }
            // タイトル品番
            if (_MitakaData.TitleDrawingList.Rows.Count == 0)
            {
                ViewState["TITLE_DRAWING_NO"] = "";
            }
            for (int i = 0; i < _MitakaData.TitleDrawingList.Rows.Count; i++)
            {
                if (i == 0)
                {
                    ViewState["TITLE_DRAWING_NO"] = _MitakaData.TitleDrawingList.Rows[i]["TITLE_DRAWING_NO"].ToString();
                }
                else
                {
                    ViewState["TITLE_DRAWING_NO"] += "," + _MitakaData.TitleDrawingList.Rows[i]["TITLE_DRAWING_NO"].ToString();
                }
            }
            // 過去トラ確認結果まとめ
            ViewState["COMMENT"] = _MitakaData.Comment;
            // 抽出条件　検索
            _SearchCondition = _MitakaData.SearchParameterList;
            // 回答リスト　過去トラ情報
            _TroubleData = _MitakaData.DeploymentTroubleList;
            // 観たか回答情報
            _MitakaAnswerData = _MitakaData.MitakaAnswerData;
            // 観たかヘッダー.状況　"10"(回答準備)or"20"(回答中)or"30"(確認完了)or"99"(取消)
            _Status = _MitakaData.Status;

            // （隠し項目）
            // 作成者（正）（隠し項目）
            ViewState["HDN_USER_MAIN"] =
                _MitakaData.CreateMainUser["USER_ID"].ToString() + "<>" + _MitakaData.CreateMainUser["USER_NAME"].ToString() +
                "<>" + _MitakaData.CreateMainUser["BU_CODE"].ToString() + "<>" + _MitakaData.CreateMainUser["KA_CODE"].ToString() +
                "<>" + _MitakaData.CreateMainUser["MAIL"].ToString();
            // 作成者（副）（隠し項目）
            ViewState["HDN_USER_SUB"] =
                _MitakaData.CreateSubUser["USER_ID"].ToString() + "<>" + _MitakaData.CreateSubUser["USER_NAME"].ToString() +
                "<>" + _MitakaData.CreateSubUser["BU_CODE"].ToString() + "<>" + _MitakaData.CreateSubUser["KA_CODE"].ToString() +
                "<>" + _MitakaData.CreateSubUser["MAIL"].ToString();

            // 回答依頼先（隠し項目）
            if(_MitakaData.ConfirmationRequestList.Rows.Count == 0)
            {
                ViewState["HDN_ANSWER_REQUEST_USER"] = "";
            }
            for (int i = 0; i < _MitakaData.ConfirmationRequestList.Rows.Count; i++)
            {
                if (i == 0)
                {
                    ViewState["HDN_ANSWER_REQUEST_USER"] = _MitakaData.ConfirmationRequestList.Rows[i]["USER_ID"].ToString() + "<>" +
                        _MitakaData.ConfirmationRequestList.Rows[i]["USER_NAME"].ToString() + "<>" +
                        _MitakaData.ConfirmationRequestList.Rows[i]["BU_CODE"].ToString() + "<>" +
                        _MitakaData.ConfirmationRequestList.Rows[i]["KA_CODE"].ToString() + "<>" +
                        _MitakaData.ConfirmationRequestList.Rows[i]["MAIL"].ToString();
                }
                else
                {
                    ViewState["HDN_ANSWER_REQUEST_USER"] += "<->" + _MitakaData.ConfirmationRequestList.Rows[i]["USER_ID"].ToString() + "<>" +
                        _MitakaData.ConfirmationRequestList.Rows[i]["USER_NAME"].ToString() + "<>" +
                        _MitakaData.ConfirmationRequestList.Rows[i]["BU_CODE"].ToString() + "<>" +
                        _MitakaData.ConfirmationRequestList.Rows[i]["KA_CODE"].ToString() + "<>" +
                        _MitakaData.ConfirmationRequestList.Rows[i]["MAIL"].ToString();
                }
            }

            // 開発符号（隠し項目）
            if(_MitakaData.DevelopmentCodeList.Rows.Count == 0)
            {
                ViewState["HDN_DEVELOPMENT_CODE"] = "";
            }
            for (int i = 0; i < _MitakaData.DevelopmentCodeList.Rows.Count; i++)
            {
                if (i == 0)
                {
                    ViewState["HDN_DEVELOPMENT_CODE"] = _MitakaData.DevelopmentCodeList.Rows[i]["DEVELOPMENT_CODE"].ToString();
                }
                else
                {
                    ViewState["HDN_DEVELOPMENT_CODE"] += "<->" + _MitakaData.DevelopmentCodeList.Rows[i]["DEVELOPMENT_CODE"].ToString();
                }
            }

            // 機種（隠し項目）
            if (_MitakaData.ModelList.Rows.Count == 0)
            {
                ViewState["HDN_MODEL"] = "";
            }
            for (int i = 0; i < _MitakaData.ModelList.Rows.Count; i++)
            {
                if (i == 0)
                {
                    ViewState["HDN_MODEL"] = _MitakaData.ModelList.Rows[i]["MODEL"].ToString();
                }
                else
                {
                    ViewState["HDN_MODEL"] += "<->" + _MitakaData.ModelList.Rows[i]["MODEL"].ToString();
                }
            }

            // BLK No（隠し項目）
            if (_MitakaData.BlockList.Rows.Count == 0)
            {
                ViewState["HDN_BLK_NO"] = "";
            }
            for (int i = 0; i < _MitakaData.BlockList.Rows.Count; i++)
            {
                if (i == 0)
                {
                    ViewState["HDN_BLK_NO"] = _MitakaData.BlockList.Rows[i]["BLK_NO"].ToString();
                }
                else
                {
                    ViewState["HDN_BLK_NO"] += "<->" + _MitakaData.BlockList.Rows[i]["BLK_NO"].ToString();
                }
            }

            // タイトル品番（隠し項目）
            if (_MitakaData.TitleDrawingList.Rows.Count == 0)
            {
                ViewState["HDN_TITLE_DRAWING_NO"] = "";
            }
            for (int i = 0; i < _MitakaData.TitleDrawingList.Rows.Count; i++)
            {
                if (i == 0)
                {
                    ViewState["HDN_TITLE_DRAWING_NO"] = _MitakaData.TitleDrawingList.Rows[i]["TITLE_DRAWING_NO"].ToString();
                }
                else
                {
                    ViewState["HDN_TITLE_DRAWING_NO"] += "<->" + _MitakaData.TitleDrawingList.Rows[i]["TITLE_DRAWING_NO"].ToString();
                }
            }

            // 設通番号（隠し項目）
            if (_MitakaData.EcsList.Rows.Count == 0)
            {
                ViewState["HDN_ECS_NO"] = "";
            }
            for (int i = 0; i < _MitakaData.EcsList.Rows.Count; i++)
            {
                if (i == 0)
                {
                    ViewState["HDN_ECS_NO"] = _MitakaData.EcsList.Rows[i]["ECS_NO"].ToString();
                }
                else
                {
                    ViewState["HDN_ECS_NO"] += "<->" + _MitakaData.EcsList.Rows[i]["ECS_NO"].ToString();
                }
            }

            // 展開対象（隠し項目）
            if (_MitakaAnswerData.MitakaAnswerList.Rows.Count == 0)
            {
                ViewState["HDN_DEPLOYMENT_TARGET"] = "";
            }
            for (int i = 0; i < _MitakaAnswerData.MitakaAnswerList.Rows.Count; i++)
            {
                if (i == 0)
                {
                    ViewState["HDN_DEPLOYMENT_TARGET"] = _MitakaAnswerData.MitakaAnswerList.Rows[i]["SYSTEM_NO"].ToString() + "<>" +
                        _MitakaAnswerData.MitakaAnswerList.Rows[i]["TARGET_FLG"].ToString() + "<>" + _MitakaAnswerData.MitakaAnswerList.Rows[i]["ANSWER_DIVISION_CODE"].ToString();
                }
                else
                {
                    ViewState["HDN_DEPLOYMENT_TARGET"] += "<->" + _MitakaAnswerData.MitakaAnswerList.Rows[i]["SYSTEM_NO"].ToString() + "<>" +
                        _MitakaAnswerData.MitakaAnswerList.Rows[i]["TARGET_FLG"].ToString() + "<>" + _MitakaAnswerData.MitakaAnswerList.Rows[i]["ANSWER_DIVISION_CODE"].ToString();
                }
            }
            //HdnDeploymentTarget.Value = ""; 

        }

        /// <summary>
        /// 過去トラ観たか情報登録
        /// </summary>
        private void postMitakaData()
        {
            ClientScriptManager csManager = Page.ClientScript;
            Type csType = this.GetType();
            ArrayList arrayMessage = new ArrayList();
            bool resultFlg = true;

            // 画面項目を過去トラ観たか情報にセット
            setScreenItemMitakaData();

            // 過去トラ観たか情報登録・更新
            resultFlg = _MitakaData.postMitakaData();

            if (resultFlg == true)
            {
                // ログインユーザーID取得
                string sUser = bcom.GetWindowsUser();

                // 過去トラ観たか情報取得
                getMitakaData(_QueryManagementNo, sUser);
            }
            else
            {
                arrayMessage.Add(Def.DefMsg_INSERTERR);
                bcom.ShowMessage(csType, csManager, arrayMessage);
                return;
            }
        }


        /// <summary>
        /// 過去トラ観たか情報削除
        /// </summary>
        private void deleteMitakaData()
        {
            ClientScriptManager csManager = Page.ClientScript;
            Type csType = this.GetType();
            ArrayList arrayMessage = new ArrayList();
            bool resultFlg = true;

            // 観たかヘッダー.状況　"10"(回答準備)or"20"(回答中)or"30"(確認完了)or"99"(取消) 
            if (_Status == "10")
            {
                // 過去トラ観たか情報削除
                resultFlg = _MitakaData.deleteMitakaData();

                if (resultFlg == true)
                {
                    // ログインユーザーID取得
                    string sUser = bcom.GetWindowsUser();

                    // 過去トラ観たか情報取得
                    getMitakaData(_QueryManagementNo, sUser);
                }
                else
                {
                    arrayMessage.Add(Def.DefMsg_DELETEERR);
                    bcom.ShowMessage(csType, csManager, arrayMessage);
                    return;
                }
            }
            else
            {
                // 過去トラ観たか情報取消
                resultFlg = _MitakaData.cancelMitakaData();

                if (resultFlg == true)
                {
                    // ログインユーザーID取得
                    string sUser = bcom.GetWindowsUser();

                    // 過去トラ観たか情報取得
                    getMitakaData(_QueryManagementNo, sUser);
                }
                else
                {
                    arrayMessage.Add(Def.DefMsg_CANCELERR);
                    bcom.ShowMessage(csType, csManager, arrayMessage);
                    return;
                }
            }
        }

        /// <summary>
        /// 過去トラ観たか情報回答依頼
        /// </summary>
        private void requestMitakaData()
        {
            ClientScriptManager csManager = Page.ClientScript;
            Type csType = this.GetType();
            ArrayList arrayMessage = new ArrayList();
            bool resultFlg = true;

            // 画面項目を過去トラ観たか情報にセット
            setScreenItemMitakaData();

            // 編集フラグが"1"の場合
            if (_MitakaData.EditFlg == true)
            {
                // 過去トラ観たか情報登録
                resultFlg = _MitakaData.postMitakaData();
                if (resultFlg == true)
                {
                    // 過去トラ観たか情報回答依頼
                    // 過去トラ観たか情報点検依頼
                    bool requestFlg = true;
                    requestFlg = _MitakaData.requestMitakaData();
                    if (requestFlg == true)
                    {
                        // 回答対象チェック
                        // ログインユーザーID取得
                        string sUser = bcom.GetWindowsUser();

                        // 過去トラ観たか情報取得
                        getMitakaData(_QueryManagementNo, sUser);
                    }
                    else
                    {
                        arrayMessage.Add(Def.DefMsg_REQUESTERR);
                        bcom.ShowMessage(csType, csManager, arrayMessage);
                        return;
                    }
                }
                else
                {
                    arrayMessage.Add(Def.DefMsg_INSERTERR);
                    bcom.ShowMessage(csType, csManager, arrayMessage);
                    return;
                }
            }
        }

        /// <summary>
        /// 過去トラ観たか情報確認完了
        /// </summary>
        private void confirmedMitakaData()
        {
            ClientScriptManager csManager = Page.ClientScript;
            Type csType = this.GetType();
            ArrayList arrayMessage = new ArrayList();
            bool resultFlg = true;

            // 画面項目を過去トラ観たか情報にセット
            setScreenItemMitakaData();

            // 編集フラグが"1"の場合
            if (_MitakaData.EditFlg == true)
            {
                // 過去トラ観たか情報登録
                resultFlg = _MitakaData.postMitakaData();
                if (resultFlg == true)
                {
                    // 過去トラ観たか情報確認完了
                    bool confirmFlg = true;
                    confirmFlg = _MitakaData.confirmedMitakaData();
                    if (confirmFlg == true)
                    {
                        // ログインユーザーID取得
                        string sUser = bcom.GetWindowsUser();

                        // 過去トラ観たか情報取得
                        getMitakaData(_QueryManagementNo, sUser);
                    }
                    else
                    {
                        arrayMessage.Add(Def.DefMsg_CONFIRMERR);
                        bcom.ShowMessage(csType, csManager, arrayMessage);
                        return;
                    }
                }
                else
                {
                    arrayMessage.Add(Def.DefMsg_INSERTERR);
                    bcom.ShowMessage(csType, csManager, arrayMessage);
                    return;
                }
            }
        }

        /// <summary>
        /// 過去トラ観たか情報完了取消
        /// </summary>
        private void cancelConfirmedMitakaData()
        {
            ClientScriptManager csManager = Page.ClientScript;
            Type csType = this.GetType();
            ArrayList arrayMessage = new ArrayList();
            bool resultFlg = true;

            // 画面項目を過去トラ観たか情報にセット
            setScreenItemMitakaData();

            // 編集フラグが"1"の場合
            if (_MitakaData.EditFlg == true)
            {
                // 過去トラ観たか情報登録
                resultFlg = _MitakaData.postMitakaData();
                if (resultFlg == true)
                {
                    // 過去トラ観たか情報完了取消
                    bool cancelFlg = true;
                    cancelFlg = _MitakaData.cancellConfirmedMitakaData();
                    if (cancelFlg == true)
                    {
                        // ログインユーザーID取得
                        string sUser = bcom.GetWindowsUser();

                        // 過去トラ観たか情報取得
                        getMitakaData(_QueryManagementNo, sUser);
                    }
                    else
                    {
                        arrayMessage.Add(Def.DefMsg_CANCELCONFIRMERR);
                        bcom.ShowMessage(csType, csManager, arrayMessage);
                        return;
                    }
                }
                else
                {
                    arrayMessage.Add(Def.DefMsg_INSERTERR);
                    bcom.ShowMessage(csType, csManager, arrayMessage);
                    return;
                }
            }
        }

        /// <summary>
        /// 画面クローズ処理
        /// </summary>
        private void closeMitakaRegist()
        {
            // 編集フラグが"1"の場合
            if (_MitakaData.EditFlg == true)
            {
                cPageLogic.getScriptForAlertMessage("AlertEditing");
            }
            else
            {
                cPageLogic.getScriptForCloseWindow();
            }
        }

        /// <summary>
        /// 画面項目を過去トラ観たか情報にセット
        /// </summary>
        private void setScreenItemMitakaData()
        {
            //string stkg = "<>";
            char chkg = char.Parse("<>");

            //string skg = "<->";
            char ckg = char.Parse("<->");

            // 作成者（正）（隠し項目）
            string[] stArrayUserMain = HdnUserMain.Value.Split(chkg);
            if (stArrayUserMain[0] != "")
            {
                for (int i = 0; i < stArrayUserMain.Length; i++)
                {
                    _MitakaData.CreateMainUser["USER_ID"] = stArrayUserMain[0];
                    _MitakaData.CreateMainUser["USER_NAME"] = stArrayUserMain[1];
                    _MitakaData.CreateMainUser["BU_CODE"] = stArrayUserMain[2];
                    _MitakaData.CreateMainUser["KA_CODE"] = stArrayUserMain[3];
                    _MitakaData.CreateMainUser["MAIL"] = stArrayUserMain[4];
                }
            }

            // 作成者（副）（隠し項目）
            string[] stArrayUserSub = HdnUserSub.Value.Split(chkg);
            if (stArrayUserSub[0] != "")
            {
                for (int i = 0; i < stArrayUserSub.Length; i++)
                {
                    _MitakaData.CreateSubUser["USER_ID"] = stArrayUserSub[0];
                    _MitakaData.CreateSubUser["USER_NAME"] = stArrayUserSub[1];
                    _MitakaData.CreateSubUser["BU_CODE"] = stArrayUserSub[2];
                    _MitakaData.CreateSubUser["KA_CODE"] = stArrayUserSub[3];
                    _MitakaData.CreateSubUser["MAIL"] = stArrayUserSub[4];
                }
            }

            // 回答依頼先（隠し項目）
            string[] stArrayAnswerRequest = HdnAnswerRequest.Value.Split(ckg);
            for (int i = 0; i < stArrayAnswerRequest.Length; i++)
            {
                string[] stArrayAnswerRequestDetail = stArrayAnswerRequest[i].Split(chkg);
                if (stArrayAnswerRequestDetail[0] != "")
                {
                    _MitakaData.ConfirmationRequestList.Rows[i]["USER_ID"] = stArrayAnswerRequestDetail[0];
                    _MitakaData.ConfirmationRequestList.Rows[i]["USER_NAME"] = stArrayAnswerRequestDetail[1];
                    _MitakaData.ConfirmationRequestList.Rows[i]["BU_CODE"] = stArrayAnswerRequestDetail[2];
                    _MitakaData.ConfirmationRequestList.Rows[i]["KA_CODE"] = stArrayAnswerRequestDetail[3];
                    _MitakaData.ConfirmationRequestList.Rows[i]["MAIL"] = stArrayAnswerRequestDetail[4];
                }
            }
            

            // 開発符号（隠し項目）
            string[] stArrayDevelopCode = HdnDevelopCode.Value.Split(ckg);
            if (stArrayDevelopCode[0] != "")
            {
                for (int i = 0; i < stArrayDevelopCode.Length; i++)
                {
                    _MitakaData.DevelopmentCodeList.Rows[i]["DEVELOPMENT_CODE"] = stArrayDevelopCode[i];
                }
            }

            // 機種（隠し項目）
            string[] stArrayHdnModel = HdnModel.Value.Split(ckg);
            if (stArrayHdnModel[0] != "")
            {
                for (int i = 0; i < stArrayHdnModel.Length; i++)
                {
                    _MitakaData.ModelList.Rows[i]["MODEL"] = stArrayHdnModel[i];
                }
            }

            // BLK No（隠し項目）
            string[] stArrayHdnBlkNo = HdnBlkNo.Value.Split(ckg);
            if (stArrayHdnBlkNo[0] != "")
            {
                for (int i = 0; i < stArrayHdnBlkNo.Length; i++)
                {
                    _MitakaData.BlockList.Rows[i]["BLK_NO"] = stArrayHdnBlkNo[i];
                }
            }

            // タイトル品番（隠し項目）
            string[] stArrayHdnTitleDrawingNo = HdnTitleDrawingNo.Value.Split(ckg);
            if (stArrayHdnTitleDrawingNo[0] != "")
            {
                for (int i = 0; i < stArrayHdnTitleDrawingNo.Length; i++)
                {
                    _MitakaData.TitleDrawingList.Rows[i]["TITLE_DRAWING_NO"] = stArrayHdnTitleDrawingNo[i];
                }
            }

            // 設通番号（隠し項目）
            string[] stArrayHdnEcsNo = HdnEcsNo.Value.Split(ckg);
            if (stArrayHdnEcsNo[0] != "")
            {
                for (int i = 0; i < stArrayHdnEcsNo.Length; i++)
                {
                    _MitakaData.EcsList.Rows[i]["ECS_NO"] = stArrayHdnEcsNo[i];
                }
            }

            // タイトル
            _MitakaData.Title = TxtTitle.Text;

            // 目的
            _MitakaData.Purpose = TxtPurpose.Text;

            // 回答期限
            _MitakaData.EndDateTime = TxtAnswerLimit.Text;

            // 点検者
            _MitakaData.InspectionUser["USER_ID"] = TxtCheckUser.Text;

            // 過去トラ確認結果まとめ
            _MitakaData.Comment = TxtSummary.Text;










        }
















        #endregion

        /// <summary>
        /// カテゴリ検索時表示名称フォーマット処理
        /// </summary>
        /// <param name="ParamCategory"></param>
        /// <param name="categoryName"></param>
        /// <param name="oldKey"></param>
        private void FormatTroubleParameter(List<string> ParamCategory, ref string categoryName, ref string oldKey)
        {
            //if (_ParamCotegoryTable.Rows.Count > 0)
            //{
            //    for (int i = 0; i < _ParamCotegoryTable.Rows.Count; i++)
            //    {
            //        string categoryKey = setTypeName(_ParamCotegoryTable.Rows[i]["TableType"].ToString());
            //        // カンマ区切りで配列化し、最終要素の値を取得する※名称のみを取り出す
            //        string itemName = _ParamCotegoryTable.Rows[i]["ItemValue1"].ToString().Split(',').Last();


            //        if (i == 0)
            //        {
            //            categoryName = "";
            //            categoryName = categoryKey + "：" + itemName;
            //        }
            //        else if (categoryKey != oldKey)
            //        {
            //            ParamCategory.Add(categoryName);
            //            categoryName = "";
            //            categoryName = categoryKey + "：" + itemName;
            //        }
            //        else
            //        {
            //            categoryName = categoryName + " " + itemName;
            //        }

            //        oldKey = categoryKey;
            //    }
            //    ParamCategory.Add(categoryName);
            //}
            //else
            //{
            //    ParamCategory.Add((String)ViewState[Def.DefSERCH_WORD]);
            //}
        }

        /// <summary>
        /// 検索タイプ名取得
        /// </summary>
        /// <param name="Type">検索タイプ</param>
        private String setTypeName(String Type)
        {
            String typeName = "";

            switch (Type)
            {
                case Def.DefTYPE_WORD:
                    // 文字列検索
                    typeName = "文字列検索";
                    break;
                case Def.DefTYPE_TOP10:
                    // ＴＯＰ１０
                    typeName = "ＴＯＰ１０";
                    break;
                case Def.DefTYPE_BUSYO:
                    // 部署
                    typeName = "部署";
                    break;
                case Def.DefTYPE_PARTS:
                    // 部品・部位
                    typeName = "部品・部位";
                    break;
                case Def.DefTYPE_KAIHATU:
                    // 開発符号
                    typeName = "開発符号";
                    break;
                case Def.DefTYPE_GENSYO:
                    // 現象（分類）
                    typeName = "現象（分類）";
                    break;
                case Def.DefTYPE_GENIN:
                    // 原因（分類）
                    typeName = "原因（分類）";
                    break;
                case Def.DefTYPE_SYAKATA:
                    // 車型特殊
                    typeName = "車型特殊";
                    break;
                case Def.DefTYPE_SGENSYO:
                    // 現象（制御系）
                    typeName = "現象（制御系）";
                    break;
                case Def.DefTYPE_SYOUIN:
                    // 要因（制御系）
                    typeName = "要因（制御系）";
                    break;
                case Def.DefTYPE_EGTM:
                    // EGTM形式
                    typeName = "ＥＧＴＭ形式";
                    break;
                case Def.DefTYPE_TOP40:
                    // TOP40
                    typeName = "ＴＯＰ４０";
                    break;
                case Def.DefTYPE_RIPRO20:
                    // リプロ20
                    typeName = "リプロ２０";
                    break;
            }

            return typeName;
        }

        #endregion
    }
}