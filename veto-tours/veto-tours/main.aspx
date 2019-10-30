<!-- Nicholas Leung Jun Yen-->
<!-- UOW ID: 5987325-->

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="vetoTours.main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

  <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"/>
  <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
  <script>
      $( function() {
          $("#tabs").tabs();
          $("#touristTabs").tabs();
          $("#tourGuideTabs").tabs();
      });


      $(function () {
          $("#spinner").spinner({
              spin: function (event, ui) {
                  if (ui.value > 99) {
                      $(this).spinner("value", 0);
                      return false;
                  } else if (ui.value < 0) {
                      $(this).spinner("value", 99);
                      return false;
                  }
              }
          });
      });

      $(function () {
          $("#addPrice").spinner({
              spin: function (event, ui) {
                  if (ui.value > 99) {
                      $(this).spinner("value", 0);
                      return false;
                  } else if (ui.value < 0) {
                      $(this).spinner("value", 99);
                      return false;
                  }
              }
          });
      });

      $(function () {
          $("#dialog").dialog();
      });

      $(function () {
          $("#accordion").accordion();
      });


  </script>

</head>
<body>
    <form id="form1" runat="server">

        <% if(Session["loggedIn"] == "true" && Session["userType"] == "user")
            {%>
		        <div>
					 <asp:GridView ID="GridView1" runat="server"></asp:GridView>
				</div>

                <div id="dialog" title="Authentication">
                  <p>You have successfully logged into the server! Feel free to browse.</p>
                </div>
            
                                

                <br />
                <br />

                 <!--
                <div id="accordion">
                    <h3>How to Filter</h3>
                    <div>
                        <p> Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat</p>
                        <p>Category A:</p>
                        <p>Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur</p>
                        <p>Category B:</p>
                        <p>xcepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
                        <p>Category C:</p>
                        <p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium</p>


                    </div>
                    <h3>Filter Settings</h3>
                    <div>
                        <asp:RadioButtonList ID="rdoFilter" runat="server" RepeatLayout="Flow">
                            <asp:ListItem Value="Genre">Genre</asp:ListItem>
                            <asp:ListItem Value="Price">Price</asp:ListItem>
                            <asp:ListItem Value="Author">Author</asp:ListItem>
                        </asp:RadioButtonList>

                        <br />
                        <asp:Button ID="filterButton" runat="server" Text="Filter Books" OnClick="filterBooks_Click"/>

                    </div>
                </div>
                 -->
                
                <br />
                <br />
                <h1>Tour Guide View</h1>
                <div id ="tourGuideTabs">
                    <ul>
                        <li><a href="#createdTours">My Created Tours</a></li>
                        <li><a href="#createTour">Create a Tour</a></li>
                        <li><a href="#editTour">Modify existing Tour</a></li>
                    </ul>
                    <div id ="createdTours">
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
                        <asp:Label ID="cTStartDate" runat="server" Text="Start Date (YYYY-MM-DD)"></asp:Label>
                        <asp:TextBox ID="createStartDate" runat="server"></asp:TextBox>
                        <br />
                        <asp:Label ID="cTEndDate" runat="server" Text="End Date (YYYY-MM-DD)"></asp:Label>
                        <asp:TextBox ID="createEndDate" runat="server"></asp:TextBox>
                        <br />
                        <asp:Label ID="cTDuration" runat="server" Text="Duration (HH:MM:SS)"></asp:Label>
                        <asp:TextBox ID="createDuration" runat="server"></asp:TextBox>
                        <br />
                        <asp:Label ID="cTPrice" runat="server" Text="Price (eg. 14.50)"></asp:Label>
                        <asp:TextBox ID="createPrice" runat="server"></asp:TextBox>
                        <br />
                        <asp:Label ID="cTStatus" runat="server" Text="Status (open/closed)"></asp:Label>
                        <asp:TextBox ID="createStatus" runat="server"></asp:TextBox>
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
                        <asp:Label ID="eDStartDate" runat="server" Text="Start Date (YYYY-MM-DD)"></asp:Label>
                        <asp:TextBox ID="editStartDate" runat="server"></asp:TextBox>
                        <br />
                        <asp:Label ID="eDEndDate" runat="server" Text="End Date (YYYY-MM-DD)"></asp:Label>
                        <asp:TextBox ID="editEndDate" runat="server"></asp:TextBox>
                        <br />
                        <asp:Label ID="eDDuration" runat="server" Text="Duration (HH:MM:SS)"></asp:Label>
                        <asp:TextBox ID="editDuration" runat="server"></asp:TextBox>
                        <br />
                        <asp:Label ID="eDPrice" runat="server" Text="Price (eg. 14.50)"></asp:Label>
                        <asp:TextBox ID="editPrice" runat="server"></asp:TextBox>
                        <br />
                        <asp:Label ID="eDStatus" runat="server" Text="Status (open/closed)"></asp:Label>
                        <asp:TextBox ID="editStatus" runat="server"></asp:TextBox>
                        <br />
                        <asp:Button ID="editTourButton" runat="server" Text="Edit Tour" OnClick="editTour_Click"/>
                        <br />
                        <asp:Label ID="outcome" runat="server" Text=""></asp:Label>

                    </div>
                </div>

                <!--
                <div id="tabs">
                  <ul>
                    <li><a href="#tabs-1">Add a new Tour</a></li>
                    <li><a href="#tabs-2">Edit a Tour</a></li>
                  </ul>
                  <div id="tabs-1">
                    <h1>Add a new tour</h1>
                    <asp:Label ID="Label1" runat="server" Text="tour ID    "></asp:Label>
                    <asp:TextBox ID="add_bookID" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="tour Name  "></asp:Label>
                    <asp:TextBox ID="add_bookName" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="Country "></asp:Label>
                    <asp:TextBox ID="add_bookGenre" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label5" runat="server" Text="Location "></asp:Label>
                    <asp:TextBox ID="add_bookAuthor" runat="server"></asp:TextBox>
                    <br />
                    <p>
                      <label for="addPrice">Set Price $:</label>
                      <input id="addPrice" name="value" runat="server"/>
                    </p>
                    <br />
                    <br />
                    <asp:Button ID="btnAddBook" runat="server" Text="Add new Tour" OnClick="btnAddBook_Click"/>
                    <br />
                    <br />
                  </div>
                  <div id="tabs-2">
                    <h2>Edit a book</h2>
                    <asp:Label ID="Label6" runat="server" Text="Book ID to Update"></asp:Label>
                    <asp:TextBox ID="target_bookID" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label7" runat="server" Text="New Book Name"></asp:Label>
                    <asp:TextBox ID="new_bookName" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label8" runat="server" Text="New Book Genre"></asp:Label>
                    <asp:TextBox ID="new_bookGenre" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label10" runat="server" Text="New Book Author"></asp:Label>
                    <asp:TextBox ID="new_bookAuthor" runat="server"></asp:TextBox>
                    <br />
                    <p>
                      <label for="spinner">Set New Price $:</label>
                      <input id="spinner" name="value" runat="server"/>
                    </p>
                    <br />
                    <br />
                    <asp:Button ID="btnEditBook" runat="server" Text="Edit Book" OnClick="btnEditBook_Click"/>
                    <br />
                  </div>

                </div>
                -->

            <%}
        %>

		<% else if(Session["loggedIn"] == "true" && Session["userType"] == "admin")
            {%>
				<h1> ADMIN PAGE COMING SOON </h1>

		    <%}
        %>


    </form>





</body>
</html>
