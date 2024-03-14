<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditQuantity.aspx.cs" Inherits="MotoPart.MotoParts.EditQuantity" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title></title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <link href="EditQuantity.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="box">
                <asp:Button ID="backBtn" class="btn btn-danger btn-close" runat="server" Text="" OnClick="backBtn_Click" />
                <table>
                    <tr>
                        <th>Item Unit</th>
                        <th>Item Description</th>
                        <th>QTTY</th>
                    </tr>
                    <tr>
                        <td><asp:Label ID="Label1" runat="server" class="label" Text=""></asp:Label></td>
                        <td><asp:Label ID="Label2" runat="server" class="label" Text=""></asp:Label></td>
                        <td><asp:Label ID="Label4" runat="server" class="label" Text=""></asp:Label></td>
                    </tr>
                </table>
                <div class="getQuantity">
                    <asp:Label ID="Label3" runat="server" Text="Number of Lacking Item/s: "></asp:Label>
                    <asp:TextBox ID="lackingQty" runat="server" ></asp:TextBox>
                </div>
                <div class="button-container">
                    <asp:Button ID="Submit" OnClientClick="return confirm('Are you sure? (Update Confirmation)');" runat="server" class="button button-primary" Text="Submit" OnClick="Submit_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
