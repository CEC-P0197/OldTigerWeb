using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class MessageConst
{
    // 
    //************************************************
    // アラートメッセージ
    //************************************************
    /// <summary>
    /// 過去トラ観たか登録成功
    /// </summary>
    public const string MITAKA_POST_SUCCESS = "登録完了";
    /// <summary>
    /// 過去トラ観たか登録失敗
    /// </summary>
    public const string MITAKA_POST_FAILURE = "登録失敗";
    /// <summary>
    /// 過去トラ観たか削除成功
    /// </summary>
    public const string MITAKA_DELETE_SUCCESS = "削除完了";
    /// <summary>
    /// 過去トラ観たか削除失敗
    /// </summary>
    public const string MITAKA_DELETE_FAILURE = "削除失敗";
    /// <summary>
    /// 過去トラ観たか回答依頼成功
    /// </summary>
    public const string MITAKA_REQUEST_SUCCESS = "回答依頼完了";
    /// <summary>
    /// 過去トラ観たか回答依頼失敗
    /// </summary>
    public const string MITAKA_REQUEST_FAILURE = "回答依頼失敗";
    /// <summary>
    /// 過去トラ観たか回答依頼メールアドレス未取得エラー
    /// </summary>
    public const string MITAKA_REQUEST_FAILURE_NOT_MAILADDRESS = "ユーザーマスタからメールアドレスが取得できないメンバーがいます。展開先を確認してください";
    /// <summary>
    /// 過去トラ観たか回答取消成功
    /// </summary>
    public const string MITAKA_CANCEL_SUCCESS = "取消成功";
    /// <summary>
    /// 過去トラ観たか回答取消失敗
    /// </summary>
    public const string MITAKA_CANCEL_FAILURE = "取消失敗";
    /// 過去トラ観たか確認完了成功
    /// </summary>
    public const string MITAKA_CONFIRMED_SUCCESS = "完了成功";
    /// <summary>
    /// 過去トラ観たか確認完了失敗
    /// </summary>
    public const string MITAKA_CONFIRMED_FAILURE = "取消失敗";
    /// 過去トラ観たか完了取消成功
    /// </summary>
    public const string MITAKA_CONFIRMED_CANCEL_SUCCESS = "取消成功";
    /// <summary>
    /// 過去トラ観たか完了取消失敗
    /// </summary>
    public const string MITAKA_CONFIRMED_CANCEL_FAILURE = "取消失敗";
    /// <summary>
    /// 過去トラ観たか回答情報登録成功
    /// </summary>
    public const string MITAKA_ANSWER_POST_SUCCESS = "登録完了";
    /// <summary>
    /// 過去トラ観たか回答情報登録失敗
    /// </summary>
    public const string MITAKA_ANSWER_POST_FAILURE = "登録失敗";
    /// <summary>
    /// 過去トラ観たか回答情報削除成功
    /// </summary>
    public const string MITAKA_ANSWER_DELETE_SUCCESS = "削除完了";
    /// <summary>
    /// 過去トラ観たか回答情報削除失敗
    /// </summary>
    public const string MITAKA_ANSWER_DELETE_FAILURE = "削除失敗";
    /// <summary>
    /// 過去トラ観たか回答情報チェック対象外
    /// </summary>
    public const string MITAKA_ANSWER_CHECK_UNKNOWN = "削除完了";

    public const string MAIL_BODY_MITAKA_REQUEST =
        "以下の過去トラ観たか回答依頼が作成されました。%0D%0A"+
        "各自、回答期限までに過去トラシステムにて回答ください。%0D%0A";
    public const string MAIL_BODY_MITAKA_COMPLETE = 
        "以下の過去トラ観たか点検作業が完了しました。%0D%0A";
    public const string MAIL_BODY_MITAKA_CANCEL =
        "以下の過去トラ観たか点検作業が中止されました。%0D%0A";
}
