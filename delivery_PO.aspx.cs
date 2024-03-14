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
    public partial class delivery_PO : System.Web.UI.Page
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nichole\OneDrive\Desktop\MOTO PARTS PROJ\MotoPart\MotoPart\App_Data\MOTOPART.mdf;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            string pid = Session["PURCH_ID"].ToString();
            string pdate = Session["PURCH_DATE"].ToString();
            string ptocost = Session["PURCH_TOCOST"].ToString();
            string psid = Session["SUPP_ID"].ToString();
            string prid = Session["REQ_ID"].ToString();
            string delvID = Session["DELV_ID"].ToString();

            deliver_num.Text = delvID;
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

                                reqItems.Columns.Clear();

                                ButtonField editButtonField = new ButtonField();
                                editButtonField.Text = "Edit";
                                editButtonField.CommandName = "EditBtn";
                                editButtonField.ButtonType = ButtonType.Button;

                                reqItems.Columns.Add(editButtonField);
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
            Response.Redirect("Delivery.aspx");
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            string pID = Session["PURCH_ID"].ToString();
            string rID = Session["REQ_ID"].ToString();

            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE PURCHASING_TBL " +
                                      "SET PURCH_STATUS = @status " +
                                      "WHERE PURCH_ID = @pid";

                    cmd.Parameters.AddWithValue("@status", "CANCELLED");
                    cmd.Parameters.AddWithValue("@pid", pID);

                    cmd.ExecuteNonQuery();

                    using (var cmd3 = db.CreateCommand())
                    {
                        cmd3.CommandType = CommandType.Text;
                        cmd3.CommandText = "UPDATE DELIVERY_TBL " +
                                           "SET DELV_STATUS = @status " +
                                           "WHERE PURCH_ID = @pid";

                        cmd3.Parameters.AddWithValue("@status", "CANCELLED");
                        cmd3.Parameters.AddWithValue("@pid", pID);

                        cmd3.ExecuteNonQuery();
                    }

                    using (var cmd2 = db.CreateCommand())
                    {
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "UPDATE REQUISITION " +
                                           "SET REQ_STATUS = @status " +
                                           "WHERE REQ_ID = @rid";

                        cmd2.Parameters.AddWithValue("@status", "CANCELLED");
                        cmd2.Parameters.AddWithValue("@rid", rID);

                        int ctr = cmd2.ExecuteNonQuery();

                        if (ctr >= 1)
                        {
                            Response.Write("<script>alert('Purchase has been cancelled')</script>");

                        }
                    }
                }
            }
            Response.Redirect("Delivery.aspx");
        }

        protected void receivedBtn_Click(object sender, EventArgs e)
        {
            string pID = Session["PURCH_ID"].ToString();
            string rID = Session["REQ_ID"].ToString();

            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE PURCHASING_TBL " +
                                      "SET PURCH_STATUS = @status " +
                                      "WHERE PURCH_ID = @pid";

                    cmd.Parameters.AddWithValue("@status", "COMPLETED");
                    cmd.Parameters.AddWithValue("@pid", pID);

                    cmd.ExecuteNonQuery();

                    using (var cmd2 = db.CreateCommand())
                    {
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "UPDATE REQUISITION " +
                                           "SET REQ_STATUS = @status " +
                                           "WHERE REQ_ID = @rid";

                        cmd2.Parameters.AddWithValue("@status", "COMPLETED");
                        cmd2.Parameters.AddWithValue("@rid", rID);

                        int ctr = cmd2.ExecuteNonQuery();

                        if (ctr >= 1)
                        {
                            Response.Write("<script>alert('Purchase has been completed')</script>");
                        }

                        List<Tuple<string, string>> inventoryUpdates = new List<Tuple<string, string>>();
                        List<Tuple<string, string, string>> inventoryInsert = new List<Tuple<string, string, string>>();

                        using (var cmd3 = db.CreateCommand())
                        {
                            cmd3.CommandType = CommandType.Text;
                            cmd3.CommandText = "SELECT INV_ID, R_ITEM_QTTY, R_ITEM_DESC, R_ITEM_UPRICE FROM REQUEST_ITEM " +
                                               "WHERE REQ_ID = @rid";

                            cmd3.Parameters.AddWithValue("@rid", rID);
                            bool INV_IDisNull = true;
                            bool INV_IDisNull2 = false;

                            using (SqlDataReader reader = cmd3.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string itemQTY = reader["R_ITEM_QTTY"].ToString();
                                    string invID = reader["INV_ID"].ToString();
                                    string itemName = reader["R_ITEM_DESC"].ToString();
                                    string itemPrice = reader["R_ITEM_UPRICE"].ToString();

                                    if (!string.IsNullOrEmpty(invID))
                                    {
                                        INV_IDisNull = false;
                                        inventoryUpdates.Add(Tuple.Create(invID, itemQTY));
                                    }
                                    else
                                    {
                                        INV_IDisNull2 = true;
                                        inventoryInsert.Add(Tuple.Create(itemName, itemPrice, itemQTY));
                                    }
                                }
                            }

                            if (!INV_IDisNull)
                            {
                                foreach (Tuple<string, string> update in inventoryUpdates)
                                {
                                    string invID = update.Item1;
                                    string itemQTY = update.Item2;
                                    DateTime currentDate = DateTime.Now.Date;
                                    string date = currentDate.ToString("yyyy-MM-dd");

                                    using (var cmd4 = db.CreateCommand())
                                    {

                                        cmd4.CommandType = CommandType.Text;
                                        cmd4.CommandText = "UPDATE INVENTORY " +
                                                           "SET INV_INSTCK += @qty, " +
                                                           "    INV_STATUS = @status, " +
                                                           "    INV_DATE = @curDate " +
                                                           "WHERE INV_ID = @invId";

                                        cmd4.Parameters.AddWithValue("@qty", itemQTY);
                                        cmd4.Parameters.AddWithValue("@invId", invID);
                                        cmd4.Parameters.AddWithValue("@status", "AVAILABLE");
                                        cmd4.Parameters.AddWithValue("@curDate", date);

                                        cmd4.ExecuteNonQuery();
                                    }
                                }
                            }

                            if (INV_IDisNull2)
                            {
                                foreach (Tuple<string, string, string> insert in inventoryInsert)
                                {
                                    string itemName = insert.Item1;
                                    string itemPrice = insert.Item2;
                                    string itemQTY = insert.Item3;

                                    using (var cmd5 = db.CreateCommand())
                                    {
                                        cmd5.CommandType = CommandType.Text;
                                        cmd5.CommandText = "INSERT INTO ITEM (ITEM_NAME) VALUES (@itName); SELECT SCOPE_IDENTITY();";

                                        cmd5.Parameters.AddWithValue("@itName", itemName);

                                        var insertedItemId = cmd5.ExecuteScalar();

                                        int itemID = Convert.ToInt32(insertedItemId);
                                        DateTime currentDate = DateTime.Now.Date;
                                        string date = currentDate.ToString("yyyy-MM-dd");

                                        using (var cmd6 = db.CreateCommand())
                                        {

                                            cmd6.CommandType = CommandType.Text;
                                            cmd6.CommandText = "INSERT INTO INVENTORY(INV_INSTCK, INV_STATUS, INV_UPRICE, ITEM_ID, INV_DATE) " +
                                                               "VALUES (@stck, @status, @price, @itID, @curDate)";

                                            cmd6.Parameters.AddWithValue("@stck", itemQTY);
                                            cmd6.Parameters.AddWithValue("@status", "AVAILABLE");
                                            cmd6.Parameters.AddWithValue("@price", itemPrice);
                                            cmd6.Parameters.AddWithValue("@itID", itemID);
                                            cmd6.Parameters.AddWithValue("@curDate", date);

                                            cmd6.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    using (var cmd7 = db.CreateCommand())
                    {
                        cmd7.CommandType = CommandType.Text;
                        cmd7.CommandText = "UPDATE DELIVERY_TBL " +
                                           "SET DELV_STATUS = @status " +
                                           "WHERE PURCH_ID = @pid";

                        cmd7.Parameters.AddWithValue("@status", "COMPLETED");
                        cmd7.Parameters.AddWithValue("@pid", pID);

                        cmd7.ExecuteNonQuery();
                    }
                }
            }
            Response.Redirect("Delivery.aspx");
        }

        protected void Item_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string prid = Session["REQ_ID"].ToString();
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditBtn")
            {
                using (var db = new SqlConnection(connString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT R_ITEM_ID, REQ_ID, R_ITEM_UNIT, R_ITEM_DESC, R_ITEM_QTTY, R_ITEM_UPRICE FROM REQUEST_ITEM " +
                                          "WHERE REQ_ID = @rid " +
                                          "ORDER BY R_ITEM_ID OFFSET @RowIndex ROWS FETCH NEXT 1 ROWS ONLY;";

                        cmd.Parameters.AddWithValue("@rid", prid);
                        cmd.Parameters.AddWithValue("@RowIndex", rowIndex);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            Session["R_ITEM_UNIT"] = reader["R_ITEM_UNIT"].ToString();
                            Session["R_ITEM_DESC"] = reader["R_ITEM_DESC"].ToString();
                            Session["R_ITEM_QTTY"] = reader["R_ITEM_QTTY"].ToString();
                            Session["R_ITEM_UPRICE"] = reader["R_ITEM_UPRICE"].ToString();
                            Session["R_ITEM_ID"] = reader["R_ITEM_ID"].ToString();

                            Response.Redirect("EditQuantity.aspx");
                        }
                    }
                }
            }
        }
    }
}