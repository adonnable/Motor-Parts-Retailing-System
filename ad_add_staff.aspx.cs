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
    public partial class ad_add_staff : System.Web.UI.Page
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nichole\OneDrive\Desktop\MOTO PARTS PROJ\MotoPart\MotoPart\App_Data\MOTOPART.mdf;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_create_Click(object sender, EventArgs e)
        {
            string firstname = Request.Form["fname"];
            string lastname = Request.Form["lname"];
            string address = Request.Form["address"];
            string c_number = Request.Form["cnum"];
            string email_add = Request.Form["email"];
            string c_pass = Request.Form["repass"];
            string status = "ACTIVE";

            DateTime currentDate = DateTime.Now.Date;
            
            byte[] encData_byte = new byte[c_pass.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(c_pass);
            string encodedData = Convert.ToBase64String(encData_byte);

            string defaultImagePath = Server.MapPath("../Images/blank-profile-picture.png");           
            // Read the default image into a byte array
            byte[] defaultImageBytes = File.ReadAllBytes(defaultImagePath);

            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT COUNT(*) FROM STAFF, ADMIN WHERE STAFF_EMAIL = @txtemail OR AD_EMAIL = @txtemail  ";
                    cmd.Parameters.AddWithValue("@txtemail", email_add);

                    int rowCount = (int)cmd.ExecuteScalar();

                        if (rowCount < 1)
                        {                       
                            cmd.Parameters.Clear();
                            cmd.CommandText = "INSERT INTO STAFF (STAFF_FNAME, STAFF_LNAME, STAFF_ADDRESS, STAFF_CNUM, STAFF_EMAIL,STAFF_PASSWORD, STAFF_STATUS, STAFF_PROFILE_PIC, STAFF_CREATED)"
                                            + "VALUES (@txt_fname, @txt_lname, @txt_address, @txt_cnum, @txt_email, @txt_password, @txt_status, @profile, @created)";

                            cmd.Parameters.AddWithValue("@txt_fname", firstname);
                            cmd.Parameters.AddWithValue("@txt_lname", lastname);
                            cmd.Parameters.AddWithValue("@txt_address", address);
                            cmd.Parameters.AddWithValue("@txt_cnum", c_number);
                            cmd.Parameters.AddWithValue("@txt_email", email_add);
                            cmd.Parameters.AddWithValue("@txt_password", encodedData);
                            cmd.Parameters.AddWithValue("@txt_status", status);
                            cmd.Parameters.AddWithValue("@profile", SqlDbType.Image).Value = defaultImageBytes;
                            cmd.Parameters.AddWithValue("@created", currentDate);

                            var ctr = cmd.ExecuteNonQuery();

                            if (ctr >= 1)
                            {
                                Response.Write("<script>alert('Record is saved')</script>");

                            }
                            else
                            {
                                Response.Write("<script>alert('❌Record is not saved')</script>");

                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('⚠️ Account is already registered!')</script>");
                        }
                }
            }
        }

        protected void linkbtn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ad_dashboard.aspx");
        }
    }
}