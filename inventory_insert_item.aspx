<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="inventory_insert_item.aspx.cs" Inherits="MotoPart.MotoParts.inventory_insert_item" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CloudTuner Garage</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.3.0/font/bootstrap-icons.css" />
    <link href="inventory_insert_item.css" rel="stylesheet" />
    <link href="bootstrap_style.css" rel="stylesheet" />
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
                                    <header>Insert Item</header>

                                    <div class="input-field">
                                        <input type="text" class="input" id="id_unit" name="unit" required="" autocomplete="off" runat="server"/>
                                        <label for="fname">Unit</label>
                                    </div>
                                    <div class="input-field">
                                        <input type="text" class="input" id="id_item" name="item" required="" autocomplete="off" runat="server"/>
                                        <label for="lname">Item Name</label>
                                    </div>

                                    <div class="input-field">
                                        <input type="number" class="input" id="id_qtty" name="qtty" min="1" required="" autocomplete="off" runat="server"/>
                                        <label for="address">Quantity</label>
                                    </div>
                                    <div class="input-field">
                                        <input type="number" class="input" id="id_uprice" name="uprice" min="1" required="" autocomplete="off" runat="server"/>
                                        <label for="cnum">Unit Price</label>
                                    </div>
                                                                  
                                    <div class="input-field">
                                        <asp:Button ID="btn_insert" runat="server" Text="Insert Item" CssClass="btn btn-primary" Onclick="btn_insert_Click"/>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="cardModal" class="modal">
            <div class="modal-content">
                <p>Do you want to insert another requisition?</p>
                <div class="btn-ap">
                    <asp:Button ID="btn_yes" runat="server" Text="Yes" Onclick="btn_yes_Click" CssClass="btn btn-primary" />
                    <asp:Button ID="btn_no" runat="server" Text="No" Onclick="btn_no_Click" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
        <script>
            function openCardModal() {
                document.getElementById('cardModal').style.display = 'block';
            }
        </script>
    </form>
</body>
</html>
