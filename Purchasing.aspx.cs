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
    public partial class Purchasing : System.Web.UI.Page
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nichole\OneDrive\Desktop\MOTO PARTS PROJ\MotoPart\MotoPart\App_Data\MOTOPART.mdf;Integrated Security=True";
        string reqId;
        string formattedTotal;
        string suppId;
        string date;
        string timee;
        string userEmail;
        int user_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            rqst.Visible = false;
            rqstLbl.Visible = false;
            delDate.Visible = false;
            delDateLbl.Visible = false;
            orderBtn.Visible = false;
            cancelBtn.Visible = false;
            time.Visible = false;
            timeLbl.Visible = false;
            reqItems.Visible = false;
            suppLbl.Visible = false;
            supplier.Visible = false;
            total.Visible = false;
            totalLbl.Visible = false;
            NoRecord.Visible = false;
            NoRecord2.Visible = false;

            if (!IsPostBack)
            {
                purchasing();
                SessionHandler();
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

        private void purchasing()
        {
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT REQ_ID AS 'REQUISITION #', REQ_STATUS AS 'STATUS', REQ_DATE AS 'DATE' ,REQ_TIME AS 'TIME' FROM REQUISITION " +
                                      "WHERE REQ_STATUS = @app";
                    cmd.Parameters.AddWithValue("@app", "APPROVED");

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        reader.Close();
                        DataTable dt1 = new DataTable();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt1);

                        Requisition.Columns.Clear();

                        ButtonField viewButtonField = new ButtonField();
                        viewButtonField.Text = "View";
                        viewButtonField.CommandName = "ViewBtn";
                        viewButtonField.ButtonType = ButtonType.Button;

                        Requisition.Columns.Add(viewButtonField);

                        Requisition.DataSource = dt1;
                        Requisition.DataBind();
                    }
                    else
                    {
                        reader.Close();
                        NoRecord.Visible = true;
                    }

                    viewPurchased();
                }
            }
        }

        protected void viewPurchased()
        {
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd2 = db.CreateCommand())
                {
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "SELECT PURCH_ID AS 'PO #', PURCH_DATE AS 'DATE', PURCH_TIME AS 'TIME', PURCH_STATUS AS 'STATUS', PURCH_TOCOST AS 'COST' FROM PURCHASING_TBL " +
                                       "WHERE PURCH_STATUS IN ('PENDING', 'COMPLETED', 'CANCELLED') " +
                                       "ORDER BY CASE PURCH_STATUS WHEN 'PENDING' THEN 1 WHEN 'COMPLETED' THEN 2 WHEN 'CANCELLED' THEN 3 END";

                    SqlDataReader reader2 = cmd2.ExecuteReader();

                    if (reader2.Read())
                    {
                        reader2.Close();
                        DataTable dt2 = new DataTable();
                        SqlDataAdapter sdaa = new SqlDataAdapter(cmd2);
                        sdaa.Fill(dt2);

                        Purchased.Columns.Clear();

                        ButtonField buttonField2 = new ButtonField();
                        buttonField2.Text = "View PO";
                        buttonField2.CommandName = "ViewPO";
                        buttonField2.ButtonType = ButtonType.Button;

                        Purchased.Columns.Add(buttonField2);

                        Purchased.DataSource = dt2;
                        Purchased.DataBind();
                    }
                    else
                    {
                        reader2.Close();
                        NoRecord2.Visible = true;
                    }
                }
            }
        }
        protected void Requisition_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "ViewBtn")
            {
                using (var db = new SqlConnection(connString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT REQ_ID, REQ_DATE, REQ_TIME FROM REQUISITION " +
                                          "WHERE REQ_STATUS = 'APPROVED' " +
                                          "ORDER BY REQ_ID OFFSET @RowIndex ROWS FETCH NEXT 1 ROWS ONLY;";
                        cmd.Parameters.AddWithValue("@RowIndex", rowIndex);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            reqId = reader["REQ_ID"].ToString();
                            Session["reqId"] = reqId;
                            string date = reader["REQ_DATE"].ToString();
                            string timelbl = reader["REQ_TIME"].ToString();
                            delDateLbl.Text = date;
                            rqst.Visible = true;
                            rqstLbl.Text = reqId;
                            rqstLbl.Visible = true;
                            delDate.Visible = true;
                            delDateLbl.Visible = true;
                            orderBtn.Visible = true;
                            cancelBtn.Visible = true;
                            timeLbl.Text = timelbl;
                            time.Visible = true;
                            timeLbl.Visible = true;

                            reader.Close();


                            using (var db2 = new SqlConnection(connString))
                            {
                                db2.Open();
                                using (var cmd2 = db2.CreateCommand())
                                {
                                    cmd2.CommandType = CommandType.Text;
                                    cmd2.CommandText = "SELECT R_ITEM_UNIT AS 'UNIT', R_ITEM_DESC AS 'DESCRIPTION'," +
                                        "R_ITEM_QTTY AS 'QTY' , R_ITEM_UPRICE AS 'PRICE', R_ITEM_TOTAL AS 'TOTAL PRICE' FROM REQUEST_ITEM " +
                                        "WHERE REQ_ID = @rID";
                                    cmd2.Parameters.AddWithValue("@rID", reqId);

                                    SqlDataReader reader1 = cmd2.ExecuteReader();

                                    if (reader1.Read())
                                    {
                                        reader1.Close();
                                        DataTable dt2 = new DataTable();
                                        SqlDataAdapter sdaa = new SqlDataAdapter(cmd2);
                                        sdaa.Fill(dt2);
                                        reqItems.DataSource = dt2;
                                        reqItems.DataBind();
                                        reqItems.Visible = true;
                                        suppLbl.Visible = true;
                                        supplier.Visible = true;

                                        using (var db3 = new SqlConnection(connString))
                                        {
                                            db3.Open();
                                            using (var cmd3 = db3.CreateCommand())
                                            {
                                                cmd3.CommandType = CommandType.Text;
                                                cmd3.CommandText = "SELECT SUM(R_ITEM_TOTAL) FROM REQUEST_ITEM " +
                                                                   "WHERE REQ_ID = @rID";
                                                cmd3.Parameters.AddWithValue("@rID", reqId);

                                                using (SqlDataReader reader2 = cmd3.ExecuteReader())
                                                {
                                                    if (reader2.Read())
                                                    {
                                                        object total1 = reader2[0];

                                                        if (total1 != null && total1 != DBNull.Value)
                                                        {
                                                            formattedTotal = Convert.ToDecimal(total1).ToString("N2");
                                                            Session["fTotal"] = formattedTotal;
                                                            totalLbl.Text = formattedTotal;
                                                            total.Visible = true;
                                                            totalLbl.Visible = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        using (var cmd4 = db.CreateCommand())
                        {
                            cmd4.CommandType = CommandType.Text;
                            cmd4.CommandText = "SELECT SUPP_CPY_NAME FROM SUPPLIER_TBL";

                            using (SqlDataReader reader3 = cmd4.ExecuteReader())
                            {
                                supplier.Items.Clear();
                                supplier.Items.Add(new ListItem("--- Select Supplier ---", "None"));

                                while (reader3.Read())
                                {
                                    string supplierName = reader3["supp_cpy_name"].ToString();
                                    string capitalizedSupplierName = supplierName.ToUpper();

                                    supplier.Items.Add(new ListItem(supplierName, capitalizedSupplierName));
                                }
                            }
                        }
                    }
                }
            }
            viewPurchased();
        }

        protected void reqItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditBtn")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string reqID = Session["reqId"].ToString();

                using (var db = new SqlConnection(connString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT R_ITEM_ID, REQ_ID, R_ITEM_UNIT, R_ITEM_DESC, R_ITEM_QTTY, R_ITEM_UPRICE FROM REQUEST_ITEM " +
                                          "WHERE REQ_ID = @rid " +
                                          "ORDER BY R_ITEM_ID OFFSET @RowIndex ROWS FETCH NEXT 1 ROWS ONLY;";

                        cmd.Parameters.AddWithValue("@rid", reqID);
                        cmd.Parameters.AddWithValue("@RowIndex", rowIndex);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            Session["R_ITEM_UNIT"] = reader["R_ITEM_UNIT"].ToString();
                            Session["R_ITEM_DESC"] = reader["R_ITEM_DESC"].ToString();
                            Session["R_ITEM_QTTY"] = reader["R_ITEM_QTTY"].ToString();
                            Session["R_ITEM_ID"] = reader["R_ITEM_ID"].ToString();
                            Session["R_ITEM_UPRICE"] = reader["R_ITEM_UPRICE"].ToString();
                            Session["REQ_ID"] = Session["reqId"].ToString();

                            Response.Redirect("UpdateQty.aspx");
                        }
                    }
                }
            }
        }

        protected void reqItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.RowIndex);
            string reqID = Session["reqId"].ToString();

            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT R_ITEM_ID, REQ_ID, R_ITEM_UNIT, R_ITEM_DESC, R_ITEM_QTTY, R_ITEM_UPRICE FROM REQUEST_ITEM " +
                                      "WHERE REQ_ID = @rid " +
                                      "ORDER BY R_ITEM_ID OFFSET @RowIndex ROWS FETCH NEXT 1 ROWS ONLY;";

                    cmd.Parameters.AddWithValue("@rid", reqID);
                    cmd.Parameters.AddWithValue("@RowIndex", rowIndex);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string req_itemID = reader["R_ITEM_ID"].ToString();
                        string req_itemDesc = reader["R_ITEM_DESC"].ToString();
                        reader.Close();

                        using (var cmd2 = db.CreateCommand())
                        {
                            cmd2.CommandType = CommandType.Text;
                            cmd2.CommandText = "DELETE FROM REQUEST_ITEM WHERE R_ITEM_ID = @itemId";

                            cmd2.Parameters.AddWithValue("@itemId", req_itemID);

                            int ctr = cmd2.ExecuteNonQuery();

                            if (ctr >= 1)
                            {
                                rqst.Visible = true;
                                rqstLbl.Visible = true;
                                delDate.Visible = true;
                                delDateLbl.Visible = true;
                                orderBtn.Visible = true;
                                cancelBtn.Visible = true;
                                time.Visible = true;
                                timeLbl.Visible = true;
                                refreshTable();
                                reqItems.Visible = true;
                                suppLbl.Visible = true;
                                supplier.Visible = true;

                                using (var cmd3 = db.CreateCommand())
                                {
                                    cmd3.CommandType = CommandType.Text;
                                    cmd3.CommandText = "SELECT SUM(R_ITEM_TOTAL) FROM REQUEST_ITEM " +
                                                       "WHERE REQ_ID = @rID";
                                    cmd3.Parameters.AddWithValue("@rID", reqID);

                                    using (SqlDataReader reader2 = cmd3.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            object total1 = reader2[0];

                                            if (total1 != null && total1 != DBNull.Value)
                                            {
                                                formattedTotal = Convert.ToDecimal(total1).ToString("N2");
                                                Session["fTotal"] = formattedTotal;
                                                totalLbl.Text = formattedTotal;
                                                total.Visible = true;
                                                totalLbl.Visible = true;
                                            }
                                        }
                                    }
                                }
                                string alertMessage = $"alert('The item ({req_itemDesc}) has been deleted for the Requisition No. {reqID}')";
                                Response.Write("<script>" + alertMessage + "</script>");
                            }
                        }
                    }
                }
            }
        }
        private void refreshTable()
        {
            string reqId = Session["reqId"].ToString();

            using (var db2 = new SqlConnection(connString))
            {
                db2.Open();
                using (var cmd2 = db2.CreateCommand())
                {
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "SELECT R_ITEM_UNIT AS 'UNIT', R_ITEM_DESC AS 'DESCRIPTION'," +
                        "R_ITEM_QTTY AS 'QTY' , R_ITEM_UPRICE AS 'PRICE', R_ITEM_TOTAL AS 'TOTAL PRICE' FROM REQUEST_ITEM " +
                        "WHERE REQ_ID = @rID";
                    cmd2.Parameters.AddWithValue("@rID", reqId);

                    SqlDataReader reader1 = cmd2.ExecuteReader();

                    if (reader1.Read())
                    {
                        reader1.Close();
                        DataTable dt2 = new DataTable();
                        SqlDataAdapter sdaa = new SqlDataAdapter(cmd2);
                        sdaa.Fill(dt2);
                        reqItems.DataSource = dt2;
                        reqItems.DataBind();
                    }
                }
            }
        }

        protected void clear_Click(object sender, EventArgs e)
        {
            rqst.Visible = false;
            rqstLbl.Visible = false;
            delDate.Visible = false;
            delDateLbl.Visible = false;
            orderBtn.Visible = false;
            cancelBtn.Visible = false;
            time.Visible = false;
            timeLbl.Visible = false;
            reqItems.Visible = false;
            suppLbl.Visible = false;
            supplier.Visible = false;
            total.Visible = false;
            totalLbl.Visible = false;
            viewPurchased();
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
                        cmd.CommandText = "SELECT PURCH_ID, PURCH_DATE, PURCH_TOCOST, SUPP_ID, REQ_ID FROM PURCHASING_TBL " +
                                          "WHERE PURCH_STATUS IN ('PENDING', 'COMPLETED', 'CANCELLED') " +
                                          "ORDER BY CASE PURCH_STATUS WHEN 'PENDING' THEN 1 WHEN 'COMPLETED' THEN 2 WHEN 'CANCELLED' THEN 3 END " +
                                          "OFFSET @RowIndex ROWS FETCH NEXT 1 ROWS ONLY;";

                        cmd.Parameters.AddWithValue("RowIndex", rowIndex);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string pID = reader["PURCH_ID"].ToString();
                            string pDATE = reader["PURCH_DATE"].ToString();
                            string pTOCOST = reader["PURCH_TOCOST"].ToString();
                            string p_sID = reader["SUPP_ID"].ToString();
                            string p_rID = reader["REQ_ID"].ToString();

                            Session["PURCH_ID"] = pID;
                            Session["PURCH_DATE"] = pDATE;
                            Session["PURCH_TOCOST"] = pTOCOST;
                            Session["SUPP_ID"] = p_sID;
                            Session["REQ_ID"] = p_rID;

                            Response.Redirect("View_PO.aspx");
                        }

                    }
                }
            }
        }
        protected void orderBtn_Click(object sender, EventArgs e)
        {
            string reqIdValue = Session["reqId"].ToString();
            string formattedTotalValue = Session["fTotal"].ToString();
            double fTotal = Convert.ToDouble(formattedTotalValue);

            if (supplier.SelectedValue == "None")
            {
                Response.Write("<script>alert('Please select a supplier for the purchase')</script>");
                rqst.Visible = true;
                rqstLbl.Visible = true;
                delDate.Visible = true;
                delDateLbl.Visible = true;
                orderBtn.Visible = true;
                cancelBtn.Visible = true;
                time.Visible = true;
                timeLbl.Visible = true;
                reqItems.Visible = true;
                suppLbl.Visible = true;
                supplier.Visible = true;
                total.Visible = true;
                totalLbl.Visible = true;
            }
            else
            {
                using (var db = new SqlConnection(connString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        string supp = supplier.SelectedValue;

                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT SUPP_ID FROM SUPPLIER_TBL " +
                                          "WHERE SUPP_CPY_NAME = @sup";
                        cmd.Parameters.AddWithValue("@sup", supp);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            suppId = reader["SUPP_ID"].ToString();
                            DateTime currentDate = DateTime.Now.Date;
                            date = currentDate.ToString("yyyy-MM-dd");
                            timee = DateTime.Now.ToString("hh:mm:ss tt");
                        }

                        reader.Close();

                        using (var cmd2 = db.CreateCommand())
                        {
                            cmd2.CommandText = "INSERT INTO PURCHASING_TBL (PURCH_DATE, PURCH_TIME, PURCH_STATUS, PURCH_TOCOST, SUPP_ID, REQ_ID)" +
                                               "VALUES (@pDate, @pTime, @pStatus, @pTocost, @sID, @rID);" +
                                               "SELECT SCOPE_IDENTITY();";

                            cmd2.Parameters.Add("@pDate", date);
                            cmd2.Parameters.Add("@pTime", timee);
                            cmd2.Parameters.Add("@pStatus", "PENDING");
                            cmd2.Parameters.Add("@pTocost", fTotal);
                            cmd2.Parameters.Add("@sID", suppId);
                            cmd2.Parameters.Add("@rID", reqIdValue);

                            var insertedPId = cmd2.ExecuteScalar();

                            if (insertedPId != null)
                            {
                                int purchID = Convert.ToInt32(insertedPId);

                                using (var cmd4 = db.CreateCommand())
                                {
                                    DateTime currentDate = DateTime.Now.Date;
                                    date = currentDate.ToString("yyyy-MM-dd");
                                    timee = DateTime.Now.ToString("hh:mm:ss tt");

                                    cmd4.CommandType = CommandType.Text;
                                    cmd4.CommandText = "INSERT INTO DELIVERY_TBL (DELV_DATE, DELV_TIME, DELV_STATUS, PURCH_ID) " +
                                                       "VALUES (@datee, @time, @status, @pID)";

                                    cmd4.Parameters.AddWithValue("@datee", date);
                                    cmd4.Parameters.AddWithValue("@time", timee);
                                    cmd4.Parameters.AddWithValue("@status", "PENDING");
                                    cmd4.Parameters.AddWithValue("@pID", purchID);

                                    int ctr = cmd4.ExecuteNonQuery();

                                    if (ctr >= 1)
                                    {
                                        Response.Write("<script>alert('Order successfully purchased and delivery details saved!')</script>");
                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('Order is saved, but delivery details are not saved!')</script>");
                                    }
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('Order is not saved!')</script>");
                            }
                        }
                        using (var cmd3 = db.CreateCommand())
                        {
                            cmd3.CommandType = CommandType.Text;
                            cmd3.CommandText = "UPDATE REQUISITION " +
                                               "SET REQ_STATUS = @status " +
                                               "WHERE REQ_ID = @req";

                            cmd3.Parameters.AddWithValue("@status", "PURCHASED");
                            cmd3.Parameters.AddWithValue("@req", reqIdValue);

                            cmd3.ExecuteNonQuery();
                        }
                    }
                }
                Response.Redirect(Request.Url.ToString());
            }
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            string reqID = Session["reqId"].ToString();

            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE REQUISITION " +
                                       "SET REQ_STATUS = @status " +
                                       "WHERE REQ_ID = @req";

                    cmd.Parameters.AddWithValue("@status", "CANCELLED");
                    cmd.Parameters.AddWithValue("@req", reqID);

                    int ctr = cmd.ExecuteNonQuery();

                    if (ctr >= 1)
                    {
                        Response.Write("<script>alert('Order successfully cancelled!')</script>");
                    }

                    purchasing();
                }
            }
        }
    }
}