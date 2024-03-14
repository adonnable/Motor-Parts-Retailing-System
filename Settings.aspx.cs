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
    public partial class Settings : System.Web.UI.Page
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nichole\OneDrive\Desktop\MOTO PARTS PROJ\MotoPart\MotoPart\App_Data\MOTOPART.mdf;Integrated Security = True";
        string userEmail = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                cName.Disabled = true;
                cAddress.Disabled = true;
                cCnum.Disabled = true;
                persName.Disabled = true;
                SessionHandler();

            }
        }
    
        protected void SessionHandler()
        {
            if (Session["AD_EMAIL"] != null)
            {
                using (var db = new SqlConnection(connString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        
                        userEmail = Session["AD_EMAIL"].ToString();
                        Response.Write("<script>alert('Email '" + userEmail + "'')</script>");
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT AD_FNAME, AD_LNAME, AD_CNUM FROM ADMIN WHERE AD_EMAIL = @userEmail";
                        cmd.Parameters.AddWithValue("@userEmail", userEmail);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string fname = reader["AD_FNAME"].ToString();
                                string lname = reader["AD_LNAME"].ToString();
                                string cnum = reader["AD_CNUM"].ToString();
                                cCnum.Value = userEmail;
                                cName.Value = fname;
                                cAddress.Value = lname;
                                persName.Value = cnum;
                            }
                            else
                            {
                                reader.Close();
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

                        userEmail = Session["STAFF_EMAIL"].ToString();
                        Response.Write("<script>alert('Email '" + userEmail + "'')</script>");
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT STAFF_FNAME, STAFF_LNAME, STAFF_CNUM FROM STAFF WHERE STAFF_EMAIL = @userEmail";
                        cmd.Parameters.AddWithValue("@userEmail", userEmail);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string fname = reader["STAFF_FNAME"].ToString();
                                string lname = reader["STAFF_LNAME"].ToString();
                                string cnum = reader["STAFF_CNUM"].ToString();
                                cCnum.Value = userEmail;
                                cName.Value = fname;
                                cAddress.Value = lname;
                                persName.Value = cnum;
                            }
                            else
                            {
                                reader.Close();
                            }
                        }


                    }
                }
            }

        }


        protected void changepass_Click(object sender, EventArgs e)
        {

            Response.Redirect("ChangePassword.aspx");
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            if (Session["AD_EMAIL"] != null)
            {
               
                Response.Redirect("ad_dashboard.aspx");

            }
            else if (Session["STAFF_EMAIL"] != null)
            {
           
                Response.Redirect("staff_dashboard.aspx");

            }

        }


        protected void update_click(object sender, EventArgs e)
        {
            Response.Redirect("UpdateProfile.aspx");
        }
    }
}