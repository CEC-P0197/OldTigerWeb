using System.Collections.Generic;
using System.Data;
using OldTigerWeb.BuisinessLogic;

namespace OldTigerWeb.DataAccess
{
    /// <summary>
    /// 過去トラ観たかDB インターフェース
    /// </summary>
    public interface IDAMitakaData
    {
        #region プロパティ
        bool ProccessSuccess { get;}
        List<string> ErrorMessage { get;}
        #endregion

        #region 管理番号リスト取得関連
        string getMaxManageNo(string divisionCode);
        DataTable getManageNoFromMitakaSearchData(MitakaSearchData parent);
        DataTable getManageNoFromRelationUser();
        #endregion
        
        #region 過去トラ観たかヘッダー関連
        DataTable getMitakaHeaderData(string manageNo = null);
        bool existMitakaHeaderData(string manageNo);
        bool postMitakaHeaderData(DataTable param);
        bool insertMitakaHeaderData(DataRow param);
        bool updateMitakaHeaderData(DataRow param);
        bool updateHeaderDataToStatus(string manageNo,string status,string reason = null);
        bool deleteMitakaHeaderData(string manageNo);
        #endregion

        #region 関連ユーザー情報関連
        DataTable getReLationUserData(string manageNo = null);
        bool existReLationUserData(DataRow param);
        bool postReLationUserData(DataTable param);
        bool insertReLationUserData(DataRow param);
        bool deleteReLationUserData(DataRow param);
        bool deleteReLationUserData(string manageNo);
        #endregion

        #region 検索条件関連
        DataTable getSearchParameterData(string manageNo = null);
        bool existSearchParameterData(DataRow param);
        bool postSearchParameterData(DataTable param);
        bool insertSearchParameterData(DataRow param);
        bool deleteSearchParameterData(DataRow param);
        bool deleteSearchParameterData(string manageNo);
        #endregion

        #region 展開対象関連
        DataTable getDeploymentTroubleData(string manageNo = null);
        bool existDeploymentTroubleData(DataRow param);
        bool postDeploymentTroubleData(DataTable param);
        bool insertDeploymentTroubleData(DataRow param);
        bool deleteDeploymentTroubleData(DataRow param);
        bool deleteDeploymentTroubleData(string manageNo);
        #endregion

        #region タイトル品番情報関連
        DataTable getTitleDrawingData(string manageNo = null);
        bool existTitleDrawingData(DataRow param);
        bool postTitleDrawingData(DataTable param);
        bool insertTitleDrawingData(DataRow param);
        bool deleteTitleDrawingData(DataRow param);
        bool deleteTitleDrawingData(string manageNo);
        #endregion

        #region 機種情報関連
        DataTable getModelData(string manageNo = null);
        bool existModelData(DataRow param);
        bool postModelData(DataTable param);
        bool insertModelData(DataRow param);
        bool deleteModelData(DataRow param);
        bool deleteModelData(string manageNo);
        #endregion

        #region BLK情報関連
        DataTable getBlockData(string manageNo = null);
        bool existBlockData(DataRow param);
        bool postBlockData(DataTable param);
        bool insertBlockData(DataRow param);
        bool deleteBlockData(DataRow param);
        bool deleteBlockData(string manageNo);
        #endregion

        #region 開発符号情報
        DataTable getDevelopmentCodeData(string manageNo = null);
        bool existDevelopmentCodeData(DataRow param);
        bool postDevelopmentCodeData(DataTable param);
        bool insertDevelopmentCodeData(DataRow param);
        bool deleteDevelopmentCodeData(DataRow param);
        bool deleteDevelopmentCodeData(string manageNo);
        #endregion

        #region 設通情報関連
        DataTable getEcsData(string manageNo = null);
        bool existEcsData(DataRow param);
        bool postEcsData(DataTable param);
        bool insertEcsData(DataRow param);
        bool deleteEcsData(DataRow param);
        bool deleteEcsData(string manageNo);
        #endregion

        #region 過去トラ観たか回答関連
        DataTable getMitakaAnswerData(string manageNo = null);
        bool existMitakaAnswerData(DataRow param);
        bool postMitakaAnswerData(DataTable param);
        bool insertMitakaAnswerData(DataRow param);
        bool deleteMitakaAnswerData(DataRow param);
        bool deleteMitakaAnswerData(string manageNo);
        #endregion
    }
}