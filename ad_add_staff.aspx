<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ad_add_staff.aspx.cs" Inherits="MotoPart.Admin_Page.ad_add_staff" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <link href="ad_add_staff_style.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.2/font/bootstrap-icons.min.css" />
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
                            <div class="col-md-6 side-image">
                            </div>
                            <div class="col-md-6 right">
                                <div class="input-box">
                                    <header>Add Account</header>
                                    <div class="d-flex">
                                        <div class="input-field">
                                            <input type="text" class="input" id="fname" name="fname" required="" autocomplete="off" />
                                            <label for="fname">First Name</label>
                                        </div>
                                        <div class="input-field">
                                            <input type="text" class="input" id="lname" name="lname" required="" autocomplete="off" />
                                            <label for="lname">Last Name</label>
                                        </div>
                                    </div>
                                    <div class="input-field">
                                        <input type="text" class="input" id="address" name="address" required="" autocomplete="off" />
                                        <label for="address">Address</label>
                                    </div>
                                    <div class="input-field">
                                        <input type="text" class="input" id="cnum" name="cnum" required="" autocomplete="off" />
                                        <label for="cnum">Contact Number</label>
                                    </div>
                                    <div class="input-field">
                                        <input type="text" class="input" id="email" name="email" required="" autocomplete="off" />
                                        <label for="email">Email</label>
                                    </div>
                                    <div class="d-flex">
                                        <div class="input-field">
                                            <input type="password" class="input" id="pass" name="pass" required="" runat="server"/>
                                            <label for="repass">Password</label>
                                        </div>
                                       
                                        <div class="input-field">
                                            <input type="password" class="input" id="repass" name="repass" required="" runat="server" />
                                            <label for="repass">Confirm Password</label>
                                        </div>
                                         <asp:CompareValidator ID="CompareValidator1" ControlToCompare="repass" ControlToValidate="pass" runat="server" ErrorMessage="*" ForeColor="Red"></asp:CompareValidator>
                                    </div>

                                    <asp:RegularExpressionValidator ID="regexPasswordValidator" runat="server"
    ControlToValidate="pass" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$"
    ErrorMessage="Password must be at least 8 characters long and contain a mix of uppercase, lowercase, numbers, and special characters."
    ForeColor="Red" Display="Dynamic" />

                                    <div class="input-field">
                                        <asp:Button ID="btn_create" runat="server" Text="Create Account" OnClick="btn_create_Click" />
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
