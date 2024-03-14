<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequisitionPage.aspx.cs" Inherits="MotoPart.MotoParts.RequisitionPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CloudTuner Garage</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.3.0/font/bootstrap-icons.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.2/font/bootstrap-icons.min.css" />
    <link href="bootstrap_style.css" rel="stylesheet" />
    <link href="RequisitionPage_style.css" rel="stylesheet" />
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
                            <a class="nav-link active" href="RequisitionPage.aspx">Requisition
                                <span class="visually-hidden">(current)</span>
                            </a>
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
                <div class="card-btn">
                    <asp:Button ID="btn_createReq" runat="server" Text="Create a Requisition" class="btn btn-primary" OnClick="btn_createReq_Click"/>
                </div>
                <div class="card">           
                    <asp:Label ID="lbl_req_items" runat="server" Text="Requested Items"></asp:Label>
                    <div class="card-contents">
                        <center>
                            <asp:Label ID="info" runat="server" Text=""></asp:Label>
                        </center>                        
                        <asp:GridView ID="dsply_item" runat="server" AutoGenerateColumns="False" AutoPostBack="True" Width="100%" CssClass="dsplygrid">
                            <HeaderStyle HorizontalAlign="Left" />
                            <Columns>
                                <asp:BoundField DataField="INV_ID" HeaderText="ID"></asp:BoundField>
                                <asp:BoundField DataField="ITEM_NAME" HeaderText="ITEM" />
                                <asp:BoundField DataField="INV_INSTCK" HeaderText="STOCK" />
                            </Columns>
                            <EditRowStyle BackColor="#CDF5FD" />
                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Height="58px" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#C9D1D3" ForeColor="#333333" Height="58px" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                            <AlternatingRowStyle BackColor="#F0F0F0" ForeColor="#284775" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="card">
                    <asp:Label ID="lbl_requested" runat="server" Text="Requisition Status"></asp:Label>
                    <div class="card-contents">
                        <center>
                             <asp:Label ID="info2" runat="server" Text=""></asp:Label>
                        </center>                      
                        <asp:GridView ID="req_stat" runat="server" AutoGenerateColumns="False" AutoPostBack="True" Width="100%" CssClass="dsplygrid" OnRowDataBound="req_stat_RowDataBound" OnRowCommand="req_stat_RowCommand">
                            <HeaderStyle HorizontalAlign="Left" />
                            <Columns>
                                <asp:BoundField DataField="REQ_ID" HeaderText="REQ #"/>
                                <asp:BoundField DataField="REQ_STATUS" HeaderText="STATUS"/>
                                <asp:BoundField DataField="REQ_DATE" HeaderText="DATE"/>
                                <asp:BoundField DataField="REQ_TIME" HeaderText="TIME"/>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button runat="server" Text="View" CommandName="ViewItem" CommandArgument='<%# Container.DataItemIndex %>' CssClass="view_btn" />
                                    </ItemTemplate>
                                    <ControlStyle BackColor="#2C3E50" ForeColor="White" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#CDF5FD" />
                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Height="58px" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#C9D1D3" ForeColor="#333333" Height="58px" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                            <AlternatingRowStyle BackColor="#F0F0F0" ForeColor="#284775" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </section>

        <%--Change Profile--%>
        <div id="cardModal" class="modal">
            <div class="modal-content">
                <span class="close" onclick="closeCardModal()">&times;</span>

                <div class="outer-box">
                    <div class="box">
                        <asp:Image ID="dsply_prof_pic" runat="server" />
                        <asp:FileUpload ID="upload_profile" runat="server" onchange="showPreview()" />
                        <label for="upload_profile" class="custom-file-upload-label">Choose Profile</label>

                        <script type="text/javascript">
                            function showPreview() {
                                var preview = document.getElementById('dsply_prof_pic');
                                var fileUpload = document.getElementById('upload_profile');

                                if (fileUpload.files && fileUpload.files[0]) {
                                    var reader = new FileReader();
                                    reader.onload = function (e) {
                                        preview.src = e.target.result;
                                    }
                                    reader.readAsDataURL(fileUpload.files[0]);
                                }
                                else {
                                    preview.src = '';
                                }
                            }
                        </script>

                        <asp:Button ID="btn_save" runat="server" Text="Save Changes" OnClick="btn_save_Click" />

                    </div>
                </div>

            </div>
        </div>
        <script>
            function openCardModal() {
                document.getElementById('cardModal').style.display = 'block';
                var modal = document.getElementById('cardModal');
                modal.classList.add('show');
            }

            function closeCardModal() {
                document.getElementById('cardModal').style.display = 'none';
            }
        </script>

        <%--Staff Info--%>
        <div id="staffcardModal" class="modal">
            <!-- Modal content goes here -->
            <div class="staff_modal-content">
                <span class="staff_close" onclick="close_staff_CardModal()">&times;</span>
                ihh
            </div>
        </div>
        <script>
            function open_staff_CardModal() {
                document.getElementById('staffcardModal').style.display = 'block';
                var modal = document.getElementById('staffcardModal');
                modal.classList.add('show');
            }

            function close_staff_CardModal() {
                document.getElementById('staffcardModal').style.display = 'none';
            }
        </script>
    </form>
</body>
</html>
