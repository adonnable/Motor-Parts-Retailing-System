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
    public partial class DeductItems : System.Web.UI.Page
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nichole\OneDrive\Desktop\MOTO PARTS PROJ\MotoPart\MotoPart\App_Data\MOTOPART.mdf;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Request_Items();
            }
        }

        protected void Request_Items()
        {
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT INVENTORY.INV_ID, INVENTORY.INV_INSTCK, INVENTORY.INV_UPRICE, ITEM.ITEM_NAME, ITEM.ITEM_ID "
                                        + "FROM INVENTORY "
                                        + "INNER JOIN ITEM ON INVENTORY.ITEM_ID = ITEM.ITEM_ID "
                                        + "WHERE INV_STATUS = 'AVAILABLE' OR INV_STATUS = 'REQUESTED' OR INV_STATUS = 'REORDERED'";
                    DataTable dt1 = new DataTable();
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd);
                    sda1.Fill(dt1);

                    if (dt1.Rows.Count > 0)
                    {
                        req_items.DataSource = dt1;
                        req_items.DataBind();
                       
                    }
                }
            }
        }     

        protected void req_items_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddItem")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Check if rowIndex is within the valid range
                if (rowIndex >= 0 && rowIndex < req_items.Rows.Count)
                {
                    string str_inv_id = req_items.Rows[rowIndex].Cells[0].Text;
                    int inv_id = int.Parse(str_inv_id);
                    Session["inv"] = inv_id;

                    string item_desc = req_items.Rows[rowIndex].Cells[1].Text;
                    string str_price = req_items.Rows[rowIndex].Cells[2].Text;

                    itemname.Text = item_desc;
                    price.Text = str_price;


                    //req_items.Rows[rowIndex].Visible = false;
                }
                else
                {
                    Response.Write("<script>alert('Invalid index!')</script>");
                }
            }
        }

        protected void btn_deduct_Click(object sender, EventArgs e)
        {

            string str_id = Session["inv"].ToString();
            int inv_id = Convert.ToInt32(str_id);

            string str_qtty = txt_qtty.Text;
            int qtty = Convert.ToInt32(str_qtty);

            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT INV_INSTCK FROM INVENTORY WHERE INV_ID = @id";
                    cmd.Parameters.AddWithValue("@id", inv_id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string str = reader["INV_INSTCK"].ToString();
                            int instck = Convert.ToInt32(str);

                            if (instck >= qtty)
                            {
                                reader.Close();
                                cmd.Parameters.Clear();
                                cmd.CommandText = "UPDATE INVENTORY SET INV_INSTCK = (INV_INSTCK - @qtty) WHERE INV_ID = @id";
                                cmd.Parameters.AddWithValue("@qtty", qtty);
                                cmd.Parameters.AddWithValue("@id", inv_id);

                                var ctr = cmd.ExecuteNonQuery();
                                if (ctr >= 1)
                                {
                                    Response.Write("<script>alert('Update Successful')</script>");
                                    check_ordrlvl();
                                    Request_Items();
                                    check_ifNoStock();
                                    itemname.Text = "";
                                    price.Text = "";
                                    txt_qtty.Text = "";
                                    total.Text = "";
                                }
                                else
                                {
                                    Response.Write("<script>alert('Update Unsuccessful')</script>");
                                    itemname.Text = "";
                                    price.Text = "";
                                    txt_qtty.Text = "";
                                    total.Text = "";
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('Sorry, insufficient stock. Please adjust your quantity.')</script>");
                                itemname.Text = "";
                                price.Text = "";
                                txt_qtty.Text = "";
                                total.Text = "";
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('No record found for the specified INV_ID')</script>");
                            itemname.Text = "";
                            price.Text = "";
                            txt_qtty.Text = "";
                            total.Text = "";
                        }
                    }
                }
            }
        }

        protected void check_ordrlvl()
        {
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE INVENTORY SET INV_STATUS = 'REQUESTED' WHERE INV_INSTCK <= INV_ORDRLVL";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void check_ifNoStock()
        {
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE INVENTORY SET INV_STATUS = 'STOCKOUT' WHERE INV_INSTCK = 0";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void linkbtn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("inventory.aspx");
        }
    }
}