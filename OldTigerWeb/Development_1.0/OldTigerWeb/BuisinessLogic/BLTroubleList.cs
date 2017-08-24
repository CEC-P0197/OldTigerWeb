using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace OldTigerWeb.BuisinessLogic
{
    public class BLTroubleList
    {
        /// <summary>
        /// 過去トラ情報取得
        /// </summary>
        /// <param name="Mode">モード：1:画面、2:Excel</param>
        /// <param name="Type">種類　カテゴリ検索の場合はnull</param>
        /// <param name="Moji">検索文字　カテゴリ検索の場合はnull</param>
        /// <param name="Table">カテゴリデータテーブル（カテゴリ検索用）</param>
        /// <param name="paraCondition">キーワード検索用 And・Or検索条件  1：And、2：Or</param>
        /// <param name="paraCategoryCondition">カテゴリ検索用 And・Or検索条件  1：And、2：Or</param> // 20170719 Add
        /// <returns>結果データテーブル</returns>
        /// <remarks></remarks>
        //20170201 機能改善 START
        //public DataTable GetToroubleList(String Mode, String Type, String Moji, ArrayList paraArrWord1, ArrayList paraArrWord2)
        // public DataTable GetToroubleList(String Mode, string Type, String Moji, DataTable Table, String paraCondition)
        //20170201 機能改善 END
        public DataTable GetToroubleList(String Mode, string Type, String Moji, DataTable Table, String paraCondition, 
            String paraCategoryCondition) // 20170719 Add
        {
            DataTable result = null;

            // データアクセス作成
            DataAccess.DATroubleList dac = new DataAccess.DATroubleList();

            // ＳＱＬ実行
            //20170201 機能改善 START
            //result = dac.SelectTroubleList(Mode, Type, Moji, paraArrWord1, paraArrWord2);
            // result = dac.SelectTroubleList(Mode, Type, Moji, Table, paraCondition);
            //20170201 機能改善 END

            result = dac.SelectTroubleList(Mode, Type, Moji, Table, paraCondition, paraCategoryCondition); // 20170719 Add

            return result;
        }

        //20170201 機能改善 START
        /// <summary>
        /// キーワードリスト情報取得
        /// </summary>
        /// <param name="">無し</param>
        /// <returns>結果データテーブル</returns>
        /// <remarks></remarks>
        public DataTable GetKeyWordList()
        {
            DataTable result = null;

            // データアクセス作成
            DataAccess.DASearch dac = new DataAccess.DASearch();

            // ＳＱＬ実行
            result = dac.SelectKeyWordList();

            return result;
        }

        /// <summary>
        /// カテゴリ名称取得
        /// </summary>
        /// <param name="strType">カテゴリ種類</param>
        /// <param name="strCode">カテゴリコード</param>
        /// <returns>結果データテーブル</returns>
        /// <remarks></remarks>
        public DataTable GetCategoryName(String strType, String strCode)
        {
            DataTable result = null;

            // データアクセス作成
            DataAccess.DATroubleList dac = new DataAccess.DATroubleList();

            // ＳＱＬ実行
            result = dac.SelectCategoryName(strType, strCode);

            return result;
        }
        //20170201 機能改善 END

        /// <summary>
        /// EXCEL 作成処理
        /// </summary>
        /// <param name="ws">ExcelWorksheet</param>
        /// <param name="Type">種類</param>
        /// <param name="kakotora">結果データテーブル</returns>
        /// <param name="Category">カテゴリ</param>
        /// <remarks></remarks>
        //20170201 機能改善 START
        //public void CreateTorableList(ExcelWorksheet ws, String Type, DataTable kakotora)
        public void CreateTorableList(ExcelWorksheet ws, String Type, DataTable kakotora, List<String> Category)
        //20170201 機能改善 END
        {
            //20170201 機能改善 START
            // 見出しの出力
            CreateTorableList_Header(ws, Type, Category);
            //20170201 機能改善 END

            // 明細の出力
            CreateTorableList_Meisai(ws, kakotora);

            // フッタの出力
            CreateTorableList_Footer(ws);
            
       }

        /// <summary>
        /// EXCEL 見出し作成処理
        /// </summary>
        /// <param name="ws">ExcelWorksheet</param>
        /// <param name="Type">種類</param>
        /// <param name="Category">カテゴリ</param>
        /// <remarks></remarks>
        //20170201 機能改善 START
        //public void CreateTorableList_Header(ExcelWorksheet ws, String Type)
        public void CreateTorableList_Header(ExcelWorksheet ws, String Type, List<String> Category)
        //20170201 機能改善 END
        {
            // 見出しの出力
            // 作成日
            ws.Cells[Def.DefCREATEYMD_ROW, Def.DefCREATEYMD_CLM].Value =
                "作成日：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm");

            // 検索条件
            ws.Cells[Def.DefCONDITION_ROW, Def.DefCONDITION_CLM].Value = Type;
            //20170201 機能改善 START
            //20170306 START k-ohmatsuzawa EXCEL表示修正
            //ws.Cells[Def.DefCATEGORY_ROW, Def.DefCATEGORY_CLM].Value = Category;
            string workValue = "";
            for (int i = 0; i < Category.Count; i++)
            {
                if (i == 0)
                {
                    workValue = Category[i];
                }
                else
                {
                    workValue = workValue + Environment.NewLine + Category[i];
                }
            }
            ws.Cells[Def.DefCATEGORY_ROW, Def.DefCATEGORY_CLM].Value = workValue;
            ws.Row(Def.DefCATEGORY_ROW).Height = ws.Row(Def.DefCATEGORY_ROW).Height * Category.Count;
            //20170306 END k-ohmatsuzawa
            //20170201 機能改善 END
        }

        /// <summary>
        /// EXCEL 明細作成処理
        /// </summary>
        /// <param name="ws">ExcelWorksheet</param>
        /// <param name="kakotora">結果データテーブル</returns>
        /// <remarks></remarks>
        public void CreateTorableList_Meisai(ExcelWorksheet ws, DataTable kakotora)
        {
            // 明細を編集する
            for (int i = 0; i < kakotora.Rows.Count; i++)
            {
                // No.
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefNO_CLM].Value =
                    kakotora.Rows[i]["ROWID"].ToString();

                // 進捗 KATO/CEC DELETE 2016/09/15
                //ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefSTATUS_CLM].Value =
                //    kakotora.Rows[i]["FOLLOW_INFO"].ToString();
                // 進捗 KATO/CEC DELETE 2016/09/15
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefSTATUS_CLM].Value =
                    kakotora.Rows[i]["BY_PU"].ToString();

                // 項目管理No.
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefKOUMOKUNO_CLM].Value =
                    kakotora.Rows[i]["KOUMOKU_KANRI_NO"].ToString();

                // 項目
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefKOUMOKU_CLM].Value =
                    kakotora.Rows[i]["KOUMOKU"].ToString();

                // FMC
                string strFugo = null;
                int flg = 0;
                if (kakotora.Rows[i]["FUGO_NAME1"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strFugo += kakotora.Rows[i]["FUGO_NAME1"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strFugo += "\n" + kakotora.Rows[i]["FUGO_NAME1"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["FUGO_NAME2"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strFugo += kakotora.Rows[i]["FUGO_NAME2"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strFugo += "\n" + kakotora.Rows[i]["FUGO_NAME2"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["FUGO_NAME3"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strFugo += kakotora.Rows[i]["FUGO_NAME3"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strFugo += "\n" + kakotora.Rows[i]["FUGO_NAME3"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["FUGO_NAME4"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strFugo += kakotora.Rows[i]["FUGO_NAME4"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strFugo += "\n" + kakotora.Rows[i]["FUGO_NAME4"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["FUGO_NAME5"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strFugo += kakotora.Rows[i]["FUGO_NAME5"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strFugo += "\n" + kakotora.Rows[i]["FUGO_NAME5"].ToString().Trim();
                    }
                }
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefFMC_CLM].Value =
                    strFugo;

                // 現象
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefGENSYO_CLM].Value =
                    kakotora.Rows[i]["GENSYO_NAIYO"].ToString();

                // 状況
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefJOKYO_CLM].Value =
                    kakotora.Rows[i]["JYOUKYO"].ToString();

                // 原因
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefGENIN_CLM].Value =
                    kakotora.Rows[i]["GENIN"].ToString();

                // 対策
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefTAISAKU_CLM].Value =
                    kakotora.Rows[i]["TAISAKU"].ToString();

                // 開発時に何故発見できなかったのか
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefHAKKEN_CLM].Value =
                    kakotora.Rows[i]["KAIHATU_MIHAKKEN_RIYU"].ToString();

                // 確認の観点
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefKANTEN_CLM].Value =
                    kakotora.Rows[i]["SQB_KANTEN"].ToString();

                // 再発防止策（設計面）
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefSBOUSISAKU_CLM].Value =
                    kakotora.Rows[i]["SAIHATU_SEKKEI"].ToString();

                // 再発防止策（評価面）
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefHBOUSISAKU_CLM].Value =
                    kakotora.Rows[i]["SAIHATU_HYOUKA"].ToString();

                // 設計部署
                string strSekkei = null;
                flg = 0;
                if (kakotora.Rows[i]["BUSYO_SEKKEI1"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strSekkei +=  kakotora.Rows[i]["BUSYO_SEKKEI1"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strSekkei += "\n" + kakotora.Rows[i]["BUSYO_SEKKEI1"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["BUSYO_SEKKEI2"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strSekkei += kakotora.Rows[i]["BUSYO_SEKKEI2"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strSekkei += "\n" + kakotora.Rows[i]["BUSYO_SEKKEI2"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["BUSYO_SEKKEI3"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strSekkei += kakotora.Rows[i]["BUSYO_SEKKEI3"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strSekkei += "\n" + kakotora.Rows[i]["BUSYO_SEKKEI3"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["BUSYO_SEKKEI4"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strSekkei += kakotora.Rows[i]["BUSYO_SEKKEI4"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strSekkei += "\n" + kakotora.Rows[i]["BUSYO_SEKKEI4"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["BUSYO_SEKKEI5"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strSekkei += kakotora.Rows[i]["BUSYO_SEKKEI5"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strSekkei += "\n" + kakotora.Rows[i]["BUSYO_SEKKEI5"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["BUSYO_SEKKEI6"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strSekkei += kakotora.Rows[i]["BUSYO_SEKKEI6"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strSekkei += "\n" + kakotora.Rows[i]["BUSYO_SEKKEI6"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["BUSYO_SEKKEI7"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strSekkei += kakotora.Rows[i]["BUSYO_SEKKEI7"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strSekkei += "\n" + kakotora.Rows[i]["BUSYO_SEKKEI7"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["BUSYO_SEKKEI8"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strSekkei += kakotora.Rows[i]["BUSYO_SEKKEI8"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strSekkei += "\n" + kakotora.Rows[i]["BUSYO_SEKKEI8"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["BUSYO_SEKKEI9"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strSekkei += kakotora.Rows[i]["BUSYO_SEKKEI9"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strSekkei += "\n" + kakotora.Rows[i]["BUSYO_SEKKEI9"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["BUSYO_SEKKEI10"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strSekkei += kakotora.Rows[i]["BUSYO_SEKKEI10"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strSekkei += "\n" + kakotora.Rows[i]["BUSYO_SEKKEI10"].ToString().Trim();
                    }
                }
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefSEKKEI_CLM].Value =
                    strSekkei;

                // 評価部署
                string strHyouka = null;
                flg = 0;
                if (kakotora.Rows[i]["BUSYO_HYOUKA1"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strHyouka += kakotora.Rows[i]["BUSYO_HYOUKA1"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strHyouka += "\n" + kakotora.Rows[i]["BUSYO_HYOUKA1"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["BUSYO_HYOUKA2"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strHyouka += kakotora.Rows[i]["BUSYO_HYOUKA2"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strHyouka += "\n" + kakotora.Rows[i]["BUSYO_HYOUKA2"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["BUSYO_HYOUKA3"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strHyouka += kakotora.Rows[i]["BUSYO_HYOUKA3"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strHyouka += "\n" + kakotora.Rows[i]["BUSYO_HYOUKA3"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["BUSYO_HYOUKA4"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strHyouka += kakotora.Rows[i]["BUSYO_HYOUKA4"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strHyouka += "\n" + kakotora.Rows[i]["BUSYO_HYOUKA4"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["BUSYO_HYOUKA5"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strHyouka += kakotora.Rows[i]["BUSYO_HYOUKA5"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strHyouka += "\n" + kakotora.Rows[i]["BUSYO_HYOUKA5"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["BUSYO_HYOUKA6"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strHyouka += kakotora.Rows[i]["BUSYO_HYOUKA6"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strHyouka += "\n" + kakotora.Rows[i]["BUSYO_HYOUKA6"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["BUSYO_HYOUKA7"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strHyouka += kakotora.Rows[i]["BUSYO_HYOUKA7"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strHyouka += "\n" + kakotora.Rows[i]["BUSYO_HYOUKA7"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["BUSYO_HYOUKA8"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strHyouka += kakotora.Rows[i]["BUSYO_HYOUKA8"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strHyouka += "\n" + kakotora.Rows[i]["BUSYO_HYOUKA8"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["BUSYO_HYOUKA9"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strHyouka += kakotora.Rows[i]["BUSYO_HYOUKA9"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strHyouka += "\n" + kakotora.Rows[i]["BUSYO_HYOUKA9"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["BUSYO_HYOUKA10"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strHyouka += kakotora.Rows[i]["BUSYO_HYOUKA10"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strHyouka += "\n" + kakotora.Rows[i]["BUSYO_HYOUKA10"].ToString().Trim();
                    }
                }
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefHYOUKA_CLM].Value =
                    strHyouka;

                // 資料No.一覧
                string strSiryo = null;
                flg = 0;
                if (kakotora.Rows[i]["SIRYOU_NO1"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strSiryo += kakotora.Rows[i]["SIRYOU_NO1"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strSiryo += "\n" + kakotora.Rows[i]["SIRYOU_NO1"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["SIRYOU_NO2"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strSiryo += kakotora.Rows[i]["SIRYOU_NO2"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strSiryo += "\n" + kakotora.Rows[i]["SIRYOU_NO2"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["SIRYOU_NO3"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strSiryo += kakotora.Rows[i]["SIRYOU_NO3"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strSiryo += "\n" + kakotora.Rows[i]["SIRYOU_NO3"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["SIRYOU_NO4"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strSiryo += kakotora.Rows[i]["SIRYOU_NO4"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strSiryo += "\n" + kakotora.Rows[i]["SIRYOU_NO4"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["SIRYOU_NO5"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strSiryo += kakotora.Rows[i]["SIRYOU_NO5"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strSiryo += "\n" + kakotora.Rows[i]["SIRYOU_NO5"].ToString().Trim();
                    }
                }
                if (kakotora.Rows[i]["KANREN_KANRI_NO"].ToString().Trim() != "")
                {
                    if (flg == 0)
                    {
                        strSiryo += kakotora.Rows[i]["KANREN_KANRI_NO"].ToString().Trim();
                        flg = 1;
                    }
                    else
                    {
                        strSiryo += "\n" + kakotora.Rows[i]["KANREN_KANRI_NO"].ToString().Trim();
                    }
                }
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefSIRYO_CLM].Value =
                    strSiryo;

                // 重要度ランク
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefRANK_CLM].Value =
                    kakotora.Rows[i]["RANK"].ToString();

                // 再発案件
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefSAIHATU_CLM].Value =
                    kakotora.Rows[i]["SAIHATU"].ToString();

                // RSC項目
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefRSC_CLM].Value =
                    kakotora.Rows[i]["RSC"].ToString();

                // 主務部署
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefSYUMU_CLM].Value =
                    kakotora.Rows[i]["SYUMU"].ToString();

                // チェック欄

                // 書式の設定
                CreateTorableList_Syosiki(ws, i);

            }
        }

        /// <summary>
        /// EXCEL 書式設定処理
        /// </summary>
        /// <param name="ws">ExcelWorksheet</param>
        /// <param name="i">行</param>
        /// <remarks></remarks>
        public void CreateTorableList_Syosiki(ExcelWorksheet ws, int i)
        {
            // 行を指示
            var cells = ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefNO_CLM,
                                Def.DefMEISAISTART_ROW + i, Def.DefCHECKC_CLM];

            // 内容すべてを表示する
            cells.Style.WrapText = true;

            // BOX罫線を引く
            cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;          
         }

        /// <summary>
        /// EXCEL フッター作成処理
        /// </summary>
        /// <param name="ws">ExcelWorksheet</param>
        /// <remarks></remarks>
        public void CreateTorableList_Footer(ExcelWorksheet ws)
        {
            //ユーザー名取得
            CommonLogic bcom = new CommonLogic();
            DataTable dtUserInfo = bcom.GetUser();

            //フッター設定
            if (dtUserInfo.Rows.Count > 0 )
            { 
                string strFooterTxt =  
                    DateTime.Now.ToString("yyyy/MM/dd") + "_" +
                    dtUserInfo.Rows[0]["USER_NAME"].ToString() + " " +
                    String.Format("{0} / {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);
                ws.HeaderFooter.OddFooter.RightAlignedText = string.Format("&10&\"\"{0}", strFooterTxt);
            }
        }
    }
}