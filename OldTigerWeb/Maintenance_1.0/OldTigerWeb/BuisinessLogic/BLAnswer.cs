using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace OldTigerWeb.BuisinessLogic
{
    public class BLAnswer
    {
        /// <summary>
        /// フォロー情報取得
        /// </summary>
        /// <param name="FMC_mc">FMC/mc区分</param>
        /// <param name="kaihatu_id">開発符号</param>
        /// <param name="by_pu">BYPU区分</param>
        /// <param name="event_no">イベントNO</param>
        /// <param name="Type">フォロー管理No</param>
        /// <param name="ka_code">課・主査コード</param>
        /// <param name="Type">システム管理番号</param>
        /// <returns>結果データテーブル</returns>
        public DataTable GetFollowData(String FMC_mc, String kaihatu_id, String by_pu, String event_no,
            String follow_no, String ka_code, String system_no)
        {
            DataTable result = null;

            // データアクセス作成
            DataAccess.DAAnswer dac = new DataAccess.DAAnswer();

            // ＳＱＬ実行
            result = dac.SelectFollowData(FMC_mc, kaihatu_id, by_pu, event_no, follow_no, ka_code, system_no);

            return result;
        }

        /// <summary>
        /// フォロー情報更新
        /// </summary>
        /// <param name="FMC_mc">FMC/mc区分</param>
        /// <param name="kaihatu_id">開発符号</param>
        /// <param name="by_pu">BYPU区分</param>
        /// <param name="event_no">イベントNO</param>
        /// <param name="Type">フォロー管理No</param>
        /// <param name="ka_code">課・主査コード</param>
        /// <param name="system_no">システム管理番号</param>
        /// <param name="SINDO">進度</param>
        /// <param name="TAIOU_NAIYO">対応内容</param>
        /// <returns>結果ステータス</returns>
        public Boolean registFollowData(String FMC_mc, String kaihatu_id, String by_pu, String event_no,
            String follow_no, String ka_code, String system_no,  String SINDO, String TAIOU_NAIYO, String UserId)
        {
            Boolean result = false;

            // データアクセス作成
            DataAccess.DAAnswer dac = new DataAccess.DAAnswer();

            // ＳＱＬ実行
            // 2016.04.20 Kanda 適用有無設計の更新を廃止
            // result = dac.UpdateFollowData(FMC_mc, kaihatu_id, by_pu, event_no, follow_no, ka_code, system_no,
            //     TEKIYO, SINDO, TAIOU_NAIYO, UserId);
            result = dac.UpdateFollowData(FMC_mc, kaihatu_id, by_pu, event_no, follow_no, ka_code, system_no,
                 SINDO, TAIOU_NAIYO, UserId);

            return result;
        }
    }
}