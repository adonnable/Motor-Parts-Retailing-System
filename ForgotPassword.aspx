<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="MotoPart.MotoParts.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>CloudTuner Garage</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.2/font/bootstrap-icons.min.css" />
    <link href="AddSupplier_design.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div class="container main">
                <div class="row">
                    <div class="back-div">
                        <asp:LinkButton ID="btn_back" runat="server" OnClick="btn_back_Click" ><i class="bi bi-x-square-fill"></i></asp:LinkButton>
                    </div>
                    <div class="col-md-6 side-image">
                    </div>
                    <div class="col-md-6 right">
                        <div class="input-box">

                            <header>Forgot Password</header>
                            <div class="input-field">
                                <input type="text" class="input" id="cName" name="cName" required="" autocomplete="off" runat="server" />
                                <label for="cName">E-mail address</label>
                            </div>
                            <div class="input-field">
                                <input type="password" class="input" id="cAddress" name="cAddress" required="" autocomplete="off" runat="server"/>
                                <label for="cAddress">New Password</label>
                            </div>
                            <div class="input-field">
                                <input type="password" class="input" id="cCnum" name="cCnum" required="" autocomplete="off" runat="server"/>
                                <label for="cCnum">Confirm Password</label>
                            </div>
                           


                            <div class="input-field">
                                <asp:Button class="btn btn-primary" runat="server" Text="Save Changes" OnClick="change_Click" CssClass="register_btn" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

