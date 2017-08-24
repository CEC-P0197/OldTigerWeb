using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace OldTigerWeb.BuisinessLogic
{
    public class BLAnswerSubWindow
    {
        // 2017/07/14 Add Start
        #region 展開区分取得
        /// <summary>
        /// 展開区分取得
        /// </summary>
        /// <param name="FMC_mc">FMC/mc区分</param>
        /// <param name="kaihatu_id">開発符号</param>
        /// <param name="by_pu">BYPU区分</param>
        /// <param name="event_no">イベントNO</param>
        /// <returns>展開区分</returns>
        public DataTable getTenkaiKbn(String FMC_mc, String kaihatu_id, String by_pu, String event_no)
        {
            DataTable result = null;

            // データアクセス作成
            DataAccess.DAAnswerSubWindow dac = new DataAccess.DAAnswerSubWindow();

            // ＳＱＬ実行
            result = dac.selectTenkaiKbn(FMC_mc, kaihatu_id, by_pu, event_no);

            return result;
        }
        #endregion
        // 2017/07/14 Add End

        #region フォロー情報取得
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
        /// <param name="tenkai_kbn">展開区分</param> // 2017/07/14 Add 引数に展開区分を追加
        /// <returns>結果データテーブル</returns>
        public DataTable GetFollowDataOtherDept(String FMC_mc, String kaihatu_id, String by_pu, String event_no,
            String follow_no, String ka_code, String system_no, String tenkai_kbn)
        {
            DataTable result = null;

            // データアクセス作成
            DataAccess.DAAnswerSubWindow dac = new DataAccess.DAAnswerSubWindow();

            // ＳＱＬ実行
            result = dac.SelectFollowDataOtherDept(FMC_mc, kaihatu_id, by_pu, event_no, follow_no, ka_code, system_no, 
                tenkai_kbn); // 2017/07/14 Add 引数に展開区分を追加

            return result;
        }
        #endregion

    }
}