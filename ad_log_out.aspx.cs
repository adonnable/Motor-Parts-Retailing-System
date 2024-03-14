using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotoPart.MotoParts
{
    public partial class ad_log_out : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_yes_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("ad_login_form.aspx");
        }

        protected void btn_no_Click(object sender, EventArgs e)
        {
            Response.Redirect("ad_dashboard.aspx");
        }
    }
}