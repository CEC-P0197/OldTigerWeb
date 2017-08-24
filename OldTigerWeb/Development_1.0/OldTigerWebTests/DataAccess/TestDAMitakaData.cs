using System.Collections.Generic;
using System.Data;
using OldTigerWeb.BuisinessLogic;

namespace OldTigerWeb.DataAccess.Tests
{
    /// <summary>
    /// 過去トラ観たかDB インターフェース
    /// </summary>
    public class TestDAMitakaData:IDAMitakaData
    {
        #region プロパティ
        public bool ProccessSuccess { get; set; }
        public List<string> ErrorMessage
        {
            get
            {
                return _ErrorMessage;
            }
            set
            {
                if (value != null)
                    _ErrorMessage = value;
            }
        }
        #endregion

        #region テスト用フィールド
        private IDAMitakaData _dbMitakaData;

        private string _GetManageTestNo;
        private DataTable _GetManageNoTestData;
        private DataTable _GetMitakaHeaderTestData;
        private DataTable _GetRelationUserTestData;
        private DataTable _GetSearchParameterTestData;
        private DataTable _GetDeployTroubleTestData;
        private DataTable _GetDrawingTestData;
        private DataTable _GetModelTestData;
        private DataTable _GetBlockTestData;
        private DataTable _GetDevelopCodeTestData;
        private DataTable _GetEcsTestData;
        private DataTable _GetMitakaAnswerTestData;
        private bool _ExistResult;
        private bool _InsertResult;
        private bool _UpdateResult;
        private bool _DeleteResult;
        private bool _PostResult;
        private List<string> _ErrorMessage;

        #region 管理番号リスト取得関連
        public List<string> getMaxManageNo_Receive;
        public Dictionary<string, MitakaSearchData> getManageNoFromMitakaSearchData_Receive;
        public int? getManageNoFromRelationUser_Receive; 
        #endregion

        #region 過去トラ観たかヘッダー関連
        public List<string> getMitakaHeaderData_Receive;
        public List<string> existMitakaHeaderData_Receive;
        public Dictionary<string, DataTable> postMitakaHeaderData_Receive;
        public DataTable insertMitakaHeaderData_Receive;
        public DataTable updateMitakaHeaderData_Receive;
        public DataTable updateHeaderDataToStatus_Receive;
        public List<string> deleteMitakaHeaderData_Receive;
        #endregion

        #region 関連ユーザー情報関連
        public List<string> getReLationUserData_Receive;
        public DataTable existReLationUserData_Receive;
        public Dictionary<string, DataTable> postReLationUserData_Receive;
        public DataTable insertReLationUserData_Receive;
        public DataTable deleteReLationUserData_Receive;
        public List<string> deleteReLationUserData_Receive_All;
        #endregion

        #region 検索条件関連
        public List<string> getSearchParameterData_Receive;
        public DataTable existSearchParameterData_Receive;
        public Dictionary<string, DataTable> postSearchParameterData_Receive;
        public DataTable insertSearchParameterData_Receive;
        public DataTable deleteSearchParameterData_Receive;
        public List<string> deleteSearchParameterData_Receive_All;
        #endregion

        #region 展開対象関連
        public List<string> getDeploymentTroubleData_Receive;
        public DataTable existDeploymentTroubleData_Receive;
        public Dictionary<string, DataTable> postDeploymentTroubleData_Receive;
        public DataTable insertDeploymentTroubleData_Receive;
        public DataTable deleteDeploymentTroubleData_Receive;
        public List<string> deleteDeploymentTroubleData_Receive_All;
        #endregion

        #region タイトル品番情報関連
        public List<string> getTitleDrawingData_Receive;
        public DataTable existTitleDrawingData_Receive;
        public Dictionary<string, DataTable> postTitleDrawingData_Receive;
        public DataTable insertTitleDrawingData_Receive;
        public DataTable deleteTitleDrawingData_Receive;
        public List<string> deleteTitleDrawingData_Receive_All;
        #endregion

        #region 機種情報関連
        public List<string> getModelData_Receive;
        public DataTable existModelData_Receive;
        public Dictionary<string, DataTable> postModelData_Receive;
        public DataTable insertModelData_Receive;
        public DataTable deleteModelData_Receive;
        public List<string> deleteModelData_Receive_All;
        #endregion

        #region BLK情報関連
        public List<string> getBlockData_Receive;
        public DataTable existBlockData_Receive;
        public Dictionary<string, DataTable> postBlockData_Receive;
        public DataTable insertBlockData_Receive;
        public DataTable deleteBlockData_Receive;
        public List<string> deleteBlockData_Receive_All;
        #endregion

        #region 開発符号情報
        public List<string> getDevelopmentCodeData_Receive;
        public DataTable existDevelopmentCodeData_Receive;
        public Dictionary<string, DataTable> postDevelopmentCodeData_Receive;
        public DataTable insertDevelopmentCodeData_Receive;
        public DataTable deleteDevelopmentCodeData_Receive;
        public List<string> deleteDevelopmentCodeData_Receive_All;
        #endregion

        #region 設通情報関連
        public List<string> getEcsData_Receive;
        public DataTable existEcsData_Receive;
        public Dictionary<string, DataTable> postEcsData_Receive;
        public DataTable insertEcsData_Receive;
        public DataTable deleteEcsData_Receive;
        public List<string> deleteEcsData_Receive_All;
        #endregion

        #region 過去トラ観たか回答関連
        public List<string> getMitakaAnswerData_Receive;
        public DataTable existMitakaAnswerData_Receive;
        public Dictionary<string, DataTable> postMitakaAnswerData_Receive;
        public DataTable insertMitakaAnswerData_Receive;
        public DataTable deleteMitakaAnswerData_Receive;
        public List<string> deleteMitakaAnswerData_Receive_All;
        #endregion



        #endregion

        #region テスト用プロパティ
        public string GetManageTestNo {
            get
            {
                return _GetManageTestNo;
            }
            set
            {
                _GetManageTestNo = value;
            }

        }

        public DataTable GetManageNoTestData
        {
            get
            {
                return _GetManageNoTestData;
            }
            set
            {
                _GetManageNoTestData = value;
            }
        }
        public DataTable GetMitakaHeaderTestData
        {
            get
            {
                return _GetMitakaHeaderTestData;
            }
            set
            {
                _GetMitakaHeaderTestData = value;
            }
        }
        public DataTable GetRelationUserTestData
        {
            get
            {
                return _GetRelationUserTestData;
            }
            set
            {
                _GetRelationUserTestData = value;
            }
        }
        public DataTable GetSearchParameterTestData
        {
            get
            {
                return _GetSearchParameterTestData;
            }
            set
            {
                _GetSearchParameterTestData = value;
            }
        }
        public DataTable GetDeployTroubleTestData
        {
            get
            {
                return _GetDeployTroubleTestData;
            }
            set
            {
                _GetDeployTroubleTestData = value;
            }
        }
        public DataTable GetDrawingTestData
        {
            get
            {
                return _GetDrawingTestData;
            }
            set
            {
                _GetDrawingTestData = value;
            }
        }
        public DataTable GetModelTestData
        {
            get
            {
                return _GetModelTestData;
            }
            set
            {
                _GetModelTestData = value;
            }
        }
        public DataTable GetBlockTestData
        {
            get
            {
                return _GetBlockTestData;
            }
            set
            {
                _GetBlockTestData = value;
            }
        }
        public DataTable GetDevelopCodeTestData
        {
            get
            {
                return _GetDevelopCodeTestData;
            }
            set
            {
                _GetDevelopCodeTestData = value;
            }
        }
        public DataTable GetEcsTestData
        {
            get
            {
                return _GetEcsTestData;
            }
            set
            {
                _GetEcsTestData = value;
            }
        }
        public DataTable GetMitakaAnswerTestData
        {
            get
            {
                return _GetMitakaAnswerTestData;
            }
            set
            {
                _GetMitakaAnswerTestData = value;
            }
        }

        public bool ExistResult
        {
            get
            {
                return _ExistResult;
            }
            set
            {
                _ExistResult = value;
            }
        }
        public bool InsertResult
        {
            get
            {
                return _InsertResult;
            }
            set
            {
                _InsertResult = value;
            }
        }
        public bool UpdateResult
        {
            get
            {
                return _UpdateResult;
            }
            set
            {
                _UpdateResult = value;
            }
        }
        public bool DeleteResult
        {
            get
            {
                return _DeleteResult;
            }
            set
            {
                _DeleteResult = value;
            }
        }
        public bool PostResult
        {
            get
            {
                return _PostResult;
            }
            set
            {
                _PostResult = value;
            }
        }

        #endregion

        public TestDAMitakaData():this(new DAMitakaData())
        {
            _ErrorMessage= new List<string>();
        }
        public TestDAMitakaData(IDAMitakaData dbMitakaData)
        {
            _dbMitakaData = dbMitakaData;
            _ErrorMessage = new List<string>();
        }

        #region 管理番号リスト取得関連
        public string getMaxManageNo(string divisionCode)
        {
            if (getMaxManageNo_Receive == null)
                getMaxManageNo_Receive = new List<string>();

            getMaxManageNo_Receive.Add(divisionCode);

            return "";
        }
        public DataTable getManageNoFromMitakaSearchData(MitakaSearchData parent)
        {
            if (getManageNoFromMitakaSearchData_Receive == null)
                getManageNoFromMitakaSearchData_Receive = new Dictionary<string, MitakaSearchData>();

            getManageNoFromMitakaSearchData_Receive.Add(
                (getManageNoFromMitakaSearchData_Receive.Count + 1).ToString(),
                parent
                );

            return _GetManageNoTestData;
        }
        public DataTable getManageNoFromRelationUser()
        {
            if (getManageNoFromRelationUser_Receive == null)
                getManageNoFromRelationUser_Receive = 0;

            getManageNoFromRelationUser_Receive++;
            return _GetManageNoTestData;
        }
        #endregion

        #region 過去トラ観たかヘッダー関連
        public DataTable getMitakaHeaderData(string manageNo = null)
        {
            if (getMitakaHeaderData_Receive == null)
                getMitakaHeaderData_Receive = new List<string>();

            getMitakaHeaderData_Receive.Add(manageNo);
            return _GetMitakaHeaderTestData;
        }
        public bool existMitakaHeaderData(string manageNo)
        {
            if (existMitakaHeaderData_Receive == null)
                existMitakaHeaderData_Receive = new List<string>();

            existMitakaHeaderData_Receive.Add(manageNo);
            return _ExistResult;
        }
        public bool postMitakaHeaderData(DataTable param)
        {
            if (postMitakaHeaderData_Receive == null)
                postMitakaHeaderData_Receive = new Dictionary<string, DataTable>();

            postMitakaHeaderData_Receive.Add(
                (postMitakaHeaderData_Receive.Count + 1).ToString(),param);
            return _PostResult;
        }
        public bool insertMitakaHeaderData(DataRow param)
        {
            if (insertMitakaHeaderData_Receive == null)
                insertMitakaHeaderData_Receive = new DataTable();

            insertMitakaHeaderData_Receive.Rows.Add(param);
            return _InsertResult;
        }
        public bool updateMitakaHeaderData(DataRow param)
        {
            if (updateMitakaHeaderData_Receive == null)
                updateMitakaHeaderData_Receive = new DataTable();

            updateMitakaHeaderData_Receive.Rows.Add(param);
            return _UpdateResult;
        }
        public bool updateHeaderDataToStatus(string manageNo, string status, string reason = null)
        {
            if (updateHeaderDataToStatus_Receive == null)
                updateHeaderDataToStatus_Receive = new DataTable();

            if (updateHeaderDataToStatus_Receive.Columns.Count ==0)
            {
                updateHeaderDataToStatus_Receive.Columns.Add("MITAKA_NO");
                updateHeaderDataToStatus_Receive.Columns.Add("STATUS");
                updateHeaderDataToStatus_Receive.Columns.Add("REASON");
            }

            var row = updateHeaderDataToStatus_Receive.NewRow();
            row["MITAKA_NO"] = manageNo;
            row["STATUS"] = status;
            row["REASON"] = reason;
            updateHeaderDataToStatus_Receive.Rows.Add(row);

            return _UpdateResult;
        }
        public bool deleteMitakaHeaderData(string manageNo)
        {
            if (deleteMitakaHeaderData_Receive == null)
                deleteMitakaHeaderData_Receive = new List<string>();

            deleteMitakaHeaderData_Receive.Add(manageNo);

            return _DeleteResult;
        }
        #endregion

        #region 関連ユーザー情報関連
        public DataTable getReLationUserData(string manageNo = null)
        {
            if (getReLationUserData_Receive == null)
                getReLationUserData_Receive = new List<string>();

            getReLationUserData_Receive.Add(manageNo);
            return GetRelationUserTestData;
        }
        public bool existReLationUserData(DataRow param)
        {
            if (existReLationUserData_Receive == null)
                existReLationUserData_Receive = new DataTable();

            existReLationUserData_Receive.Rows.Add(param);
            return _ExistResult;
        }
        public bool postReLationUserData(DataTable param)
        {
            if (postReLationUserData_Receive == null)
                postReLationUserData_Receive = new Dictionary<string, DataTable>();

            postReLationUserData_Receive.Add(
                (postReLationUserData_Receive.Count + 1).ToString(), param);
             
            return _PostResult;
        }
        public bool insertReLationUserData(DataRow param)
        {
            if (insertReLationUserData_Receive == null)
                insertReLationUserData_Receive = new DataTable();

            insertReLationUserData_Receive.Rows.Add(param);
            return _InsertResult;
        }
        public bool deleteReLationUserData(DataRow param)
        {
            if (deleteReLationUserData_Receive == null)
                deleteReLationUserData_Receive = new DataTable();

            deleteReLationUserData_Receive.Rows.Add(param);

            return _DeleteResult;
        }
        public bool deleteReLationUserData(string manageNo)
        {
            if (deleteReLationUserData_Receive_All == null)
                deleteReLationUserData_Receive_All = new List<string>();

            deleteReLationUserData_Receive_All.Add(manageNo);

            return _DeleteResult;
        }
        #endregion

        #region 検索条件関連
        public DataTable getSearchParameterData(string manageNo = null)
        {
            if (getSearchParameterData_Receive == null)
                getSearchParameterData_Receive = new List<string>();

            getSearchParameterData_Receive.Add(manageNo);
            return _GetSearchParameterTestData;
        }
        public bool existSearchParameterData(DataRow param)
        {
            if (existSearchParameterData_Receive == null)
                existSearchParameterData_Receive = new DataTable();

            existSearchParameterData_Receive.Rows.Add(param);

            return _ExistResult;
        }
        public bool postSearchParameterData(DataTable param)
        {
            if (postSearchParameterData_Receive == null)
                postSearchParameterData_Receive = new Dictionary<string, DataTable>();

            postSearchParameterData_Receive.Add(
                (postSearchParameterData_Receive.Count + 1).ToString(), param);

            return _PostResult;
        }
        public bool insertSearchParameterData(DataRow param)
        {
            if (insertSearchParameterData_Receive == null)
                insertSearchParameterData_Receive = new DataTable();

            insertSearchParameterData_Receive.Rows.Add(param);

            return _PostResult;
        }
        public bool deleteSearchParameterData(DataRow param)
        {
            if (deleteSearchParameterData_Receive == null)
                deleteSearchParameterData_Receive = new DataTable();

            deleteSearchParameterData_Receive.Rows.Add(param);

            return _DeleteResult;
        }
        public bool deleteSearchParameterData(string manageNo)
        {
            if (deleteSearchParameterData_Receive_All == null)
                deleteSearchParameterData_Receive_All = new List<string>();

            deleteSearchParameterData_Receive_All.Add(manageNo);

            return _DeleteResult;
        }
        #endregion

        #region 展開対象関連
        public DataTable getDeploymentTroubleData(string manageNo = null)
        {
            if (getDeploymentTroubleData_Receive == null)
                getDeploymentTroubleData_Receive = new List<string>();

            getDeploymentTroubleData_Receive.Add(manageNo);

            return _GetDeployTroubleTestData;
        }
        public bool existDeploymentTroubleData(DataRow param)
        {
            if (existDeploymentTroubleData_Receive == null)
                existDeploymentTroubleData_Receive = new DataTable();

            existDeploymentTroubleData_Receive.Rows.Add(param);

            return _ExistResult;
        }
        public bool postDeploymentTroubleData(DataTable param)
        {
            if (postDeploymentTroubleData_Receive == null)
                postDeploymentTroubleData_Receive = new Dictionary<string, DataTable>();

            postDeploymentTroubleData_Receive.Add(
                (postDeploymentTroubleData_Receive.Count + 1).ToString(), param);

            return _PostResult;
        }
        public bool insertDeploymentTroubleData(DataRow param)
        {
            if (insertDeploymentTroubleData_Receive == null)
                insertDeploymentTroubleData_Receive = new DataTable();

            insertDeploymentTroubleData_Receive.Rows.Add(param);

            return _InsertResult;
        }
        public bool deleteDeploymentTroubleData(DataRow param)
        {
            if (deleteDeploymentTroubleData_Receive == null)
                deleteDeploymentTroubleData_Receive = new DataTable();

            deleteDeploymentTroubleData_Receive.Rows.Add(param);

            return _DeleteResult;
        }
        public bool deleteDeploymentTroubleData(string manageNo)
        {
            if (deleteDeploymentTroubleData_Receive_All == null)
                deleteDeploymentTroubleData_Receive_All = new List<string>();

            deleteDeploymentTroubleData_Receive_All.Add(manageNo);

            return _DeleteResult; ;
        }
        #endregion

        #region タイトル品番情報関連
        public DataTable getTitleDrawingData(string manageNo = null)
        {
            if (getTitleDrawingData_Receive == null)
                getTitleDrawingData_Receive = new List<string>();

            getTitleDrawingData_Receive.Add(manageNo);

            return _GetDrawingTestData;
        }
        public bool existTitleDrawingData(DataRow param)
        {
            if (existTitleDrawingData_Receive == null)
                existTitleDrawingData_Receive = new DataTable();

            existTitleDrawingData_Receive.Rows.Add(param);

            return _ExistResult;
        }
        public bool postTitleDrawingData(DataTable param)
        {
            if (postTitleDrawingData_Receive == null)
                postTitleDrawingData_Receive = new Dictionary<string, DataTable>();

            postTitleDrawingData_Receive.Add(
                (postTitleDrawingData_Receive.Count + 1).ToString(), param);

            return _PostResult;
        }
        public bool insertTitleDrawingData(DataRow param)
        {
            if (insertTitleDrawingData_Receive == null)
                insertTitleDrawingData_Receive = new DataTable();

            insertTitleDrawingData_Receive.Rows.Add(param);

            return _InsertResult;
        }
        public bool deleteTitleDrawingData(DataRow param)
        {
            if (deleteTitleDrawingData_Receive == null)
                deleteTitleDrawingData_Receive = new DataTable();

            return _DeleteResult;
        }
        public bool deleteTitleDrawingData(string manageNo)
        {
            if (deleteTitleDrawingData_Receive_All == null)
                deleteTitleDrawingData_Receive_All = new List<string>();

            deleteTitleDrawingData_Receive_All.Add(manageNo);

            return _DeleteResult;
        }
        #endregion

        #region 機種情報関連
        public DataTable getModelData(string manageNo = null)
        {
            if (getModelData_Receive == null)
                getModelData_Receive = new List<string>();

            getModelData_Receive.Add(manageNo);

            return _GetModelTestData;
        }
        public bool existModelData(DataRow param)
        {
            if (existModelData_Receive == null)
                existModelData_Receive = new DataTable();

            existModelData_Receive.Rows.Add(param);

            return _ExistResult;
        }
        public bool postModelData(DataTable param)
        {
            if (postModelData_Receive == null)
                postModelData_Receive = new Dictionary<string, DataTable>();

            postModelData_Receive.Add(
                (postModelData_Receive.Count + 1).ToString(), param);

            return _PostResult;
        }
        public bool insertModelData(DataRow param)
        {
            if (insertModelData_Receive == null)
                insertModelData_Receive = new DataTable();

            insertModelData_Receive.Rows.Add(param);

            return _InsertResult;
        }
        public bool deleteModelData(DataRow param)
        {
            if (deleteModelData_Receive == null)
                deleteModelData_Receive = new DataTable();

            deleteModelData_Receive.Rows.Add(param);

            return _DeleteResult;
        }
        public bool deleteModelData(string manageNo)
        {
            if (deleteModelData_Receive_All == null)
                deleteModelData_Receive_All = new List<string>();

            deleteModelData_Receive_All.Add(manageNo);

            return _DeleteResult;
        }
        #endregion

        #region BLK情報関連
        public DataTable getBlockData(string manageNo = null)
        {
            if (getBlockData_Receive == null)
                getBlockData_Receive = new List<string>();

            getBlockData_Receive.Add(manageNo);

            return GetBlockTestData;
        }
        public bool existBlockData(DataRow param)
        {
            if (existBlockData_Receive == null)
                existBlockData_Receive = new DataTable();

            existBlockData_Receive.Rows.Add(param);

            return _ExistResult;
        }
        public bool postBlockData(DataTable param)
        {
            if (postBlockData_Receive == null)
                postBlockData_Receive = new Dictionary<string, DataTable>();

            postBlockData_Receive.Add(
                (postBlockData_Receive.Count + 1).ToString(), param);

            return _PostResult;
        }
        public bool insertBlockData(DataRow param)
        {
            if (insertBlockData_Receive == null)
                insertBlockData_Receive = new DataTable();

            insertBlockData_Receive.Rows.Add(param);

            return _InsertResult;
        }
        public bool deleteBlockData(DataRow param)
        {
            if (deleteBlockData_Receive == null)
                deleteBlockData_Receive = new DataTable();

            deleteBlockData_Receive.Rows.Add(param);

            return _DeleteResult;
        }
        public bool deleteBlockData(string manageNo)
        {
            if (deleteBlockData_Receive_All == null)
                deleteBlockData_Receive_All = new List<string>();

            deleteBlockData_Receive_All.Add(manageNo);

            return _DeleteResult;
        }
        #endregion

        #region 開発符号情報
        public DataTable getDevelopmentCodeData(string manageNo = null)
        {
            if (getDevelopmentCodeData_Receive == null)
                getDevelopmentCodeData_Receive = new List<string>();

            getDevelopmentCodeData_Receive.Add(manageNo);

            return _GetDevelopCodeTestData;
        }
        public bool existDevelopmentCodeData(DataRow param)
        {
            if (existDevelopmentCodeData_Receive == null)
                existDevelopmentCodeData_Receive = new DataTable();

            existDevelopmentCodeData_Receive.Rows.Add(param);

            return _ExistResult;
        }
        public bool postDevelopmentCodeData(DataTable param)
        {
            if (postDevelopmentCodeData_Receive == null)
                postDevelopmentCodeData_Receive = new Dictionary<string, DataTable>();

            postDevelopmentCodeData_Receive.Add(
                (postDevelopmentCodeData_Receive.Count+1).ToString(),param);

            return _PostResult;
        }
        public bool insertDevelopmentCodeData(DataRow param)
        {
            if (insertDevelopmentCodeData_Receive == null)
                insertDevelopmentCodeData_Receive = new DataTable();

            insertDevelopmentCodeData_Receive.Rows.Add(param);

            return _InsertResult;
        }
        public bool deleteDevelopmentCodeData(DataRow param)
        {
            if (deleteDevelopmentCodeData_Receive == null)
                deleteDevelopmentCodeData_Receive = new DataTable();

            deleteDevelopmentCodeData_Receive.Rows.Add(param);

            return _DeleteResult;
        }
        public bool deleteDevelopmentCodeData(string manageNo)
        {
            if (deleteDevelopmentCodeData_Receive_All == null)
                deleteDevelopmentCodeData_Receive_All = new List<string>();

            deleteDevelopmentCodeData_Receive_All.Add(manageNo);
            return _DeleteResult;
        }
        #endregion

        #region 設通情報関連
        public DataTable getEcsData(string manageNo = null)
        {
            if (getEcsData_Receive == null)
                getEcsData_Receive = new List<string>();

            getEcsData_Receive.Add(manageNo);

            return _GetEcsTestData;
        }
        public bool existEcsData(DataRow param)
        {
            if (existEcsData_Receive == null)
                existEcsData_Receive = new DataTable();

            existEcsData_Receive.Rows.Add(param);

            return _ExistResult;
        }
        public bool postEcsData(DataTable param)
        {
            if (postEcsData_Receive == null)
                postEcsData_Receive = new Dictionary<string, DataTable>();

            postEcsData_Receive.Add(
                (postEcsData_Receive.Count+1).ToString(),param);

            return _PostResult;
        }
        public bool insertEcsData(DataRow param)
        {
            if (insertEcsData_Receive == null)
                insertEcsData_Receive = new DataTable();

            insertEcsData_Receive.Rows.Add(param);

            return _InsertResult;
        }
        public bool deleteEcsData(DataRow param)
        {
            if (deleteEcsData_Receive == null)
                deleteEcsData_Receive = new DataTable();

            deleteEcsData_Receive.Rows.Add(param);

            return _DeleteResult;
        }
        public bool deleteEcsData(string manageNo)
        {
            if (deleteEcsData_Receive_All == null)
                deleteEcsData_Receive_All = new List<string>();

            deleteEcsData_Receive_All.Add(manageNo);

            return _DeleteResult;

        }
        #endregion

        #region 過去トラ観たか回答関連
        public DataTable getMitakaAnswerData(string manageNo = null)
        {
            if (getMitakaAnswerData_Receive == null)
                getMitakaAnswerData_Receive = new List<string>();

            getMitakaAnswerData_Receive.Add(manageNo);

            return _GetMitakaAnswerTestData;
        }
        public bool existMitakaAnswerData(DataRow param)
        {
            if (existMitakaAnswerData_Receive == null)
                existMitakaAnswerData_Receive = new DataTable();

            existMitakaAnswerData_Receive.Rows.Add(param);

            return _ExistResult;
        }
        public bool postMitakaAnswerData(DataTable param)
        {
            if (postMitakaAnswerData_Receive == null)
                postMitakaAnswerData_Receive = new Dictionary<string, DataTable>();

            postMitakaAnswerData_Receive.Add(
                (postMitakaAnswerData_Receive.Count+1).ToString(),param);

            return _PostResult;
        }
        public bool insertMitakaAnswerData(DataRow param)
        {
            if (insertMitakaAnswerData_Receive == null)
                insertMitakaAnswerData_Receive = new DataTable();

            insertMitakaAnswerData_Receive.Rows.Add(param);

            return _InsertResult;
        }
        public bool deleteMitakaAnswerData(DataRow param)
        {
            if (deleteMitakaAnswerData_Receive == null)
                deleteMitakaAnswerData_Receive = new DataTable();

            deleteMitakaAnswerData_Receive.Rows.Add(param);

            return _DeleteResult;
        }
        public bool deleteMitakaAnswerData(string manageNo)
        {
            if (deleteMitakaAnswerData_Receive_All == null)
                deleteMitakaAnswerData_Receive_All = new List<string>();

            deleteMitakaAnswerData_Receive_All.Add(manageNo);

            return _DeleteResult;
        }
        #endregion
    }
}