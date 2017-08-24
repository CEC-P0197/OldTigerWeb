using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using OldTigerWeb.DataAccess;

namespace OldTigerWeb.BuisinessLogic
{
    public interface IDATroubleData 
    {
        DataTable getTroubleList(String keyword, String keywordCondition, DataTable cotegory, String cotegoryCondition, String mode = Def.DefMODE_DISP);

        StringBuilder createParameterForKeyword(string keyword, string condition = Def.DefTYPE_AND);

        StringBuilder createParameterForCotegory(DataTable cotegoryTable, string condition = Def.DefTYPE_AND);
        DataTable selectCotegoryName(string strType, string strCode);
    }
}                                                                         