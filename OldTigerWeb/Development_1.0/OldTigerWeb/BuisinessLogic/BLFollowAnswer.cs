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
    }
}