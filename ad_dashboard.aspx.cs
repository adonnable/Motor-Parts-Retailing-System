using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;

namespace MotoPart.Admin_Page
{
    public partial class ad_dashboard : System.Web.UI.Page
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nichole\OneDrive\Desktop\MOTO PARTS PROJ\MotoPart\MotoPart\App_Data\MOTOPART.mdf;Integrated Security=True";
        string userEmail;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DsplyUserInfo();
                Bind_staff_grid();
                supp_grid_dsply();
                Display_ReqStat();
            }
        }

        protected void SessionHandler()
        {
            if (Session["AD_EMAIL"] != null)
            {
                userEmail = Session["AD_EMAIL"].ToString();

            }
        }

        protected void DsplyUserInfo()
        {
            SessionHandler();
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT AD_FNAME, AD_LNAME, AD_PROFILE_PIC FROM ADMIN WHERE AD_EMAIL = '" + userEmail + "'";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
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
                        else
                        {
                            reader.Close();
                        }
                    }
                }
            }
        }

        protected void user_profile_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "openCardModal", "openCardModal();", true);
            DsplyUserInfo();
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            SessionHandler();
            HttpPostedFile postedFile = upload_profile.PostedFile;
            string filename = Path.GetFileName(postedFile.FileName);
            string filext = Path.GetExtension(filename).ToLower();
            int filesize = postedFile.ContentLength;
            byte[] pic = new byte[upload_profile.PostedFile.ContentLength];
            upload_profile.PostedFile.InputStream.Read(pic, 0, upload_profile.PostedFile.ContentLength);

            if (filext == ".bmp" || filext == ".jpg" || filext == ".png" || filext == ".jpeg")
            {
                using (var db = new SqlConnection(connString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT AD_EMAIL FROM ADMIN WHERE AD_EMAIL = '" + userEmail + "'";
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            cmd.Parameters.Clear();
                            reader.Close();
                            cmd.CommandText = "UPDATE ADMIN SET AD_PROFILE_PIC = @file WHERE AD_EMAIL = '" + userEmail + "'";
                            cmd.Parameters.AddWithValue("@file", pic);
                            cmd.ExecuteNonQuery();

                            cmd.Parameters.Clear();
                            cmd.CommandText = "SELECT AD_PROFILE_PIC FROM ADMIN WHERE AD_EMAIL = '" + userEmail + "'";
                            SqlDataReader reader1 = cmd.ExecuteReader();
                            if (reader1.Read())
                            {
                                byte[] new_prof = (byte[])reader1["AD_PROFILE_PIC"];
                                string str = Convert.ToBase64String(new_prof);
                                user_profile.ImageUrl = "data:image/jpeg;base64," + str;
                            }
                            else
                            {
                                reader.Close();
                                cmd.Parameters.Clear();
                                cmd.CommandText = "UPDATE STAFF SET STAFF_PROFILE_PIC = @file WHERE STAFF_EMAIL = '" + userEmail + "'";
                                cmd.Parameters.AddWithValue("@file", pic);
                                var ctr = cmd.ExecuteNonQuery();

                                if (ctr >= 1)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = "SELECT STAFF_PROFILE_PIC FROM STAFF WHERE STAFF_EMAIL = '" + userEmail + "'";
                                    SqlDataReader reader2 = cmd.ExecuteReader();
                                    if (reader2.Read())
                                    {
                                        byte[] new_prof = (byte[])reader2["STAFF_PROFILE_PIC"];
                                        string str = Convert.ToBase64String(new_prof);
                                        user_profile.ImageUrl = "data:image/jpeg;base64," + str;
                                    }
                                }
                            }
                        }
                    }
                }

            }
            else
            {
                Response.Write("<script>alert('❌ Invalid file type!')</script>");
            }
        }

        protected void Bind_staff_grid()
        {
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT STAFF_FNAME, STAFF_LNAME, STAFF_PROFILE_PIC, STAFF_EMAIL, STAFF_ADDRESS, STAFF_CNUM,STAFF_STATUS,STAFF_CREATED FROM STAFF ORDER BY STAFF_FNAME";
                    SqlDataReader reader = cmd.ExecuteReader();

                    staff_grid.DataSource = reader;
                    staff_grid.DataBind();
                }
            }
        }

        protected void staff_grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string fname = DataBinder.Eval(e.Row.DataItem, "STAFF_FNAME").ToString();
                string lname = DataBinder.Eval(e.Row.DataItem, "STAFF_LNAME").ToString();
                string email = DataBinder.Eval(e.Row.DataItem, "STAFF_EMAIL").ToString();
                string address = DataBinder.Eval(e.Row.DataItem, "STAFF_ADDRESS").ToString();
                string cnum = DataBinder.Eval(e.Row.DataItem, "STAFF_CNUM").ToString();
                string status = DataBinder.Eval(e.Row.DataItem, "STAFF_STATUS").ToString();
                byte[] pic = (byte[])DataBinder.Eval(e.Row.DataItem, "STAFF_PROFILE_PIC");

                Label lblStaffName = (Label)e.Row.FindControl("lbl_staffName");
                Label lblStaffEmail = (Label)e.Row.FindControl("lbl_staffemail");
                Label lblStaffAdd = (Label)e.Row.FindControl("lbl_staffadd");
                Label lblStaffCnum = (Label)e.Row.FindControl("lbl_staffcnum");
                Label lblStaffStatus = (Label)e.Row.FindControl("lbl_staffstatus");
                Label lblStaffCreated = (Label)e.Row.FindControl("lbl_staffcreated");

                Image profImg = (Image)e.Row.FindControl("prof_img");


                string concatenatedValue = fname + " " + lname;


                lblStaffName.Text = concatenatedValue;
                lblStaffEmail.Text = email;
                lblStaffAdd.Text = address;
                lblStaffCnum.Text = cnum;
                lblStaffStatus.Text = status;
                profImg.ImageUrl = $"data:image;base64,{Convert.ToBase64String(pic)}";
            }
        }

        protected void supp_grid_dsply()
        {
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT SUPP_CPY_NAME, SUPP_CPY_ADDRESS, SUPP_CPY_CNUM, SUPP_PERS_NAME, SUPP_PERS_CNUM FROM SUPPLIER_TBL ORDER BY SUPP_CPY_NAME";
                    DataTable dt1 = new DataTable();
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd);
                    sda1.Fill(dt1);

                    if (dt1.Rows.Count > 0)
                    {
                        supp_grid.DataSource = dt1;
                        supp_grid.DataBind();
                    }
                }
            }
        }

        protected void req_stat_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SessionHandler();

            if (e.CommandName == "View")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                if (rowIndex >= 0 && rowIndex < req_stat.Rows.Count)
                {
                    string str_req_id = req_stat.Rows[rowIndex].Cells[0].Text;
                    int R_id = int.Parse(str_req_id);

                    Session["REQ_ID"] = R_id;
                    Response.Redirect("ApproveRequest.aspx");
                }
            }
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
                    cmd.CommandText = "SELECT REQ_ID, REQ_STATUS, REQ_DATE, REQ_TIME FROM REQUISITION WHERE REQ_STATUS = 'PENDING' ORDER BY REQ_DATE DESC, REQ_TIME DESC";

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

        }
    }
}