<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ad_login_form.aspx.cs" Inherits="MotoPart.Admin_Page.ad_login_form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CloudTuner Garage</title>
    <meta charset="UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>  
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous"/>
    <link href="ad_login_form.css" rel="stylesheet" />
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
                
                   <header>Log In</header>
                   <div class="input-field">
                        <input type="text" class="input" id="id_email" name="email" required="" runat="server"/>
                       
                        <label for="email">Email</label> 
                    </div> 
                   <div class="input-field">
                        <input type="password" class="input" id="id_pass" name="pass" required="" runat="server"/>
                        <label for="pass">Password</label>
                    </div> 
                   <div class="input-field">                        
                       <asp:Button ID="btn_login" runat="server" Text="Log In" OnClick="btn_login_Click"/>
                   </div> 
                   <div class="signin">
                    <a href="ForgotPassword.aspx">Forgot Password</a>
                   </div>
                </div>  
            </div>
        </div>
    </div>
</div>
</form>
</body>

</html>
