using OldTigerWeb.Const;

public class CommonPageLogic
{
    /// <summary>
    /// 画面オープン
    /// </summary>
    /// <param name="pageId"></param>
    /// <returns></returns>
    public string openWindow(string pageId)
    {
        string strScr = string.Empty;

        strScr += "<script type='text/javascript'>";

        if (pageId == Def.DefPageId_TroubleList)    // 過去トラ検索結果画面
        {
            openWindowLoading(ref strScr,false);
            openWindowTroubleList(ref strScr);
            focusWindowLoading(ref strScr);
        }
        else if (pageId == Def.DefPageId_FollowAnswer)    // FMC/MCフォロー情報画面
        {
            //OpenWindowLoading(ref strScr, false);
            openWindowFollowAnswer(ref strScr);
        }
        else if (pageId == Def.DefPageId_Loading)
        {
            openWindowLoading(ref strScr);
        }
        strScr += "</script>";

        return strScr;
    }
    /// <summary>
    /// 過去トラ検索結果画面オープン
    /// </summary>
    /// <param name="strScr"></param>
    /// <param name="init"></param>
    /// <returns></returns>
    private static string openWindowTroubleList(ref string　strScr, bool init = true)
    {
        strScr += "var urlTroubleList  = './frmTroubleList.aspx'; ";
        strScr += "var featuresTroubleList = 'menubar=no,toolbar=no,location=no,resizable=no,scrollbars=no,status=no,height=700,width=1340,";
        strScr += "left=(window.screen.width-1340)/2,top=(window.screen.height-700)/2';";
        if (init) { strScr += "var "; }
        strScr += "frmTroubleList = window.open(urlTroubleList, 'frmTroubleList', featuresTroubleList); ";
        return strScr;
    }
    /// <summary>
    /// FMC・mcフォロー情報一覧画面オープン
    /// </summary>
    /// <param name="strScr"></param>
    /// <param name="init"></param>
    /// <returns></returns>
    private static string openWindowFollowAnswer(ref string strScr, bool init = true)
    {
        strScr += "var urlFollowAnswer = './frmFollowAnswer.aspx'; ";
        strScr += "var featuresFollowAnswer = 'menubar=no,toolbar=no,location=no,resizable=no,scrollbars=no,status=no,height=720,width=1340,";
        strScr += "left=(window.screen.width-1340)/2,top=(window.screen.height-720)/2';";
        if (init) { strScr += "var "; }
        strScr += "frmFollowAnswer = window.open(urlFollowAnswer, 'frmFollowAnswer', featuresFollowAnswer); ";
        return strScr;
    }
    /// <summary>
    /// ロード画面オープン
    /// </summary>
    /// <param name="strScr"></param>
    /// <param name="init"></param>
    /// <returns></returns>
    private static string openWindowLoading(ref string strScr, bool init = true)
    {
        strScr += "var urlLoading = './frmLoading.aspx'; ";
        strScr += "var featuresLoading = 'menubar=no,toolbar=no,location=no,resizable=no,scrollbars=no,status=no,height=200,width=340,";
        strScr += "left=(window.screen.width-340)/2,top=(window.screen.height-200)/2';";
        if (init) { strScr += "var "; }
        strScr += "frmLoading = window.open(urlLoading, 'frmLoading', featuresLoading); ";
        return strScr;
    }
    /// <summary>
    /// ロード画面フォーカス
    /// </summary>
    /// <param name="strScr"></param>
    /// <returns></returns>
    private static string focusWindowLoading(ref string strScr)
    {
        strScr += "if(frmLoading != null){frmLoading.focus()}";

        return strScr;
    }
}
