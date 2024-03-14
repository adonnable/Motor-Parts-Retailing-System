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
    public partial class EditQuantity : System.Web.UI.Page
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nichole\OneDrive\Desktop\MOTO PARTS PROJ\MotoPart\MotoPart\App_Data\MOTOPART.mdf;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            string itemUnit = Session["R_ITEM_UNIT"].ToString();
            string itemDesc = Session["R_ITEM_DESC"].ToString();
            string itemQtty = Session["R_ITEM_QTTY"].ToString();

            Label1.Text = itemUnit;
            Label2.Text = itemDesc;
            Label4.Text = itemQtty;
        }

        protected void backBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("PurchaseOrder.aspx");
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string itemID = Session["R_ITEM_ID"].ToString();

            if (int.TryParse(lackingQty.Text, out int lackQty))
            {
                using (var db = new SqlConnection(connString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT R_ITEM_QTTY FROM REQUEST_ITEM " +
                                          "WHERE R_ITEM_ID = @reqID";
                        cmd.Parameters.AddWithValue("@reqID", itemID);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string STRrecentQty = reader["R_ITEM_QTTY"].ToString();
                            int recentQty = Convert.ToInt32(STRrecentQty);

                            reader.Close();

                            if (lackQty > 0)
                            {
                                if (recentQty >= lackQty)
                                {
                                    using (var db2 = new SqlConnection(connString))
                                    {
                                        db2.Open();
                                        using (var cmd1 = db2.CreateCommand())
                                        {
                                            cmd1.CommandType = CommandType.Text;
                                            cmd1.CommandText = "UPDATE REQUEST_ITEM " +
                                                              "SET R_ITEM_QTTY -= @lqty " +
                                                              "WHERE R_ITEM_ID = @rid";

                                            cmd1.Parameters.AddWithValue("@lqty", lackQty);
                                            cmd1.Parameters.AddWithValue("@rid", itemID);

                                            cmd1.ExecuteNonQuery();

                                            using (var cmd2 = db2.CreateCommand())
                                            {
                                                string itemQty = Session["R_ITEM_QTTY"].ToString();
                                                string itemPrice = Session["R_ITEM_UPRICE"].ToString();
                                                string itemDesc = Session["R_ITEM_DESC"].ToString();
                                                int intItemQty = Convert.ToInt32(itemQty);
                                                decimal declItemPrice = Convert.ToDecimal(itemPrice);

                                                int updatedQty = intItemQty - lackQty;
                                                decimal updatedTotalPrice = updatedQty * declItemPrice;

                                                cmd2.CommandType = CommandType.Text;
                                                cmd2.CommandText = "UPDATE REQUEST_ITEM " +
                                                                  "SET R_ITEM_TOTAL = @updTotPrice " +
                                                                  "WHERE R_ITEM_ID = @ridd";

                                                cmd2.Parameters.AddWithValue("@updTotPrice", updatedTotalPrice);
                                                cmd2.Parameters.AddWithValue("@ridd", itemID);

                                                int ctr = cmd2.ExecuteNonQuery();

                                                if (ctr >= 1)
                                                {
                                                    string alertMessage = $"alert('Quantity of {itemDesc} has been updated')";
                                                    Response.Write("<script>" + alertMessage + "</script>");
                                                }
                                            }
                                        }
                                    }
                                    Response.Redirect("delivery_PO.aspx");
                                }
                                else
                                {
                                    Response.Write("<script>alert('Invalid! The value you inputted is greater than the recent quantity of the item.')</script>");
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('Invalid! Please enter a positive number.')</script>");
                            }
                        }
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid! Please input a number.')</script>");
            }
        }
    }
}