<!-- Nicholas Leung Jun Yen-->
<!-- UOW ID: 5987325-->

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="task4._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Bookshop Authentication System</h1>
    <br />
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="UserName"></asp:Label>
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click"/>
            <br />
            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
        </div>
    </form>
    <br />
    <h3>Please use user1 as the Username and password1 as the password in order to login</h3>

</body>
</html>
