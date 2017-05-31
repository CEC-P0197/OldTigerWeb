using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace OldTigerWeb.BuisinessLogic
{
    public class BLAnswerSubWindow
    {
        /// <summary>
        /// フォロー情報取得
        /// </summary>
        /// <param name="FMC_mc">FMC/mc区分</param>
        /// <param name="kaihatu_id">開発符号</param>
        /// <param name="by_pu">BYPU区分</param>
        /// <param name="event_no">イベントNO</param>
        /// <param name="follow_no">フォロー管理No</param>
        /// <param name="ka_code">課・主査コード</param>
        /// <param name="system_no">システム管理番号</param>
        /// <returns>結果データテーブル</returns>
        public DataTable GetFollowDataOtherDept(String FMC_mc, String kaihatu_id, String by_pu, String event_no,
            String follow_no, String ka_code, String system_no)
        {
            DataTable result = null;

            // データアクセス作成
            DataAccess.DAAnswerSubWindow dac = new DataAccess.DAAnswerSubWindow();

            // ＳＱＬ実行
            result = dac.SelectFollowDataOtherDept(FMC_mc, kaihatu_id, by_pu, event_no, follow_no, ka_code, system_no);

            return result;
        }

    }
}