using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Drawing.Imaging;
using System.IO;

namespace MotoPart.Admin_Page
{
    public partial class ad_login_form : System.Web.UI.Page
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nichole\OneDrive\Desktop\MOTO PARTS PROJ\MotoPart\MotoPart\App_Data\MOTOPART.mdf;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            string email = id_email.Value;
            string pass = id_pass.Value;

            string def_pass = "admin";

            byte[] encData_byte = new byte[def_pass.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(def_pass);
            string encodedData = Convert.ToBase64String(encData_byte);

            string defaultImagePath = Server.MapPath("../Images/admin_vector_icon.jpg");
            // Read the default image into a byte array
            byte[] defaultImageBytes = File.ReadAllBytes(defaultImagePath);

            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT COUNT(*) FROM ADMIN";
                    int rowCount = (int)cmd.ExecuteScalar();

                    if(rowCount < 1)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "INSERT INTO ADMIN (AD_EMAIL, AD_PASS, AD_LNAME, AD_PROFILE_PIC) VALUES ('admin@gmail.com', '"+ encodedData + "', '"+(rowCount+1)+"', '@profile')";
                        cmd.Parameters.AddWithValue("@profile", SqlDbType.Image).Value = defaultImageBytes;
                        cmd.ExecuteNonQuery();                       
                    }             
                    
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT AD_EMAIL, AD_PASS FROM ADMIN WHERE AD_EMAIL COLLATE Latin1_General_CS_AS = @email"; // Compare case-sensitive
                    cmd.Parameters.AddWithValue("@email", email);
                    using (SqlDataReader reader2 = cmd.ExecuteReader())
                    {
                        if (reader2.Read())
                        {
                            
                            string storedPassword = Encoding.UTF8.GetString(Convert.FromBase64String(reader2["AD_PASS"].ToString()));

                            if (pass == storedPassword)
                            {
                                Session["AD_EMAIL"] = email;
                                Response.Redirect("ad_dashboard.aspx");
                            }
                            else
                            {
                                Response.Write("<script>alert('Incorrect email or password!')</script>");
                                id_pass.Value = "";
                            }                            
                        }
                        else
                        {
                            reader2.Close();
                            cmd.Parameters.Clear();
                            cmd.CommandText = "SELECT STAFF_EMAIL, STAFF_PASSWORD FROM STAFF WHERE STAFF_EMAIL COLLATE Latin1_General_CS_AS = @email"; // Compare case-sensitive
                            cmd.Parameters.AddWithValue("@email", email);
                            SqlDataReader reader3 = cmd.ExecuteReader();
                            if (reader3.Read())
                            {
                                string storedPassword = Encoding.UTF8.GetString(Convert.FromBase64String(reader3["STAFF_PASSWORD"].ToString()));

                                if (pass == storedPassword)
                                {
                                    Session["STAFF_EMAIL"] = email;
                                    Response.Redirect("staff_dashboard.aspx");
                                }
                                else
                                {
                                    Response.Write("<script>alert('Incorrect email or password!')</script>");
                                    id_pass.Value = "";
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('The email and password have not been registered!')</script>");
                                id_email.Value = "";
                                id_pass.Value = "";
                            }                            
                        }
                    }
                }
            }
        }
    }
}

