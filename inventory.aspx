<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="inventory.aspx.cs" Inherits="MotoPart.MotoParts.inventory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="bootstrap_style.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.3.0/font/bootstrap-icons.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.2/font/bootstrap-icons.min.css" />
    <link href="inventory_.css" rel="stylesheet" />
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
                            <a class="nav-link" href="ad_dashboard.aspx">Home</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link active" href="#">Inventory
                                <span class="visually-hidden">(current)</span>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="RequisitionPage.aspx">Requisition</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="Purchasing.aspx">Purchasing</a>
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
                                    <li><a class="dropdown-item" href="#">Suspend Account</a></li>
                                    <li><a class="dropdown-item" href="#">Log Out</a></li>
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
                    <asp:Label ID="lbl_supplier" runat="server" Text="Inventory" BackColor="#008484" ForeColor="White" Style="text-align: center; border-radius: 5px; margin-left: 10px; padding: 5px 50px 5px 50px;" EnableTheming="True"></asp:Label>
                    <p></p>
                    <div class="display">

                        <div class="control-func">
                            <div class="func-inner">
                                <asp:Button ID="insert_item" class="btn btn-primary" runat="server" Text="Insert Item" OnClick="insert_item_Click" />
                            <asp:Button ID="deduct_item" runat="server" Text="Deduct Item" class="btn btn-warning" Onclick="deduct_item_Click"/>
                            
                            </div>
                            
                            <div class="search">
                                <asp:TextBox ID="searchText" runat="server" placeholder="Search"></asp:TextBox>
                                <asp:Button ID="searchBtn" class="btn btn-primary" runat="server" Text="Search" OnClick="searchBtn_Click" />
                            </div>
                        </div>

                    </div>
                    <p></p>
                    <div class="tableView">
                        <asp:GridView ID="InventoryTable" runat="server" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" Width="100%" AutoGenerateColumns="false" OnRowDataBound="InventoryTable_RowDataBound"
                            DataKeyNames="INV_ID" ShowFooter="true" OnRowEditing="InventoryTable_RowEditing" OnRowCancelingEdit="InventoryTable_RowCancelingEdit" OnRowUpdating="InventoryTable_RowUpdating" FooterStyle-Height="25px" AlternatingRowStyle-HorizontalAlign="Justify">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <FooterStyle BackColor="White" ForeColor="#333333" />
                            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="White" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                            <SortedAscendingHeaderStyle BackColor="#487575" />
                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                            <SortedDescendingHeaderStyle BackColor="#275353" />

                            <Columns>
                                <asp:TemplateField HeaderText="INV ID">
                                    <ItemTemplate>
                                        <asp:Label ID="label_id" runat="server" Text='<%# Eval("INV_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="UNIT">
                                    <ItemTemplate>
                                        <asp:Label ID="label_unit" runat="server" Text='<%# Eval("INV_UNIT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_unit" runat="server" Text='<%# Eval("INV_UNIT") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ITEM NAME" ItemStyle-Width="300px">
                                    <ItemTemplate>
                                        <asp:Label ID="label_item" runat="server" Text='<%# Eval("ITEM_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_item" runat="server" Text='<%# Eval("ITEM_NAME") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="INSTOCK">
                                    <ItemTemplate>
                                        <asp:Label ID="label_instck" runat="server" Text='<%# Eval("INV_INSTCK") %>'></asp:Label>
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="STATUS">
                                    <ItemTemplate>
                                        <asp:Label ID="label_status" runat="server" Text='<%# Eval("INV_STATUS") %>'></asp:Label>
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ORDER LEVEL" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="label_olvl" runat="server" Text='<%# Eval("INV_ORDRLVL") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_olvl" runat="server" Text='<%# Eval("INV_ORDRLVL") %>' CssClass="custom-width"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="UNIT PRICE">
                                    <ItemTemplate>
                                        <asp:Label ID="label_uprice" runat="server" Text='<%# Eval("INV_UPRICE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_uprice" runat="server" Text='<%# Eval("INV_UPRICE") %>' CssClass="custom-width"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="TOTAL">
                                    <ItemTemplate>
                                        <asp:Label ID="label_total" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="DATE">
                                    <ItemTemplate>
                                        <asp:Label ID="label_date" runat="server" Text='<%# Eval("INV_DATE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="115px">
                                    <ItemTemplate>
                                        <asp:Button ID="edit_btn" runat="server" Text="Edit" CommandName="Edit" ToolTip="Edit" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="update_btn" runat="server" CommandName="Update" ToolTip="Update" CssClass="update_btn"><i class="bi bi-floppy2-fill"></i></i></asp:LinkButton>
                                        <asp:LinkButton ID="cancel_btn" runat="server" CommandName="Cancel" ToolTip="Cancel" CssClass="cancel_btn"><i class="bi bi-x-square"></i></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                    <center>
                        <asp:Label Style="margin-top: 200px; color: red;" ID="NoRecord" runat="server" Text="No Records Found"></asp:Label></center>
                </div>
            </div>
        </section>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
