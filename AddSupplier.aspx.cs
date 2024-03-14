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
    public partial class AddSupplier : System.Web.UI.Page
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nichole\OneDrive\Desktop\MOTO PARTS PROJ\MotoPart\MotoPart\App_Data\MOTOPART.mdf;Integrated Security=True";
        string userIS;
        string userEmail;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessionHandler();
                VerifyEmail();
                
            }
        }
        protected void SessionHandler()
        {
            if (Session["AD_EMAIL"] != null)
            {
                userEmail = Session["AD_EMAIL"].ToString();
            }
            else if(Session["STAFF_EMAIL"] != null)
            {
                userEmail = Session["STAFF_EMAIL"].ToString();               
            }
            else
            {
                Response.Write("<script>alert('Session Not found')</script>");
            }
        }

        protected void VerifyEmail()
        {
            SessionHandler();
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT STAFF_EMAIL FROM STAFF WHERE STAFF_EMAIL = '" + userEmail + "'";
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userIS = "STAFF";
                        Response.Write("<script>alert('SSTAFF "+userEmail+"')</script>");
                    }
                    else
                    {
                        userIS = "ADMIN";
                        Response.Write("<script>alert('ADMIN " + userEmail + "')</script>");
                    }
                }
            }
        }


        protected void Add_Click(object sender, EventArgs e)
        {
            string cname = Request.Form["cName"].ToUpper();
            string caddress = Request.Form["cAddress"].ToUpper();
            string ccnum = Request.Form["cCnum"].ToUpper();
            string persname = Request.Form["persName"].ToUpper();
            string perscontno = Request.Form["persContno"].ToUpper();


            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM SUPPLIER_TBL WHERE SUPP_CPY_NAME = @compName";
                    cmd.Parameters.AddWithValue("@compName", cname);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.Read())
                    {
                        reader.Close();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "INSERT INTO SUPPLIER_TBL (SUPP_CPY_NAME, SUPP_CPY_ADDRESS, SUPP_CPY_CNUM, SUPP_PERS_NAME, SUPP_PERS_CNUM)" +
                                          "VALUES (@Cname, @Caddress, @Ccnum, @Persname, @Perscontno)";

                        cmd.Parameters.AddWithValue("@Cname", cname);
                        cmd.Parameters.AddWithValue("@Caddress", caddress);
                        cmd.Parameters.AddWithValue("@Ccnum", ccnum);
                        cmd.Parameters.AddWithValue("@Persname", persname);
                        cmd.Parameters.AddWithValue("@Perscontno", perscontno);

                        var ctr = cmd.ExecuteNonQuery();

                        if (ctr >= 1)
                        {
                            Response.Write("<script>alert('Supplier successfully added!')</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Supplier is not saved!')</script>");
                        }

                    }
                    else
                    {
                        Response.Write("<script>alert('Supplier already exists!')</script>");
                    }
                }
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            VerifyEmail(); // This should set userIS and userEmail based on the session

            // Clear session variables
            Session["STAFF_EMAIL"] = null;
            Session["AD_EMAIL"] = null;

            if (userIS == "ADMIN")
            {
                Session["AD_EMAIL"] = userEmail;
                Response.Redirect("ad_dashboard.aspx");
                Response.Write("<script>alert('ADMIN " + userEmail + "')</script>");
            }
            else
            {
                Response.Write("<script>alert('SSTAFF " + userEmail + "')</script>");
                Session["STAFF_EMAIL"] = userEmail;
                Response.Redirect("staff_dashboard.aspx");
            }
        }


    }
}
    
