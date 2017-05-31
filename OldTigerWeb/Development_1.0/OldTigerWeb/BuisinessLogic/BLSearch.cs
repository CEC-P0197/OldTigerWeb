using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Linq;
using System.Web;

namespace OldTigerWeb.BuisinessLogic
{
    public class BLSearch
    {
        /// <summary>
        /// キーワードリスト情報取得
        /// </summary>
        /// <param name="">無し</param>
        /// <returns>結果データテーブル</returns>
        /// <remarks></remarks>
        public DataTable GetKeyWordList()
        {
            DataTable result = null;

            // データアクセス作成
            DataAccess.DASearch dac = new DataAccess.DASearch();

            // ＳＱＬ実行
            result = dac.SelectKeyWordList();

            return result;
        }

        /// <summary>
        /// おすすめTOP10情報取得
        /// </summary>
        /// <param name="">無し</param>
        /// <returns>結果データテーブル</returns>
        public DataTable GetRecommendList()
        {
            DataTable result = null;

            // データアクセス作成
            DataAccess.DASearch dac = new DataAccess.DASearch();

            // ＳＱＬ実行
            result = dac.SelectRecommendList();

            return result;
        }

        /// <summary>
        /// 部品・部位情報取得
        /// </summary>
        /// <param name="BY">BY</param>
        /// <param name="PU">PU</param>
        /// <param name="dtSystem">システム選択リスト</param>
        /// <returns>処理結果情報</returns>
        public DataTable GetPartsList(String BY, String PU, ArrayList dtSystem)
        {
            DataTable result = null;

            // データアクセス作成
            DataAccess.DASearch dac = new DataAccess.DASearch();

            // ＳＱＬ実行
            result = dac.SelectPartsList(BY, PU, dtSystem);

            return result;
        }

        /// <summary>
        /// 各マスタ情報取得
        /// </summary>
        /// <param name="Type">種類</param>
        /// <returns>結果データテーブル</returns>
        /// <remarks></remarks>
        public DataTable GetMasterList(String Type, String BYPU)
        {
            DataTable result = null;

            // データアクセス作成
            DataAccess.DASearch dac = new DataAccess.DASearch();

            // ＳＱＬ実行
            result = dac.SelectMasterList(Type, BYPU);

            return result;
        }

        /// <summary>
        /// カテゴリ履歴情報
        /// </summary>
        /// <param name="Type">種類</param>
        /// <returns>結果データテーブル</returns>
        /// <remarks></remarks>
        public DataTable SetHistoryList(String Type, String BYPU)
        {
            DataTable result = null;

            // データアクセス作成
            DataAccess.DASearch dac = new DataAccess.DASearch();

            // ＳＱＬ実行
            result = dac.SelectMasterList(Type, BYPU);

            return result;
        }

    }
}