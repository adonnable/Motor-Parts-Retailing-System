<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Staff_details.aspx.cs" Inherits="MotoPart.Admin_Page.Staff_details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.2/font/bootstrap-icons.min.css" />
    <link href="Staff_details.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div class="container main">
                <div class="row">
                    <div class="column-flex">
                        <div class="cancel_div">
                            <asp:LinkButton ID="linkbtn_cancel" runat="server" OnClick="linkbtn_cancel_Click"><i class="bi bi-x-square-fill"></i></asp:LinkButton>
                        </div>
                        <div class="row-flex">
                            <asp:Label ID="lbl_name" runat="server" Text="Staff Information"></asp:Label>

                            <asp:GridView ID="dsply_staff" runat="server" AutoGenerateColumns="False" AutoPostBack="True" Width="100%" CssClass="dsplygrid"
                                OnRowDataBound="dsply_staff_RowDataBound">
                            <HeaderStyle HorizontalAlign="Left" />
                          
                            
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Image ID="prof_img" runat="server" Text='<%# Eval("STAFF_PROFILE_PIC") %>' CssClass="prof_size" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="280px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_staffName" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="250px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_staffemail" runat="server" Text='<%# Eval("STAFF_EMAIL") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="250px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_staffaddress" runat="server" Text='<%# Eval("STAFF_ADDRESS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="250px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_staffcnum" runat="server" Text='<%# Eval("STAFF_CNUM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="250px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_staffstatus" runat="server" Text='<%# Eval("STAFF_STATUS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="250px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_staffcreated" runat="server" Text='<%# Eval("STAFF_CREATED") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                            <AlternatingRowStyle BackColor="#F0F0F0" ForeColor="#284775" />
                        </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
