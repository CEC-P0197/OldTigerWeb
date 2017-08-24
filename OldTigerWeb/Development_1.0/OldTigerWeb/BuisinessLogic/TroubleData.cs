using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Linq;
using System.Web;
using OldTigerWeb.DataAccess;
using OfficeOpenXml;
using System.Text;
using OfficeOpenXml.Style;

namespace OldTigerWeb.BuisinessLogic
{

    public class TroubleData
    {
        #region フィールド

        private IDATroubleData _dbTroubleData;
        #endregion


        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TroubleData():this(new DATroubleData()){}
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbTroubleData"></param>
        public TroubleData(IDATroubleData dbTroubleData)
        {
            _dbTroubleData = dbTroubleData;
        }
        #endregion

        #region メソッド

        /// <summary>
        /// 過去トラ情報取得
        /// </summary>
        /// <param name="mode">モード(画面用："1"、EXCEL用："2")</param>
        /// <param name="keyword">検索キーワード</param>
        /// <param name="keywordCondition">キーワード検索用 And・Or検索条件(1：AND、2：OR)</param>
        /// <param name="cotegory">検索カテゴリ</param>
        /// <param name="cotegoryCondition">カテゴリ検索用 And・Or検索条件(1：AND、2：OR)</param>
        /// <returns>過去トラ情報</returns>
        public DataTable getTroubleList(String keyword, String keywordCondition,
            DataTable cotegory, String cotegoryCondition, String mode = Def.DefMODE_DISP)
        {
            return _dbTroubleData.getTroubleList(keyword, keywordCondition, cotegory,cotegoryCondition, mode);
        }
        /// <summary>
        /// キーワードリスト情報取得
        /// </summary>
        /// <returns></returns>
        public DataTable getKeyWordList()
        {
            DataTable result = null;

            // データアクセス作成
            DASearch db = new DASearch();

            // ＳＱＬ実行
            result = db.SelectKeyWordList();

            return result;
        }
        /// <summary>
        /// カテゴリ名称取得
        /// </summary>
        /// <param name="strType">カテゴリ種類</param>
        /// <param name="strCode">カテゴリコード</param>
        /// <returns>結果データテーブル</returns>
        /// <remarks></remarks>
        public DataTable getCategoryName(String strType, String strCode)
        {
            DataTable result = null;

            // データアクセス作成
            DataAccess.DATroubleList dac = new DataAccess.DATroubleList();

            // ＳＱＬ実行
            result = dac.SelectCategoryName(strType, strCode);

            return result;
        }

        /// <summary>
        /// EXCEL 作成処理
        /// </summary>
        /// <param name="ws">ExcelWorksheet</param>
        /// <param name="Type">種類</param>
        /// <param name="kakotora">結果データテーブル</returns>
        /// <param name="Category">カテゴリ</param>
        /// <remarks></remarks>
        public void CreateTroubleList(ExcelWorksheet ws, String Type, DataTable kakotora, List<String> Category)
        {
            // 見出しの出力
            createTroubleList_Header(ws, Type, Category);

            // 明細の出力
            createTroubleList_Detail(ws, kakotora);

            // フッタの出力
            CreateTorableList_Footer(ws);

        }
        /// <summary>
        /// EXCEL 見出し作成処理   
        /// </summary>
        /// <param name="ws">ワークシート</param>
        /// <param name="searchType">種類</param>
        /// <param name="cotegory">カテゴリ</param>
        private void createTroubleList_Header(ExcelWorksheet ws,string searchType ,List<string> cotegory)
        {
            // 見出し出力
            /// 作成日
            ws.Cells[Def.DefCREATEYMD_ROW, Def.DefCREATEYMD_CLM].Value =
                "作成日：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm");

            /// 検索条件
            ws.Cells[Def.DefCONDITION_ROW, Def.DefCREATEYMD_CLM].Value = searchType;

            string workVal = "";
            for (int i= 0;i< cotegory.Count;i++)
            {
                if (i != 0)
                {
                    workVal = workVal + Environment.NewLine;
                }
                workVal = workVal + cotegory[i];
            }
            ws.Cells[Def.DefCATEGORY_ROW, Def.DefCATEGORY_CLM].Value = workVal;
            ws.Row(Def.DefCATEGORY_ROW).Height = ws.Row(Def.DefCATEGORY_ROW).Height * cotegory.Count;
        }

        /// <summary>
        /// EXCEL 明細作成処理
        /// </summary>
        /// <param name="ws">ExcelWorksheet</param>
        /// <param name="kakotora">結果データテーブル</returns>
        /// <remarks></remarks>
        private void createTroubleList_Detail(ExcelWorksheet ws, DataTable troubleList)
        {
            // 明細編集(行毎)
            for (int i = 0; i < troubleList.Rows.Count; i++)
            {
                // No
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefNO_CLM].Value =
                    troubleList.Rows[i]["BY_PU"].ToString();

                // 進捗
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefSTATUS_CLM].Value =
                    troubleList.Rows[i]["KOUMOKU_KANRI_NO"].ToString();

                // 項目
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefKOUMOKUNO_CLM].Value =
                    troubleList.Rows[i]["KOUMOKU"].ToString();

                // FMC
                StringBuilder sbFmc = new StringBuilder();
                for (int j = 1; j <= 5; j++)
                {
                    if (troubleList.Rows[i]["FUGO_NAME" + j.ToString()].ToString().Trim() != "")
                    {
                        if (sbFmc.Length != 0)
                        {
                            sbFmc.AppendLine("\n");
                        }
                        sbFmc.AppendLine(troubleList.Rows[i]["FUGO_NAME" + j.ToString()].ToString().Trim());
                    }
                }
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefFMC_CLM].Value = sbFmc.ToString();

                // 現象
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefGENSYO_CLM].Value =
                    troubleList.Rows[i]["GENSYO_NAIYO"].ToString();

                // 状況
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefJOKYO_CLM].Value =
                    troubleList.Rows[i]["JOUKYO"].ToString();

                // 原因
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefGENIN_CLM].Value =
                    troubleList.Rows[i]["GENIN"].ToString();

                // 対策
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefTAISAKU_CLM].Value =
                    troubleList.Rows[i]["TAISAKU"].ToString();


                // 開発時に何故発見できなかったのか
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefHAKKEN_CLM].Value =
                    troubleList.Rows[i]["KAIHATU_MIHAKKEN_RIYU"].ToString();

                // 確認の観点
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefKANTEN_CLM].Value =
                    troubleList.Rows[i]["SQB_KANTEN"].ToString();

                // 再発防止策（設計面）
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefSBOUSISAKU_CLM].Value =
                    troubleList.Rows[i]["SAIHATU_SEKKEI"].ToString();

                // 再発防止策（評価面）
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefHBOUSISAKU_CLM].Value =
                    troubleList.Rows[i]["SAIHATU_HYOUKA"].ToString();

                // 設計部署
                StringBuilder sbDesign = new StringBuilder();
                for (int j = 1; j <= 10; j++)
                {
                    if (troubleList.Rows[i]["BUSYO_SEKKEI" + j.ToString()].ToString().Trim() != "")
                    {
                        if (sbDesign.Length != 0)
                        {
                            sbDesign.AppendLine("\n");
                        }
                        sbDesign.AppendLine(troubleList.Rows[i]["BUSYO_SEKKEI" + j.ToString()].ToString().Trim());
                    }
                }
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefSEKKEI_CLM].Value = sbDesign.ToString();

                // 評価部署
                StringBuilder sbEvaluation = new StringBuilder();
                for (int j = 1; j <= 10; j++)
                {
                    if (troubleList.Rows[i]["BUSYO_HYOUKA" + j.ToString()].ToString().Trim() != "")
                    {
                        if (sbEvaluation.Length != 0)
                        {
                            sbEvaluation.AppendLine("\n");
                        }
                        sbEvaluation.AppendLine(troubleList.Rows[i]["BUSYO_HYOUKA" + j.ToString()].ToString().Trim());
                    }
                }
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefHYOUKA_CLM].Value = sbEvaluation.ToString();

                // 関連No.一覧
                StringBuilder sbRelation = new StringBuilder();
                for (int j = 1; j <= 5; j++)
                {
                    if (troubleList.Rows[i]["SIRYOU_NO" + j.ToString()].ToString().Trim() != "")
                    {
                        if (sbRelation.Length != 0)
                        {
                            sbRelation.AppendLine("\n");
                        }
                        sbRelation.AppendLine(troubleList.Rows[i]["SIRYOU_NO" + j.ToString()].ToString().Trim());
                    }
                }
                if (troubleList.Rows[i]["KANREN_KANRI_NO"].ToString().Trim() != "")
                {
                    if (sbRelation.Length != 0)
                    {
                        sbRelation.AppendLine("\n");
                    }
                    sbRelation.AppendLine(troubleList.Rows[i]["KANREN_KANRI_NO"].ToString().Trim());
                }

                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefSIRYO_CLM].Value = sbRelation.ToString();

                // 重要度ランク
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefRANK_CLM].Value =
                    troubleList.Rows[i]["RANK"].ToString();

                // 再発案件
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefSAIHATU_CLM].Value =
                    troubleList.Rows[i]["SAIHATU"].ToString();

                // RSC項目
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefRSC_CLM].Value =
                    troubleList.Rows[i]["RSC"].ToString();

                // 主務部署
                ws.Cells[Def.DefMEISAISTART_ROW + i, Def.DefSYUMU_CLM].Value =
                    troubleList.Rows[i]["SYUMU"].ToString();

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
            if (dtUserInfo.Rows.Count > 0)
            {
                string strFooterTxt =
                    DateTime.Now.ToString("yyyy/MM/dd") + "_" +
                    dtUserInfo.Rows[0]["USER_NAME"].ToString() + " " +
                    String.Format("{0} / {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);
                ws.HeaderFooter.OddFooter.RightAlignedText = string.Format("&10&\"\"{0}", strFooterTxt);
            }
        }

        /// <summary>
        /// カテゴリ用データテーブルフォーマット取得
        /// </summary>
        /// <returns></returns>
        public DataTable getCotegoryDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TableType",typeof(string));
            dt.Columns.Add("ItemValue1",typeof(string));

            return dt;
        }

        #endregion
    }
}