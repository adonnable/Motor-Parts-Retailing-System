<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="requisition.aspx.cs" Inherits="MotoPart.Admin_Staff_Page.requisition" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.3.0/font/bootstrap-icons.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.2/font/bootstrap-icons.min.css" />
    <link href="requisition_bootstrap.css" rel="stylesheet" />
    <link href="requisition_style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <section>
            <div class="main-content">
                <div class="card">
                    <asp:Button ID="btn_cancel_req" OnClientClick="return confirm('You have an unsaved requisition. Are you sure you want to cancel?');" runat="server" Text="Cancel Requisition" OnClick="btn_cancel_req_Click" />
                </div>

                <div class="card">
                    <asp:Label ID="lbl_pending" runat="server" Text="Requested Items"></asp:Label>
                    <div class="card1-wrapper">
                        <center>
                            <asp:Label ID="info" runat="server" Text=""></asp:Label>
                        </center>
                        <asp:GridView ID="req_items" runat="server" AutoGenerateColumns="False" AutoPostBack="True" OnRowCommand="req_items_RowCommand" Width="100%" CssClass="dsplygrid">
                            <HeaderStyle HorizontalAlign="Left" />
                            <Columns>
                                <asp:BoundField DataField="INV_ID" HeaderText="ID">
                                    <ItemStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ITEM_NAME" HeaderText="ITEM">
                                    <ItemStyle Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="INV_INSTCK" HeaderText="STK">
                                    <ItemStyle Width="55px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="ADD ITEM">
                                    <ItemTemplate>
                                        <asp:Button runat="server" Text="Add Item" CommandName="AddItem" CommandArgument='<%# Container.DataItemIndex %>' CssClass="add_item_btn" OnClientClick="return required_false();" />
                                    </ItemTemplate>
                                    <ControlStyle BackColor="#2C3E50" ForeColor="White" />
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle Height="40px" />
                            <EditRowStyle BackColor="White" ForeColor="#333333" />
                            <HeaderStyle BackColor="#336666" BorderColor="#333333" BorderStyle="Double" ForeColor="White" />
                        </asp:GridView>
                    </div>
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
                        <div class="card-3">
                            <div class="req_form-content">
                                <div class="row-add-item">
                                    <div class="text-inputs">
                                        <div class="div_unit">
                                            <asp:Label ID="lblunit" runat="server" Text="Unit"></asp:Label>
                                            <asp:TextBox ID="txt_unit" runat="server"></asp:TextBox>
                                        </div>
                                        <div>
                                            <asp:Label ID="lbldesc" runat="server" Text="Description"></asp:Label>
                                            <asp:RequiredFieldValidator ID="rv_desc" runat="server" ErrorMessage="Please fill this field" ControlToValidate="txt_desc" ValidationGroup="AddItem_Validation" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txt_desc" runat="server"></asp:TextBox>
                                        </div>
                                        <div>
                                            <asp:Label ID="lblqtty" runat="server" Text="Quantity"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txt_qtty" ValidationGroup="AddItem_Validation" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="r" ControlToValidate="txt_qtty" MinimumValue="1" MaximumValue="10000" ValidationGroup="AddItem_Validation" ForeColor="Red" Display="Static" Type="Integer"></asp:RangeValidator>
                                            <asp:TextBox ID="txt_qtty" runat="server" oninput="cal_tot_val();" TextMode="Number"></asp:TextBox>
                                        </div>
                                        <div>
                                            <asp:Label ID="lbluprice" runat="server" Text="Unit Price"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txt_price" ValidationGroup="AddItem_Validation" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txt_price" runat="server" Type="number" oninput="cal_tot_val();" placeholder="0.00"></asp:TextBox>
                                        </div>
                                        <div class="div_total">
                                            <asp:Label ID="lbltotal" runat="server" Text="Total"></asp:Label>
                                            <asp:Label ID="lbl_total" runat="server" Text=""></asp:Label>
                                        </div>
                                        <asp:Button ID="btn_new_item" runat="server" Text="Add New Item" OnClick="btn_new_item_Click" CssClass="btn btn-primary" ValidationGroup="AddItem_Validation" />
                                    </div>
                                </div>
                            </div>

                            <asp:GridView ID="selected_items" runat="server" AutoGenerateColumns="false" DataKeyNames="R_ITEM_ID" ShowFooter="true" OnRowDataBound="selected_items_RowDataBound"
                                OnRowEditing="selected_items_RowEditing" OnRowCancelingEdit="selected_items_RowCancelingEdit" OnRowUpdating="selected_items_RowUpdating" OnRowDeleting="selected_items_RowDeleting"
                                BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal">
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
                                    <asp:TemplateField HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:Label ID="label_id" runat="server" Text='<%# Eval("INV_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="UNIT">
                                        <ItemTemplate>
                                            <asp:Label ID="label_unit" runat="server" Text='<%# Eval("R_ITEM_UNIT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_unit" runat="server" Text='<%# Eval("R_ITEM_UNIT") %>' CssClass="custom-width"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="DESCRIPTION">
                                        <ItemTemplate>
                                            <asp:Label ID="label_desc" runat="server" Text='<%# Eval("R_ITEM_DESC") %>' ></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_desc" runat="server" Text='<%# Eval("R_ITEM_DESC") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="QTTY">
                                        <ItemTemplate>
                                            <asp:Label ID="label_qtty" runat="server" Text='<%# Eval("R_ITEM_QTTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_qtty" runat="server" Text='<%# Eval("R_ITEM_QTTY") %>' CssClass="custom-width"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="UNIT PRICE">
                                        <ItemTemplate>
                                            <asp:Label ID="label_price" runat="server" Text='<%# Eval("R_ITEM_UPRICE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_price" runat="server" Text='<%# Eval("R_ITEM_UPRICE") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="label_tot" runat="server" Text="TOTAL:"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="True" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="AMOUNT">
                                        <ItemTemplate>
                                            <asp:Label ID="label_amt" runat="server" Text='<%# Eval("R_ITEM_TOTAL") %>'></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="label_total" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" />
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-Width="115px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="edit_btn" runat="server" CommandName="Edit" ToolTip="Edit" CssClass="edit_btn"><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                            <asp:LinkButton ID="delete_btn" runat="server" CommandName="Delete" ToolTip="Delete" CssClass="delete_btn"><i class="bi bi-x-square-fill"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="update_btn" runat="server" CommandName="Update" ToolTip="Update" CssClass="update_btn"><i class="bi bi-floppy2-fill"></i></i></asp:LinkButton>
                                            <asp:LinkButton ID="cancel_btn" runat="server" CommandName="Cancel" ToolTip="Cancel" CssClass="cancel_btn"><i class="bi bi-x-square"></i></asp:LinkButton>
                                        </EditItemTemplate>

                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <div class="form-footer">
                                <asp:Button ID="btn_confirm_req" runat="server" Text="Create" CssClass="btn btn-primary" OnClick="btn_confirm_req_Click" OnClientClick="return showConfirmation();" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        function cal_tot_val() {
            var txtQtty = document.getElementById('<%= txt_qtty.ClientID %>');
            var txtPrice = document.getElementById('<%= txt_price.ClientID %>');
            var lblTotal = document.getElementById('<%= lbl_total.ClientID %>');

            if (txtQtty.value === "" && txtPrice.value === "") {
                lblTotal.innerText = "";
            } else if (txtQtty.value !== "" && txtPrice.value === "") {
                lblTotal.innerText = txtQtty.value;
            } else if (txtQtty.value === "" && txtPrice.value !== "") {
                lblTotal.innerText = txtPrice.value;
            } else {
                var num1 = parseInt(txtQtty.value);
                var num2 = parseFloat(txtPrice.value);

                var total = num1 * num2;
                total = total.toFixed(2);

                lblTotal.innerText = parseFloat(total).toLocaleString(undefined, {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                });

            }
        }


    </script>
</body>
</html>
