using Microsoft.VisualStudio.TestTools.UnitTesting;
using OldTigerWeb.DataAccess;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OldTigerWeb.BuisinessLogic;

namespace OldTigerWeb.DataAccess.Tests
{
    [TestClass()]
    public class DAMitakaDataTests
    {
        DAMitakaData testClass;

        [TestInitialize]
        public void DAMitakaDataTestInitialize()
        {
            string testUser = TestSettings.TEST_PARAM_LOGINUSER;   // ユーザーID
            testClass = new DAMitakaData(testUser);
        }


        #region コンストラクタ

        [TestMethod()]
        public void DAMitakaDataTest()
        {
            try
            {
                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(testClass, "クラスがNULLでないこと");
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                #endregion

                #endregion
            }
            catch
            {
                Assert.Fail();
            }
        }

        #endregion

        #region 管理番号リスト取得関連


        [TestMethod()]
        public void getMaxManageNoTest()
        {

            try
            {

                // テスト用パラメータ
                string testDivisionCode = TestSettings.TEST_PARAM_DIVISION_CODE1;   // ユーザーID（コンストラクタ引数）

                // メソッド実行
                var methodProccess = testClass.getMaxManageNo(testDivisionCode);    // 課コード

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                #endregion

                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }

        }

        [TestMethod()]
        public void getManageNoFromMitakaSearchDataTest()
        {
            try
            {
                // テスト用パラメータ
                MitakaSearchData mitakaSearchData = new MitakaSearchData(TestSettings.TEST_PARAM_LOGINUSER);   // 過去トラ観たか検索クラスインスタンス
                mitakaSearchData.Title = TestSettings.TEST_PARAM_TITLE;
                mitakaSearchData.ManageNo = TestSettings.TEST_PARAM_MANAGE_NO;
                mitakaSearchData.CreateDepartmentCode = TestSettings.TEST_PARAM_DIVISION_CODE1;
                mitakaSearchData.AnswerStartDateTimeDisp = TestSettings.TEST_PARAM_ANSWER_MONTH_START;
                mitakaSearchData.Status = TestSettings.TEST_PARAM_STATUS;
                mitakaSearchData.AnswerEndDateTimeDisp = TestSettings.TEST_PARAM_ANSWER_MONTH_END;
                mitakaSearchData.AnswerDepartmentCode = TestSettings.TEST_PARAM_DIVISION_CODE2;
                mitakaSearchData.DevelopmentCode = TestSettings.TEST_PARAM_DEVELOPMENT_CODE;
                mitakaSearchData.Model = TestSettings.TEST_PARAM_MODEL;
                mitakaSearchData.BlockNo = TestSettings.TEST_PARAM_BLKNO;
                mitakaSearchData.TitleDrawingNo = TestSettings.TEST_PARAM_TITLEDRAWINGNO;
                mitakaSearchData.EcsNo = TestSettings.TEST_PARAM_ECSNO;

                // メソッド実行
                var methodProccess = testClass.getManageNoFromMitakaSearchData(mitakaSearchData);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                // カラム「管理番号」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("MITAKA_NO"), "カラム「管理番号」が存在すること");
                #endregion

                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void getManageNoFromRelationUserTest()
        {
            try
            {
                // メソッド実行
                var methodProccess = testClass.getManageNoFromRelationUser();

                #region 

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                // カラム「管理番号」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("MITAKA_NO"), "カラム「管理番号」が存在すること");
                #endregion

                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        #endregion

        #region 過去トラ観たかヘッダー関連

        [TestMethod()]
        public void getMitakaHeaderDataTest()
        {
            try
            {
                //string manageNo = null;
                // メソッド実行
                var methodProccess = testClass.getMitakaHeaderData();

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                // カラム「管理番号」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("MITAKA_NO"), "カラム「管理番号」が存在すること");
                // カラム「管理部署コード１」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("MANAGE_DIVISION_CODE1"), "カラム「管理部署コード１」が存在すること");
                // カラム「管理部署コード２」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("MANAGE_DIVISION_CODE2"), "カラム「管理部署コード２」が存在すること");
                // カラム「タイトル」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("TITLE"), "カラム「タイトル」が存在すること");
                // カラム「目的」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("PURPOSE"), "カラム「目的」が存在すること");
                // カラム「コメント」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("COMMENT"), "カラム「コメント」が存在すること");
                // カラム「回答開始日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("START_YMD"), "カラム「回答開始日時」が存在すること");
                // カラム「回答終了日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("END_YMD"), "カラム「回答終了日時」が存在すること");
                // カラム「状況」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("STATUS"), "カラム「状況」が存在すること");
                // カラム「状況コメント」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("STATUS_COMMENT"), "カラム「状況コメント」が存在すること");
                // カラム「登録ユーザー」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("INSERT_USER"), "カラム「登録ユーザー」が存在すること");
                // カラム「更新ユーザー」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("UPDATE_USER"), "カラム「更新ユーザー」が存在すること");
                // カラム「登録日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("INSERT_YMD"), "カラム「登録日時」が存在すること");
                // カラム「更新日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("UPDATE_YMD"), "カラム「更新日時」が存在すること");
                // カラム「編集フラグ」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("EDIT_FLG"), "カラム「編集フラグ」が存在すること");
                #endregion



                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void existMitakaHeaderDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // メソッド実行
                var methodProccess = testClass.existMitakaHeaderData(manageNo);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                #endregion

                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void insertMitakaHeaderDataTest()
        {
            String manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getMitakaHeaderData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["MANAGE_DIVISION_CODE1"] = TestSettings.TEST_PARAM_DIVISION_CODE1;
                testField["MANAGE_DIVISION_CODE2"] = TestSettings.TEST_PARAM_DIVISION_CODE2;
                testField["TITLE"] = TestSettings.TEST_PARAM_TITLE;
                testField["PURPOSE"] = TestSettings.TEST_PARAM_PURPOSE;
                testField["COMMENT"] = TestSettings.TEST_PARAM_COMMENT;
                testField["END_YMD"] = TestSettings.TEST_PARAM_ANSWER_END;
                testField["STATUS_COMMENT"] = TestSettings.TEST_PARAM_STATUS_COMMENT;

                // テスト前処理
                testClass.deleteMitakaHeaderData(testField["MITAKA_NO"].ToString());

                // メソッド実行
                var methodProccess = testClass.insertMitakaHeaderData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                var getData = testClass.getMitakaHeaderData(TestSettings.TEST_PARAM_MANAGE_NO);
                Assert.IsTrue(getData.Rows.Count > 0, "データが正常に登録されていること");
                Assert.IsTrue(getData.Rows[0]["MITAKA_NO"].ToString() == testField["MITAKA_NO"].ToString(),
                    "管理番号が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["MANAGE_DIVISION_CODE1"].ToString() == testField["MANAGE_DIVISION_CODE1"].ToString(),
                    "管理部署コード１が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["MANAGE_DIVISION_CODE2"].ToString() == testField["MANAGE_DIVISION_CODE2"].ToString(),
                    "管理部署コード２が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["TITLE"].ToString() == testField["TITLE"].ToString(),
                    "タイトルが想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["PURPOSE"].ToString() == testField["PURPOSE"].ToString(),
                    "目的が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["COMMENT"].ToString() == testField["COMMENT"].ToString(),
                    "コメントが想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["START_YMD"].ToString()
                    , "回答開始日時が想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["END_YMD"].ToString()
                    , "回答終了日時が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["STATUS"].ToString() == Def.MITAKA_STATUS_PREPARATION,
                    "状況が想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["STATUS_COMMENT"],
                    "状況コメントが想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["INSERT_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                    "登録ユーザが想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["INSERT_YMD"],
                    "登録日時が登録されていること");
                Assert.IsTrue(getData.Rows[0]["UPDATE_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                    "更新ユーザが想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["UPDATE_YMD"],
                    "更新日時が登録されていること");
                #endregion
                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                // テスト後処理
                testClass.deleteMitakaHeaderData(manageNo);
            }
        }

        [TestMethod()]
        public void updateMitakaHeaderDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getMitakaHeaderData();
                var testField = testFormat.NewRow();

                testField["MITAKA_NO"] = manageNo;
                testField["MANAGE_DIVISION_CODE1"] = TestSettings.TEST_PARAM_DIVISION_CODE1;
                testField["MANAGE_DIVISION_CODE2"] = TestSettings.TEST_PARAM_DIVISION_CODE2;
                testField["TITLE"] = TestSettings.TEST_PARAM_TITLE;
                testField["PURPOSE"] = TestSettings.TEST_PARAM_PURPOSE;
                testField["COMMENT"] = TestSettings.TEST_PARAM_COMMENT;
                testField["START_YMD"] = TestSettings.TEST_PARAM_ANSWER_START;
                testField["END_YMD"] = TestSettings.TEST_PARAM_ANSWER_END;
                testField["STATUS"] = TestSettings.TEST_PARAM_STATUS;
                testField["STATUS_COMMENT"] = TestSettings.TEST_PARAM_STATUS_COMMENT;

                // テスト前処理
                if (!testClass.existMitakaHeaderData(testField["MITAKA_NO"].ToString()))
                {
                    testClass.insertMitakaHeaderData(testField);
                }
                // メソッド実行
                var methodProccess = testClass.updateMitakaHeaderData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                var getData = testClass.getMitakaHeaderData(TestSettings.TEST_PARAM_MANAGE_NO);
                Assert.IsTrue(getData.Rows.Count > 0, "データが正常に登録されていること（前提確認）");
                Assert.IsTrue(getData.Rows[0]["MITAKA_NO"].ToString() ==
                    testField["MITAKA_NO"].ToString(), "管理番号が想定通りに更新されていること");
                Assert.IsTrue(getData.Rows[0]["MANAGE_DIVISION_CODE1"].ToString() ==
                    testField["MANAGE_DIVISION_CODE1"].ToString(), "管理部署コード１が想定通りに更新されていること");
                Assert.IsTrue(getData.Rows[0]["MANAGE_DIVISION_CODE2"].ToString() ==
                    testField["MANAGE_DIVISION_CODE2"].ToString(), "管理部署コード２が想定通りに更新されていること");
                Assert.IsTrue(getData.Rows[0]["TITLE"].ToString() ==
                    testField["TITLE"].ToString(), "タイトルが想定通りに更新されていること");
                Assert.IsTrue(getData.Rows[0]["PURPOSE"].ToString() ==
                    testField["PURPOSE"].ToString(), "目的が想定通りに更新されていること");
                Assert.IsTrue(getData.Rows[0]["COMMENT"].ToString() ==
                    testField["COMMENT"].ToString(), "コメントが想定通りに更新されていること");
                Assert.IsNotNull(getData.Rows[0]["START_YMD"].ToString()
                    , "回答開始日時がNULLじゃないこと");
                Assert.IsNotNull(getData.Rows[0]["END_YMD"].ToString()
                    , "回答終了日時がNULLじゃないこと");
                Assert.IsTrue(getData.Rows[0]["STATUS"].ToString() ==
                    testField["STATUS"].ToString(), "状況が想定通りに更新されていること");
                Assert.IsNotNull(getData.Rows[0]["STATUS_COMMENT"],
                testField["STATUS_COMMENT"].ToString(), "状況コメントが想定通りに更新されていること");
                Assert.IsTrue(getData.Rows[0]["UPDATE_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                    "更新ユーザが想定通りに更新されていること");
                Assert.IsNotNull(getData.Rows[0]["UPDATE_YMD"].ToString()
                    , "更新日時がNULLじゃないこと");
                #endregion
                #endregion
            }

            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                // テスト後処理
                testClass.deleteMitakaHeaderData(manageNo);
            }
        }

        [TestMethod()]
        public void updateHeaderDataToStatusTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                string status = Def.MITAKA_STATUS_ANSWER;
                string reason = null;
                reason = "SHOT1";

                // テスト前処理
                if (!testClass.existMitakaHeaderData(manageNo))
                {
                    DataTable testFormat = testClass.getMitakaHeaderData();
                    var testField = testFormat.NewRow();
                    testField["MITAKA_NO"] = manageNo;
                    testField["MANAGE_DIVISION_CODE1"] = TestSettings.TEST_PARAM_DIVISION_CODE1;
                    testField["MANAGE_DIVISION_CODE2"] = TestSettings.TEST_PARAM_DIVISION_CODE2;
                    testField["TITLE"] = TestSettings.TEST_PARAM_TITLE;
                    testField["PURPOSE"] = TestSettings.TEST_PARAM_PURPOSE;
                    testField["COMMENT"] = TestSettings.TEST_PARAM_COMMENT;
                    testField["END_YMD"] = TestSettings.TEST_PARAM_ANSWER_END;
                    testField["STATUS_COMMENT"] = TestSettings.TEST_PARAM_STATUS_COMMENT;
                    testClass.insertMitakaHeaderData(testField);
                }

                var getData = testClass.getMitakaHeaderData(manageNo);
                Assert.IsTrue(getData.Rows.Count > 0,
                    "更新対象のテストデータが存在しない為、テスト終了");

                // メソッド実行
                testClass.updateHeaderDataToStatus(manageNo, status, reason);

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                getData = testClass.getMitakaHeaderData(manageNo);
                Assert.IsTrue(getData.Rows.Count > 0, "データが存在していること（前提確認）");
                Assert.IsTrue(getData.Rows[0]["STATUS"].ToString() == status,
                "状況が想定通りに更新されていること");
                Assert.IsTrue(getData.Rows[0]["STATUS_COMMENT"].ToString() == reason,
                "状況コメントが想定通りに更新されていないこと");
                Assert.IsTrue(getData.Rows[0]["UPDATE_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                "更新ユーザが想定通りに更新されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());

            }
            finally
            {
                // テスト後処理
                testClass.deleteMitakaHeaderData(manageNo);
            }
        }

        [TestMethod()]
        public void deleteMitakaHeaderDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト前処理
                if (!testClass.existMitakaHeaderData(manageNo))
                {
                    DataTable testFormat = testClass.getMitakaHeaderData();
                    var testField = testFormat.NewRow();
                    testField["MITAKA_NO"] = manageNo;
                    testField["MANAGE_DIVISION_CODE1"] = TestSettings.TEST_PARAM_DIVISION_CODE1;
                    testField["MANAGE_DIVISION_CODE2"] = TestSettings.TEST_PARAM_DIVISION_CODE2;
                    testField["TITLE"] = TestSettings.TEST_PARAM_TITLE;
                    testField["PURPOSE"] = TestSettings.TEST_PARAM_PURPOSE;
                    testField["COMMENT"] = TestSettings.TEST_PARAM_COMMENT;
                    testField["END_YMD"] = TestSettings.TEST_PARAM_ANSWER_END;
                    testField["STATUS_COMMENT"] = TestSettings.TEST_PARAM_STATUS_COMMENT;
                    testClass.insertMitakaHeaderData(testField);
                }

                // メソッド実行
                testClass.deleteMitakaHeaderData(manageNo);

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                var getData = testClass.getMitakaHeaderData(manageNo);
                Assert.IsTrue(getData.Rows.Count == 0, "データが削除されていること");
                Assert.IsTrue(!testClass.existMitakaHeaderData(manageNo), "データが削除されていること");

                #endregion
                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());

            }
        }

        [TestMethod()]
        public void postMitakaHeaderDataTest()
        {
            String manageNo = "TEST";

            try
            {
                // テスト用パラメータ
                DataTable testDt = testClass.getMitakaHeaderData();

                for (int i = 1; i <= 3; i++)
                {
                    var testField = testDt.NewRow();
                    testField["MITAKA_NO"] = manageNo;
                    testField["MANAGE_DIVISION_CODE1"] = "TEST1" + i.ToString();
                    testField["MANAGE_DIVISION_CODE2"] = "TEST2" + i.ToString();
                    testField["TITLE"] = "TITLE" + i.ToString();
                    testField["PURPOSE"] = "PURPOSE" + i.ToString();
                    testField["COMMENT"] = "COMMENT" + i.ToString();
                    testField["END_YMD"] = DateTime.Parse("2017-" + i.ToString("00") + "-01 23:59:59");
                    testField["STATUS"] = Def.MITAKA_STATUS_ANSWER;
                    testField["STATUS_COMMENT"] = "STATUS_COMENT" + i.ToString();
                    testField["EDIT_FLG"] = "1";
                    testDt.Rows.Add(testField);
                }
                // テスト前処理
                testClass.deleteMitakaHeaderData(manageNo);

                // メソッド実行
                testClass.postMitakaHeaderData(testDt);

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                #endregion
                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                // テスト後処理
                testClass.deleteMitakaHeaderData(manageNo);
            }
        }

        #endregion

        #region 関連ユーザー情報関連

        [TestMethod()]
        public void getReLationUserDataTest()
        {
            try
            {
                //string manageNo = null;
                // メソッド実行
                var methodProccess = testClass.getReLationUserData();

                #region データ検証
                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                // カラム「管理番号」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("MITAKA_NO"), "カラム「管理番号」が存在すること");
                // カラム「管理部署コード１」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("RELATION_TYPE"), "カラム「関連タイプ」が存在すること");
                // カラム「管理部署コード２」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("USER_ID"), "カラム「ユーザ」が存在すること");
                // カラム「登録ユーザー」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("INSERT_USER"), "カラム「登録ユーザー」が存在すること");
                // カラム「更新ユーザー」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("UPDATE_USER"), "カラム「更新ユーザー」が存在すること");
                // カラム「登録日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("INSERT_YMD"), "カラム「登録日時」が存在すること");
                // カラム「更新日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("UPDATE_YMD"), "カラム「更新日時」が存在すること");
                // カラム「編集フラグ」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("EDIT_FLG"), "カラム「編集フラグ」が存在すること");
                // カラム「氏名」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("USER_NAME"), "カラム「氏名」が存在すること");
                // カラム「メールアドレス」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("MAIL"), "カラム「メールアドレス」が存在すること");
                // カラム「部コード」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BU_CODE"), "カラム「部コード」が存在すること");
                // カラム「課コード」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("KA_CODE"), "カラム「課コード」が存在すること");
                // カラム「SQBフラグ」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SQB_FLG"), "カラム「SQBフラグ」が存在すること");
                #endregion

                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void existReLationUserDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;
            try
            {
                DataTable testFormat = testClass.getReLationUserData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["RELATION_TYPE"] = Def.MITAKA_RELATION_TYPE_CREATER_MAIN;
                testField["USER_ID"] = TestSettings.TEST_PARAM_RELATION_USER;
                testField["EDIT_FLG"] = "1";

                // メソッド実行
                var methodProccess = testClass.existReLationUserData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                #endregion

                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void insertReLationUserDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;
            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getReLationUserData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["RELATION_TYPE"] = Def.MITAKA_RELATION_TYPE_CREATER_MAIN;
                testField["USER_ID"] = TestSettings.TEST_PARAM_RELATION_USER;

                // テスト前処理
                testClass.deleteReLationUserData(testField["MITAKA_NO"].ToString());

                // メソッド実行
                var methodProccess = testClass.insertReLationUserData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                var getData = testClass.getReLationUserData(testField["MITAKA_NO"].ToString());
                Assert.IsTrue(getData.Rows.Count > 0, "データが正常に登録されていること");
                Assert.IsTrue(getData.Rows[0]["MITAKA_NO"].ToString() == testField["MITAKA_NO"].ToString(),
                    "管理番号が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["RELATION_TYPE"].ToString() == testField["RELATION_TYPE"].ToString(),
                    "関連タイプが想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["USER_ID"].ToString() == testField["USER_ID"].ToString(),
                    "ユーザーが想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["INSERT_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                    "登録ユーザが想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["INSERT_YMD"],
                    "登録日時が登録されていること");
                Assert.IsTrue(getData.Rows[0]["UPDATE_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                    "更新ユーザが想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["UPDATE_YMD"],
                    "更新日時が登録されていること");
                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                // テスト後処理
                testClass.deleteReLationUserData(manageNo);
            }
        }

        [TestMethod()]
        public void deleteReLationUserDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getReLationUserData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["RELATION_TYPE"] = Def.MITAKA_RELATION_TYPE_CREATER_MAIN;
                testField["USER_ID"] = TestSettings.TEST_PARAM_RELATION_USER;

                // テスト前処理
                if (!testClass.existReLationUserData(testField))
                {
                    testClass.insertReLationUserData(testField);
                }

                // メソッド実行
                testClass.deleteReLationUserData(testField);

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                Assert.IsTrue(!testClass.existReLationUserData(testField), "データが削除されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void deleteReLationUserDataTest1()
        {
            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getReLationUserData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = TestSettings.TEST_PARAM_MANAGE_NO;
                testField["RELATION_TYPE"] = Def.MITAKA_RELATION_TYPE_CREATER_MAIN;
                testField["USER_ID"] = TestSettings.TEST_PARAM_RELATION_USER;

                // テスト前処理
                if (!testClass.existReLationUserData(testField))
                {
                    testClass.insertReLationUserData(testField);
                }

                // メソッド実行
                testClass.deleteReLationUserData(testField["MITAKA_NO"].ToString());

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                Assert.IsTrue(!testClass.existReLationUserData(testField), "データが削除されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void postReLationUserDataTest()
        {
            String manageNo = "TEST";

            try
            {
                // テスト用パラメータ
                DataTable testDt = testClass.getReLationUserData();

                for (int i = 1; i <= 3; i++)
                {
                    var testField = testDt.NewRow();
                    testField["MITAKA_NO"] = manageNo;
                    testField["RELATION_TYPE"] = Def.MITAKA_RELATION_TYPE_REQUEST;
                    testField["USER_ID"] = "USER" + i.ToString();
                    testField["EDIT_FLG"] = i % 2;
                    testDt.Rows.Add(testField);
                }
                // テスト前処理
                testClass.deleteReLationUserData(manageNo);

                // メソッド実行
                testClass.postReLationUserData(testDt);
                // メソッド実行
                testClass.postReLationUserData(testDt);

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                #endregion
                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());

            }
            finally
            {
                // テスト後処理
                testClass.deleteReLationUserData(manageNo);
            }
        }

        #endregion

        #region 検索条件関連

        [TestMethod()]
        public void getSearchParameterDataTest()
        {
            try
            {
                //string manageNo = null;
                // メソッド実行
                var methodProccess = testClass.getSearchParameterData();

                #region データ検証
                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                // カラム「管理番号」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("MITAKA_NO"), "カラム「管理番号」が存在すること");
                // カラム「グループID」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("GROUP_ID"), "カラム「グループID」が存在すること");
                // カラム「検索種類」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SEARCH_TYPE"), "カラム「検索種類」が存在すること");
                // カラム「検索区分」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SEARCH_CLASS"), "カラム「検索区分」が存在すること");
                // カラム「検索区分」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SEARCH_PARAMETER"), "カラム「検索条件」が存在すること");
                // カラム「登録ユーザー」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("INSERT_USER"), "カラム「登録ユーザー」が存在すること");
                // カラム「更新ユーザー」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("UPDATE_USER"), "カラム「更新ユーザー」が存在すること");
                // カラム「登録日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("INSERT_YMD"), "カラム「登録日時」が存在すること");
                // カラム「更新日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("UPDATE_YMD"), "カラム「更新日時」が存在すること");
                // カラム「編集フラグ」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("EDIT_FLG"), "カラム「編集フラグ」が存在すること");
                #endregion

                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void existSearchParameterDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                DataTable testFormat = testClass.getSearchParameterData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["GROUP_ID"] = DateTime.Now.ToString("yyyyMMddhhmmss");
                testField["SEARCH_TYPE"] = TestSettings.TEST_PARAM_SEARCH_TYPE;
                testField["SEARCH_CLASS"] = TestSettings.TEST_PARAM_SEARCH_CLASS;
                testField["SEARCH_PARAMETER"] = TestSettings.TEST_PARAM_SEARCH_PARAMETER_KEYWORD;
                testField["EDIT_FLG"] = "1";

                // メソッド実行
                var methodProccess = testClass.existSearchParameterData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                #endregion
                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void insertSearchParameterDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getSearchParameterData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["SEARCH_TYPE"] = TestSettings.TEST_PARAM_SEARCH_TYPE;
                testField["SEARCH_CLASS"] = TestSettings.TEST_PARAM_SEARCH_CLASS;
                testField["SEARCH_PARAMETER"] = TestSettings.TEST_PARAM_SEARCH_PARAMETER_KEYWORD;

                // テスト前処理
                testClass.deleteSearchParameterData(testField["MITAKA_NO"].ToString());

                // メソッド実行
                var methodProccess = testClass.insertSearchParameterData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                var getData = testClass.getSearchParameterData(testField["MITAKA_NO"].ToString());
                Assert.IsTrue(getData.Rows.Count > 0, "データが正常に登録されていること");
                Assert.IsTrue(getData.Rows[0]["MITAKA_NO"].ToString() == testField["MITAKA_NO"].ToString(),
                    "管理番号が想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["GROUP_ID"].ToString(),
                    "グループIDが想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["SEARCH_TYPE"].ToString() == testField["SEARCH_TYPE"].ToString(),
                    "検索種類が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["SEARCH_CLASS"].ToString() == testField["SEARCH_CLASS"].ToString(),
                    "検索区分が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["SEARCH_PARAMETER"].ToString() == testField["SEARCH_PARAMETER"].ToString(),
                    "検索条件が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["INSERT_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                    "登録ユーザが想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["INSERT_YMD"],
                    "登録日時が登録されていること");
                Assert.IsTrue(getData.Rows[0]["UPDATE_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                    "更新ユーザが想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["UPDATE_YMD"],
                    "更新日時が登録されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                // テスト後処理
                testClass.deleteSearchParameterData(manageNo);
            }
        }

        [TestMethod()]
        public void deleteSearchParameterDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getSearchParameterData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["GROUP_ID"] = DateTime.Now.ToString("yyyyMMddhhmmss");
                testField["SEARCH_TYPE"] = TestSettings.TEST_PARAM_RELATION_USER;
                testField["SEARCH_CLASS"] = TestSettings.TEST_PARAM_RELATION_USER;
                testField["SEARCH_PARAMETER"] = TestSettings.TEST_PARAM_RELATION_USER;

                // テスト前処理
                if (!testClass.existSearchParameterData(testField))
                {
                    testClass.insertSearchParameterData(testField);
                }

                // メソッド実行
                testClass.deleteSearchParameterData(testField);

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                Assert.IsTrue(!testClass.existSearchParameterData(testField), "データが削除されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void deleteSearchParameterDataTest1()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;
            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getSearchParameterData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["GROUP_ID"] = DateTime.Now.ToString("yyyyMMddhhmmss");
                testField["SEARCH_TYPE"] = TestSettings.TEST_PARAM_SEARCH_TYPE;
                testField["SEARCH_CLASS"] = TestSettings.TEST_PARAM_SEARCH_CLASS;
                testField["SEARCH_PARAMETER"] = TestSettings.TEST_PARAM_SEARCH_PARAMETER_KEYWORD;

                // テスト前処理
                if (!testClass.existSearchParameterData(testField))
                {
                    testClass.insertSearchParameterData(testField);
                }

                // メソッド実行
                testClass.deleteSearchParameterData(testField["MITAKA_NO"].ToString());

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                Assert.IsTrue(!testClass.existSearchParameterData(testField), "データが削除されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void postSearchParameterDataTest()
        {
            String manageNo = TestSettings.TEST_PARAM_MANAGE_NO_ROOP;

            try
            {
                // テスト用パラメータ
                DataTable testDt = testClass.getSearchParameterData();
                string date = DateTime.Now.ToString("yyyyMMddhhmm");

                for (int i = 1; i <= 3; i++)
                {
                    var testField = testDt.NewRow();
                    testField["MITAKA_NO"] = TestSettings.TEST_PARAM_MANAGE_NO;
                    testField["GROUP_ID"] = date + i.ToString("00");
                    testField["SEARCH_TYPE"] = i.ToString();
                    testField["SEARCH_CLASS"] = i.ToString();
                    testField["SEARCH_PARAMETER"] = TestSettings.TEST_PARAM_SEARCH_PARAMETER_KEYWORD;
                    testField["EDIT_FLG"] = "1";
                    testDt.Rows.Add(testField);
                }
                // テスト前処理
                testClass.deleteSearchParameterData(manageNo);

                // メソッド実行
                testClass.postSearchParameterData(testDt);
                // メソッド実行
                testClass.postSearchParameterData(testDt);

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());

            }
            finally
            {
                // テスト後処理
                testClass.deleteSearchParameterData(manageNo);
            }
        }

        #endregion

        #region 展開対象関連

        [TestMethod()]
        public void getDeploymentTroubleDataTest()
        {
            try
            {
                //string manageNo = null;
                // メソッド実行
                var methodProccess = testClass.getDeploymentTroubleData();

                #region データ検証
                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                // カラム「管理番号」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("MITAKA_NO"), "カラム「管理番号」が存在すること");
                // カラム「システムNo」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SYSTEM_NO"), "カラム「システムNo」が存在すること");
                // カラム「対象フラグ」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("TARGET_FLG"), "カラム「対象フラグ」が存在すること");
                // カラム「登録ユーザ」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("INSERT_USER"), "カラム「登録ユーザ」が存在すること");
                // カラム「更新ユーザ」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("UPDATE_USER"), "カラム「更新ユーザ」が存在すること");
                // カラム「登録日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("INSERT_YMD"), "カラム「登録日時」が存在すること");
                // カラム「更新日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("UPDATE_YMD"), "カラム「更新日時」が存在すること");
                // カラム「BY/PU区分」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BY_PU"), "カラム「BY/PU区分」が存在すること");
                // カラム「項目管理番号」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("KOUMOKU_KANRI_NO"), "カラム「項目管理番号」が存在すること");
                // カラム「項目」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("KOUMOKU"), "カラム「項目」が存在すること");
                // カラム「ランク」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("RANK"), "カラム「ランク」が存在すること");
                // カラム「再発」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SAIHATU"), "カラム「再発」が存在すること");
                // カラム「RSC」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("RSC"), "カラム「RSC」が存在すること");
                // カラム「部品名」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUHIN_NAME"), "カラム「部品名」が存在すること");
                // カラム「開発符号１」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("FUGO_NAME1"), "カラム「開発符号１」が存在すること");
                // カラム「開発符号２」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("FUGO_NAME2"), "カラム「開発符号２」が存在すること");
                // カラム「開発符号３」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("FUGO_NAME3"), "カラム「開発符号３」が存在すること");
                // カラム「開発符号４」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("FUGO_NAME4"), "カラム「開発符号４」が存在すること");
                // カラム「開発符号５」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("FUGO_NAME5"), "カラム「開発符号５」が存在すること");
                // カラム「原因」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("GENIN"), "カラム「原因」が存在すること");
                // カラム「対策」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("TAISAKU"), "カラム「対策」が存在すること");
                // カラム「開発時の流出要因」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("KAIHATU_MIHAKKEN_RIYU"), "カラム「開発時の流出要因」が存在すること");
                // カラム「確認の観点」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SQB_KANTEN"), "カラム「確認の観点」が存在すること");
                // カラム「再発防止策（設計面）」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SAIHATU_SEKKEI"), "カラム「再発防止策（設計面）」が存在すること");
                // カラム「再発防止策（評価面）」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SAIHATU_HYOUKA"), "カラム「再発防止策（評価面）」が存在すること");
                // カラム「資料No１」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SIRYOU_NO1"), "カラム「資料No１」が存在すること");
                // カラム「資料No２」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SIRYOU_NO2"), "カラム「資料No２」が存在すること");
                // カラム「設通番号１」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SETTU_NO1"), "カラム「設通番号１」が存在すること");
                // カラム「設通番号２」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SETTU_NO2"), "カラム「設通番号２」が存在すること");
                // カラム「設通番号３」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SETTU_NO3"), "カラム「設通番号３」が存在すること");
                // カラム「設通番号４」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SETTU_NO4"), "カラム「設通番号４」が存在すること");
                // カラム「設通番号５」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SETTU_NO5"), "カラム「設通番号５」が存在すること");
                // カラム「設計部署１」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_SEKKEI1"), "カラム「設計部署１」が存在すること");
                // カラム「設計部署２」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_SEKKEI2"), "カラム「設計部署２」が存在すること");
                // カラム「設計部署３」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_SEKKEI3"), "カラム「設計部署３」が存在すること");
                // カラム「設計部署４」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_SEKKEI4"), "カラム「設計部署４」が存在すること");
                // カラム「設計部署５」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_SEKKEI5"), "カラム「設計部署５」が存在すること");
                // カラム「設計部署６」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_SEKKEI6"), "カラム「設計部署６」が存在すること");
                // カラム「設計部署７」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_SEKKEI7"), "カラム「設計部署７」が存在すること");
                // カラム「設計部署８」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_SEKKEI8"), "カラム「設計部署８」が存在すること");
                // カラム「設計部署９」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_SEKKEI9"), "カラム「設計部署９」が存在すること");
                // カラム「設計部署１０」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_SEKKEI10"), "カラム「設計部署１０」が存在すること");
                // カラム「評価部署１」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_HYOUKA1"), "カラム「評価部署１」が存在すること");
                // カラム「評価部署２」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_HYOUKA2"), "カラム「評価部署２」が存在すること");
                // カラム「評価部署３」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_HYOUKA3"), "カラム「評価部署３」が存在すること");
                // カラム「評価部署４」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_HYOUKA4"), "カラム「評価部署４」が存在すること");
                // カラム「評価部署５」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_HYOUKA5"), "カラム「評価部署５」が存在すること");
                // カラム「評価部署６」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_HYOUKA6"), "カラム「評価部署６」が存在すること");
                // カラム「評価部署７」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_HYOUKA7"), "カラム「評価部署７」が存在すること");
                // カラム「評価部署８」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_HYOUKA8"), "カラム「評価部署８」が存在すること");
                // カラム「評価部署９」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_HYOUKA9"), "カラム「評価部署９」が存在すること");
                // カラム「評価部署１０」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_HYOUKA10"), "カラム「評価部署１０」が存在すること");
                // カラム「編集フラグ」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("EDIT_FLG"), "カラム「編集フラグ」が存在すること");
                #endregion

                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void existDeploymentTroubleDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                DataTable testFormat = testClass.getDeploymentTroubleData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["SYSTEM_NO"] = TestSettings.TEST_PARAM_DEPLOY_SYSTEMNO;
                testField["EDIT_FLG"] = "1";

                // メソッド実行
                var methodProccess = testClass.existDeploymentTroubleData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                #endregion
                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void insertDeploymentTroubleDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getDeploymentTroubleData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["SYSTEM_NO"] = TestSettings.TEST_PARAM_DEPLOY_SYSTEMNO;
                testField["TARGET_FLG"] = "1";

                // テスト前処理
                testClass.deleteDeploymentTroubleData(testField["MITAKA_NO"].ToString());

                // メソッド実行
                var methodProccess = testClass.insertDeploymentTroubleData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                var getData = testClass.getDeploymentTroubleData(testField["MITAKA_NO"].ToString());
                Assert.IsTrue(getData.Rows.Count > 0, "データが正常に登録されていること");
                Assert.IsTrue(getData.Rows[0]["MITAKA_NO"].ToString() == testField["MITAKA_NO"].ToString(),
                    "管理番号が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["SYSTEM_NO"].ToString() == testField["SYSTEM_NO"].ToString(),
                "システムNoが想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["TARGET_FLG"].ToString() == testField["TARGET_FLG"].ToString(),
                "対象フラグが想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["INSERT_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                "登録ユーザが想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["INSERT_YMD"],
                "登録日時が登録されていること");
                Assert.IsTrue(getData.Rows[0]["UPDATE_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                "更新ユーザが想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["UPDATE_YMD"],
                "更新日時が登録されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                // テスト後処理
                testClass.deleteDeploymentTroubleData(manageNo);
            }
        }

        [TestMethod()]
        public void updateDeploymentTroubleDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getDeploymentTroubleData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["SYSTEM_NO"] = TestSettings.TEST_PARAM_DEPLOY_SYSTEMNO;

                // テスト前処理
                if (!testClass.existDeploymentTroubleData(testField))
                {
                    testField["TARGET_FLG"] = "0";
                    testClass.insertDeploymentTroubleData(testField);
                }

                testField["TARGET_FLG"] = "1";

                // メソッド実行
                var methodProccess = testClass.updateDeploymentTroubleData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                var getData = testClass.getDeploymentTroubleData(testField["MITAKA_NO"].ToString());
                Assert.IsTrue(getData.Rows.Count > 0, "データが正常に登録されていること");
                Assert.IsTrue(getData.Rows[0]["MITAKA_NO"].ToString() == testField["MITAKA_NO"].ToString(),
                    "管理番号が想定通りの値であること");
                Assert.IsTrue(getData.Rows[0]["SYSTEM_NO"].ToString() == testField["SYSTEM_NO"].ToString(),
                "システムNoが想定通りの値であること");
                Assert.IsTrue(getData.Rows[0]["TARGET_FLG"].ToString() == testField["TARGET_FLG"].ToString(),
                "対象フラグが想定通りに更新されていること");
                Assert.IsNotNull(getData.Rows[0]["INSERT_USER"].ToString(),
                "登録ユーザがNULLでないこと");
                Assert.IsNotNull(getData.Rows[0]["INSERT_YMD"],
                "登録日時がNULLでないこと");
                Assert.IsTrue(getData.Rows[0]["UPDATE_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                "更新ユーザが想定通りに更新されていること");
                Assert.IsNotNull(getData.Rows[0]["UPDATE_YMD"],
                "更新日時がNULLでないこと");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                // テスト後処理
                testClass.deleteDeploymentTroubleData(manageNo);
            }
        }

        [TestMethod()]
        public void deleteDeploymentTroubleDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getDeploymentTroubleData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["SYSTEM_NO"] = TestSettings.TEST_PARAM_DEPLOY_SYSTEMNO;

                // テスト前処理
                if (!testClass.existDeploymentTroubleData(testField))
                {
                    testClass.insertDeploymentTroubleData(testField);
                }

                // メソッド実行
                testClass.deleteDeploymentTroubleData(testField);

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                Assert.IsTrue(!testClass.existDeploymentTroubleData(testField), "データが削除されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void deleteDeploymentTroubleDataTest1()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getDeploymentTroubleData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["SYSTEM_NO"] = TestSettings.TEST_PARAM_DEPLOY_SYSTEMNO;

                // テスト前処理
                if (!testClass.existDeploymentTroubleData(testField))
                {
                    testClass.insertDeploymentTroubleData(testField);
                }

                // メソッド実行
                testClass.deleteDeploymentTroubleData(testField["MITAKA_NO"].ToString());

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                Assert.IsTrue(!testClass.existDeploymentTroubleData(testField), "データが削除されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void postDeploymentTroubleDataTest()
        {
            String manageNo = TestSettings.TEST_PARAM_MANAGE_NO_ROOP;

            try
            {
                // テスト用パラメータ
                DataTable testDt = testClass.getDeploymentTroubleData();

                for (int i = 1; i <= 3; i++)
                {
                    var testField = testDt.NewRow();
                    testField["MITAKA_NO"] = TestSettings.TEST_PARAM_MANAGE_NO;
                    testField["SYSTEM_NO"] = TestSettings.TEST_PARAM_DEPLOY_SYSTEMNO;
                    testField["TARGET_FLG"] = (i % 2).ToString();
                    testField["EDIT_FLG"] = "1";
                    testDt.Rows.Add(testField);
                }
                // テスト前処理
                testClass.deleteDeploymentTroubleData(manageNo);

                // メソッド実行
                testClass.postDeploymentTroubleData(testDt);
                // メソッド実行
                testClass.postDeploymentTroubleData(testDt);

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());

            }
            finally
            {
                // テスト後処理
                testClass.deleteDeploymentTroubleData(manageNo);
            }
        }

        #endregion

        #region タイトル品番情報関連

        [TestMethod()]
        public void getTitleDrawingDataTest()
        {
            try
            {
                //string manageNo = null;
                // メソッド実行
                var methodProccess = testClass.getTitleDrawingData();

                #region データ検証
                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                // カラム「管理番号」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("MITAKA_NO"), "カラム「管理番号」が存在すること");
                // カラム「タイトル品番」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("TITLE_DRAWING_NO"), "カラム「タイトル品番」が存在すること");
                // カラム「登録ユーザー」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("INSERT_USER"), "カラム「登録ユーザー」が存在すること");
                // カラム「更新ユーザー」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("UPDATE_USER"), "カラム「更新ユーザー」が存在すること");
                // カラム「登録日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("INSERT_YMD"), "カラム「登録日時」が存在すること");
                // カラム「更新日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("UPDATE_YMD"), "カラム「更新日時」が存在すること");
                // カラム「編集フラグ」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("EDIT_FLG"), "カラム「編集フラグ」が存在すること");
                #endregion

                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void existTitleDrawingDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                DataTable testFormat = testClass.getTitleDrawingData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["TITLE_DRAWING_NO"] = TestSettings.TEST_PARAM_DRAWING_NO;
                testField["EDIT_FLG"] = "11";

                // メソッド実行
                var methodProccess = testClass.existTitleDrawingData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                #endregion
                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void insertTitleDrawingDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getTitleDrawingData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["TITLE_DRAWING_NO"] = TestSettings.TEST_PARAM_DRAWING_NO;

                // テスト前処理
                testClass.deleteTitleDrawingData(testField["MITAKA_NO"].ToString());

                // メソッド実行
                var methodProccess = testClass.insertTitleDrawingData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                var getData = testClass.getTitleDrawingData(testField["MITAKA_NO"].ToString());
                Assert.IsTrue(getData.Rows.Count > 0, "データが正常に登録されていること");
                Assert.IsTrue(getData.Rows[0]["MITAKA_NO"].ToString() == testField["MITAKA_NO"].ToString(),
                    "管理番号が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["TITLE_DRAWING_NO"].ToString() == testField["TITLE_DRAWING_NO"].ToString(),
                "タイトル品番が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["INSERT_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                "登録ユーザが想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["INSERT_YMD"],
                "登録日時が登録されていること");
                Assert.IsTrue(getData.Rows[0]["UPDATE_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                "更新ユーザが想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["UPDATE_YMD"],
                "更新日時が登録されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                // テスト後処理
                testClass.deleteTitleDrawingData(manageNo);
            }
        }

        [TestMethod()]
        public void deleteTitleDrawingDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getTitleDrawingData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["TITLE_DRAWING_NO"] = TestSettings.TEST_PARAM_DRAWING_NO;

                // テスト前処理
                if (!testClass.existTitleDrawingData(testField))
                {
                    testClass.insertTitleDrawingData(testField);
                }

                // メソッド実行
                testClass.deleteTitleDrawingData(testField);

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                Assert.IsTrue(!testClass.existTitleDrawingData(testField), "データが削除されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void deleteTitleDrawingDataTest1()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getTitleDrawingData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["TITLE_DRAWING_NO"] = TestSettings.TEST_PARAM_DRAWING_NO;

                // テスト前処理
                if (!testClass.existTitleDrawingData(testField))
                {
                    testClass.insertTitleDrawingData(testField);
                }

                // メソッド実行
                testClass.deleteTitleDrawingData(testField["MITAKA_NO"].ToString());

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                Assert.IsTrue(!testClass.existTitleDrawingData(testField), "データが削除されていること");
                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void postTitleDrawingDataTest()
        {
            String manageNo = TestSettings.TEST_PARAM_MANAGE_NO_ROOP;

            try
            {
                // テスト用パラメータ
                DataTable testDt = testClass.getTitleDrawingData();

                for (int i = 1; i <= 3; i++)
                {
                    var testField = testDt.NewRow();
                    testField["MITAKA_NO"] = TestSettings.TEST_PARAM_MANAGE_NO;
                    testField["TITLE_DRAWING_NO"] = TestSettings.TEST_PARAM_TITLEDRAWINGNO;
                    testField["EDIT_FLG"] = "1";
                    testDt.Rows.Add(testField);
                }
                // テスト前処理
                testClass.deleteTitleDrawingData(manageNo);

                // メソッド実行
                testClass.postTitleDrawingData(testDt);
                // メソッド実行
                testClass.postTitleDrawingData(testDt);

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());

            }
            finally
            {
                // テスト後処理
                testClass.deleteTitleDrawingData(manageNo);
            }
        }

        #endregion

        #region 機種情報関連

        [TestMethod()]
        public void getModelDataTest()
        {
            try
            {
                //string manageNo = null;
                // メソッド実行
                var methodProccess = testClass.getModelData();

                #region データ検証
                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                // カラム「管理番号」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("MITAKA_NO"), "カラム「管理番号」が存在すること");
                // カラム「機種」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("MODEL"), "カラム「機種」が存在すること");
                // カラム「登録ユーザー」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("INSERT_USER"), "カラム「登録ユーザー」が存在すること");
                // カラム「更新ユーザー」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("UPDATE_USER"), "カラム「更新ユーザー」が存在すること");
                // カラム「登録日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("INSERT_YMD"), "カラム「登録日時」が存在すること");
                // カラム「更新日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("UPDATE_YMD"), "カラム「更新日時」が存在すること");
                // カラム「編集フラグ」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("EDIT_FLG"), "カラム「編集フラグ」が存在すること");
                #endregion

                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void existModelDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                DataTable testFormat = testClass.getModelData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["MODEL"] = TestSettings.TEST_PARAM_MODEL_NO;
                testField["EDIT_FLG"] = "1";

                // メソッド実行
                var methodProccess = testClass.existModelData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                #endregion
                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void insertModelDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getModelData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["MODEL"] = TestSettings.TEST_PARAM_MODEL_NO;

                // テスト前処理
                testClass.deleteModelData(testField["MITAKA_NO"].ToString());

                // メソッド実行
                var methodProccess = testClass.insertModelData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                var getData = testClass.getModelData(testField["MITAKA_NO"].ToString());
                Assert.IsTrue(getData.Rows.Count > 0, "データが正常に登録されていること");
                Assert.IsTrue(getData.Rows[0]["MITAKA_NO"].ToString() == testField["MITAKA_NO"].ToString(),
                    "管理番号が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["MODEL"].ToString() == testField["MODEL"].ToString(),
                "機種が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["INSERT_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                "登録ユーザが想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["INSERT_YMD"],
                "登録日時が登録されていること");
                Assert.IsTrue(getData.Rows[0]["UPDATE_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                "更新ユーザが想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["UPDATE_YMD"],
                "更新日時が登録されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                // テスト後処理
                testClass.deleteModelData(manageNo);
            }
        }

        [TestMethod()]
        public void deleteModelDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getModelData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["MODEL"] = TestSettings.TEST_PARAM_MODEL_NO;

                // テスト前処理
                if (!testClass.existModelData(testField))
                {
                    testClass.insertModelData(testField);
                }

                // メソッド実行
                testClass.deleteModelData(testField);

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                Assert.IsTrue(!testClass.existModelData(testField), "データが削除されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void deleteModelDataTest1()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getModelData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["MODEL"] = TestSettings.TEST_PARAM_MODEL_NO;

                // テスト前処理
                if (!testClass.existModelData(testField))
                {
                    testClass.insertModelData(testField);
                }

                // メソッド実行
                testClass.deleteModelData(testField["MITAKA_NO"].ToString());

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                Assert.IsTrue(!testClass.existModelData(testField), "データが削除されていること");
                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void postModelDataTest()
        {
            String manageNo = TestSettings.TEST_PARAM_MANAGE_NO_ROOP;

            try
            {
                // テスト用パラメータ
                DataTable testDt = testClass.getModelData();

                for (int i = 1; i <= 3; i++)
                {
                    var testField = testDt.NewRow();
                    testField["MITAKA_NO"] = TestSettings.TEST_PARAM_MANAGE_NO;
                    testField["MODEL"] = TestSettings.TEST_PARAM_MODEL_NO;
                    testField["EDIT_FLG"] = "1";
                    testDt.Rows.Add(testField);
                }
                // テスト前処理
                testClass.deleteModelData(manageNo);

                // メソッド実行
                testClass.postModelData(testDt);
                // メソッド実行
                testClass.postModelData(testDt);

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());

            }
            finally
            {
                // テスト後処理
                testClass.deleteModelData(manageNo);
            }
        }

        #endregion

        #region BLK情報関連

        [TestMethod()]
        public void getBlockDataTest()
        {
            try
            {
                //string manageNo = null;
                // メソッド実行
                var methodProccess = testClass.getBlockData();

                #region データ検証
                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                // カラム「管理番号」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("MITAKA_NO"), "カラム「管理番号」が存在すること");
                // カラム「BLK NO」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BLK_NO"), "カラム「BLK No」が存在すること");
                // カラム「登録ユーザー」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("INSERT_USER"), "カラム「登録ユーザー」が存在すること");
                // カラム「更新ユーザー」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("UPDATE_USER"), "カラム「更新ユーザー」が存在すること");
                // カラム「登録日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("INSERT_YMD"), "カラム「登録日時」が存在すること");
                // カラム「更新日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("UPDATE_YMD"), "カラム「更新日時」が存在すること");
                // カラム「編集フラグ」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("EDIT_FLG"), "カラム「編集フラグ」が存在すること");
                #endregion

                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void existBlockDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                DataTable testFormat = testClass.getBlockData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["BLK_NO"] = TestSettings.TEST_PARAM_BLOCK_NO;
                testField["EDIT_FLG"] = "1";

                // メソッド実行
                var methodProccess = testClass.existBlockData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                #endregion
                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void insertBlockDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getBlockData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["BLK_NO"] = TestSettings.TEST_PARAM_BLOCK_NO;

                // テスト前処理
                testClass.deleteBlockData(testField["MITAKA_NO"].ToString());

                // メソッド実行
                var methodProccess = testClass.insertBlockData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                var getData = testClass.getBlockData(testField["MITAKA_NO"].ToString());
                Assert.IsTrue(getData.Rows.Count > 0, "データが正常に登録されていること");
                Assert.IsTrue(getData.Rows[0]["MITAKA_NO"].ToString() == testField["MITAKA_NO"].ToString(),
                    "管理番号が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["BLK_NO"].ToString() == testField["BLK_NO"].ToString(),
                "BLK Noが想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["INSERT_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                "登録ユーザが想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["INSERT_YMD"],
                "登録日時が登録されていること");
                Assert.IsTrue(getData.Rows[0]["UPDATE_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                "更新ユーザが想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["UPDATE_YMD"],
                "更新日時が登録されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                // テスト後処理
                testClass.deleteBlockData(manageNo);
            }
        }

        [TestMethod()]
        public void deleteBlockDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getBlockData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["BLK_NO"] = TestSettings.TEST_PARAM_BLOCK_NO;

                // テスト前処理
                if (!testClass.existBlockData(testField))
                {
                    testClass.insertBlockData(testField);
                }

                // メソッド実行
                testClass.deleteBlockData(testField);

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                Assert.IsTrue(!testClass.existBlockData(testField), "データが削除されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void deleteBlockDataTest1()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getBlockData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["BLK_NO"] = TestSettings.TEST_PARAM_BLOCK_NO;

                // テスト前処理
                if (!testClass.existBlockData(testField))
                {
                    testClass.insertBlockData(testField);
                }

                // メソッド実行
                testClass.deleteBlockData(testField["MITAKA_NO"].ToString());

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                Assert.IsTrue(!testClass.existBlockData(testField), "データが削除されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void postBlockDataTest()
        {
            String manageNo = TestSettings.TEST_PARAM_MANAGE_NO_ROOP;

            try
            {
                // テスト用パラメータ
                DataTable testDt = testClass.getBlockData();

                for (int i = 1; i <= 3; i++)
                {
                    var testField = testDt.NewRow();
                    testField["MITAKA_NO"] = TestSettings.TEST_PARAM_MANAGE_NO;
                    testField["BLK_NO"] = TestSettings.TEST_PARAM_BLOCK_NO;
                    testField["EDIT_FLG"] = "1";
                    testDt.Rows.Add(testField);
                }
                // テスト前処理
                testClass.deleteBlockData(manageNo);

                // メソッド実行
                testClass.postBlockData(testDt);
                // メソッド実行
                testClass.postBlockData(testDt);

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());

            }
            finally
            {
                // テスト後処理
                testClass.deleteBlockData(manageNo);
            }
        }

        #endregion

        #region 開発符号情報

        [TestMethod()]
        public void getDevelopmentCodeDataTest()
        {
            try
            {
                //string manageNo = null;
                // メソッド実行
                var methodProccess = testClass.getDevelopmentCodeData();

                #region データ検証
                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                // カラム「管理番号」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("MITAKA_NO"), "カラム「管理番号」が存在すること");
                // カラム「開発符号」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("DEVELOPMENT_CODE"), "カラム「開発符号」が存在すること");
                // カラム「登録ユーザー」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("INSERT_USER"), "カラム「登録ユーザー」が存在すること");
                // カラム「更新ユーザー」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("UPDATE_USER"), "カラム「更新ユーザー」が存在すること");
                // カラム「登録日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("INSERT_YMD"), "カラム「登録日時」が存在すること");
                // カラム「更新日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("UPDATE_YMD"), "カラム「更新日時」が存在すること");
                // カラム「編集フラグ」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("EDIT_FLG"), "カラム「編集フラグ」が存在すること");
                #endregion

                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void existDevelopmentCodeDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                DataTable testFormat = testClass.getDevelopmentCodeData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["DEVELOPMENT_CODE"] = TestSettings.TEST_PARAM_DEVELOP_CODE;
                testField["EDIT_FLG"] = "1";

                // メソッド実行
                var methodProccess = testClass.existDevelopmentCodeData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                #endregion
                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void insertDevelopmentCodeDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getDevelopmentCodeData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["DEVELOPMENT_CODE"] = TestSettings.TEST_PARAM_DEVELOP_CODE;

                // テスト前処理
                testClass.deleteDevelopmentCodeData(testField["MITAKA_NO"].ToString());

                // メソッド実行
                var methodProccess = testClass.insertDevelopmentCodeData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                var getData = testClass.getDevelopmentCodeData(testField["MITAKA_NO"].ToString());
                Assert.IsTrue(getData.Rows.Count > 0, "データが正常に登録されていること");
                Assert.IsTrue(getData.Rows[0]["MITAKA_NO"].ToString() == testField["MITAKA_NO"].ToString(),
                    "管理番号が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["DEVELOPMENT_CODE"].ToString() == testField["DEVELOPMENT_CODE"].ToString(),
                "開発符号が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["INSERT_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                "登録ユーザが想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["INSERT_YMD"],
                "登録日時が登録されていること");
                Assert.IsTrue(getData.Rows[0]["UPDATE_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                "更新ユーザが想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["UPDATE_YMD"],
                "更新日時が登録されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                // テスト後処理
                testClass.deleteDevelopmentCodeData(manageNo);
            }
        }

        [TestMethod()]
        public void deleteDevelopmentCodeDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getDevelopmentCodeData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["DEVELOPMENT_CODE"] = TestSettings.TEST_PARAM_DEVELOP_CODE;

                // テスト前処理
                if (!testClass.existDevelopmentCodeData(testField))
                {
                    testClass.insertDevelopmentCodeData(testField);
                }

                // メソッド実行
                testClass.deleteDevelopmentCodeData(testField);

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                Assert.IsTrue(!testClass.existDevelopmentCodeData(testField), "データが削除されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void deleteDevelopmentCodeDataTest1()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getDevelopmentCodeData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["DEVELOPMENT_CODE"] = TestSettings.TEST_PARAM_DEVELOP_CODE;

                // テスト前処理
                if (!testClass.existDevelopmentCodeData(testField))
                {
                    testClass.insertDevelopmentCodeData(testField);
                }

                // メソッド実行
                testClass.deleteDevelopmentCodeData(testField["MITAKA_NO"].ToString());

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                Assert.IsTrue(!testClass.existDevelopmentCodeData(testField), "データが削除されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void postDevelopmentCodeDataTest()
        {
            String manageNo = TestSettings.TEST_PARAM_MANAGE_NO_ROOP;

            try
            {
                // テスト用パラメータ
                DataTable testDt = testClass.getDevelopmentCodeData();

                for (int i = 1; i <= 3; i++)
                {
                    var testField = testDt.NewRow();
                    testField["MITAKA_NO"] = TestSettings.TEST_PARAM_MANAGE_NO;
                    testField["DEVELOPMENT_CODE"] = TestSettings.TEST_PARAM_DEVELOP_CODE;
                    testField["EDIT_FLG"] = "1";
                    testDt.Rows.Add(testField);
                }
                // テスト前処理
                testClass.deleteDevelopmentCodeData(manageNo);

                // メソッド実行
                testClass.postDevelopmentCodeData(testDt);
                // メソッド実行
                testClass.postDevelopmentCodeData(testDt);

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());

            }
            finally
            {
                // テスト後処理
                testClass.deleteDevelopmentCodeData(manageNo);
            }
        }

        #endregion

        #region 設通情報関連

        [TestMethod()]
        public void getEcsDataTest()
        {
            try
            {
                //string manageNo = null;
                // メソッド実行
                var methodProccess = testClass.getEcsData();

                #region データ検証
                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                // カラム「管理番号」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("MITAKA_NO"), "カラム「管理番号」が存在すること");
                // カラム「設通番号」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("ECS_NO"), "カラム「設通番号」が存在すること");
                // カラム「登録ユーザー」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("INSERT_USER"), "カラム「登録ユーザー」が存在すること");
                // カラム「更新ユーザー」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("UPDATE_USER"), "カラム「更新ユーザー」が存在すること");
                // カラム「登録日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("INSERT_YMD"), "カラム「登録日時」が存在すること");
                // カラム「更新日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("UPDATE_YMD"), "カラム「更新日時」が存在すること");
                // カラム「編集フラグ」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("EDIT_FLG"), "カラム「編集フラグ」が存在すること");
                #endregion

                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void existEcsDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                DataTable testFormat = testClass.getEcsData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["ECS_NO"] = TestSettings.TEST_PARAM_ECS_NO;
                testField["EDIT_FLG"] = "1";

                // メソッド実行
                var methodProccess = testClass.existEcsData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                #endregion
                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void insertEcsDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getEcsData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["ECS_NO"] = TestSettings.TEST_PARAM_ECS_NO;

                // テスト前処理
                testClass.deleteEcsData(testField["MITAKA_NO"].ToString());

                // メソッド実行
                var methodProccess = testClass.insertEcsData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                var getData = testClass.getEcsData(testField["MITAKA_NO"].ToString());
                Assert.IsTrue(getData.Rows.Count > 0, "データが正常に登録されていること");
                Assert.IsTrue(getData.Rows[0]["MITAKA_NO"].ToString() == testField["MITAKA_NO"].ToString(),
                    "管理番号が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["ECS_NO"].ToString() == testField["ECS_NO"].ToString(),
                "設通番号が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["INSERT_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                "登録ユーザが想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["INSERT_YMD"],
                "登録日時が登録されていること");
                Assert.IsTrue(getData.Rows[0]["UPDATE_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                "更新ユーザが想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["UPDATE_YMD"],
                "更新日時が登録されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                // テスト後処理
                testClass.deleteEcsData(manageNo);
            }
        }

        [TestMethod()]
        public void deleteEcsDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getEcsData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["ECS_NO"] = TestSettings.TEST_PARAM_ECS_NO;

                // テスト前処理
                if (!testClass.existEcsData(testField))
                {
                    testClass.insertEcsData(testField);
                }

                // メソッド実行
                testClass.deleteEcsData(testField);

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                Assert.IsTrue(!testClass.existEcsData(testField), "データが削除されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void deleteEcsDataTest1()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getEcsData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["ECS_NO"] = TestSettings.TEST_PARAM_ECS_NO;

                // テスト前処理
                if (!testClass.existEcsData(testField))
                {
                    testClass.insertEcsData(testField);
                }

                // メソッド実行
                testClass.deleteEcsData(testField["MITAKA_NO"].ToString());

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                Assert.IsTrue(!testClass.existEcsData(testField), "データが削除されていること");
                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void postEcsDataTest()
        {
            String manageNo = TestSettings.TEST_PARAM_MANAGE_NO_ROOP;

            try
            {
                // テスト用パラメータ
                DataTable testDt = testClass.getEcsData();

                for (int i = 1; i <= 3; i++)
                {
                    var testField = testDt.NewRow();
                    testField["MITAKA_NO"] = TestSettings.TEST_PARAM_MANAGE_NO;
                    testField["ECS_NO"] = TestSettings.TEST_PARAM_ECS_NO;
                    testField["EDIT_FLG"] = "1";
                    testDt.Rows.Add(testField);
                }
                // テスト前処理
                testClass.deleteEcsData(manageNo);

                // メソッド実行
                testClass.postEcsData(testDt);
                // メソッド実行
                testClass.postEcsData(testDt);

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());

            }
            finally
            {
                // テスト後処理
                testClass.deleteEcsData(manageNo);
            }
        }

        #endregion

        #region 過去トラ観たか回答関連

        [TestMethod()]
        public void getMitakaAnswerDataTest()
        {
            try
            {
                //string manageNo = null;
                // メソッド実行
                var methodProccess = testClass.getMitakaAnswerData();

                #region データ検証
                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                // カラム「管理番号」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("MITAKA_NO"), "カラム「管理番号」が存在すること");
                // カラム「システムNo」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SYSTEM_NO"), "カラム「システムNo」が存在すること");
                // カラム「回答対象部署コード」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("ANSWER_DIVISION_CODE"), "カラム「回答対象部署コード」が存在すること");
                // カラム「進捗状況」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("STATUS"), "カラム「進捗状況」が存在すること");
                // カラム「回答内容」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("ANSWER_CONTENT"), "カラム「回答内容」が存在すること");
                // カラム「対象フラグ」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("TARGET_FLG"), "カラム「対象フラグ」が存在すること");
                // カラム「登録ユーザ」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("INSERT_USER"), "カラム「登録ユーザ」が存在すること");
                // カラム「更新ユーザ」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("UPDATE_USER"), "カラム「更新ユーザ」が存在すること");
                // カラム「登録日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("INSERT_YMD"), "カラム「登録日時」が存在すること");
                // カラム「更新日時」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("UPDATE_YMD"), "カラム「更新日時」が存在すること");
                // カラム「編集フラグ」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("EDIT_FLG"), "カラム「編集フラグ」が存在すること");
                // カラム「BY/PU区分」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BY_PU"), "カラム「BY/PU区分」が存在すること");
                // カラム「項目管理番号」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("KOUMOKU_KANRI_NO"), "カラム「項目管理番号」が存在すること");
                // カラム「項目」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("KOUMOKU"), "カラム「項目」が存在すること");
                // カラム「ランク」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("RANK"), "カラム「ランク」が存在すること");
                // カラム「再発」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SAIHATU"), "カラム「再発」が存在すること");
                // カラム「RSC」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("RSC"), "カラム「RSC」が存在すること");
                // カラム「部品名」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUHIN_NAME"), "カラム「部品名」が存在すること");
                // カラム「開発符号１」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("FUGO_NAME1"), "カラム「開発符号１」が存在すること");
                // カラム「開発符号２」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("FUGO_NAME2"), "カラム「開発符号２」が存在すること");
                // カラム「開発符号３」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("FUGO_NAME3"), "カラム「開発符号３」が存在すること");
                // カラム「開発符号４」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("FUGO_NAME4"), "カラム「開発符号４」が存在すること");
                // カラム「開発符号５」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("FUGO_NAME5"), "カラム「開発符号５」が存在すること");
                // カラム「原因」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("GENIN"), "カラム「原因」が存在すること");
                // カラム「対策」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("TAISAKU"), "カラム「対策」が存在すること");
                // カラム「開発時の流出要因」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("KAIHATU_MIHAKKEN_RIYU"), "カラム「開発時の流出要因」が存在すること");
                // カラム「確認の観点」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SQB_KANTEN"), "カラム「確認の観点」が存在すること");
                // カラム「再発防止策（設計面）」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SAIHATU_SEKKEI"), "カラム「再発防止策（設計面）」が存在すること");
                // カラム「再発防止策（評価面）」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SAIHATU_HYOUKA"), "カラム「再発防止策（評価面）」が存在すること");
                // カラム「資料No１」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SIRYOU_NO1"), "カラム「資料No１」が存在すること");
                // カラム「資料No２」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SIRYOU_NO2"), "カラム「資料No２」が存在すること");
                // カラム「設通番号１」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SETTU_NO1"), "カラム「設通番号１」が存在すること");
                // カラム「設通番号２」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SETTU_NO2"), "カラム「設通番号２」が存在すること");
                // カラム「設通番号３」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SETTU_NO3"), "カラム「設通番号３」が存在すること");
                // カラム「設通番号４」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SETTU_NO4"), "カラム「設通番号４」が存在すること");
                // カラム「設通番号５」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("SETTU_NO5"), "カラム「設通番号５」が存在すること");
                // カラム「設計部署１」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_SEKKEI1"), "カラム「設計部署１」が存在すること");
                // カラム「設計部署２」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_SEKKEI2"), "カラム「設計部署２」が存在すること");
                // カラム「設計部署３」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_SEKKEI3"), "カラム「設計部署３」が存在すること");
                // カラム「設計部署４」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_SEKKEI4"), "カラム「設計部署４」が存在すること");
                // カラム「設計部署５」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_SEKKEI5"), "カラム「設計部署５」が存在すること");
                // カラム「設計部署６」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_SEKKEI6"), "カラム「設計部署６」が存在すること");
                // カラム「設計部署７」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_SEKKEI7"), "カラム「設計部署７」が存在すること");
                // カラム「設計部署８」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_SEKKEI8"), "カラム「設計部署８」が存在すること");
                // カラム「設計部署９」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_SEKKEI9"), "カラム「設計部署９」が存在すること");
                // カラム「設計部署１０」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_SEKKEI10"), "カラム「設計部署１０」が存在すること");
                // カラム「評価部署１」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_HYOUKA1"), "カラム「評価部署１」が存在すること");
                // カラム「評価部署２」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_HYOUKA2"), "カラム「評価部署２」が存在すること");
                // カラム「評価部署３」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_HYOUKA3"), "カラム「評価部署３」が存在すること");
                // カラム「評価部署４」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_HYOUKA4"), "カラム「評価部署４」が存在すること");
                // カラム「評価部署５」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_HYOUKA5"), "カラム「評価部署５」が存在すること");
                // カラム「評価部署６」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_HYOUKA6"), "カラム「評価部署６」が存在すること");
                // カラム「評価部署７」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_HYOUKA7"), "カラム「評価部署７」が存在すること");
                // カラム「評価部署８」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_HYOUKA8"), "カラム「評価部署８」が存在すること");
                // カラム「評価部署９」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_HYOUKA9"), "カラム「評価部署９」が存在すること");
                // カラム「評価部署１０」が取得できること
                Assert.IsTrue(methodProccess.Columns.Contains("BUSYO_HYOUKA10"), "カラム「評価部署１０」が存在すること");
                #endregion

                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void existMitakaAnswerDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                DataTable testFormat = testClass.getMitakaAnswerData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["SYSTEM_NO"] = TestSettings.TEST_PARAM_DEPLOY_SYSTEMNO;
                testField["ANSWER_DIVISION_CODE"] = TestSettings.TEST_PARAM_ANSWER_DIVISION_CODE;
                testField["EDIT_FLG"] = "1";

                // メソッド実行
                var methodProccess = testClass.existMitakaAnswerData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                #endregion
                #endregion

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void insertMitakaAnswerDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getMitakaAnswerData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["SYSTEM_NO"] = TestSettings.TEST_PARAM_ANSWER_SYSTEMNO;
                testField["ANSWER_DIVISION_CODE"] = TestSettings.TEST_PARAM_ANSWER_DIVISION_CODE;
                testField["TARGET_FLG"] = "1";

                // テスト前処理
                testClass.deleteMitakaAnswerData(testField["MITAKA_NO"].ToString());

                // メソッド実行
                var methodProccess = testClass.insertMitakaAnswerData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                var getData = testClass.getMitakaAnswerData(testField["MITAKA_NO"].ToString());
                Assert.IsTrue(getData.Rows.Count > 0, "データが正常に登録されていること");
                Assert.IsTrue(getData.Rows[0]["MITAKA_NO"].ToString() == testField["MITAKA_NO"].ToString(),
                    "管理番号が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["SYSTEM_NO"].ToString() == testField["SYSTEM_NO"].ToString(),
                "システムNoが想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["ANSWER_DIVISION_CODE"].ToString() == testField["ANSWER_DIVISION_CODE"].ToString(),
                "回答対象部署コードが想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["STATUS"].ToString() == "",
                "進捗状況が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["ANSWER_CONTENT"].ToString() == "",
                "回答内容が想定通りに登録されていること");
                Assert.IsTrue(getData.Rows[0]["INSERT_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                "登録ユーザが想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["INSERT_YMD"],
                "登録日時が登録されていること");
                Assert.IsTrue(getData.Rows[0]["UPDATE_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                "更新ユーザが想定通りに登録されていること");
                Assert.IsNotNull(getData.Rows[0]["UPDATE_YMD"],
                "更新日時が登録されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                // テスト後処理
                testClass.deleteMitakaAnswerData(manageNo);
            }
        }

        [TestMethod()]
        public void updateMitakaAnswerDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getMitakaAnswerData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["SYSTEM_NO"] = TestSettings.TEST_PARAM_ANSWER_SYSTEMNO;
                testField["ANSWER_DIVISION_CODE"] = TestSettings.TEST_PARAM_ANSWER_DIVISION_CODE;
                testField["STATUS"] = TestSettings.TEST_PARAM_ANSWER_STATUS;
                testField["ANSWER_CONTENT"] = TestSettings.TEST_PARAM_ANSWER_CONTENT;

                // テスト前処理
                if (!testClass.existMitakaAnswerData(testField))
                {
                    testField["TARGET_FLG"] = "0";
                    testClass.insertMitakaAnswerData(testField);
                }

                testField["TARGET_FLG"] = "1";

                // メソッド実行
                var methodProccess = testClass.updateMitakaAnswerData(testField);

                #region データ検証

                #region 返却値検証
                // クラスがNULLでないこと
                Assert.IsNotNull(methodProccess, "返却値がNULLでないこと");
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");

                var getData = testClass.getMitakaAnswerData(testField["MITAKA_NO"].ToString());
                Assert.IsTrue(getData.Rows.Count > 0, "データが正常に登録されていること");
                Assert.IsTrue(getData.Rows[0]["MITAKA_NO"].ToString() == testField["MITAKA_NO"].ToString(),
                    "管理番号が想定通りの値であること");
                Assert.IsTrue(getData.Rows[0]["SYSTEM_NO"].ToString() == testField["SYSTEM_NO"].ToString(),
                "システムNoが想定通りの値であること");
                Assert.IsTrue(getData.Rows[0]["ANSWER_DIVISION_CODE"].ToString() == testField["ANSWER_DIVISION_CODE"].ToString(),
                "回答対象部署コードが想定通りの値であること");
                Assert.IsTrue(getData.Rows[0]["STATUS"].ToString() == testField["STATUS"].ToString(),
                "進捗状況が想定通りに更新されていること");
                Assert.IsTrue(getData.Rows[0]["ANSWER_CONTENT"].ToString() == testField["ANSWER_CONTENT"].ToString(),
                "回答内容が想定通りに更新されていること");
                Assert.IsNotNull(getData.Rows[0]["INSERT_USER"].ToString(),
                "登録ユーザがNULLでないこと");
                Assert.IsNotNull(getData.Rows[0]["INSERT_YMD"],
                "登録日時がNULLでないこと");
                Assert.IsTrue(getData.Rows[0]["UPDATE_USER"].ToString() == TestSettings.TEST_PARAM_LOGINUSER,
                "更新ユーザが想定通りに更新されていること");
                Assert.IsNotNull(getData.Rows[0]["UPDATE_YMD"],
                "更新日時がNULLでないこと");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                // テスト後処理
                testClass.deleteMitakaAnswerData(manageNo);
            }
        }

        [TestMethod()]
        public void deleteMitakaAnswerDataTest()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getMitakaAnswerData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["SYSTEM_NO"] = TestSettings.TEST_PARAM_ANSWER_SYSTEMNO;
                testField["ANSWER_DIVISION_CODE"] = TestSettings.TEST_PARAM_ANSWER_DIVISION_CODE;

                // テスト前処理
                if (!testClass.existMitakaAnswerData(testField))
                {
                    testClass.insertMitakaAnswerData(testField);
                }

                // メソッド実行
                testClass.deleteMitakaAnswerData(testField);

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                Assert.IsTrue(!testClass.existDeploymentTroubleData(testField), "データが削除されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void deleteMitakaAnswerDataTest1()
        {
            string manageNo = TestSettings.TEST_PARAM_MANAGE_NO;

            try
            {
                // テスト用パラメータ
                DataTable testFormat = testClass.getMitakaAnswerData();
                var testField = testFormat.NewRow();
                testField["MITAKA_NO"] = manageNo;
                testField["SYSTEM_NO"] = TestSettings.TEST_PARAM_ANSWER_SYSTEMNO;
                testField["ANSWER_DIVISION_CODE"] = TestSettings.TEST_PARAM_ANSWER_DIVISION_CODE;

                // テスト前処理
                if (!testClass.existMitakaAnswerData(testField))
                {
                    testClass.insertMitakaAnswerData(testField);
                }

                // メソッド実行
                testClass.deleteMitakaAnswerData(testField["MITAKA_NO"].ToString());

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                Assert.IsTrue(!testClass.existDeploymentTroubleData(testField), "データが削除されていること");

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void postMitakaAnswerDataTest()
        {
            String manageNo = TestSettings.TEST_PARAM_MANAGE_NO_ROOP;

            try
            {
                // テスト用パラメータ
                DataTable testDt = testClass.getMitakaAnswerData();

                for (int i = 1; i <= 3; i++)
                {
                    var testField = testDt.NewRow();
                    testField["MITAKA_NO"] = TestSettings.TEST_PARAM_MANAGE_NO;
                    testField["SYSTEM_NO"] = TestSettings.TEST_PARAM_ANSWER_SYSTEMNO;
                    testField["ANSWER_DIVISION_CODE"] = TestSettings.TEST_PARAM_ANSWER_DIVISION_CODE;
                    testField["STATUS"] = (i % 2).ToString();
                    testField["EDIT_FLG"] = (i % 2).ToString();
                    testDt.Rows.Add(testField);
                }
                // テスト前処理
                testClass.deleteMitakaAnswerData(manageNo);

                // メソッド実行
                testClass.postMitakaAnswerData(testDt);
                // メソッド実行
                testClass.postMitakaAnswerData(testDt);

                #region データ検証

                #region 返却値検証
                // メソッド処理が正常終了していること
                Assert.IsTrue(testClass.ProccessSuccess == true, "処理が成功していること");
                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());

            }
            finally
            {
                // テスト後処理
                testClass.deleteMitakaAnswerData(manageNo);
            }
        }
        #endregion
    }
}