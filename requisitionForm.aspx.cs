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
    public partial class requisitionForm : System.Web.UI.Page
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nichole\OneDrive\Desktop\MOTO PARTS PROJ\MotoPart\MotoPart\App_Data\MOTOPART.mdf;Integrated Security=True";
        int req_id;
        string userEmail = "";
        string user_id;
        string txt_req_status;
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
            if (Session["EMAIL"] != null && Session["REQ_ID"] != null)
            {
                userEmail = Session["EMAIL"].ToString();
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
                using(var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;                    
                    cmd.CommandText = "SELECT * FROM REQUISITION WHERE REQ_ID = '" + req_id + "' "
                        +"ORDER BY CASE REQ_STATUS WHEN 'PENDING' THEN 1 WHEN 'APPROVED' THEN 2 WHEN 'COMPLETED' THEN 3 WHEN 'CANCELLED' THEN 4 END, "
                        +"REQ_DATE DESC, REQ_TIME DESC";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null && reader.Read())
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal("AD_ID")))
                            {
                                user_id = reader["AD_ID"].ToString();
                                req_num.Text = reader["REQ_ID"].ToString();
                                txt_date.Text = reader["REQ_DATE"].ToString();
                                txt_req_status= reader["REQ_STATUS"].ToString();
                           
                                if (txt_req_status != "CANCELLED")
                                {
                                    req_cancel.Enabled = false;
                                    req_cancel.Visible = false;
                                    req_again.Enabled = false;
                                    req_again.Visible = false;
                                }
                                else
                                {
                                    req_cancel.Enabled = true;
                                    req_cancel.Visible = true;
                                    req_again.Enabled = true;
                                    req_again.Visible = true;
                                }

                                cmd.Parameters.Clear();
                                reader.Close();
                                cmd.CommandText = "SELECT AD_FNAME, AD_LNAME FROM ADMIN WHERE AD_ID = '"+user_id+"'";
                                using(SqlDataReader readname = cmd.ExecuteReader())
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
                                cmd.CommandText = "SELECT REQ_ID, REQ_DATE,REQ_STATUS, STAFF_ID FROM REQUISITION WHERE REQ_ID = '"+req_id+ "' "
                                    +"ORDER BY CASE REQ_STATUS WHEN 'PENDING' THEN 1 WHEN 'APPROVED' THEN 2 WHEN 'COMPLETED' THEN 3 WHEN 'CANCELLED' THEN 4 END, "
                                    +"REQ_DATE DESC, REQ_TIME DESC";
                                using(SqlDataReader reader1 = cmd.ExecuteReader())
                                {
                                    if (reader1 != null && reader1.Read())
                                    {
                                        if (!reader1.IsDBNull(reader1.GetOrdinal("STAFF_ID")))
                                        {
                                            user_id = reader1["STAFF_ID"].ToString();
                                            req_num.Text = reader1["REQ_ID"].ToString();
                                            txt_date.Text = reader1["REQ_DATE"].ToString();
                                            txt_req_status = reader1["REQ_STATUS"].ToString();
                                       
                                            if (txt_req_status != "CANCELLED")
                                            {
                                                req_cancel.Enabled = false;
                                                req_cancel.Visible = false;
                                                req_again.Enabled = false;
                                                req_again.Visible = false;

                                            }
                                            else
                                            {
                                                req_cancel.Enabled = true;
                                                req_cancel.Visible = true;
                                                req_again.Enabled = true;
                                                req_again.Visible = true;
                                            }


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
            using(var db = new SqlConnection(connString))
            {
                db.Open();
                using(var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT INV_ID, R_ITEM_UNIT, R_ITEM_DESC, R_ITEM_QTTY, R_ITEM_UPRICE, R_ITEM_TOTAL " +
                                    "FROM REQUEST_ITEM WHERE REQ_ID = '" + req_id + "'";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            DataTable dt1 = new DataTable();
                            dt1.Load(reader);
                            selected_items.DataSource = dt1;
                            selected_items.DataBind();
                        }
                        else
                        {
                            Response.Write("<script>alert('No items retrieved.')</script>");
                        }
                    }
                }
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("RequisitionPage.aspx");
        }

        protected void req_again_Click(object sender, EventArgs e)
        {
            SessionHandler();
            using(var db = new SqlConnection(connString))
            {
                db.Open();
                using(var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE REQUISITION SET REQ_STATUS = 'PENDING' WHERE REQ_ID = @id AND REQ_STATUS = 'CANCELLED'";
                    cmd.Parameters.AddWithValue("@id",req_id);
                    var ctr = cmd.ExecuteNonQuery();

                    if(ctr >= 1)
                    {
                        Response.Write("<script>alert('Successfully Updated')</script>");
                        Response.Redirect("RequisitionPage.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('Update Unsuccessful')</script>");
                    }
                }
            }
        }

        protected void req_cancel_Click(object sender, EventArgs e)
        {
            SessionHandler();
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    foreach (GridViewRow row in selected_items.Rows)
                    {
                        Label labelId = row.FindControl("label_id") as Label;

                        Response.Write("<script>alert('Lavel - id : " + labelId + "')</script>");
                        if (labelId != null)
                        {
                            cmd.CommandText = "UPDATE INVENTORY SET INV_STATUS = 'REQUESTED' WHERE INV_STATUS = 'REORDERED AND INV_ID = '"+labelId+"'";
                            var ctr = cmd.ExecuteNonQuery();
                            if (ctr > 0)
                            {
                                Response.Write("<script>alert('Na Update sa inventory')</script>");
                            }
                            else
                            {
                                Response.Write("<script>alert('Wla na update sa inventory')</script>");
                            }
                        }
                        
                    }
                    
                }
            }
        }
    }
}