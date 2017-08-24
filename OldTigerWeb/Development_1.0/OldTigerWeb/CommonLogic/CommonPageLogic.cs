using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

public class CommonPageLogic: System.Web.UI.Page
{

    #region メッセージ表示関連
    /// <summary>
    /// 動的スクリプト作成（アラートメッセージ表示）
    /// </summary>
    /// <param name="message">表示メッセージ</param>
    /// <returns>動的スクリプト</returns>
    public string getScriptForAlertMessage(string message)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<script type='text/javascript'>");
        sb.AppendLine("window.alert('@message')");
        sb.AppendLine("</script>");

        sb = sb.Replace("@message", message);
        return sb.ToString();
    }
    #endregion

    #region 画面オープン
    /// <summary>
    /// 動的スクリプト作成（画面オープン）   
    /// </summary>
    /// <param name="pageId">画面ID</param>
    /// <returns>動的スクリプト（画面オープン）</returns>
    public string getScriptForOpenWindow(string pageId)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("<script type='text/javascript'>");

        if (pageId == Def.DefPageId_TroubleList)    // 過去トラ検索結果画面
        {
            scriptOpenWindowLoading(ref sb,false);
            scriptOpenWindowTroubleList(ref sb);
            focusWindowLoading(ref sb);
        }
        else if (pageId == Def.DefPageId_FollowAnswer)    // FMC/MCフォロー情報画面
        {
            //OpenWindowLoading(ref strScr, false);
            scriptOpenWindowFollowAnswer(ref sb);
        }
        else if (pageId == Def.DefPageId_Loading)
        {
            scriptOpenWindowLoading(ref sb);
        }
        sb.AppendLine("</script>");

        return sb.ToString();
    }

    /// <summary>
    /// 過去トラ検索結果画面オープン
    /// </summary>
    /// <param name="sb">格納用変数</param>
    /// <param name="init">var追記フラグ</param>
    /// <returns>動的スクリプト（画面オープン）</returns>
    private static StringBuilder scriptOpenWindowTroubleList(ref StringBuilder　sb, bool init = true)
    {
        sb.AppendLine("var urlTroubleList  = './frmTroubleList.aspx'; ");
        sb.AppendLine("var featuresTroubleList = 'menubar=no,toolbar=no,location=no,resizable=no,scrollbars=no,status=no,height=700,width=1340,'+");
        sb.AppendLine("'left=(window.screen.width-1340)/2,top=(window.screen.height-700)/2';");
        sb.AppendLine("@varfrmTroubleList = window.open(urlTroubleList, 'frmTroubleList', featuresTroubleList); ");
        if (init){ sb = sb.Replace("@var", "var "); }
        else{ sb = sb.Replace("@var", ""); };
        return sb;
    }
    /// <summary>
    /// FMC・mcフォロー情報一覧画面オープン
    /// </summary>
    /// <param name="sb">格納用変数</param>
    /// <param name="init">var追記フラグ</param>
    /// <returns>動的スクリプト（画面オープン）</returns>
    private static StringBuilder scriptOpenWindowFollowAnswer(ref StringBuilder sb, bool init = true)
    {

        sb.AppendLine("var urlFollowAnswer = './frmFollowAnswer.aspx'; ");
        sb.AppendLine("var featuresFollowAnswer = 'menubar=no,toolbar=no,location=no,resizable=no,scrollbars=no,status=no,height=720,width=1340,'+");
        sb.AppendLine("'left=(window.screen.width-1340)/2,top=(window.screen.height-720)/2';");
        sb.AppendLine("@varfrmFollowAnswer = window.open(urlFollowAnswer, 'frmFollowAnswer', featuresFollowAnswer); ");
        if (init) { sb = sb.Replace("@var", "var "); }
        else { sb = sb.Replace("@var", ""); };

        return sb;
    }
    /// <summary>
    /// ロード画面オープン
    /// </summary>
    /// <param name="sb">格納用変数</param>
    /// <param name="init">var追記フラグ</param>
    /// <returns>動的スクリプト（画面オープン）</returns>
    private static StringBuilder scriptOpenWindowLoading(ref StringBuilder sb, bool init = true)
    {
        sb.AppendLine("var urlLoading = './frmLoading.aspx'; ");
        sb.AppendLine("var featuresLoading = 'menubar=no,toolbar=no,location=no,resizable=no,scrollbars=no,status=no,height=200,width=340,'+");
        sb.AppendLine("'left=(window.screen.width-340)/2,top=(window.screen.height-200)/2';");
        sb.AppendLine("@varfrmLoading = window.open(urlLoading, 'frmLoading', featuresLoading); ");
        if (init) { sb = sb.Replace("@var", "var "); }
        else { sb = sb.Replace("@var", ""); };

        return sb;
    }
    /// <summary>
    /// ロード画面フォーカス
    /// </summary>
    /// <param name="strScr"></param>
    /// <returns></returns>
    /// <summary>
    /// ローディング画面フォーカス
    /// </summary>
    /// <param name="sb">格納用変数</param>
    /// <returns>動的スクリプト（画面オープン）</returns>
    private static StringBuilder focusWindowLoading(ref StringBuilder sb)
    {
        sb.AppendLine("if(frmLoading != null){frmLoading.focus()}");

        return sb;
    }
    #endregion

    #region 画面クローズ
    /// <summary>
    /// 動的スクリプト作成（画面クローズ）
    /// </summary>
    /// <returns>動的スクリプト（画面クローズ）</returns>
    public string getScriptForCloseWindow()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<script type='text/javascript'>");
        sb.AppendLine("window.close()");
        sb.AppendLine("</script>");

        return sb.ToString();
    }
    #endregion

    #region メール作成
    /// <summary>
    /// 動的スクリプト取得   （メール作成）
    /// </summary>
    /// <param name="toList">宛先アドレスリスト</param>
    /// <param name="ccList">CCアドレスリスト</param>
    /// <param name="subject">件名</param>
    /// <param name="body">本文</param>
    /// <returns></returns>
    public string getScriptForCreatingMail(List<string> toList,List<string> ccList,string subject,string body)
    {
        // メール作成スクリプト作成
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("mailto:@sendMember");
        sb.AppendLine("?cc=@ccMember");
        sb.AppendLine("&subject=@subject");
        sb.AppendLine("&body=@body");

        // 宛先リスト展開
        string toAdress = "";
        for(int i = 0; i < toList.Count;i++)
        {
            toAdress = toAdress + toList[i] + ";";
        }

        // CCリスト展開
        string ccAdress = "";
        for (int i = 0; i < ccList.Count; i++)
        {
            ccAdress = ccAdress + ccList[i] + ";";
        }

        sb = sb.Replace("@sendMember", toAdress);
        sb = sb.Replace("@ccMember",ccAdress);
        sb = sb.Replace("@subject", subject);
        sb = sb.Replace("@body", body);

        return sb.ToString();
    }

    /// <summary>
    /// 過去トラ検索条件取得（セッション）
    /// </summary>
    /// <param name="keyword">検索キーワード</param>
    /// <param name="keywordCondition">キーワード検索区分</param>
    /// <param name="cotegory">検索カテゴリ</param>
    /// <param name="cotegoryCondition">カテゴリ検索区分</param>
    public void getTrobuleSearchParameterFromSession(out string keyword,out string keywordCondition,out DataTable cotegory,out string cotegoryCondition)
    {
        keyword = null;
        keywordCondition = null;
        cotegory = null;
        cotegoryCondition = null;

        // 検索キーワード
        if ((String)Session[Def.DefPARA_WORD] != null)
        {
            keyword = (String)Session[Def.DefPARA_WORD];
        }
        // キーワード検索区分(AND/OR)
        if ((String)Session[Def.DefPARA_CONDITION_FLG] != null)
        {
            keywordCondition = (String)Session[Def.DefPARA_CONDITION_FLG];
        }

        // 検索カテゴリ
        if ((DataTable)Session[Def.DefPARA_TABLE] != null)
        {
            cotegory = (DataTable)Session[Def.DefPARA_TABLE];
        }
        // カテゴリ検索区分(AND/OR)
        if ((String)Session[Def.DefPARA_CATEGORY_CONDITION_FLG] != null)
        {
            cotegoryCondition = (String)Session[Def.DefPARA_CATEGORY_CONDITION_FLG];
        }

    }
    #endregion
}
