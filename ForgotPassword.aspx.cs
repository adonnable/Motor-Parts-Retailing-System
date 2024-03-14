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
    public partial class ForgotPassword : System.Web.UI.Page
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nichole\OneDrive\Desktop\MOTO PARTS PROJ\MotoPart\MotoPart\App_Data\MOTOPART.mdf;Integrated Security = True";
        string userEmail = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionHandler();
            }
        }

        protected void SessionHandler()
        {
            if (Session["AD_EMAIL"] != null)
            {
                userEmail = Session["AD_EMAIL"].ToString();
                Response.Write("<script>alert('Email '" + userEmail + "'')</script>");

            }
            else if (Session["STAFF_EMAIL"] != null)
            {
                userEmail = Session["STAFF_EMAIL"].ToString();
                Response.Write("<script>alert('Email '" + userEmail + "'')</script>");
            }

        }

        protected void change_Click(object sender, EventArgs e)
        {
            string enteredEmail = cName.Value; // Assuming cName is used to capture the entered email

            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    if (!string.IsNullOrEmpty(enteredEmail))
                    {
                        cmd.Parameters.AddWithValue("@userEmail", enteredEmail);

                        // Check if the entered email is associated with an admin account
                        cmd.CommandText = "SELECT * FROM ADMIN WHERE AD_EMAIL COLLATE Latin1_General_CS_AS = @userEmail";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string oldpass = Encoding.UTF8.GetString(Convert.FromBase64String(reader["AD_PASS"].ToString()));
                                string newpass = cAddress.Value;
                                string confirmpass = cCnum.Value;
                                userEmail = reader["AD_EMAIL"].ToString();
                                reader.Close();
                            
                                if (oldpass != newpass && newpass == confirmpass)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = "UPDATE ADMIN SET AD_PASS = @newpass WHERE AD_EMAIL = @userEmailz";
                                    cmd.Parameters.AddWithValue("@newpass", Convert.ToBase64String(Encoding.UTF8.GetBytes(newpass)));
                                    cmd.Parameters.AddWithValue("@userEmailz", userEmail);

                                    int affectedRows = cmd.ExecuteNonQuery();

                                    if (affectedRows > 0)
                                    {
                                        Response.Write("<script>alert('Password changed successfully.')</script>");
                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('Failed to update password.')</script>");
                                    }
                                }
                                else
                                {
                                    Response.Write("<script>alert('Invalid old password or new password does not match confirmation.')</script>");
                                }
                                return;
                            }
                            reader.Close();
                        }

                        // Check if the entered email is associated with a staff account
                        cmd.CommandText = "SELECT * FROM STAFF WHERE STAFF_EMAIL COLLATE Latin1_General_CS_AS = @userEmail";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string oldpass = Encoding.UTF8.GetString(Convert.FromBase64String(reader["STAFF_PASSWORD"].ToString()));
                                string newpass = cAddress.Value;
                                string confirmpass = cCnum.Value;
                                userEmail = reader["STAFF_EMAIL"].ToString();

                                reader.Close();
                                if (oldpass != newpass && newpass == confirmpass)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = "UPDATE STAFF SET STAFF_PASSWORD = @newpass WHERE STAFF_EMAIL = @userEmailz";
                                    cmd.Parameters.AddWithValue("@newpass", Convert.ToBase64String(Encoding.UTF8.GetBytes(newpass)));
                                    cmd.Parameters.AddWithValue("@userEmailz", userEmail);

                                    int affectedRows = cmd.ExecuteNonQuery();

                                    if (affectedRows > 0)
                                    {
                                        Response.Write("<script>alert('Password changed successfully.')</script>");
                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('Failed to update password.')</script>");
                                    }
                                }
                                else
                                {
                                    Response.Write("<script>alert('Invalid old password or new password does not match confirmation.')</script>");
                                }
                               
                                return;
                            }
                            reader.Close();
                        }
                    }
                }
            }

            // If no matching email found in both admin and staff tables
            Response.Write("<script>alert('No records found for the provided email.')</script>");
        }
    

        protected void btn_back_Click(object sender, EventArgs e)
        {
                Response.Redirect("ad_login_form.aspx");
        }
    }   

}