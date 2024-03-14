<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View_PO.aspx.cs" Inherits="MotoPart.MotoParts.View_PO" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title></title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <link href="View_PO.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <asp:Button ID="backBtn" class="btn btn-danger btn-close" runat="server" Text="" OnClick="backBtn_Click" />
            <div class="header">
                <img src="../images/logo-motor.png" class="logo" />
                <h1>Purchase Order</h1>
                <div class="POandDate">
                    <div>
                        <b>PO #:</b>
                        <asp:Label ID="POnumber" runat="server" Text=""></asp:Label>
                    </div>
                    <div>
                        <b>Date:</b>
                        <asp:Label ID="Date" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
            <p></p>
            <p></p>
            <p></p>
            <div class="purchase-order-details">
                <b>Requisitioner:</b> &nbsp
                <asp:Label ID="requestor" runat="server" Text="" Style="text-decoration: underline;"></asp:Label>
                <p></p>
                <p></p>
                <p></p>
                <div class="company-info">
                    <h3>Company Name: Cloud Tuner Garage</h3>
                    <asp:Label ID="stadd" runat="server" Text="Company Street: Labogon Street"></asp:Label>
                    <asp:Label ID="city" runat="server" Text="Company [Address,ZIP]: Mandaue City, Philippines, 6014"></asp:Label>
                    <asp:Label ID="phone" runat="server" Text="Company Phone: 0999 581 4023"></asp:Label>
                </div>
            </div>

            <div class="Supplier-and-ShipTo">
                <div class="cont">
                    <center>
                        <asp:Label ID="Label11" Style="padding: 5px 50px 5px 50px; margin-right: 200px;" runat="server" Text="SUPPLIER" BackColor="#008484" ForeColor="White" Width="250"></asp:Label></center>
                    <asp:Label class="text" ID="cpyName" runat="server" Text="Label"></asp:Label>
                    <asp:Label class="text" ID="cpyAddress" runat="server" Text="Label"></asp:Label>
                    <asp:Label class="text" ID="cpyCnum" runat="server" Text="Label"></asp:Label>
                    <asp:Label class="text" ID="persName" runat="server" Text="Label"></asp:Label>
                    <asp:Label class="text" ID="persCnum" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="cont2">
                    <center>
                        <asp:Label ID="Label12" Style="padding: 5px 50px 5px 50px; margin-right: 200px;" runat="server" Text="SHIP TO" BackColor="#008484" ForeColor="White" Width="250"></asp:Label></center>
                    <asp:Label class="text" ID="Label6" runat="server" Text="CLOUD TUNER GARAGE"></asp:Label>
                    <asp:Label class="text" ID="Label7" runat="server" Text="LABOGON STREET"></asp:Label>
                    <asp:Label class="text" ID="Label8" runat="server" Text="MANDAUE CITY, PHILIPPINES, 6014"></asp:Label>
                    <asp:Label class="text" ID="Label9" runat="server" Text="PHONE: 0999 581 4023"></asp:Label>
                </div>
            </div>
            <div class="product-list">
                <asp:GridView ID="reqItems" runat="server" CellPadding="10" CellSpacing="5" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
            <div class="total">
                <label>Total Amount:</label>
                <asp:Label ID="totalAmount" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
