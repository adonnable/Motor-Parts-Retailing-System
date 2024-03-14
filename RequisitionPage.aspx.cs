using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MotoPart.MotoParts
{
    public partial class RequisitionPage : System.Web.UI.Page
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nichole\OneDrive\Desktop\MOTO PARTS PROJ\MotoPart\MotoPart\App_Data\MOTOPART.mdf;Integrated Security=True";
        string userEmail = "";
        int user_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionHandler();
                Display_Items();
                Display_ReqStat();
            }
        }

        protected void SessionHandler()
        {
            if (Session["AD_EMAIL"] != null)
            {
                userEmail = Session["AD_EMAIL"].ToString();

                using (var db = new SqlConnection(connString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT AD_ID, AD_FNAME, AD_LNAME, AD_PROFILE_PIC FROM ADMIN WHERE AD_EMAIL = @userEmail";
                        cmd.Parameters.AddWithValue("@userEmail", userEmail);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string str_id = reader["AD_ID"].ToString();
                                user_id = Convert.ToInt32(str_id);
                                string fname = reader["AD_FNAME"].ToString();
                                string lname = reader["AD_LNAME"].ToString();
                                user_name.Text = fname + " " + lname;

                                if (!reader.IsDBNull(reader.GetOrdinal("AD_PROFILE_PIC")))
                                {
                                    byte[] pic = (byte[])reader["AD_PROFILE_PIC"];
                                    string str = Convert.ToBase64String(pic);
                                    user_profile.ImageUrl = "data:image/jpeg;base64," + str;
                                    dsply_prof_pic.ImageUrl = "data:image/jpeg;base64," + str;
                                }
                            }
                        }
                    }
                }
            }
            else if (Session["STAFF_EMAIL"] != null)
            {
                userEmail = Session["STAFF_EMAIL"].ToString();

                using (var db = new SqlConnection(connString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT STAFF_ID, STAFF_FNAME, STAFF_LNAME, STAFF_PROFILE_PIC FROM STAFF WHERE STAFF_EMAIL = @userEmail";
                        cmd.Parameters.AddWithValue("@userEmail", userEmail);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string str_id = reader["STAFF_ID"].ToString();
                                user_id = Convert.ToInt32(str_id);
                                string fname = reader["STAFF_FNAME"].ToString();
                                string lname = reader["STAFF_LNAME"].ToString();
                                user_name.Text = fname + " " + lname;

                                if (!reader.IsDBNull(reader.GetOrdinal("STAFF_PROFILE_PIC")))
                                {
                                    byte[] pic = (byte[])reader["STAFF_PROFILE_PIC"];
                                    string str = Convert.ToBase64String(pic);
                                    user_profile.ImageUrl = "data:image/jpeg;base64," + str;
                                    dsply_prof_pic.ImageUrl = "data:image/jpeg;base64," + str;
                                }
                            }
                        }
                    }
                }
            }

        }

        void row_Count()
        {
            info.Text = "No Requested Items";
            info2.Text = "No Requisitions Initiated";
            int rowCount_item = dsply_item.Rows.Count;

            if (rowCount_item > 0)
            { info.Enabled = false; info.Visible = false; }
            else { info.Enabled = true; info.Visible = true; }

            int rowCount_req = req_stat.Rows.Count;

            if (rowCount_req > 0)
            { info2.Enabled = false; info2.Visible = false; }
            else { info2.Enabled = true; info2.Visible = true; }

        }

        protected void btn_createReq_Click(object sender, EventArgs e)
        {
            SessionHandler();

            string status = "INITIAL";
            DateTime currentDate = DateTime.Now.Date;
            string date = currentDate.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("hh:mm:ss tt");

            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT AD_ID FROM ADMIN WHERE AD_ID = '" + user_id + "' AND AD_EMAIL = '" + userEmail + "'";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            reader.Close();
                            cmd.CommandText = "INSERT INTO REQUISITION (REQ_STATUS, REQ_DATE, REQ_TIME, AD_ID) VALUES ('" + status + "', '" + date + "', '" + time + "', '" + user_id + "')";

                            var ctr = cmd.ExecuteNonQuery();

                            if (ctr >= 1)
                            {

                                Session["AD_EMAIL"] = userEmail;
                                Response.Redirect("requisition.aspx");
                            }
                            else
                            {
                                Response.Write("<script>alert('Insertion unsuccessful')</script>");

                            }
                        }
                        else
                        {

                            reader.Close();
                            cmd.Parameters.Clear();
                            cmd.CommandText = "SELECT STAFF_ID FROM STAFF WHERE STAFF_ID = '" + user_id + "' AND STAFF_EMAIL = '" + userEmail + "'";
                            SqlDataReader reader1 = cmd.ExecuteReader();
                            if (reader1.Read())
                            {
                                reader1.Close();
                                cmd.CommandText = "INSERT INTO REQUISITION (REQ_STATUS, REQ_DATE, REQ_TIME, STAFF_ID) VALUES ('" + status + "', '" + date + "', '" + time + "', '" + user_id + "')";

                                var ctr = cmd.ExecuteNonQuery();

                                if (ctr >= 1)
                                {
                                    Session["STAFF_EMAIL"] = userEmail;
                                    Response.Redirect("requisition.aspx");
                                }
                                else
                                {
                                    Response.Write("<script>alert('Insertion unsuccessful')</script>");

                                }
                            }
                        }
                    }
                }
            }
        }

        private void Display_Items()
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
                        dsply_item.DataSource = dt1;
                        dsply_item.DataBind();

                    }
                }
            }
            row_Count();
        }

        private void Display_ReqStat()
        {
            DataTable dt = new DataTable();
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT REQ_ID, REQ_STATUS, REQ_DATE, REQ_TIME FROM REQUISITION WHERE REQ_STATUS IN ('PENDING', 'APPROVED', 'COMPLETED', 'DENIED') ORDER BY CASE REQ_STATUS WHEN 'PENDING' THEN 1 WHEN 'APPROVED' THEN 2 WHEN 'COMPLETED' THEN 3 WHEN 'DENIED' THEN 4 END, REQ_DATE DESC, REQ_TIME DESC";

                    DataTable dt1 = new DataTable();
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd);
                    sda1.Fill(dt1);

                    if (dt1.Rows.Count > 0)
                    {
                        req_stat.DataSource = dt1;
                        req_stat.DataBind();
                    }
                }
            }
            row_Count();
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {

        }

        protected void req_stat_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = DataBinder.Eval(e.Row.DataItem, "REQ_STATUS").ToString();

                // Set font color based on status
                if (status == "PENDING")
                {
                    e.Row.ForeColor = System.Drawing.Color.FromArgb(255, 140, 0);
                }
                else if (status == "APPROVED")
                {
                    e.Row.ForeColor = System.Drawing.Color.Blue;
                }
                else if (status == "DENIED")
                {
                    e.Row.ForeColor = System.Drawing.Color.Red;
                }
                else if (status == "COMPLETED")
                {
                    e.Row.ForeColor = System.Drawing.Color.Green;
                }
            }
        }

        protected void req_stat_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SessionHandler();

            if (e.CommandName == "ViewItem")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                if (rowIndex >= 0 && rowIndex < req_stat.Rows.Count)
                {
                    string str_req_id = req_stat.Rows[rowIndex].Cells[0].Text;
                    int R_id = int.Parse(str_req_id);

                    using (var db = new SqlConnection(connString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT REQ_ID FROM REQUISITION WHERE REQ_ID = '" + R_id + "'";

                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                Session["EMAIL"] = userEmail;
                                Session["REQ_ID"] = R_id.ToString();
                                Response.Redirect("requisitionForm.aspx");
                            }
                        }
                    }
                }
            }
        }
    }
}