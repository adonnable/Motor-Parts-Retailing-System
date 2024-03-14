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
    public partial class View_PO : System.Web.UI.Page
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nichole\OneDrive\Desktop\MOTO PARTS PROJ\MotoPart\MotoPart\App_Data\MOTOPART.mdf;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            string pid = Session["PURCH_ID"].ToString();
            string pdate = Session["PURCH_DATE"].ToString();
            string ptocost = Session["PURCH_TOCOST"].ToString();
            string psid = Session["SUPP_ID"].ToString();
            string prid = Session["REQ_ID"].ToString();

            POnumber.Text = pid;
            DateTime date = DateTime.ParseExact(pdate, "yyyy-MM-dd", null);
            string formattedDate = date.ToString("MM-dd-yyyy");
            Date.Text = formattedDate;

            string requisitioner;
            getRequestorID(prid, out requisitioner);
            requestor.Text = requisitioner;

            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT SUPP_CPY_NAME, SUPP_CPY_ADDRESS, SUPP_CPY_CNUM, SUPP_PERS_NAME, SUPP_PERS_CNUM FROM SUPPLIER_TBL " +
                                      "WHERE SUPP_ID = @suppID";

                    cmd.Parameters.AddWithValue("@suppID", psid);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        cpyName.Text = reader["SUPP_CPY_NAME"].ToString();
                        cpyAddress.Text = reader["SUPP_CPY_ADDRESS"].ToString();
                        cpyCnum.Text = reader["SUPP_CPY_CNUM"].ToString();
                        persName.Text = reader["SUPP_PERS_NAME"].ToString();
                        persCnum.Text = reader["SUPP_PERS_CNUM"].ToString();

                        reader.Close();

                        using (var cmd2 = db.CreateCommand())
                        {
                            cmd2.CommandType = CommandType.Text;
                            cmd2.CommandText = "SELECT R_ITEM_UNIT AS 'UNIT', R_ITEM_DESC AS 'DESCRIPTION'," +
                                "R_ITEM_QTTY AS 'QTY' , R_ITEM_UPRICE AS 'PRICE', R_ITEM_TOTAL AS 'TOTAL PRICE' FROM REQUEST_ITEM " +
                                "WHERE REQ_ID = @rID";
                            cmd2.Parameters.AddWithValue("@rID", prid);

                            SqlDataReader reader1 = cmd2.ExecuteReader();

                            if (reader1.Read())
                            {
                                reader1.Close();
                                DataTable dt = new DataTable();
                                SqlDataAdapter sda = new SqlDataAdapter(cmd2);
                                sda.Fill(dt);
                                reqItems.DataSource = dt;
                                reqItems.DataBind();

                                using (var cmd3 = db.CreateCommand())
                                {
                                    cmd3.CommandType = CommandType.Text;
                                    cmd3.CommandText = "SELECT SUM(R_ITEM_TOTAL) FROM REQUEST_ITEM " +
                                                       "WHERE REQ_ID = @rID";
                                    cmd3.Parameters.AddWithValue("@rID", prid);

                                    using (SqlDataReader reader2 = cmd3.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            object total1 = reader2[0];

                                            if (total1 != null && total1 != DBNull.Value)
                                            {
                                                string formattedTotal = Convert.ToDecimal(total1).ToString("N2");
                                                Session["fTotal"] = formattedTotal;
                                                totalAmount.Text = formattedTotal;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void getRequestorID(string reqID, out string requestor)
        {
            string reqId = reqID;

            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT STAFF_ID, AD_ID FROM REQUISITION " +
                                      "WHERE REQ_ID = @reqID";

                    cmd.Parameters.AddWithValue("@reqID", reqId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string staffID = reader["STAFF_ID"].ToString();
                        string adminID = reader["AD_ID"].ToString();

                        reader.Close();

                        if (staffID == "")
                        {
                            using (var cmd2 = db.CreateCommand())
                            {
                                cmd2.CommandType = CommandType.Text;
                                cmd2.CommandText = "SELECT AD_FNAME, AD_LNAME FROM ADMIN " +
                                                   "WHERE AD_ID = @adID";

                                cmd2.Parameters.AddWithValue("@adID", adminID);

                                using (SqlDataReader reader2 = cmd2.ExecuteReader())
                                {
                                    if (reader2.Read())
                                    {
                                        string fname = reader2["AD_FNAME"].ToString();
                                        string lname = reader2["AD_LNAME"].ToString();

                                        string fullName = string.Concat(fname, " ", lname);

                                        requestor = fullName;
                                    }
                                    else
                                    {
                                        requestor = "None";
                                    }
                                }

                            }
                        }
                        else if (adminID == "")
                        {
                            using (var cmd3 = db.CreateCommand())
                            {
                                cmd3.CommandType = CommandType.Text;
                                cmd3.CommandText = "SELECT STAFF_FNAME, STAFF_LNAME FROM STAFF " +
                                                   "WHERE STAFF_ID = @staffID";

                                cmd3.Parameters.AddWithValue("@staffID", staffID);

                                using (SqlDataReader reader3 = cmd3.ExecuteReader())
                                {
                                    if (reader3.Read())
                                    {
                                        string fname = reader3["STAFF_FNAME"].ToString();
                                        string lname = reader3["STAFF_LNAME"].ToString();

                                        string fullName = string.Concat(fname, " ", lname);

                                        requestor = fullName;
                                    }
                                    else
                                    {
                                        requestor = "None";
                                    }
                                }
                            }
                        }
                        else
                        {
                            requestor = "None";
                        }
                    }
                    else
                    {
                        requestor = "None";
                    }
                }
            }
        }

        protected void backBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Purchasing.aspx");
        }
    }
}
