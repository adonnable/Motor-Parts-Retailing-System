<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveRequest.aspx.cs" Inherits="MotoPart.MotoParts.ApproveRequest" %>

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
    <link href="ApproveRequest.css" rel="stylesheet" />
</head>
<body>
   <form id="form1" runat="server">

        <section>
            <div class="main-content">
                <div class="card-btn">
                    <asp:Button ID="btn_cancel" runat="server" Text="X" OnClick="btn_cancel_Click" />
                </div>
                <div class="card">
                    <asp:Label ID="lbl_form" runat="server" Text="Requisition Form"></asp:Label>
                    <div class="req-form">
                        <div class="card-2">
                            <div class="f-row">
                                <div>
                                    <asp:Label ID="lbl_reqnum" runat="server" Text="Requisition #:"></asp:Label>
                                    <asp:Label ID="req_num" runat="server" Text=""></asp:Label>
                                </div>
                                <div>
                                    <asp:Label ID="lbl_date" runat="server" Text="Date:"></asp:Label>
                                    <asp:TextBox ID="txt_date" runat="server" type="date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="s-row">
                                <div class="t-row">
                                    <asp:Label ID="req_name" runat="server" Text="Requestor's Name: "></asp:Label>
                                    <asp:Label ID="reqname_lbl" runat="server" Text=""></asp:Label>
                                </div>
                                <asp:Label ID="Label1" runat="server" Text="PLEASE PURCHASE THE FOLLOWING ITEMS:"></asp:Label>
                            </div>
                        </div>

                        <asp:GridView ID="selected_items" runat="server" AutoGenerateColumns="false"
                            BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal">
                            <FooterStyle BackColor="White" ForeColor="#333333" />
                            <FooterStyle BackColor="White" ForeColor="#333333" />
                            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="Black" />
                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="White" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                            <SortedAscendingHeaderStyle BackColor="#487575" />
                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                            <SortedDescendingHeaderStyle BackColor="#275353" />

                            <Columns>
                                <asp:BoundField DataField="INV_ID" HeaderText="INV ID"></asp:BoundField>
                                <asp:BoundField DataField="R_ITEM_UNIT" HeaderText="UNIT"></asp:BoundField>
                                <asp:BoundField DataField="R_ITEM_DESC" HeaderText="DESCRIPTION"></asp:BoundField>
                                <asp:BoundField DataField="R_ITEM_QTTY" HeaderText="QTTY"></asp:BoundField>
                                <asp:BoundField DataField="R_ITEM_UPRICE" HeaderText="UNIT PRICE"></asp:BoundField>
                                <asp:BoundField DataField="R_ITEM_TOTAL" HeaderText="TOTAL"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <div class="align-total">
                            <asp:Label ID="lbl_tot" runat="server" Text=""></asp:Label>
                        </div>       

                        <div class="approveDeny">
                        <asp:Button ID="A_button" runat="server" Text="Approve" CssClass="btn btn-primary" Onclick="A_button_Click"/>  
                        <asp:Button ID="D_button" runat="server" Text="Deny" CssClass="btn btn-warning" Onclick="D_button_Click"/>
                    </div>
                    </div>
                </div>
            </div>
        </section>

    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
