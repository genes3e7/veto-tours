<!-- Nicholas Leung Jun Yen-->
<!-- UOW ID: 5987325-->

<%@ Page Language="C#" MaintainScrollPositionOnPostBack="true" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="vetoTours.main" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Veto Tours | Dashboard</title>

    <link href="Assests/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="Assests/font-awesome/css/font-awesome.css" rel="stylesheet"/>

    <!-- Toastr style -->
    
    <link href="Assests/css/plugins/toastr/toastr.min.css" rel="stylesheet"/>

    <!-- Gritter -->
    <link href="Assests/js/plugins/gritter/jquery.gritter.css" rel="stylesheet"/>

    <link href="Assests/css/animate.css" rel="stylesheet"/>
    <link href="Assests/css/style.css" rel="stylesheet"/>
   <!-- FooTable -->
    <link href="Assests/css/plugins/footable/footable.core.css" rel="stylesheet"/>
  <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"/>
  <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
  <script>
      $(function () {
          $("#viewTabs").tabs();
          $("#tabs").tabs();
          $("#touristTabs").tabs();
          $("#tourGuideTabs").tabs();
          $("#personalMessagesTab").tabs();
          $("#adminTabs").tabs();
      });


      $(function () {
          $("#setRatingTourist").spinner({
              spin: function (event, ui) {
                  if (ui.value > 5) {
                      $(this).spinner("value", 0);
                      return false;
                  } else if (ui.value < 0) {
                      $(this).spinner("value", 5);
                      return false;
                  }
              }
          });

          $("#setRatingTourist").bind("keydown", function (event) {
              event.preventDefault();
          });
      });

      $(function () {
          $("#setRating").spinner({
              spin: function (event, ui) {
                  if (ui.value > 5) {
                      $(this).spinner("value", 0);
                      return false;
                  } else if (ui.value < 0) {
                      $(this).spinner("value", 5);
                      return false;
                  }
              }
          });

          $("#setRating").bind("keydown", function (event) {
              event.preventDefault();
          });
      });

      $(function () {
          $("#dialog_suspended").dialog();
          $("#general_dialog").dialog();
          $("#errorDialogAdmin").dialog();
          $("#adminDialog").dialog();

      });

      $(function () {
          $("#successfulDialog").dialog();
      });
      $(function () {
          $("#accordion").accordion();
      });


  </script>

</head>
<body runat="server">
    
     <form id="form2" runat="server">
         <div id="general_dialog" title="Notification" visible="false" runat="server"></div>

       <% if(Session["loggedIn"] == "true" && Session["userType"] == "user" && Session["status"] == "normal")
            {%>
         
        <div id="wrapper">
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

                    <div id="Div2" title="Error" visible="false" runat="server"></div>
				
        <nav class="navbar-default navbar-static-side" role="navigation">
            <div class="sidebar-collapse">
                <ul class="nav metismenu" id="side-menu">
                    <li class="nav-header">
                        <div class="dropdown profile-element"> 
                            <a href="main.aspx">
                            <span class="clear"> <span class="block m-t-xs"> <strong class="font-bold">USER PAGE</strong>
                             </span></a>
                            
                        </div>
                        <div class="logo-element">
                            &nbsp;
                        </div>
                    </li>
                    <li class="active">
                        <a href="main.aspx"><i class="fa fa-th-large"></i> <span class="nav-label">Dashboard</span></a>
                        
                    </li>
                     <li>
                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="logout_Click">Logout  <i class="fa fa-sign-out"></i> </asp:LinkButton>

                </li>
                        
                   
                </ul>

            </div>
        </nav>

        <div id="page-wrapper" class="gray-bg dashbard-1">
        <div class="row border-bottom">
        <nav class="navbar navbar-static-top" role="navigation" style="margin-bottom: 0">
        <div class="navbar-header">
            <a class="navbar-minimalize minimalize-styl-2 btn btn-primary " href="#"><i class="fa fa-bars"></i> </a>
            
        </div>
            <ul class="nav navbar-top-links navbar-right">
                <li>
                    <span class="m-r-sm text-muted welcome-message">Welcome to Dash Board.</span>
                </li>
                
                


                <li>
                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="logout_Click">Logout  <i class="fa fa-sign-out"></i> </asp:LinkButton>

                </li>
                <li>
                    <a class="right-sidebar-toggle">
                        <i class="fa fa-tasks"></i>
                    </a>
                </li>
            </ul>

        </nav>
        </div>
            <div id="Div3" title="Error" visible="false" runat="server"></div>
				 
                
        <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-content">
                                     <div id="viewTabs">
                    <ul>
                        <li><a href="#tourGuideTabs" class="btn">Tour Guide Mode</a></li>
                        <li><a href="#touristTabs" class="btn">Tourist Mode</a></li>
                        <li><a href="#personalMessagesTab" class="btn">My Inbox</a></li>
                        <li><a href="#modifyProfile" class="btn">Edit Profile</a></li>
                    </ul>
                    <div id ="tourGuideTabs">
                        <h2>Tour Guide Mode</h2>
                        <ul>
                            <li><a href="#createdTours" class="btn">My Created Tours</a></li>
                            <li><a href="#createTour" class="btn">Create a Tour</a></li>
                            <li><a href="#editTour" class="btn">Modify existing Tour</a></li>
                            <li><a href="#rateTourist" class="btn">Rate a Tourist</a></li>
                        </ul>
                        <div id ="createdTours" style="max-height:500px; overflow-y:auto">
                            <h2>My Created Tours</h2>
                            <asp:GridView ID="createdToursView" CssClass="table table-bordered table-hover table-stripped table-condensed" runat="server">
                                 <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </div>

                        <div id ="createTour">
                            <br />
                            <h2>Create a Tour</h2>
                            <br />
                            <div class="form-group">
                            <asp:Label ID="cTName" Cssclass="col-sm-2 control-label" runat="server" Text="Tour Name"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="createTourName" runat="server"></asp:TextBox>
                            </div>
                            </div><br /> <div class="hr-line-dashed"></div>
                             <div class="form-group">
                            <asp:Label ID="cTCapacity" Cssclass="col-sm-2 control-label" runat="server" Text="Capacity"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="createCapacity" runat="server"></asp:TextBox>
                            </div>
                            </div><br /> <div class="hr-line-dashed"></div>
                               <div class="form-group">
                            <asp:Label ID="cTLocation" Cssclass="col-sm-2 control-label" runat="server" Text="Location"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="createLocation" runat="server"></asp:TextBox>
                            </div>
                            </div><br /> <div class="hr-line-dashed"></div>
                                <div class="form-group">
                            <asp:Label ID="cTDescription" Cssclass="col-sm-2 control-label" runat="server" Text="Description"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="createDescription" runat="server"></asp:TextBox>
                            </div>
                            </div><br /> <div class="hr-line-dashed"></div>
                             <div class="form-group">
                            <asp:Label ID="cTStartDate" Cssclass="col-sm-2 control-label" runat="server" Text="Start Date (YYYY-MM-DD HH:MM:SS)"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="createStartDate" TextMode="DateTime" runat="server"></asp:TextBox>
                            </div>
                            </div><br /> <div class="hr-line-dashed"></div>
                            <div class="form-group">
                            <asp:Label ID="cTEndDate" Cssclass="col-sm-2 control-label" runat="server" Text="End Date (YYYY-MM-DD HH:MM:SS)"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="createEndDate" TextMode="DateTime" runat="server"></asp:TextBox>
                            </div>
                            </div><br /> <div class="hr-line-dashed"></div>
                          
                            <div class="form-group">
                            <asp:Label ID="cTPrice" Cssclass="col-sm-2 control-label" runat="server" Text="Price (eg. 14.50)"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="createPrice" runat="server"></asp:TextBox>
                            </div>
                            </div><br /> <div class="hr-line-dashed"></div>
                              <div class="form-group">
                            <asp:Label ID="cTStatus" Cssclass="col-sm-2 control-label" runat="server" Text="Status (open/closed)"></asp:Label>
                            <div class="col-sm-10">
                                <asp:DropDownList Cssclass="col-sm-2 control-label" ID="ddCreateStatus" runat="server">
                                 <asp:ListItem Selected="True" Value="open">Open</asp:ListItem>
                                 <asp:ListItem Value="closed">Closed</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                            </div><br /> 

                         
                            <br />
                            <asp:Button ID="createTourButton" CssClass="btn btn-primary block m-b" runat="server" Text="Create Tour" OnClick="tourCreationController"/>
                        </div>

                        <div id ="editTour">
                            <h2>Edit a Tour</h2>
                            <br />
                             <div class="form-group">
                            <asp:Label ID="eDID" Cssclass="col-sm-2 control-label" runat="server" Text="ID of Tour to Edit"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="editID" runat="server"></asp:TextBox>
                            </div>
                        </div>
                       <br />
                        <br />
                            <h2>Editable Details:</h2>
                            <br />
                             <div class="form-group">
                            <asp:Label ID="eDName" Cssclass="col-sm-2 control-label" runat="server" Text="Tour Name"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="editName" runat="server"></asp:TextBox>
                            </div>
                            </div><br /> <div class="hr-line-dashed"></div>
                              <div class="form-group">
                            <asp:Label ID="eDCapacity" Cssclass="col-sm-2 control-label" runat="server" Text="Capacity"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="editCapacity" runat="server"></asp:TextBox>
                            </div>
                            </div><br /> <div class="hr-line-dashed"></div>
                            
                           <div class="form-group">
                            <asp:Label ID="eDLocation" Cssclass="col-sm-2 control-label" runat="server" Text="Location"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="editLocation" runat="server"></asp:TextBox>
                            </div>
                            </div><br /> <div class="hr-line-dashed"></div>
                              <div class="form-group">
                            <asp:Label ID="eDDescription" Cssclass="col-sm-2 control-label" runat="server" Text="Description"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="editDescription" runat="server"></asp:TextBox>
                            </div>
                            </div><br /> <div class="hr-line-dashed"></div>

                               <div class="form-group">
                            <asp:Label ID="eDStartDate" Cssclass="col-sm-2 control-label" runat="server" Text="Start Date (YYYY-MM-DD HH:MM:SS)"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="editStartDate" TextMode="DateTime" runat="server"></asp:TextBox>
                            </div>
                            </div><br /> <div class="hr-line-dashed"></div>
                           
                           
                           <div class="form-group">
                            <asp:Label ID="eDEndDate" Cssclass="col-sm-2 control-label" runat="server" Text="End Date (YYYY-MM-DD HH:MM:SS)"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="editEndDate" TextMode="DateTime" runat="server"></asp:TextBox>
                            </div>
                            </div><br /> <div class="hr-line-dashed"></div>

                           <div class="form-group">
                            <asp:Label ID="eDPrice" Cssclass="col-sm-2 control-label" runat="server" Text="Price (eg. 14.50)"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="editPrice" runat="server"></asp:TextBox>
                            </div>
                            </div><br /> <div class="hr-line-dashed"></div>
                            <div class="form-group">
                            <asp:Label ID="eDStatus" Cssclass="col-sm-2 control-label" runat="server" Text="Status"></asp:Label>
                            <div class="col-sm-10">
                                 <asp:DropDownList Cssclass="col-sm-2 control-label" ID="ddEditStatus" runat="server">
                                <asp:ListItem Selected="True" Value="open">Open</asp:ListItem>
                                <asp:ListItem Value="closed">Closed</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                            </div><br /> <div class="hr-line-dashed"></div>
                         
                            <br />
                            <asp:Button ID="editTourButton" CssClass="btn btn-primary block m-b" runat="server" Text="Edit Tour" OnClick="tourEditingController"/>
                            <br />
                            <asp:Label ID="outcome" runat="server" Text=""></asp:Label>
                        </div>

                        <div id="rateTourist">
                            <h2>Rate a Tourist</h2>
                            <br />
                           
                             <div class="form-group">
                            <asp:Label ID="touristlbl" Cssclass="col-sm-2 control-label" runat="server" Text="Tourist Username"></asp:Label>
                           <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="rateTouristID" runat="server"></asp:TextBox>
                            </div>
                            </div>
                          
                            <br /><div class="hr-line-dashed"></div>

                             <div class="form-group">
                            <asp:Label ID="starslbl" Cssclass="col-sm-2 control-label" runat="server" Text="Stars"></asp:Label>
                           <div class="col-sm-10">
                              <asp:DropDownList ID="ddTouristStars" runat="server">
                                <asp:ListItem Selected="True" Value="0">0</asp:ListItem>
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                            </div>
                            <br />
                            <br />
                            <asp:Button ID="btnSetRatingTourist" CssClass="btn btn-primary block m-b"  runat="server" Text="Give Rating" OnClick="giveRatingTouristController"/>
                        </div>

                    </div>

                    <div id ="touristTabs">
                        <h2>Tourist Mode</h2>
                        <ul>
                            <li><a href="#availableTours"  class="btn">Available Tours</a></li>
                            <li><a href="#bookedTours" class="btn">My Upcoming Booked Tours</a></li>
                            <li><a href="#bookingHistory" class="btn">My Booking History</a></li>
                            <li><a href="#makeBooking" class="btn">Create a Booking</a></li>
                            <li><a href="#rateTourGuide" class="btn">Rate a Tour Guide</a></li>
                        </ul>
                        <div id ="availableTours">
                            <h2>Available Tours</h2>
                            <div style="max-height:500px; overflow-y:auto">
                                <asp:GridView ID="availableToursView" CssClass="table table-bordered table-hover table-stripped table-condensed" runat="server">
                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                            </div>
                             <div class="form-group">
                            <asp:Label ID="filterTourslbl" Cssclass="col-sm-2 control-label"  runat="server" Text="Filter Tour"></asp:Label>
                           
                            <div class="col-sm-10">
                               <asp:DropDownList ID="ddFilterTour" runat="server">
                                <asp:ListItem  Value="Price">By Price</asp:ListItem>
                                <asp:ListItem Value="Rating">By Rating</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                            </div><br /> <div class="hr-line-dashed"></div>
                            
                            <div class="form-group">
                             <asp:Label ID="ascdsc" runat="server" Cssclass="col-sm-2 control-label"  Text="Criteria"></asp:Label>
                           
                            <div class="col-sm-10">
                              <asp:DropDownList ID="ddFilterCriteria" runat="server">
                                <asp:ListItem Value="Ascending">Ascending</asp:ListItem>
                                <asp:ListItem Value="Descending">Descending</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                            </div><br /> 
                            
                            <br />
                            <asp:Button ID="btnFilterTours" CssClass="btn btn-primary block m-b" runat="server" Text="Filter" OnClick="filterToursController"/>

                        </div>

                        <div id ="bookedTours">
                            <h2>My Upcoming Booked Tours</h2>
                            <asp:GridView ID="bookedToursView" CssClass="table table-bordered table-hover table-stripped table-condensed" runat="server">
                                 <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </div>

                        <div id ="bookingHistory">
                            <h2>My Booking History</h2>
                            <asp:GridView ID="bookingHistoryView" CssClass="table table-bordered table-hover table-stripped table-condensed" runat="server">
                                 <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </div>

                        <div id="makeBooking">
                            <h2>Create a Booking</h2>
                            <br />
                             <div class="form-group">
                            
                            <asp:Label ID="cTBooking"  Cssclass="col-sm-2 control-label" runat="server" Text="ID of Tour"></asp:Label>
                           
                            <div class="col-sm-10">
                           
                            <asp:TextBox ID="createBooking" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                            </div><br /> 

                            <br />
                            <asp:Button ID="createBookingButton" CssClass="btn btn-primary block m-b" runat="server" Text="Create Booking" OnClick="bookingController"/>

                        </div>

                        <div id="rateTourGuide">
                            <h2>Rate a TourGuide</h2>
                            <br />
                            <div class="form-group">
                            <asp:Label ID="rateTGName" Cssclass="col-sm-2 control-label" runat="server" Text="Tour Guide Username"></asp:Label>
                            <div class="col-sm-10">
                                   <asp:TextBox CssClass="form-control" ID="rateTourGuideID" runat="server"></asp:TextBox>
                        
                            </div>
                            </div><br /> <div class="hr-line-dashed"></div>
                         
                            <div class="form-group">
<asp:Label ID="tgStarslbl" Cssclass="col-sm-2 control-label"  runat="server" Text="Stars"></asp:Label>
                            <div class="col-sm-10">
                                   <asp:DropDownList ID="ddTourGuideStars" runat="server">
                                <asp:ListItem Selected="True" Value="0">0</asp:ListItem>
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                            </div><br /> 
                         
                            
                           
                            <br />
                            <asp:Button ID="btnGiveRating"  CssClass="btn btn-primary block m-b" runat="server" Text="Give Rating" OnClick="giveRatingTourGuideController"/>

                        </div>


                    </div>

                    <div id ="personalMessagesTab">
                        <h2>My Inbox</h2>
                        <ul>
                            <li><a href="#pmInbox" class="btn">My Inbox</a></li>
                            <li><a href="#createMessage" class="btn">Compose Message</a></li>
                        </ul>
                        <div id ="pmInbox" runat="server">
                            
                            
                        </div>

                        <div id ="createMessage">

                            <div class="form-group">
                            <asp:Label ID="labelSendTo" Cssclass="col-sm-2 control-label" runat="server" Text="To: "></asp:Label>
                            <div class="col-sm-10">
                                   <asp:TextBox CssClass="form-control" ID="sendTo" runat="server"></asp:TextBox>
                        
                            </div>
                            </div><br /> <div class="hr-line-dashed"></div>

                              <div class="form-group">
                            <asp:Label ID="labelSubject" Cssclass="col-sm-2 control-label" runat="server" Text="Subject: "></asp:Label>
                            <div class="col-sm-10">
                                   <asp:TextBox CssClass="form-control" ID="msgSubject" runat="server"></asp:TextBox>
                        
                            </div>
                            </div><br /> <div class="hr-line-dashed"></div>
                          
                            <div class="form-group">
                            <asp:Label ID="labelMsg" Cssclass="col-sm-2 control-label" runat="server" Text="Message: "></asp:Label>
                            <div class="col-sm-10">
                                   <asp:TextBox CssClass="form-control" ID="msgField" TextMode="MultiLine" Width="400px" Height="200px" runat="server"></asp:TextBox>
                        
                            </div>
                            </div><br /> 
                            <br />
                            <asp:Button ID="sendMsgBtn" CssClass="btn btn-primary block m-b"  runat="server" Text="Send Message" OnClick="sendMsgController"/>
                        </div>
                    </div>

                    <div id="modifyProfile">
                        <h2>Current Profile Details</h2>
                        <br />

                        <asp:GridView ID="myProfileView" CssClass="table table-bordered table-hover table-stripped table-condensed"  runat="server">
                             <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                        <br />
                        <h2>Editable Fields</h2>
                        <br />
                        <div class="form-group">
                            <asp:Label ID="eDPhone" Cssclass="col-sm-2 control-label" runat="server" Text="Phone Number"></asp:Label>
                            <div class="col-sm-10">
                                   <asp:TextBox CssClass="form-control" ID="newPhoneNumber" runat="server"></asp:TextBox>
                        
                            </div>
                            </div><br /> <div class="hr-line-dashed"></div>
                         <div class="form-group">
                            <asp:Label ID="eDDesc" Cssclass="col-sm-2 control-label" runat="server" Text="Description"></asp:Label>
                            <div class="col-sm-10">
                                   <asp:TextBox CssClass="form-control" TextMode="MultiLine" Width="400px" Height="200px" ID="newDescription" runat="server"></asp:TextBox>
                        
                            </div>
                            </div><br />
                        <br />
                        <asp:Button ID="editProfile" CssClass="btn btn-primary block m-b" runat="server" Text="Edit Details" OnClick="editProfileController"/>
                    </div>

                </div>

                        </div>
                    </div>
                </div>
            </div>
                
                </div>

        </div>
        
        
        
    </div>

            <%}
        %>
     <% else if(Session["loggedIn"] == "true" && Session["userType"] == "user" && Session["status"] == "suspended")
                {%>

                 <div id="dialog_suspended" title="Account Suspended">
                  <p>Your Account has been suspended, please contact support for more information!</p>
                </div>


                <%}
        %>
    <% else if(Session["loggedIn"] == "true" && Session["userType"] == "admin")
            {%>

        <div id="wrapper">
    <asp:Label ID="nameLabel" runat="server" Text=""></asp:Label>

                    <div id="Div1" title="Error" visible="false" runat="server"></div>
				 <div id="Div4" title="Notification" visible="false" runat="server"></div>

        <nav class="navbar-default navbar-static-side" role="navigation">
            <div class="sidebar-collapse">
                <ul class="nav metismenu" id="side-menu">
                    <li class="nav-header">
                        <div class="dropdown profile-element"> 
                            <a href="main.aspx">
                            <span class="clear"> <span class="block m-t-xs"> <strong class="font-bold">ADMIN PAGE</strong>
                             </span></a>
                            
                        </div>
                        <div class="logo-element">
                            &nbsp;
                        </div>
                    </li>
                    <li class="active">
                        <a href="main.aspx"><i class="fa fa-th-large"></i> <span class="nav-label">Dashboard</span></a>
                        
                    </li>
                     <li>
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="logout_Click">Logout  <i class="fa fa-sign-out"></i> </asp:LinkButton>

                </li>
                        
                   
                </ul>

            </div>
        </nav>

        <div id="page-wrapper" class="gray-bg dashbard-1">
        <div class="row border-bottom">
        <nav class="navbar navbar-static-top" role="navigation" style="margin-bottom: 0">
        <div class="navbar-header">
            <a class="navbar-minimalize minimalize-styl-2 btn btn-primary " href="#"><i class="fa fa-bars"></i> </a>
            
        </div>
            <ul class="nav navbar-top-links navbar-right">
                <li>
                    <span class="m-r-sm text-muted welcome-message">Welcome to Dash Board.</span>
                </li>
                
                


                <li>
                    <asp:LinkButton ID="Button6" runat="server" OnClick="logout_Click">Logout  <i class="fa fa-sign-out"></i> </asp:LinkButton>

                </li>
                <li>
                    <a class="right-sidebar-toggle">
                        <i class="fa fa-tasks"></i>
                    </a>
                </li>
            </ul>

        </nav>
        </div>

            <div id="adminDialog" title="Notification" visible="false" runat="server"></div>
				
                
        <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-content">
                                    <div id="adminTabs">
                    <ul>
                        <li><a href="#editUser" class="btn">Edit User</a></li>
                        <li><a href="#createUser" class="btn">Create a User</a></li>
                        <li><a href="#suspendUser" class="btn">Suspend a User</a></li>
                        
                    </ul>
                    <div id="editUser">
                       
                        <br />
                        <h2>Edit a User Profile</h2>
                        <br />
                        <div class="form-group">
                            <asp:Label ID="eDUID" Cssclass="col-sm-2 control-label" runat="server" Text="ID of User to Edit"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="editUserID" runat="server"></asp:TextBox>
                            </div>
                        </div>
                       <br />
                        <br />
                        <h2>Editable Details:</h2>  <br />
                        <div class="form-group">
                            <asp:Label ID="eDPW" Cssclass="col-sm-2 control-label" runat="server" Text="Password"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" TextMode="Password" ID="editPassword" runat="server"></asp:TextBox>
                            </div>
                        </div><br />
                           <div class="hr-line-dashed"></div>
                        <div class="form-group">
                            <asp:Label ID="eDRN" Cssclass="col-sm-2 control-label" runat="server" Text="Real Name"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control"  ID="editRealName" runat="server"></asp:TextBox>
                            </div>
                        </div><br />
                           <div class="hr-line-dashed"></div>
                        <div class="form-group">
                            <asp:Label ID="eDEM" Cssclass="col-sm-2 control-label" runat="server" Text="Email"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="editEmail" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        
                        <br />
                         <div class="hr-line-dashed"></div>
                        <div class="form-group">
                            <asp:Label ID="eDPN" Cssclass="col-sm-2 control-label" runat="server" Text="Phone Number"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="editPhone" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    
                        <br />
                         <div class="hr-line-dashed"></div>
                        <div class="form-group">
                            <asp:Label ID="eDDE" Cssclass="col-sm-2 control-label" runat="server" Text="Description"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="editDesc" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                         <div class="hr-line-dashed"></div>
                        <div class="form-group">
                               <asp:Label ID="eDST" Cssclass="col-sm-2 control-label" runat="server" Text="Status(0/1)"></asp:Label>
                            <div class="col-sm-10">
                                 <asp:DropDownList Cssclass="col-sm-2 control-label" ID="ddEditStat" runat="server">
                                     <asp:ListItem Selected="True" Value="0">0</asp:ListItem>
                                     <asp:ListItem Value="1">1</asp:ListItem>
                                 </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                         <div class="hr-line-dashed"></div>
                        <asp:Button ID="editUserButton" CssClass="btn btn-primary block m-b" runat="server" Text="Edit User Profile" OnClick="adminEditUserController"/>
                        <br />

                        <br />
                        
                        <div style="max-height:500px; overflow-y:auto">
                            <asp:GridView ID="editUserView" CssClass="table table-bordered table-hover table-stripped table-condensed" data-page-size="8" runat="server">
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </div>
                    </div>

                    <div id="createUser">
                        <h2>Create a New User</h2>
                        <br />
                          <div class="form-group">
                            <asp:Label ID="regName" Cssclass="col-sm-2 control-label" runat="server" Text="UserName"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="regUserName" runat="server"></asp:TextBox>
                            </div>
                        </div><br />
                           <div class="hr-line-dashed"></div>
                        <div class="form-group">
                            <asp:Label ID="regPw" Cssclass="col-sm-2 control-label" runat="server" Text="Password"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" TextMode="Password" ID="regPassword" runat="server"></asp:TextBox>
                            </div>
                        </div><br />
                           <div class="hr-line-dashed"></div>
                          <div class="form-group">
                            <asp:Label ID="regRn" Cssclass="col-sm-2 control-label" runat="server" Text="Real Name"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="regRealName" runat="server"></asp:TextBox>
                            </div>
                        </div><br />
                           <div class="hr-line-dashed"></div>
                         <div class="form-group">
                            <asp:Label ID="regEm" Cssclass="col-sm-2 control-label" runat="server" Text="Email"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="regEmail" runat="server"></asp:TextBox>
                            </div>
                        </div><br />
                           <div class="hr-line-dashed"></div>
                       
                      
                        <div class="form-group">
                            <asp:Label ID="regPh" Cssclass="col-sm-2 control-label" runat="server" Text="Phone Number"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="regPhone" runat="server"></asp:TextBox>
                            </div>
                        </div><br />
                           <div class="hr-line-dashed"></div>
                       
                       <div class="form-group">
                            <asp:Label ID="regDesc" Cssclass="col-sm-2 control-label" runat="server"  Text="Description"></asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox CssClass="form-control" ID="regDescription" TextMode="multiline" runat="server"></asp:TextBox>
                            </div>
                        </div><br />
                           <div class="hr-line-dashed"></div>
                         <div class="form-group">
                            <asp:Label ID="regStat" Cssclass="col-sm-2 control-label" runat="server" Text="Status"></asp:Label>
                            <div class="col-sm-10">
                                <asp:DropDownList Cssclass="col-sm-2 control-label" ID="ddRegStat" runat="server">
                                     <asp:ListItem Selected="True" Value="0">0</asp:ListItem>
                                     <asp:ListItem Value="1">1</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div><br />
                           <div class="hr-line-dashed"></div>
                       
                        <asp:Button ID="btnCreateUser" CssClass="btn btn-primary block m-b" runat="server" Text="Create New User" OnClick="adminCreaterUserController"/>
                        <br />                       
                    </div>

                    <div id="suspendUser">
                        <h2>Suspend a User</h2>
                        <br />
                         <div class="form-group">
                            <asp:Label ID="suspendUserLabel" Cssclass="col-sm-3 control-label" runat="server" Text="Enter ID of user to suspend"></asp:Label>
                            <div class="col-sm-8">
                                <asp:TextBox CssClass="form-control" ID="suspendUserField" runat="server"></asp:TextBox>
                            </div>
                        </div><br />
                           <div class="hr-line-dashed"></div>
                      
                        <asp:Button ID="btnSuspend" CssClass="btn btn-primary block m-b" runat="server" Text="Suspend User" OnClick="adminSuspendUserController"/>

                    </div>

                 </div>
                     
                        </div>
                    </div>
                </div>
            </div>
                
                </div>

        </div>
        
        
        
    </div>
          <%}
        %>








         </form>
    

    


 <!-- Mainly scripts -->
 <!-- By Nurul Hilda Binte Mohd Rahim --> 
    <script src="Assests/js/jquery-2.1.1.js"></script>
    <script src="Assests/js/bootstrap.min.js"></script>
    <script src="Assests/js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="Assests/js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

    <!-- Flot -->
    <script src="Assests/js/plugins/flot/jquery.flot.js"></script>
    <script src="Assests/js/plugins/flot/jquery.flot.tooltip.min.js"></script>
    <script src="Assests/js/plugins/flot/jquery.flot.spline.js"></script>
    <script src="Assests/js/plugins/flot/jquery.flot.resize.js"></script>
    <script src="Assests/js/plugins/flot/jquery.flot.pie.js"></script>

    <!-- Peity -->
    <script src="Assests/js/plugins/peity/jquery.peity.min.js"></script>
    <script src="Assests/js/demo/peity-demo.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="Assests/js/inspinia.js"></script>
    <script src="Assests/js/plugins/pace/pace.min.js"></script>

    <!-- jQuery UI -->
    <script src="Assests/js/plugins/jquery-ui/jquery-ui.min.js"></script>

    <!-- GITTER -->
    <script src="Assests/js/plugins/gritter/jquery.gritter.min.js"></script>

    <!-- Sparkline -->
    <script src="Assests/js/plugins/sparkline/jquery.sparkline.min.js"></script>

    <!-- Sparkline demo data  -->
    <script src="Assests/js/demo/sparkline-demo.js"></script>

    <!-- ChartJS-->
    <script src="Assests/js/plugins/chartJs/Chart.min.js"></script>

    <!-- Toastr -->
    <script src="Assests/js/plugins/toastr/toastr.min.js"></script>
 <!-- FooTable -->
    <script src="Assests/js/plugins/footable/footable.all.min.js"></script>
    <!-- Page-Level Scripts -->
   
 <!-- iCheck -->
    <script src="Assests/js/plugins/iCheck/icheck.min.js"></script>
        
    
    <script>
         
    <!-- Not in use--> 
        $(document).ready(function () {
            setTimeout(function() {
                toastr.options = {
                    closeButton: true,
                    progressBar: true,
                    showMethod: 'slideDown',
                    timeOut: 4000
                };
                toastr.success('Dashboard Console', 'Welcome to VetoTours');

            }, 1300);

            var data1 = [
                [0,4],[1,8],[2,5],[3,10],[4,4],[5,16],[6,5],[7,11],[8,6],[9,11],[10,30],[11,10],[12,13],[13,4],[14,3],[15,3],[16,6]
            ];
            var data2 = [
                [0,1],[1,0],[2,2],[3,0],[4,1],[5,3],[6,1],[7,5],[8,2],[9,3],[10,2],[11,1],[12,0],[13,2],[14,8],[15,0],[16,0]
            ];
            $("#flot-dashboard-chart").length && $.plot($("#flot-dashboard-chart"), [
                data1, data2
            ],
                    {
                        series: {
                            lines: {
                                show: false,
                                fill: true
                            },
                            splines: {
                                show: true,
                                tension: 0.4,
                                lineWidth: 1,
                                fill: 0.4
                            },
                            points: {
                                radius: 0,
                                show: true
                            },
                            shadowSize: 2
                        },
                        grid: {
                            hoverable: true,
                            clickable: true,
                            tickColor: "#d5d5d5",
                            borderWidth: 1,
                            color: '#d5d5d5'
                        },
                        colors: ["#1ab394", "#1C84C6"],
                        xaxis:{
                        },
                        yaxis: {
                            ticks: 4
                        },
                        tooltip: false
                    }
            );

            var doughnutData = [
                {
                    value: 300,
                    color: "#a3e1d4",
                    highlight: "#1ab394",
                    label: "App"
                },
                {
                    value: 50,
                    color: "#dedede",
                    highlight: "#1ab394",
                    label: "Software"
                },
                {
                    value: 100,
                    color: "#A4CEE8",
                    highlight: "#1ab394",
                    label: "Laptop"
                }
            ];

            var doughnutOptions = {
                segmentShowStroke: true,
                segmentStrokeColor: "#fff",
                segmentStrokeWidth: 2,
                percentageInnerCutout: 45, 
                animationSteps: 100,
                animationEasing: "easeOutBounce",
                animateRotate: true,
                animateScale: false
            };

            var ctx = document.getElementById("doughnutChart").getContext("2d");
            var DoughnutChart = new Chart(ctx).Doughnut(doughnutData, doughnutOptions);

            var polarData = [
                {
                    value: 300,
                    color: "#a3e1d4",
                    highlight: "#1ab394",
                    label: "App"
                },
                {
                    value: 140,
                    color: "#dedede",
                    highlight: "#1ab394",
                    label: "Software"
                },
                {
                    value: 200,
                    color: "#A4CEE8",
                    highlight: "#1ab394",
                    label: "Laptop"
                }
            ];

            var polarOptions = {
                scaleShowLabelBackdrop: true,
                scaleBackdropColor: "rgba(255,255,255,0.75)",
                scaleBeginAtZero: true,
                scaleBackdropPaddingY: 1,
                scaleBackdropPaddingX: 1,
                scaleShowLine: true,
                segmentShowStroke: true,
                segmentStrokeColor: "#fff",
                segmentStrokeWidth: 2,
                animationSteps: 100,
                animationEasing: "easeOutBounce",
                animateRotate: true,
                animateScale: false
            };
            var ctx = document.getElementById("polarChart").getContext("2d");
            var Polarchart = new Chart(ctx).PolarArea(polarData, polarOptions);

        });
    </script>
        

</body>
</html>
