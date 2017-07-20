using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace OldTigerWeb.BuisinessLogic
{
    public class BLFollowAnswer
    {
        /// <summary>
        /// フォロー回答情報一覧取得
        /// </summary>
        /// <param name="FMC_mc">FMC/mc 1:FMC、2:mc</param>
        /// <param name="Type">種類 1:現在、2:過去</param>
        /// <returns>結果ステータス</returns>
        /// <remarks></remarks>
        public DataTable GetFollowDataList(String FMC_mc, String kaihatu_id, String by_pu, String event_no, String ka_code)
        {
            DataTable result = null;

            // データアクセス作成
            DataAccess.DAFollowAnswer dac = new DataAccess.DAFollowAnswer();

            // ＳＱＬ実行
            result = dac.SelectFollowDataList(FMC_mc, kaihatu_id, by_pu, event_no, ka_code);

            return result;
        }

        // 2017/07/14 Add Start
        /// <summary>
        /// フォロー対象部署一覧取得
        /// </summary>
        /// <param name="FMC_mc">FMC/mc区分</param>
        /// <param name="kaihatu_id">開発符号</param>
        /// <param name="by_pu">BYPU区分</param>
        /// <param name="event_no">イベントNO</param>
        /// <returns>結果ステータス</returns>
        /// <remarks></remarks>
        public DataTable GetKaCodeFollowDataList(String FMC_mc, String kaihatu_id, String by_pu, String event_no)
        {
            DataTable result = null;

            // データアクセス作成
            DataAccess.DAFollowAnswer dac = new DataAccess.DAFollowAnswer();

            // ＳＱＬ実行
            result = dac.SelectKaCodeFollowDataList(FMC_mc, kaihatu_id, by_pu, event_no);

            return result;
        }
        // 2017/07/14 Add End
    }
}