using Microsoft.VisualStudio.TestTools.UnitTesting;
using OldTigerWeb.BuisinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OldTigerWeb.DataAccess.Tests;
using OldTigerWeb.DataAccess;

namespace OldTigerWeb.BuisinessLogic.Tests
{
    [TestClass()]
    public class MitakaDataTests
    {
        MitakaData testClass;
        TestDAMitakaData testDb;

        [TestInitialize]
        public void MitakaDataTestInitialize()
        {
            //string testManageNo = TestSettings.TEST_PARAM_MANAGE_NO;    // 管理番号
            //string testUser = TestSettings.TEST_PARAM_LOGINUSER;        // ユーザーID
            //testClass = new MitakaData(testManageNo,testUser);
        }

        [TestMethod()]
        public void MitakaDataPropertyTest()
        {

            #region データ検証

            // メソッド実行
            testClass = new MitakaData(TestSettings.TEST_PARAM_LOGINUSER, true);
            MitakaData localTestClass;

            // インスタンス検証
            // クラスがNULLでないこと
            Assert.IsNotNull(testClass);

            #region プロパティ取得

            // 管理番号がNULLでないこと
            Assert.IsNotNull(testClass.ManageNo, "管理番号がNULLでないこと");
            // 所有者がNULLでないこと
            Assert.IsNotNull(testClass.Owner, "所有者がNULLでないこと");
            // タイトルがNULLでないこと
            Assert.IsNotNull(testClass.Title, "タイトルがNULLでないこと");
            // 目的がNULLでないこと
            Assert.IsNotNull(testClass.Purpose, "目的がNULLでないこと");
            // コメントがNULLでないこと
            Assert.IsNotNull(testClass.Comment, "コメントがNULLでないこと");
            // 回答開始日時がNULLでないこと
            Assert.IsNotNull(testClass.StartDateTime, "回答開始日時がNULLでないこと");
            // 回答終了日時がNULLでないこと
            Assert.IsNotNull(testClass.EndDateTime, "回答終了日時がNULLでないこと");
            // 状況がNULLでないこと
            Assert.IsNotNull(testClass.Status, "状況がNULLでないこと");
            // 状況（表示用）がNULLでないこと
            Assert.IsNotNull(testClass.StatusDisp, "状況（表示用）がNULLでないこと");
            // 状況コメントがNULLでないこと
            Assert.IsNotNull(testClass.StatusComment, "状況コメントがNULLでないこと");
            // 作成者がNULLでないこと
            Assert.IsNotNull(testClass.InsertUser, "作成者がNULLでないこと");
            // 作成者名がNULLでないこと
            Assert.IsNotNull(testClass.InsertUserName, "作成者名がNULLでないこと");
            // 更新者がNULLでないこと
            Assert.IsNotNull(testClass.UpdateUser, "更新者がNULLでないこと");
            // 更新者名がNULLでないこと
            Assert.IsNotNull(testClass.UpdateUser, "更新者名がNULLでないこと");
            // 作成日時がNULLでないこと
            Assert.IsNotNull(testClass.InsertDate, "作成日時がNULLでないこと");
            // 更新日時がNULLでないこと
            Assert.IsNotNull(testClass.UpdateDate, "更新日時がNULLでないこと");
            // 検索条件リストがNULLでないこと
            Assert.IsNotNull(testClass.SearchParameterList, "検索条件リストがNULLでないこと");
            // 関連ユーザー情報リストがNULLでないこと
            Assert.IsNotNull(testClass.ReLationUserList, "関連ユーザー情報リストがNULLでないこと");
            // 作成者（主）情報がNULLでないこと
            Assert.IsNotNull(testClass.CreateMainUser, "作成者（主）情報がNULLでないこと");
            // 作成者（副）情報がNULLでないこと
            Assert.IsNotNull(testClass.CreateSubUser, "作成者（副）情報がNULLでないこと");
            // 点検者情報がNULLでないこと
            Assert.IsNotNull(testClass.InspectionUser, "点検者情報がNULLでないこと");
            // 管理部署（主）コードがNULLでないこと
            Assert.IsNotNull(testClass.ManageMainDivisionCode, "管理部署（主）コードがNULLでないこと");
            // 管理部署（副）コードがNULLでないこと
            Assert.IsNotNull(testClass.ManageSubDivisionCode, "管理部署（副）コードがNULLでないこと");
            //  回答依頼先情報リストがNULLでないこと
            Assert.IsNotNull(testClass.ConfirmationRequestList, "回答依頼先情報リストがNULLでないこと");
            // 展開対象リストがNULLでないこと
            Assert.IsNotNull(testClass.DeploymentTroubleList, "展開対象リストがNULLでないこと");
            // 展開対象リスト（最新）がNULLでないこと
            Assert.IsNotNull(testClass.DeploymentTroubleLatestList, "展開対象（最新）リストがNULLでないこと");
            // タイトル品番情報リストがNULLでないこと
            Assert.IsNotNull(testClass.TitleDrawingList, "タイトル品番情報リストがNULLでないこと");
            // 機種情報リストがNULLでないこと
            Assert.IsNotNull(testClass.ModelList, "機種情報リストがNULLでないこと");
            // BLK情報リストがNULLでないこと
            Assert.IsNotNull(testClass.BlockList, "BLK情報リストがNULLでないこと");
            // 開発符号情報リストがNULLでないこと
            Assert.IsNotNull(testClass.DevelopmentCodeList, "開発符号情報リストがNULLでないこと");
            // 設通情報リストがNULLでないこと
            Assert.IsNotNull(testClass.EcsList, "設通情報がNULLでないこと");
            // 過去トラ観たか回答情報インスタンスがNULLでないこと
            Assert.IsNotNull(testClass.MitakaAnswerData, "過去トラ観たか回答情報インスタンスがNULLでないこと");
            // データ処理結果がNULLでないこと
            Assert.IsNotNull(testClass.DataProcessResult, "データ処理結果がNULLでないこと");
            // 編集フラグがNULLでないこと
            Assert.IsNotNull(testClass.EditFlg, "編集フラグがNULLでないこと");
            // 過去トラ観たか権限がNULLでないこと
            Assert.IsNotNull(testClass.MitakaAuthority, "過去トラ観たか権限がNULLでないこと");
            // 埋め込みスクリプトがNULLでないこと
            Assert.IsNotNull(testClass.embeddedScript, "埋め込みスクリプトがNULLでないこと");

            #endregion

            #region プロパティ設定

            // タイトルが正しく設定されていること
            localTestClass = new MitakaData(TestSettings.TEST_PARAM_LOGINUSER, true);
            localTestClass.Title = TestSettings.TEST_PARAM_TITLE;
            Assert.AreEqual(localTestClass.Title, TestSettings.TEST_PARAM_TITLE,
                "タイトルが正しく設定されていること(新規行)");
            Assert.IsTrue(localTestClass.EditFlg,
                "編集フラグが正しく設定されていること(新規行)");
            testClass.Title = TestSettings.TEST_PARAM_TITLE;
            Assert.AreEqual(testClass.Title, TestSettings.TEST_PARAM_TITLE,
                "タイトルが正しく設定されていること(既存行)");

            // 目的が正しく設定されていること
            localTestClass = new MitakaData(TestSettings.TEST_PARAM_LOGINUSER, true);
            localTestClass.Purpose = TestSettings.TEST_PARAM_PURPOSE;
            Assert.AreEqual(localTestClass.Purpose, TestSettings.TEST_PARAM_PURPOSE,
                "目的が正しく設定されていること(新規行)");
            Assert.IsTrue(localTestClass.EditFlg,
                "編集フラグが正しく設定されていること(新規行)");
            testClass.Purpose = TestSettings.TEST_PARAM_PURPOSE;
            Assert.AreEqual(testClass.Purpose, TestSettings.TEST_PARAM_PURPOSE,
                "目的が正しく設定されていること(既存行)");

            // コメントが正しく設定されていること
            localTestClass = new MitakaData(TestSettings.TEST_PARAM_LOGINUSER, true);
            localTestClass.Comment = TestSettings.TEST_PARAM_COMMENT;
            Assert.AreEqual(localTestClass.Comment, TestSettings.TEST_PARAM_COMMENT,
                "コメントが正しく設定されていること(新規行)");
            Assert.IsTrue(localTestClass.EditFlg,
                "編集フラグが正しく設定されていること(新規行)");
            testClass.Comment = TestSettings.TEST_PARAM_COMMENT;
            Assert.AreEqual(testClass.Comment, TestSettings.TEST_PARAM_COMMENT,
                "コメントが正しく設定されていること(既存行)");

            // 回答開始日時が正しく設定されていること
            localTestClass = new MitakaData(TestSettings.TEST_PARAM_LOGINUSER, true);
            localTestClass.StartDateTime = TestSettings.TEST_PARAM_DATE_START;
            Assert.AreEqual(localTestClass.StartDateTime, TestSettings.TEST_PARAM_DATE_START,
                "回答開始日時が正しく設定されていること(新規行)");
            Assert.IsTrue(localTestClass.EditFlg,
                "編集フラグが正しく設定されていること(新規行)");
            testClass.StartDateTime = TestSettings.TEST_PARAM_DATE_START;
            Assert.AreEqual(testClass.StartDateTime, TestSettings.TEST_PARAM_DATE_START,
                "回答開始日時が正しく設定されていること(既存行)");

            // 回答終了日時が正しく設定されていること
            localTestClass = new MitakaData(TestSettings.TEST_PARAM_LOGINUSER, true);
            localTestClass.EndDateTime = TestSettings.TEST_PARAM_DATE_END;
            Assert.AreEqual(localTestClass.EndDateTime, TestSettings.TEST_PARAM_DATE_END,
                "回答終了日時が正しく設定されていること(新規行)");
            Assert.IsTrue(localTestClass.EditFlg,
                "編集フラグが正しく設定されていること(新規行)");
            testClass.EndDateTime = TestSettings.TEST_PARAM_DATE_END;
            Assert.AreEqual(testClass.EndDateTime, TestSettings.TEST_PARAM_DATE_END,
                "回答終了日時が正しく設定されていること(既存行)");

            //// 状況が正しく設定されていること →処理廃止_2017/08/20
            //localTestClass = new MitakaData(TestSettings.TEST_PARAM_LOGINUSER, true);
            //localTestClass.Status= TestSettings.TEST_PARAM_STATUS;
            //Assert.AreEqual(localTestClass.Status, TestSettings.TEST_PARAM_STATUS,
            //    "状況が正しく設定されていること(新規行)");
            //Assert.IsTrue(localTestClass.EditFlg,
            //    "編集フラグが正しく設定されていること(新規行)");
            //testClass.Status = Def.MITAKA_STATUS_PREPARATION;
            //Assert.AreEqual(testClass.Status, Def.MITAKA_STATUS_PREPARATION,
            //    "状況(回答準備)が正しく設定されていること(既存行)");
            //testClass.Status = Def.MITAKA_STATUS_ANSWER;
            //Assert.AreEqual(testClass.Status, Def.MITAKA_STATUS_ANSWER,
            //    "状況(回答中)が正しく設定されていること(既存行)");

            // 状況コメントが正しく設定されていること
            localTestClass = new MitakaData(TestSettings.TEST_PARAM_LOGINUSER, true);
            localTestClass.StatusComment = TestSettings.TEST_PARAM_STATUS_COMMENT;
            Assert.AreEqual(localTestClass.StatusComment, TestSettings.TEST_PARAM_STATUS_COMMENT,
                "状況コメントが正しく設定されていること(新規行)");
            Assert.IsTrue(localTestClass.EditFlg,
                "編集フラグが正しく設定されていること(新規行)");
            testClass.StatusComment = TestSettings.TEST_PARAM_STATUS_COMMENT;
            Assert.AreEqual(testClass.StatusComment, TestSettings.TEST_PARAM_STATUS_COMMENT,
                "状況コメントが正しく設定されていること(既存行)");

            // 作成者（主）が正しく設定されていること
            var drCreateMain = testClass.CreateMainUser;
            drCreateMain["RELATION_TYPE"] = "99"; // 作成者（主）でないコードを設定
            drCreateMain["USER_ID"] = TestSettings.TEST_PARAM_RELATION_USER;

            localTestClass = new MitakaData(TestSettings.TEST_PARAM_LOGINUSER, true);
            localTestClass.CreateMainUser = drCreateMain;
            Assert.AreEqual((string)localTestClass.CreateMainUser["RELATION_TYPE"], Def.MITAKA_RELATION_TYPE_CREATER_MAIN,
                "作成者（主）.関連タイプが正しく設定されていること(新規行)");
            Assert.AreEqual((string)localTestClass.CreateMainUser["USER_ID"], TestSettings.TEST_PARAM_RELATION_USER,
                "作成者（主）.ユーザーが正しく設定されていること(新規行)");
            Assert.AreEqual((string)localTestClass.CreateMainUser["EDIT_FLG"], "1",
                "作成者（主）.編集フラグが正しく設定されていること(新規行)");
            Assert.IsTrue((string)localTestClass.ManageMainDivisionCode == "",
                "管理部署（主）コードが正しく設定されていること(新規行)");
            Assert.IsTrue(localTestClass.EditFlg,
                "編集フラグが正しく設定されていること(新規行)");
            testClass.CreateMainUser = drCreateMain;
            Assert.AreEqual((string)testClass.CreateMainUser["RELATION_TYPE"], Def.MITAKA_RELATION_TYPE_CREATER_MAIN,
                "作成者（主）.関連タイプが正しく設定されていること(既存行)");
            Assert.AreEqual((string)testClass.CreateMainUser["USER_ID"], TestSettings.TEST_PARAM_RELATION_USER,
                "作成者（主）.ユーザーが正しく設定されていること(既存行)");
            Assert.AreEqual((string)testClass.CreateMainUser["EDIT_FLG"], "1",
                "作成者（主）.編集フラグが正しく設定されていること(既存行)");
            Assert.IsTrue((string)testClass.ManageMainDivisionCode == "",
                "管理部署（主）コードが正しく設定されていること(新規行)");

            // 作成者（副）が正しく設定されていること
            var drCreateSub = testClass.CreateSubUser;
            drCreateSub["RELATION_TYPE"] = "99"; // 作成者（副）でないコードを設定
            drCreateSub["USER_ID"] = TestSettings.TEST_PARAM_RELATION_USER;

            localTestClass = new MitakaData(TestSettings.TEST_PARAM_LOGINUSER, true);
            localTestClass.CreateSubUser = drCreateSub;
            Assert.AreEqual((string)localTestClass.CreateSubUser["RELATION_TYPE"], Def.MITAKA_RELATION_TYPE_CREATER_SUB,
                "作成者（副）.関連タイプが正しく設定されていること(新規行)");
            Assert.AreEqual((string)localTestClass.CreateSubUser["USER_ID"], TestSettings.TEST_PARAM_RELATION_USER,
                "作成者（副）.ユーザーが正しく設定されていること(新規行)");
            Assert.AreEqual((string)localTestClass.CreateSubUser["EDIT_FLG"], "1",
                "作成者（副）.編集フラグが正しく設定されていること(新規行)");
            Assert.IsTrue((string)localTestClass.ManageSubDivisionCode == "",
                "管理部署（副）コードが正しく設定されていること(新規行)");
            Assert.IsTrue(localTestClass.EditFlg,
                "編集フラグが正しく設定されていること(新規行)");
            testClass.CreateSubUser = drCreateSub;
            Assert.AreEqual((string)testClass.CreateSubUser["RELATION_TYPE"], Def.MITAKA_RELATION_TYPE_CREATER_SUB,
                "作成者（副）.関連タイプが正しく設定されていること(既存行)");
            Assert.AreEqual((string)testClass.CreateSubUser["USER_ID"], TestSettings.TEST_PARAM_RELATION_USER,
                "作成者（副）.ユーザーが正しく設定されていること(既存行)");
            Assert.AreEqual((string)testClass.CreateSubUser["EDIT_FLG"], "1",
                "作成者（副）.編集フラグが正しく設定されていること(既存行)");
            Assert.IsTrue((string)testClass.ManageSubDivisionCode == "",
                "管理部署（副）コードが正しく設定されていること(既存行)");

            // 点検者が正しく設定されていること
            var drInspection = testClass.InspectionUser;
            drInspection["RELATION_TYPE"] = "99"; // 点検者でないコードを設定
            drInspection["USER_ID"] = TestSettings.TEST_PARAM_RELATION_USER;

            localTestClass = new MitakaData(TestSettings.TEST_PARAM_LOGINUSER, true);
            localTestClass.InspectionUser = drInspection;
            Assert.AreEqual((string)localTestClass.InspectionUser["RELATION_TYPE"], Def.MITAKA_RELATION_TYPE_INSPECTOR,
                "点検者.関連タイプが正しく設定されていること(新規行)");
            Assert.AreEqual((string)localTestClass.InspectionUser["USER_ID"], TestSettings.TEST_PARAM_RELATION_USER,
                "点検者.ユーザーが正しく設定されていること(新規行)");
            Assert.AreEqual((string)localTestClass.InspectionUser["EDIT_FLG"], "1",
                "点検者.編集フラグが正しく設定されていること(新規行)");
            Assert.IsTrue(localTestClass.EditFlg,
                "編集フラグが正しく設定されていること(新規行)");
            testClass.InspectionUser = drInspection;
            Assert.AreEqual((string)testClass.InspectionUser["RELATION_TYPE"], Def.MITAKA_RELATION_TYPE_INSPECTOR,
                "点検者.関連タイプが正しく設定されていること(既存行)");
            Assert.AreEqual((string)testClass.InspectionUser["USER_ID"], TestSettings.TEST_PARAM_RELATION_USER,
                "点検者.ユーザーが正しく設定されていること(既存行)");
            Assert.AreEqual((string)testClass.InspectionUser["EDIT_FLG"], "1",
                "点検者.編集フラグが正しく設定されていること(既存行)");

            // 回答依頼先リストが正しく設定されていること
            var dtRequest = testClass.ConfirmationRequestList;
            for (int i = 1; i <= 2;i++)
            {
                var drRequest = dtRequest.NewRow();
                drRequest["RELATION_TYPE"] = "99"; // 回答依頼先でないコードを設定
                drRequest["USER_ID"] = TestSettings.TEST_PARAM_RELATION_USER;
                dtRequest.Rows.Add(drRequest);
            }

            localTestClass = new MitakaData(TestSettings.TEST_PARAM_LOGINUSER, true);
            localTestClass.ConfirmationRequestList= dtRequest.Copy();
            Assert.AreEqual((string)localTestClass.ConfirmationRequestList.Rows[0]["RELATION_TYPE"], Def.MITAKA_RELATION_TYPE_REQUEST,
                "回答依頼先リスト.関連タイプが正しく設定されていること(新規行)");
            Assert.AreEqual((string)localTestClass.ConfirmationRequestList.Rows[0]["USER_ID"], TestSettings.TEST_PARAM_RELATION_USER,
                "回答依頼先リスト.ユーザーが正しく設定されていること(新規行)");
            Assert.AreEqual((string)localTestClass.ConfirmationRequestList.Rows[0]["EDIT_FLG"], "1",
                "回答依頼先リスト.編集フラグが正しく設定されていること(新規行)");
            Assert.IsTrue(localTestClass.EditFlg,
                "編集フラグが正しく設定されていること(新規行)");
            testClass.ConfirmationRequestList = dtRequest.Copy();
            Assert.AreEqual((string)testClass.ConfirmationRequestList.Rows[0]["RELATION_TYPE"], Def.MITAKA_RELATION_TYPE_REQUEST,
                "回答依頼先リスト.関連タイプが正しく設定されていること(既存行)");
            Assert.AreEqual((string)testClass.ConfirmationRequestList.Rows[0]["USER_ID"], TestSettings.TEST_PARAM_RELATION_USER,
                "回答依頼先リスト.ユーザーが正しく設定されていること(既存行)");
            Assert.AreEqual((string)testClass.ConfirmationRequestList.Rows[0]["EDIT_FLG"], "1",
                "回答依頼先リスト.編集フラグが正しく設定されていること(既存行)");

            // 展開対象リストが正しく設定されていること
            var dtTrouble = testClass.DeploymentTroubleList;
            for (int i = 1; i <= 2; i++)
            {
                var drTrouble = dtTrouble.NewRow();
                drTrouble["SYSTEM_NO"] = TestSettings.TEST_PARAM_DEPLOY_SYSTEMNO; 
                drTrouble["TARGET_FLG"] = "1"; // 回答依頼先でないコードを設定
                dtTrouble.Rows.Add(drTrouble);
            }

            localTestClass = new MitakaData(TestSettings.TEST_PARAM_LOGINUSER, true);
            localTestClass.DeploymentTroubleList = dtTrouble.Copy();
            Assert.AreEqual((int)localTestClass.DeploymentTroubleList.Rows[0]["SYSTEM_NO"], TestSettings.TEST_PARAM_DEPLOY_SYSTEMNO,
                "展開対象リスト.システムNoが正しく設定されていること(新規行)");
            Assert.AreEqual((string)localTestClass.DeploymentTroubleList.Rows[0]["TARGET_FLG"], "1",
                "展開対象リスト.対象フラグが正しく設定されていること(新規行)");
            Assert.AreEqual((string)localTestClass.DeploymentTroubleList.Rows[0]["EDIT_FLG"], "1",
                "展開対象リスト.編集フラグが正しく設定されていること(新規行)");
            Assert.IsTrue(localTestClass.EditFlg,
                "編集フラグが正しく設定されていること(新規行)");
            testClass.DeploymentTroubleList = dtTrouble.Copy();
            Assert.AreEqual((int)testClass.DeploymentTroubleList.Rows[0]["SYSTEM_NO"], TestSettings.TEST_PARAM_DEPLOY_SYSTEMNO,
                "展開対象リスト.システムNoが正しく設定されていること(既存行)");
            Assert.AreEqual((string)testClass.DeploymentTroubleList.Rows[0]["TARGET_FLG"], "1",
                "展開対象リスト.対象フラグが正しく設定されていること(既存行)");
            Assert.AreEqual((string)testClass.DeploymentTroubleList.Rows[0]["EDIT_FLG"], "1",
                "展開対象リスト.編集フラグが正しく設定されていること(既存行)");

            // タイトル図面情報リストが正しく設定されていること
            var dtDrawing = testClass.TitleDrawingList;
            for (int i = 1; i <= 2; i++)
            {
                var drDrawing = dtDrawing.NewRow();
                drDrawing["TITLE_DRAWING_NO"] = TestSettings.TEST_PARAM_DRAWING_NO; 
                dtDrawing.Rows.Add(drDrawing);
            }

            localTestClass = new MitakaData(TestSettings.TEST_PARAM_LOGINUSER, true);
            localTestClass.TitleDrawingList = dtDrawing.Copy();
            Assert.AreEqual((string)localTestClass.TitleDrawingList.Rows[0]["TITLE_DRAWING_NO"], TestSettings.TEST_PARAM_DRAWING_NO,
                "タイトル図面リスト.タイトル図面番号が正しく設定されていること(新規行)");
            Assert.AreEqual((string)localTestClass.TitleDrawingList.Rows[0]["EDIT_FLG"], "1",
                "タイトル図面リスト.編集フラグが正しく設定されていること(新規行)");
            Assert.IsTrue(localTestClass.EditFlg,
                "編集フラグが正しく設定されていること(新規行)");
            testClass.TitleDrawingList= dtDrawing.Copy();
            Assert.AreEqual((string)testClass.TitleDrawingList.Rows[0]["TITLE_DRAWING_NO"], TestSettings.TEST_PARAM_DRAWING_NO,
                "タイトル図面リスト.システムNoが正しく設定されていること(既存行)");
            Assert.AreEqual((string)testClass.TitleDrawingList.Rows[0]["EDIT_FLG"], "1",
                "タイトル図面リスト.編集フラグが正しく設定されていること(既存行)");

            // 機種情報リストが正しく設定されていること
            var dtModel = testClass.ModelList;
            for (int i = 1; i <= 2; i++)
            {
                var drModel = dtModel.NewRow();
                drModel["MODEL"] = TestSettings.TEST_PARAM_MODEL_NO;
                dtModel.Rows.Add(drModel);
            }

            localTestClass = new MitakaData(TestSettings.TEST_PARAM_LOGINUSER, true);
            localTestClass.ModelList= dtModel.Copy();
            Assert.AreEqual((string)localTestClass.ModelList.Rows[0]["MODEL"], TestSettings.TEST_PARAM_MODEL_NO,
                "機種リスト.機種が正しく設定されていること(新規行)");
            Assert.AreEqual((string)localTestClass.ModelList.Rows[0]["EDIT_FLG"], "1",
                "機種リスト.編集フラグが正しく設定されていること(新規行)");
            Assert.IsTrue(localTestClass.EditFlg,
                "編集フラグが正しく設定されていること(新規行)");
            testClass.ModelList= dtModel.Copy();
            Assert.AreEqual((string)testClass.ModelList.Rows[0]["MODEL"], TestSettings.TEST_PARAM_MODEL_NO,
                "機種リスト.機種が正しく設定されていること(既存行)");
            Assert.AreEqual((string)testClass.ModelList.Rows[0]["EDIT_FLG"], "1",
                "機種リスト.編集フラグが正しく設定されていること(既存行)");

            // BLK情報リストが正しく設定されていること
            var dtBlock = testClass.BlockList;
            for (int i = 1; i <= 2; i++)
            {
                var drBlock = dtBlock.NewRow();
                drBlock["BLK_NO"] = TestSettings.TEST_PARAM_BLOCK_NO;
                dtBlock.Rows.Add(drBlock);
            }

            localTestClass = new MitakaData(TestSettings.TEST_PARAM_LOGINUSER, true);
            localTestClass.BlockList = dtBlock.Copy();
            Assert.AreEqual((string)localTestClass.BlockList.Rows[0]["BLK_NO"], TestSettings.TEST_PARAM_BLOCK_NO,
                "BLK情報リスト.機種が正しく設定されていること(新規行)");
            Assert.AreEqual((string)localTestClass.BlockList.Rows[0]["EDIT_FLG"], "1",
                "BLK情報リスト.編集フラグが正しく設定されていること(新規行)");
            Assert.IsTrue(localTestClass.EditFlg,
                "編集フラグが正しく設定されていること(新規行)");
            testClass.BlockList = dtBlock.Copy();
            Assert.AreEqual((string)testClass.BlockList.Rows[0]["BLK_NO"], TestSettings.TEST_PARAM_BLOCK_NO,
                "BLK情報リスト.機種が正しく設定されていること(既存行)");
            Assert.AreEqual((string)testClass.BlockList.Rows[0]["EDIT_FLG"], "1",
                "BLK情報リスト.編集フラグが正しく設定されていること(既存行)");

            // 開発符号情報リストが正しく設定されていること
            var dtDevelop = testClass.DevelopmentCodeList;
            for (int i = 1; i <= 2; i++)
            {
                var drDevelop = dtDevelop.NewRow();
                drDevelop["DEVELOPMENT_CODE"] = TestSettings.TEST_PARAM_DEVELOPMENT_CODE;
                dtDevelop.Rows.Add(drDevelop);
            }

            localTestClass = new MitakaData(TestSettings.TEST_PARAM_LOGINUSER, true);
            localTestClass.DevelopmentCodeList = dtDevelop.Copy();
            Assert.AreEqual((string)localTestClass.DevelopmentCodeList.Rows[0]["DEVELOPMENT_CODE"], TestSettings.TEST_PARAM_DEVELOPMENT_CODE,
                "開発符号情報リスト.機種が正しく設定されていること(新規行)");
            Assert.AreEqual((string)localTestClass.DevelopmentCodeList.Rows[0]["EDIT_FLG"], "1",
                "開発符号情報リスト.編集フラグが正しく設定されていること(新規行)");
            Assert.IsTrue(localTestClass.EditFlg,
                "編集フラグが正しく設定されていること(新規行)");
            testClass.DevelopmentCodeList = dtDevelop.Copy();
            Assert.AreEqual((string)testClass.DevelopmentCodeList.Rows[0]["DEVELOPMENT_CODE"], TestSettings.TEST_PARAM_DEVELOPMENT_CODE,
                "開発符号情報リスト.機種が正しく設定されていること(既存行)");
            Assert.AreEqual((string)testClass.DevelopmentCodeList.Rows[0]["EDIT_FLG"], "1",
                "開発符号情報リスト.編集フラグが正しく設定されていること(既存行)");

            // 開発符号情報リストが正しく設定されていること
            var dtEcs= testClass.EcsList;
            for (int i = 1; i <= 2; i++)
            {
                var drEcs= dtEcs.NewRow();
                drEcs["ECS_NO"] = TestSettings.TEST_PARAM_ECS_NO;
                dtEcs.Rows.Add(drEcs);
            }

            localTestClass = new MitakaData(TestSettings.TEST_PARAM_LOGINUSER, true);
            localTestClass.EcsList= dtEcs.Copy();
            Assert.AreEqual((string)localTestClass.EcsList.Rows[0]["ECS_NO"], TestSettings.TEST_PARAM_ECS_NO,
                "設通情報リスト.機種が正しく設定されていること(新規行)");
            Assert.AreEqual((string)localTestClass.EcsList.Rows[0]["EDIT_FLG"], "1",
                "設通情報リスト.編集フラグが正しく設定されていること(新規行)");
            Assert.IsTrue(localTestClass.EditFlg,
                "編集フラグが正しく設定されていること(新規行)");
            testClass.EcsList= dtEcs.Copy();
            Assert.AreEqual((string)testClass.EcsList.Rows[0]["ECS_NO"], TestSettings.TEST_PARAM_ECS_NO,
                "設通情報リスト.機種が正しく設定されていること(既存行)");
            Assert.AreEqual((string)testClass.EcsList.Rows[0]["EDIT_FLG"], "1",
                "設通情報リスト.編集フラグが正しく設定されていること(既存行)");

            #endregion

            #endregion
        }



        [TestMethod()]
        public void MitakaDataTest()
        {
            // テストパラメータ初期化
            testDb = new TestDAMitakaData();
            var shot01 = new MitakaData(testDb,true);

            // DB取得アクション確認
            Assert.IsNotNull(testDb.getMitakaHeaderData_Receive, "過去トラ観たかヘッダー情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getSearchParameterData_Receive, "検索条件取得処理に到達していること");
            Assert.IsNotNull(testDb.getReLationUserData_Receive, "関連ユーザー情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getDeploymentTroubleData_Receive, "展開対象取得処理に到達していること");
            Assert.IsNotNull(testDb.getTitleDrawingData_Receive, "タイトル情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getModelData_Receive, "機種情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getBlockData_Receive, "BLK情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getDevelopmentCodeData_Receive, "開発符号情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getEcsData_Receive, "設通情報取得処理に到達していること");

            // DB取得引数確認(初期化の為、NULLがセットされていること)
            Assert.IsNull(testDb.getMitakaHeaderData_Receive[0], "過去トラ観たかヘッダー情報取得処理にNULLが渡されていること");
            Assert.IsNull(testDb.getSearchParameterData_Receive[0], "検索条件取得処理にNULLが渡されていること");
            Assert.IsNull(testDb.getReLationUserData_Receive[0], "関連ユーザー情報取得処理にNULLが渡されていること");
            Assert.IsNull(testDb.getDeploymentTroubleData_Receive[0], "展開対象取得処理にNULLが渡されていること");
            Assert.IsNull(testDb.getTitleDrawingData_Receive[0], "タイトル情報取得処理にNULLが渡されていること");
            Assert.IsNull(testDb.getModelData_Receive[0], "機種情報取得処理にNULLが渡されていること");
            Assert.IsNull(testDb.getBlockData_Receive[0], "BLK情報取得処理にNULLが渡されていること");
            Assert.IsNull(testDb.getDevelopmentCodeData_Receive[0], "開発符号情報取得処理にNULLが渡されていること");
            Assert.IsNull(testDb.getEcsData_Receive[0], "設通情報取得処理にNULLが渡されていること");

            // 編集フラグ設定値確認
            Assert.IsFalse(shot01.EditFlg, "編集フラグにfalseが設定されていること");
        }

        [TestMethod()]
        public void MitakaDataTest1()
        {
            // テストパラメータ初期化
            testDb = new TestDAMitakaData();
            var shot01 = new MitakaData(testDb,TestSettings.TEST_PARAM_MANAGE_NO,TestSettings.TEST_PARAM_LOGINUSER,"" ,true);

            // DB取得アクション確認
            Assert.IsNotNull(testDb.getMitakaHeaderData_Receive, "過去トラ観たかヘッダー情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getSearchParameterData_Receive, "検索条件取得処理に到達していること");
            Assert.IsNotNull(testDb.getReLationUserData_Receive, "関連ユーザー情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getDeploymentTroubleData_Receive, "展開対象取得処理に到達していること");
            Assert.IsNotNull(testDb.getTitleDrawingData_Receive, "タイトル情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getModelData_Receive, "機種情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getBlockData_Receive, "BLK情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getDevelopmentCodeData_Receive, "開発符号情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getEcsData_Receive, "設通情報取得処理に到達していること");

            // DB取得引数確認(初期化の為、NULLがセットされていること)
            Assert.AreEqual(testDb.getMitakaHeaderData_Receive[testDb.getMitakaHeaderData_Receive.Count - 1],
                TestSettings.TEST_PARAM_MANAGE_NO,
                "過去トラ観たかヘッダー情報取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getSearchParameterData_Receive[testDb.getSearchParameterData_Receive.Count - 1],
                TestSettings.TEST_PARAM_MANAGE_NO,
                "検索条件取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getReLationUserData_Receive[testDb.getReLationUserData_Receive.Count - 1],
                TestSettings.TEST_PARAM_MANAGE_NO,
                "関連ユーザー情報取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getDeploymentTroubleData_Receive[testDb.getDeploymentTroubleData_Receive.Count - 1],
                TestSettings.TEST_PARAM_MANAGE_NO,
                "展開対象取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getTitleDrawingData_Receive[testDb.getTitleDrawingData_Receive.Count - 1],
                TestSettings.TEST_PARAM_MANAGE_NO,
                "タイトル情報取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getModelData_Receive[testDb.getModelData_Receive.Count - 1],
                TestSettings.TEST_PARAM_MANAGE_NO,
                "機種情報取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getBlockData_Receive[testDb.getBlockData_Receive.Count - 1],
                TestSettings.TEST_PARAM_MANAGE_NO,
                "BLK情報取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getDevelopmentCodeData_Receive[testDb.getDevelopmentCodeData_Receive.Count - 1],
                TestSettings.TEST_PARAM_MANAGE_NO,
                "開発符号情報取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getEcsData_Receive[testDb.getEcsData_Receive.Count - 1],
                TestSettings.TEST_PARAM_MANAGE_NO,
                "設通情報取得処理に管理番号が渡されていること");

            // 編集フラグ設定値確認
            Assert.IsFalse(shot01.EditFlg, "編集フラグにfalseが設定されていること");
            // 過去トラ権限設定確認
            Assert.IsTrue(shot01.MitakaAuthority != "", "過去トラ観たか権限に権限が設定されていること");

        }

        [TestMethod()]
        public void initMitakaDataTest()
        {
            // テストパラメータ初期化
            testDb = new TestDAMitakaData();
            var shot01 = new MitakaData(testDb, true);
            shot01.initMitakaData();

            // DB取得アクション確認
            Assert.IsNotNull(testDb.getMitakaHeaderData_Receive, "過去トラ観たかヘッダー情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getSearchParameterData_Receive, "検索条件取得処理に到達していること");
            Assert.IsNotNull(testDb.getReLationUserData_Receive, "関連ユーザー情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getDeploymentTroubleData_Receive, "展開対象取得処理に到達していること");
            Assert.IsNotNull(testDb.getTitleDrawingData_Receive, "タイトル情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getModelData_Receive, "機種情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getBlockData_Receive, "BLK情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getDevelopmentCodeData_Receive, "開発符号情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getEcsData_Receive, "設通情報取得処理に到達していること");

            // DB取得引数確認(初期化の為、NULLがセットされていること)
            Assert.IsNull(testDb.getMitakaHeaderData_Receive[testDb.getMitakaHeaderData_Receive.Count-1],
                "過去トラ観たかヘッダー情報取得処理にNULLが渡されていること");
            Assert.IsNull(testDb.getSearchParameterData_Receive[testDb.getSearchParameterData_Receive.Count-1], 
                "検索条件取得処理にNULLが渡されていること");
            Assert.IsNull(testDb.getReLationUserData_Receive[testDb.getReLationUserData_Receive.Count-1], 
                "関連ユーザー情報取得処理にNULLが渡されていること");
            Assert.IsNull(testDb.getDeploymentTroubleData_Receive[testDb.getDeploymentTroubleData_Receive.Count-1], 
                "展開対象取得処理にNULLが渡されていること");
            Assert.IsNull(testDb.getTitleDrawingData_Receive[testDb.getTitleDrawingData_Receive.Count-1], 
                "タイトル情報取得処理にNULLが渡されていること");
            Assert.IsNull(testDb.getModelData_Receive[testDb.getModelData_Receive.Count-1], 
                "機種情報取得処理にNULLが渡されていること");
            Assert.IsNull(testDb.getBlockData_Receive[testDb.getBlockData_Receive.Count-1], 
                "BLK情報取得処理にNULLが渡されていること");
            Assert.IsNull(testDb.getDevelopmentCodeData_Receive[testDb.getDevelopmentCodeData_Receive.Count-1], 
                "開発符号情報取得処理にNULLが渡されていること");
            Assert.IsNull(testDb.getEcsData_Receive[testDb.getEcsData_Receive.Count-1], 
                "設通情報取得処理にNULLが渡されていること");

            // 編集フラグ設定値確認
            Assert.IsFalse(shot01.EditFlg, "編集フラグにfalseが設定されていること");

        }

        [TestMethod()]
        public void checkMitakaDataTest()
        {
            // テストパラメータ初期化
            testDb = new TestDAMitakaData();
            var shot = new MitakaData(TestSettings.TEST_PARAM_LOGINUSER,true);

            #region SHOT01・SHOT02
            // デフォルト（設定無し）
            var result01 = shot.checkMitakaData("1");
            var result02 = shot.checkMitakaData("2");
            // SHOT01:チェックNGとなること
            Assert.IsFalse(result01, "過去トラ観たか項目チェックから想定通りの値が返却されること");
            // SHOT02:チェックNGとなること
            Assert.IsFalse(result02, "過去トラ観たか項目チェックから想定通りの値が返却されること");
            #endregion

            #region SHOT03・SHOT04
            //  管理番号設定
            shot = new MitakaData(
                TestSettings.TEST_PARAM_MANAGE_NO, 
                TestSettings.TEST_PARAM_LOGINUSER, "", true);

            var result03 = shot.checkMitakaData("1");
            var result04 = shot.checkMitakaData("2");
            // SHOT01:チェックNGとなること
            Assert.IsFalse(result03, "過去トラ観たか項目チェックから想定通りの値が返却されること");
            // SHOT02:チェックOKとなること
            Assert.IsTrue(result04, "過去トラ観たか項目チェックから想定通りの値が返却されること");
            #endregion

            #region SHOT05・SHOT06
            //  管理番号・タイトル設定
            shot.Title = TestSettings.TEST_PARAM_TITLE;

            var result05 = shot.checkMitakaData("1");
            var result06 = shot.checkMitakaData("2");
            // SHOT01:チェックNGとなること
            Assert.IsFalse(result05, "過去トラ観たか項目チェックから想定通りの値が返却されること");
            // SHOT02:チェックOKとなること
            Assert.IsTrue(result06, "過去トラ観たか項目チェックから想定通りの値が返却されること");
            #endregion

            #region SHOT07・SHOT08
            //  管理番号・タイトル・目的設定
            shot.Purpose = TestSettings.TEST_PARAM_PURPOSE;

            var result07 = shot.checkMitakaData("1");
            var result08 = shot.checkMitakaData("2");
            // SHOT01:チェックNGとなること
            Assert.IsFalse(result07, "過去トラ観たか項目チェックから想定通りの値が返却されること");
            // SHOT02:チェックOKとなること
            Assert.IsTrue(result08, "過去トラ観たか項目チェックから想定通りの値が返却されること");
            #endregion

            #region SHOT09・SHOT10
            //  管理番号・タイトル・目的・回答終了日時設定
            shot.EndDateTime = TestSettings.TEST_PARAM_DATE_END;

            var result09 = shot.checkMitakaData("1");
            var result10 = shot.checkMitakaData("2");
            // SHOT01:チェックNGとなること
            Assert.IsFalse(result09, "過去トラ観たか項目チェックから想定通りの値が返却されること");
            // SHOT02:チェックOKとなること
            Assert.IsTrue(result10, "過去トラ観たか項目チェックから想定通りの値が返却されること");
            #endregion

            #region SHOT11・SHOT12
            //  管理番号・タイトル・目的・回答終了日時・検索条件設定
            shot.EndDateTime = TestSettings.TEST_PARAM_DATE_END;

            var result11 = shot.checkMitakaData("1");
            var result12 = shot.checkMitakaData("2");
            // SHOT01:チェックNGとなること
            Assert.IsFalse(result11, "過去トラ観たか項目チェックから想定通りの値が返却されること");
            // SHOT02:チェックOKとなること
            Assert.IsTrue(result12, "過去トラ観たか項目チェックから想定通りの値が返却されること");
            #endregion

            #region SHOT13・SHOT14
            //  管理番号・タイトル・目的・回答終了日時・検索条件・作成者設定
            var createMainUser = shot.CreateMainUser;
            createMainUser["USER_ID"] = TestSettings.TEST_PARAM_RELATION_USER;
            shot.CreateMainUser = createMainUser;

            var result13 = shot.checkMitakaData("1");
            var result14 = shot.checkMitakaData("2");
            // SHOT01:チェックNGとなること
            Assert.IsFalse(result13, "過去トラ観たか項目チェックから想定通りの値が返却されること");
            // SHOT02:チェックOKとなること
            Assert.IsTrue(result14, "過去トラ観たか項目チェックから想定通りの値が返却されること");
            #endregion

            #region SHOT15・SHOT16
            //  管理番号・タイトル・目的・回答終了日時・検索条件・作成者設定
            var inspectionUser = shot.InspectionUser;
            inspectionUser["USER_ID"] = TestSettings.TEST_PARAM_RELATION_USER;
            shot.InspectionUser = inspectionUser;

            var result15 = shot.checkMitakaData("1");
            var result16 = shot.checkMitakaData("2");
            // SHOT01:チェックNGとなること
            Assert.IsTrue(result15, "過去トラ観たか項目チェックから想定通りの値が返却されること");
            // SHOT02:チェックOKとなること
            Assert.IsTrue(result16, "過去トラ観たか項目チェックから想定通りの値が返却されること");
            #endregion

        }

        [TestMethod()]
        public void getMitakaDataTest()
        {
            // テストパラメータ初期化
            testDb = new TestDAMitakaData();
            var shot01 = new MitakaData(testDb, TestSettings.TEST_PARAM_MANAGE_NO, TestSettings.TEST_PARAM_LOGINUSER, "", true);
            shot01.initMitakaData();

            #region ヘッダーのみ取得

            // メソッド実行
            shot01.getMitakaData();

            // DB取得アクション確認
            Assert.IsNotNull(testDb.getMitakaHeaderData_Receive, "過去トラ観たかヘッダー情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getSearchParameterData_Receive, "検索条件取得処理に到達していること");
            Assert.IsNotNull(testDb.getReLationUserData_Receive, "関連ユーザー情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getDeploymentTroubleData_Receive, "展開対象取得処理に到達していること");
            Assert.IsNotNull(testDb.getTitleDrawingData_Receive, "タイトル情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getModelData_Receive, "機種情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getBlockData_Receive, "BLK情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getDevelopmentCodeData_Receive, "開発符号情報取得処理に到達していること");
            Assert.IsNotNull(testDb.getEcsData_Receive, "設通情報取得処理に到達していること");

            // DB取得引数確認(初期化の為、NULLがセットされていること)
            Assert.AreEqual(testDb.getMitakaHeaderData_Receive[testDb.getMitakaHeaderData_Receive.Count - 1],
                TestSettings.TEST_PARAM_MANAGE_NO,
                "過去トラ観たかヘッダー情報取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getReLationUserData_Receive[testDb.getReLationUserData_Receive.Count - 1],
                TestSettings.TEST_PARAM_MANAGE_NO,
                "関連ユーザー情報取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getSearchParameterData_Receive[testDb.getSearchParameterData_Receive.Count - 1],
                null,
                "検索条件取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getDeploymentTroubleData_Receive[testDb.getDeploymentTroubleData_Receive.Count - 1],
                null,
                "展開対象取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getTitleDrawingData_Receive[testDb.getTitleDrawingData_Receive.Count - 1],
                null,
                "タイトル情報取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getModelData_Receive[testDb.getModelData_Receive.Count - 1],
                null,
                "機種情報取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getBlockData_Receive[testDb.getBlockData_Receive.Count - 1],
                null,
                "BLK情報取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getDevelopmentCodeData_Receive[testDb.getDevelopmentCodeData_Receive.Count - 1],
                null,
                "開発符号情報取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getEcsData_Receive[testDb.getEcsData_Receive.Count - 1],
                null,
                "設通情報取得処理に管理番号が渡されていること");

            // 編集フラグ設定値確認
            Assert.IsFalse(shot01.EditFlg, "編集フラグにfalseが設定されていること");
            // 過去トラ権限設定確認
            Assert.IsTrue(shot01.MitakaAuthority != "", "過去トラ観たか権限に権限が設定されていること");

            #endregion

            #region 全取得

            shot01.getMitakaData(true);

            // DB取得引数確認(初期化の為、NULLがセットされていること)
            Assert.AreEqual(testDb.getMitakaHeaderData_Receive[testDb.getMitakaHeaderData_Receive.Count - 1],
                TestSettings.TEST_PARAM_MANAGE_NO,
                "過去トラ観たかヘッダー情報取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getReLationUserData_Receive[testDb.getReLationUserData_Receive.Count - 1],
                TestSettings.TEST_PARAM_MANAGE_NO,
                "関連ユーザー情報取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getSearchParameterData_Receive[testDb.getSearchParameterData_Receive.Count - 1],
                TestSettings.TEST_PARAM_MANAGE_NO,
                "検索条件取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getDeploymentTroubleData_Receive[testDb.getDeploymentTroubleData_Receive.Count - 1],
                TestSettings.TEST_PARAM_MANAGE_NO,
                "展開対象取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getTitleDrawingData_Receive[testDb.getTitleDrawingData_Receive.Count - 1],
                TestSettings.TEST_PARAM_MANAGE_NO,
                "タイトル情報取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getModelData_Receive[testDb.getModelData_Receive.Count - 1],
                TestSettings.TEST_PARAM_MANAGE_NO,
                "機種情報取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getBlockData_Receive[testDb.getBlockData_Receive.Count - 1],
                TestSettings.TEST_PARAM_MANAGE_NO,
                "BLK情報取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getDevelopmentCodeData_Receive[testDb.getDevelopmentCodeData_Receive.Count - 1],
                TestSettings.TEST_PARAM_MANAGE_NO,
                "開発符号情報取得処理に管理番号が渡されていること");
            Assert.AreEqual(testDb.getEcsData_Receive[testDb.getEcsData_Receive.Count - 1],
                TestSettings.TEST_PARAM_MANAGE_NO,
                "設通情報取得処理に管理番号が渡されていること");

            #endregion
        }

        [TestMethod()]
        public void postMitakaDataTest()
        {
            // テストパラメータ初期化
            testDb = new TestDAMitakaData();
            var _db = new DAMitakaData(TestSettings.TEST_PARAM_LOGINUSER);

            testDb.GetManageTestNo = TestSettings.TEST_PARAM_MANAGE_NO;
            testDb.GetMitakaHeaderTestData = _db.getMitakaHeaderData();
            testDb.GetRelationUserTestData= _db.getReLationUserData();
            testDb.GetSearchParameterTestData = _db.getSearchParameterData();
            testDb.GetDeployTroubleTestData= _db.getDeploymentTroubleData();
            testDb.GetDrawingTestData= _db.getTitleDrawingData();
            testDb.GetModelTestData= _db.getModelData();
            testDb.GetBlockTestData= _db.getBlockData();
            testDb.GetDevelopCodeTestData= _db.getDevelopmentCodeData();
            testDb.GetEcsTestData= _db.getEcsData();
            testDb.GetMitakaAnswerTestData= _db.getMitakaAnswerData();
            testDb.ExistResult = false; 
            testDb.PostResult = true;

            testClass = new MitakaData(testDb, true);

            // タイトル
            testClass.Title = TestSettings.TEST_PARAM_TITLE;
            // 目的
            testClass.Purpose = TestSettings.TEST_PARAM_PURPOSE;
            // コメント
            testClass.Comment = TestSettings.TEST_PARAM_COMMENT;
            // 回答終了日時
            testClass.EndDateTime = TestSettings.TEST_PARAM_DATE_END;
            // 作成者（主）
            var drCreateMain = testClass.CreateMainUser;
            drCreateMain["RELATION_TYPE"] = "99"; // 作成者（主）でないコードを設定
            drCreateMain["USER_ID"] = TestSettings.TEST_PARAM_RELATION_USER;
            testClass.CreateMainUser = drCreateMain;
            // 作成者（副）
            var drCreateSub = testClass.CreateSubUser;
            drCreateSub["RELATION_TYPE"] = "99"; // 作成者（副）でないコードを設定
            drCreateSub["USER_ID"] = TestSettings.TEST_PARAM_RELATION_USER;
            testClass.CreateSubUser = drCreateSub;
            // 点検者
            var drInspection = testClass.InspectionUser;
            drInspection["RELATION_TYPE"] = "99"; // 点検者でないコードを設定
            drInspection["USER_ID"] = TestSettings.TEST_PARAM_RELATION_USER;
            testClass.InspectionUser = drInspection;
            // 回答依頼先リスト
            var dtRequest = testClass.ConfirmationRequestList;
            for (int i = 1; i <= 2; i++)
            {
                var drRequest = dtRequest.NewRow();
                drRequest["RELATION_TYPE"] = "99"; // 回答依頼先でないコードを設定
                drRequest["USER_ID"] = TestSettings.TEST_PARAM_RELATION_USER;
                dtRequest.Rows.Add(drRequest);
            }
            testClass.ConfirmationRequestList = dtRequest.Copy();
            // 展開対象リスト
            var dtTrouble = testClass.DeploymentTroubleList;
            for (int i = 1; i <= 2; i++)
            {
                var drTrouble = dtTrouble.NewRow();
                drTrouble["SYSTEM_NO"] = TestSettings.TEST_PARAM_DEPLOY_SYSTEMNO;
                drTrouble["TARGET_FLG"] = "1"; // 回答依頼先でないコードを設定
                dtTrouble.Rows.Add(drTrouble);
            }
            testClass.DeploymentTroubleList = dtTrouble.Copy();
            // タイトル図面情報リスト
            var dtDrawing = testClass.TitleDrawingList;
            for (int i = 1; i <= 2; i++)
            {
                var drDrawing = dtDrawing.NewRow();
                drDrawing["TITLE_DRAWING_NO"] = TestSettings.TEST_PARAM_DRAWING_NO;
                dtDrawing.Rows.Add(drDrawing);
            }
            testClass.TitleDrawingList = dtDrawing.Copy();
            // 機種情報リスト
            var dtModel = testClass.ModelList;
            for (int i = 1; i <= 2; i++)
            {
                var drModel = dtModel.NewRow();
                drModel["MODEL"] = TestSettings.TEST_PARAM_MODEL_NO;
                dtModel.Rows.Add(drModel);
            }
            testClass.ModelList = dtModel.Copy();
            // BLK情報リスト
            var dtBlock = testClass.BlockList;
            for (int i = 1; i <= 2; i++)
            {
                var drBlock = dtBlock.NewRow();
                drBlock["BLK_NO"] = TestSettings.TEST_PARAM_BLOCK_NO;
                dtBlock.Rows.Add(drBlock);
            }
            testClass.BlockList = dtBlock.Copy();
            // 開発符号情報リスト
            var dtDevelop = testClass.DevelopmentCodeList;
            for (int i = 1; i <= 2; i++)
            {
                var drDevelop = dtDevelop.NewRow();
                drDevelop["DEVELOPMENT_CODE"] = TestSettings.TEST_PARAM_DEVELOPMENT_CODE;
                dtDevelop.Rows.Add(drDevelop);
            }
            testClass.DevelopmentCodeList = dtDevelop.Copy();
            // 設通情報リスト
            var dtEcs = testClass.EcsList;
            for (int i = 1; i <= 2; i++)
            {
                var drEcs = dtEcs.NewRow();
                drEcs["ECS_NO"] = TestSettings.TEST_PARAM_ECS_NO;
                dtEcs.Rows.Add(drEcs);
            }
            testClass.EcsList = dtEcs.Copy();
            var dtAnswer = testClass.MitakaAnswerData.MitakaAnswerList;
            for (int i = 1; i <= 2; i++)
            {
                var drAnswer = dtAnswer.NewRow();
                drAnswer["MITAKA_NO"] = TestSettings.TEST_PARAM_MANAGE_NO;
                drAnswer["SYSTEM_NO"] = TestSettings.TEST_PARAM_ANSWER_SYSTEMNO;
                drAnswer["ANSWER_DIVISION_CODE"] = TestSettings.TEST_PARAM_ANSWER_DIVISION_CODE;
                drAnswer["STATUS"] = TestSettings.TEST_PARAM_ANSWER_STATUS;
                drAnswer["ANSWER_CONTENT"] = TestSettings.TEST_PARAM_ANSWER_CONTENT;
                dtAnswer.Rows.Add(drAnswer);
            }
            testClass.MitakaAnswerData.MitakaAnswerList = dtAnswer.Copy();


            // メソッド実行
            var result01 = testClass.postMitakaData();

            // 過去トラ観たか情報登録処理結果確認
            Assert.IsTrue(result01, "過去トラ観たか情報登録結果がOKであること");
            Assert.IsNotNull(testDb.postMitakaHeaderData_Receive,
                "過去トラ観たかヘッダー登録に到達していること");
            Assert.IsNotNull(testDb.postReLationUserData_Receive,
                "関連ユーザー情報登録に到達していること");
            Assert.IsNotNull(testDb.postDeploymentTroubleData_Receive,
                "展開対象登録に到達していること");
            Assert.IsNotNull(testDb.postTitleDrawingData_Receive,
                "タイトル品番情報登録に到達していること");
            Assert.IsNotNull(testDb.postModelData_Receive,
                "機種情報登録に到達していること");
            Assert.IsNotNull(testDb.postBlockData_Receive,
                "BLK情報登録に到達していること");
            Assert.IsNotNull(testDb.postDevelopmentCodeData_Receive,
                "開発符号情報登録に到達していること");
            Assert.IsNotNull(testDb.postEcsData_Receive,
                "設通情報登録に到達していること");
            Assert.IsTrue(testClass.embeddedScript != "",
                "埋め込みスクリプトが設定されていること");
        }

        [TestMethod()]
        public void deleteMitakaDataTest()
        {
            // テストパラメータ初期化
            testDb = new TestDAMitakaData();
            testDb.ExistResult = true;
            testDb.DeleteResult = true;

            testClass = new MitakaData(testDb,
                TestSettings.TEST_PARAM_MANAGE_NO,
               TestSettings.TEST_PARAM_LOGINUSER,"" , true);

            // メソッド実行
                var result01 = testClass.deleteMitakaData();

            // 過去トラ観たか情報削除処理結果確認
            Assert.IsTrue(result01, "過去トラ観たか情報削除結果がOKであること");
            Assert.IsNotNull(testDb.deleteMitakaHeaderData_Receive,
                "過去トラ観たかヘッダー登録に到達していること");
            Assert.IsNotNull(testDb.deleteReLationUserData_Receive_All,
                "関連ユーザー情報登録に到達していること");
            Assert.IsNotNull(testDb.deleteDeploymentTroubleData_Receive_All,
                "展開対象登録に到達していること");
            Assert.IsNotNull(testDb.deleteTitleDrawingData_Receive_All,
                "タイトル品番情報登録に到達していること");
            Assert.IsNotNull(testDb.deleteModelData_Receive_All,
                "機種情報登録に到達していること");
            Assert.IsNotNull(testDb.deleteBlockData_Receive_All,
                "BLK情報登録に到達していること");
            Assert.IsNotNull(testDb.deleteDevelopmentCodeData_Receive_All,
                "開発符号情報登録に到達していること");
            Assert.IsNotNull(testDb.deleteEcsData_Receive_All,
                "設通情報登録に到達していること");
            Assert.IsTrue(testClass.embeddedScript != "",
                "埋め込みスクリプトが設定されていること");
        }

        [TestMethod()]
        public void requestMitakaDataTest()
        {
            // テストパラメータ初期化
            testDb = new TestDAMitakaData();
            var _db = new DAMitakaData(TestSettings.TEST_PARAM_LOGINUSER);
            testDb.GetMitakaHeaderTestData = _db.getMitakaHeaderData();
            testDb.GetRelationUserTestData = _db.getReLationUserData();
            testDb.GetSearchParameterTestData = _db.getSearchParameterData();
            testDb.GetDeployTroubleTestData = _db.getDeploymentTroubleData();
            testDb.GetDrawingTestData = _db.getTitleDrawingData();
            testDb.GetModelTestData = _db.getModelData();
            testDb.GetBlockTestData = _db.getBlockData();
            testDb.GetDevelopCodeTestData = _db.getDevelopmentCodeData();
            testDb.GetEcsTestData = _db.getEcsData();
            testDb.GetMitakaAnswerTestData = _db.getMitakaAnswerData();
            testDb.ExistResult = false;
            testDb.UpdateResult = true;

            testClass = new MitakaData(testDb,
                TestSettings.TEST_PARAM_MANAGE_NO,
               TestSettings.TEST_PARAM_LOGINUSER, "", true);

            // 状況コメント設定
            testClass.StatusComment = TestSettings.TEST_PARAM_STATUS_COMMENT;

            var result01 = testClass.requestMitakaData();

            Assert.IsTrue(result01, "過去トラ観たか情報点検依頼結果がOKであること");
            Assert.IsNotNull(testDb.updateHeaderDataToStatus_Receive,
                "過去トラ観たかヘッダーステータス更新処理に到達していること");
            Assert.IsTrue(testDb.updateHeaderDataToStatus_Receive.Rows[0]["MITAKA_NO"].ToString()
                == TestSettings.TEST_PARAM_MANAGE_NO,
                "過去トラ観たかヘッダーステータス更新処理に管理番号が渡されていること");
            Assert.IsTrue(testDb.updateHeaderDataToStatus_Receive.Rows[0]["STATUS"].ToString()
                == Def.MITAKA_STATUS_ANSWER ,
                "過去トラ観たかヘッダーステータス更新処理にステータス（点検依頼）が渡されていること");
            Assert.IsTrue(testDb.updateHeaderDataToStatus_Receive.Rows[0].IsNull("REASON"),
                "過去トラ観たかヘッダーステータス更新処理に状況コメントが渡されていないこと");
        }

        [TestMethod()]
        public void cancelMitakaDataTest()
        {
            // テストパラメータ初期化
            testDb = new TestDAMitakaData();
            var _db = new DAMitakaData(TestSettings.TEST_PARAM_LOGINUSER);
            testDb.GetMitakaHeaderTestData = _db.getMitakaHeaderData();
            testDb.GetRelationUserTestData = _db.getReLationUserData();
            testDb.GetSearchParameterTestData = _db.getSearchParameterData();
            testDb.GetDeployTroubleTestData = _db.getDeploymentTroubleData();
            testDb.GetDrawingTestData = _db.getTitleDrawingData();
            testDb.GetModelTestData = _db.getModelData();
            testDb.GetBlockTestData = _db.getBlockData();
            testDb.GetDevelopCodeTestData = _db.getDevelopmentCodeData();
            testDb.GetEcsTestData = _db.getEcsData();
            testDb.GetMitakaAnswerTestData = _db.getMitakaAnswerData();
            testDb.ExistResult = false;
            testDb.UpdateResult = true;

            testClass = new MitakaData(testDb,
                TestSettings.TEST_PARAM_MANAGE_NO,
               TestSettings.TEST_PARAM_LOGINUSER, "", true);

            // 状況コメント設定
            testClass.StatusComment = TestSettings.TEST_PARAM_STATUS_COMMENT;

            var result01 = testClass.cancelMitakaData();

            Assert.IsTrue(result01, "過去トラ観たか情報点検依頼結果がOKであること");
            Assert.IsNotNull(testDb.updateHeaderDataToStatus_Receive,
                "過去トラ観たかヘッダーステータス更新処理に到達していること");
            Assert.IsTrue(testDb.updateHeaderDataToStatus_Receive.Rows[0]["MITAKA_NO"].ToString()
                == TestSettings.TEST_PARAM_MANAGE_NO,
                "過去トラ観たかヘッダーステータス更新処理に管理番号が渡されていること");
            Assert.IsTrue(testDb.updateHeaderDataToStatus_Receive.Rows[0]["STATUS"].ToString()
                == Def.MITAKA_STATUS_CANCEL,
                "過去トラ観たかヘッダーステータス更新処理にステータス（取消）が渡されていること");
            Assert.IsTrue(!testDb.updateHeaderDataToStatus_Receive.Rows[0].IsNull("REASON"),
                "過去トラ観たかヘッダーステータス更新処理に状況コメントが渡されていること");
        }

        [TestMethod()]
        public void confirmedMitakaDataTest()
        {
            // テストパラメータ初期化
            testDb = new TestDAMitakaData();
            var _db = new DAMitakaData(TestSettings.TEST_PARAM_LOGINUSER);
            testDb.GetMitakaHeaderTestData = _db.getMitakaHeaderData();
            testDb.GetRelationUserTestData = _db.getReLationUserData();
            testDb.GetSearchParameterTestData = _db.getSearchParameterData();
            testDb.GetDeployTroubleTestData = _db.getDeploymentTroubleData();
            testDb.GetDrawingTestData = _db.getTitleDrawingData();
            testDb.GetModelTestData = _db.getModelData();
            testDb.GetBlockTestData = _db.getBlockData();
            testDb.GetDevelopCodeTestData = _db.getDevelopmentCodeData();
            testDb.GetEcsTestData = _db.getEcsData();
            testDb.GetMitakaAnswerTestData = _db.getMitakaAnswerData();
            testDb.ExistResult = false;
            testDb.UpdateResult = true;

            testClass = new MitakaData(testDb,
                TestSettings.TEST_PARAM_MANAGE_NO,
               TestSettings.TEST_PARAM_LOGINUSER, "", true);

            // 状況コメント設定
            testClass.StatusComment = TestSettings.TEST_PARAM_STATUS_COMMENT;

            var result01 = testClass.confirmedMitakaData();

            Assert.IsTrue(result01, "過去トラ観たか情報点検依頼結果がOKであること");
            Assert.IsNotNull(testDb.updateHeaderDataToStatus_Receive,
                "過去トラ観たかヘッダーステータス更新処理に到達していること");
            Assert.IsTrue(testDb.updateHeaderDataToStatus_Receive.Rows[0]["MITAKA_NO"].ToString()
                == TestSettings.TEST_PARAM_MANAGE_NO,
                "過去トラ観たかヘッダーステータス更新処理に管理番号が渡されていること");
            Assert.IsTrue(testDb.updateHeaderDataToStatus_Receive.Rows[0]["STATUS"].ToString()
                == Def.MITAKA_STATUS_CONFIRMED,
                "過去トラ観たかヘッダーステータス更新処理にステータス（取消）が渡されていること");
            Assert.IsTrue(testDb.updateHeaderDataToStatus_Receive.Rows[0].IsNull("REASON"),
                "過去トラ観たかヘッダーステータス更新処理に状況コメントが渡されていないこと");
        }

        [TestMethod()]
        public void cancellConfirmedMitakaDataTest()
        {
            // テストパラメータ初期化
            testDb = new TestDAMitakaData();
            var _db = new DAMitakaData(TestSettings.TEST_PARAM_LOGINUSER);
            var dtHeader = _db.getMitakaHeaderData();
            var drHeader = dtHeader.NewRow();
            drHeader["STATUS"] = Def.MITAKA_STATUS_CONFIRMED;
            dtHeader.Rows.Add(drHeader);
            testDb.GetMitakaHeaderTestData = dtHeader.Copy();

            testDb.GetRelationUserTestData = _db.getReLationUserData();
            testDb.GetSearchParameterTestData = _db.getSearchParameterData();
            testDb.GetDeployTroubleTestData = _db.getDeploymentTroubleData();
            testDb.GetDrawingTestData = _db.getTitleDrawingData();
            testDb.GetModelTestData = _db.getModelData();
            testDb.GetBlockTestData = _db.getBlockData();
            testDb.GetDevelopCodeTestData = _db.getDevelopmentCodeData();
            testDb.GetEcsTestData = _db.getEcsData();
            testDb.GetMitakaAnswerTestData = _db.getMitakaAnswerData();
            testDb.ExistResult = false;
            testDb.UpdateResult = true;

            testClass = new MitakaData(testDb,
                TestSettings.TEST_PARAM_MANAGE_NO,
               TestSettings.TEST_PARAM_LOGINUSER, "", true);

            // 状況コメント設定
            testClass.StatusComment = TestSettings.TEST_PARAM_STATUS_COMMENT;

            var result01 = testClass.confirmedMitakaData();
            result01 = testClass.cancellConfirmedMitakaData();

            Assert.IsTrue(result01, "過去トラ観たか情報点検依頼結果がOKであること");
            Assert.IsNotNull(testDb.updateHeaderDataToStatus_Receive,
                "過去トラ観たかヘッダーステータス更新処理に到達していること");
            Assert.IsTrue(testDb.updateHeaderDataToStatus_Receive.Rows[0]["MITAKA_NO"].ToString()
                == TestSettings.TEST_PARAM_MANAGE_NO,
                "過去トラ観たかヘッダーステータス更新処理に管理番号が渡されていること");
            Assert.IsTrue(testDb.updateHeaderDataToStatus_Receive.Rows[0]["STATUS"].ToString()
                == Def.MITAKA_STATUS_ANSWER,
                "過去トラ観たかヘッダーステータス更新処理にステータス（完了取消）が渡されていること");
            Assert.IsTrue(testDb.updateHeaderDataToStatus_Receive.Rows[0].IsNull("REASON"),
                "過去トラ観たかヘッダーステータス更新処理に状況コメントが渡されていないこと");
        }
    }
}