
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="vetoTours._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"/>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#generalDialog").dialog();
        });
    </script>
</head>
<body>
    <h1>Veto Tours Authentication System</h1>
    <br />
    <form id="form1" runat="server">
        <div id="generalDialog" title="Notification" visible="false" runat="server"></div>
      
        <div>
            <asp:Label ID="Label1" runat="server" Text="UserName"></asp:Label>
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
            <br />
			<asp:Label ID="Label3" runat="server" Text="Login as"></asp:Label>
           <asp:DropDownList runat="server" ID="userType">
                <asp:ListItem Text="User" Value="user" Selected="true" />
                <asp:ListItem Text="Admin" Value="admin"/>
            </asp:DropDownList>
            <br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click"/>
            <br />
            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
        </div>
    
    <br />

	<br />
	<br />

        <div>
			<h2>Registration Form</h2>
            <asp:Label ID="regName" runat="server" Text="UserName"></asp:Label>
            <asp:TextBox ID="regUserName" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="regPw" runat="server" Text="Password"></asp:Label>
            <asp:TextBox ID="regPassword" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="regRn" runat="server" Text="Real Name"></asp:Label>
            <asp:TextBox ID="regRealName" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="regEm" runat="server" Text="Email"></asp:Label>
            <asp:TextBox ID="regEmail" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="regPh" runat="server" Text="Phone Number"></asp:Label>
            <asp:TextBox ID="regPhone" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="regDesc" runat="server" Text="Description"></asp:Label>
            <asp:TextBox ID="regDescription" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click"/>
            <br />
            <asp:Label ID="regStatus" runat="server" Text=""></asp:Label>
        </div>
	</form>

</body>
</html>
