using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OldTigerWeb.Const
{
    public class Def
    {
        // エラーメッセージ
        public const string DefMsg_URLERR         = "不正なアクセスがされました。";
        public const string DefMsg_USERERR        = "ユーザ登録されていない為、使用できません。";
        public const string DefMsg_DATA_NOTFOUND  = "過去トラデータが存在しません。";
        public const string DefMsg_KAITO_NOTFOUND = "フォロー回答情報が、他で削除されました。\\n画面を終了して下さい。";
        public const string DefMsg_KAITO_EDITED   = "フォロー回答情報が、他で変更されました。\\n再度、表示し直して下さい。";
        public const string DefMsg_KAITO_COMPLETE = "フォロー回答情報を更新しました。";
        public const string DefMsg_FILE_NOTFOUND  = "関連資料ファイルが存在しません。";

        // 引数・セッション変数・ビューステート
        public const string DefPARA_KANRINO = "KANRINO";    // 管理番号
        public const string DefPARA_TYPE    = "TYPE";       // 検索タイプ
        public const string DefPARA_WORD    = "WORD";       // 検索文字
        public const string DefPARA_ARRY    = "ARRAY";      // 配列・各マスタor設計部署
        public const string DefPARA_ARRY2   = "ARRAY2";     // 配列２・評価部署
        public const string DefPARA_CATNM   = "CATNM";      // カテゴリ検索名
        public const string DefPARA_FOLLOW  = "FOLLOW";     // フォロー情報キー
        public const string DefPARA_EVENTNM = "EventName";  // フォローイベント名
        public const string DefSERCH_WORD   = "SerchWord";  // 検索条件・全て
        public const string DefPARA_FMCMC = "FMCMC";     // ＦＭＣ／ｍｃ区分情報キー
        public const string DefPARA_KAIHATSUID = "KAIHATSUID";     // 開発符号情報キー  
        public const string DefPARA_BYPU = "BYPU";     // ＢＹＰＵ区分情報キー  
        public const string DefPARA_EVENTNO = "EVENTNO";     // イベントＮＯ情報キー  
        public const string DefPARA_FOLLOWNO = "FOLLOWNO";     // フォロー管理ＮＯ情報キー  
        public const string DefPARA_KACODE = "KACODE";     // 課・主査コード情報キー  
        public const string DefPARA_SYSTEMNO = "SYSTEMNO";     // システム管理番号情報キー 
        public const string DefPARA_TABS = "Tabs";       // タブタイプ 
        public const string DefPARA_CONDITION_FLG = "CONDITION_FLG";       // And・Or検索条件 
        public const string DefPARA_TABLE = "TABLE";       // Datatable
        //20170201 機能改善 START
        public const string DefPARA_TROUBLELISTBY = "TROUBLELISTBY";        // トラブルリスト（BY）
        public const string DefPARA_TROUBLELISTPU = "TROUBLELISTPU";        // トラブルリスト（PU）
        //20170201 機能改善 END
        // KATO/CEC 追加（PDFブラウザ表示）
        public const string DefPDF_FileNo = "fileNo";   // 関連資料NO
        public const string DefPDF_Asterisk = "*";        // アスタリスク
        public const string DefPDF_ExtensionPDF = ".pdf";     // 拡張子:PDF

        // FMC/mc区分
        public const string DefTYPE_FMC = "1";              // 1;FMC
        public const string DefTYPE_mc  = "2";              // 2:Tmc

        // FMC/mc区分
        public const string DefMODE_DISP  = "1";            // 1;画面
        public const string DefMODE_EXCEL = "2";            // 2:Excel

        // 現在/過去区分
        public const string DefTYPE_Now = "1";              // 1;現在
        public const string DefTYPE_Old = "2";              // 2:T過去

        // AND/OR区分
        public const string DefTYPE_AND = "1";             // 1:AND
        public const string DefTYPE_OR = "2";              // 2:OR
        
        // 検索タイプ
        public const string DefTYPE_WORD    = "1";          // 1;文字列検索
        public const string DefTYPE_TOP10   = "2";          // 2:TOP10検索
        public const string DefTYPE_BUSYO   = "3";          // 3:設計部署検索
        public const string DefTYPE_HYOUKA  = "14";         // 14:評価部署検索
        public const string DefTYPE_BUSYO_BY = "31";        // 31:部署(BY)
        public const string DefTYPE_BUSYO_PU = "32";        // 32:部署(PU)
        public const string DefTYPE_PARTS   = "4";          // 4:部品・部品部位検索
        public const string DefTYPE_PARTS_S = "41";         // 41:部品・システム
        public const string DefTYPE_PARTS_N = "42";         // 42:部品・絞込み
        public const string DefTYPE_KAIHATU = "5";          // 5:開発符号
        public const string DefTYPE_GENSYO  = "6";          // 6:現象（分類）
        public const string DefTYPE_GENIN   = "7";          // 7:原因（分類）
        public const string DefTYPE_SYAKATA = "8";          // 8:車型特殊
        public const string DefTYPE_SGENSYO = "9";          // 9:現象（制御系）
        public const string DefTYPE_SYOUIN  = "10";         // 10:要因（制御系）
        public const string DefTYPE_EGTM    = "11";         // 11:EGTM形式
        public const string DefTYPE_TOP40   = "12";         // 12:TOP40
        public const string DefTYPE_RIPRO20 = "13";         // 13:リプロ20
        public const string DefTYPE_DETAIL  = "90";         // 90:過去トラ項目詳細画面表示
        //public const string DefTYPE_DOCUMENT= "99";         // 99:過去トラ関連資料ブラウザ表示

        // 検索名
        public const string DefTYPENM_WORD    = "文字列検索";      // 1;文字列検索
        public const string DefTYPENM_TOP10   = "ＴＯＰ１０検索";  // 2:TOP10検索
        public const string DefTYPENM_BUSYO   = "部署";            // 3:部署
        public const string DefTYPENM_PARTS   = "部品・部位";      // 4:部品・部位
        public const string DefTYPENM_KAIHATU = "開発符号";        // 5:開発符号
        public const string DefTYPENM_GENSYO  = "現象（分類）";    // 6:現象（分類）
        public const string DefTYPENM_GENIN   = "原因（分類）";    // 7:原因（分類）
        public const string DefTYPENM_SYAKATA = "車型特殊";        // 8:車型特殊
        public const string DefTYPENM_SGENSYO = "現象（制御系）";  // 9:現象（制御系）
        public const string DefTYPENM_SYOUIN  = "要因（制御系）";  // 10:要因（制御系）
        public const string DefTYPENM_EGTM    = "ＥＧＴＭ形式";    // 11:EGTM形式
        public const string DefTYPENM_TOP40   = "ＴＯＰ４０";      // 12:TOP40
        public const string DefTYPENM_RIPRO20 = "リプロ２０";      // 13:リプロ20

        //20170201 機能改善 START
        // 検索履歴
        public const string DefHISTORY = "HISTORY"; // 検索履歴
        public const int DefSEARCH_BUSYO = 4;       // 設計部署
        public const int DefSEARCH_HYOUKA = 5;      // 評価部署
        public const int DefSEARCH_PARTS_S = 6;     // 部品・システム
        public const int DefSEARCH_PARTS_N = 7;     // 部品・絞込み
        public const int DefSEARCH_KAIHATU = 8;     // 開発符号
        public const int DefSEARCH_GENSYO = 9;      // 現象(分類)
        public const int DefSEARCH_GENIN = 10;      // 原因(分類)
        public const int DefSEARCH_SYAKATA = 11;    // 車型特殊
        public const int DefSEARCH_SGENSYO = 12;    // 現象(制御系)
        public const int DefSEARCH_SYOUIN = 13;     // 要因(制御系)
        public const int DefSEARCH_EGTM = 14;       // EGTM形式
        //20170201 機能改善 START

            // フォロー情報回答・明細行数
        public const int DefFOLLOW_LINECNT = 6;                     // 明細数

        // EXCEL 過去トラベタ出力
        public const string DefKakotoraExcelName = "Cacotola_";     // ファイル名
        public const string DefKakotoraWorksheetName = "過去トラベタ出力";  // ワークシート名
        public const int DefCREATEYMD_ROW = 1;                      // 作成日行
        public const int DefCREATEYMD_CLM = 7;                      // 作成日列
        public const int DefCONDITION_ROW = 2;                      // 検索条件行
        public const int DefCONDITION_CLM = 4;                      // 検索条件列
        //20170201 機能改善 START
        public const int DefCATEGORY_ROW = 3;                       // カテゴリ行
        public const int DefCATEGORY_CLM = 4;                       // カテゴリ列
        //20170201 機能改善 END

        //20170201 機能改善 START
        //public const int DefMEISAISTART_ROW = 5;                    // 明細開始行
        public const int DefMEISAISTART_ROW = 6;                    // 明細開始行
        //20170201 機能改善 END
        public const int DefNO_CLM = 1;                             // №列
        public const int DefSTATUS_CLM = 2;                         // 進捗列
        public const int DefKOUMOKUNO_CLM = 3;                      // 項目管理No.列
        public const int DefKOUMOKU_CLM = 4;                        // 項目列
        public const int DefFMC_CLM = 5;                            // FMC列
        public const int DefGENSYO_CLM = 6;                         // 現象列
        public const int DefJOKYO_CLM = 7;                          // 状況列
        public const int DefGENIN_CLM = 8;                          // 原因列
        public const int DefTAISAKU_CLM = 9;                        // 対策列
        public const int DefHAKKEN_CLM = 10;                        // 未発見理由列
        public const int DefKANTEN_CLM = 11;                        // 確認の観点列
        public const int DefSBOUSISAKU_CLM = 12;                    // 再発防止策_設計面列
        public const int DefHBOUSISAKU_CLM = 13;                    // 再発防止策_評価面列
        public const int DefSEKKEI_CLM = 14;                        // 設計列
        public const int DefHYOUKA_CLM = 15;                        // 評価列
        public const int DefSIRYO_CLM = 16;                         // 資料No.一覧列
        public const int DefRANK_CLM = 17;                          // 重要度ランク列
        public const int DefSAIHATU_CLM = 18;                       // 再発案件列
        public const int DefRSC_CLM = 19;                           // RSC項目列
        public const int DefSYUMU_CLM = 20;                         // 主務部署列
        public const int DefCHECKA_CLM = 21;                        // チェック欄A列
        public const int DefCHECKB_CLM = 22;                        // チェック欄B列
        public const int DefCHECKC_CLM = 23;                        // チェック欄C列

        // EXCEL    フォロー回答情報出力
        public const string DefRUIJI = "類似";                    　// 類似
        public const string DefFollowExcelName = "Caco Follow_";       // ファイル名
        public const string DefFollowWorksheetName = "フォロー情報出力";  // ワークシート名
        public const int DefHDFCREATEYMD_ROW = 1;                   // 作成日行
        public const int DefHDFCREATEYMD_CLM = 12;                  // 作成日列
        public const int DefHDFCONDITION_ROW = 1;                   // 進捗行
        public const int DefHDFCONDITION_CLM = 6;                   // 進捗列
        public const int DefHDFEVENT_ROW = 2;                       // イベント名行
        public const int DefHDFEVENT_CLM = 18;                      // イベント名列

        public const int DefFMEISAISTART_ROW = 4;                   // 明細開始行
        public const int DefFNO_CLM = 1;                            // №列
        public const int DefFBUHIN_CLM = 2;                         // 部品列
        public const int DefFGENSYO_CLM = 3;                        // 現象列
        public const int DefFSEIGYOFACTOR_CLM = 4;                  // 制御系要因列
        public const int DefFSINCHOKU_CLM = 5;                      // 進捗列
        public const int DefFKANRINO_CLM = 6;                       // 項目管理No.列
        public const int DefFKOUMOKU_CLM = 7;                       // 項目列
        public const int DefFGENIN_CLM = 8;                         // 原因列
        public const int DefFTAISAKU_CLM = 9;                       // 対策列
        public const int DefFHAKKEN_CLM = 10;                       // 開発時の流出要因列
        public const int DefFKANTEN_CLM = 11;                       // 確認の観点列
        public const int DefFSBOUSISAKU_CLM = 12;                   // 再発防止策_設計面列
        public const int DefFHBOUSISAKU_CLM = 13;                   // 再発防止策_評価面列
        public const int DefFSIRYO_CLM = 14;                        // 資料No.一覧列
        public const int DefFBUSYO_CLM = 15;                        // 部署列
        public const int DefFSQB_CLM = 16;                          // SQB列
        public const int DefFSHYOUKA_CLM = 17;                      // 設計評価列
        public const int DefFHEARING_CLM = 18;                      // ﾋｱﾘﾝｸﾞ要望列
        public const int DefFSINDO_CLM = 19;                        // 進度列
        public const int DefFKAITO_CLM = 20;                        // 対応内容列
        public const int DefFRANK_CLM = 21;                         // 重要度ランク列
        public const int DefFSAIHATU_CLM = 22;                      // 再発案件列
        public const int DefFRSC_CLM = 23;                          // RSC項目列
        public const int DefFSYUMU_CLM = 24;                        // 主務部署列

        #region API通信用共通パラメータ
        /// <summary>
        /// MediaType : JSON
        /// </summary>
        public const string MEDIA_TYPE_JSON = "application/json";
        /// <summary>
        /// HTTPリクエストヘッダータイプ：GET
        /// </summary>
        public const string API_HTTP_REQUEST_GET = "GET";

        // API通信用URIパラメータ
        /// <summary>
        /// ベースURL
        /// </summary>
        public const string API_BASE_URL = "http://{0}/";    
        /// <summary>
        /// 設通リスト取得用URI
        /// api/EcsList/{EcsNo}
        /// </summary>
        //public const string API_URL = "api/EcsList/{0}";
        public const string API_ECSLIST_URL = "EcsList/{0}";
        /// <summary>
        /// 設通取得用URI
        /// api/Ecs/{EcsNo}/{EcsSeries}
        /// </summary>
        public const string API_ECS_URL = "Ecs/{0}/{1}";

        /// <summary>
        /// 図面取得用URI
        /// api/DrawingList/{DrawingNo}/
        /// </summary>
        //public const string API_URL = "api/DrawingList/{0}/{1}";
        public const string API_DRAWINGLIST_URL = "DrawingList/{0}";

        #endregion

        #region "ページ判定用文字列"
        public const string DefPageId_TroubleList = "frmTroubleList";       // 過去トラ検索結果画面
        public const string DefPageId_FollowAnswer = "frmFollowAnswer";     // フォロー情報画面
        public const string DefPageId_Loading = "frmLoading";               // ロード画面
        #endregion

    }
}