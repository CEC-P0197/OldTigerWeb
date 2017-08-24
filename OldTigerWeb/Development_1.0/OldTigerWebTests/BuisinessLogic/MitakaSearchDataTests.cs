using Microsoft.VisualStudio.TestTools.UnitTesting;
using OldTigerWeb.BuisinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OldTigerWeb.DataAccess;
using OldTigerWeb.DataAccess.Tests;
using System.Data;

namespace OldTigerWeb.BuisinessLogic.Tests
{
    [TestClass()]
    public class MitakaSearchDataTests
    {
        MitakaSearchData testClass;
        TestDAMitakaData testDb;

        [TestMethod()]
        public void MitakaSearchDataPropertyTest()
        {
            //testDb = new DAMitakaData(TestSettings.TEST_PARAM_LOGINUSER);
            testClass = new MitakaSearchData(testDb, TestSettings.TEST_PARAM_LOGINUSER);

            #region プロパティ取得確認
            // 所有者
            Assert.IsNotNull(testClass.Owner, "所有者がNullで取得されないこと");
            // タイトル
            Assert.IsNotNull(testClass.Title, "タイトルがNullで取得されないこと");
            // 管理番号
            Assert.IsNotNull(testClass.ManageNo, "管理番号がNullで取得されないこと");
            // 作成部署コード
            Assert.IsNotNull(testClass.CreateDepartmentCode, "作成部署コードがNullで取得されないこと");
            // 回答対象部署コード
            Assert.IsNotNull(testClass.AnswerDepartmentCode, "回答対象部署コードがNullで取得されないこと");
            // 回答期間（カラ）
            Assert.IsNotNull(testClass.AnswerStartDateTime, "回答期間（カラ）がNullで取得されないこと");
            // 回答期間（カラ）表示用
            Assert.IsNotNull(testClass.AnswerStartDateTimeDisp, "回答期間（カラ）表示用がNullで取得されないこと");
            // 回答期間（マデ）
            Assert.IsNotNull(testClass.AnswerEndDateTime, "回答期間（マデ）がNullで取得されないこと");
            // 回答期間（マデ）表示用
            Assert.IsNotNull(testClass.AnswerEndDateTimeDisp, "回答期間（マデ）表示用がNullで取得されないこと");
            // 状況
            Assert.IsNotNull(testClass.Status, "状況がNullで取得されないこと");
            // 状況リスト
            Assert.IsNotNull(testClass.StatusList, "状況リストがNullで取得されないこと");
            // 回答区分
            Assert.IsNotNull(testClass.AnswerPattern, "回答区分がNullで取得されないこと");
            // 回答区分リスト
            Assert.IsNotNull(testClass.AnswerPatternList, "回答区分リストがNullで取得されないこと");
            // 開発符号
            Assert.IsNotNull(testClass.DevelopmentCode, "開発符号がNullで取得されないこと");
            // 機種
            Assert.IsNotNull(testClass.Model, "機種がNullで取得されないこと");
            // BLK No
            Assert.IsNotNull(testClass.BlockNo, "BLK NoがNullで取得されないこと");
            // タイトル品番
            Assert.IsNotNull(testClass.TitleDrawingNo, "タイトル品番がNullで取得されないこと");
            // 設通番号
            Assert.IsNotNull(testClass.EcsNo, "設通番号がNullで取得されないこと");
            // 検索結果リスト
            Assert.IsNotNull(testClass.SearchResultList, "検索結果リストがNullで取得されないこと");
            // 埋め込みスクリプト
            Assert.IsNotNull(testClass.EmbeddedScript, "埋め込みスクリプトがNullで取得されないこと");
            #endregion

            #region プロパティ設定確認

            // 所有者
            Assert.AreEqual(testClass.Owner,TestSettings.TEST_PARAM_LOGINUSER,
                "所有者が想定通りに設定されていること");
            // タイトル
            testClass.Title = TestSettings.TEST_PARAM_TITLE;
            Assert.AreEqual(testClass.Title,TestSettings.TEST_PARAM_TITLE,
                "タイトルが想定通りに設定されていること");
            // 管理番号
            testClass.ManageNo= TestSettings.TEST_PARAM_MANAGE_NO;
            Assert.AreEqual(testClass.ManageNo, TestSettings.TEST_PARAM_MANAGE_NO,
                "管理番号が想定通りに設定されていること");
            // 作成部署コード
            testClass.CreateDepartmentCode = TestSettings.TEST_PARAM_DIVISION_CODE1;
            Assert.AreEqual(testClass.CreateDepartmentCode, TestSettings.TEST_PARAM_DIVISION_CODE1,
                "作成部署コードが想定通りに設定されていること");
            // 回答対象部署コード
            testClass.AnswerDepartmentCode = TestSettings.TEST_PARAM_DIVISION_CODE2;
            Assert.AreEqual(testClass.AnswerDepartmentCode, TestSettings.TEST_PARAM_DIVISION_CODE2,
                "回答対象部署コードが想定通りに設定されていること");
            // 回答期間（カラ）表示用
            testClass.AnswerStartDateTimeDisp = TestSettings.TEST_PARAM_ANSWER_MONTH_START;
            Assert.AreEqual(testClass.AnswerStartDateTimeDisp, TestSettings.TEST_PARAM_ANSWER_MONTH_START,
                "回答期間（カラ）表示用が想定通りに設定されていること");
            // 回答期間（マデ）表示用
            testClass.AnswerEndDateTimeDisp = TestSettings.TEST_PARAM_ANSWER_MONTH_END;
            Assert.AreEqual(testClass.AnswerEndDateTimeDisp, TestSettings.TEST_PARAM_ANSWER_MONTH_END,
                "回答期間（マデ）表示用が想定通りに設定されていること");
            // 状況
            testClass.Status = TestSettings.TEST_PARAM_STATUS;
            Assert.AreEqual(testClass.Status, TestSettings.TEST_PARAM_STATUS,
                "状況が想定通りに設定されていること");
            // 回答区分
            testClass.AnswerPattern = TestSettings.TEST_PARAM_ANSWER_PATTERN;
            Assert.AreEqual(testClass.AnswerPattern, TestSettings.TEST_PARAM_ANSWER_PATTERN,
                "回答区分が想定通りに設定されていること");
            // 開発符号
            testClass.DevelopmentCode = TestSettings.TEST_PARAM_DEVELOPMENT_CODE;
            Assert.AreEqual(testClass.DevelopmentCode, TestSettings.TEST_PARAM_DEVELOPMENT_CODE,
                "開発符号が想定通りに設定されていること");
            // 機種
            testClass.Model= TestSettings.TEST_PARAM_MODEL_NO;
            Assert.AreEqual(testClass.Model, TestSettings.TEST_PARAM_MODEL_NO,
                "機種が想定通りに設定されていること");
            // BLK No
            testClass.BlockNo = TestSettings.TEST_PARAM_BLOCK_NO;
            Assert.AreEqual(testClass.BlockNo, TestSettings.TEST_PARAM_BLOCK_NO,
                "BLK Noが想定通りに設定されていること");
            // タイトル品番
            testClass.TitleDrawingNo = TestSettings.TEST_PARAM_TITLEDRAWINGNO;
            Assert.AreEqual(testClass.TitleDrawingNo, TestSettings.TEST_PARAM_TITLEDRAWINGNO,
                "タイトル品番が想定通りに設定されていること");
            // 設通番号
            testClass.EcsNo= TestSettings.TEST_PARAM_ECS_NO;
            Assert.AreEqual(testClass.EcsNo, TestSettings.TEST_PARAM_ECS_NO,
                "設通番号が想定通りに設定されていること");
            // 検索結果リスト
            Assert.IsNotNull(testClass.SearchResultList, "検索結果リストがNullで取得されないこと");
            // 埋め込みスクリプト
            Assert.IsNotNull(testClass.EmbeddedScript, "タイトル品番がNullで取得されないこと");

            #endregion

        }

        [TestMethod()]
        public void MitakaSearchDataTest()
        {
            testClass = new MitakaSearchData(TestSettings.TEST_PARAM_LOGINUSER);

            #region コンストラクタ確認

            // 所有者
            Assert.AreEqual(testClass.Owner, TestSettings.TEST_PARAM_LOGINUSER,"所有者が想定通りに設定されていること");
            // タイトル
            Assert.AreEqual(testClass.Title, "","タイトルが想定通りに設定されていること");
            // 管理番号
            Assert.AreEqual(testClass.ManageNo, "","管理番号が想定通りに設定されていること");
            // 作成部署コード
            Assert.AreEqual(testClass.CreateDepartmentCode, "", "作成部署コードが想定通りに設定されていること");
            // 回答対象部署コード
            Assert.AreEqual(testClass.AnswerDepartmentCode, "", "回答対象部署コードが想定通りに設定されていること");
            // 回答期間（カラ）
            Assert.AreEqual(testClass.AnswerStartDateTime, DateTime.Parse(Def.SQL_DATETIME_MIN), "回答期間（カラ）が想定通りに設定されていること");
            // 回答期間（カラ）表示用
            Assert.AreEqual(testClass.AnswerStartDateTimeDisp, "", "回答期間（カラ）表示用が想定通りに設定されていること");
            // 回答期間（マデ）
            Assert.AreEqual(testClass.AnswerEndDateTime, DateTime.Parse(Def.SQL_DATETIME_MAX), "回答期間（マデ）が想定通りに設定されていること");
            // 回答期間（マデ）表示用
            Assert.AreEqual(testClass.AnswerEndDateTimeDisp, "", "回答期間（マデ）表示用が想定通りに設定されていること");
            // 状況
            Assert.AreEqual(testClass.Status, "", "状況が想定通りに設定されていること");
            // 状況リスト
            Assert.IsNotNull(testClass.StatusList, "状況リストがnullでないこと");
            // 回答区分
            Assert.AreEqual(testClass.AnswerPattern, "", "回答区分が想定通りに設定されていること");
            // 回答区分リスト
            Assert.IsNotNull(testClass.AnswerPatternList, "回答区分リストがnullでないこと");
            // 開発符号
            Assert.AreEqual(testClass.DevelopmentCode, "", "開発符号が想定通りに設定されていること");
            // 機種
            Assert.AreEqual(testClass.Model, "", "機種が想定通りに設定されていること");
            // BLK No
            Assert.AreEqual(testClass.BlockNo, "", "BLK Noが想定通りに設定されていること");
            // タイトル品番
            Assert.AreEqual(testClass.TitleDrawingNo, "", "タイトル品番が想定通りに設定されていること");
            // 設通番号
            Assert.AreEqual(testClass.EcsNo, "", "設通番号が想定通りに設定されていること");
            // 検索結果リスト
            Assert.IsNotNull(testClass.SearchResultList, "検索結果リストがnullでないこと");
            // 埋め込みスクリプト
            Assert.AreEqual(testClass.EmbeddedScript, "", "埋め込みスクリプトが想定通りに設定されていること");

            #endregion
        }

        [TestMethod()]
        public void initMitakaSearchDataTest()
        {
            testClass = new MitakaSearchData(TestSettings.TEST_PARAM_LOGINUSER);

            #region プロパティ設定
            // タイトル
            testClass.Title = TestSettings.TEST_PARAM_TITLE;
            // 管理番号
            testClass.ManageNo = TestSettings.TEST_PARAM_MANAGE_NO;
            // 作成部署コード
            testClass.CreateDepartmentCode = TestSettings.TEST_PARAM_DIVISION_CODE1;
            // 回答対象部署コード
            testClass.AnswerDepartmentCode = TestSettings.TEST_PARAM_DIVISION_CODE2;
            // 回答期間（カラ）表示用
            testClass.AnswerStartDateTimeDisp = TestSettings.TEST_PARAM_ANSWER_MONTH_START;
            // 回答期間（マデ）表示用
            testClass.AnswerEndDateTimeDisp = TestSettings.TEST_PARAM_ANSWER_MONTH_END;
            // 状況
            testClass.Status = TestSettings.TEST_PARAM_STATUS;
            // 回答区分
            testClass.AnswerPattern = TestSettings.TEST_PARAM_ANSWER_PATTERN;
            // 開発符号
            testClass.DevelopmentCode = TestSettings.TEST_PARAM_DEVELOPMENT_CODE;
            // 機種
            testClass.Model = TestSettings.TEST_PARAM_MODEL_NO;
            // BLK No
            testClass.BlockNo = TestSettings.TEST_PARAM_BLOCK_NO;
            // タイトル品番
            testClass.TitleDrawingNo = TestSettings.TEST_PARAM_TITLEDRAWINGNO;
            // 設通番号
            testClass.EcsNo = TestSettings.TEST_PARAM_ECS_NO;
            #endregion

            // メソッド実行
            testClass.initMitakaSearchData();

            #region データ検証
            // 所有者
            Assert.AreEqual(testClass.Owner, TestSettings.TEST_PARAM_LOGINUSER, "所有者が想定通りに設定されていること");
            // タイトル
            Assert.AreEqual(testClass.Title, "", "タイトルが想定通りに設定されていること");
            // 管理番号
            Assert.AreEqual(testClass.ManageNo, "", "管理番号が想定通りに設定されていること");
            // 作成部署コード
            Assert.AreEqual(testClass.CreateDepartmentCode, "", "作成部署コードが想定通りに設定されていること");
            // 回答対象部署コード
            Assert.AreEqual(testClass.AnswerDepartmentCode, "", "回答対象部署コードが想定通りに設定されていること");
            // 回答期間（カラ）
            Assert.AreEqual(testClass.AnswerStartDateTime, DateTime.Parse(Def.SQL_DATETIME_MIN), "回答期間（カラ）が想定通りに設定されていること");
            // 回答期間（カラ）表示用
            Assert.AreEqual(testClass.AnswerStartDateTimeDisp, "", "回答期間（カラ）表示用が想定通りに設定されていること");
            // 回答期間（マデ）
            Assert.AreEqual(testClass.AnswerEndDateTime, DateTime.Parse(Def.SQL_DATETIME_MAX), "回答期間（マデ）が想定通りに設定されていること");
            // 回答期間（マデ）表示用
            Assert.AreEqual(testClass.AnswerEndDateTimeDisp, "", "回答期間（マデ）表示用が想定通りに設定されていること");
            // 状況
            Assert.AreEqual(testClass.Status, "", "状況が想定通りに設定されていること");
            // 状況リスト
            Assert.IsNotNull(testClass.StatusList, "状況リストがnullでないこと");
            // 回答区分
            Assert.AreEqual(testClass.AnswerPattern, "", "回答区分が想定通りに設定されていること");
            // 回答区分リスト
            Assert.IsNotNull(testClass.AnswerPatternList, "回答区分リストがnullでないこと");
            // 開発符号
            Assert.AreEqual(testClass.DevelopmentCode, "", "開発符号が想定通りに設定されていること");
            // 機種
            Assert.AreEqual(testClass.Model, "", "機種が想定通りに設定されていること");
            // BLK No
            Assert.AreEqual(testClass.BlockNo, "", "BLK Noが想定通りに設定されていること");
            // タイトル品番
            Assert.AreEqual(testClass.TitleDrawingNo, "", "タイトル品番が想定通りに設定されていること");
            // 設通番号
            Assert.AreEqual(testClass.EcsNo, "", "設通番号が想定通りに設定されていること");
            // 検索結果リスト
            Assert.IsNotNull(testClass.SearchResultList, "検索結果リストがnullでないこと");
            // 埋め込みスクリプト
            Assert.AreEqual(testClass.EmbeddedScript, "", "埋め込みスクリプトが想定通りに設定されていること");

            #endregion
        }

        [TestMethod()]
        public void searchMitakaDataConditionTest()
        {
            // テストパラメータ初期化
            testDb = new TestDAMitakaData();
            DataTable dt = new DataTable();
            dt.Columns.Add("MITAKA_NO");
            testDb.GetManageNoTestData = dt.Copy();

            testClass = new MitakaSearchData(testDb,TestSettings.TEST_PARAM_LOGINUSER);

            testClass.searchMitakaDataCondition();

            Assert.IsNotNull(testDb.getManageNoFromMitakaSearchData_Receive,
                "過去トラ観たか情報検索（検索条件指定）処理に到達していること");
        }

        [TestMethod()]
        public void searchMitakaDataMineTest()
        {
            // テストパラメータ初期化
            testDb = new TestDAMitakaData();
            DataTable dt = new DataTable();
            dt.Columns.Add("MITAKA_NO");
            testDb.GetManageNoTestData = dt.Copy();

            testClass = new MitakaSearchData(testDb, TestSettings.TEST_PARAM_LOGINUSER);

            testClass.searchMitakaDataMine();

            Assert.IsNotNull(testDb.getManageNoFromRelationUser_Receive,
                "過去トラ観たか情報検索（所有）処理に到達していること");
        }
    }
}