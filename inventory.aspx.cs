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
    public partial class inventory : System.Web.UI.Page
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nichole\OneDrive\Desktop\MOTO PARTS PROJ\MotoPart\MotoPart\App_Data\MOTOPART.mdf;Integrated Security=True";
        int stck;
        int ordrlvl;
        float price;
        string userEmail = "";
        int user_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DisplayAll();
                NoRecord.Visible = false;
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

        protected void DisplayAll()
        {
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT INV_ID, INV_UNIT, ITEM_NAME, INV_INSTCK, INV_STATUS, INV_ORDRLVL, INV_UPRICE, INV_DATE, (INV_INSTCK * INV_UPRICE) AS TOTAL FROM INVENTORY "
                                + "INNER JOIN ITEM ON INVENTORY.ITEM_ID = ITEM.ITEM_ID ORDER BY INV_ID";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            NoRecord.Visible = false;                            
                        }
                        else
                        {
                            NoRecord.Visible = true;
                        }
                    }

                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    InventoryTable.DataSource = dt;
                    InventoryTable.DataBind();
                }
            }
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            string text_search = searchText.Text;

            if (int.TryParse(text_search, out _))
            {             
                int numericValue = int.Parse(text_search);
                

                using(var db = new SqlConnection(connString))
                {
                    db.Open();
                    using(var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT *, (INV_INSTCK * INV_UPRICE) AS TOTAL FROM INVENTORY Y INNER JOIN ITEM M ON Y.ITEM_ID = M.ITEM_ID WHERE Y.INV_ID = @id";

                        cmd.Parameters.AddWithValue("@id",numericValue);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                NoRecord.Visible = false;
                            }
                            else
                            {
                                NoRecord.Visible = true;
                            }
                        }

                        DataTable dt = new DataTable();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        InventoryTable.DataSource = dt;
                        InventoryTable.DataBind();
                    }
                }
            }
            else
            {
            
                using (var db = new SqlConnection(connString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT *, (INV_INSTCK * INV_UPRICE) AS TOTAL FROM INVENTORY Y INNER JOIN ITEM M ON Y.ITEM_ID = M.ITEM_ID WHERE M.ITEM_NAME LIKE @searchText";
                        cmd.Parameters.AddWithValue("@searchText", "%" + text_search + "%");


                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                NoRecord.Visible = false;
                            }
                            else
                            {
                                NoRecord.Visible = true;
                            }
                        }

                        DataTable dt = new DataTable();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        InventoryTable.DataSource = dt;
                        InventoryTable.DataBind();
                    }
                }
            }
        }

        protected void InventoryTable_RowEditing(object sender, GridViewEditEventArgs e)
        {
            InventoryTable.EditIndex = e.NewEditIndex;
            DisplayAll();
        }

        protected void InventoryTable_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            InventoryTable.EditIndex = -1;
            DisplayAll();
        }

        protected void InventoryTable_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(InventoryTable.DataKeys[e.RowIndex].Value.ToString());
            string unit = ((TextBox)InventoryTable.Rows[e.RowIndex].FindControl("txt_unit")).Text.Trim();
            string item = ((TextBox)InventoryTable.Rows[e.RowIndex].FindControl("txt_item")).Text.Trim();
            string str_ordrlvl = ((TextBox)InventoryTable.Rows[e.RowIndex].FindControl("txt_olvl")).Text.Trim();
            string str_price = ((TextBox)InventoryTable.Rows[e.RowIndex].FindControl("txt_uprice")).Text.Trim();

            if (!string.IsNullOrEmpty(str_ordrlvl) && !string.IsNullOrEmpty(str_price))
            {
               
                ordrlvl = Convert.ToInt32(str_ordrlvl);
                price = float.Parse(str_price);
       
                using (var db = new SqlConnection(connString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE INVENTORY SET INV_UNIT = @unit, "
                                        + "INV_ORDRLVL = @olvl, INV_UPRICE = @price WHERE INV_ID = @id";

                        cmd.Parameters.AddWithValue("@unit", unit);
                        cmd.Parameters.AddWithValue("@instock", stck);
                        cmd.Parameters.AddWithValue("@olvl", ordrlvl);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@id", id);

                        var ctr = cmd.ExecuteNonQuery();
                        if (ctr >= 1)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = "UPDATE ITEM SET ITEM_NAME = @itemname " +
                                                "FROM ITEM INNER JOIN INVENTORY ON ITEM.ITEM_ID = INVENTORY.ITEM_ID " +
                                                "WHERE INV_ID = @id";
                            cmd.Parameters.AddWithValue("@itemname", item);
                            cmd.Parameters.AddWithValue("@id", id);
                            var ctr2 = cmd.ExecuteNonQuery();
                            if (ctr2 >= 1)
                            {
                                InventoryTable.EditIndex = -1;
                                DisplayAll();
                               
                            }
                            else
                            {
                                Response.Write("<script>alert('Update Unsuccessful!')</script>");
                            }

                        }
                        else
                        {
                            Response.Write("<script>alert('Update Unsuccessful!')</script>");
                        }

                    }
                }
            }
            else
            {
                Response.Write("<script>alert('Fill all the cells')</script>");
            }                     
        }

        protected void insert_item_Click(object sender, EventArgs e)
        {
            Response.Redirect("inventory_insert_item.aspx");
        }

        protected void InventoryTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = e.Row.DataItem as DataRowView;

                if (rowView != null)
                {
                    // Check if the "TOTAL" column exists in the DataTable
                    if (rowView.Row.Table.Columns.Contains("TOTAL"))
                    {
                        // Ensure that the "TOTAL" column is not null before conversion
                        if (rowView["TOTAL"] != DBNull.Value)
                        {
                            decimal total = Convert.ToDecimal(rowView["TOTAL"]);
                            Label labelTotal = e.Row.FindControl("label_total") as Label;

                            if (labelTotal != null)
                            {
                                labelTotal.Text = total.ToString("N");
                            }
                        }
                    }
                    else
                    {
                        
                        Label labelTotal = e.Row.FindControl("label_total") as Label;

                        if (labelTotal != null)
                        {
                            labelTotal.Text = "N/A";
                        }
                    }
                }
            }
        }



        protected void deduct_item_Click(object sender, EventArgs e)
        {
            Response.Redirect("DeductItems.aspx");
        }
    }
}