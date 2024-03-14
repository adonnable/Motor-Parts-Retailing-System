<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="delivery.aspx.cs" Inherits="MotoPart.MotoParts.delivery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="bootstrap_style.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.3.0/font/bootstrap-icons.css"/>
    <link href="delivery_design.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-lg bg-primary" data-bs-theme="dark">
            <div class="container-fluid">

                <img src="../Images/logo-motor.png" style="height: 60px; width: 60px;" />
                <div class="navbar-brand" style="font-weight: bold; font-size: 20px; margin-left: 10px;">CloudTuner Garage</div>

                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarColor01" aria-controls="navbarColor01" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>

                <div class="collapse navbar-collapse" id="navbarColor01">
                    <ul class="navbar-nav me-auto">
                        <li class="nav-item">
                            <a class="nav-link" href="ad_dashboard.aspx">Home </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="inventory.aspx">Inventory</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="RequisitionPage.aspx">Requisition</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="Purchasing.aspx">Purchasing</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link active" href="delivery.aspx">Delivery
                                <span class="visually-hidden">(current)</span>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="transaction_history.aspx">Transaction</a>
                        </li>
                    </ul>

                    <div class="icons">
                        <ul class="navbar-nav me-auto">
                            <li>
                                <a target="_blank" rel="noopener" class="fb-icon nav-link" href="https://web.facebook.com/teletubiesmemes"><i class="bi bi-facebook" style="margin-bottom: 5px; margin-left: 1px;"></i></a>
                            </li>
                        </ul>
                    </div>

                    <div class="custom_wrapper">
                        <div class="container_wrap">
                            <div class="profile_info">
                                <asp:ImageButton ID="user_profile" runat="server"
                                    Height="60px" Width="60px" Style="border-radius: 50%; border: solid; border-width: 3px; border-color: #2C3E50;" onmouseover="this.style.borderColor='#13967d'" onmouseout="this.style.borderColor='#2C3E50'" />
                                <asp:Label ID="user_name" runat="server" Text="" Style="margin-left: 10px;"></asp:Label>
                            </div>
                            <div class="dropdown">
                                <a class="btn btn-secondary dropdown_arrow dropdown-toggle" role="button" data-bs-toggle="dropdown" aria-expanded="false"><i class="bi bi-three-dots-vertical"></i></a>
                                <ul class="dropdown-menu">
                                    <li><a class="dropdown-item" href="#">Settings</a></li>
                                    <li><a class="dropdown-item" href="ad_add_staff.aspx">Add Account</a></li>
                                    <li><a class="dropdown-item" href="AddSupplier.aspx">Add Supplier</a></li>
                                    <li><a class="dropdown-item" href="ad_log_out.aspx">Log Out</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </nav>

      <div class="container">
            <div class="picture">
                <img src="../images/logo-motor.png"  style="width: 27rem; height: 30rem;"/>
            </div>
            <div class="data">      
                <center><asp:Label ID="Label1" style="border-radius: 10px; padding:5px 50px 5px 50px;" runat="server" Text="Incoming Delivery" BackColor="#008484" ForeColor="White"></asp:Label></center>
                <p></p>
                <p></p>
                <center class="adjust">
                    <asp:GridView ID="Purchased" runat="server" CellPadding="10" OnRowDataBound="Purchased_RowDataBound" GridLines="None" OnRowCommand="Purchased_RowCommand" ForeColor="#333333" CellSpacing="5">
                         <Columns>
                              <asp:ButtonField Text="View PO" CommandName="ViewPO" />
                         </Columns>
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
                    <asp:Label style="margin-top: 200px; color: red;" ID="NoRecord" runat="server" Text="No Records Found"></asp:Label>
                </center>
            </div>
        </div>

       
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>

