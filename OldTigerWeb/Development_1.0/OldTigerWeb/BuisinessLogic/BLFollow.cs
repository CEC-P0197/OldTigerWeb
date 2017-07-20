using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace OldTigerWeb.BuisinessLogic
{
    public class BLFollow
    {
        /// <summary>
        /// フォロー情報取得
        /// </summary>
        /// <param name="FMC_mc">FMC/mc 1:FMC、2:mc</param>
        /// <param name="Type">種類 1:現在、2:過去</param>
        /// <returns>結果ステータス</returns>
        /// <remarks></remarks>
        public DataTable GetFollowList(String FMC_mc, String Type)
        {
            DataTable result = null;

            // データアクセス作成
            DataAccess.DAFollow dac = new DataAccess.DAFollow();

            // ＳＱＬ実行
            result = dac.SelectFollowList(FMC_mc, Type);

            return result;
        }
        
        /// <summary>
        /// フォローダウンロード情報取得
        /// </summary>
        /// <param name="FMC_mc">FMC/mc 1:FMC、2:mc</param>
        /// <param name="KaihatuId">開発ＩＤ</param>
        /// <param name="BY_PU">BY/PU</param>
        /// <param name="EventNo">イベントNo</param>
        /// <param name="KaCode">課コード</param>
        /// <returns>結果データテーブル</returns>
        public DataTable GetDownloadList(String FMC_mc, String KaihatuId, String BY_PU, String EventNo, String KaCode)
        {
            DataTable result = null;

            // データアクセス作成
            DataAccess.DAFollow dac = new DataAccess.DAFollow();

            // ＳＱＬ実行
            result = dac.SelectFollowDownList(FMC_mc, KaihatuId, BY_PU, EventNo, KaCode);

            return result;
        }

        /// <summary>
        /// EXCEL 作成処理
        /// </summary>
        /// <param name="ws">ExcelWorksheet</param>
        /// <param name="fullEvent">フルイベント名</param>
        /// <param name="Event">イベント名</param>
        /// <param name="kakotora">結果データテーブル</returns>
        public void CreateFollowList(ExcelWorksheet ws, String fullEvent, String Event, DataTable followinfo)
        {
            // 見出しの出力
            CreateFollowList_Header(ws, fullEvent, Event);

            if (followinfo == null || followinfo.Rows.Count > 0 )
            {
                // 明細の出力
                CreateFollowList_Meisai(ws, followinfo);
            }

            // フッタの出力
            CreateFollowList_Footer(ws);

        }

        /// <summary>
        /// EXCEL 見出し作成処理
        /// </summary>
        /// <param name="ws">ExcelWorksheet</param>
        /// <param name="fullEvent">フルイベント名</param>
        /// <param name="Event">イベント名</param>
        /// <remarks></remarks>
        public void CreateFollowList_Header(ExcelWorksheet ws, String fullEvent, String Event)
        {
            // 見出しの出力

            // フォローイベント情報
            ws.Cells[Const.Def.DefHDFCONDITION_ROW, Const.Def.DefHDFCONDITION_CLM].Value = fullEvent;

            // 作成日
            ws.Cells[Const.Def.DefHDFCREATEYMD_ROW, Const.Def.DefHDFCREATEYMD_CLM].Value =
                "作成日：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm");

            // フォローイベント情報
            ws.Cells[Const.Def.DefHDFEVENT_ROW, Const.Def.DefHDFEVENT_CLM].Value = Event;

        }

        /// <summary>
        /// EXCEL 明細作成処理
        /// </summary>
        /// <param name="ws">ExcelWorksheet</param>
        /// <param name="kakotora">結果データテーブル</returns>
        /// <remarks></remarks>
        public void CreateFollowList_Meisai(ExcelWorksheet ws, DataTable kakotora)
        {
            // 明細を編集する
            for (int i = 0; i < kakotora.Rows.Count; i++)
            {
                // No.
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFNO_CLM].Value =
                    kakotora.Rows[i]["ROWID"].ToString();

                // 部品
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFBUHIN_CLM].Value =
                    kakotora.Rows[i]["BUHIN_NAME"].ToString();

                // マスタの現象
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFGENSYO_CLM].Value =
                    kakotora.Rows[i]["BUNRUI_GENSYO_NAME"].ToString();

                // マスタの制御系要因
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFSEIGYOFACTOR_CLM].Value =
                    kakotora.Rows[i]["SEIGYO_FACTOR_NAME"].ToString();

                // 進捗
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFSINCHOKU_CLM].Value =
                    kakotora.Rows[i]["FOLLOW_INFO"].ToString();

                // 項目管理No.
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFKANRINO_CLM].Value =
                    kakotora.Rows[i]["KOUMOKU_KANRI_NO"].ToString();

                // 項目
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFKOUMOKU_CLM].Value =
                    kakotora.Rows[i]["KOUMOKU"].ToString();

                // 原因
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFGENIN_CLM].Value =
                    kakotora.Rows[i]["GENIN"].ToString();

                // 対策
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFTAISAKU_CLM].Value =
                    kakotora.Rows[i]["TAISAKU"].ToString();

                // 開発時の流出要因
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFHAKKEN_CLM].Value =
                    kakotora.Rows[i]["KAIHATU_MIHAKKEN_RIYU"].ToString();

                // 確認の観点
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFKANTEN_CLM].Value =
                    kakotora.Rows[i]["SQB_KANTEN"].ToString();

                // 再発防止策（設計面）
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFSBOUSISAKU_CLM].Value =
                    kakotora.Rows[i]["SAIHATU_SEKKEI"].ToString();

                // 再発防止策（評価面）
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFHBOUSISAKU_CLM].Value =
                    kakotora.Rows[i]["SAIHATU_HYOUKA"].ToString();

                // 資料No.一覧
                string strSiryo = null;
                int flg = 0;
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
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFSIRYO_CLM].Value =
                    strSiryo;

                // 部署
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFBUSYO_CLM].Value =
                    kakotora.Rows[i]["KA_CODE"].ToString();

                // 適用有無<SQB>
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFSQB_CLM].Value =
                    kakotora.Rows[i]["TEKIYO_SQB"].ToString();

                // 適用有無<設計>
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFSHYOUKA_CLM].Value =
                    kakotora.Rows[i]["TEKIYO_SEKKEI"].ToString();

                // ﾋｱﾘﾝｸﾞ要望
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFHEARING_CLM].Value =
                    kakotora.Rows[i]["HEARING"].ToString();

                // 進度
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFSINDO_CLM].Value =
                    kakotora.Rows[i]["SINDO"].ToString();

                // 対応内容
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFKAITO_CLM].Value =
                    kakotora.Rows[i]["TAIOU_NAIYO"].ToString();

                // 重要度ランク
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFRANK_CLM].Value =
                    kakotora.Rows[i]["RANK"].ToString();

                // 再発案件
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFSAIHATU_CLM].Value =
                    kakotora.Rows[i]["SAIHATU"].ToString();

                // RSC項目
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFRSC_CLM].Value =
                    kakotora.Rows[i]["RSC"].ToString();

                // 主務部署
                ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFSYUMU_CLM].Value =
                    kakotora.Rows[i]["SYUMU"].ToString();

                String grey = "OFF";

                // 類似は背景色設定
                if ( kakotora.Rows[i]["SAIHATU"].ToString().Trim() == Const.Def.DefRUIJI )
                {
                    grey = "ON";
                }

                // 書式の設定
                CreateFollowList_Syosiki(ws, i, grey);

            }
        }

        /// <summary>
        /// EXCEL 書式設定処理
        /// </summary>
        /// <param name="ws">ExcelWorksheet</param>
        /// <param name="i">行</param>
        /// <remarks></remarks>
        public void CreateFollowList_Syosiki(ExcelWorksheet ws, int i, String grey)
        {
            // 行を指示
            var cells = ws.Cells[Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFNO_CLM,
                                Const.Def.DefFMEISAISTART_ROW + i, Const.Def.DefFSYUMU_CLM];

            if (grey == "ON")
            {
                cells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                cells.Style.Fill.BackgroundColor.SetColor(Color.Gray);
            }

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
        public void CreateFollowList_Footer(ExcelWorksheet ws)
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
                    String.Format("{0} / {1}", ExcelHeaderFooter.PageNumber,  ExcelHeaderFooter.NumberOfPages);
                ws.HeaderFooter.OddFooter.RightAlignedText = string.Format("&10&\"\"{0}", strFooterTxt);
            }
        }

    }
}