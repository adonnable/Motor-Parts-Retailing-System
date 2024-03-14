using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotoPart.MotoParts
{
    public partial class receivingMemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve delv_date from session
                string delvDate = Session["DelvDate"] as string;
                string po = Session["PurchID"].ToString();
                string suppName = Session["SuppName"].ToString();

                lblDate.Text = delvDate;
                lblPO.Text = po;
                lblSupp.Text = suppName;

            }
        }
    }
}