using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace MotoPart.MotoParts
{
    public partial class ApproveRequest : System.Web.UI.Page
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nichole\OneDrive\Desktop\MOTO PARTS PROJ\MotoPart\MotoPart\App_Data\MOTOPART.mdf;Integrated Security=True";
        int req_id;
        string user_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionHandler();
                Display_FormItems();
            }
        }

        protected void SessionHandler()
        {
            if (Session["REQ_ID"] != null)
            {

                string str_id = Session["REQ_ID"].ToString();
                req_id = Convert.ToInt32(str_id);

            }
            else
            {
                Response.Write("<script>alert('Session values are null')</script>");
            }
        }

        protected void Display_FormItems()
        {
            SessionHandler();
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM REQUISITION WHERE REQ_ID = '" + req_id + "' "
                        + "ORDER BY CASE REQ_STATUS WHEN 'PENDING' THEN 1 WHEN 'APPROVED' THEN 2 WHEN 'COMPLETED' THEN 3 WHEN 'CANCELLED' THEN 4 END, "
                        + "REQ_DATE DESC, REQ_TIME DESC";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null && reader.Read())
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal("AD_ID")))
                            {
                                user_id = reader["AD_ID"].ToString();
                                req_num.Text = reader["REQ_ID"].ToString();
                                txt_date.Text = reader["REQ_DATE"].ToString();

                                cmd.Parameters.Clear();
                                reader.Close();
                                cmd.CommandText = "SELECT AD_FNAME, AD_LNAME FROM ADMIN WHERE AD_ID = '" + user_id + "'";
                                using (SqlDataReader readname = cmd.ExecuteReader())
                                {
                                    if (readname.Read())
                                    {
                                        string fname = readname["AD_FNAME"].ToString();
                                        string lname = readname["AD_LNAME"].ToString();
                                        reqname_lbl.Text = fname + " " + lname;
                                        get_theItems();

                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('Name was not read.')</script>");
                                    }
                                }
                            }
                            else
                            {
                                reader.Close();
                                cmd.Parameters.Clear();
                                cmd.CommandText = "SELECT REQ_ID, REQ_DATE, STAFF_ID FROM REQUISITION WHERE REQ_ID = '" + req_id + "' "
                                    + "ORDER BY CASE REQ_STATUS WHEN 'PENDING' THEN 1 WHEN 'APPROVED' THEN 2 WHEN 'COMPLETED' THEN 3 WHEN 'CANCELLED' THEN 4 END, "
                                    + "REQ_DATE DESC, REQ_TIME DESC";
                                using (SqlDataReader reader1 = cmd.ExecuteReader())
                                {
                                    if (reader1 != null && reader1.Read())
                                    {
                                        if (!reader1.IsDBNull(reader1.GetOrdinal("STAFF_ID")))
                                        {
                                            user_id = reader1["STAFF_ID"].ToString();
                                            req_num.Text = reader1["REQ_ID"].ToString();
                                            txt_date.Text = reader1["REQ_DATE"].ToString();

                                            cmd.Parameters.Clear();
                                            reader1.Close();
                                            cmd.CommandText = "SELECT STAFF_FNAME, STAFF_LNAME FROM STAFF WHERE STAFF_ID = '" + user_id + "'";
                                            using (SqlDataReader readname = cmd.ExecuteReader())
                                            {
                                                if (readname.Read())
                                                {
                                                    string fname = readname["STAFF_FNAME"].ToString();
                                                    string lname = readname["STAFF_LNAME"].ToString();
                                                    reqname_lbl.Text = fname + " " + lname;
                                                    get_theItems();
                                                }
                                                else
                                                {
                                                    Response.Write("<script>alert('Name was not read.')</script>");
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('No data found for the specified REQ_ID')</script>");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('No data found for the specified REQ_ID')</script>");
                        }
                    }
                }
            }
        }


        protected void get_theItems()
        {
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT SUM(R_ITEM_TOTAL) FROM REQUEST_ITEM " +
                                                       "WHERE REQ_ID = @rID";
                    cmd.Parameters.AddWithValue("@rID", req_id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            object total = reader[0];

                            if (total != null && total != DBNull.Value)
                            {
                                string formattedTotal = Convert.ToDecimal(total).ToString("N2");

                                lbl_tot.Text = "Total: " + "  " + formattedTotal;
                                reader.Close();
                                cmd.Parameters.Clear();
                                cmd.CommandText = "SELECT INV_ID, R_ITEM_UNIT, R_ITEM_DESC, R_ITEM_QTTY, R_ITEM_UPRICE, R_ITEM_TOTAL " +
                                    "FROM REQUEST_ITEM WHERE REQ_ID = '" + req_id + "'";

                                using (SqlDataReader reader1 = cmd.ExecuteReader())
                                {
                                    if (reader1.HasRows)
                                    {
                                        DataTable dt1 = new DataTable();
                                        dt1.Load(reader1);
                                        selected_items.DataSource = dt1;
                                        selected_items.DataBind();
                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('No items retrieved.')</script>");
                                    }
                                }

                            }
                            else
                            {
                                Response.Write("<script>alert('Total not Read')</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('Reader not Read')</script>");
                        }
                    }

                }
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ad_dashboard.aspx");
        }

        protected void A_button_Click(object sender, EventArgs e)
        {
            SessionHandler();
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE REQUISITION SET REQ_STATUS = 'APPROVED' WHERE REQ_ID = @id AND REQ_STATUS = 'PENDING'";
                    cmd.Parameters.AddWithValue("@id", req_id);
                    var ctr = cmd.ExecuteNonQuery();

                    if (ctr >= 1)
                    {

                        string alertScript = "alert('Requisition Approved');";
                        string redirectScript = "window.location.href='ad_dashboard.aspx';";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertScript", alertScript + redirectScript, true);
                    }
                    else
                    {
                        Response.Write("<script>alert('Pending Approval')</script>");
                    }
                }
            }
        }

        protected void D_button_Click(object sender, EventArgs e)
        {
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {

                    foreach (GridViewRow row in selected_items.Rows)
                    {

                        string str_invId = row.Cells[0].Text;
                        if (!string.IsNullOrEmpty(str_invId) && int.TryParse(str_invId, out int invId))
                        {
                            using (var cmd1 = db.CreateCommand())
                            {
                                cmd1.CommandType = CommandType.Text;
                                cmd1.CommandText = "UPDATE INVENTORY SET INV_STATUS = 'REQUESTED' WHERE INV_ID = @invId";
                                cmd1.Parameters.AddWithValue("@invId", invId);
                                cmd1.ExecuteNonQuery();
                            }
                        }


                    }
                    SessionHandler();
                    using (var cmd2 = db.CreateCommand())
                    {
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "UPDATE REQUISITION SET REQ_STATUS = 'DENIED' WHERE REQ_ID = '" + req_id + "' AND REQ_STATUS = 'PENDING'";
                        var ctr2 = cmd2.ExecuteNonQuery();
                        if (ctr2 >= 1)
                        {
                            string alertScript = "alert('Requisition Denied');";
                            string redirectScript = "window.location.href='ad_dashboard.aspx';";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertScript", alertScript + redirectScript, true);

                        }
                        else
                        {
                            Response.Write("<script>alert('Not updated')</script>");
                        }

                    }

                }
            }
        }
    }
}
