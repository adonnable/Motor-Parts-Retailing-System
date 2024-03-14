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
    public partial class ChangePassword : System.Web.UI.Page
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


        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Settings.aspx");
        }

        protected void changepass_Click(object sender, EventArgs e)
        {
            SessionHandler();

            if (Session["AD_EMAIL"] != null)
            {
                using (var db = new SqlConnection(connString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        userEmail = Session["AD_EMAIL"].ToString();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT AD_PASS FROM ADMIN WHERE AD_EMAIL = @userEmail";
                        cmd.Parameters.AddWithValue("@userEmail", userEmail);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string oldpass = Encoding.UTF8.GetString(Convert.FromBase64String(reader["AD_PASS"].ToString()));
                                string newpass = cAddress.Value;
                                string confirmpass = cCnum.Value;
                                reader.Close();

                                if (oldpass == cName.Value && newpass == confirmpass)
                                {
                                    cmd.CommandText = "UPDATE ADMIN SET AD_PASS = @newpass WHERE AD_EMAIL = @userEmails";
                                    cmd.Parameters.AddWithValue("@newpass", Convert.ToBase64String(Encoding.UTF8.GetBytes(newpass)));
                                    cmd.Parameters.AddWithValue("@userEmails", userEmail);

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
                using (var db = new SqlConnection(connString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        userEmail = Session["STAFF_EMAIL"].ToString();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT STAFF_PASSWORD FROM STAFF WHERE STAFF_EMAIL = @userEmail ";
                        cmd.Parameters.AddWithValue("@userEmail", userEmail);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string oldpass = Encoding.UTF8.GetString(Convert.FromBase64String(reader["STAFF_PASSWORD"].ToString()));
                                string newpass = cAddress.Value;
                                string confirmpass = cCnum.Value;
                                reader.Close();


                                if (oldpass == cName.Value && newpass == confirmpass)
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

    }
}
