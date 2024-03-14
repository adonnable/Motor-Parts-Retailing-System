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
    public partial class delivery : System.Web.UI.Page
    {
        string userEmail;
        int user_id;
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nichole\OneDrive\Desktop\MOTO PARTS PROJ\MotoPart\MotoPart\App_Data\MOTOPART.mdf;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            NoRecord.Visible = false;

            if (!IsPostBack)
            {
                SessionHandler();
                using (var db = new SqlConnection(connString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT PURCH_ID AS 'PO #', PURCH_DATE AS 'DATE', PURCH_TIME AS 'TIME', PURCH_STATUS AS 'STATUS', PURCH_TOCOST AS 'COST' FROM PURCHASING_TBL " +
                                           "WHERE PURCH_STATUS = 'PENDING' ";

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            reader.Close();
                            DataTable dt = new DataTable();
                            SqlDataAdapter sda = new SqlDataAdapter(cmd);
                            sda.Fill(dt);

                            Purchased.Columns.Clear();

                            ButtonField buttonField2 = new ButtonField();
                            buttonField2.Text = "View PO";
                            buttonField2.CommandName = "ViewPO";
                            buttonField2.ButtonType = ButtonType.Button;

                            Purchased.Columns.Add(buttonField2);

                            Purchased.DataSource = dt;
                            Purchased.DataBind();
                        }
                        else
                        {
                            NoRecord.Visible = true;
                        }
                    }
                }
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

                                }
                            }
                        }
                    }
                }
            }

        }
        protected void Purchased_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewPO")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                using (var db = new SqlConnection(connString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT PURCH_ID, PURCH_DATE, PURCH_TIME, PURCH_STATUS, PURCH_TOCOST, SUPP_ID, REQ_ID FROM PURCHASING_TBL " +
                                           "WHERE PURCH_STATUS = 'PENDING' " +
                                           "ORDER BY PURCH_ID OFFSET @RowIndex ROWS FETCH NEXT 1 ROWS ONLY;";

                        cmd.Parameters.AddWithValue("RowIndex", rowIndex);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string pID = reader["PURCH_ID"].ToString();
                            string pDATE = reader["PURCH_DATE"].ToString();
                            string pTOCOST = reader["PURCH_TOCOST"].ToString();
                            string p_sID = reader["SUPP_ID"].ToString();
                            string p_rID = reader["REQ_ID"].ToString();

                            reader.Close();
                            cmd.Parameters.Clear();
                            int PO = Convert.ToInt32(pID);
                            cmd.CommandText = "SELECT DELV_ID FROM DELIVERY_TBL " +
                                                   "WHERE PURCH_ID = @po";
                            cmd.Parameters.AddWithValue("@po", PO);
                            using (SqlDataReader reader1 = cmd.ExecuteReader())
                            {
                                if (reader1.Read())
                                {
                                    Session["DELV_ID"] = reader1["DELV_ID"].ToString();
                                    Session["PURCH_ID"] = pID;
                                    Session["PURCH_DATE"] = pDATE;
                                    Session["PURCH_TOCOST"] = pTOCOST;
                                    Session["SUPP_ID"] = p_sID;
                                    Session["REQ_ID"] = p_rID;

                                    Response.Redirect("delivery_PO.aspx");
                                }
                            }

                        
                        }
                    }
                }
            }
        }

        protected void Purchased_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Customize the padding for each cell in the row
                foreach (TableCell cell in e.Row.Cells)
                {
                    cell.Style["font-size"] = "15px";
                }
            }
        }
    }
}

