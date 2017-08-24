using System;
using System.Linq;
using System.Data;
//using System.Net.Http;
//using System.Threading;
//using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Web.UI;

using OfficeOpenXml;
//20170306 START k-ohmatsuzawa EXCEL表示修正
using System.Collections;
using System.Collections.Generic;
//20170306 END k-ohmatsuzawa

namespace OldTigerWeb
{
    public partial class frmTroubleList : System.Web.UI.Page
    {
        public DataTable gbTrableData = null, troubleListBY = null, 
            excelTroubleList = null, troubleListPU = null, editDt = null ;
        public string userSight;
        public int rowCount = 0;
        public bool saveFlg = false;

        #region "フィールド"
        /// <summary>
        /// 共通ビジネスロジック定義
        /// </summary>
        CommonLogic bcom = new CommonLogic();
        /// <summary>
        /// 過去トラリストビジネスロジック定義
        /// </summary>
        BuisinessLogic.BLTroubleList bLogic = new BuisinessLogic.BLTroubleList();
        /// <summary>
        /// ページ埋め込みロジック定義
        /// </summary>
        CommonPageLogic cPageLogic = new CommonPageLogic();
        #endregion

        /// <summary>
        /// ページロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // ポストバック時はリターン
            if (IsPostBack == true)
            {
                return;
            }

            //------ TYPE:検索タイプ
            // 1;文字列検索
            // 2:TOP10検索
            // 3:部署
            // 4:部品
            // 5:開発符号
            // 6:現象（分類）
            // 7:原因（分類）
            // 8:車型特殊
            // 9:現象（制御系）
            // 10:要因（制御系）
            // 11:EGTM形式
            // 12:TOP40
            // 13:リプロ20

            String paraType = (String)Session[Def.DefPARA_TYPE];              // 検索タイプ
            String paraWord = (String)Session[Def.DefPARA_WORD];              // 文字列検索
            DataTable paraTable = (DataTable)Session[Def.DefPARA_TABLE];      // Datatable
            String paraCondition = (String)Session[Def.DefPARA_CONDITION_FLG];// And・Or検索条件
            String paraCategoryCondition = (String)Session[Def.DefPARA_CATEGORY_CONDITION_FLG];// カテゴリ検索用 And・Or検索条件 // 20170719 Add
            ViewState[Def.DefPARA_TYPE] = paraType;       // 検索タイプ
            ViewState[Def.DefPARA_WORD] = paraWord;       // 文字列検索
            ViewState[Def.DefPARA_TABLE] = paraTable;               // Datatable
            ViewState[Def.DefPARA_CONDITION_FLG] = paraCondition;   // And・Or検索条件
            if ((String)ViewState[Def.DefPARA_CONDITION_FLG] == Def.DefTYPE_AND)
            ViewState[Def.DefPARA_TYPE] = paraType;       // 検索タイプ
            ViewState[Def.DefPARA_WORD] = paraWord;       // 文字列検索
            ViewState[Def.DefPARA_TABLE] = paraTable;               // Datatable
            ViewState[Def.DefPARA_CONDITION_FLG] = paraCondition;   // キーワード検索用 And・Or検索条件
            ViewState[Def.DefPARA_CATEGORY_CONDITION_FLG] = paraCategoryCondition;   // カテゴリ検索用 And・Or検索条件 // 20170719 Add

            //20170201 機能改善 START

            if ((String)ViewState[Def.DefPARA_CONDITION_FLG] == Def.DefTYPE_AND)
            {
                // テキストボックスカラー変更（AND）
                txtSearch.BackColor = System.Drawing.ColorTranslator.FromHtml("#66FFFF");
            }
            else　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　// OR条件
            {
                txtSearch.BackColor = System.Drawing.ColorTranslator.FromHtml("#99FF99");
            }
            if((string)ViewState[Def.DefPARA_CATEGORY_CONDITION_FLG] == Def.DefTYPE_AND)
            {
                lblCategory.BackColor = System.Drawing.ColorTranslator.FromHtml("#66FFFF");
            }
            else
            {
                lblCategory.BackColor = System.Drawing.ColorTranslator.FromHtml("#99FF99");

            }

            // SearchTroubleList(paraType, paraWord, paraArrWord, paraArrWord2, paraTable, paraCondition);
            SearchTroubleList(paraType, paraWord, paraTable, paraCondition , paraCategoryCondition); // 20170719 Add
            //20170201 機能改善 END
        }
        /// <summary>
        /// Excel 作成（Excelダウンロードボタン押下時）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 
        protected void btn_Excel_Click(Object sender, EventArgs e)
        {
            CommonLogic bcom = new CommonLogic();

            try
            {
                String paraTypeName = "";
                List<string> paraCategory = new List<string>();
                String paraType = (String)ViewState[Def.DefPARA_TYPE];            // 検索タイプ
                String paraWord = (String)ViewState[Def.DefPARA_WORD];            // 文字列検索
                DataTable paraTable = (DataTable)ViewState[Def.DefPARA_TABLE];          // Datatable
                String paraCondition = (String)ViewState[Def.DefPARA_CONDITION_FLG];  // And・Or検索条件

                String paraCategoryCondition = (String)ViewState[Def.DefPARA_CATEGORY_CONDITION_FLG];  // カテゴリ検索用 And・Or検索条件 // 20170719 Add

                BuisinessLogic.BLTroubleList bLogic = new BuisinessLogic.BLTroubleList();

                DataTable trableList = null;
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("ROWID");
                dtTemp.Columns.Add("RANK");
                dtTemp.Columns.Add("SAIHATU");
                dtTemp.Columns.Add("RSC");
                dtTemp.Columns.Add("SYUMU");
                dtTemp.Columns.Add("SYSTEM_NO");
                dtTemp.Columns.Add("BY_PU",typeof(String));　//KATO/CEC ADD 2016/09/15
                dtTemp.Columns.Add("KOUMOKU_KANRI_NO");
                dtTemp.Columns.Add("KOUMOKU");
                dtTemp.Columns.Add("FUGO_NAME1");
                dtTemp.Columns.Add("FUGO_NAME2");
                dtTemp.Columns.Add("FUGO_NAME3");
                dtTemp.Columns.Add("FUGO_NAME4");
                dtTemp.Columns.Add("FUGO_NAME5");
                dtTemp.Columns.Add("GENSYO_NAIYO");
                dtTemp.Columns.Add("JYOUKYO");
                dtTemp.Columns.Add("GENIN");
                dtTemp.Columns.Add("TAISAKU");
                dtTemp.Columns.Add("KAIHATU_MIHAKKEN_RIYU");
                dtTemp.Columns.Add("SQB_KANTEN");
                dtTemp.Columns.Add("SAIHATU_SEKKEI");
                dtTemp.Columns.Add("SAIHATU_HYOUKA");
                dtTemp.Columns.Add("BUSYO_SEKKEI1");
                dtTemp.Columns.Add("BUSYO_SEKKEI2");
                dtTemp.Columns.Add("BUSYO_SEKKEI3");
                dtTemp.Columns.Add("BUSYO_SEKKEI4");
                dtTemp.Columns.Add("BUSYO_SEKKEI5");
                dtTemp.Columns.Add("BUSYO_SEKKEI6");
                dtTemp.Columns.Add("BUSYO_SEKKEI7");
                dtTemp.Columns.Add("BUSYO_SEKKEI8");
                dtTemp.Columns.Add("BUSYO_SEKKEI9");
                dtTemp.Columns.Add("BUSYO_SEKKEI10");
                dtTemp.Columns.Add("BUSYO_HYOUKA1");
                dtTemp.Columns.Add("BUSYO_HYOUKA2");
                dtTemp.Columns.Add("BUSYO_HYOUKA3");
                dtTemp.Columns.Add("BUSYO_HYOUKA4");
                dtTemp.Columns.Add("BUSYO_HYOUKA5");
                dtTemp.Columns.Add("BUSYO_HYOUKA6");
                dtTemp.Columns.Add("BUSYO_HYOUKA7");
                dtTemp.Columns.Add("BUSYO_HYOUKA8");
                dtTemp.Columns.Add("BUSYO_HYOUKA9");
                dtTemp.Columns.Add("BUSYO_HYOUKA10");
                dtTemp.Columns.Add("SIRYOU_NO1");
                dtTemp.Columns.Add("SIRYOU_NO2");
                dtTemp.Columns.Add("SIRYOU_NO3");
                dtTemp.Columns.Add("SIRYOU_NO4");
                dtTemp.Columns.Add("SIRYOU_NO5");
                dtTemp.Columns.Add("KANREN_KANRI_NO");
                dtTemp.Columns.Add("INSERT_YMD", typeof(string)); // KATO/CEC ADD 2016/09/15
                dtTemp.Columns.Add("YMD_HENSYU", typeof(string)); // KATO/CEC ADD 2016/09/15
                dtTemp.Columns.Add("INPUT_ROW", typeof(int)); // KATO/CEC ADD 2016/09/15
                dtTemp.Columns.Add("SEQ", typeof(int)); // KATO/CEC ADD 2016/09/15

                // 過去トラ一覧リスト取得
                trableList = bLogic.GetToroubleList(Def.DefMODE_EXCEL, paraType, paraWord, paraTable, paraCondition,
                    paraCategoryCondition);

                DataTable excelListBY = dtTemp.Clone();
                DataTable excelListPU = dtTemp.Clone();
                excelTroubleList = dtTemp.Clone();
                DataTable tempBY = dtTemp.Clone();
                DataTable tempPU = dtTemp.Clone();
                DataRow drBY = null, drPU = null;

                for (int i = 0; trableList.Rows.Count > i; i++)
                {

                    //過去トラ検索結果一覧の拠点分け
                    if (trableList.Rows[i]["BY_PU"].ToString().Trim() == "BY")
                    {
                        drBY = tempBY.NewRow();
                        drBY["ROWID"] = trableList.Rows[i]["ROWID"];
                        drBY["RANK"] = trableList.Rows[i]["RANK"];
                        drBY["SAIHATU"] = trableList.Rows[i]["SAIHATU"];
                        drBY["RSC"] = trableList.Rows[i]["RSC"];
                        drBY["SYUMU"] = trableList.Rows[i]["SYUMU"];
                        drBY["SYSTEM_NO"] = trableList.Rows[i]["SYSTEM_NO"];
                        drBY["BY_PU"] = trableList.Rows[i]["BY_PU"];
                        drBY["KOUMOKU_KANRI_NO"] = trableList.Rows[i]["KOUMOKU_KANRI_NO"];
                        drBY["KOUMOKU"] = trableList.Rows[i]["KOUMOKU"];
                        drBY["FUGO_NAME1"] = trableList.Rows[i]["FUGO_NAME1"];
                        drBY["FUGO_NAME2"] = trableList.Rows[i]["FUGO_NAME2"];
                        drBY["FUGO_NAME3"] = trableList.Rows[i]["FUGO_NAME3"];
                        drBY["FUGO_NAME4"] = trableList.Rows[i]["FUGO_NAME4"];
                        drBY["FUGO_NAME5"] = trableList.Rows[i]["FUGO_NAME5"];
                        drBY["GENSYO_NAIYO"] = trableList.Rows[i]["GENSYO_NAIYO"];
                        drBY["JYOUKYO"] = trableList.Rows[i]["JYOUKYO"];
                        drBY["GENIN"] = trableList.Rows[i]["GENIN"];
                        drBY["TAISAKU"] = trableList.Rows[i]["TAISAKU"];
                        drBY["KAIHATU_MIHAKKEN_RIYU"] = trableList.Rows[i]["KAIHATU_MIHAKKEN_RIYU"];
                        drBY["SQB_KANTEN"] = trableList.Rows[i]["SQB_KANTEN"];
                        drBY["SAIHATU_SEKKEI"] = trableList.Rows[i]["SAIHATU_SEKKEI"];
                        drBY["SAIHATU_HYOUKA"] = trableList.Rows[i]["SAIHATU_HYOUKA"];
                        drBY["BUSYO_SEKKEI1"] = trableList.Rows[i]["BUSYO_SEKKEI1"];
                        drBY["BUSYO_SEKKEI2"] = trableList.Rows[i]["BUSYO_SEKKEI2"];
                        drBY["BUSYO_SEKKEI3"] = trableList.Rows[i]["BUSYO_SEKKEI3"];
                        drBY["BUSYO_SEKKEI4"] = trableList.Rows[i]["BUSYO_SEKKEI4"];
                        drBY["BUSYO_SEKKEI5"] = trableList.Rows[i]["BUSYO_SEKKEI5"];
                        drBY["BUSYO_SEKKEI6"] = trableList.Rows[i]["BUSYO_SEKKEI6"];
                        drBY["BUSYO_SEKKEI7"] = trableList.Rows[i]["BUSYO_SEKKEI7"];
                        drBY["BUSYO_SEKKEI8"] = trableList.Rows[i]["BUSYO_SEKKEI8"];
                        drBY["BUSYO_SEKKEI9"] = trableList.Rows[i]["BUSYO_SEKKEI9"];
                        drBY["BUSYO_SEKKEI10"] = trableList.Rows[i]["BUSYO_SEKKEI10"];
                        drBY["BUSYO_HYOUKA1"] = trableList.Rows[i]["BUSYO_HYOUKA1"];
                        drBY["BUSYO_HYOUKA2"] = trableList.Rows[i]["BUSYO_HYOUKA2"];
                        drBY["BUSYO_HYOUKA3"] = trableList.Rows[i]["BUSYO_HYOUKA3"];
                        drBY["BUSYO_HYOUKA4"] = trableList.Rows[i]["BUSYO_HYOUKA4"];
                        drBY["BUSYO_HYOUKA5"] = trableList.Rows[i]["BUSYO_HYOUKA5"];
                        drBY["BUSYO_HYOUKA6"] = trableList.Rows[i]["BUSYO_HYOUKA6"];
                        drBY["BUSYO_HYOUKA7"] = trableList.Rows[i]["BUSYO_HYOUKA7"];
                        drBY["BUSYO_HYOUKA8"] = trableList.Rows[i]["BUSYO_HYOUKA8"];
                        drBY["BUSYO_HYOUKA9"] = trableList.Rows[i]["BUSYO_HYOUKA9"];
                        drBY["BUSYO_HYOUKA10"] = trableList.Rows[i]["BUSYO_HYOUKA10"];
                        drBY["SIRYOU_NO1"] = trableList.Rows[i]["SIRYOU_NO1"];
                        drBY["SIRYOU_NO2"] = trableList.Rows[i]["SIRYOU_NO2"];
                        drBY["SIRYOU_NO3"] = trableList.Rows[i]["SIRYOU_NO3"];
                        drBY["SIRYOU_NO4"] = trableList.Rows[i]["SIRYOU_NO4"];
                        drBY["SIRYOU_NO5"] = trableList.Rows[i]["SIRYOU_NO5"];
                        drBY["KANREN_KANRI_NO"] = trableList.Rows[i]["KANREN_KANRI_NO"];
                        drBY["INSERT_YMD"] = trableList.Rows[i]["INSERT_YMD"];
                        drBY["YMD_HENSYU"] = trableList.Rows[i]["YMD_HENSYU"];
                        drBY["INPUT_ROW"] = trableList.Rows[i]["INPUT_ROW"];
                        drBY["SEQ"] = trableList.Rows[i]["SEQ"];
                        tempBY.Rows.Add(drBY);
                    }
                    else if (trableList.Rows[i]["BY_PU"].ToString().Trim() == "PU")
                    {
                        drPU = tempPU.NewRow();
                        drPU["ROWID"] = trableList.Rows[i]["ROWID"];
                        drPU["RANK"] = trableList.Rows[i]["RANK"];
                        drPU["SAIHATU"] = trableList.Rows[i]["SAIHATU"];
                        drPU["RSC"] = trableList.Rows[i]["RSC"];
                        drPU["SYUMU"] = trableList.Rows[i]["SYUMU"];
                        drPU["SYSTEM_NO"] = trableList.Rows[i]["SYSTEM_NO"];
                        drPU["BY_PU"] = trableList.Rows[i]["BY_PU"];
                        drPU["KOUMOKU_KANRI_NO"] = trableList.Rows[i]["KOUMOKU_KANRI_NO"];
                        drPU["KOUMOKU"] = trableList.Rows[i]["KOUMOKU"];
                        drPU["FUGO_NAME1"] = trableList.Rows[i]["FUGO_NAME1"];
                        drPU["FUGO_NAME2"] = trableList.Rows[i]["FUGO_NAME2"];
                        drPU["FUGO_NAME3"] = trableList.Rows[i]["FUGO_NAME3"];
                        drPU["FUGO_NAME4"] = trableList.Rows[i]["FUGO_NAME4"];
                        drPU["FUGO_NAME5"] = trableList.Rows[i]["FUGO_NAME5"];
                        drPU["GENSYO_NAIYO"] = trableList.Rows[i]["GENSYO_NAIYO"];
                        drPU["JYOUKYO"] = trableList.Rows[i]["JYOUKYO"];
                        drPU["GENIN"] = trableList.Rows[i]["GENIN"];
                        drPU["TAISAKU"] = trableList.Rows[i]["TAISAKU"];
                        drPU["KAIHATU_MIHAKKEN_RIYU"] = trableList.Rows[i]["KAIHATU_MIHAKKEN_RIYU"];
                        drPU["SQB_KANTEN"] = trableList.Rows[i]["SQB_KANTEN"];
                        drPU["SAIHATU_SEKKEI"] = trableList.Rows[i]["SAIHATU_SEKKEI"];
                        drPU["SAIHATU_HYOUKA"] = trableList.Rows[i]["SAIHATU_HYOUKA"];
                        drPU["BUSYO_SEKKEI1"] = trableList.Rows[i]["BUSYO_SEKKEI1"];
                        drPU["BUSYO_SEKKEI2"] = trableList.Rows[i]["BUSYO_SEKKEI2"];
                        drPU["BUSYO_SEKKEI3"] = trableList.Rows[i]["BUSYO_SEKKEI3"];
                        drPU["BUSYO_SEKKEI4"] = trableList.Rows[i]["BUSYO_SEKKEI4"];
                        drPU["BUSYO_SEKKEI5"] = trableList.Rows[i]["BUSYO_SEKKEI5"];
                        drPU["BUSYO_SEKKEI6"] = trableList.Rows[i]["BUSYO_SEKKEI6"];
                        drPU["BUSYO_SEKKEI7"] = trableList.Rows[i]["BUSYO_SEKKEI7"];
                        drPU["BUSYO_SEKKEI8"] = trableList.Rows[i]["BUSYO_SEKKEI8"];
                        drPU["BUSYO_SEKKEI9"] = trableList.Rows[i]["BUSYO_SEKKEI9"];
                        drPU["BUSYO_SEKKEI10"] = trableList.Rows[i]["BUSYO_SEKKEI10"];
                        drPU["BUSYO_HYOUKA1"] = trableList.Rows[i]["BUSYO_HYOUKA1"];
                        drPU["BUSYO_HYOUKA2"] = trableList.Rows[i]["BUSYO_HYOUKA2"];
                        drPU["BUSYO_HYOUKA3"] = trableList.Rows[i]["BUSYO_HYOUKA3"];
                        drPU["BUSYO_HYOUKA4"] = trableList.Rows[i]["BUSYO_HYOUKA4"];
                        drPU["BUSYO_HYOUKA5"] = trableList.Rows[i]["BUSYO_HYOUKA5"];
                        drPU["BUSYO_HYOUKA6"] = trableList.Rows[i]["BUSYO_HYOUKA6"];
                        drPU["BUSYO_HYOUKA7"] = trableList.Rows[i]["BUSYO_HYOUKA7"];
                        drPU["BUSYO_HYOUKA8"] = trableList.Rows[i]["BUSYO_HYOUKA8"];
                        drPU["BUSYO_HYOUKA9"] = trableList.Rows[i]["BUSYO_HYOUKA9"];
                        drPU["BUSYO_HYOUKA10"] = trableList.Rows[i]["BUSYO_HYOUKA10"];
                        drPU["SIRYOU_NO1"] = trableList.Rows[i]["SIRYOU_NO1"];
                        drPU["SIRYOU_NO2"] = trableList.Rows[i]["SIRYOU_NO2"];
                        drPU["SIRYOU_NO3"] = trableList.Rows[i]["SIRYOU_NO3"];
                        drPU["SIRYOU_NO4"] = trableList.Rows[i]["SIRYOU_NO4"];
                        drPU["SIRYOU_NO5"] = trableList.Rows[i]["SIRYOU_NO5"];
                        drPU["KANREN_KANRI_NO"] = trableList.Rows[i]["KANREN_KANRI_NO"];
                        drPU["INSERT_YMD"] = trableList.Rows[i]["INSERT_YMD"];
                        drPU["YMD_HENSYU"] = trableList.Rows[i]["YMD_HENSYU"];
                        drPU["INPUT_ROW"] = trableList.Rows[i]["INPUT_ROW"];
                        drPU["SEQ"] = trableList.Rows[i]["SEQ"];
                        tempPU.Rows.Add(drPU);
                    }
                    else
                    {
                        continue;
                    }
                }

                // 並び順変更
                DataRow[] sortBY = tempBY.Select(null, "SEQ ASC ,YMD_HENSYU DESC, INSERT_YMD DESC ");
                for (int i = 0; sortBY.Length > i; i++)
                {
                    DataRow d = excelListBY.NewRow();
                    d["ROWID"] = sortBY[i]["ROWID"];
                    d["RANK"] = sortBY[i]["RANK"];
                    d["SAIHATU"] = sortBY[i]["SAIHATU"];
                    d["RSC"] = sortBY[i]["RSC"];
                    d["SYUMU"] = sortBY[i]["SYUMU"];
                    d["SYSTEM_NO"] = sortBY[i]["SYSTEM_NO"];
                    d["BY_PU"] = sortBY[i]["BY_PU"];
                    d["KOUMOKU_KANRI_NO"] = sortBY[i]["KOUMOKU_KANRI_NO"]; 
                    d["KOUMOKU"] = sortBY[i]["KOUMOKU"];
                    d["FUGO_NAME1"] = sortBY[i]["FUGO_NAME1"];
                    d["FUGO_NAME2"] = sortBY[i]["FUGO_NAME2"];
                    d["FUGO_NAME3"] = sortBY[i]["FUGO_NAME3"];
                    d["FUGO_NAME4"] = sortBY[i]["FUGO_NAME4"];
                    d["FUGO_NAME5"] = sortBY[i]["FUGO_NAME5"];
                    d["GENSYO_NAIYO"] = sortBY[i]["GENSYO_NAIYO"];
                    d["JYOUKYO"] = sortBY[i]["JYOUKYO"];
                    d["GENIN"] = sortBY[i]["GENIN"];
                    d["TAISAKU"] = sortBY[i]["TAISAKU"];
                    d["KAIHATU_MIHAKKEN_RIYU"] = sortBY[i]["KAIHATU_MIHAKKEN_RIYU"];
                    d["SQB_KANTEN"] = sortBY[i]["SQB_KANTEN"];
                    d["SAIHATU_SEKKEI"] = sortBY[i]["SAIHATU_SEKKEI"];
                    d["SAIHATU_HYOUKA"] = sortBY[i]["SAIHATU_HYOUKA"];
                    d["BUSYO_SEKKEI1"] = sortBY[i]["BUSYO_SEKKEI1"];
                    d["BUSYO_SEKKEI2"] = sortBY[i]["BUSYO_SEKKEI2"];
                    d["BUSYO_SEKKEI3"] = sortBY[i]["BUSYO_SEKKEI3"];
                    d["BUSYO_SEKKEI4"] = sortBY[i]["BUSYO_SEKKEI4"];
                    d["BUSYO_SEKKEI5"] = sortBY[i]["BUSYO_SEKKEI5"];
                    d["BUSYO_SEKKEI6"] = sortBY[i]["BUSYO_SEKKEI6"];
                    d["BUSYO_SEKKEI7"] = sortBY[i]["BUSYO_SEKKEI7"];
                    d["BUSYO_SEKKEI8"] = sortBY[i]["BUSYO_SEKKEI8"];
                    d["BUSYO_SEKKEI9"] = sortBY[i]["BUSYO_SEKKEI9"];
                    d["BUSYO_SEKKEI10"] = sortBY[i]["BUSYO_SEKKEI10"];
                    d["BUSYO_HYOUKA1"] = sortBY[i]["BUSYO_HYOUKA1"];
                    d["BUSYO_HYOUKA2"] = sortBY[i]["BUSYO_HYOUKA2"];
                    d["BUSYO_HYOUKA3"] = sortBY[i]["BUSYO_HYOUKA3"];
                    d["BUSYO_HYOUKA4"] = sortBY[i]["BUSYO_HYOUKA4"];
                    d["BUSYO_HYOUKA5"] = sortBY[i]["BUSYO_HYOUKA5"];
                    d["BUSYO_HYOUKA6"] = sortBY[i]["BUSYO_HYOUKA6"];
                    d["BUSYO_HYOUKA7"] = sortBY[i]["BUSYO_HYOUKA7"];
                    d["BUSYO_HYOUKA8"] = sortBY[i]["BUSYO_HYOUKA8"];
                    d["BUSYO_HYOUKA9"] = sortBY[i]["BUSYO_HYOUKA9"];
                    d["BUSYO_HYOUKA10"] = sortBY[i]["BUSYO_HYOUKA10"];
                    d["SIRYOU_NO1"] = sortBY[i]["SIRYOU_NO1"];
                    d["SIRYOU_NO2"] = sortBY[i]["SIRYOU_NO2"];
                    d["SIRYOU_NO3"] = sortBY[i]["SIRYOU_NO3"];
                    d["SIRYOU_NO4"] = sortBY[i]["SIRYOU_NO4"];
                    d["SIRYOU_NO5"] = sortBY[i]["SIRYOU_NO5"];
                    d["KANREN_KANRI_NO"] = sortBY[i]["KANREN_KANRI_NO"];
                    d["INSERT_YMD"] = sortBY[i]["INSERT_YMD"];
                    d["YMD_HENSYU"] = sortBY[i]["YMD_HENSYU"];
                    d["INPUT_ROW"] = sortBY[i]["INPUT_ROW"];
                    d["SEQ"] = sortBY[i]["SEQ"];
                    excelListBY.Rows.Add(d);
                }
                DataRow[] sortPU = tempPU.Select(null, "INPUT_ROW DESC ");
                for (int i = 0; sortPU.Length > i; i++)
                {
                    DataRow d = excelListPU.NewRow();
                    d["ROWID"] = sortPU[i]["ROWID"];
                    d["RANK"] = sortPU[i]["RANK"];
                    d["SAIHATU"] = sortPU[i]["SAIHATU"];
                    d["RSC"] = sortPU[i]["RSC"];
                    d["SYUMU"] = sortPU[i]["SYUMU"];
                    d["SYSTEM_NO"] = sortPU[i]["SYSTEM_NO"];
                    d["BY_PU"] = sortPU[i]["BY_PU"];
                    d["KOUMOKU_KANRI_NO"] = sortPU[i]["KOUMOKU_KANRI_NO"]; 
                    d["KOUMOKU"] = sortPU[i]["KOUMOKU"];
                    d["FUGO_NAME1"] = sortPU[i]["FUGO_NAME1"];
                    d["FUGO_NAME2"] = sortPU[i]["FUGO_NAME2"];
                    d["FUGO_NAME3"] = sortPU[i]["FUGO_NAME3"];
                    d["FUGO_NAME4"] = sortPU[i]["FUGO_NAME4"];
                    d["FUGO_NAME5"] = sortPU[i]["FUGO_NAME5"];
                    d["GENSYO_NAIYO"] = sortPU[i]["GENSYO_NAIYO"];
                    d["JYOUKYO"] = sortPU[i]["JYOUKYO"];
                    d["GENIN"] = sortPU[i]["GENIN"];
                    d["TAISAKU"] = sortPU[i]["TAISAKU"];
                    d["KAIHATU_MIHAKKEN_RIYU"] = sortPU[i]["KAIHATU_MIHAKKEN_RIYU"];
                    d["SQB_KANTEN"] = sortPU[i]["SQB_KANTEN"];
                    d["SAIHATU_SEKKEI"] = sortPU[i]["SAIHATU_SEKKEI"];
                    d["SAIHATU_HYOUKA"] = sortPU[i]["SAIHATU_HYOUKA"];
                    d["BUSYO_SEKKEI1"] = sortPU[i]["BUSYO_SEKKEI1"];
                    d["BUSYO_SEKKEI2"] = sortPU[i]["BUSYO_SEKKEI2"];
                    d["BUSYO_SEKKEI3"] = sortPU[i]["BUSYO_SEKKEI3"];
                    d["BUSYO_SEKKEI4"] = sortPU[i]["BUSYO_SEKKEI4"];
                    d["BUSYO_SEKKEI5"] = sortPU[i]["BUSYO_SEKKEI5"];
                    d["BUSYO_SEKKEI6"] = sortPU[i]["BUSYO_SEKKEI6"];
                    d["BUSYO_SEKKEI7"] = sortPU[i]["BUSYO_SEKKEI7"];
                    d["BUSYO_SEKKEI8"] = sortPU[i]["BUSYO_SEKKEI8"];
                    d["BUSYO_SEKKEI9"] = sortPU[i]["BUSYO_SEKKEI9"];
                    d["BUSYO_SEKKEI10"] = sortPU[i]["BUSYO_SEKKEI10"];
                    d["BUSYO_HYOUKA1"] = sortPU[i]["BUSYO_HYOUKA1"];
                    d["BUSYO_HYOUKA2"] = sortPU[i]["BUSYO_HYOUKA2"];
                    d["BUSYO_HYOUKA3"] = sortPU[i]["BUSYO_HYOUKA3"];
                    d["BUSYO_HYOUKA4"] = sortPU[i]["BUSYO_HYOUKA4"];
                    d["BUSYO_HYOUKA5"] = sortPU[i]["BUSYO_HYOUKA5"];
                    d["BUSYO_HYOUKA6"] = sortPU[i]["BUSYO_HYOUKA6"];
                    d["BUSYO_HYOUKA7"] = sortPU[i]["BUSYO_HYOUKA7"];
                    d["BUSYO_HYOUKA8"] = sortPU[i]["BUSYO_HYOUKA8"];
                    d["BUSYO_HYOUKA9"] = sortPU[i]["BUSYO_HYOUKA9"];
                    d["BUSYO_HYOUKA10"] = sortPU[i]["BUSYO_HYOUKA10"];
                    d["SIRYOU_NO1"] = sortPU[i]["SIRYOU_NO1"];
                    d["SIRYOU_NO2"] = sortPU[i]["SIRYOU_NO2"];
                    d["SIRYOU_NO3"] = sortPU[i]["SIRYOU_NO3"];
                    d["SIRYOU_NO4"] = sortPU[i]["SIRYOU_NO4"];
                    d["SIRYOU_NO5"] = sortPU[i]["SIRYOU_NO5"];
                    d["KANREN_KANRI_NO"] = sortPU[i]["KANREN_KANRI_NO"];
                    d["INSERT_YMD"] = sortPU[i]["INSERT_YMD"];
                    d["YMD_HENSYU"] = sortPU[i]["YMD_HENSYU"];
                    d["INPUT_ROW"] = sortPU[i]["INPUT_ROW"];
                    d["SEQ"] = sortPU[i]["SEQ"];
                    excelListPU.Rows.Add(d);
                }

                DataTable userInfo = bcom.GetUser();
                userSight = userInfo.Rows[0]["BY_PU"].ToString().Trim();

                if (userSight=="BY")
                {
                    AddExcelTroubleList(excelListBY);
                    AddExcelTroubleList(excelListPU);
                }
                else
                {
                    AddExcelTroubleList(excelListPU);
                    AddExcelTroubleList(excelListBY);
                }
                
            
                if (paraType == Def.DefTYPE_WORD || paraType == Def.DefTYPE_TOP10)
                {
                    if ((String)ViewState[Def.DefPARA_CONDITION_FLG] == Def.DefTYPE_AND)
                    {
                        paraTypeName = setTypeName(paraType) + "：（AND）" + paraWord;
                    }
                    else
                    {
                        paraTypeName = setTypeName(paraType) + "：（OR）" + paraWord;
                    }

                }
                //20170304 START k-ohmatsuzawa カテゴリ履歴検索時のEXCEL表示修正
                //paraCategory =  (String)ViewState[Def.DefSERCH_WORD];
                string categoryName = "";
                string oldKey = "";
                if (paraTable.Rows.Count > 0)
                {
                    for (int i = 0; i < paraTable.Rows.Count; i++)
                    {
                        string categoryKey = setTypeName(paraTable.Rows[i]["TableType"].ToString());
                        // カンマ区切りで配列化し、最終要素の値を取得する※名称のみを取り出す
                        string itemName = paraTable.Rows[i]["ItemValue1"].ToString().Split(',').Last();


                        if (i == 0 )
                        {
                            categoryName = "";
                            categoryName = categoryKey + "：" + itemName;
                        }
                        else if (categoryKey != oldKey)
                        {
                            paraCategory.Add(categoryName);
                            categoryName = "";
                            categoryName = categoryKey + "：" + itemName;
                        }
                        else
                        {
                            categoryName = categoryName + " " + itemName;
                        }

                        oldKey = categoryKey;
                    }
                    paraCategory.Add(categoryName);
                }
                else
                {
                    paraCategory.Add((String)ViewState[Def.DefSERCH_WORD]);
                }
                //20170304 END k-ohmatsuzawa
                //20170201 機能改善 END

                // TemplateのFileInfo EXCELテンプレートフルパス
                FileInfo template = new FileInfo(@bcom.GetExcelTemplate("K"));

                // 作成EXCELのFileInfo
                FileInfo newFile = new FileInfo(Def.DefKakotoraExcelName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");

                // EXCEL作成
                using (ExcelPackage excelPkg = new ExcelPackage(newFile, template))
                {
                    ExcelWorksheet worksheet = null;
                    worksheet = excelPkg.Workbook.Worksheets.Where(s => s.Name == Def.DefKakotoraWorksheetName).FirstOrDefault();

                    //処理を記述
                    //20170201 機能改善 START
                    //bLogic.CreateTorableList(worksheet, paraTypeName, excelTroubleList);
                    bLogic.CreateTorableList(worksheet, paraTypeName, excelTroubleList, paraCategory);
                    //20170201 機能改善 END

                    // ダウンロード処理
                    String fn = Def.DefKakotoraExcelName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", String.Format("attachment; filename=" + HttpUtility.UrlDecode(fn)));
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.BinaryWrite(excelPkg.GetAsByteArray());
                    Response.Flush();
                    Response.End();
                }

            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmTroubleList", "btn_Excel_Click", ex, this.Response);
            }
        }

        //20170201 機能改善 START
        /// <summary>
        /// 検索処理（検索ボタン押下時）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 
        protected void btn_Search_Click(Object sender, EventArgs e)
        {
            String[] strArrayData = null;
            String strMoji = "";
            String strWord = "";

            strMoji = txtSearch.Text.Trim();

            if (strMoji.Length > 0)
            {
                ViewState[Def.DefPARA_TYPE] = Def.DefTYPE_WORD;   // 検索タイプ

                // 全角スペースをを半角に置換
                strMoji = strMoji.Replace("　", " ");

                // 文字数チェック
                strArrayData = strMoji.Split(' ');
                int i = 0;
                int iWord = 0;

                for (i = 0; i < strArrayData.Count(); i++)
                {
                    if (strArrayData[i].ToString().Trim() != "")
                    {
                        if (i != 0) { strWord += " "; }
                        strWord += strArrayData[i];
                        iWord++;
                    }
                    if (iWord > 3) break;
                }

                txtSearch.Text = strWord.Trim();

                ViewState[Def.DefPARA_WORD] = strWord.Trim();    // 検索文字列
            }
            else
            {
                ViewState[Def.DefPARA_TYPE] = null;   // 検索タイプ
                ViewState[Def.DefPARA_WORD] = null;   // 検索文字列
            }

            String paraType = (String)ViewState[Def.DefPARA_TYPE];              // 検索タイプ
            String paraWord = (String)ViewState[Def.DefPARA_WORD];              // 文字列検索
            DataTable paraTable = (DataTable)ViewState[Def.DefPARA_TABLE];      // Datatable
            String paraCondition = (String)ViewState[Def.DefPARA_CONDITION_FLG];// And・Or検索条件
                String paraCategoryCondition = (String)ViewState[Def.DefPARA_CATEGORY_CONDITION_FLG];// カテゴリ検索用 And・Or検索条件 // 20170719 Add
            SearchTroubleList(paraType, paraWord, paraTable, paraCondition, paraCategoryCondition); // 20170719 Add
        }

        /// <summary>
        /// And・Or検索条件（ORボタン押下時）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 
        protected void btn_OR_Click(Object sender, EventArgs e)
        {
            CommonLogic bcom = new CommonLogic();
            try
            {
                DataTable userInfo = bcom.GetUser();
                userSight = userInfo.Rows[0]["BY_PU"].ToString().Trim();
                troubleListBY = (DataTable)ViewState[Def.DefPARA_TROUBLELISTBY];
                troubleListPU = (DataTable)ViewState[Def.DefPARA_TROUBLELISTPU];
                txtSearch.BackColor = System.Drawing.ColorTranslator.FromHtml("#99FF99");
                ViewState[Def.DefPARA_CONDITION_FLG] = Def.DefTYPE_OR;
                saveFlg = true;
            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmSearch", "btn_OR_Click", ex, this.Response);
            }
        }

        /// <summary>
        /// And・Or検索条件（ANDボタン押下時）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 
        protected void btn_AND_Click(Object sender, EventArgs e)
        {
            CommonLogic bcom = new CommonLogic();
            try
            {
                DataTable userInfo = bcom.GetUser();
                userSight = userInfo.Rows[0]["BY_PU"].ToString().Trim();
                troubleListBY  = (DataTable)ViewState[Def.DefPARA_TROUBLELISTBY];
                troubleListPU = (DataTable)ViewState[Def.DefPARA_TROUBLELISTPU];
                txtSearch.BackColor = System.Drawing.ColorTranslator.FromHtml("#66FFFF");
                ViewState[Def.DefPARA_CONDITION_FLG] = Def.DefTYPE_AND;
                saveFlg = true;
            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmSearch", "btn_AND_Click", ex, this.Response);
            }
        }
        //20170201 機能改善 END


        #region 固有関数

        #region 画面表示処理
        /// <summary>
        /// 画面表示処理
        /// </summary>
        /// <param name="Mode">モード：1:画面、2:Excel</param>
        /// <param name="Type">種類、カテゴリ検索の場合はnull</param>
        /// <param name="Moji">検索文字、カテゴリ検索の場合はnull</param>
        /// <param name="Table">カテゴリデータテーブル（カテゴリ検索用）</param>
        /// <param name="paraCondition">キーワード検索用 And・Or検索条件  1：And、2：Or</param>
        /// <param name="paraCategoryCondition">カテゴリ検索用 And・Or検索条件  1：And、2：Or</param> // 20170719 Add
        protected void initialDisp(String Type, String Moji, DataTable Table, String paraCondition, 
            String paraCategoryCondition) // 20170719 Add
        {
            BuisinessLogic.BLTroubleList bLogic = new BuisinessLogic.BLTroubleList();
            CommonLogic bCom = new CommonLogic();

            gbTrableData = null;
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("ROWID");
            dtTemp.Columns.Add("SYUMU");
            dtTemp.Columns.Add("RANK");
            dtTemp.Columns.Add("SYSTEM_NO");
            //dtTemp.Columns.Add("FOLLOW_INFO"); KATO/CEC DELETE 2016/09/15
            dtTemp.Columns.Add("BY_PU", typeof(String));　//KATO/CEC ADD 2016/09/15
            dtTemp.Columns.Add("KOUMOKU_KANRI_NO");
            dtTemp.Columns.Add("KOUMOKU");
            dtTemp.Columns.Add("FUGO_NAME");
            dtTemp.Columns.Add("GENSYO_NAIYO");
            dtTemp.Columns.Add("JYOUKYO");
            dtTemp.Columns.Add("GENIN");
            dtTemp.Columns.Add("TAISAKU");
            dtTemp.Columns.Add("KAIHATU_MIHAKKEN_RIYU");
            dtTemp.Columns.Add("SQB_KANTEN");
            dtTemp.Columns.Add("SAIHATU_SEKKEI");
            dtTemp.Columns.Add("SAIHATU_HYOUKA");
            dtTemp.Columns.Add("BUSYO_CODE");
            dtTemp.Columns.Add("SIRYOU_NO");
            dtTemp.Columns.Add("LINK_FOLDER_PATH");
            dtTemp.Columns.Add("INSERT_YMD", typeof(String)); // KATO/CEC ADD 2016/09/15
            dtTemp.Columns.Add("YMD_HENSYU", typeof(String)); // KATO/CEC ADD 2016/09/15
            dtTemp.Columns.Add("INPUT_ROW", typeof(int)); // KATO/CEC ADD 2016/09/15
            dtTemp.Columns.Add("SEQ", typeof(int)); // KATO/CEC ADD 2016/09/15

            String strForder = "";
            String strUrl = "";

            // 過去トラ一覧リスト取得
            //20170201 機能改善 START
            gbTrableData = bLogic.GetToroubleList(Def.DefMODE_DISP, Type, Moji, Table, paraCondition,
                paraCategoryCondition); // 20170719 Add
            //20170201 機能改善 END

            // 20170810 KATO/CEC Excelダウンロード機能廃止に伴い処理削除
            //// レコードがない時、Excelダウンロードボタン非活性
            //if (gbTrableData.Rows.Count == 0)
            //{
            //    btnExcel.Enabled = false;
            //}
            //else
            //{
            //    btnExcel.Enabled = true;
            //    // リンクフォルダ取得
            //    strForder = bCom.GetLinkForder();
            //}
            troubleListBY = dtTemp.Clone();
            troubleListPU = dtTemp.Clone();
            //20170201 機能改善 START
            ViewState[Def.DefPARA_TROUBLELISTBY] = troubleListBY;
            ViewState[Def.DefPARA_TROUBLELISTPU] = troubleListPU;
            //20170201 機能改善 END
            DataTable tempBY = dtTemp.Clone();
            DataTable tempPU = dtTemp.Clone();
            DataRow drBY = null, drPU = null;

            // 参照権限のないフォルダはリンク不可
            for (int i = 0; gbTrableData.Rows.Count > i; i++)
            {


                // 過去トラ情報毎リンクフォルダパスの取得
                if (gbTrableData.Rows[i]["LINK_FOLDER_PATH"].ToString().Trim() != "")
                {
                    if (bCom.CheckFolder(strForder + gbTrableData.Rows[i]["LINK_FOLDER_PATH"].ToString().Trim()) == 0)
                    {
                        strUrl = strForder + gbTrableData.Rows[i]["LINK_FOLDER_PATH"].ToString();
                        gbTrableData.Rows[i]["LINK_FOLDER_PATH"] = "file:" + strUrl.ToString().Trim().Replace("\\", "/");
                    }
                    else
                    {
                        gbTrableData.Rows[i]["LINK_FOLDER_PATH"] = "";
                    }
                }
                //過去トラ検索結果一覧の拠点分け
                if (gbTrableData.Rows[i]["BY_PU"].ToString().Trim() == "BY")
                {
                    drBY = tempBY.NewRow();
                    drBY["ROWID"] = gbTrableData.Rows[i]["ROWID"];
                    drBY["SYUMU"] = gbTrableData.Rows[i]["SYUMU"];
                    drBY["RANK"] = gbTrableData.Rows[i]["RANK"];
                    drBY["SYSTEM_NO"] = gbTrableData.Rows[i]["SYSTEM_NO"];
                    drBY["BY_PU"] = gbTrableData.Rows[i]["BY_PU"]; 
                    drBY["KOUMOKU_KANRI_NO"] = gbTrableData.Rows[i]["KOUMOKU_KANRI_NO"];
                    drBY["KOUMOKU"] = gbTrableData.Rows[i]["KOUMOKU"];
                    drBY["FUGO_NAME"] = gbTrableData.Rows[i]["FUGO_NAME"];
                    drBY["GENSYO_NAIYO"] = gbTrableData.Rows[i]["GENSYO_NAIYO"];
                    drBY["JYOUKYO"] = gbTrableData.Rows[i]["JYOUKYO"];
                    drBY["GENIN"] = gbTrableData.Rows[i]["GENIN"];
                    drBY["TAISAKU"] = gbTrableData.Rows[i]["TAISAKU"];
                    drBY["KAIHATU_MIHAKKEN_RIYU"] = gbTrableData.Rows[i]["KAIHATU_MIHAKKEN_RIYU"];
                    drBY["SQB_KANTEN"] = gbTrableData.Rows[i]["SQB_KANTEN"];
                    drBY["SAIHATU_SEKKEI"] = gbTrableData.Rows[i]["SAIHATU_SEKKEI"];
                    drBY["SAIHATU_HYOUKA"] = gbTrableData.Rows[i]["SAIHATU_HYOUKA"];
                    drBY["BUSYO_CODE"] = gbTrableData.Rows[i]["BUSYO_CODE"];
                    drBY["SIRYOU_NO"] = gbTrableData.Rows[i]["SIRYOU_NO"];
                    drBY["LINK_FOLDER_PATH"] = gbTrableData.Rows[i]["LINK_FOLDER_PATH"];
                    drBY["INSERT_YMD"] = gbTrableData.Rows[i]["INSERT_YMD"]; // KATO/CEC ADD 2016/09/15
                    drBY["YMD_HENSYU"] = gbTrableData.Rows[i]["YMD_HENSYU"]; // KATO/CEC ADD 2016/09/15
                    drBY["INPUT_ROW"] = gbTrableData.Rows[i]["INPUT_ROW"];
                    drBY["SEQ"] = gbTrableData.Rows[i]["SEQ"];
                    tempBY.Rows.Add(drBY);
                }
                else if (gbTrableData.Rows[i]["BY_PU"].ToString().Trim() == "PU")
                {
                    drPU = tempPU.NewRow();
                    drPU["ROWID"] = gbTrableData.Rows[i]["ROWID"];
                    drPU["SYUMU"] = gbTrableData.Rows[i]["SYUMU"];
                    drPU["RANK"] = gbTrableData.Rows[i]["RANK"];
                    drPU["SYSTEM_NO"] = gbTrableData.Rows[i]["SYSTEM_NO"];
                    drPU["BY_PU"] = gbTrableData.Rows[i]["BY_PU"];
                    drPU["KOUMOKU_KANRI_NO"] = gbTrableData.Rows[i]["KOUMOKU_KANRI_NO"];
                    drPU["KOUMOKU"] = gbTrableData.Rows[i]["KOUMOKU"];
                    drPU["FUGO_NAME"] = gbTrableData.Rows[i]["FUGO_NAME"];
                    drPU["GENSYO_NAIYO"] = gbTrableData.Rows[i]["GENSYO_NAIYO"];
                    drPU["JYOUKYO"] = gbTrableData.Rows[i]["JYOUKYO"];
                    drPU["GENIN"] = gbTrableData.Rows[i]["GENIN"];
                    drPU["TAISAKU"] = gbTrableData.Rows[i]["TAISAKU"];
                    drPU["KAIHATU_MIHAKKEN_RIYU"] = gbTrableData.Rows[i]["KAIHATU_MIHAKKEN_RIYU"];
                    drPU["SQB_KANTEN"] = gbTrableData.Rows[i]["SQB_KANTEN"];
                    drPU["SAIHATU_SEKKEI"] = gbTrableData.Rows[i]["SAIHATU_SEKKEI"];
                    drPU["SAIHATU_HYOUKA"] = gbTrableData.Rows[i]["SAIHATU_HYOUKA"];
                    drPU["BUSYO_CODE"] = gbTrableData.Rows[i]["BUSYO_CODE"];
                    drPU["SIRYOU_NO"] = gbTrableData.Rows[i]["SIRYOU_NO"];
                    drPU["LINK_FOLDER_PATH"] = gbTrableData.Rows[i]["LINK_FOLDER_PATH"];
                    drPU["INSERT_YMD"] = gbTrableData.Rows[i]["INSERT_YMD"];
                    drPU["YMD_HENSYU"] = gbTrableData.Rows[i]["YMD_HENSYU"];
                    drPU["INPUT_ROW"] = gbTrableData.Rows[i]["INPUT_ROW"];
                    drPU["SEQ"] = gbTrableData.Rows[i]["SEQ"];
                    tempPU.Rows.Add(drPU);
                }
                else
                {
                    continue;
                }
            }

            // 並び順変更
            DataRow[] sortBY = tempBY.Select(null, "SEQ ASC ,YMD_HENSYU DESC ,INSERT_YMD DESC ");
            for(int i = 0; sortBY.Length > i; i++ )
            {
                DataRow d = troubleListBY.NewRow();
                d["ROWID"] = sortBY[i]["ROWID"];
                d["SYUMU"] = sortBY[i]["SYUMU"];
                d["RANK"] = sortBY[i]["RANK"];
                d["SYSTEM_NO"] = sortBY[i]["SYSTEM_NO"];
                d["BY_PU"] = sortBY[i]["BY_PU"]; 
                d["KOUMOKU_KANRI_NO"] = sortBY[i]["KOUMOKU_KANRI_NO"];
                d["KOUMOKU"] = sortBY[i]["KOUMOKU"];
                d["FUGO_NAME"] = sortBY[i]["FUGO_NAME"];
                d["GENSYO_NAIYO"] = sortBY[i]["GENSYO_NAIYO"];
                d["JYOUKYO"] = sortBY[i]["JYOUKYO"];
                d["GENIN"] = sortBY[i]["GENIN"];
                d["TAISAKU"] = sortBY[i]["TAISAKU"];
                d["KAIHATU_MIHAKKEN_RIYU"] = sortBY[i]["KAIHATU_MIHAKKEN_RIYU"];
                d["SQB_KANTEN"] = sortBY[i]["SQB_KANTEN"];
                d["SAIHATU_SEKKEI"] = sortBY[i]["SAIHATU_SEKKEI"];
                d["SAIHATU_HYOUKA"] = sortBY[i]["SAIHATU_HYOUKA"];
                d["BUSYO_CODE"] = sortBY[i]["BUSYO_CODE"];
                d["SIRYOU_NO"] = sortBY[i]["SIRYOU_NO"];
                d["LINK_FOLDER_PATH"] = sortBY[i]["LINK_FOLDER_PATH"];
                d["INSERT_YMD"] = sortBY[i]["INSERT_YMD"];
                d["YMD_HENSYU"] = sortBY[i]["YMD_HENSYU"];
                d["INPUT_ROW"] = sortBY[i]["INPUT_ROW"];
                d["SEQ"] = sortBY[i]["SEQ"];
                troubleListBY.Rows.Add(d);
            }
            DataRow[] sortPU = tempPU.Select(null, "INPUT_ROW DESC ");
            for (int i = 0; sortPU.Length > i; i++)
            {
                DataRow d = troubleListPU.NewRow();
                d["ROWID"] = sortPU[i]["ROWID"];
                d["SYUMU"] = sortPU[i]["SYUMU"];
                d["RANK"] = sortPU[i]["RANK"];
                d["SYSTEM_NO"] = sortPU[i]["SYSTEM_NO"];
                //d["FOLLOW_INFO"] = sortPU[i]["FOLLOW_INFO"]; KATO/CEC DELETE 2016/09/15
                d["BY_PU"] = sortPU[i]["BY_PU"]; // KATO/CEC ADD 2016/09/15
                d["KOUMOKU_KANRI_NO"] = sortPU[i]["KOUMOKU_KANRI_NO"];
                d["KOUMOKU"] = sortPU[i]["KOUMOKU"];
                d["FUGO_NAME"] = sortPU[i]["FUGO_NAME"];
                d["GENSYO_NAIYO"] = sortPU[i]["GENSYO_NAIYO"];
                d["JYOUKYO"] = sortPU[i]["JYOUKYO"];
                d["GENIN"] = sortPU[i]["GENIN"];
                d["TAISAKU"] = sortPU[i]["TAISAKU"];
                d["KAIHATU_MIHAKKEN_RIYU"] = sortPU[i]["KAIHATU_MIHAKKEN_RIYU"];
                d["SQB_KANTEN"] = sortPU[i]["SQB_KANTEN"];
                d["SAIHATU_SEKKEI"] = sortPU[i]["SAIHATU_SEKKEI"];
                d["SAIHATU_HYOUKA"] = sortPU[i]["SAIHATU_HYOUKA"];
                d["BUSYO_CODE"] = sortPU[i]["BUSYO_CODE"];
                d["SIRYOU_NO"] = sortPU[i]["SIRYOU_NO"];
                d["LINK_FOLDER_PATH"] = sortPU[i]["LINK_FOLDER_PATH"];
                d["INSERT_YMD"] = sortPU[i]["INSERT_YMD"];
                d["YMD_HENSYU"] = sortPU[i]["YMD_HENSYU"];
                d["INPUT_ROW"] = sortPU[i]["INPUT_ROW"];
                d["SEQ"] = sortPU[i]["SEQ"];
                troubleListPU.Rows.Add(d);
            }

            //20170201 機能改善 START
            DataTable dtKeyword = null;

            // AutoComplete のキーワードリスト取得
            dtKeyword = bLogic.GetKeyWordList();

            string jisyoInfo = "";

            for (int i = 0; i < dtKeyword.Rows.Count; i++)
            {
                if (i > 0) jisyoInfo += ",\n";

                jisyoInfo += "{ label: '" + dtKeyword.Rows[i]["WORD_KEY"].ToString().Trim() + "　" + dtKeyword.Rows[i]["WORD_CHAR"].ToString().Trim() + "', value: '" + dtKeyword.Rows[i]["WORD_KEY"].ToString().Trim() + "'}";
            }

            ViewState["JisyoInfo"] = jisyoInfo;
            //20170201 機能改善 END
        }

        #endregion

        #region 検索タイプ名取得
        /// <summary>
        /// 検索タイプ名取得
        /// </summary>
        /// <param name="Type">検索タイプ</param>
        protected String setTypeName(String Type)
        {
            String typeName = "";

            switch (Type)
            {
                // 20170721 Add Start
                case Def.DefTYPE_CATEGORY_AND:
                    // カテゴリAND検索
                    typeName = "(AND)";
                    break;
                case Def.DefTYPE_CATEGORY_OR:
                    // カテゴリOR検索
                    typeName = "(OR)";
                    break;
                // 20170721 Add End
                case Def.DefTYPE_WORD:
                    // 文字列検索
                    typeName = "文字列検索";
                    break;
                case Def.DefTYPE_TOP10:
                    // ＴＯＰ１０
                    typeName = "ＴＯＰ１０";
                    break;
                case Def.DefTYPE_BUSYO:
                case Def.DefTYPE_HYOUKA: // 20170724 Add
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

        private void AddExcelTroubleList(DataTable dt)
        {
                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    DataRow d = excelTroubleList.NewRow();
                    d["ROWID"] = excelTroubleList.Rows.Count + 1;
                    d["RANK"] = dt.Rows[i]["RANK"];
                    d["SAIHATU"] = dt.Rows[i]["SAIHATU"];
                    d["RSC"] = dt.Rows[i]["RSC"];
                    d["SYUMU"] = dt.Rows[i]["SYUMU"];
                    d["SYSTEM_NO"] = dt.Rows[i]["SYSTEM_NO"];
                    d["BY_PU"] = dt.Rows[i]["BY_PU"];
                    d["KOUMOKU_KANRI_NO"] = dt.Rows[i]["KOUMOKU_KANRI_NO"];
                    d["KOUMOKU"] = dt.Rows[i]["KOUMOKU"];
                    d["FUGO_NAME1"] = dt.Rows[i]["FUGO_NAME1"];
                    d["FUGO_NAME2"] = dt.Rows[i]["FUGO_NAME2"];
                    d["FUGO_NAME3"] = dt.Rows[i]["FUGO_NAME3"];
                    d["FUGO_NAME4"] = dt.Rows[i]["FUGO_NAME4"];
                    d["FUGO_NAME5"] = dt.Rows[i]["FUGO_NAME5"];
                    d["GENSYO_NAIYO"] = dt.Rows[i]["GENSYO_NAIYO"];
                    d["JYOUKYO"] = dt.Rows[i]["JYOUKYO"];
                    d["GENIN"] = dt.Rows[i]["GENIN"];
                    d["TAISAKU"] = dt.Rows[i]["TAISAKU"];
                    d["KAIHATU_MIHAKKEN_RIYU"] = dt.Rows[i]["KAIHATU_MIHAKKEN_RIYU"];
                    d["SQB_KANTEN"] = dt.Rows[i]["SQB_KANTEN"];
                    d["SAIHATU_SEKKEI"] = dt.Rows[i]["SAIHATU_SEKKEI"];
                    d["SAIHATU_HYOUKA"] = dt.Rows[i]["SAIHATU_HYOUKA"];
                    d["BUSYO_SEKKEI1"] = dt.Rows[i]["BUSYO_SEKKEI1"];
                    d["BUSYO_SEKKEI2"] = dt.Rows[i]["BUSYO_SEKKEI2"];
                    d["BUSYO_SEKKEI3"] = dt.Rows[i]["BUSYO_SEKKEI3"];
                    d["BUSYO_SEKKEI4"] = dt.Rows[i]["BUSYO_SEKKEI4"];
                    d["BUSYO_SEKKEI5"] = dt.Rows[i]["BUSYO_SEKKEI5"];
                    d["BUSYO_SEKKEI6"] = dt.Rows[i]["BUSYO_SEKKEI6"];
                    d["BUSYO_SEKKEI7"] = dt.Rows[i]["BUSYO_SEKKEI7"];
                    d["BUSYO_SEKKEI8"] = dt.Rows[i]["BUSYO_SEKKEI8"];
                    d["BUSYO_SEKKEI9"] = dt.Rows[i]["BUSYO_SEKKEI9"];
                    d["BUSYO_SEKKEI10"] = dt.Rows[i]["BUSYO_SEKKEI10"];
                    d["BUSYO_HYOUKA1"] = dt.Rows[i]["BUSYO_HYOUKA1"];
                    d["BUSYO_HYOUKA2"] = dt.Rows[i]["BUSYO_HYOUKA2"];
                    d["BUSYO_HYOUKA3"] = dt.Rows[i]["BUSYO_HYOUKA3"];
                    d["BUSYO_HYOUKA4"] = dt.Rows[i]["BUSYO_HYOUKA4"];
                    d["BUSYO_HYOUKA5"] = dt.Rows[i]["BUSYO_HYOUKA5"];
                    d["BUSYO_HYOUKA6"] = dt.Rows[i]["BUSYO_HYOUKA6"];
                    d["BUSYO_HYOUKA7"] = dt.Rows[i]["BUSYO_HYOUKA7"];
                    d["BUSYO_HYOUKA8"] = dt.Rows[i]["BUSYO_HYOUKA8"];
                    d["BUSYO_HYOUKA9"] = dt.Rows[i]["BUSYO_HYOUKA9"];
                    d["BUSYO_HYOUKA10"] = dt.Rows[i]["BUSYO_HYOUKA10"];
                    d["SIRYOU_NO1"] = dt.Rows[i]["SIRYOU_NO1"];
                    d["SIRYOU_NO2"] = dt.Rows[i]["SIRYOU_NO2"];
                    d["SIRYOU_NO3"] = dt.Rows[i]["SIRYOU_NO3"];
                    d["SIRYOU_NO4"] = dt.Rows[i]["SIRYOU_NO4"];
                    d["SIRYOU_NO5"] = dt.Rows[i]["SIRYOU_NO5"];
                    d["KANREN_KANRI_NO"] = dt.Rows[i]["KANREN_KANRI_NO"];
                    d["INSERT_YMD"] = dt.Rows[i]["INSERT_YMD"];
                    d["YMD_HENSYU"] = dt.Rows[i]["YMD_HENSYU"];
                    d["INPUT_ROW"] = dt.Rows[i]["INPUT_ROW"];
                    d["SEQ"] = dt.Rows[i]["SEQ"];
                excelTroubleList.Rows.Add(d);
                }
        }

        #endregion

        //20170201 機能改善 START
        #region 過去トラテーブル検索
        private void SearchTroubleList(String paraType, String paraWord, DataTable paraTable, String paraCondition, String paraCategoryCondition) // 20170719 Add
        {
            CommonLogic bcom = new CommonLogic();

            try
            {
                Boolean result = false;
                DateTime date_time = DateTime.Now;

                ClientScriptManager csManager = Page.ClientScript;
                Type csType = this.GetType();
                ArrayList arrayMessage = new ArrayList();

                // 検索タイプ
                if ((paraType == "" || paraType == null) && paraTable.Rows.Count == 0)
                {
                    //btnExcel.Enabled = false;

                    arrayMessage.Add(Def.DefMsg_URLERR);
                    bcom.ShowMessage(csType, csManager, arrayMessage);
                    return;
                }

                // Windowsログイン・ユーザマスタチェック
                result = bcom.CheckUser();
                if (result)
                {
                    //btnExcel.Visible = false;

                    arrayMessage.Add(Def.DefMsg_USERERR);
                    bcom.ShowMessage(csType, csManager, arrayMessage);
                    return;
                }
                else
                {
                    DataTable userInfo = bcom.GetUser();
                    userSight = userInfo.Rows[0]["BY_PU"].ToString().Trim();
                }

                const string strKuten = "、";
                ViewState["HightLightWord"] = String.Empty;
                if (paraType == Def.DefTYPE_WORD || paraType == Def.DefTYPE_TOP10)
                {
                    // 検索ログ登録
                    result = bcom.RegistLogData(paraType, paraWord, 0, date_time);
                    // 検索タイプ名
                    lblType.Text = setTypeName(paraType);
                    // 文字列
                    txtSearch.Text = paraWord;
                    //ハイライト文字
                    ViewState["HightLightWord"] = paraWord + " ";
                }

                //データテーブルを、検索タイプ毎に分離
                String strLog = null;
                String strBusyo = null;
                String strHyouka = null;
                String strPartsS = null;
                String strPartsN = null;
                String strKaihatu = null;
                String strGensyo = null;
                String strGenin = null;
                String strSyakata = null;
                String strSgensyo = null;
                String strSyouin = null;
                String strEgtm = null;
                DataRow[] drSelectType = null;
                lblCategory.Text = "";

                // 20170721 Add Start
                // カテゴリAND検索
                if (paraCategoryCondition == Def.DefTYPE_AND)
                {
                    // 検索タイプ名
                    lblCategory.Text = setTypeName(Def.DefTYPE_CATEGORY_AND);
                }
                // カテゴリOR検索
                if (paraCategoryCondition == Def.DefTYPE_OR)
                {
                    // 検索タイプ名
                    lblCategory.Text = setTypeName(Def.DefTYPE_CATEGORY_OR);
                }
                // 20170721 Add End

                //設計部署
                drSelectType = setSelectType(paraTable, Def.DefTYPE_BUSYO);
                if (drSelectType.Length > 0)
                {
                    strLog = setAddValue(drSelectType, 0);
                    strBusyo = setAddValue(drSelectType, 1);
                }
                //評価部署
                drSelectType = setSelectType(paraTable, Def.DefTYPE_HYOUKA);
                if (drSelectType.Length > 0)
                {
                    if (strLog != null)
                    {
                        strLog += ",";
                    }
                    strLog += setAddValue(drSelectType, 0);
                    strHyouka = setAddValue(drSelectType, 1);
                }
                if (strLog != null)
                {
                    // 検索ログ登録
                    result = bcom.RegistLogData(Def.DefTYPE_BUSYO, strLog, 0, date_time);
                    // 検索タイプ名
                    lblCategory.Text += setTypeName(Def.DefTYPE_BUSYO) + strKuten;
                    //ハイライト文字
                    ViewState["HightLight"] = strLog.Replace(",", " ") + " ";
                }
                //部品・部品部位
                drSelectType = setSelectType(paraTable, Def.DefTYPE_PARTS);
                if (drSelectType.Length > 0)
                {
                    strLog = setAddValue(drSelectType, 0);
                    // 検索ログ登録
                    result = bcom.RegistLogData(Def.DefTYPE_PARTS, strLog, 0, date_time);
                    // 検索タイプ名
                    lblCategory.Text += setTypeName(Def.DefTYPE_PARTS) + strKuten;
                }
                String[] strPartsArrayData = null;
                String[] strPartsArrayName1 = null;
                String[] strPartsArrayName2 = null;
                String strPartsName1 = null;
                String strPartsName2 = null;


                String strWorkPartsS = null;
                for (int i = 0; drSelectType.Length > i; i++)
                {
                    strPartsArrayData = drSelectType[i]["ItemValue1"].ToString().Trim().Split(',');

                    if (strWorkPartsS != strPartsArrayData[0].ToString())
                    {
                        if (strPartsS != null)
                        {
                            strPartsS += ",";
                        }
                        strPartsS += strPartsArrayData[0].ToString();
                        strWorkPartsS = strPartsArrayData[0].ToString();
                    }

                    if (!(strPartsArrayData[1].ToString() == ""))
                    {
                        if (strPartsN != null)
                        {
                            strPartsN += ",";
                        }
                        strPartsN += strPartsArrayData[0].ToString();
                        strPartsN += strPartsArrayData[1].ToString();
                    }
                    if (!(strPartsArrayData[2].ToString() == ""))
                    {
                        strPartsN += strPartsArrayData[2].ToString();
                    }
                    //ハイライト文字
                    strPartsArrayName1 = strPartsArrayData[3].ToString().Trim().Split('／');
                    if (strPartsName1 != strPartsArrayName1[0].ToString())
                    {
                        ViewState["HightLight"] += strPartsArrayName1[0].ToString() + " ";
                        strPartsName1 = strPartsArrayName1[0].ToString();
                    }

                    if (strPartsArrayName1.Length  > 1)
                    {
                        strPartsArrayName2 = strPartsArrayName1[1].ToString().Trim().Split('｜');
                        if (strPartsName2 != strPartsArrayName2[0].ToString())
                        {
                            ViewState["HightLight"] += strPartsArrayName2[0].ToString() + " ";
                            strPartsName2 = strPartsArrayName2[0].ToString();
                        }
                        if (strPartsArrayName2.Length > 1)
                        {
                            ViewState["HightLight"] += strPartsArrayName2[1].ToString() + " ";
                        }
                    }
                }
                //開発符号
                drSelectType = setSelectType(paraTable, Def.DefTYPE_KAIHATU);
                if (drSelectType.Length > 0)
                {
                    strLog = setAddValue(drSelectType, 0);
                    strKaihatu = setAddValue(drSelectType, 1);
                    // 検索ログ登録
                    result = bcom.RegistLogData(Def.DefTYPE_KAIHATU, strLog, 0, date_time);
                    // 検索タイプ名
                    lblCategory.Text += setTypeName(Def.DefTYPE_KAIHATU) + strKuten;
                    //ハイライト文字
                    ViewState["HightLight"] += setAddValue(drSelectType, 2) + " ";
                }
                //現象（分類）
                drSelectType = setSelectType(paraTable, Def.DefTYPE_GENSYO);
                if (drSelectType.Length > 0)
                {
                    strLog = setAddValue(drSelectType, 0);
                    strGensyo = setAddValue(drSelectType, 1);
                    // 検索ログ登録
                    result = bcom.RegistLogData(Def.DefTYPE_GENSYO, strLog, 0, date_time);
                    // 検索タイプ名
                    lblCategory.Text += setTypeName(Def.DefTYPE_GENSYO) + strKuten;
                    //ハイライト文字
                    ViewState["HightLight"] += setAddValue(drSelectType, 2) + " ";
                }
                //原因（分類）
                drSelectType = setSelectType(paraTable, Def.DefTYPE_GENIN);
                if (drSelectType.Length > 0)
                {
                    strLog = setAddValue(drSelectType, 0);
                    strGenin = setAddValue(drSelectType, 1);
                    // 検索ログ登録
                    result = bcom.RegistLogData(Def.DefTYPE_GENIN, strLog, 0, date_time);
                    // 検索タイプ名
                    lblCategory.Text += setTypeName(Def.DefTYPE_GENIN) + strKuten;
                    //ハイライト文字
                    ViewState["HightLight"] += setAddValue(drSelectType, 2) + " ";
                }
                //車型特殊
                drSelectType = setSelectType(paraTable, Def.DefTYPE_SYAKATA);
                if (drSelectType.Length > 0)
                {
                    strLog = setAddValue(drSelectType, 0);
                    strSyakata = setAddValue(drSelectType, 1);
                    // 検索ログ登録
                    result = bcom.RegistLogData(Def.DefTYPE_SYAKATA, strLog, 0, date_time);
                    // 検索タイプ名
                    lblCategory.Text += setTypeName(Def.DefTYPE_SYAKATA) + strKuten;
                    //ハイライト文字
                    ViewState["HightLight"] += setAddValue(drSelectType, 2) + " ";
                }
                //現象（制御系）
                drSelectType = setSelectType(paraTable, Def.DefTYPE_SGENSYO);
                if (drSelectType.Length > 0)
                {
                    strLog = setAddValue(drSelectType, 0);
                    strSgensyo = setAddValue(drSelectType, 1);
                    // 検索ログ登録
                    result = bcom.RegistLogData(Def.DefTYPE_SGENSYO, strLog, 0, date_time);
                    // 検索タイプ名
                    lblCategory.Text += setTypeName(Def.DefTYPE_SGENSYO) + strKuten;
                    //ハイライト文字
                    ViewState["HightLight"] += setAddValue(drSelectType, 2) + " ";
                }
                //要因（制御系）
                drSelectType = setSelectType(paraTable, Def.DefTYPE_SYOUIN);
                if (drSelectType.Length > 0)
                {
                    strLog = setAddValue(drSelectType, 0);
                    strSyouin = setAddValue(drSelectType, 1);
                    // 検索ログ登録
                    result = bcom.RegistLogData(Def.DefTYPE_SYOUIN, strLog, 0, date_time);
                    // 検索タイプ名
                    lblCategory.Text += setTypeName(Def.DefTYPE_SYOUIN) + strKuten;
                    //ハイライト文字
                    ViewState["HightLight"] += setAddValue(drSelectType, 2) + " ";
                }
                //EGTM形式
                drSelectType = setSelectType(paraTable, Def.DefTYPE_EGTM);
                if (drSelectType.Length > 0)
                {
                    strLog = setAddValue(drSelectType, 0);
                    strEgtm = setAddValue(drSelectType, 1);
                    // 検索ログ登録
                    result = bcom.RegistLogData(Def.DefTYPE_EGTM, strLog, 0, date_time);
                    // 検索タイプ名
                    lblCategory.Text += setTypeName(Def.DefTYPE_EGTM) + strKuten;
                    //ハイライト文字
                    ViewState["HightLight"] += setAddValue(drSelectType, 2) + " ";
                }

                // 語尾句点削除（画面カテゴリラベル）
                if (lblCategory.Text.EndsWith(strKuten))
                {
                    lblCategory.Text = lblCategory.Text.Remove(lblCategory.Text.Length - 1);
                }

                ViewState[Def.DefSERCH_WORD] = lblCategory.Text;   // 検索条件退避・EXCEL用

                // 20170721 Add Start ツールチップ設定
                List<string> paraCategory = new List<string>();
                string categoryName = "";
                string oldKey = "";
                if (paraTable.Rows.Count > 0)
                {
                    for (int i = 0; i < paraTable.Rows.Count; i++)
                    {
                        string categoryKey = setTypeName(paraTable.Rows[i]["TableType"].ToString());
                        // カンマ区切りで配列化し、最終要素の値を取得する※名称のみを取り出す
                        string itemName = paraTable.Rows[i]["ItemValue1"].ToString().Split(',').Last();


                        if (i == 0)
                        {
                            categoryName = "";
                            categoryName = categoryKey + "：" + itemName;
                        }
                        else if (categoryKey != oldKey)
                        {
                            paraCategory.Add(categoryName);
                            categoryName = "";
                            categoryName = categoryKey + "：" + itemName;
                        }
                        else
                        {
                            categoryName = categoryName + " " + itemName;
                        }

                        oldKey = categoryKey;
                    }
                    paraCategory.Add(categoryName);

                    // カテゴリにツールチップ設定
                    for (int j = 0; j < paraCategory.Count; j++)
                    {
                        if (j == 0)
                        {
                            lblCategory.ToolTip = paraCategory[j];
                        }
                        if (j > 0)
                        {
                            lblCategory.ToolTip += Environment.NewLine + paraCategory[j]; // 改行コード + 検索条件
                        }
                    }
                }
                // 20170721 Add End

                // 検索履歴登録
                if (paraType == Def.DefTYPE_WORD | paraType == Def.DefTYPE_TOP10)
                { 
                    result = bcom.RegistHistoryLogData(paraCondition, paraType, paraWord, null, null, null, null, null, null, null, null, null, null, null);
                }
                // 過去トラテーブル検索
                initialDisp(paraType, paraWord, paraTable, paraCondition, paraCategoryCondition); // 20170719 Add
            }
            // システムエラー処理（ログ出力、エラー画面遷移）
            catch (Exception ex)
            {
                // システムエラー処理（ログ出力＆エラーページ表示）
                bcom.ErrorProcess("frmTroubleList", "Page_Load", ex, this.Response);
            }
        }
        #endregion

        #region 検索タイプ別抽出
        /// <summary>
        /// 検索タイプ別抽出
        /// </summary>
        /// <param name="dtAllType">全種類</param>
        /// <param name="strType">検索タイプ</param>
        /// <returns>検索タイプ別抽出データ</returns>
        private DataRow[] setSelectType(DataTable dtAllType,  string strType)
        {
            DataRow[] drSelectType = (
                from row in dtAllType.AsEnumerable()
                let columnID = row.Field<string>("TableType")
                where columnID == strType
                select row
            ).ToArray();
            return drSelectType;
        }
        #endregion

        #region 検索ログ登録、ハイライト用文字列取得
        /// <summary>
        /// 検索ログ登録、ハイライト用文字列取得
        /// </summary>
        /// <param name="drSelectType">検索タイプ別抽出</param>
        /// <param name="intEditType">編集タイプ</param>
        /// <returns>結合後検索ログ登録用文字列</returns>
        private String setAddValue(DataRow[] drSelectType, int intEditType)
        {
            String strAddValue = "";
            for (int i = 0; drSelectType.Length > i; i++)
            {
                if (i != 0)
                {
                    if (intEditType == 0 || intEditType == 1)
                    {
                        strAddValue += ",";
                    }
                    else if (intEditType == 2)
                    {
                        strAddValue += " ";
                    }
                }

                if (intEditType == 0)
                {
                    strAddValue += drSelectType[i]["ItemValue1"].ToString().Trim();
                }
                else
                {
                    String[] strArrayData = drSelectType[i]["ItemValue1"].ToString().Trim().Split(',');
                    if(intEditType == 1)
                    {
                        strAddValue += strArrayData[0].ToString().Trim();
                    }
                    else if(intEditType == 2)
                    {
                        strAddValue += strArrayData[1].ToString().Trim();
                    }
                }
            }
            return strAddValue;
        }
        #endregion

        #endregion
    }
}