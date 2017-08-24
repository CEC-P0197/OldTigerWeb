using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

/// <summary>
/// クエリパラメータ管理用クラスのコレクション。未使用。
/// </summary>
/// <remarks></remarks>
public class SqlParamCollection : List<SqlParam>
{

    /// <summary>
    /// パラメータを追加します。
    /// </summary>
    /// <param name="fieldName">フィールド名を指定します。</param>
    /// <param name="sqlDbType">データの型を指定します。</param>
    /// <param name="paramValue">フィールドの値を指定します。</param>
    public void Add(string fieldName, SqlDbType sqlDbType, object paramValue)
    {
        this.Add(new SqlParam(fieldName, sqlDbType, paramValue));
    }

}
