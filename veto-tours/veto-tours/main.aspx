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
        $( "#tabs" ).tabs();
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
        <div>
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>

        </div>
        <% if(Session["loggedIn"] == "true")
            {%>
                <div id="dialog" title="Authentication">
                  <p>You have successfully logged into the server! Feel free to browse.</p>
                </div>

                <br />
                <br />

                <div id="accordion">
                    <h3>How to Filter</h3>
                    <div>
                        <p> You can choose to sort the way the book catalogue is presented to you based on the following presets:</p>
                        <p>Genre:</p>
                        <p>Sorts the tables based on Genre in alphabatical order.</p>
                        <p>Price:</p>
                        <p>Sorts the tables based on lowest price to highest price.</p>
                        <p>Author:</p>
                        <p>Sorts the tables based on Author's name.</p>


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
                
                <br />
                <br />

                <div id="tabs">
                  <ul>
                    <li><a href="#tabs-1">Add a new Book</a></li>
                    <li><a href="#tabs-2">Edit a Book</a></li>
                  </ul>
                  <div id="tabs-1">
                    <h1>Add a new Book</h1>
                    <asp:Label ID="Label1" runat="server" Text="Book ID    "></asp:Label>
                    <asp:TextBox ID="add_bookID" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="Book Name  "></asp:Label>
                    <asp:TextBox ID="add_bookName" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="Book Genre "></asp:Label>
                    <asp:TextBox ID="add_bookGenre" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label5" runat="server" Text="Book Author"></asp:Label>
                    <asp:TextBox ID="add_bookAuthor" runat="server"></asp:TextBox>
                    <br />
                    <p>
                      <label for="addPrice">Set Price $:</label>
                      <input id="addPrice" name="value" runat="server"/>
                    </p>
                    <br />
                    <br />
                    <asp:Button ID="btnAddBook" runat="server" Text="Add new Book" OnClick="btnAddBook_Click"/>
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


            <%}
        %>



    </form>





</body>
</html>
