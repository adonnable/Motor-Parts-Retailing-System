<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ad_log_out.aspx.cs" Inherits="MotoPart.MotoParts.ad_log_out" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CloudTuner Garage</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <link href="ad_log_out.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div class="container main">
                <div class="row">
                    <div class="col-md-6 side-image">
                    </div>
                    <div class="col-md-6 right">
                        <div class="input-box">
                            <header>Log Out</header>
                            <center><p>Are you sure you want to log out?</p></center>
                            <div class="btnss">
                                <asp:Button ID="btn_yes" runat="server" Text="Yes" class="btn btn-primary" OnClick="btn_yes_Click" />
                                <asp:Button ID="btn_no" runat="server" Text="No" class="btn btn-warning" Onclick="btn_no_Click"/>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
