<!-- Nicholas Leung Jun Yen-->
<!-- UOW ID: 5987325-->

<%@ Page Language="C#" MaintainScrollPositionOnPostBack="true" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="vetoTours.main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

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
          $("#dialog").dialog();
          $("#dialog_suspended").dialog();
          $("#errorDialog").dialog();
          $("#errorDialogAdmin").dialog();
      });

      $(function () {
          $("#accordion").accordion();
      });


  </script>

</head>
<body>
    <asp:Label ID="nameLabel" runat="server" Text=""></asp:Label>
    <form id="form1" runat="server">

        <% if(Session["loggedIn"] == "true" && Session["userType"] == "user" && Session["status"] == "normal")
            {%>

                <asp:Button ID="btnUserLogout" runat="server" Text="Logout" OnClick="logout_Click"/>

                <div id="dialog" title="Authentication">
                  <p>You have successfully logged into the server! Feel free to browse.</p>
                </div>
                <div id="test" runat="server"></div>
                <div id="errorDialog" title="Error" visible="false" runat="server"></div>

                <br />
                <br />
                <div id="viewTabs">
                    <ul>
                        <li><a href="#tourGuideTabs">Tour Guide Mode</a></li>
                        <li><a href="#touristTabs">Tourist Mode</a></li>
                        <li><a href="#personalMessagesTab">My Inbox</a></li>
                        <li><a href="#modifyProfile">Edit Profile</a></li>
                    </ul>
                    <div id ="tourGuideTabs">
                        <h2>Tour Guide Mode</h2>
                        <ul>
                            <li><a href="#createdTours">My Created Tours</a></li>
                            <li><a href="#createTour">Create a Tour</a></li>
                            <li><a href="#editTour">Modify existing Tour</a></li>
                            <li><a href="#rateTourist">Rate a Tourist</a></li>
                        </ul>
                        <div id ="createdTours" style="max-height:500px; overflow-y:auto">
                            <h2>My Created Tours</h2>
                            <asp:GridView ID="createdToursView" runat="server"></asp:GridView>
                        </div>

                        <div id ="createTour">
                            <h2>Create a Tour</h2>
                            <asp:Label ID="cTName" runat="server" Text="Tour Name"></asp:Label>
                            <asp:TextBox ID="createTourName" runat="server"></asp:TextBox>
                            <br />
                            <asp:Label ID="cTCapacity" runat="server" Text="Capacity"></asp:Label>
                            <asp:TextBox ID="createCapacity" runat="server"></asp:TextBox>
                            <br />
                            <asp:Label ID="cTLocation" runat="server" Text="Location"></asp:Label>
                            <asp:TextBox ID="createLocation" runat="server"></asp:TextBox>
                            <br />
                            <asp:Label ID="cTDescription" runat="server" Text="Description"></asp:Label>
                            <asp:TextBox ID="createDescription" runat="server"></asp:TextBox>
                            <br />
                            <asp:Label ID="cTStartDate" runat="server" Text="Start Date (YYYY-MM-DD HH:MM:SS)"></asp:Label>
                            <asp:TextBox ID="createStartDate" runat="server"></asp:TextBox>
                            <br />
                            <asp:Label ID="cTEndDate" runat="server" Text="End Date (YYYY-MM-DD HH:MM:SS)"></asp:Label>
                            <asp:TextBox ID="createEndDate" runat="server"></asp:TextBox>
                            <br />
                            <asp:Label ID="cTPrice" runat="server" Text="Price (eg. 14.50)"></asp:Label>
                            <asp:TextBox ID="createPrice" runat="server"></asp:TextBox>
                            <br />
                            <asp:Label ID="cTStatus" runat="server" Text="Status (open/closed)"></asp:Label>
                            <asp:DropDownList ID="ddCreateStatus" runat="server">
                                <asp:ListItem Selected="True" Value="open">Open</asp:ListItem>
                                <asp:ListItem Value="closed">Closed</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:Button ID="createTourButton" runat="server" Text="Create Tour" OnClick="createTour_Click"/>
                        </div>

                        <div id ="editTour">
                            <h2>Edit a Tour</h2>
                            <asp:Label ID="eDID" runat="server" Text="ID of Tour to Edit"></asp:Label>
                            <asp:TextBox ID="editID" runat="server"></asp:TextBox>
                            <br />
                            <h2>Editable Details:</h2>
                            <asp:Label ID="eDName" runat="server" Text="Tour Name"></asp:Label>
                            <asp:TextBox ID="editName" runat="server"></asp:TextBox>
                            <br />
                            <asp:Label ID="eDCapacity" runat="server" Text="Capacity"></asp:Label>
                            <asp:TextBox ID="editCapacity" runat="server"></asp:TextBox>
                            <br />
                            <asp:Label ID="eDLocation" runat="server" Text="Location"></asp:Label>
                            <asp:TextBox ID="editLocation" runat="server"></asp:TextBox>
                            <br />
                            <asp:Label ID="eDDescription" runat="server" Text="Description"></asp:Label>
                            <asp:TextBox ID="editDescription" runat="server"></asp:TextBox>
                            <br />
                            <asp:Label ID="eDStartDate" runat="server" Text="Start Date (YYYY-MM-DD HH:MM:SS)"></asp:Label>
                            <asp:TextBox ID="editStartDate" runat="server"></asp:TextBox>
                            <br />
                            <asp:Label ID="eDEndDate" runat="server" Text="End Date (YYYY-MM-DD HH:MM:SS)"></asp:Label>
                            <asp:TextBox ID="editEndDate" runat="server"></asp:TextBox>
                            <br />
                            <asp:Label ID="eDPrice" runat="server" Text="Price (eg. 14.50)"></asp:Label>
                            <asp:TextBox ID="editPrice" runat="server"></asp:TextBox>
                            <br />
                            <asp:Label ID="eDStatus" runat="server" Text="Status (open/closed)"></asp:Label>
                            <asp:DropDownList ID="ddEditStatus" runat="server">
                                <asp:ListItem Selected="True" Value="open">Open</asp:ListItem>
                                <asp:ListItem Value="closed">Closed</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:Button ID="editTourButton" runat="server" Text="Edit Tour" OnClick="editTour_Click"/>
                            <br />
                            <asp:Label ID="outcome" runat="server" Text=""></asp:Label>
                        </div>

                        <div id="rateTourist">
                            <h2>Rate a Tourist</h2>
                            <asp:Label ID="touristlbl" runat="server" Text="Tourist ID"></asp:Label>
                            <asp:TextBox ID="rateTouristID" runat="server"></asp:TextBox>
                            <br />
                            <label for="setRatingTourist">Stars</label>
                            <input id="setRatingTourist" name="ratingValue" runat="server" />
                            <br />
                            <asp:Button ID="btnSetRatingTourist" runat="server" Text="Give Rating" OnClick="giveRatingTourist_Click"/>
                        </div>

                    </div>

                    <div id ="touristTabs">
                        <h2>Tourist Mode</h2>
                        <ul>
                            <li><a href="#availableTours">Available Tours</a></li>
                            <li><a href="#bookedTours">My Upcoming Booked Tours</a></li>
                            <li><a href="#bookingHistory">My Booking History</a></li>
                            <li><a href="#makeBooking">Create a Booking</a></li>
                            <li><a href="#rateTourGuide">Rate a Tour Guide</a></li>
                        </ul>
                        <div id ="availableTours">
                            <h2>Available Tours</h2>
                            <div style="max-height:500px; overflow-y:auto">
                                <asp:GridView ID="availableToursView" runat="server"/>
                            </div>
                            <asp:Label ID="filterTourslbl" runat="server" Text="Filter Tour"></asp:Label>
                            <asp:DropDownList ID="ddFilterTour" runat="server">
                                <asp:ListItem Value="Price">By Price</asp:ListItem>
                                <asp:ListItem Value="Rating">By Rating</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:Label ID="ascdsc" runat="server" Text="Criteria"></asp:Label>
                            <asp:DropDownList ID="ddFilterCriteria" runat="server">
                                <asp:ListItem Value="Ascending">Ascending</asp:ListItem>
                                <asp:ListItem Value="Descending">Descending</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:Button ID="btnFilterTours" runat="server" Text="Filter" OnClick="filterTours_Click"/>

                        </div>

                        <div id ="bookedTours">
                            <h2>My Upcoming Booked Tours</h2>
                            <asp:GridView ID="bookedToursView" runat="server"></asp:GridView>
                        </div>

                        <div id ="bookingHistory">
                            <h2>My Booking History</h2>
                            <asp:GridView ID="bookingHistoryView" runat="server"></asp:GridView>
                        </div>

                        <div id="makeBooking">
                            <h2>Create a Booking</h2>
                            <asp:Label ID="cTBooking" runat="server" Text="ID of Tour"></asp:Label>
                            <asp:TextBox ID="createBooking" runat="server"></asp:TextBox>
                            <br />
                            <asp:Button ID="createBookingButton" runat="server" Text="Create Booking" OnClick="createBooking_Click"/>

                        </div>

                        <div id="rateTourGuide">
                            <h2>Rate a TourGuide</h2>
                            <asp:Label ID="rateTGName" runat="server" Text="Tour Guide ID"></asp:Label>
                            <asp:TextBox ID="rateTourGuideID" runat="server"></asp:TextBox>
                            <br />
                            <label for="setRating">Stars</label>
                            <input id="setRating" name="ratingValue" runat="server" />
                            <br />
                            <asp:Button ID="btnGiveRating" runat="server" Text="Give Rating" OnClick="giveRatingTourGuide_Click"/>

                        </div>


                    </div>

                    <div id ="personalMessagesTab">
                        <h2>My Inbox</h2>
                        <ul>
                            <li><a href="#pmInbox">My Inbox</a></li>
                            <li><a href="#createMessage">Compose Message</a></li>
                        </ul>
                        <div id ="pmInbox" runat="server">
                            
                            
                        </div>

                        <div id ="createMessage">
                            <asp:Label ID="labelSendTo" runat="server" Text="To: "></asp:Label>
                            <asp:TextBox ID="sendTo" runat="server"></asp:TextBox>
                            <br />
                            <asp:Label ID="labelSubject" runat="server" Text="Subject: "></asp:Label>
                            <asp:TextBox ID="msgSubject" runat="server"></asp:TextBox>
                            <br />
                            <asp:Label ID="labelMsg" runat="server" Text="Message: "></asp:Label>
                            <br />
                            <asp:TextBox ID="msgField" runat="server" TextMode="MultiLine" Width="400px" Height="200px"></asp:TextBox>
                            <br />
                            <asp:Button ID="sendMsgBtn" runat="server" Text="Send Message" OnClick="sendMsg_Click"/>
                        </div>
                    </div>

                    <div id="modifyProfile">
                        <h2>Current Profile Details</h2>
                        <asp:GridView ID="myProfileView" runat="server"></asp:GridView>
                        <br />
                        <h2>Editable Fields</h2>
                        <asp:Label ID="eDPhone" runat="server" Text="Phone Number"></asp:Label>
                        <asp:TextBox ID="newPhoneNumber" runat="server"></asp:TextBox>
                        <br />
                        <asp:Label ID="eDDesc" runat="server" Text="Description"></asp:Label>
                        <asp:TextBox ID="newDescription" runat="server"></asp:TextBox>
                        <br />
                        <asp:Button ID="editProfile" runat="server" Text="Edit Details" OnClick="editProfile_Click"/>
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
                <div id="errorDialogAdmin" title="Error" visible="false" runat="server"></div>
				<h1> ADMIN PAGE</h1>
                <br />
                <asp:Button ID="btnAdminLogout" runat="server" Text="Logout" OnClick="logout_Click"/>
                <div id ="adminTabs">
                    <ul>
                        <li><a href="#editUser">Edit User</a></li>
                        <li><a href="#createUser">Create a User</a></li>
                        <li><a href="#suspendUser">Suspend a User</a></li>
                        
                    </ul>
                    <div id ="editUser">
                        <div style="max-height:500px; overflow-y:auto">
                            <asp:GridView ID="editUserView" runat="server"></asp:GridView>
                        </div>
                        <br />
                        <h2>Edit a User Profile</h2>
                        <asp:Label ID="eDUID" runat="server" Text="ID of User to Edit"></asp:Label>
                        <asp:TextBox ID="editUserID" runat="server"></asp:TextBox>
                        <br />
                        <h2>Editable Details:</h2>
                        <asp:Label ID="eDPW" runat="server" Text="Password"></asp:Label>
                        <asp:TextBox ID="editPassword" runat="server"></asp:TextBox>
                        <br />
                        <asp:Label ID="eDRN" runat="server" Text="Real Name"></asp:Label>
                        <asp:TextBox ID="editRealName" runat="server"></asp:TextBox>
                        <br />
                        <asp:Label ID="eDEM" runat="server" Text="Email"></asp:Label>
                        <asp:TextBox ID="editEmail" runat="server"></asp:TextBox>
                        <br />
                        <asp:Label ID="eDPN" runat="server" Text="Phone Number"></asp:Label>
                        <asp:TextBox ID="editPhone" runat="server"></asp:TextBox>
                        <br />
                        <asp:Label ID="eDDE" runat="server" Text="Description"></asp:Label>
                        <asp:TextBox ID="editDesc" runat="server"></asp:TextBox>
                        <br />
                        <asp:Label ID="eDST" runat="server" Text="Status(0/1)"></asp:Label>
                        <asp:TextBox ID="editStat" runat="server"></asp:TextBox>
                        <br />
                        <asp:Button ID="editUserButton" runat="server" Text="Edit User Profile" OnClick="editUser_Click"/>
                        <br />
                    </div>

                    <div id="createUser">
                        <h2>Create a New User</h2>
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
                        <asp:Label ID="regStat" runat="server" Text="Status(0/1)"></asp:Label>
                        <asp:TextBox ID="regStatus" runat="server"></asp:TextBox>
                        <br />
                        <asp:Button ID="btnCreateUser" runat="server" Text="Create New User" OnClick="btnCreateUser_Click"/>
                        <br />                       
                    </div>

                    <div id="suspendUser">
                        <h2>Suspend a User</h2>
                        <asp:Label ID="suspendUserLabel" runat="server" Text="Enter ID of user to suspend"></asp:Label>
                        <asp:TextBox ID="suspendUserField" runat="server"></asp:TextBox>
                        <br />
                        <asp:Button ID="btnSuspend" runat="server" Text="Suspend User" OnClick="btnSuspendUser_Click"/>

                    </div>

                 </div>

		    <%}
        %>


    </form>





</body>
</html>
