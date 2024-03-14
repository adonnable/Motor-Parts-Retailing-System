<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="receivingMemo.aspx.cs" Inherits="MotoPart.MotoParts.receivingMemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CloudTuner Garage</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.3.0/font/bootstrap-icons.css" />
    <link href="receivingMemo.css" rel="stylesheet" />
</head>
<body>
   <form id="form1" runat="server">
       <div class="container">
           <div class="main-content">
            <div class="first">
                <asp:Label ID="LblCompName" runat="server" Text="Cloud Tuner's Garage"></asp:Label> <br />
                <asp:Label ID="lblCompAdd" runat="server" Text="923 Labogon Rd, Mandaue City, Cebu"></asp:Label> <br />
                ____________________________________________________ <br />
                <asp:Label ID="lblAck" runat="server" Text="Acknolowedghement of Reciept "></asp:Label>
                <br />
                <br />
                <br />
            </div>
               <div class="align-date">
                   <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
               </div>
                <br /> <br />
            <div class="second">
                Po #:<asp:Label ID="lblPO" runat="server" Text="Label"></asp:Label>  <br />
                Supplier: <asp:Label ID="lblSupp" runat="server" Text="Label"></asp:Label> <br />
            </div>
            <br />
            <br />
            <div class="third">
                <p>I am writing to acknowledge the receipt of the said goods. I would like to confirm that the items was received in good condition.</p>
                <p>I have carefully reviewed the item and can confirm that it meets our expectations. If there are any discrepancies or issues that arise after further inspection,</p>
                <p>I will contact you promptly.
                    I appreciate your prompt delivery and attention to detail in ensuring that the item arrived safely. Thank you for your cooperation."</p>
            <br /> <br /> <br /> Al Joey Monterroyo Lopez, <br /> Purchasing Manager  
            </div>
        </div>
       </div>
        
    </form>
</body>
</html>
