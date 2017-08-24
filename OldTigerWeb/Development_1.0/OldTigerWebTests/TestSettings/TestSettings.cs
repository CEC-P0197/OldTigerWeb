using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// テスト用設定
/// </summary>
public class TestSettings
{
    //************************************************
    // 外部ファイル・フォルダパス
    //************************************************
    /// <summary>
    /// EXCELテンプレートファイルパス
    /// </summary>
    public const string TEST_FILEPATH_EXCEL_TEMPLATE = "D:/Source/SUBARU/OldTigerWeb/OldTigerWeb/Development_1.0/OldTigerWeb/App_Data/template/Kakotora_Template.xltx";
    /// <summary>
    /// リンクフォルダパス
    /// </summary>
    public const string TEST_FOLDERPATH_LINK = "D:/Source/SUBARU/OldTigerWeb/OldTigerWeb/Development_1.0/OldTigerWeb/App_Data/template/Kakotora_Template.xltx";
    /// <summary>
    /// ヘルプファイルパス
    /// </summary>
    public const string TEST_FOLDERPATH_HELP = "D:/Source/SUBARU/OldTigerWeb/OldTigerWeb/Development_1.0/OldTigerWeb/App_Data/template/Kakotora_Template.xltx";

    //************************************************
    // メールアドレス
    //************************************************
    /// <summary>
    /// BY用メールアドレス
    /// </summary>
    public const string TEST_ADDRESS_MAIL_BY = "TestBY@test.com";
    /// <summary>
    /// PU用メールアドレス
    /// </summary>
    public const string TEST_ADDRESS_MAIL_PU = "TestPU@test.com";

    //************************************************
    // 過去トラ観たか関連テストパラメータ
    //************************************************
    /// <summary>
    /// ログインユーザー
    /// </summary>
    public const string TEST_PARAM_LOGINUSER= "TestUser";
    /// <summary>
    /// 管理番号
    /// </summary>
    public const string TEST_PARAM_MANAGE_NO = "TEST";
    /// <summary>
    /// 管理番号（ループ用）
    /// 末尾に一連Noが追加される
    /// </summary>
    public const string TEST_PARAM_MANAGE_NO_ROOP = "TEST";
    /// <summary>
    /// 課コード１
    /// </summary>
    public const string TEST_PARAM_DIVISION_CODE1 = "DIV1";
    /// <summary>
    /// 課コード２
    /// </summary>
    public const string TEST_PARAM_DIVISION_CODE2 = "DIV2";
    /// <summary>
    /// タイトル
    /// </summary>
    public const string TEST_PARAM_TITLE = "タイトル";
    /// <summary>
    /// 目的
    /// </summary>
    public const string TEST_PARAM_PURPOSE = "目的";
    /// <summary>
    /// 目的
    /// </summary>
    public const string TEST_PARAM_COMMENT = "コメント";
    /// <summary>
    /// 開始日(yyyy/MM/DD)
    /// </summary>
    public const string TEST_PARAM_DATE_START = "2017/05/01";
    /// <summary>
    /// 終了日(yyyy/MM/DD)
    /// </summary>
    public const string TEST_PARAM_DATE_END = "2017/08/31";
    /// <summary>
    /// 回答開始日時
    /// </summary>
    public const string TEST_PARAM_ANSWER_START = "2017-05-01 00:00:00";
    /// <summary>
    /// 回答終了日時
    /// </summary>
    public const string TEST_PARAM_ANSWER_END = "2017-08-31 23:59:59";
    /// <summary>
    /// 回答開始日時（年月）
    /// </summary>
    public const string TEST_PARAM_ANSWER_MONTH_START= "201705";
    /// <summary>
    /// 回答終了日時（年月）
    /// </summary>
    public const string TEST_PARAM_ANSWER_MONTH_END = "201708";
    /// <summary>
    /// 状況
    /// </summary>
    public const string TEST_PARAM_STATUS = Def.MITAKA_STATUS_ANSWER;
    /// <summary>
    /// 状況コメント
    /// </summary>
    public const string TEST_PARAM_STATUS_COMMENT = "状況コメント";
    /// 回答区分
    /// </summary>
    public const string TEST_PARAM_ANSWER_PATTERN = "1";
    /// <summary>
    /// 開発符号
    /// </summary>
    public const string TEST_PARAM_DEVELOPMENT_CODE= "開発符号";
    /// <summary>
    /// 機種
    /// </summary>
    public const string TEST_PARAM_MODEL = "機種";
    /// <summary>
    /// ブロック番号
    /// </summary>
    public const string TEST_PARAM_BLKNO = "ブロック番号";
    /// <summary>
    /// タイトル品番
    /// </summary>
    public const string TEST_PARAM_TITLEDRAWINGNO= "タイトル品番番号";
    /// <summary>
    /// 設通番号
    /// </summary>
    public const string TEST_PARAM_ECSNO= "設通番号";
    /// <summary>
    /// 関連ユーザーID
    /// </summary>
    public const string TEST_PARAM_RELATION_USER= "USER10";
    /// <summary>
    /// 検索タイプ
    /// </summary>
    public const string TEST_PARAM_SEARCH_TYPE = "1";
    /// <summary>
    /// 検索区分
    /// </summary>
    public const string TEST_PARAM_SEARCH_CLASS = "1";
    /// <summary>
    /// 検索条件_キーワード
    /// </summary>
    public const string TEST_PARAM_SEARCH_PARAMETER_KEYWORD= "サスペンション";
    /// <summary>
    /// 検索条件_カテゴリ
    /// </summary>
    public const string TEST_PARAM_SEARCH_PARAMETER_COTEGORY = "";
    /// <summary>
    /// 展開対象_システムNo
    /// </summary>
    public const int TEST_PARAM_DEPLOY_SYSTEMNO = 1;
    /// <summary>
    /// タイトル品番情報_タイトル図面番号
    /// </summary>
    public const string TEST_PARAM_DRAWING_NO = "0000000001";
    /// <summary>
    /// 機種情報_機種
    /// </summary>
    public const string TEST_PARAM_MODEL_NO = "TEST";
    /// <summary>
    /// BLK情報_BLKNo
    /// </summary>
    public const string TEST_PARAM_BLOCK_NO = "TEST";
    /// <summary>
    /// 開発符号情報_開発符号
    /// </summary>
    public const string TEST_PARAM_DEVELOP_CODE = "TEST";
    /// <summary>
    /// 設通情報_設通番号
    /// </summary>
    public const string TEST_PARAM_ECS_NO= "TEST";
    /// <summary>
    /// 過去トラ観たか回答_システムNo
    /// </summary>
    public const int TEST_PARAM_ANSWER_SYSTEMNO = 1;
    /// <summary>
    /// 過去トラ観たか回答_回答対象部署コード
    /// </summary>
    public const string TEST_PARAM_ANSWER_DIVISION_CODE = "TEST";
    /// <summary>
    /// 過去トラ観たか回答_進捗状況
    /// </summary>
    public const string TEST_PARAM_ANSWER_STATUS= "1";
    /// <summary>
    /// 過去トラ観たか回答_回答内容
    /// </summary>
    public const string TEST_PARAM_ANSWER_CONTENT = "TEST";

}
