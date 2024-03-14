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

namespace MotoPart.MotoParts
{
    public partial class staff_dashboard : System.Web.UI.Page
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nichole\OneDrive\Desktop\MOTO PARTS PROJ\MotoPart\MotoPart\App_Data\MOTOPART.mdf;Integrated Security=True";
        string userEmail;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Dsply_UserInfo();
                supp_grid_dsply();
            }
        }
        protected void SessionHandler()
        {
            if (Session["STAFF_EMAIL"] != null)
            {
                userEmail = Session["STAFF_EMAIL"].ToString();                
            }
        }

        protected void Dsply_UserInfo()
        {
            SessionHandler();
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT STAFF_FNAME, STAFF_LNAME, STAFF_PROFILE_PIC FROM STAFF WHERE STAFF_EMAIL = @userEmail";
                    cmd.Parameters.AddWithValue("@userEmail", userEmail);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
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
                            else
                            {
                                Response.Write("<script>alert('ang pic null')</script>");
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

        protected void user_profile_Click1(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "openCardModal", "openCardModal();", true);
            Dsply_UserInfo();
        }


        protected void btn_save_Click(object sender, EventArgs e)
        {
            SessionHandler();
            Dsply_UserInfo();
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
                        }
                        else
                        {
                            reader.Close();
                            cmd.Parameters.Clear();
                            cmd.CommandText = "UPDATE STAFF SET STAFF_PROFILE_PIC = @file WHERE STAFF_EMAIL = '" + userEmail + "'";
                            cmd.Parameters.AddWithValue("@file", pic);
                            var ctr = cmd.ExecuteNonQuery();

                            if(ctr >= 1)
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
            else
            {
                Response.Write("<script>alert('❌ Invalid file type!')</script>");
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
    }

    
}