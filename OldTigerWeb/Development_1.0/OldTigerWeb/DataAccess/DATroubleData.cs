using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using OldTigerWeb.DataAccess;

namespace OldTigerWeb.BuisinessLogic
{
    public class DATroubleData:IDATroubleData
    {

        #region メソッド

        /// <summary>
        /// 過去トラ情報取得
        /// </summary>
        /// <param name="keyword">検索文字　カテゴリ検索の場合はnull</param>
        /// <param name="keywordCondition">キーワード検索用 And・Or検索条件  1：And、2：Or</param>
        /// <param name="cotegory">カテゴリデータテーブル（カテゴリ検索用）</param>
        /// <param name="cotegoryCondition">カテゴリ検索用 And・Or検索条件  1：And、2：Or</param> // 20170719 Add
        /// <param name="Mode">モード：1:画面、2:Excel</param>
        /// <returns>取得結果情報</returns>
        public DataTable getTroubleList(String keyword, String keywordCondition,
           DataTable cotegory, String cotegoryCondition, String mode = Def.DefMODE_DISP) 
        {
            StringBuilder sb = new StringBuilder();
            List<string> whereList= new List<string>();
            StringBuilder sbWhere = new StringBuilder();

            DataTable result = new DataTable();
            DataTable resultCopy = new DataTable(); // 20170721 Add

            sb.AppendLine("SELECT ");

            if (mode == Def.DefMODE_DISP)
            {   // 画面情報
                sb.AppendLine("CONVERT(varchar,ROW_NUMBER() OVER(ORDER BY YMD_HENSYU DESC, BY_PU ASC, FOLLOW_NO ASC, FOLLOW_EDA DESC)) AS ROWID, ");
                sb.AppendLine("RANK + '<br>' + SAIHATU + '<br>' + RSC + '<br>' + ");
                sb.AppendLine("CASE WHEN RTRIM(SYUMU_SEIZO) = '○' AND RTRIM(SYUMU_GAISEI) != '○' THEN '製造' ");
                sb.AppendLine("    ELSE CASE WHEN RTRIM(SYUMU_SEIZO) != '○' AND RTRIM(SYUMU_GAISEI) = '○' THEN '外製' ");
                sb.AppendLine("        ELSE '設計' ");
                sb.AppendLine("    END ");
                sb.AppendLine("END AS SYUMU, RANK, ");
                sb.AppendLine("SYSTEM_NO, FOLLOW_INFO, KOUMOKU_KANRI_NO, KOUMOKU, ");
                sb.AppendLine("FUGO_NAME1 + '<br>' + FUGO_NAME2 + '<br>' + FUGO_NAME3 + '<br>' + FUGO_NAME4 AS FUGO_NAME, ");
                sb.AppendLine("GENSYO_NAIYO, JYOUKYO, GENIN, TAISAKU, KAIHATU_MIHAKKEN_RIYU, SQB_KANTEN, SAIHATU_SEKKEI, SAIHATU_HYOUKA, ");
                sb.AppendLine("BUSYO_SEKKEI1 + '<br>' + BUSYO_SEKKEI2 + '<br>' + BUSYO_HYOUKA1  + '<br>' + BUSYO_HYOUKA2 AS BUSYO_CODE, ");
                sb.AppendLine("RTRIM(SIRYOU_NO1) + '<br>' +  RTRIM(SIRYOU_NO2)");
                sb.AppendLine("AS SIRYOU_NO, LINK_FOLDER_PATH,");
                sb.AppendLine("BY_PU, FUGO_NO1, TRA.INSERT_YMD, YMD_HENSYU, INPUT_ROW, SEQ ");
            }
            else
            {   // Excel情報
                sb.AppendLine("CONVERT(varchar,ROW_NUMBER() OVER(ORDER BY YMD_HENSYU DESC, BY_PU ASC, FOLLOW_NO ASC, FOLLOW_EDA DESC)) AS ROWID, ");
                sb.AppendLine("RANK, SAIHATU, RSC, ");
                sb.AppendLine("CASE WHEN RTRIM(SYUMU_SEIZO) = '○' AND RTRIM(SYUMU_GAISEI) != '○' THEN '製造' ");
                sb.AppendLine("    ELSE CASE WHEN RTRIM(SYUMU_SEIZO) != '○' AND RTRIM(SYUMU_GAISEI) = '○' THEN '外製' ");
                sb.AppendLine("        ELSE '設計' ");
                sb.AppendLine("    END ");
                sb.AppendLine("END AS SYUMU, ");
                sb.AppendLine("SYSTEM_NO, FOLLOW_INFO, KOUMOKU_KANRI_NO, KOUMOKU, ");
                sb.AppendLine("FUGO_NAME1, FUGO_NAME2, FUGO_NAME3, FUGO_NAME4, FUGO_NAME5, ");
                sb.AppendLine("GENSYO_NAIYO, JYOUKYO, GENIN, TAISAKU, KAIHATU_MIHAKKEN_RIYU, SQB_KANTEN, ");
                sb.AppendLine("SAIHATU_SEKKEI, SAIHATU_HYOUKA, ");
                sb.AppendLine("BUSYO_SEKKEI1, BUSYO_SEKKEI2, BUSYO_SEKKEI3, BUSYO_SEKKEI4, BUSYO_SEKKEI5, ");
                sb.AppendLine("BUSYO_SEKKEI6, BUSYO_SEKKEI7, BUSYO_SEKKEI8, BUSYO_SEKKEI9, BUSYO_SEKKEI10, ");
                sb.AppendLine("BUSYO_HYOUKA1, BUSYO_HYOUKA2, BUSYO_HYOUKA3, BUSYO_HYOUKA4, BUSYO_HYOUKA5, ");
                sb.AppendLine("BUSYO_HYOUKA6, BUSYO_HYOUKA7, BUSYO_HYOUKA8, BUSYO_HYOUKA9, BUSYO_HYOUKA10, ");
                sb.AppendLine("SIRYOU_NO1, SIRYOU_NO2, SIRYOU_NO3, SIRYOU_NO4, SIRYOU_NO5, KANREN_KANRI_NO, ");
                sb.AppendLine("BY_PU, FUGO_NO1, TRA.INSERT_YMD, YMD_HENSYU, INPUT_ROW, SEQ ");
            }
            sb.AppendLine("FROM T_TROUBLE_DATA TRA ");
            sb.AppendLine("LEFT JOIN M_DEVELOPMENTSIGN DEVSIGN ");
            sb.AppendLine("ON TRA.FUGO_NAME1 = DEVSIGN.KAIHATU_FUGO ");
            //strSql += "WHERE RANK <> 'X' " + "\r\n";

            // タイプにより取得条件を組立てる

            // キーワード検索
            if (keywordCondition != null)
            {
                whereList.Add(createParameterForKeyword(keyword,keywordCondition).ToString());
            }

            // カテゴリ検索
            if (cotegoryCondition != null)
            {
                whereList.Add(createParameterForCotegory(cotegory, cotegoryCondition).ToString());
            }

            if(whereList.Count > 0)
            {
                sb.AppendLine("WHERE @where ");
                for (int i=0;i<whereList.Count;i++)
                {
                    if (i != 0)
                    {
                        sbWhere.AppendLine(" AND ");
                    }
                    sbWhere.AppendLine(whereList[i]);
                }
                sb = sb.Replace("@where",sbWhere.ToString());
            }

            // SQL実行
            return  new SqlBridging().ExecuteReader(sb.ToString());
        }

        /// <summary>
        /// キーワード検索条件SQL作成
        /// </summary>
        /// <param name="keyword">検索キーワード</param>
        /// <param name="condition">検索区分(AND："1"、OR："2")</param>
        /// <returns>キーワード検索条件SQL</returns>
        public StringBuilder createParameterForKeyword(string keyword,string condition = Def.DefTYPE_AND)
        {
            StringBuilder strWhereKeyword = new StringBuilder();
            List<string> parameterList = new List<string>();
            StringBuilder parameter = new StringBuilder();

            // 半角スペースでスプリット
            string[] strArrayData = keyword.Trim().Split(' ');
            for (int i = 0; i < strArrayData.Length; i++)
            {
                if (i > 3)
                {
                    break;
                }
                if (i != 0)
                {
                    strWhereKeyword.AppendLine("OR");
                }

                string strWork = "@moji";

                parameter.AppendLine("KOUMOKU_KANRI_NO LIKE " + strWork + " OR ");
                parameter.AppendLine("BY_PU LIKE " + strWork + " OR ");

                for (int j = 1; j < 2; j++)
                {
                    parameter.AppendLine("SYSTEM_NAME" + j.ToString() + " LIKE " + strWork + " OR ");
                    parameter.AppendLine("BUHIN_NAME" + j.ToString() + " LIKE " + strWork + " OR ");
                    parameter.AppendLine("KOBUHIN_NAME" + j.ToString() + " LIKE " + strWork + " OR ");
                }
                parameter.AppendLine("BUNRUI_GENSYO_NAME LIKE " + strWork + " OR ");
                parameter.AppendLine("BUNRUI_CASE_NAME LIKE " + strWork + " OR ");
                parameter.AppendLine("SEIGYO_UNIT_NAME LIKE " + strWork + " OR ");
                parameter.AppendLine("SEIGYO_GENSYO_NAME LIKE " + strWork + " OR ");
                parameter.AppendLine("SEIGYO_FACTOR_NAME LIKE " + strWork + " OR ");
                parameter.AppendLine("KATA_NAME LIKE " + strWork + " OR ");
                parameter.AppendLine("EGTM_NAME LIKE " + strWork + " OR ");
                parameter.AppendLine("HAIKI_NAME LIKE " + strWork + " OR ");
                parameter.AppendLine("KOUMOKU LIKE " + strWork + " OR ");
                parameter.AppendLine("GENSYO_NAIYO LIKE " + strWork + " OR ");
                parameter.AppendLine("JYOUKYO LIKE " + strWork + " OR ");
                parameter.AppendLine("GENIN LIKE " + strWork + " OR ");
                parameter.AppendLine("TAISAKU LIKE " + strWork + " OR ");
                parameter.AppendLine("KAIHATU_MIHAKKEN_RIYU LIKE " + strWork + " OR ");
                parameter.AppendLine("SAIHATU_SEKKEI LIKE " + strWork + " OR ");
                parameter.AppendLine("SAIHATU_HYOUKA LIKE " + strWork + " OR ");
                parameter.AppendLine("SQB_KANTEN LIKE " + strWork + " OR ");

                for (int j = 1; j <= 10; j++)
                {
                    parameter.AppendLine("BUSYO_SEKKEI" + j.ToString() + " LIKE " + strWork + " OR ");
                }
                for (int j = 1; j <= 10; j++)
                {
                    parameter.AppendLine("BUSYO_HYOUKA" + j.ToString() + " LIKE " + strWork + " OR ");
                }
                for (int j = 1; j <=5 ; j++)
                {
                    parameter.AppendLine("FUGO_NAME" + j.ToString() + " LIKE " + strWork + " OR ");
                }
                for (int j = 1; j <= 3; j++)
                {
                    parameter.AppendLine("BLKNO" + j.ToString() + " LIKE " + strWork + " OR ");
                }
                for (int j = 1; j <= 5; j++)
                {
                    parameter.AppendLine("BUHIN_BANGO" + j.ToString() + " LIKE " + strWork + " OR ");
                }

                parameter.AppendLine("KANREN_KANRI_NO LIKE " + strWork + " OR ");
                parameter.AppendLine("KEYWORD LIKE " + strWork + " OR ");
                parameter.AppendLine("JYUYO_HOUKI LIKE " + strWork + " ");
                parameter = parameter.Replace("@moji", strArrayData[i].GrantSingleQuotes());

                parameterList.Add(parameter.ToString());
            }

            for(int i = 0; i < parameterList.Count; i++)
            {
                if(i != 0)
                {
                    strWhereKeyword.AppendLine(" @condition ");
                    if (condition == Def.DefTYPE_AND)
                    {
                        strWhereKeyword = strWhereKeyword.Replace("@condition", " AND ");
                    }
                    else
                    {
                        strWhereKeyword = strWhereKeyword.Replace("@condition", " OR ");
                    }
                }
                strWhereKeyword.AppendLine(parameterList[i].GrantBrackets());
            }

            return strWhereKeyword;
        }

        /// <summary>
        /// カテゴリ検索条件SQL作成
        /// </summary>
        /// <param name="cotegoryTable">検索カテゴリ</param>
        /// <param name="condition">検索区分(AND："1"、OR："2")</param>
        /// <returns>カテゴリ検索条件SQL</returns>
        public StringBuilder createParameterForCotegory(DataTable cotegoryTable, string condition = Def.DefTYPE_AND)
        {

            // カテゴリ検索
            string field;
            List<string> fieldList;
            StringBuilder parameter;  // 各項目編集用変数
            List<string> parmList = new List<string>(); // 検索条件（列項目別）

            // 設計部署・評価部署
            DataRow[] drSekkei;
            DataRow[] drHyoka;
            fieldList = new List<string>();
            parameter = new StringBuilder();
            // 該当検索条件抽出(linq使用)
            drSekkei = cotegoryTable.AsEnumerable()
                .Where(row => row["TableType"].ToString() == Def.DefTYPE_BUSYO).ToArray();

            if (drSekkei.Length > 0)
            {
                field = "";
                
                // パラメータ作成（SQLのINに入るテキスト）
                drSekkei.Select(row => row["ItemValue1"].ToString()).ToList().CombineFromString(ref field);

                // SQL作成
                for (int i = 1; i <= 10; i++)
                {
                    fieldList.Add("BUSYO_SEKKEI" + i.ToString() + " IN (" + field + ")");
                }
            }

            // 該当検索条件抽出(linq使用)
            drHyoka = cotegoryTable.AsEnumerable()
                .Where(row => row["TableType"].ToString() == Def.DefTYPE_HYOUKA).ToArray();

            if (drHyoka.Length > 0)
            {
                field = "";
                // パラメータ作成（SQLのINに入るテキスト）
                drHyoka.Select(row => row["ItemValue1"].ToString()).ToList().CombineFromString(ref field);

                // SQL作成
                for (int i = 1; i <= 10; i++)
                {
                    fieldList.Add("BUSYO_HYOUKA" + i.ToString() + " IN (" + field + ")");
                }
            }
            // 検索条件にセット
            for (int i = 1; i <= fieldList.Count; i++)
            {
                if (i != 1)
                {
                    parameter.AppendLine(" OR ");
                }

                parameter.AppendLine(fieldList[i]);
            }
            if (parameter.Length > 0)
            {
                parmList.Add(parameter.ToString());
            }

            // 部品・部位
            DataRow[] drParts;
            fieldList = new List<string>();
            parameter = new StringBuilder();
            // 該当検索条件抽出(linq使用)
            drParts = cotegoryTable.AsEnumerable()
                .Where(row => row["TableType"].ToString() == Def.DefTYPE_PARTS).ToArray();

            if (drParts.Length > 0)
            {
                // パラメータ作成（SQLのINに入るテキスト）
                fieldList = drParts.Select(row => row["ItemValue1"].ToString()).ToList();

                for (int i = 0; i < fieldList.Count; i++)
                {
                    // システムNo／部品No／子部品Noでリスト作成
                    var list = fieldList[i].Split(',');


                    for (int j = 1; j <= 2; j++)
                    {

                        StringBuilder child = new StringBuilder();
                        child.AppendLine("SYSTEM_NO" + j.ToString() + " = " + list[0].GrantSingleQuotes());
                        if (!(list[1] == ""))
                        {
                            child.AppendLine(" AND ");
                            child.AppendLine("BUHIN_NO" + j.ToString() + " = " + list[1].GrantSingleQuotes());
                        }
                        if (!(list[2] == ""))
                        {
                            child.AppendLine(" AND ");
                            child.AppendLine("KOBUHIN_NO" + j.ToString() + " = " + list[2].GrantSingleQuotes());
                        }
                        parameter.AppendLine(child.ToString().GrantSingleQuotes().GrantBrackets());
                    }
                    parmList.Add(parameter.ToString());
                }
            }

            // 開発符号
            DataRow[] drKaihatsu;
            fieldList = new List<string>();
            parameter = new StringBuilder();
            // 該当検索条件抽出(linq使用)
            drKaihatsu = cotegoryTable.AsEnumerable()
                .Where(row => row["TableType"].ToString() == Def.DefTYPE_KAIHATU).ToArray();

            if (drKaihatsu.Length > 0)
            {
                field = "";
                // パラメータ作成（SQLのINに入るテキスト）
                drKaihatsu.Select(row => row["ItemValue1"].ToString()).ToList().CombineFromString(ref field);
                // SQL作成
                for (int i = 1; i <= 5; i++)
                {
                    fieldList.Add("FUGO_NO" + i.ToString() + " IN (" + field + ")");
                }
            }
            // 検索条件にセット
            for (int i = 1; i <= fieldList.Count; i++)
            {
                if (i != 1)
                {
                    parameter.AppendLine(" OR ");
                }
                parameter.AppendLine(fieldList[i]);
            }
            if (parameter.Length > 0)
            {
                parmList.Add(parameter.ToString());
            }

            // 現象（分類）
            DataRow[] drGensyo;
            fieldList = new List<string>();
            parameter = new StringBuilder();
            // 該当検索条件抽出(linq使用)
            drGensyo = cotegoryTable.AsEnumerable()
                .Where(row => row["TableType"].ToString() == Def.DefTYPE_GENSYO).ToArray();

            if (drGensyo.Length > 0)
            {
                field = "";
                // パラメータ作成（SQLのINに入るテキスト）
                drGensyo.Select(row => row["ItemValue1"].ToString()).ToList().CombineFromString(ref field);
                // SQL作成
                for (int i = 1; i <= 1; i++)
                {
                    fieldList.Add("BUNRUI_GENSYO_NO" + i.ToString() + " IN (" + field + ")");
                }
            }
            // 検索条件にセット
            for (int i = 1; i <= fieldList.Count; i++)
            {
                if (i != 1)
                {
                    parameter.AppendLine(" OR ");
                }
                parameter.AppendLine(fieldList[i]);
            }
            if (parameter.Length > 0)
            {
                parmList.Add(parameter.ToString());
            }

            // 現象（制御系）
            DataRow[] drSGensyo;
            fieldList = new List<string>();
            parameter = new StringBuilder();
            // 該当検索条件抽出(linq使用)
            drSGensyo = cotegoryTable.AsEnumerable()
                .Where(row => row["TableType"].ToString() == Def.DefTYPE_SGENSYO).ToArray();

            if (drSGensyo.Length > 0)
            {
                field = "";
                // パラメータ作成（SQLのINに入るテキスト）
                drSGensyo.Select(row => row["ItemValue1"].ToString()).ToList().CombineFromString(ref field);
                // SQL作成
                for (int i = 1; i <= 1; i++)
                {
                    fieldList.Add("SEIGYO_GENSYO_NO " + i.ToString() + " IN (" + field + ")");
                }
            }
            // 検索条件にセット
            for (int i = 1; i <= fieldList.Count; i++)
            {
                if (i != 1)
                {
                    parameter.AppendLine(" OR ");
                }
                parameter.AppendLine(fieldList[i]);
            }
            if (parameter.Length > 0)
            {
                parmList.Add(parameter.ToString());
            }


            // 原因（分類）
            DataRow[] drGenin;
            fieldList = new List<string>();
            parameter = new StringBuilder();
            // 該当検索条件抽出(linq使用)
            drGenin = cotegoryTable.AsEnumerable()
                .Where(row => row["TableType"].ToString() == Def.DefTYPE_GENIN).ToArray();

            if (drGenin.Length > 0)
            {
                field = "";
                // パラメータ作成（SQLのINに入るテキスト）
                drGenin.Select(row => row["ItemValue1"].ToString()).ToList().CombineFromString(ref field);
                // SQL作成
                for (int i = 1; i <= 1; i++)
                {
                    fieldList.Add("BUNRUI_CASE_NO" + i.ToString() + " IN (" + field + ")");
                }
            }
            // 検索条件にセット
            for (int i = 1; i <= fieldList.Count; i++)
            {
                if (i != 1)
                {
                    parameter.AppendLine(" OR ");
                }
                parameter.AppendLine(fieldList[i]);
            }
            if (parameter.Length > 0)
            {
                parmList.Add(parameter.ToString());
            }

            // 車型特殊
            DataRow[] drSyakata;
            fieldList = new List<string>();
            parameter = new StringBuilder();
            // 該当検索条件抽出(linq使用)
            drSyakata = cotegoryTable.AsEnumerable()
                .Where(row => row["TableType"].ToString() == Def.DefTYPE_SYAKATA).ToArray();

            if (drSyakata.Length > 0)
            {
                field = "";
                // パラメータ作成（SQLのINに入るテキスト）
                drSyakata.Select(row => row["ItemValue1"].ToString()).ToList().CombineFromString(ref field);
                // SQL作成
                for (int i = 1; i <= 1; i++)
                {
                    fieldList.Add("KATA_NO" + i.ToString() + " IN (" + field + ")");
                }
            }
            // 検索条件にセット
            for (int i = 1; i <= fieldList.Count; i++)
            {
                if (i != 1)
                {
                    parameter.AppendLine(" OR ");
                }
                parameter.AppendLine(fieldList[i]);
            }
            if (parameter.Length > 0)
            {
                parmList.Add(parameter.ToString());
            }

            // 要因（制御系）
            DataRow[] drSYouin;
            fieldList = new List<string>();
            parameter = new StringBuilder();
            // 該当検索条件抽出(linq使用)
            drSYouin = cotegoryTable.AsEnumerable()
                .Where(row => row["TableType"].ToString() == Def.DefTYPE_SYOUIN).ToArray();

            if (drSYouin.Length > 0)
            {
                field = "";
                // パラメータ作成（SQLのINに入るテキスト）
                drSYouin.Select(row => row["ItemValue1"].ToString()).ToList().CombineFromString(ref field);
                // SQL作成
                for (int i = 1; i <= 1; i++)
                {
                    fieldList.Add("SEIGYO_FACTOR_NO" + i.ToString() + " IN (" + field + ")");
                }
            }
            // 検索条件にセット
            for (int i = 1; i <= fieldList.Count; i++)
            {
                if (i != 1)
                {
                    parameter.AppendLine(" OR ");
                }
                parameter.AppendLine(fieldList[i]);
            }
            if (parameter.Length > 0)
            {
                parmList.Add(parameter.ToString());
            }

            // EGTM形式
            DataRow[] drEgtm;
            fieldList = new List<string>();
            parameter = new StringBuilder();
            // 該当検索条件抽出(linq使用)
            drEgtm = cotegoryTable.AsEnumerable()
                .Where(row => row["TableType"].ToString() == Def.DefTYPE_EGTM).ToArray();

            if (drEgtm.Length > 0)
            {
                field = "";
                // パラメータ作成（SQLのINに入るテキスト）
                drEgtm.Select(row => row["ItemValue1"].ToString()).ToList().CombineFromString(ref field);
                // SQL作成
                for (int i = 1; i <= 1; i++)
                {
                    fieldList.Add("EGTM_NO" + i.ToString() + " IN (" + field + ")");
                }
            }
            // 検索条件にセット
            for (int i = 1; i <= fieldList.Count; i++)
            {
                if (i != 1)
                {
                    parameter.AppendLine(" OR ");
                }
                parameter.AppendLine(fieldList[i]);
            }
            if (parameter.Length > 0)
            {
                parmList.Add(parameter.ToString());
            }

            StringBuilder cotegoryParameter = new StringBuilder();
            // カテゴリ検索条件マージ
            if (parmList.Count > 0)
            {
                for (int i = 0; i < parmList.Count; i++)
                {
                    if (i != 1)
                    {
                        parameter.AppendLine(" OR ");
                    }
                    cotegoryParameter.AppendLine(parmList[i].GrantBrackets());
                }
            }
            return cotegoryParameter;
        }

        /// <summary>
        /// カテゴリ名称取得
        /// </summary>
        /// <param name="strType">カテゴリ種類</param>
        /// <param name="strCode">カテゴリコード</param>
        /// <returns>結果データテーブル</returns>
        /// <remarks></remarks>
        public DataTable selectCotegoryName(String strType, String strCode)
        {
            try
            {


                StringBuilder sb = new StringBuilder();
                StringBuilder select = new StringBuilder();
                StringBuilder table = new StringBuilder();
                StringBuilder where = new StringBuilder();

                sb.AppendLine("SELECT ");
                sb.AppendLine("@select");
                sb.AppendLine("FROM @table ");
                sb.AppendLine("WHERE ");
                sb.AppendLine("@where");

                switch (strType)
                {
                    case Def.DefTYPE_PARTS:
                        switch (strCode.Length)
                        {
                            case 2:
                                select.AppendLine("SYSTEM_NAME ");
                                table.AppendLine("M_PARTS ");
                                where.AppendLine("BY_PU IN ('BY','PU') ");
                                where.AppendLine("AND SYSTEM_NO  = '@code1' ");
                                where = where.Replace("@code1", strCode);
                                break;
                            case 4:
                                select.AppendLine("SYSTEM_NAME + '／'+ BUHIN_NAME ");
                                table.AppendLine("M_PARTS ");
                                where.AppendLine("BY_PU IN ('BY','PU') ");
                                where.AppendLine("AND SYSTEM_NO  = '@code1' ");
                                where.AppendLine("AND BUHIN_NO  = '@code2' ");
                                where = where.Replace("@code1", strCode.Substring(0, 2));
                                where = where.Replace("@code2", strCode.Substring(2, 2));
                                break;
                            case 6:
                                select.AppendLine("SYSTEM_NAME + '／'+ BUHIN_NAME + '／'+ KOBUHIN_NAME ");
                                table.AppendLine("M_PARTS ");
                                where.AppendLine("BY_PU IN ('BY','PU') ");
                                where.AppendLine("AND SYSTEM_NO  = '@code1' ");
                                where.AppendLine("AND BUHIN_NO  = '@code2' ");
                                where.AppendLine("AND KOBUHIN_NO  = '@code3' ");
                                where = where.Replace("@code1", strCode.Substring(0, 2));
                                where = where.Replace("@code2", strCode.Substring(2, 2));
                                where = where.Replace("@code3", strCode.Substring(4, 2));
                                break;
                        }
                        break;
                    case Def.DefTYPE_KAIHATU:
                        select.AppendLine("SYAKEI + '／' + KAIHATU_FUGO ");
                        table.AppendLine("M_DEVELOPMENTSIGN ");
                        where.AppendLine("KAIHATU_ID  = '@code1' ");
                        where = where.Replace("@code1", strCode);
                        break;
                    case Def.DefTYPE_GENSYO:
                    case Def.DefTYPE_SGENSYO:
                        select.AppendLine("GENSYO_NAME ");
                        table.AppendLine("M_GENSYO ");
                        where.AppendLine("GENSYO_NO  = '@code1' ");
                        where = where.Replace("@code1", strCode);
                        break;
                    case Def.DefTYPE_GENIN:
                        select.AppendLine("CASE_NAME ");
                        table.AppendLine("M_CASE ");
                        where.AppendLine("CASE_NO  = '@code1' ");
                        where = where.Replace("@code1", strCode);
                        break;
                    case Def.DefTYPE_SYAKATA:
                        select.AppendLine("KATA_NAME ");
                        table.AppendLine("M_SYAKATA ");
                        where.AppendLine("KATA_NO  = '@code1' ");
                        where = where.Replace("@code1", strCode);
                        break;
                    case Def.DefTYPE_SYOUIN:
                        select.AppendLine("FACTOR_NAME ");
                        table.AppendLine("M_FACTOR");
                        where.AppendLine("FACTOR_NO  = '@code1' ");
                        where = where.Replace("@code1", strCode);
                        break;
                    case Def.DefTYPE_EGTM:
                        select.AppendLine("EGTM_NAME ");
                        table.AppendLine("M_EGTM ");
                        where.AppendLine("EGTM_NO  = '@code1' ");
                        where = where.Replace("@code1", strCode);
                        break;
                    default:
                        return new DataTable();
                }
                sb = sb
                    .Replace("@select", select.ToString())
                    .Replace("@table", table.ToString())
                    .Replace("@where", where.ToString());

                return new SqlBridging().ExecuteReader(sb.ToString());
            }
            catch
            {
                return new DataTable();
            }

        }

        #endregion

    }
}
