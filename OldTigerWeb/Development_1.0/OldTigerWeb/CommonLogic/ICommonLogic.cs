using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Text;
using System.IO;
using System.Configuration;
using OldTigerWeb.DataAccess.Common;

// デバッグパラメータ用モデル
public interface ICommonLogic
{

    void ShowMessage(Type csType, ClientScriptManager csManager, ArrayList arrayMessage);
    void ErrorProcess(string errorForm, string errorPlace, Exception errorEx, HttpResponse response);
    void DebugProcess(DebugParameter dp);
    string GetExcelTemplate(String ExcelType);
    string GetMailAddress(String BYPU);
    string GetHelpForder(String SF);
    string GetLinkForder();
    int CheckFolder(String FilePath);
    int CheckFile(String FilePath);
    Boolean CheckUser();
    DataTable GetUser();
    String GetWindowsUser();
    String Getdate();
    DataTable GetTroubleData(String kanri_no);
    Boolean RegistLogData(String type, String word, int kanri_no, DateTime date_time);
    Boolean RegistHistoryLogData(String paraCondition, String paraType, String paraWord,
                                            String paraBusyo, String paraHyouka, String paraPartsS, String paraPartsN,
                                            String paraKaihatu, String paraGensyo, String paraGenin, String ParamSyakata,
                                            String ParamSgensyo, String ParamSyouin, String paraEgtm);
    DataTable GetSearchHistoryList(String user_id);


}
