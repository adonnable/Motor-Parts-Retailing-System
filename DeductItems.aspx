<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeductItems.aspx.cs" Inherits="MotoPart.MotoParts.DeductItems" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <link href="DeductItem.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.2/font/bootstrap-icons.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div class="main">
                <div class="row">
                    <div class="left-panel">
                        <asp:Label ID="lbl_deduct_items" runat="server" Text="Deduct Items"></asp:Label>
                        <asp:GridView ID="req_items" runat="server" AutoGenerateColumns="False" AutoPostBack="True" Width="100%" OnRowCommand="req_items_RowCommand">
                            <HeaderStyle HorizontalAlign="Left" />
                            <Columns>
                                <asp:BoundField DataField="INV_ID" HeaderText="ID">
                                    <ItemStyle Width="50px" Font-Size="18px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ITEM_NAME" HeaderText="ITEM">
                                    <ItemStyle Width="300px" Font-Size="18px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="INV_UPRICE" HeaderText="PRICE">
                                    <ItemStyle Width="150px" Font-Size="18px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="INV_INSTCK" HeaderText="STK">
                                    <ItemStyle Width="55px" Font-Size="18px" />
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="ADD ITEM">
                                    <ItemTemplate>
                                        <asp:Button runat="server" Text="Add Item" CommandName="AddItem" CommandArgument='<%# Container.DataItemIndex %>' CssClass="add_item_btn" OnClientClick="return required_false();" />
                                    </ItemTemplate>
                                    <ControlStyle BackColor="#2C3E50" ForeColor="White" />
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle Height="40px" />
                            <RowStyle Height="40px" BackColor="White" />
                            <EditRowStyle BackColor="White" ForeColor="#333333" />
                            <HeaderStyle BackColor="#336666" BorderColor="#333333" BorderStyle="Double" ForeColor="White" />
                        </asp:GridView>

                    </div>

                    <div class="right-panel">
                        <div class="cancel_div">
                            <asp:LinkButton ID="linkbtn_cancel" runat="server" OnClick="linkbtn_cancel_Click"><i class="bi bi-x-square-fill"></i></asp:LinkButton>
                        </div>

                        <div class="content">
                            <div class="align-logo">
                                <img src="../Images/logo-motor.png" class="img-logo" />
                            </div>

                            <div class="e-flex">
                                <asp:Label ID="lbl_itemname" runat="server" Text="Item Name :"></asp:Label>
                                <asp:Label ID="itemname" runat="server" Text=""></asp:Label>
                            </div>

                            <div class="e-flex">
                                <asp:Label ID="lbl_price" runat="server" Text="Price :"></asp:Label>
                                <asp:Label ID="price" runat="server" Text=""></asp:Label>
                            </div>

                            <div class="e-flex">
                                <asp:Label ID="lbl_qtty" runat="server" Text="Quanity :"></asp:Label>
                                <asp:TextBox ID="txt_qtty" runat="server" type="number" onchange="cal_tot_val()" min="1"></asp:TextBox>
                            </div>

                            <div class="e-flex">
                                <asp:Label ID="lbl_total" runat="server" Text="Total:"></asp:Label>
                                <asp:Label ID="total" runat="server" Text=""></asp:Label>
                            </div>

                            <asp:Button ID="btn_deduct" runat="server" Text="Deduct" Onclick="btn_deduct_Click"/>


                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        function cal_tot_val() {
            var txtQtty = document.getElementById('<%= txt_qtty.ClientID %>');
            var lblPrice = document.getElementById('<%= price.ClientID %>');
            var lblTotal = document.getElementById('<%= total.ClientID %>');

            var num1 = parseInt(txtQtty.value) || 0;
            var num2 = parseFloat(lblPrice.innerText) || 0;

            if (!isNaN(num1) && !isNaN(num2)) {
                var total = (num1 * num2).toFixed(2);
                lblTotal.innerText = parseFloat(total).toLocaleString(undefined, {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                });
            } else {
                // Clear the total if either quantity or price is not a valid numeric value
                lblTotal.innerText = "";
            }

        }
    </script>

</body>
</html>
