
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="vetoTours._default" %>

<!DOCTYPE html>

<html>
<head  runat="server">
<meta charset="utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0"/>
<title>Veto Tours | Login</title>
<link href="Assests/css/bootstrap.min.css" rel="stylesheet"/>
<link href="Assests/font-awesome/css/font-awesome.css" rel="stylesheet"/>
<link href="Assests/css/animate.css" rel="stylesheet"/>
<link href="Assests/css/style.css" rel="stylesheet"/>
        <link href="css/plugins/iCheck/custom.css" rel="stylesheet">
     <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"/>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <script>
        $(function () {
            $("#generalDialog").dialog();
        });
    </script>
</head>

<body class="gray-bg">
<div class="middle-box text-center loginscreen animated fadeInDown">
  <div>
    <div>
      <h1 class="logo-name">VetoTours</h1>
    </div>
    <h3>Welcome to VetoTours</h3>
   
    <p>Login now and start your enriching experience with us!</p>
    <form class="m-t" role="form" id="form2" runat="server">
         <div id="Div1" title="Notification" visible="false" runat="server"></div>
      
        <div>
            <asp:TextBox CssClass="form-control" placeholder="Username" ID="txtUserName" runat="server"></asp:TextBox>
            <br />
            <asp:TextBox CssClass="form-control" placeholder="Password" TextMode="Password" ID="txtPassword" runat="server"></asp:TextBox>
            <br />
			
        <label class="control-label">Log In As</label>
            <br />
           <asp:DropDownList CssClass="form-control" runat="server" ID="userType">
                <asp:ListItem Text="User" Value="user" Selected="true" />
                <asp:ListItem Text="Admin" Value="admin"/>
            </asp:DropDownList>
            <br />
            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary block full-width m-b" Text="Login" OnClick="loginController"/>
             <div class="alert alert-danger" id="generalDialog" title="Notification" visible="false" runat="server"></div>
       
            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            <p class="text-muted text-center"><small>Do not have an account?</small></p>
             <a class="btn btn-sm btn-white btn-block" data-toggle="modal" data-target="#myModal"">Create an account</a>
        </div>
    
    <br />

	<br />
	<br />

        <div id="myModal" class="modal fade text-center animated fadeInDown col-lg-6 col-lg-offset-3" style="margin-top:120px;" role="dialog">
  <div class="modal-dialog ibox float-e-margins" >
      <div class="ibox-content">
			<h2>Registration Form   
                <button type="button" class="btn btn-default" style="float:right" data-dismiss="modal">Close</button> 
			</h2> 
            <br />
     
            <asp:TextBox ID="regUserName" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
            <br />
            <asp:TextBox ID="regPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Password"></asp:TextBox>
            <br />
            <asp:TextBox ID="regRealName" runat="server" CssClass="form-control" placeholder="Real Name"></asp:TextBox>
            <br />
            <asp:TextBox ID="regEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
            <br />
            <asp:TextBox ID="regPhone" runat="server" CssClass="form-control" placeholder="Phone Number"></asp:TextBox>
            <br />
           
            <asp:TextBox ID="regDescription" TextMode="multiline" Columns="50" Rows="3" runat="server" CssClass="form-control" placeholder="Description"></asp:TextBox>
            <br />
            <asp:Button ID="Button2" CssClass="form-control" runat="server" Text="Register" OnClick="registrationController"/>
            <br />
            <asp:Label ID="regStatus" runat="server" Text=""></asp:Label>
          </div>
       </div>
</div>
   
     
      </form>
  </div>
</div>

<!-- Mainly scripts --> 
<!-- By Nurul Hilda Binte Mohd Rahim --> 
<script src="Assests/js/jquery-2.1.1.js"></script> 
<script src="Assests/js/bootstrap.min.js"></script>
</body>
<script type="text/javascript">
	$("#form").submit(function(){
		var loginAs=$('#loginAs').val();
	if(loginAs=="Admin")	{
		    $('#form').attr('action', "main.aspx").submit();
		}else{
		 $('#form').attr('action', "main.aspx").submit();
		 }
});
</script>
</html>
