using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OldTigerWeb
{
    public partial class frmErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true)
            {
                return;
            }

            try
            {
                // 画面初期表示
                lblFormId.Text = Page.Request.QueryString.Get("form_id");
                lblEvent.Text = Page.Request.QueryString.Get("place");
                lblEx.Text = Page.Request.QueryString.Get("ex");
            }
            catch (Exception)
            {
                return;

            }
        }
    }
}