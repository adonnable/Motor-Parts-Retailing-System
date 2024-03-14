using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;



namespace MotoPart.Admin_Staff_Page
{
    public partial class requisition : System.Web.UI.Page
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nichole\OneDrive\Desktop\MOTO PARTS PROJ\MotoPart\MotoPart\App_Data\MOTOPART.mdf;Integrated Security=True";
        int req_id;
        decimal tot_InAll = 0;
        int user_id;
        string userEmail;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionHandler();
                Request_Items();
                row_Count();
                display_form_items();
                if_ReqStat_Initial();

                btn_confirm_req.Enabled = false;
                btn_confirm_req.Visible = false;
            }

        }

        protected void SessionHandler()
        {
            if (Session["AD_EMAIL"] != null)
            {
                userEmail = Session["AD_EMAIL"].ToString();

            }
            else if (Session["STAFF_EMAIL"] != null)
            {
                userEmail = Session["STAFF_EMAIL"].ToString();

            }

        }

        void clear()
        {
            txt_unit.Text = "";
            txt_desc.Text = "";
            txt_qtty.Text = "";
            txt_price.Text = "";
            lbl_total.Text = "";
        }

        void row_Count()
        {
            info.Text = "No Requested Items";
            int rowCount_item = req_items.Rows.Count;

            if (rowCount_item > 0)
            { info.Enabled = false; info.Visible = false; }
            else { info.Enabled = true; info.Visible = true; }

        }

        protected void get_req_id()
        {
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT REQ_ID FROM REQUISITION WHERE REQ_STATUS = 'INITIAL'";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string temp = reader["REQ_ID"].ToString();
                            req_id = Convert.ToInt32(temp);
                            reader.Close();
                        }
                    }
                }
            }
        }

        protected void get_UserId()
        {
            SessionHandler();
            string str_user_id;
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT AD_ID FROM ADMIN WHERE AD_EMAIL = '" + userEmail + "'";
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        str_user_id = reader["AD_ID"].ToString();
                        user_id = Convert.ToInt32(str_user_id);
                        reader.Close();
                    }
                    else
                    {
                        reader.Close();
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SELECT STAFF_ID FROM STAFF WHERE STAFF_EMAIL = '" + userEmail + "'";
                        SqlDataReader reader1 = cmd.ExecuteReader();
                        if (reader1.Read())
                        {
                            str_user_id = reader1["STAFF_ID"].ToString();
                            user_id = Convert.ToInt32(str_user_id);
                            reader1.Close();
                        }
                    }

                }
            }
        }

        protected void if_ReqStat_Initial()
        {
            SessionHandler();
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT REQUISITION.AD_ID FROM REQUISITION INNER JOIN ADMIN ON REQUISITION.AD_ID = ADMIN.AD_ID WHERE ADMIN.AD_EMAIL = '" + userEmail + "'";
                    SqlDataReader readAd = cmd.ExecuteReader();
                    if (readAd.Read())
                    {
                        readAd.Close();
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SELECT * FROM REQUISITION R INNER JOIN ADMIN A ON R.AD_ID = A.AD_ID WHERE REQ_STATUS = 'INITIAL'";
                        SqlDataReader reader1 = cmd.ExecuteReader();
                        if (reader1.Read())
                        {
                            req_num.Text = reader1["REQ_ID"].ToString();
                            txt_date.Text = reader1["REQ_DATE"].ToString();
                            string fname = reader1["AD_FNAME"].ToString();
                            string lname = reader1["AD_LNAME"].ToString();
                            reqname_lbl.Text = fname + " " + lname;
                        }
                        else
                        {
                            Response.Write("<script>alert('Req Status Initial Not found!')</script>");
                        }
                        reader1.Close();
                    }
                    else
                    {
                        readAd.Close();
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SELECT * FROM REQUISITION R INNER JOIN STAFF S ON R.STAFF_ID = S.STAFF_ID WHERE REQ_STATUS = 'INITIAL'";
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            req_num.Text = reader["REQ_ID"].ToString();
                            txt_date.Text = reader["REQ_DATE"].ToString();
                            string fname = reader["STAFF_FNAME"].ToString();
                            string lname = reader["STAFF_LNAME"].ToString();
                            reqname_lbl.Text = fname + " " + lname;
                        }
                        else
                        {
                            Response.Write("<script>alert('Req Status Initial Not found!')</script>");
                        }
                        reader.Close();
                    }
                }
            }
        }

        protected void Request_Items()
        {
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT INVENTORY.INV_ID, INVENTORY.INV_INSTCK, INVENTORY.INV_ORDRLVL, ITEM.ITEM_NAME, ITEM.ITEM_ID "
                                        + "FROM INVENTORY "
                                        + "INNER JOIN ITEM ON INVENTORY.ITEM_ID = ITEM.ITEM_ID "
                                        + "WHERE INVENTORY.INV_INSTCK <= INVENTORY.INV_ORDRLVL AND INV_STATUS = 'REQUESTED' ORDER BY INVENTORY.INV_INSTCK DESC";
                    DataTable dt1 = new DataTable();
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd);
                    sda1.Fill(dt1);

                    if (dt1.Rows.Count > 0)
                    {
                        req_items.DataSource = dt1;
                        req_items.DataBind();
                    }
                }
            }
        }

        protected void req_items_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            get_req_id();
            btn_confirm_req.Enabled = true;
            btn_confirm_req.Visible = true;
            float price;
            if (e.CommandName == "AddItem")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Check if rowIndex is within the valid range
                if (rowIndex >= 0 && rowIndex < req_items.Rows.Count)
                {
                    string str_inv_id = req_items.Rows[rowIndex].Cells[0].Text;
                    int inv_id = int.Parse(str_inv_id);

                    string item_desc = req_items.Rows[rowIndex].Cells[1].Text;

                    using (var db = new SqlConnection(connString))
                    {
                        db.Open();

                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "UPDATE INVENTORY SET INV_STATUS = 'ADDED' WHERE INV_ID = '" + inv_id + "' ";

                            var ctr = cmd.ExecuteNonQuery();

                            if (ctr >= 1)
                            {
                                cmd.Parameters.Clear();
                                cmd.CommandText = "SELECT INV_UPRICE, INV_UNIT FROM INVENTORY WHERE INV_ID = '" + inv_id + "'";
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        string str_price = reader["INV_UPRICE"].ToString();
                                        string unit = reader["INV_UNIT"].ToString().ToUpper();
                                        price = float.Parse(str_price);
                                        reader.Close();

                                        cmd.Parameters.Clear();
                                        cmd.CommandText = "INSERT INTO REQUEST_ITEM (REQ_ID, INV_ID, R_ITEM_UNIT, R_ITEM_DESC, R_ITEM_UPRICE) "
                                                            + "VALUES (@req_id, @inv_id, @unit, @desc, @price)";
                                        cmd.Parameters.AddWithValue("@req_id", req_id);
                                        cmd.Parameters.AddWithValue("@inv_id", inv_id);
                                        cmd.Parameters.AddWithValue("@unit", unit);
                                        cmd.Parameters.AddWithValue("@desc", item_desc);
                                        cmd.Parameters.AddWithValue("@price", price);

                                        var ctr1 = cmd.ExecuteNonQuery();
                                        if (ctr1 >= 1)
                                        {
                                            display_form_items();
                                            selected_items.Visible = true;

                                        }
                                    }
                                }
                            }
                        }
                    }

                    req_items.Rows[rowIndex].Visible = false;
                }
                else
                {
                    Response.Write("<script>alert('Invalid index!')</script>");
                }
            }
            row_Count();
        }

        protected void btn_new_item_Click(object sender, EventArgs e)
        {
            get_req_id();
            btn_confirm_req.Enabled = true;
            btn_confirm_req.Visible = true;
            string unit = txt_unit.Text.ToUpper();
            string desc = txt_desc.Text.ToUpper();
            string str_qtty = txt_qtty.Text.ToUpper();
            string str_uprice = txt_price.Text.ToUpper();

            int qtty = Convert.ToInt32(str_qtty);
            float uprice = float.Parse(str_uprice);

            float total = qtty * uprice;

            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO REQUEST_ITEM (R_ITEM_UNIT, R_ITEM_DESC, R_ITEM_QTTY, R_ITEM_UPRICE, R_ITEM_TOTAL, REQ_ID) "
                                                + "VALUES (@unit, @desc, @qtty, @uprice, @total, @req_id)";
                    cmd.Parameters.AddWithValue("@unit", unit);
                    cmd.Parameters.AddWithValue("@desc", desc);
                    cmd.Parameters.AddWithValue("@qtty", qtty);
                    cmd.Parameters.AddWithValue("@uprice", uprice);
                    cmd.Parameters.AddWithValue("@total", total);
                    cmd.Parameters.AddWithValue("@req_id", req_id);

                    var ctr = cmd.ExecuteNonQuery();
                    if (ctr >= 1)
                    {
                        display_form_items();
                        selected_items.Visible = true;
                        clear();

                    }
                }
            }
        }

        protected void display_form_items()
        {
            get_req_id();
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM REQUEST_ITEM WHERE REQ_ID = '" + req_id + "'";

                    DataTable dt1 = new DataTable();
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd);
                    sda1.Fill(dt1);

                    selected_items.DataSource = null;
                    selected_items.DataBind();


                    if (dt1.Rows.Count > 0)
                    {
                        selected_items.DataSource = dt1;
                        selected_items.DataBind();
                    }
                }
            }
        }

        protected void selected_items_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (decimal.TryParse(DataBinder.Eval(e.Row.DataItem, "R_ITEM_TOTAL").ToString(), out decimal cellValue))
                {
                    tot_InAll += cellValue;
                }
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label label_total = (Label)e.Row.FindControl("label_total");

                if (label_total != null)
                {
                    label_total.Text = tot_InAll.ToString("0.00");
                }
            }
        }

        protected void selected_items_RowEditing(object sender, GridViewEditEventArgs e)
        {
            selected_items.EditIndex = e.NewEditIndex;
            display_form_items();
            btn_confirm_req.Visible = false;
            btn_confirm_req.Enabled = false;
        }

        protected void selected_items_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            selected_items.EditIndex = -1;
            display_form_items();
            btn_confirm_req.Visible = true;
            btn_confirm_req.Enabled = true;
        }

        protected void selected_items_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(selected_items.DataKeys[e.RowIndex].Value.ToString());
            string unit = ((TextBox)selected_items.Rows[e.RowIndex].FindControl("txt_unit")).Text.Trim();
            string desc = ((TextBox)selected_items.Rows[e.RowIndex].FindControl("txt_desc")).Text.Trim();
            string str_qtty = ((TextBox)selected_items.Rows[e.RowIndex].FindControl("txt_qtty"))?.Text?.Trim() ?? "";
            string str_price = ((TextBox)selected_items.Rows[e.RowIndex].FindControl("txt_price")).Text.Trim();

            int qtty;
            float price = float.Parse(str_price);

            if (!string.IsNullOrEmpty(str_qtty))
            {
                qtty = Convert.ToInt32(str_qtty);
                float total = qtty * price;
                if (qtty > 0)
                {
                    using (var db = new SqlConnection(connString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT R_ITEM_ID FROM REQUEST_ITEM WHERE R_ITEM_ID = '" + id + "'";
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = "UPDATE REQUEST_ITEM SET R_ITEM_UNIT = '" + unit + "' ,R_ITEM_DESC = '" + desc
                                      + "' ,R_ITEM_QTTY = '" + qtty + "' ,R_ITEM_UPRICE = '" + price + "' ,R_ITEM_TOTAL = '" + total + "'"
                                      + " WHERE R_ITEM_ID = '" + id + "'";
                                    reader.Close();
                                    var ctr = cmd.ExecuteNonQuery();
                                    if (ctr >= 1)
                                    {
                                        selected_items.EditIndex = -1;
                                        display_form_items();
                                        btn_confirm_req.Visible = true;
                                        btn_confirm_req.Enabled = true;
                                    }
                                }
                            }
                        }
                    }

                }
                else
                {
                    Response.Write("<script>alert('Quantity must be greater than 0!')</script>");
                }
            }
        }

        protected void selected_items_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(selected_items.DataKeys[e.RowIndex].Value.ToString());

            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT INV_ID FROM REQUEST_ITEM WHERE R_ITEM_ID = '" + id + "'";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string temp = reader["INV_ID"].ToString();
                            reader.Close();

                            if (!string.IsNullOrEmpty(temp))
                            {
                                int invid = Convert.ToInt32(temp);

                                cmd.Parameters.Clear();
                                cmd.CommandText = "UPDATE INVENTORY SET INV_STATUS = 'REQUESTED' WHERE INV_ID = '" + invid + "'";
                                var ctr = cmd.ExecuteNonQuery();

                                if (ctr >= 1)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = "DELETE FROM REQUEST_ITEM WHERE R_ITEM_ID= '" + id + "'";
                                    int ctr1 = cmd.ExecuteNonQuery();
                                    if (ctr1 >= 1)
                                    {
                                        selected_items.EditIndex = -1;
                                        display_form_items();
                                        Request_Items();
                                        if (selected_items.Rows.Count < 1)
                                        {
                                            btn_confirm_req.Enabled = false;
                                            btn_confirm_req.Visible = false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                cmd.Parameters.Clear();
                                cmd.CommandText = "DELETE FROM REQUEST_ITEM WHERE R_ITEM_ID= '" + id + "'";
                                int ctr1 = cmd.ExecuteNonQuery();
                                if (ctr1 >= 1)
                                {
                                    selected_items.EditIndex = -1;
                                    display_form_items();
                                    if (selected_items.Rows.Count < 1)
                                    {
                                        btn_confirm_req.Enabled = false;
                                        btn_confirm_req.Visible = false;
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }

        protected void btn_cancel_req_Click(object sender, EventArgs e)
        {
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    foreach (GridViewRow row in selected_items.Rows)
                    {
                        Label labelId = row.FindControl("label_id") as Label;

                        if (labelId != null)
                        {
                            cmd.CommandText = "UPDATE INVENTORY SET INV_STATUS = 'REQUESTED' WHERE INV_STATUS = 'ADDED'";
                            cmd.ExecuteNonQuery();

                        }
                    }
                    get_req_id();
                    Response.Write("<script>alert('Req_id = " + req_id + "')</script>");
                    cmd.Parameters.Clear();
                    cmd.CommandText = "DELETE FROM REQUEST_ITEM WHERE REQ_ID = @req_id";
                    cmd.Parameters.AddWithValue("req_id", req_id);

                    cmd.ExecuteNonQuery();


                    cmd.Parameters.Clear();
                    cmd.CommandText = "DELETE FROM REQUISITION WHERE REQ_STATUS = 'INITIAL'";
                    var ctr2 = cmd.ExecuteNonQuery();
                    if (ctr2 >= 1)
                    {
                        Request_Items();
                        Response.Redirect("RequisitionPage.aspx");
                    }
                    else
                    {
                        Response.Write("<scrDeletion Failed')</script>");
                    }
                }
            }
        }

        protected void btn_confirm_req_Click(object sender, EventArgs e)
        {
            bool isNotEmpty = true;
            get_req_id();
            SessionHandler();

            foreach (GridViewRow row in selected_items.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label label_desc = (Label)row.FindControl("label_desc");
                    Label label_qtty = (Label)row.FindControl("label_qtty");
                    Label label_price = (Label)row.FindControl("label_price");

                    if ((label_desc != null && !string.IsNullOrWhiteSpace(label_desc.Text.Trim()) && label_desc.Text.Trim() != "&nbsp;")
                        && (label_qtty != null && !string.IsNullOrWhiteSpace(label_qtty.Text.Trim()) && label_qtty.Text.Trim() != "&nbsp;")
                        && (label_price != null && !string.IsNullOrWhiteSpace(label_price.Text.Trim()) && label_price.Text.Trim() != "&nbsp;"))
                    {
                        isNotEmpty = true;
                    }
                    else
                    {
                        // At least one label is empty, so it's considered as empty
                        isNotEmpty = false;
                        break;
                    }
                }
            }

            if (isNotEmpty)
            {
                List<int> invIds = new List<int>();

                using (var db = new SqlConnection(connString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandText = "SELECT INV_ID FROM REQUEST_ITEM WHERE REQ_ID = '" + req_id + "'";

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string str_invId = reader["INV_ID"].ToString();

                                    if (!string.IsNullOrEmpty(str_invId))
                                    {
                                        invIds.Add(Convert.ToInt32(str_invId));
                                    }

                                }
                            }
                        }

                        foreach (int invId in invIds)
                        {
                            using (var cmd1 = db.CreateCommand())
                            {
                                cmd1.CommandType = CommandType.Text;
                                cmd1.CommandText = "UPDATE INVENTORY SET INV_STATUS = 'REORDERED' WHERE INV_STATUS = 'ADDED' AND INV_ID = '" + invId + "'";
                                var ctr1 = cmd1.ExecuteNonQuery();
                            }
                        }

                        cmd.Parameters.Clear();
                        get_UserId();
                        cmd.CommandText = "SELECT AD_ID FROM ADMIN WHERE AD_ID = '" + user_id + "' AND AD_EMAIL = '" + userEmail + "'";
                        SqlDataReader ad_id = cmd.ExecuteReader();
                        if (ad_id.Read())
                        {
                            ad_id.Close();
                            get_req_id();

                            DateTime currentDate = DateTime.Now.Date;
                            string date = currentDate.ToString("yyyy-MM-dd");
                            string time = DateTime.Now.ToString("hh:mm:ss tt");

                            using (var db1 = new SqlConnection(connString))
                            {
                                db1.Open();
                                using (var cmd1 = db.CreateCommand())
                                {
                                    cmd1.CommandType = CommandType.Text;
                                    cmd1.CommandText = "UPDATE REQUISITION SET REQ_STATUS = 'APPROVED', REQ_DATE = @Date, REQ_TIME = @Time WHERE REQ_ID = @ReqID AND AD_ID = @UserID";
                                    cmd1.Parameters.AddWithValue("@Date", date);
                                    cmd1.Parameters.AddWithValue("@Time", time);
                                    cmd1.Parameters.AddWithValue("@ReqID", req_id);
                                    cmd1.Parameters.AddWithValue("@UserID", user_id);

                                    var ctr = cmd1.ExecuteNonQuery();
                                    if (ctr >= 1)
                                    {

                                        string alertScript = "alert('Your requisition has been successfully submitted.');";
                                        string redirectScript = "window.location.href='RequisitionPage.aspx';";
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertScript", alertScript + redirectScript, true);
                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('Update Unsuccessfull')</script>");
                                    }

                                }
                            }

                        }
                        else
                        {

                            ad_id.Close();
                            cmd.Parameters.Clear();
                            DateTime currentDate = DateTime.Now.Date;
                            string date = currentDate.ToString("yyyy-MM-dd");
                            string time = DateTime.Now.ToString("hh:mm:ss tt");

                            cmd.CommandText = "UPDATE REQUISITION SET REQ_STATUS = 'PENDING', REQ_DATE = '" + date + "', REQ_TIME = '" + time + "' WHERE REQ_STATUS = 'INITIAL' AND REQ_ID = '" + req_id + "'";
                            var ctr = cmd.ExecuteNonQuery();
                            if (ctr >= 1)
                            {
                                string alertScript = "alert('Your requisition has been successfully submitted. Kindly await admin\\'s approval');";
                                string redirectScript = "window.location.href='staff_RequisitionPage.aspx';";
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertScript", alertScript + redirectScript, true);

                                btn_confirm_req.Enabled = false; btn_confirm_req.Visible = false;

                            }
                            else
                            {
                                Response.Write("<script>alert('Requisition Update Failed')</script>");
                            }
                        }
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('Please fill in all cells')</script>");
            }
        }
    }
}