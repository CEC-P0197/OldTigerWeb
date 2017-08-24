using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Text;

namespace OldTigerWeb.DataAccess
{
    public class DATroubleList
    {
        #region 過去トラ情報取得
        /// <summary>
        /// 過去トラ情報取得
        /// </summary>
        /// <param name="Mode">モード：1:画面、2:Excel</param>
        /// <param name="Type">種類　カテゴリ検索の場合はnull</param>
        /// <param name="Moji">検索文字　カテゴリ検索の場合はnull</param>
        /// <param name="paraArrWord1">カテゴリ検索用配列１</param>
        /// <param name="paraArrWord2">カテゴリ検索用配列２（評価部署用）</param>
        /// <param name="Table">カテゴリデータテーブル（カテゴリ検索用）</param>
        /// <param name="paraCondition">キーワード検索用 And・Or検索条件  1：And、2：Or</param>
        /// <param name="paraCategoryCondition">カテゴリ検索用 And・Or検索条件  1：And、2：Or</param> // 20170719 Add
        /// <returns>取得結果情報</returns>
        //20170201 機能改善 START
        //public DataTable SelectTroubleList(String Mode, String Type, String Moji, ArrayList paraArrWord1, ArrayList paraArrWord2)
        //public DataTable SelectTroubleList(String Mode, String Type, String Moji, ArrayList paraArrWord1, ArrayList paraArrWord2, DataTable Table, String paraCondition)
        //20170201 機能改善 END
        public DataTable SelectTroubleList(String Mode, String Type, String Moji, ArrayList paraArrWord1, ArrayList paraArrWord2, DataTable Table, String paraCondition, 
            String paraCategoryCondition) // 20170719 Add
        {
            String strSql = "";
            String strWork = "";
            String strWhere = ""; // 20170724 Add
            String strKeyWordStartKbn = ""; // 過去トラ検索結果 カテゴリ検索 0:一番目ではない、1:一番目 // 20170725 Add

            //String strMoji = "";
            String[] strArrayData = null;

            DataTable result = new DataTable();
            DataTable resultCopy = new DataTable(); // 20170721 Add

            strSql = "SELECT " + "\r\n";

            if (Mode == Const.Def.DefMODE_DISP)
            {   // 画面情報
                strSql += "CONVERT(varchar,ROW_NUMBER() OVER(ORDER BY YMD_HENSYU DESC, BY_PU ASC, FOLLOW_NO ASC, FOLLOW_EDA DESC)) AS ROWID, " + "\r\n";
                strSql += "RANK + '<br>' + SAIHATU + '<br>' + RSC + '<br>' + " + "\r\n";
                strSql += "CASE WHEN RTRIM(SYUMU_SEIZO) = '○' AND RTRIM(SYUMU_GAISEI) != '○' THEN '製造' " + "\r\n";
                strSql += "    ELSE CASE WHEN RTRIM(SYUMU_SEIZO) != '○' AND RTRIM(SYUMU_GAISEI) = '○' THEN '外製' " + "\r\n";
                strSql += "        ELSE '設計' " + "\r\n";
                strSql += "    END " + "\r\n";
                strSql += "END AS SYUMU, RANK, " + "\r\n";
                strSql += "SYSTEM_NO, FOLLOW_INFO, KOUMOKU_KANRI_NO, KOUMOKU, " + "\r\n";
                strSql += "FUGO_NAME1 + '<br>' + FUGO_NAME2 + '<br>' + FUGO_NAME3 + '<br>' + FUGO_NAME4 AS FUGO_NAME, " + "\r\n";
                strSql += "GENSYO_NAIYO, JYOUKYO, GENIN, TAISAKU, KAIHATU_MIHAKKEN_RIYU, SQB_KANTEN, SAIHATU_SEKKEI, SAIHATU_HYOUKA, " + "\r\n";
                strSql += "BUSYO_SEKKEI1 + '<br>' + BUSYO_SEKKEI2 + '<br>' + BUSYO_HYOUKA1  + '<br>' + BUSYO_HYOUKA2 AS BUSYO_CODE, " + "\r\n";
                strSql += "RTRIM(SIRYOU_NO1) + '<br>' +  RTRIM(SIRYOU_NO2)" + "\r\n";
                strSql += "AS SIRYOU_NO, LINK_FOLDER_PATH," + "\r\n";
                strSql += "BY_PU, FUGO_NO1, TRA.INSERT_YMD, YMD_HENSYU, INPUT_ROW, SEQ " + "\r\n";
            }
            else
            {   // Excel情報
                strSql += "CONVERT(varchar,ROW_NUMBER() OVER(ORDER BY YMD_HENSYU DESC, BY_PU ASC, FOLLOW_NO ASC, FOLLOW_EDA DESC)) AS ROWID, " + "\r\n";
                strSql += "RANK, SAIHATU, RSC, " + "\r\n";
                strSql += "CASE WHEN RTRIM(SYUMU_SEIZO) = '○' AND RTRIM(SYUMU_GAISEI) != '○' THEN '製造' " + "\r\n";
                strSql += "    ELSE CASE WHEN RTRIM(SYUMU_SEIZO) != '○' AND RTRIM(SYUMU_GAISEI) = '○' THEN '外製' " + "\r\n";
                strSql += "        ELSE '設計' " + "\r\n";
                strSql += "    END " + "\r\n";
                strSql += "END AS SYUMU, " + "\r\n";
                strSql += "SYSTEM_NO, FOLLOW_INFO, KOUMOKU_KANRI_NO, KOUMOKU, " + "\r\n";
                strSql += "FUGO_NAME1, FUGO_NAME2, FUGO_NAME3, FUGO_NAME4, FUGO_NAME5, " + "\r\n";
                strSql += "GENSYO_NAIYO, JYOUKYO, GENIN, TAISAKU, KAIHATU_MIHAKKEN_RIYU, SQB_KANTEN, " + "\r\n";
                strSql += "SAIHATU_SEKKEI, SAIHATU_HYOUKA, " + "\r\n";
                strSql += "BUSYO_SEKKEI1, BUSYO_SEKKEI2, BUSYO_SEKKEI3, BUSYO_SEKKEI4, BUSYO_SEKKEI5, " + "\r\n";
                strSql += "BUSYO_SEKKEI6, BUSYO_SEKKEI7, BUSYO_SEKKEI8, BUSYO_SEKKEI9, BUSYO_SEKKEI10, " + "\r\n";
                strSql += "BUSYO_HYOUKA1, BUSYO_HYOUKA2, BUSYO_HYOUKA3, BUSYO_HYOUKA4, BUSYO_HYOUKA5, " + "\r\n";
                strSql += "BUSYO_HYOUKA6, BUSYO_HYOUKA7, BUSYO_HYOUKA8, BUSYO_HYOUKA9, BUSYO_HYOUKA10, " + "\r\n";
                strSql += "SIRYOU_NO1, SIRYOU_NO2, SIRYOU_NO3, SIRYOU_NO4, SIRYOU_NO5, KANREN_KANRI_NO, " + "\r\n";
                strSql += "BY_PU, FUGO_NO1, TRA.INSERT_YMD, YMD_HENSYU, INPUT_ROW, SEQ " + "\r\n";
            }

            strSql += "FROM T_TROUBLE_DATA TRA " + "\r\n";
            strSql += "LEFT JOIN M_DEVELOPMENTSIGN DEVSIGN " + "\r\n";
            strSql += "ON TRA.FUGO_NAME1 = DEVSIGN.KAIHATU_FUGO " + "\r\n";
            //20170201 機能改善 START
            //strSql += "WHERE RANK <> 'X' " + "\r\n";
            strSql += "WHERE "; // 20170724 Add WHERE句の内容はstrWhereに格納

            // タイプにより取得条件を組立てる
            bool andFlg = false;

            if (Type == Const.Def.DefTYPE_WORD || Type == Const.Def.DefTYPE_TOP10)
            {
                // 文字列検索
                // TOP10検索
                strWork = "";
                strWork = "@moji";
                 
                // 半角スペースでスプリット
                strArrayData = Moji.Trim().Split(' ');

                for (int i = 0; i < strArrayData.Length; i++)
                {   
                    if (i > 3)
                    {
                        break;
                    }
                    if (i != 0)
                    {
                        if (paraCondition == Const.Def.DefTYPE_AND)
                        {
                            strWhere += ") AND (";
                        }
                        else
                        {
                            strWhere += ") OR (";
                        }
                    }
                    else
                    {
                        strWhere += "AND ((";
                    }

                    strWork = "@moji" + i.ToString();

                    strWhere += "KOUMOKU_KANRI_NO LIKE " + strWork + " OR BY_PU LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "SYSTEM_NAME1 LIKE " + strWork + " OR SYSTEM_NAME2 LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "BUHIN_NAME1 LIKE " + strWork + " OR BUHIN_NAME2 LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "KOBUHIN_NAME1 LIKE " + strWork + " OR KOBUHIN_NAME2 LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "BUNRUI_GENSYO_NAME LIKE " + strWork + " OR BUNRUI_CASE_NAME LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "SEIGYO_UNIT_NAME LIKE " + strWork + " OR SEIGYO_GENSYO_NAME LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "SEIGYO_FACTOR_NAME LIKE " + strWork + " OR KATA_NAME LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "EGTM_NAME LIKE " + strWork + " OR HAIKI_NAME LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "KOUMOKU LIKE " + strWork + " OR GENSYO_NAIYO LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "JYOUKYO LIKE " + strWork + " OR GENIN LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "TAISAKU LIKE " + strWork + " OR KAIHATU_MIHAKKEN_RIYU LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "SAIHATU_SEKKEI LIKE " + strWork + " OR SAIHATU_HYOUKA LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "SQB_KANTEN LIKE " + strWork + " OR BUSYO_SEKKEI1 LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "BUSYO_SEKKEI2 LIKE " + strWork + " OR BUSYO_SEKKEI3 LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "BUSYO_SEKKEI4 LIKE " + strWork + " OR BUSYO_SEKKEI5 LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "BUSYO_SEKKEI6 LIKE " + strWork + " OR BUSYO_SEKKEI7 LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "BUSYO_SEKKEI8 LIKE " + strWork + " OR BUSYO_SEKKEI9 LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "BUSYO_SEKKEI10 LIKE " + strWork + " OR BUSYO_HYOUKA1 LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "BUSYO_HYOUKA2 LIKE " + strWork + " OR BUSYO_HYOUKA3 LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "BUSYO_HYOUKA4 LIKE " + strWork + " OR BUSYO_HYOUKA5 LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "BUSYO_HYOUKA6 LIKE " + strWork + " OR BUSYO_HYOUKA7 LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "BUSYO_HYOUKA8 LIKE " + strWork + " OR BUSYO_HYOUKA9 LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "BUSYO_HYOUKA10 LIKE " + strWork + " OR FUGO_NAME1 LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "FUGO_NAME2 LIKE " + strWork + " OR FUGO_NAME3 LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "FUGO_NAME4 LIKE " + strWork + " OR FUGO_NAME5 LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "BLKNO1 LIKE " + strWork + " OR BLKNO2 LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "BLKNO3 LIKE " + strWork + " OR BUHIN_BANGO1 LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "BUHIN_BANGO2 LIKE " + strWork + " OR BUHIN_BANGO3 LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "BUHIN_BANGO4 LIKE " + strWork + " OR BUHIN_BANGO5 LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "KANREN_KANRI_NO LIKE " + strWork + " OR KEYWORD LIKE " + strWork + " OR " + "\r\n";
                    strWhere += "JYUYO_HOUKI LIKE " + strWork + " " + "\r\n";
                }
                strWhere += ")) " + "\r\n";
                strKeyWordStartKbn = Const.Def.DefTYPE_FIRST;
            }

            // 部署・設計
            // 設計部署
            DataRow[] drBusyo;
            SetSelectTable(Table, out drBusyo, Const.Def.DefTYPE_BUSYO);

            if (drBusyo.Length  > 0)
            {
                strWork = "";
                for (int i = 0; drBusyo.Length > i; i++)
                {
                    if (i != 0)
                    {
                        strWork += ",";
                    }
                    strWork += "'" + drBusyo[i]["ItemValue1"].ToString().Trim() + "'";
                }

                // 20170719 Add Start
                if (paraCategoryCondition == Const.Def.DefTYPE_AND)
                {
                    strWhere += "AND (";
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type != null
                    && strKeyWordStartKbn == Const.Def.DefTYPE_FIRST) // 過去トラ検索結果　カテゴリの一番目
                {
                    strWhere += "AND ((";
                    strKeyWordStartKbn = Const.Def.DefTYPE_NEXT;
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type == null) // 過去トラ検索
                {
                    strWhere += "OR (";
                }
                // 20170719 Add End

                strWhere += "BUSYO_SEKKEI1  IN (" + strWork + ") OR " + "\r\n";
                strWhere += "BUSYO_SEKKEI2  IN (" + strWork + ") OR " + "\r\n";
                strWhere += "BUSYO_SEKKEI3  IN (" + strWork + ") OR " + "\r\n";
                strWhere += "BUSYO_SEKKEI4  IN (" + strWork + ") OR " + "\r\n";
                strWhere += "BUSYO_SEKKEI5  IN (" + strWork + ") OR " + "\r\n";
                strWhere += "BUSYO_SEKKEI6  IN (" + strWork + ") OR " + "\r\n";
                strWhere += "BUSYO_SEKKEI7  IN (" + strWork + ") OR " + "\r\n";
                strWhere += "BUSYO_SEKKEI8  IN (" + strWork + ") OR " + "\r\n";
                strWhere += "BUSYO_SEKKEI9  IN (" + strWork + ") OR " + "\r\n";
                strWhere += "BUSYO_SEKKEI10 IN (" + strWork + ") " + "\r\n";
                andFlg = true;
            }

            // 評価部署
            DataRow[] drHyouka;
            SetSelectTable(Table, out drHyouka, Const.Def.DefTYPE_HYOUKA);

            if (drHyouka.Length > 0)
            {
                strWork = "";
                for (int i = 0; drHyouka.Length > i; i++)
                {
                    if (i != 0)
                    {
                        strWork += ",";
                    }
                    strWork += "'" + drHyouka[i]["ItemValue1"].ToString().Trim() + "'";
                }

                if (!andFlg)
                {
                    // 20170719 Add Start
                    if (paraCategoryCondition == Const.Def.DefTYPE_AND)
                    {
                        strWhere += "AND (";
                    }
                    else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type != null
                        && strKeyWordStartKbn == Const.Def.DefTYPE_FIRST) // 過去トラ検索結果　カテゴリの一番目
                    {
                        strWhere += "AND ((";
                        strKeyWordStartKbn = Const.Def.DefTYPE_NEXT;
                    }
                    else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type != null
                        && strKeyWordStartKbn == Const.Def.DefTYPE_NEXT) // 過去トラ検索結果　カテゴリの一番目ではない
                    {
                        strWhere += "OR (";
                    }
                    else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type == null) // 過去トラ検索
                    {
                        strWhere += "OR (";
                    }
                    // 20170719 Add End
                    andFlg = true;
                }
                else
                {
                    strWhere += "OR ";
                }

                strWhere += "BUSYO_HYOUKA1  IN (" + strWork + ") OR " + "\r\n";
                strWhere += "BUSYO_HYOUKA2  IN (" + strWork + ") OR " + "\r\n";
                strWhere += "BUSYO_HYOUKA3  IN (" + strWork + ") OR " + "\r\n";
                strWhere += "BUSYO_HYOUKA4  IN (" + strWork + ") OR " + "\r\n";
                strWhere += "BUSYO_HYOUKA5  IN (" + strWork + ") OR " + "\r\n";
                strWhere += "BUSYO_HYOUKA6  IN (" + strWork + ") OR " + "\r\n";
                strWhere += "BUSYO_HYOUKA7  IN (" + strWork + ") OR " + "\r\n";
                strWhere += "BUSYO_HYOUKA8  IN (" + strWork + ") OR " + "\r\n";
                strWhere += "BUSYO_HYOUKA9  IN (" + strWork + ") OR " + "\r\n";
                strWhere += "BUSYO_HYOUKA10 IN (" + strWork + ") " + "\r\n";
            }

            if (andFlg)
            {
                strWhere += ") ";
            }

            // 部品・部位
            DataRow[] drParts;
            SetSelectTable(Table, out drParts, Const.Def.DefTYPE_PARTS);
            String[] strPartsArrayData = null;

            for (int i = 0; drParts.Length > i ; i++)
            {
                if (i == 0)
                {
                    // 20170719 Add Start
                    if (paraCategoryCondition == Const.Def.DefTYPE_AND)
                    {
                        strWhere += "AND (";
                    }
                    else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type != null
                        && strKeyWordStartKbn == Const.Def.DefTYPE_FIRST) // 過去トラ検索結果　カテゴリの一番目
                    {
                        strWhere += "AND ((";
                        strKeyWordStartKbn = Const.Def.DefTYPE_NEXT;
                    }
                    else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type != null
                        && strKeyWordStartKbn == Const.Def.DefTYPE_NEXT) // 過去トラ検索結果　カテゴリの一番目ではない
                    {
                        strWhere += "OR (";
                    }
                    else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type == null) // 過去トラ検索
                    {
                        strWhere += "OR (";
                    }
                    // 20170719 Add End
                }
                else
                {
                    strWhere += " OR ";
                }

                strPartsArrayData = drParts[i]["ItemValue1"].ToString().Trim().Split(',');

                strWhere += "(SYSTEM_NO1 = '" + strPartsArrayData[0].ToString() + "'" + "\r\n";

                if (!(strPartsArrayData[1].ToString() == ""))
                {
                    strWhere += " AND BUHIN_NO1 = '" + strPartsArrayData[1].ToString() + "'" + "\r\n";
                }
                if (!(strPartsArrayData[2].ToString() == ""))
                {
                    strWhere += " AND KOBUHIN_NO1 = '" + strPartsArrayData[2].ToString() + "'" + "\r\n";
                }
                strWhere += ") OR (SYSTEM_NO2 = '" + strPartsArrayData[0].ToString() + "'" + "\r\n";
                if (!(strPartsArrayData[1].ToString() == ""))
                {
                    strWhere += " AND BUHIN_NO2 = '" + strPartsArrayData[1].ToString() + "'" + "\r\n";
                }
                if (!(strPartsArrayData[2].ToString() == ""))
                {
                    strWhere += " AND KOBUHIN_NO2 = '" + strPartsArrayData[2].ToString() + "'" + "\r\n";
                }
                strWhere += ") " + "\r\n";

                if (i == drParts.Length - 1)
                {
                    strWhere += ") " + "\r\n";
                }
            }

            // 開発符号
            DataRow[] drKaihatu;
            SetSelectTable(Table, out drKaihatu, Const.Def.DefTYPE_KAIHATU);

            if (drKaihatu.Length > 0)
            {
                SetWork(drKaihatu, out strWork);
                // 20170719 Add Start
                if (paraCategoryCondition == Const.Def.DefTYPE_AND)
                {
                    strWhere += "AND (";
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type != null
                    && strKeyWordStartKbn == Const.Def.DefTYPE_FIRST) // 過去トラ検索結果　カテゴリの一番目
                {
                    strWhere += "AND ((";
                    strKeyWordStartKbn = Const.Def.DefTYPE_NEXT;
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type != null
                    && strKeyWordStartKbn == Const.Def.DefTYPE_NEXT) // 過去トラ検索結果　カテゴリの一番目ではない
                {
                    strWhere += "OR (";
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type == null) // 過去トラ検索
                {
                    strWhere += "OR (";
                }
                // 20170719 Add End
                strWhere += "FUGO_NO1 IN (" + strWork + ") OR " + "\r\n";
                strWhere += "FUGO_NO2 IN (" + strWork + ") OR " + "\r\n";
                strWhere += "FUGO_NO3 IN (" + strWork + ") OR " + "\r\n";
                strWhere += "FUGO_NO4 IN (" + strWork + ") OR " + "\r\n";
                strWhere += "FUGO_NO5 IN (" + strWork + ") " + "\r\n";
                strWhere += ") " + "\r\n";
            }

            // 現象（分類）
            DataRow[] drGensyo;
            SetSelectTable(Table, out drGensyo, Const.Def.DefTYPE_GENSYO);

            if (drGensyo.Length > 0)
            {
                SetWork(drGensyo, out strWork);
                // 20170719 Add Start
                if (paraCategoryCondition == Const.Def.DefTYPE_AND)
                {
                    strWhere += "AND ";
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type != null
                    && strKeyWordStartKbn == Const.Def.DefTYPE_FIRST) // 過去トラ検索結果　カテゴリの一番目
                {
                    strWhere += "AND (";
                    strKeyWordStartKbn = Const.Def.DefTYPE_NEXT;
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type != null
                    && strKeyWordStartKbn == Const.Def.DefTYPE_NEXT) // 過去トラ検索結果　カテゴリの一番目ではない
                {
                    strWhere += "OR ";
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type == null) // 過去トラ検索
                {
                    strWhere += "OR ";
                }
                // 20170719 Add End
                strWhere += "BUNRUI_GENSYO_NO IN (" + strWork + ") " + "\r\n";
            }

            // 現象（制御系）
            DataRow[] drSGensyo;
            SetSelectTable(Table, out drSGensyo, Const.Def.DefTYPE_SGENSYO);

            if (drSGensyo.Length > 0)
            {
                SetWork(drSGensyo, out strWork);
                // 20170719 Add Start
                if (paraCategoryCondition == Const.Def.DefTYPE_AND)
                {
                    strWhere += "AND ";
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type != null
                    && strKeyWordStartKbn == Const.Def.DefTYPE_FIRST) // 過去トラ検索結果　カテゴリの一番目
                {
                    strWhere += "AND (";
                    strKeyWordStartKbn = Const.Def.DefTYPE_NEXT;
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type != null
                    && strKeyWordStartKbn == Const.Def.DefTYPE_NEXT) // 過去トラ検索結果　カテゴリの一番目ではない
                {
                    strWhere += "OR ";
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type == null) // 過去トラ検索
                {
                    strWhere += "OR ";
                }
                // 20170719 Add End
                strWhere += "SEIGYO_GENSYO_NO IN (" + strWork + ") " + "\r\n";
            }

            // 原因（分類）
            DataRow[] drGenin;
            SetSelectTable(Table, out drGenin, Const.Def.DefTYPE_GENIN);

            if (drGenin.Length > 0)
            {
                SetWork(drGenin, out strWork);
                // 20170719 Add Start
                if (paraCategoryCondition == Const.Def.DefTYPE_AND)
                {
                    strWhere += "AND ";
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type != null
                    && strKeyWordStartKbn == Const.Def.DefTYPE_FIRST) // 過去トラ検索結果　カテゴリの一番目
                {
                    strWhere += "AND (";
                    strKeyWordStartKbn = Const.Def.DefTYPE_NEXT;
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type != null
                    && strKeyWordStartKbn == Const.Def.DefTYPE_NEXT) // 過去トラ検索結果　カテゴリの一番目ではない
                {
                    strWhere += "OR ";
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type == null) // 過去トラ検索
                {
                    strWhere += "OR ";
                }
                // 20170719 Add End
                strWhere += "BUNRUI_CASE_NO IN (" + strWork + ") " + "\r\n";
            }

            // 車型特殊
            DataRow[] drSyakata;
            SetSelectTable(Table, out drSyakata, Const.Def.DefTYPE_SYAKATA);

            if (drSyakata.Length > 0)
            {
                SetWork(drSyakata, out strWork);
                // 20170719 Add Start
                if (paraCategoryCondition == Const.Def.DefTYPE_AND)
                {
                    strWhere += "AND ";
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type != null
                    && strKeyWordStartKbn == Const.Def.DefTYPE_FIRST) // 過去トラ検索結果　カテゴリの一番目
                {
                    strWhere += "AND (";
                    strKeyWordStartKbn = Const.Def.DefTYPE_NEXT;
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type != null
                    && strKeyWordStartKbn == Const.Def.DefTYPE_NEXT) // 過去トラ検索結果　カテゴリの一番目ではない
                {
                    strWhere += "OR ";
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type == null) // 過去トラ検索
                {
                    strWhere += "OR ";
                }
                // 20170719 Add End
                strWhere += "KATA_NO IN (" + strWork + ") " + "\r\n";
            }

            // 要因（制御系）
            DataRow[] drSYouin;
            SetSelectTable(Table, out drSYouin, Const.Def.DefTYPE_SYOUIN);

            if (drSYouin.Length > 0)
            {
                SetWork(drSYouin, out strWork);
                // 20170719 Add Start
                if (paraCategoryCondition == Const.Def.DefTYPE_AND)
                {
                    strWhere += "AND ";
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type != null
                    && strKeyWordStartKbn == Const.Def.DefTYPE_FIRST) // 過去トラ検索結果　カテゴリの一番目
                {
                    strWhere += "AND (";
                    strKeyWordStartKbn = Const.Def.DefTYPE_NEXT;
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type != null
                    && strKeyWordStartKbn == Const.Def.DefTYPE_NEXT) // 過去トラ検索結果　カテゴリの一番目ではない
                {
                    strWhere += "OR ";
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type == null) // 過去トラ検索
                {
                    strWhere += "OR ";
                }
                // 20170719 Add End
                strWhere += "SEIGYO_FACTOR_NO IN (" + strWork + ") " + "\r\n";
            }

            // EGTM形式
            DataRow[] drEgtm;
            SetSelectTable(Table, out drEgtm, Const.Def.DefTYPE_EGTM);

            if (drEgtm.Length > 0)
            {
                SetWork(drEgtm, out strWork);
                // 20170719 Add Start
                if (paraCategoryCondition == Const.Def.DefTYPE_AND)
                {
                    strWhere += "AND ";
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type != null
                    && strKeyWordStartKbn == Const.Def.DefTYPE_FIRST) // 過去トラ検索結果　カテゴリの一番目
                {
                    strWhere += "AND (";
                    strKeyWordStartKbn = Const.Def.DefTYPE_NEXT;
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type != null
                    && strKeyWordStartKbn == Const.Def.DefTYPE_NEXT) // 過去トラ検索結果　カテゴリの一番目ではない
                {
                    strWhere += "OR ";
                }
                else if (paraCategoryCondition == Const.Def.DefTYPE_OR && Type == null) // 過去トラ検索
                {
                    strWhere += "OR ";
                }
                // 20170719 Add End
                strWhere += "EGTM_NO IN (" + strWork + ") " + "\r\n";
            }
            if (Type != null && strKeyWordStartKbn == Const.Def.DefTYPE_NEXT) // 過去トラ検索結果　カテゴリの最後尾
            {
                strWhere += ") " + "\r\n";
            }

            //20170201 機能改善 END
            strWhere += "ORDER BY YMD_HENSYU DESC, BY_PU ASC, FOLLOW_NO ASC, FOLLOW_EDA DESC " + "\r\n";

            //20170724 Add Start strSqlとstrWhereを結合
            //カテゴリ検索でAND条件の場合、strWhereの"AND"以降の文字列を取得
            //カテゴリ検索でOR条件の場合、strWhereの"OR "以降の文字列を取得
            //TOP10検索、履歴キーワード検索の場合、paraCategoryConditionがnullになる
            //キーワード検索の場合、paraCategoryConditionが"0"になる
            if (paraCategoryCondition == Const.Def.DefTYPE_AND) //カテゴリ検索AND条件
            {
                strSql += strWhere.Substring(3); //"AND"以降の文字列取得
            }
            else if (paraCategoryCondition == Const.Def.DefTYPE_OR) //カテゴリ検索OR条件
            {
                strSql += strWhere.Substring(3); //"OR "以降の文字列取得　過去トラ検索結果のキーワード検索で"AND"のケース対応
            }
            else if (paraCategoryCondition == null) //TOP10検索、履歴キーワード検索
            {
                strSql += strWhere.Substring(3); // strWhereは"AND"で始まる
            }
            else if (paraCategoryCondition == "0") //キーワード検索
            {
                strSql += strWhere.Substring(3); // strWhereは"AND"で始まる
            }
            //20170724 Add End

            // DBオープン
            DataAccess.Common.SqlCommon dbBase = new DataAccess.Common.SqlCommon();

            SqlConnection connDb = dbBase.dbOpen();

            try
            {
                // SQL作成
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connDb;
                cmd.CommandText = strSql;
                cmd.Parameters.Clear();

                //20170201 機能改善 START
                CommonLogic bcom = new CommonLogic();
                DebugParameter dp = new DebugParameter();
                dp.Rank = 9;
                dp.FileName = System.IO.Path.GetFileName(this.GetType().Assembly.Location);
                dp.ClassName = this.GetType().FullName;
                dp.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                dp.Title = "過去トラ検索結果表示用SQL";
                dp.Content = strSql;
                bcom.DebugProcess(dp);
                //20170201 機能改善 END

                if (Type == Const.Def.DefTYPE_WORD || Type == Const.Def.DefTYPE_TOP10 )
                {
                    // 文字列検索
                    // TOP10検索
                    //cmd.Parameters.AddWithValue("@moji", "%" + strMoji + "%");

                    for (int i = 0; i < strArrayData.Length; i++)
                    {
                        cmd.Parameters.AddWithValue("@moji" + i.ToString(), "%" + strArrayData[i].ToString().Trim() + "%");
                    }
                }

                // コマンドを実行
                SqlDataReader reader = cmd.ExecuteReader();

                // SqlDataReader からデータを DataTable に読み込む
                result.Load(reader);

                reader.Close();

                // 20170724 Add Start
                if (result.Rows.Count > 0)
                {
                    // resultからRANK != 'X'のデータを除く
                    var resultItems = result.AsEnumerable()
                    .Where(x => x["RANK"].ToString() != "X").CopyToDataTable();

                    resultCopy = resultItems.Copy();
                }
                else if (result.Rows.Count == 0)
                {
                    resultCopy = result.Copy();
                }

                return resultCopy;
                // 20170724 Add End
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connDb.Close();
                connDb.Dispose();
                connDb = null;
            }
        }
        #endregion

        //20170201 機能改善 START
        #region カテゴリ名称取得
        /// <summary>
        /// カテゴリ名称取得
        /// </summary>
        /// <param name="strType">カテゴリ種類</param>
        /// <param name="strCode">カテゴリコード</param>
        /// <returns>結果データテーブル</returns>
        /// <remarks></remarks>
        public DataTable SelectCategoryName(String strType, String strCode)
        {
            DataTable work_t = new DataTable();

            // DBオープン
            DataAccess.Common.SqlCommon dbBase = new DataAccess.Common.SqlCommon();
            SqlConnection connDb = dbBase.dbOpen();

            try
            {
                // SQL作成
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connDb;
                // 20170304 START k-ohmatsuzawa EXCEL出力のカテゴリ検索条件の表示修正
                //switch (strType)
                //{
                //    case Const.Def.DefTYPE_PARTS:
                //        return work_t;
                //    case Const.Def.DefTYPE_KAIHATU:
                //        return work_t;
                //    case Const.Def.DefTYPE_GENSYO:
                //        return work_t;
                //    case Const.Def.DefTYPE_GENIN:
                //        return work_t;
                //    case Const.Def.DefTYPE_SYAKATA:
                //        return work_t;
                //    case Const.Def.DefTYPE_SGENSYO:
                //        return work_t;
                //    case Const.Def.DefTYPE_SYOUIN:
                //        return work_t;
                //    case Const.Def.DefTYPE_EGTM:
                //        cmd.CommandText = "SELECT ";
                //        cmd.CommandText += "EGTM_NAME ";
                //        cmd.CommandText += "FROM M_EGTM ";
                //        cmd.CommandText += "WHERE EGTM_NO  = @code ";
                //        cmd.Parameters.AddWithValue("@code", strCode);
                //        break;
                //    default:
                //        return work_t;
                //}
                switch (strType)
                {
                    case Const.Def.DefTYPE_PARTS:
                        switch (strCode.Length)
                        {
                            case 2:
                                cmd.CommandText = "SELECT ";
                                cmd.CommandText += "SYSTEM_NAME ";
                                cmd.CommandText += "FROM M_PARTS ";
                                cmd.CommandText += "WHERE BY_PU IN ('BY','PU') ";
                                cmd.CommandText += "AND SYSTEM_NO  = @code ";
                                cmd.Parameters.AddWithValue("@code", strCode);
                                break;
                            case 4:
                                cmd.CommandText = "SELECT ";
                                cmd.CommandText += "SYSTEM_NAME  + '／' + BUHIN_NAME ";
                                cmd.CommandText += "FROM M_PARTS ";
                                cmd.CommandText += "WHERE BY_PU IN ('BY','PU') ";
                                cmd.CommandText += "AND SYSTEM_NO  = @code ";
                                cmd.CommandText += "AND BUHIN_NO  = @code2 ";
                                cmd.Parameters.AddWithValue("@code", strCode.Substring(0,2));
                                cmd.Parameters.AddWithValue("@code2", strCode.Substring(2, 2));
                                break;
                            case 6:
                                cmd.CommandText = "SELECT ";
                                cmd.CommandText += "SYSTEM_NAME  + '／' + BUHIN_NAME + '／' + KOBUHIN_NAME ";
                                cmd.CommandText += "FROM M_PARTS ";
                                cmd.CommandText += "WHERE BY_PU IN ('BY','PU') ";
                                cmd.CommandText += "AND SYSTEM_NO  = @code ";
                                cmd.CommandText += "AND BUHIN_NO  = @code2 ";
                                cmd.CommandText += "AND KOBUHIN_NO  = @code3 ";
                                cmd.Parameters.AddWithValue("@code", strCode.Substring(0, 2));
                                cmd.Parameters.AddWithValue("@code2", strCode.Substring(2, 2));
                                cmd.Parameters.AddWithValue("@code3", strCode.Substring(4, 2));
                                break;
                        }
                        break;
                    case Const.Def.DefTYPE_KAIHATU:
                        cmd.CommandText = "SELECT ";
                        cmd.CommandText += "SYAKEI + '／' + KAIHATU_FUGO ";
                        cmd.CommandText += "FROM M_DEVELOPMENTSIGN ";
                        cmd.CommandText += "WHERE KAIHATU_ID  = @code ";
                        cmd.Parameters.AddWithValue("@code", strCode);
                        break;
                    case Const.Def.DefTYPE_GENSYO:
                    case Const.Def.DefTYPE_SGENSYO:
                        cmd.CommandText = "SELECT ";
                        cmd.CommandText += "GENSYO_NAME ";
                        cmd.CommandText += "FROM M_GENSYO ";
                        cmd.CommandText += "WHERE GENSYO_NO  = @code ";
                        cmd.Parameters.AddWithValue("@code", strCode);
                        break;
                    case Const.Def.DefTYPE_GENIN:
                        cmd.CommandText = "SELECT ";
                        cmd.CommandText += "CASE_NAME ";
                        cmd.CommandText += "FROM M_CASE ";
                        cmd.CommandText += "WHERE CASE_NO  = @code ";
                        cmd.Parameters.AddWithValue("@code", strCode);
                        break;
                    case Const.Def.DefTYPE_SYAKATA:
                        cmd.CommandText = "SELECT ";
                        cmd.CommandText += "KATA_NAME ";
                        cmd.CommandText += "FROM M_SYAKATA ";
                        cmd.CommandText += "WHERE KATA_NO  = @code ";
                        cmd.Parameters.AddWithValue("@code", strCode);
                        break;
                    case Const.Def.DefTYPE_SYOUIN:
                        cmd.CommandText = "SELECT ";
                        cmd.CommandText += "FACTOR_NAME ";
                        cmd.CommandText += "FROM M_FACTOR ";
                        cmd.CommandText += "WHERE FACTOR_NO  = @code ";
                        cmd.Parameters.AddWithValue("@code", strCode);
                        break;
                    case Const.Def.DefTYPE_EGTM:
                        cmd.CommandText = "SELECT ";
                        cmd.CommandText += "EGTM_NAME ";
                        cmd.CommandText += "FROM M_EGTM ";
                        cmd.CommandText += "WHERE EGTM_NO  = @code ";
                        cmd.Parameters.AddWithValue("@code", strCode);
                        break;
                    default:
                        return work_t;
                }
                // 20170304 END k-ohmatsuzawa

                // コマンドを実行
                SqlDataReader reader = cmd.ExecuteReader();

                // SqlDataReader からデータを DataTable に読み込む
                work_t.Load(reader);

                reader.Close();

                return work_t;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connDb.Close();
                connDb.Dispose();
                connDb = null;
            }
        }
        #endregion

        /// <summary>
        /// 検索タイプ別抽出
        /// </summary>
        /// <param name="dtTable">カテゴリデータテーブル</param>
        /// <param name="drSelectTable">検索タイプ別抽出カテゴリデータ</param>
        /// <param name="strType">検索タイプ</param>
        private void SetSelectTable(DataTable dtTable, out DataRow[] drSelectTable, string strType)
        {
            drSelectTable = (
                from row in dtTable.AsEnumerable()
                let columnID = row.Field<string>("TableType")
                where columnID == strType
                select row
            ).ToArray();
        }

        /// <summary>
        /// SQL抽出条件取得
        /// </summary>
        /// <param name="drSelectTable">検索タイプ別抽出カテゴリデータ</param>
        /// <param name="strWork">SQL抽出条件</param>
        private void SetWork(DataRow[] drSelectTable, out string strWork)
        {
            strWork = "";
            for (int i = 0; drSelectTable.Length > i; i++)
            {
                if (i != 0)
                {
                    strWork += ",";
                }
                String[] strArrayData  = drSelectTable[i]["ItemValue1"].ToString().Trim().Split(',');
                strWork += "'" + strArrayData[0].ToString().Trim() + "'";
            }
        }
        //20170201 機能改善 END

    }
}