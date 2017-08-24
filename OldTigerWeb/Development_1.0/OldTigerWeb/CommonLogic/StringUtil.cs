using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

static class StringUtil
{
    #region フィールド
    #endregion

    #region プロパティ
    /// <summary>
    /// DateTime変換（年月のみ指定）
    /// </summary>
    /// <param name="yyyyMM">年月</param>
    /// <returns>DateTime</returns>
    public static DateTime StringToDateTimeOfMonthsAndYear(this string yearAndMonth)
    {
        string yyyyMM = yearAndMonth.Replace("/", "");

        if (yyyyMM.Length >= 6)
        {
            DateTime dt;
            if (DateTime.TryParseExact(yyyyMM + "01", "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out dt))
                return dt;
        }

        return DateTime.MaxValue;
    }

    public static string CombineFromStringSingleQuotes(this List<string> list, ref string combineString)
    {
        if (combineString != null)
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (combineString != "")
                        combineString += ",";

                    combineString += list[i].Trim().GrantSingleQuotes();
                }

                return combineString;
            }

        return "";
    }
    public static string CombineFromString(this List<string> list, ref string combineString)
    {
        if (list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != "")
                {
                    if (combineString != "")
                    {
                        combineString += ",";
                    }

                    combineString += list[i].Trim();
                }
            }

            return combineString;


        }


        return "";
    }

    public static string GrantSingleQuotes(this string character)
    {
        return "'" + character + "'";
    }
    public static string GrantDoubleQuotes(this string character)
    {
        return '"' + character + '"';
    }

    public static string GrantBrackets(this string character)
    {
        return "(" + character + ")";
    }

    /// <summary>
    /// 日付データの文字列変換
    /// </summary>
    /// <param name="obj">日付データ</param>
    /// <returns></returns>
    public static string FormatDateTimeToString(this string obj)
    {
        DateTime dt;

        if (DateTime.TryParse(obj, out dt))
        {
            return dt.ToString(Def.DATETIME_FORMAT);
        }
        else
        {
            return "";
        }
    }

    #endregion
}
