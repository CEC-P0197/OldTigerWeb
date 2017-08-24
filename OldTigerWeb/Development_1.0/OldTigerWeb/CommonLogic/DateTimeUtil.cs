using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

static class DateTimeUtil
{
    #region フィールド

    private static readonly int FiscalYearStartingMonth = 4;
    #endregion

    #region メソッド
    /// <summary>
    /// 該当年月日数返却
    /// </summary>
    /// <param name="dt">年月</param>
    /// <returns>年月の日数</returns>
    public static int DayInMonth(this DateTime dt)
    {
        return DateTime.DaysInMonth(dt.Year, dt.Month);
    }
    /// <summary>
    /// 月初日返却
    /// </summary>
    /// <param name="dt">年月</param>
    /// <returns>年月日（月初日）</returns>
    public static DateTime BeginOfMonth (this DateTime dt)
    {
        return dt.AddDays((dt.Day - 1) * -1);
    }
    /// <summary>
    /// 月末日返却
    /// </summary>
    /// <param name="dt">年月</param>
    /// <returns>年月日（月末日）</returns>
    public static DateTime EndOfMonth(this DateTime dt)
    {
        return new DateTime(dt.Year,dt.Month,DayInMonth(dt));
    }

    /// <summary>
    /// 日付取得（時刻落とし）
    /// </summary>
    /// <param name="dt">対象日時</param>
    /// <returns>日付</returns>
    public static DateTime StripDate(this DateTime dt)
    {
        return new DateTime(dt.Year,dt.Month,dt.Day);
    }
    /// <summary>
    /// 時刻取得（日付落とし）
    /// </summary>
    /// <param name="dt">対象日時</param>
    /// <param name="base_date">基準日</param>
    /// <returns>時刻</returns>
    public static DateTime StripTime(this DateTime dt,DateTime? base_date = null)
    {
        base_date = base_date ?? DateTime.MinValue;
        return new DateTime(base_date.Value.Year, base_date.Value.Month, base_date.Value.Day,
            dt.Hour,dt.Minute,dt.Second);
    }
    /// <summary>
    /// 該当日付の年度返却
    /// </summary>
    /// <param name="dt">該当日付</param>
    /// <param name="startingMonth">年度の開始月</param>
    /// <returns></returns>
    public static int FiscalYear(this DateTime dt,int? startingMonth = null)
    {
        return (dt.Month >= (startingMonth ?? FiscalYearStartingMonth)) ? dt.Year : dt.Year - 1;
    }
    /// <summary>
    /// 対象日における開始時刻取得
    /// </summary>
    /// <param name="dt">該当日付</param>
    /// <returns></returns>
    public static DateTime TimeStartDefault(this DateTime dt)
    {
        DateTime minValue = DateTime.MinValue;
        return  new DateTime(dt.Year,dt.Month,dt.Day,minValue.Hour,minValue.Minute,minValue.Second);
    }
    /// <summary>
    /// 対象日における終了時刻取得
    /// </summary>
    /// <param name="dt">該当日付</param>
    /// <returns></returns>
    public static DateTime TimeEndDefault(this DateTime dt)
    {
        DateTime maxValue = DateTime.MaxValue;
        return new DateTime(dt.Year, dt.Month, dt.Day, maxValue.Hour, maxValue.Minute, maxValue.Second);
    }

    #endregion
}
