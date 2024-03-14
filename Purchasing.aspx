<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Purchasing.aspx.cs" Inherits="MotoPart.MotoParts.Purchasing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CloudTuner Garage</title>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="ad_dashboard_bootstrap.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.3.0/font/bootstrap-icons.css"/>
    <link href="Purchasing.css" rel="stylesheet" />
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
                            <a class="nav-link active" href="Purchasing.aspx">Purchasing
                                <span class="visually-hidden">(current)</span>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="delivery.aspx">Delivery</a>
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
       
      <section>
        <div class="main-content">
            <div class="card">
                <center><asp:Label ID="lbl_staff" style="border-radius: 10px; padding:5px 50px 5px 50px;" runat="server" Text="Aprroved Request/s" BackColor="#008484" ForeColor="White"></asp:Label></center>
                <p></p>
                <p></p>
                <center>
                    <asp:GridView ID="Requisition" runat="server" CellPadding="10" GridLines="None" OnRowCommand="Requisition_RowCommand"  ForeColor="#333333" CellSpacing="5">
                         <Columns>
                              <asp:ButtonField Text="View" CommandName="ViewBtn"/>
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
            <div class="card">
                <center><asp:Label ID="Label1" style="border-radius: 10px; padding:5px 50px 5px 50px;" runat="server" Text="Purchased" BackColor="#008484" ForeColor="White"></asp:Label></center>
                <p></p>
                <p></p>
                <center>
                    <asp:GridView ID="Purchased" runat="server" CellPadding="10" GridLines="None" OnRowCommand="Purchased_RowCommand" ForeColor="#333333" CellSpacing="5">
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
                    <asp:Label style="margin-top: 200px; color: red;" ID="NoRecord2" runat="server" Text="No Records Found"></asp:Label>
                </center>
            </div>
            <div class="card">
                <div style="display: flex;">
                    <asp:Button ID="clear" class="btn btn-outline-danger" runat="server" Text="Clear" OnClick="clear_Click" />
                    <asp:Label ID="lbl_supplier" runat="server" Text="Request Item/s" BackColor="#008484" ForeColor="White" style="border-radius: 10px; margin-left: 140px; padding:5px 50px 5px 50px;" EnableTheming="True"></asp:Label>
                </div>
                <div class="info">
                    <asp:Label CssClass="shadow-label" ID="rqst" runat="server" Text="Requistion No. :"></asp:Label> 
                    <asp:Label CssClass="shadow-label" ID="rqstLbl" runat="server" Text=""></asp:Label>
                    <asp:Label CssClass="shadow-label" ID="delDate" style="margin-left:70px;" runat="server" Text="Date: "></asp:Label> 
                    <asp:Label CssClass="shadow-label" ID="delDateLbl" runat="server" Text=""></asp:Label>
                    <asp:Label CssClass="shadow-label" ID="time" style="margin-left:50px;" runat="server" Text="Time: "></asp:Label> 
                    <asp:Label CssClass="shadow-label" ID="timeLbl" runat="server" Text=""></asp:Label>
                </div>
                <center style="margin: 50px 0px 50px 0px;">
                    <asp:GridView ID="reqItems" runat="server" OnRowCommand="reqItems_RowCommand" OnRowDeleting="reqItems_RowDeleting" CellPadding="10" CellSpacing="5" ForeColor="#333333" GridLines="None">
                            <Columns>
                                <asp:ButtonField Text="x" CommandName="Delete" ButtonType="Button" ItemStyle-CssClass="btn btn-danger"/>
                                <asp:ButtonField Text="Edit" CommandName="EditBtn" ButtonType="Button"/>
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
                </center>
                <div class="info">
                    <asp:Label style="margin-left: 400px" ID="total" runat="server" Text="Total Cost:"></asp:Label>
                    <asp:Label ID="totalLbl" runat="server" Text=""></asp:Label>
                    <p></p>
                    <asp:Label ID="suppLbl" runat="server" Text="Suppiler: "></asp:Label>
                    <p></p>
                    <asp:DropDownList CssClass="form-control , pl-12,pr-50" ID="supplier" runat="server">
                    </asp:DropDownList>                   
                </div>
                <p></p>
                <p></p>
                <p></p>
                <div class="buttons">
                    <asp:Button ID="orderBtn" OnClientClick="return confirm('Are you sure? (Purchase Confirmation)');" runat="server" class="btn btn-success" Text="Purchase" OnClick="orderBtn_Click" />
                    <asp:Button ID="cancelBtn" OnClientClick="return confirm('Are you sure? (Cancel Confirmation)');" runat="server" class="btn btn-danger" Text="Cancel" style="" OnClick="cancelBtn_Click" />
                </div>
            </div>  
         </div>
    </section>        
         
</form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
