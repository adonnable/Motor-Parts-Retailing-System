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
    public partial class inventory_insert_item : System.Web.UI.Page
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nichole\OneDrive\Desktop\MOTO PARTS PROJ\MotoPart\MotoPart\App_Data\MOTOPART.mdf;Integrated Security=True";
        int item_id;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_insert_Click(object sender, EventArgs e)
        {
            string i_unit = id_unit.Value.ToUpper();
            string i_item = id_item.Value.ToUpper();
            string i_qtty = id_qtty.Value;
            string i_price = id_uprice.Value;

            DateTime currentDate = DateTime.Now.Date;
            string date = currentDate.ToString("yyyy-MM-dd");
  
            int qtty = Convert.ToInt32(i_qtty);
            float uprice = float.Parse(i_price);

            using(var db = new SqlConnection(connString))
            {
                db.Open();
                using(var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO ITEM (ITEM_NAME) VALUES(@item)";
                    cmd.Parameters.AddWithValue("@item", i_item);
                    var ctr = cmd.ExecuteNonQuery();
                    if(ctr >= 1)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SELECT ITEM_ID FROM ITEM WHERE ITEM_NAME = @itemname";
                        cmd.Parameters.AddWithValue("itemname", i_item);
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                               string str = reader["ITEM_ID"].ToString();
                                item_id = Convert.ToInt32(str);
                                Response.Write("<script>alert('Item Name Inserted')</script>");
                            }
                            else
                            {
                                Response.Write("<script>alert('ID not read!')</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Insertion Unsuccessful')</script>");
                    }

                }

                using(var cmd1 = db.CreateCommand())
                {
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "INSERT INTO INVENTORY (INV_UNIT, INV_INSTCK, INV_UPRICE,INV_STATUS,INV_DATE, ITEM_ID) VALUES (@unit, @stck, @price,@status,@date, @id)";

                    cmd1.Parameters.AddWithValue("@unit", i_unit);
                    cmd1.Parameters.AddWithValue("@stck", qtty);
                    cmd1.Parameters.AddWithValue("@price", uprice);
                    cmd1.Parameters.AddWithValue("@status", "AVAILABLE");
                    cmd1.Parameters.AddWithValue("@date", date);
                    cmd1.Parameters.AddWithValue("@id", item_id);

                    var ctr = cmd1.ExecuteNonQuery();
                    if(ctr >= 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "openCardModal", "openCardModal();", true);
                    }
                    else
                    {
                        Response.Write("<script>alert('Insertion Unsuccessful')</script>");
                    }

                }
            }
        }

        protected void linkbtn_cancel_Click(object sender, EventArgs e)
        {
            id_unit.Disabled = true;
            id_item.Disabled = true;
            id_qtty.Disabled = true;
            id_uprice.Disabled = true;
            Response.Redirect("inventory.aspx");
        }

        protected void btn_yes_Click(object sender, EventArgs e)
        {
            id_unit.Disabled = true;
            id_item.Disabled = true;
            id_qtty.Disabled = true;
            id_uprice.Disabled = true;
            Response.Redirect("inventory_insert_item.aspx");
        }

        protected void btn_no_Click(object sender, EventArgs e)
        {
            Response.Redirect("inventory.aspx");
        }
    }
}