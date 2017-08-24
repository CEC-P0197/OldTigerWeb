using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

/// <summary>
/// クエリパラメータ管理用クラス。未使用。
/// </summary>
/// <remarks></remarks>
public class SqlParam
{

    /// <summary>
    /// フィールドタイプ
    /// </summary>

    private SqlDbType _sqlDbType = new SqlDbType();
    /// <summary>
    /// フィールドサイズ
    /// </summary>

    private int _size = 0;
    /// <summary>
    /// フィールド名
    /// </summary>

    private string _paramName = string.Empty;
    /// <summary>
    /// 値
    /// </summary>

    private object _paramValue = string.Empty;
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="fieldName">フィールド名を指定します。</param>
    /// <param name="sqlDbType">データの型を指定します。</param>
    /// <param name="paramValue">データを指定します。</param>
    public SqlParam(string fieldName, SqlDbType sqlDbType, object paramValue)
    {
        _paramName = string.Format("@{0}", fieldName);
        _sqlDbType = sqlDbType;
        _size = 4000;
        _paramValue = paramValue;
    }

    /// <summary>
    /// フィールドタイプを取得、または設定します。
    /// </summary>
    public SqlDbType SqlDbType
    {
        get { return _sqlDbType; }
        set { _sqlDbType = value; }
    }

    /// <summary>
    /// データベースサイズを取得、または設定します。
    /// </summary>
    public int Size
    {
        get { return _size; }
        set { _size = value; }
    }

    /// <summary>
    /// フィールド名を取得、または設定します。
    /// </summary>
    public string ParamName
    {
        get { return _paramName; }
        set { _paramName = value; }
    }

    /// <summary>
    /// 値を取得、または設定します。
    /// </summary>
    public object ParamValue
    {
        get { return _paramValue; }
        set { _paramValue = value; }
    }

}
