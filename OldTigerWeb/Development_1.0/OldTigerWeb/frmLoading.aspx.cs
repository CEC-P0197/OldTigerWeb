using System;

namespace OldTigerWeb
{
    public partial class frmLoading : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // ポストバック時はリターン
            if (IsPostBack == true)
            {
                return;
            }
        }
    }
}