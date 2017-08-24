using Microsoft.VisualStudio.TestTools.UnitTesting;
using OldTigerWeb.BuisinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using OldTigerWeb.DataAccess.Tests;
using OldTigerWeb.DataAccess;

namespace OldTigerWeb.BuisinessLogic.Tests
{
    [TestClass()]
    public class MitakaAnswerDataTests
    {
        //MitakaAnswerData testClass;
        TestDAMitakaData testDb;

        [TestInitialize]
        public void MitakaAnswerDataTestInitialize()
        {
            //string testManageNo = TestSettings.TEST_PARAM_MANAGE_NO;    // 管理番号
            //testClass = new MitakaAnswerData(testManageNo);
        }
        [TestMethod()]
        public void MitakaAnswerDataPropertyTest()
        {
            #region プロパティ検証
            // テスト用インスタンス作成
            var shot = new MitakaAnswerData(TestSettings.TEST_PARAM_MANAGE_NO);
            // DBテストパラメータ作成
            var dt = new DAMitakaData().getMitakaAnswerData();

            #region 初期値

            // プロパティ取得
            // 管理番号
            Assert.IsNotNull(shot.ManageNo, "管理番号がNullで取得されないこと");
            // システムNo
            Assert.IsNotNull(shot.AnswerSystemNo, "システムNoがNullで取得されないこと");
            // 回答対象部署コード
            Assert.IsNotNull(shot.AnswerDepartmentCode, "回答対象部署コードがNullで取得されないこと");
            // 観たか回答リスト（全部署）
            Assert.IsNotNull(shot.MitakaAnswerList, "観たか回答リスト（全部署）がNullで取得されないこと");
            // 観たか回答リスト（指定部署）
            Assert.IsNotNull(shot.MitakaAnswerDepartmentList, "観たか回答リスト（指定部署）がNullで取得されないこと");
            // 観たか回答データ（回答単位）
            Assert.IsNotNull(shot.MitakaAnswerTargetData, "観たか回答データ（回答単位）がNullで取得されないこと");
            // 処理結果
            Assert.IsNotNull(shot.DataProcessResult, "処理結果がNullで取得されないこと");
            // 編集フラグ
            Assert.IsNotNull(shot.EditFlg, "編集フラグがNullで取得されないこと");
            // 埋め込みスクリプト
            Assert.IsNotNull(shot.embeddedScript, "埋め込みスクリプトがNullで取得されないこと");

            // プロパティ設定

            // システムNo
            shot.AnswerSystemNo = TestSettings.TEST_PARAM_ANSWER_SYSTEMNO.ToString();
            Assert.AreEqual(shot.AnswerSystemNo, TestSettings.TEST_PARAM_ANSWER_SYSTEMNO.ToString(),
                "システムNoが設定値と同一であること");
            // 回答対象部署コード
            shot.AnswerDepartmentCode = TestSettings.TEST_PARAM_ANSWER_DIVISION_CODE;
            Assert.AreEqual(shot.AnswerDepartmentCode, TestSettings.TEST_PARAM_ANSWER_DIVISION_CODE,
                "回答対象部署コードが設定値と同一であること");

            // 観たか回答データ（回答単位）
            var drAnswerData = dt.NewRow();
            drAnswerData["MITAKA_NO"] = "TEST01";
            drAnswerData["SYSTEM_NO"] = TestSettings.TEST_PARAM_ANSWER_SYSTEMNO;
            drAnswerData["ANSWER_DIVISION_CODE"] = TestSettings.TEST_PARAM_ANSWER_DIVISION_CODE;
            drAnswerData["STATUS"] = TestSettings.TEST_PARAM_ANSWER_STATUS;
            drAnswerData["ANSWER_CONTENT"] = TestSettings.TEST_PARAM_ANSWER_CONTENT;
            shot.MitakaAnswerTargetData = drAnswerData;
            Assert.IsTrue(shot.MitakaAnswerList.Rows.Count > 0,
                "過去トラ観たか回答データから設定できること");

            // 観たか回答リスト（全部署）
            var mitakaAnswerList = shot.MitakaAnswerList.Copy();
            mitakaAnswerList.Rows.Clear();
            for (int i = 1; i <= 2; i++)
            {
                var drAnswerList = mitakaAnswerList.NewRow();
                drAnswerList["MITAKA_NO"] = "TEST02";
                drAnswerList["SYSTEM_NO"] = TestSettings.TEST_PARAM_ANSWER_SYSTEMNO;
                drAnswerList["ANSWER_DIVISION_CODE"] = TestSettings.TEST_PARAM_ANSWER_DIVISION_CODE +"01";
                drAnswerList["STATUS"] = i.ToString();
                drAnswerList["ANSWER_CONTENT"] = TestSettings.TEST_PARAM_ANSWER_CONTENT + i.ToString("00");
                mitakaAnswerList.Rows.Add(drAnswerList);
            }

            shot.MitakaAnswerList = mitakaAnswerList;

            Assert.IsTrue(shot.MitakaAnswerList.Rows.Count == 1,
                "過去トラ観たか回答データから設定できること");
            Assert.IsTrue(shot.MitakaAnswerList.Rows[0]["MITAKA_NO"].ToString()
                == "TEST02","管理番号に想定通りの値が設定されていること");
            Assert.IsTrue(shot.MitakaAnswerList.Rows[0]["SYSTEM_NO"].ToString()
                == TestSettings.TEST_PARAM_ANSWER_SYSTEMNO.ToString(), "システムNoに想定通りの値が設定されていること");
            Assert.IsTrue(shot.MitakaAnswerList.Rows[0]["ANSWER_DIVISION_CODE"].ToString()
                == TestSettings.TEST_PARAM_ANSWER_DIVISION_CODE+ "01", "回答対象部署コードに想定通りの値が設定されていること");
            Assert.IsTrue(shot.MitakaAnswerList.Rows[0]["STATUS"].ToString()
                 == "2".ToString(), "状況に想定通りの値が設定されていること");
            Assert.IsTrue(shot.MitakaAnswerList.Rows[0]["ANSWER_CONTENT"].ToString()
                 == "TEST"+"02".ToString(), "状況に想定通りの値が設定されていること");

            // 処理結果
            List<string> strlist = new List<string>();
            int count = 5;
            for (int i = 1; i <= count; i++)
                strlist.Add("TEST" + i.ToString("00"));

            for (int i = 1; i <= count; i++)
                Assert.AreEqual(strlist[i - 1], "TEST" + i.ToString("00"));

            #endregion
            #endregion


        }
        [TestMethod()]
        public void MitakaAnswerDataTest()
        {
            TestDAMitakaData testDb;

            #region SHOT01

            // テストパラメータ初期化
            testDb = new TestDAMitakaData();

            // メソッド実行（インスタンス作成）
            var shot01 = new MitakaAnswerData(
                testDb);

            #region データ検証
            // インスタンス検証
            // クラスがNULLでないこと
            Assert.IsNotNull(shot01, "インスタンスが作成されていること");

            // コンストラクタ処理確認

            // DB取得アクション確認
            Assert.IsNotNull(testDb.getMitakaAnswerData_Receive, "過去トラ観たか回答情報取得処理に到達していること");
            Assert.IsTrue(testDb.getMitakaAnswerData_Receive.Any(row => row == null),
                "過去トラ観たか回答情報取得処理に想定通りの引数が渡っていること");
            Assert.AreEqual(shot01.AnswerSystemNo, "",
                "過去トラ観たか回答情報インスタンスに想定通りの回答対象システムNoが設定されていること");
            Assert.AreEqual(shot01.AnswerDepartmentCode, "",
                "過去トラ観たか回答情報インスタンスに想定通りの回答対象部署コードが設定されていること");

            #endregion
            #endregion

            #region SHOT02

            // メソッド実行（インスタンス作成）
            var shot02 = new MitakaAnswerData();

            #region データ検証
            // インスタンス検証
            // クラスがNULLでないこと
            Assert.IsNotNull(shot02);

            // プロパティ検証
            // 管理番号がNULLでないこと
            Assert.IsNotNull(shot02.ManageNo, "管理番号がNULLでないこと");
            // 回答対象システムNoがNULLでないこと
            Assert.IsNotNull(shot02.AnswerSystemNo,"回答対象システムNoがNULLでないこと");
            // 回答対象部署コードがNULLでないこと
            Assert.IsNotNull(shot02.AnswerDepartmentCode, "回答対象部署コードがNULLでないこと");
            // 過去トラ観たか回答リスト（全部署）がNULLでないこと
            Assert.IsNotNull(shot02.MitakaAnswerList, "過去トラ観たか回答リスト（全部署）がNULLでないこと");
            // 過去トラ観たか回答リスト（指定部署）がNULLでないこと
            Assert.IsNotNull(shot02.MitakaAnswerDepartmentList, "過去トラ観たか回答リスト（指定部署）がNULLでないこと");
            // 過去トラ観たか回答リスト（回答単位）がNULLでないこと
            Assert.IsNotNull(shot02.MitakaAnswerTargetData, "過去トラ観たか回答リスト（回答単位）がNULLでないこと");
            // データ処理結果がNULLでないこと
            Assert.IsNotNull(shot02.DataProcessResult, "データ処理結果がNULLでないこと");
            // 編集フラグがNULLでないこと
            Assert.AreEqual(shot02.EditFlg,false, "編集フラグがfalseであること");
            // 埋め込みスクリプトがNULLでないこと
            Assert.AreEqual(shot02.embeddedScript, "", "埋め込みスクリプトが''であること");
            #endregion

            #endregion
        }

        [TestMethod()]
        public void MitakaAnswerDataTest1()
        {
            #region SHOT01

            // テストパラメータ初期化
            testDb = new TestDAMitakaData();

            // メソッド実行（インスタンス作成）
            var shot01 = new MitakaAnswerData(testDb,
                TestSettings.TEST_PARAM_MANAGE_NO);

            #region データ検証
            // インスタンス検証
            // クラスがNULLでないこと
            Assert.IsNotNull(shot01,"インスタンスが作成されていること");

            // コンストラクタ処理確認

            // DB取得アクション確認
            Assert.IsNotNull(testDb.getMitakaAnswerData_Receive,"過去トラ観たか回答情報取得処理に到達していること");
            Assert.IsTrue(testDb.getMitakaAnswerData_Receive.Any(row => row == TestSettings.TEST_PARAM_MANAGE_NO),
                "過去トラ観たか回答情報取得処理に想定通りの引数が渡っていること");
            Assert.AreEqual(shot01.AnswerSystemNo, "",
                "過去トラ観たか回答情報インスタンスに想定通りの回答対象システムNoが設定されていること");
            Assert.AreEqual(shot01.AnswerDepartmentCode, "",
                "過去トラ観たか回答情報インスタンスに想定通りの回答対象部署コードが設定されていること");

            #endregion
            #endregion

            #region SHOT02

            // テストパラメータ初期化
            testDb = new TestDAMitakaData();

            // メソッド実行（インスタンス作成）
            var shot02 = new MitakaAnswerData(testDb,
                TestSettings.TEST_PARAM_MANAGE_NO,
                TestSettings.TEST_PARAM_ANSWER_SYSTEMNO,
                TestSettings.TEST_PARAM_ANSWER_DIVISION_CODE
                );

            #region データ検証
            // インスタンス検証
            // クラスがNULLでないこと
            Assert.IsNotNull(shot02);

            // コンストラクタ処理確認

            // DB取得アクション確認
            Assert.IsNotNull(testDb.getMitakaAnswerData_Receive, "過去トラ観たか回答情報取得処理に到達していること");
            Assert.IsTrue(testDb.getMitakaAnswerData_Receive.Any(row => row == TestSettings.TEST_PARAM_MANAGE_NO),
                "過去トラ観たか回答情報取得処理に想定通りの引数が渡っていること");
            Assert.AreEqual(shot02.AnswerSystemNo,TestSettings.TEST_PARAM_ANSWER_SYSTEMNO.ToString(),
                "過去トラ観たか回答情報インスタンスに想定通りの回答対象システムNoが設定されていること");
            Assert.AreEqual(shot02.AnswerDepartmentCode,TestSettings.TEST_PARAM_ANSWER_DIVISION_CODE,
                "過去トラ観たか回答情報インスタンスに想定通りの回答対象部署コードが設定されていること");
            #endregion
            #endregion

            #region SHOT03
            // メソッド実行（インスタンス作成）
            var shot03 = new MitakaAnswerData();

            #region データ検証
            // インスタンス検証
            // クラスがNULLでないこと
            Assert.IsNotNull(shot03);

            // プロパティ検証
            // 管理番号がNULLでないこと
            Assert.IsNotNull(shot03.ManageNo, "管理番号がNULLでないこと");
            // 回答対象システムNoがNULLでないこと
            Assert.IsNotNull(shot03.AnswerSystemNo, "回答対象システムNoがNULLでないこと");
            // 回答対象部署コードがNULLでないこと
            Assert.IsNotNull(shot03.AnswerDepartmentCode, "回答対象部署コードがNULLでないこと");
            // 過去トラ観たか回答リスト（全部署）がNULLでないこと
            Assert.IsNotNull(shot03.MitakaAnswerList, "過去トラ観たか回答リスト（全部署）がNULLでないこと");
            // 過去トラ観たか回答リスト（指定部署）がNULLでないこと
            Assert.IsNotNull(shot03.MitakaAnswerDepartmentList, "過去トラ観たか回答リスト（指定部署）がNULLでないこと");
            // 過去トラ観たか回答リスト（回答単位）がNULLでないこと
            Assert.IsNotNull(shot03.MitakaAnswerTargetData, "過去トラ観たか回答リスト（回答単位）がNULLでないこと");
            // データ処理結果がNULLでないこと
            Assert.IsNotNull(shot03.DataProcessResult, "データ処理結果がNULLでないこと");
            // 編集フラグがNULLでないこと
            Assert.AreEqual(shot03.EditFlg, false, "編集フラグがfalseであること");
            // 埋め込みスクリプトがNULLでないこと
            Assert.AreEqual(shot03.embeddedScript, "", "埋め込みスクリプトが''であること");
            #endregion
            #endregion

        }

        [TestMethod()]
        public void initMitakaAnswerDataTest()
        {
            #region SHOT01

            // テストパラメータ初期化
            testDb = new TestDAMitakaData();
            // メソッド実行（インスタンス作成）
            var shot01 = new MitakaAnswerData(testDb,
                TestSettings.TEST_PARAM_MANAGE_NO);

            // テストパラメータ設定
            testDb.getMitakaAnswerData_Receive = null;

            // メソッド実行（インスタンス作成）
            shot01.initMitakaAnswerData();

            #region データ検証
            // インスタンス検証
            // クラスがNULLでないこと
            Assert.IsNotNull(shot01, "インスタンスが作成されていること");

            // DB取得アクション確認
            Assert.IsNotNull(testDb.getMitakaAnswerData_Receive, "過去トラ観たか回答情報取得処理に到達していること");
            Assert.IsTrue(testDb.getMitakaAnswerData_Receive.Any(row => row == null),
                "過去トラ観たか回答情報取得処理に想定通りの引数が渡っていること");
            Assert.AreEqual(shot01.EditFlg, false, "編集フラグがfalseに設定されていること");
            #endregion
            #endregion
        }

        [TestMethod()]
        public void checkMitakaAnswerDataTest()
        {
            var _db = new DAMitakaData(TestSettings.TEST_PARAM_LOGINUSER);
            var dtAnswer = _db.getMitakaAnswerData();
            var drAnswer = dtAnswer.NewRow() ;
            dtAnswer.Rows.Add(drAnswer);

            // メソッド実行（インスタンス作成）
            var shot = new MitakaAnswerData();

            #region SHOT01
            // テストパラメータ設定
            // 初期値で実行

            // メソッド実行（インスタンス作成）
            if (shot.checkMitakaAnswerData("1")) // 新規登録 失敗ケース
                Assert.Fail("SHOT01で登録できないこと");

            if(shot.checkMitakaAnswerData("2"))  // 削除　失敗ケース
                Assert.Fail("SHOT01で削除できないこと");
            #endregion

            #region SHOT02
            // テストパラメータ設定

            shot = new MitakaAnswerData(TestSettings.TEST_PARAM_MANAGE_NO); // 管理番号セット
            shot = new MitakaAnswerData(TestSettings.TEST_PARAM_MANAGE_NO); // 管理番号セット
            
            // メソッド実行（インスタンス作成）
            if (shot.checkMitakaAnswerData("1")) // 新規登録 チェックNG
                Assert.Fail("SHOT02で登録できないこと");

            if (!shot.checkMitakaAnswerData("2"))  // 削除　チェックOK
                Assert.Fail("SHOT02で削除できること");
            #endregion

            #region SHOT03
            // テストパラメータ設定
            shot = new MitakaAnswerData(TestSettings.TEST_PARAM_MANAGE_NO); // 管理番号セット
            dtAnswer.Rows[0]["SYSTEM_NO"] = TestSettings.TEST_PARAM_ANSWER_SYSTEMNO; // システムNoセット
            shot.MitakaAnswerList = dtAnswer.Copy();

            // メソッド実行（インスタンス作成）
            if (shot.checkMitakaAnswerData("1")) // 新規登録 チェックNG
                Assert.Fail("SHOT03で登録できないこと");

            if (!shot.checkMitakaAnswerData("2"))  // 削除　チェックOK
                Assert.Fail("SHOT03で削除できること");
            #endregion

            #region SHOT04
            // テストパラメータ設定

            shot = new MitakaAnswerData(TestSettings.TEST_PARAM_MANAGE_NO); // 管理番号セット
            dtAnswer.Clear();
            drAnswer = dtAnswer.NewRow();
            drAnswer["ANSWER_DIVISION_CODE"] = TestSettings.TEST_PARAM_ANSWER_DIVISION_CODE; // 部署コードセット
            dtAnswer.Rows.Add(drAnswer);
            shot.MitakaAnswerList = dtAnswer.Copy();

            // メソッド実行（インスタンス作成）
            if (shot.checkMitakaAnswerData("1")) // 新規登録 チェックNG
                Assert.Fail("SHOT04で登録できないこと");

            if (!shot.checkMitakaAnswerData("2"))  // 削除　チェックOK
                Assert.Fail("SHOT04で削除できること");
            #endregion

            #region SHOT05
            // テストパラメータ設定
            shot = new MitakaAnswerData(TestSettings.TEST_PARAM_MANAGE_NO); // 管理番号セット
            dtAnswer.Rows[0]["SYSTEM_NO"] = TestSettings.TEST_PARAM_ANSWER_SYSTEMNO; // システムNoセット
            dtAnswer.Rows[0]["ANSWER_DIVISION_CODE"] = TestSettings.TEST_PARAM_ANSWER_DIVISION_CODE; // 部署コードセット

            // メソッド実行（インスタンス作成）
            if (shot.checkMitakaAnswerData("1")) // 新規登録 チェックNG
                Assert.Fail("SHOT05で登録できないこと");

            if (!shot.checkMitakaAnswerData("2"))  // 削除　チェックOK
                Assert.Fail("SHOT05で削除できること");
            #endregion

            #region SHOT06
            // テストパラメータ設定
            shot = new MitakaAnswerData(TestSettings.TEST_PARAM_MANAGE_NO); // 管理番号セット
            dtAnswer.Rows[0]["STATUS"] = TestSettings.TEST_PARAM_ANSWER_STATUS; // 状況セット

            // メソッド実行（インスタンス作成）
            if (shot.checkMitakaAnswerData("1")) // 新規登録 チェックNG
                Assert.Fail("SHOT06で登録できないこと");

            if (!shot.checkMitakaAnswerData("2"))  // 削除　チェックOK
                Assert.Fail("SHOT06で削除できること");
            #endregion

            #region SHOT07
            // テストパラメータ設定
            shot = new MitakaAnswerData(TestSettings.TEST_PARAM_MANAGE_NO); // 管理番号セット
            shot.AnswerSystemNo = TestSettings.TEST_PARAM_ANSWER_SYSTEMNO.ToString(); // システムNoセット
            shot.AnswerDepartmentCode = TestSettings.TEST_PARAM_ANSWER_DIVISION_CODE; // 部署コードセット
            dtAnswer.Rows[0]["ANSWER_CONTENT"] = TestSettings.TEST_PARAM_ANSWER_CONTENT; // 回答内容セット

            // メソッド実行（インスタンス作成）
            if (shot.checkMitakaAnswerData("1")) // 新規登録 チェックOK
                Assert.Fail("SHOT07で登録できること");

            if (!shot.checkMitakaAnswerData("2"))  // 削除　チェックOK
                Assert.Fail("SHOT07で削除できること");
            #endregion



        }

        [TestMethod()]
        public void getMitakaAnswerDataTest()
        {
            #region SHOT01

            // テストパラメータ初期化
            testDb = new TestDAMitakaData();
            // メソッド実行（インスタンス作成）
            var shot01 = new MitakaAnswerData(testDb,
                TestSettings.TEST_PARAM_MANAGE_NO);

            // テストパラメータ設定
            testDb.getMitakaAnswerData_Receive = null;

            // メソッド実行（テスト実行）
            shot01.getMitakaAnswerData();

            #region データ検証
            // インスタンス検証
            // クラスがNULLでないこと
            Assert.IsNotNull(shot01, "インスタンスが作成されていること");

            // DB取得アクション確認
            Assert.IsNotNull(testDb.getMitakaAnswerData_Receive, "過去トラ観たか回答情報取得処理に到達していること");
            Assert.IsTrue(testDb.getMitakaAnswerData_Receive.Any(row => row == TestSettings.TEST_PARAM_MANAGE_NO),
                "過去トラ観たか回答情報取得処理に想定通りの引数が渡っていること");
            Assert.AreEqual(shot01.EditFlg, false, "編集フラグがfalseに設定されていること");
            #endregion
            #endregion
        }

        [TestMethod()]
        public void deployMitakaAnswerDataTest()
        {
            #region SHOT01
            // メソッド実行（インスタンス作成）
            var shot01 = new MitakaAnswerData();

            // テストパラメータ作成
            var dt = new DAMitakaData().getDeploymentTroubleData();
            var dr = dt.NewRow();
            dr["SYSTEM_NO"] = TestSettings.TEST_PARAM_ANSWER_SYSTEMNO;

            int count = 10;
            for (int i = 1; i <= count; i++)
            {
                dr["BUSYO_SEKKEI" + i.ToString()] = "TEST" + i.ToString();
                dr["BUSYO_HYOUKA" + i.ToString()] = "TEST" + (i+count).ToString();
            }

            // メソッド実行（テスト実行）
            shot01.deployMitakaAnswerData(dr);

            #region データ検証
            // インスタンス検証
            // クラスがNULLでないこと
            Assert.IsNotNull(shot01, "インスタンスが作成されていること");

            // DB取得アクション確認
            Assert.IsNotNull(shot01.MitakaAnswerList, "過去トラ観たか回答情報リストがNULLでないこと");
            Assert.AreEqual(shot01.MitakaAnswerList.Rows.Count, count*2, "過去トラ観たか回答情報リストが想定件数展開されていること");
            #endregion
            #endregion

        }

        [TestMethod()]
        public void postMitakaAnswerDataTest()
        {
            MitakaAnswerData shot01;
            MitakaAnswerData shot02;
            MitakaAnswerData shot03;
            TestDAMitakaData testDb01;
            TestDAMitakaData testDb02;
            TestDAMitakaData testDb03;
            bool result1;
            bool result2;
            bool result3;

            // DB関連設定テストパラメータ作成
            testDb01 = new TestDAMitakaData();
            testDb02 = new TestDAMitakaData();
            testDb03 = new TestDAMitakaData();
            var dt = new DAMitakaData().getMitakaAnswerData();
            // 検索結果0件の為、削除処理無し
            testDb01.GetMitakaAnswerTestData = dt.Clone(); 
            testDb02.GetMitakaAnswerTestData = dt.Clone(); 
            testDb03.GetMitakaAnswerTestData = dt.Clone();
            // DBに存在しない
            testDb01.ExistResult = false; 
            testDb02.ExistResult = false; 
            testDb03.ExistResult = false;
            // DB登録が成功とした想定にする
            testDb01.PostResult = true; 
            testDb02.PostResult = true; 
            testDb03.PostResult = true;


            #region SHOT01・02・03
            // インスタンス作成
            // 管理番号未取得 & 登録行の管理番号設定
            shot01 = new MitakaAnswerData(testDb01);　
            // 管理番号取得 & 登録行の管理番号設定
            shot02 = new MitakaAnswerData(testDb02, 
                TestSettings.TEST_PARAM_MANAGE_NO); 
            // 管理番号未取得 & 登録行の管理番号未設定
            shot03 = new MitakaAnswerData(testDb03);　

            // テストパラメータ作成
            int i;
            for (i = 1; i <= 2; i++)
            {
                var dr = dt.NewRow();
                dr["MITAKA_NO"] = "SHOT" + i.ToString("00");
                dr["SYSTEM_NO"] = i;
                dr["ANSWER_DIVISION_CODE"] = TestSettings.TEST_PARAM_ANSWER_DIVISION_CODE;
                dr["STATUS"] = TestSettings.TEST_PARAM_ANSWER_STATUS;
                dr["ANSWER_CONTENT"] = TestSettings.TEST_PARAM_ANSWER_CONTENT;
                dr["EDIT_FLG"] = (i % 2).ToString();
                shot02.MitakaAnswerTargetData = dr;
                dt.Rows.Add(dr);
            }
            // 登録値セット
            shot01.MitakaAnswerList = dt.Copy();
            shot02.MitakaAnswerList = dt.Copy();
            //shot03.MitakaAnswerList = dt.Copy();
            var dtShot03 = dt.Copy();
            for (int j = 0; j < dtShot03.Rows.Count; j ++)
            {
                dtShot03.Rows[j]["MITAKA_NO"] = "";
            }
            shot03.MitakaAnswerList = dtShot03;

            // メソッド実行
            result1 = shot01.postMitakaAnswerData();
            result2 = shot02.postMitakaAnswerData();
            result3 = shot03.postMitakaAnswerData();

            #region データ検証
            // インスタンス検証
            Assert.IsNotNull(shot01, "インスタンスが作成されていること");
            Assert.IsNotNull(shot02, "インスタンスが作成されていること");
            Assert.IsNotNull(shot03, "インスタンスが作成されていること");

            // DB取得アクション確認
            Assert.IsTrue(result1, "SHOT01の登録結果が想定通りに返却されていること");
            Assert.IsNotNull(testDb01.postMitakaAnswerData_Receive,
                "SHOT01のDB処理「過去トラ観たか回答情報登録」まで到達していること");
            Assert.IsTrue(shot01.embeddedScript != "", "SHOT01の埋め込みスクリプトが取得できていること");

            Assert.IsTrue(result2, "SHOT02の登録結果が想定通りに返却されていること");
            Assert.IsNotNull(testDb02.postMitakaAnswerData_Receive,
                "SHOT02のDB処理「過去トラ観たか回答情報登録」まで到達していること");
            Assert.IsTrue(shot02.embeddedScript != "", "SHOT02の埋め込みスクリプトが取得できていること");

            Assert.IsTrue(!result3, "SHOT03の登録結果が想定通りに返却されていること");
            Assert.IsNull(testDb03.postMitakaAnswerData_Receive,
                "SHOT03のDB処理「過去トラ観たか回答情報登録」まで到達していないこと");
            Assert.IsTrue(shot03.embeddedScript != "", "SHOT03の埋め込みスクリプトが取得できていること");
            #endregion
            #endregion

            #region SHOT04・05

            // 登録失敗ケース
            testDb01.PostResult = false;
            testDb02.PostResult = false;

            // メソッド実行
            result1 = shot01.postMitakaAnswerData();
            result2 = shot02.postMitakaAnswerData();

            #region データ検証
            // インスタンス検証
            Assert.IsNotNull(shot01, "インスタンスが作成されていること");
            Assert.IsNotNull(shot02, "インスタンスが作成されていること");
            Assert.IsNotNull(shot03, "インスタンスが作成されていること");

            // DB取得アクション確認
            Assert.IsTrue(!result1, "SHOT01の登録結果が想定通りに返却されていること");
            Assert.IsTrue(shot01.embeddedScript != "", "SHOT01の埋め込みスクリプトが取得できていること");

            Assert.IsTrue(!result2, "SHOT02の登録結果が想定通りに返却されていること");
            Assert.IsTrue(shot02.embeddedScript != "", "SHOT01の埋め込みスクリプトが取得できていること");

            #endregion

            #endregion
        }

        [TestMethod()]
        public void deleteMitakaAnswerDataTest()
        {
            MitakaAnswerData shot01;
            MitakaAnswerData shot02;
            TestDAMitakaData testDb01;
            TestDAMitakaData testDb02;
            bool result1;
            bool result2;

            // DB関連設定テストパラメータ作成
            testDb01 = new TestDAMitakaData();
            testDb02 = new TestDAMitakaData();

            // DBに存在しない
            testDb01.ExistResult = true;
            testDb02.ExistResult = true;
            // DB削除が成功とした想定にする
            testDb01.DeleteResult = true;
            testDb02.DeleteResult = true;

            #region SHOT01・02
            // インスタンス作成

            // 管理番号未設定
            shot01 = new MitakaAnswerData(testDb01);
            // 管理番号設定
            shot02 = new MitakaAnswerData(testDb02,
                TestSettings.TEST_PARAM_MANAGE_NO);

            // メソッド実行
            result1 = shot01.deleteMitakaAnswerData();
            result2 = shot02.deleteMitakaAnswerData();

            #region データ検証
            
            // インスタンス検証
            Assert.IsNotNull(shot01, "インスタンスが作成されていること");
            Assert.IsNotNull(shot02, "インスタンスが作成されていること");

            Assert.IsTrue(!result1, "SHOT01の削除結果が想定通りに返却されていること");
            Assert.IsNull(testDb01.deleteMitakaAnswerData_Receive_All,
                "SHOT01のDB処理「過去トラ観たか回答情報削除」まで到達していないこと");
            Assert.IsTrue(shot01.embeddedScript != "", "SHOT01の埋め込みスクリプトが取得できていること");

            Assert.IsTrue(result2, "SHOT02の登録結果が想定通りに返却されていること");
            Assert.IsNotNull(testDb02.deleteMitakaAnswerData_Receive_All,
                "SHOT02のDB処理「過去トラ観たか回答情報削除」まで到達していること");
            Assert.IsTrue(shot02.embeddedScript != "", "SHOT02の埋め込みスクリプトが取得できていること");
            #endregion

            #endregion

            #region SHOT03・04

            // 登録失敗ケース
            testDb01.DeleteResult = false;
            testDb02.DeleteResult = false;

            // メソッド実行
            result1 = shot01.deleteMitakaAnswerData();
            result2 = shot02.deleteMitakaAnswerData();

            #region データ検証
            // インスタンス検証
            Assert.IsNotNull(shot01, "インスタンスが作成されていること");
            Assert.IsNotNull(shot02, "インスタンスが作成されていること");

            // DB取得アクション確認
            Assert.IsTrue(!result1, "SHOT01の削除結果が想定通りに返却されていること");

            Assert.IsTrue(!result2, "SHOT02の削除結果が想定通りに返却されていること");

            #endregion

            #endregion
        }
    }
}