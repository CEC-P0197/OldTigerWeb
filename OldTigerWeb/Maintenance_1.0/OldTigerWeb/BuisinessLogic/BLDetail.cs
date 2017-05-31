using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OldTigerWeb.BuisinessLogic
{
    public class BLDetail
    {
        /// <summary>
        /// TOP40マスタ存在チェック
        /// </summary>
        /// <param name="Type">種類</param>
        /// <returns>結果ステータス</returns>
        /// <remarks></remarks>
        public Boolean ChkTOP40(String FollowNo)
        {
            Boolean result = false;

            // データアクセス作成
            DataAccess.DADetail dac = new DataAccess.DADetail();

            // ＳＱＬ実行
            result = dac.SelectTop40(FollowNo);

            return result;
        }

        /// <summary>
        /// リプロ20マスタ存在チェック
        /// </summary>
        /// <param name="Type">種類</param>
        /// <returns>結果ステータス</returns>
        /// <remarks></remarks>
        public Boolean ChkRipro20(String FollowNo)
        {
            Boolean result = false;

            // データアクセス作成
            DataAccess.DADetail dac = new DataAccess.DADetail();

            // ＳＱＬ実行
            result = dac.SelectRipro20(FollowNo);

            return result;
        }
    }
}