// Nicholas Leung Jun Yen
// UOW ID: 5987325

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient; 
using System.Configuration; 

namespace vetoTours
{
    public partial class main : System.Web.UI.Page
    {
        user currUser;
        admin currAdmin;
        protected void Page_Load(object sender, EventArgs e)
        {
           if (Session["loggedIn"] == "true" && Session["userType"] == "user" && Session["status"] == "normal")
           {
                currUser = fetchUserObject(Session["userID"].ToString());
                nameLabel.Text = "Hello " + currUser.getName();

                // Check if page refresh was due to a successful action
                if(Session["success"] == "giveRating")
                {
                    general_dialog.InnerHtml = "You have successfully rated the user";
                    general_dialog.Visible = true;
                    Session["success"] = "";
                }

                else if (Session["success"] == "msgSent")
                {
                    general_dialog.InnerHtml = "You have successfully sent a message";
                    general_dialog.Visible = true;
                    Session["success"] = "";
                }


                SqlConnection conn = null;
                SqlCommand cmd = null;
                SqlDataReader reader = null;


                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());

                conn.Open();

                // Pull Tour Guides Created Tours
                currUser.getCreatedTours(createdToursView);
                conn.Close();


               

                // Fetch available Tours
                List<tour> availableTours = new List<tour>();
                availableTours = fetchTours();

                if (Session["filterType"] == "Price" && Session["criteria"] == "Ascending")
                    availableTours.Sort((x, y) => x.getPrice().CompareTo(y.getPrice()));

                else if (Session["filterType"] == "Price" && Session["criteria"] == "Descending")
                    availableTours.Sort((x, y) => -1 * x.getPrice().CompareTo(y.getPrice()));

                else if (Session["filterType"] == "Rating" && Session["criteria"] == "Ascending")
                    availableTours.Sort((x, y) => x.fetchTourGuideRating().CompareTo(y.fetchTourGuideRating()));

                else if (Session["filterType"] == "Rating" && Session["criteria"] == "Descending")
                    availableTours.Sort((x, y) => -1 * x.fetchTourGuideRating().CompareTo(y.fetchTourGuideRating()));

                var _bind = from a in availableTours
                                select new
                                {
                                    Tour_ID = a.getTourID(),
                                    Created_By = a.getUserID(),
                                    Rating = a.fetchTourGuideRating(),
                                    Tour_Name = a.getTourName(),
                                    Tour_Capacity = a.getCapacity(),
                                    Tour_Location = a.getLocation(),
                                    Tour_Description = a.getTourDescription(),
                                    Start_Date = a.getStartDate(),
                                    End_Date = a.getEndDate(),
                                    Price = a.getPrice(),
                                    Status = a.getStatus()
                                };



                    availableToursView.DataSource = _bind;
                    availableToursView.DataBind();



               

                // Pull all booked tours that have yet to start
                currUser.getUpcomingBookings(bookedToursView);

                // Pull booking history where the events have ended
                currUser.getBookingHistory(bookingHistoryView);

                // Pull User Profile Information
                currUser.getProfileDetails(myProfileView);

                // Fetch user Inbox
                List<chat> allMessages = new List<chat>();
                allMessages = fetchMessages();
                pmInbox.InnerHtml = "";

                foreach (chat msg in allMessages)
                {
                    pmInbox.InnerHtml += ("Sender: " + msg.getSender() + "<br/>" + "Time Sent:" + msg.getDateTime().ToString() +"<br/> Subject: " + msg.getSubject() + "<br/>" + "Message: " + "<br />" + "<textarea rows=\"4\" cols=\"50\" readonly>" + msg.getMessage() + "</textarea>" + "<br/> <hr> <br/>");
                }



            }

            else if (Session["loggedIn"] == "true" && Session["userType"] == "user" && Session["status"] == "suspended")
            {

            }


            else if (Session["loggedIn"] == "true" && Session["userType"] == "admin")
            {
                currAdmin = fetchAdminObject(Session["userID"].ToString());

                // Fetch all currently registered users
                List<user> allUsers = new List<user>();
                allUsers = fetchUsers();

                var _bind = from a in allUsers
                            select new
                            {
                                User_ID = a.getUserID(),
                                Name = a.getName(),
                                Email = a.getEmail(),
                                Phone_Number = a.getPhoneNumber(),
                                Personal_Description = a.getPersonalDescription(),
                                status = a.getStatus()
                            };

                editUserView.DataSource = _bind;
               editUserView.DataBind();

            }

        }

        // Create tour
        protected void tourCreationController(object sender, EventArgs e)
        {
            TourErrorHandler tourHandler = new TourErrorHandler();
            string tempStart = createStartDate.Text;
            string tempEnd = createEndDate.Text;
            double test;
            bool tryDouble = double.TryParse(createPrice.Text,out test);
            
            if (createTourName.Text == "")
                tourHandler.emptyTourName();
            if (createCapacity.Text == "")
                tourHandler.emptyCapacity();
            if (!createCapacity.Text.All(char.IsDigit))
                tourHandler.invalidCapacity();
            if (createLocation.Text == "")
                tourHandler.emptyLocation();
            if (createDescription.Text == "")
                tourHandler.emptyDescription();
            if (createStartDate.Text == "")
                tourHandler.emptyStartDate();
            if (!System.Text.RegularExpressions.Regex.IsMatch(tempStart, "[0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2}"))
                tourHandler.invalidStartDate();
            if (createEndDate.Text == "")
                tourHandler.emptyEndDate();
            if (!System.Text.RegularExpressions.Regex.IsMatch(tempEnd, "[0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2}"))
                tourHandler.invalidEndDate();
            if (createPrice.Text == "")
                tourHandler.emptyPrice();
            if (tryDouble == false)
                tourHandler.invalidPrice();

            if (tourHandler.error == "")
            {
                DateTime startDate = DateTime.ParseExact(tempStart, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(tempEnd, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                if (endDate <= startDate)
                    tourHandler.endBeforeStart();

                if (tourHandler.error == "")
                {
                    tour newTour = new tour(currUser.getUserID(), createTourName.Text, int.Parse(createCapacity.Text), createLocation.Text, createDescription.Text, startDate, endDate, double.Parse(createPrice.Text), ddCreateStatus.SelectedValue);
                    newTour.createTour();
                    general_dialog.Visible = false;
                    Response.Redirect("main.aspx");
                }

                else
                {
                    general_dialog.InnerHtml = tourHandler.error;
                    general_dialog.Visible = true;
                }

            }

            else
            {
                general_dialog.InnerHtml = tourHandler.error;
                general_dialog.Visible = true;
            }

        }

        // Edit Tour
        protected void tourEditingController(object sender, EventArgs e)
        {
            TourErrorHandler tourHandler = new TourErrorHandler();
            string tempStart = editStartDate.Text;
            string tempEnd = editEndDate.Text;
            double test;
            int intTest;
            bool tryDouble = double.TryParse(editPrice.Text, out test);
            bool tryInt = int.TryParse(editID.Text, out intTest);

            if (editID.Text == "")
                tourHandler.emptyTourID();
            if (tryInt == false)
                tourHandler.invalidTourID();
            if (editName.Text == "")
                tourHandler.emptyTourName();
            if (editCapacity.Text == "")
                tourHandler.emptyCapacity();
            if (!editCapacity.Text.All(char.IsDigit))
                tourHandler.invalidCapacity();
            if (editLocation.Text == "")
                tourHandler.emptyLocation();
            if (editDescription.Text == "")
                tourHandler.emptyDescription();
            if (editStartDate.Text == "")
                tourHandler.emptyStartDate();
            if (!System.Text.RegularExpressions.Regex.IsMatch(tempStart, "[0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2}"))
                tourHandler.invalidStartDate();
            if (editEndDate.Text == "")
                tourHandler.emptyEndDate();
            if (!System.Text.RegularExpressions.Regex.IsMatch(tempEnd, "[0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2}"))
                tourHandler.invalidEndDate();
            if (editPrice.Text == "")
                tourHandler.emptyPrice();
            if (tryDouble == false)
                tourHandler.invalidPrice();
            
            

            if (tourHandler.error == "")
            {
                DateTime startDate = DateTime.ParseExact(tempStart, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(tempEnd, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                if (endDate <= startDate)
                    tourHandler.endBeforeStart();

                int tourID = int.Parse(editID.Text);
                tour editTour = fetchTourObject(tourID);

                if (editTour == null)
                    tourHandler.noSuchTourName();

                if (tourHandler.error == "")
                {
                    // Tweak Class variables using the mutator
                    editTour.setTourName(editName.Text);
                    editTour.setCapacity(int.Parse(editCapacity.Text));
                    editTour.setLocation(editLocation.Text);
                    editTour.setTourDescription(editDescription.Text);
                    editTour.setStartDate(DateTime.Parse(editStartDate.Text));
                    editTour.setEndDate(DateTime.Parse(editEndDate.Text));
                    editTour.setPrice(double.Parse(editPrice.Text));
                    editTour.setStatus(ddEditStatus.SelectedValue);

                    // Execute class function to modify tour
                    editTour.modifyTour();
                    general_dialog.Visible = false;
                    Response.Redirect("main.aspx");
                }

                else
                {
                    general_dialog.InnerHtml = tourHandler.error;
                    general_dialog.Visible = true;
                }

            }

            else
            {
                general_dialog.InnerHtml = tourHandler.error;
                general_dialog.Visible = true;
            }



        }

        protected void bookingController(object sender, EventArgs e)
        {
            bookingErrorHandler bookingHandler = new bookingErrorHandler();
            int intTest;
            bool tryInt = int.TryParse(createBooking.Text, out intTest);
            if (createBooking.Text == "")
                bookingHandler.emptyTourID();
            if (tryInt == false)
                bookingHandler.invalidTourID();

            if (bookingHandler.error == "")
            {
                int tourID = int.Parse(createBooking.Text);
                tour targetTour = fetchTourObject(tourID);

                if (targetTour != null)
                {
                    if (targetTour.getStatus() == "closed")
                        bookingHandler.tourClosed();

                    if (targetTour.getStatus() == "suspended")
                        bookingHandler.tourSuspended();

                    if (targetTour.getCapacity() < 1)
                        bookingHandler.fullyBooked();

                    if (bookingHandler.error == "")
                    {
                        int currCapacity = targetTour.getCapacity() - 1;
                        targetTour.setCapacity(currCapacity);
                        targetTour.modifyTour();
                        booking newBooking = new booking(currUser.getUserID(), tourID);
                        newBooking.createBooking();
                        general_dialog.Visible = false;
                        Response.Redirect("main.aspx");
                    }

                    else
                    {
                        general_dialog.InnerHtml = bookingHandler.error;
                        general_dialog.Visible = true;
                    }
                }

                else
                {
                    bookingHandler.invalidTourID();
                    general_dialog.InnerHtml = bookingHandler.error;
                    general_dialog.Visible = true;
                }
            }
            else
            {
                general_dialog.InnerHtml = bookingHandler.error;
                general_dialog.Visible = true;
            }

        }


        protected void editProfileController(object sender, EventArgs e)
        {
            registrationErrorHandler editHandler = new registrationErrorHandler();
            if (newPhoneNumber.Text == "")
                editHandler.emptyPhoneNumber();
            if (!newPhoneNumber.Text.All(char.IsDigit))
                editHandler.invalidPhoneNumber();
            if (newDescription.Text == "")
                editHandler.emptyDescription();

            if (editHandler.error == "")
            {
                int phone = int.Parse(newPhoneNumber.Text);
                string description = newDescription.Text;
                currUser.modifyAccount(phone, description);
                general_dialog.Visible = false;
                Response.Redirect("main.aspx");
            }

            else
            {
                general_dialog.InnerHtml = editHandler.error;
                general_dialog.Visible = true;
            }

        }

        protected void adminEditUserController(object sender, EventArgs e)
        {
            registrationErrorHandler editHandler = new registrationErrorHandler();

            if (editUserID.Text == "")
                editHandler.emptyUserName();
            if (editPassword.Text == "")
                editHandler.emptyPassword();
            if (editRealName.Text == "")
                editHandler.emptyRealName();
            if (editEmail.Text == "")
                editHandler.emptyEmail();
            if (!editEmail.Text.Contains("@"))
                editHandler.invalidEmail();
            if (editPhone.Text == "")
                editHandler.emptyPhoneNumber();
            if (!editPhone.Text.All(char.IsDigit))
                editHandler.invalidPhoneNumber();
            if (editDesc.Text == "")
                editHandler.emptyDescription();
            if (editStat.Text == "")
                editHandler.emptyStatus();
            if (!editStat.Text.All(char.IsDigit))
                editHandler.invalidStatus();

            if(editStat.Text.All(char.IsDigit))
            {
                int tryInt = int.Parse(editStat.Text);
                if (tryInt < 0 || tryInt > 1)
                    editHandler.invalidStatus();
            }

            // Fetch the user object from database 
            user targetUser = fetchUserObject(editUserID.Text);

            if (targetUser == null)
                editHandler.userNameNotExists();


            if (editHandler.error == "")
            {
                // Edit the user object based on the provided fields
                targetUser.setPassword(editPassword.Text);
                targetUser.setName(editRealName.Text);
                targetUser.setEmail(editEmail.Text);
                targetUser.setPhoneNumber(int.Parse(editPhone.Text));
                targetUser.setPersonalDescription(editDesc.Text);
                targetUser.setStatus(int.Parse(editStat.Text));

                currAdmin.editUser(targetUser);
                adminDialog.Visible = false;
                Response.Redirect("main.aspx");
            }

            else
            {
                adminDialog.InnerHtml = editHandler.error;
                adminDialog.Visible = true;
            }

        }

        protected void adminCreaterUserController(object sender, EventArgs e)
        {
            registrationErrorHandler regHandler = new registrationErrorHandler();
            if (regUserName.Text == "")
                regHandler.emptyUserName();
            if (regPassword.Text == "")
                regHandler.emptyPassword();
            if (regRealName.Text == "")
                regHandler.emptyRealName();
            if (regEmail.Text == "")
                regHandler.emptyEmail();
            if (!regEmail.Text.Contains("@"))
                regHandler.invalidEmail();
            if (regPhone.Text == "")
                regHandler.emptyPhoneNumber();
            if (!regPhone.Text.All(char.IsDigit))
                regHandler.invalidPhoneNumber();
            if (regDescription.Text == "")
                regHandler.emptyDescription();
            if (regStatus.Text == "")
                regHandler.emptyStatus();
            if (!regStatus.Text.All(char.IsDigit))
                regHandler.invalidStatus();

            if (regStatus.Text.All(char.IsDigit))
            {
                int tryInt = int.Parse(editStat.Text);
                if (tryInt < 0 || tryInt > 1)
                    regHandler.invalidStatus();
            }


            // Check username exists
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            con.Open();
            string query = "SELECT * FROM users WHERE userID='" + regUserName.Text + "';";
            cmd = new SqlCommand(query, con);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                if (reader.GetString(0) == regUserName.Text)
                    regHandler.userNameExists();
            }
            con.Close();

            if (regHandler.error == "")
            {
                user newUser = new user(regUserName.Text, regPassword.Text, regRealName.Text, regEmail.Text, int.Parse(regPhone.Text), regDescription.Text, int.Parse(regStatus.Text));
                currAdmin.createUser(newUser);
                adminDialog.Visible = false;
                Response.Redirect("main.aspx");

            }

            else
            {
                adminDialog.InnerHtml = regHandler.error;
                adminDialog.Visible = true;
            }

        }

        protected user fetchUserObject(string userID)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());

            conn.Open();

            string query = "SELECT * FROM users WHERE userID='" + userID +"';";
            cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                user temp = new user(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetString(5), reader.GetInt32(6));
                reader.Close();
                conn.Close();
                return temp;
            }
            conn.Close();

            return null;

        }

        protected admin fetchAdminObject(string adminID)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());

            conn.Open();

            string query = "SELECT * FROM admins WHERE userID='" + adminID + "';";
            cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                admin temp = new admin(reader.GetString(0), reader.GetString(1));
                reader.Close();
                conn.Close();
                return temp;
            }

            conn.Close();
            return null;
        }

        protected tour fetchTourObject(int tourID)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());

            conn.Open();

            string query = "SELECT * FROM tours WHERE tourID='" + tourID + "';";
            cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                
                tour temp = new tour(reader.GetInt32(0),reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5), reader.GetDateTime(6), reader.GetDateTime(7), (double)reader.GetDecimal(8),
                                        reader.GetString(9));
                
                reader.Close();
                conn.Close();
                return temp;
            }
            conn.Close();
            return null;
        }

        protected List<tour> fetchTours()
        {
            List<tour> availableTours = new List<tour>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());

            conn.Open();

            string query = "SELECT *  FROM  tours WHERE startDate >= GETDATE();";
            cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                tour temp = new tour(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5), reader.GetDateTime(6), reader.GetDateTime(7), (double)reader.GetDecimal(8),
                                        reader.GetString(9));
                availableTours.Add(temp);
            }
            reader.Close();
            conn.Close();
            return availableTours;
        }

        protected List<user> fetchUsers()
        {
            List<user> allUsers = new List<user>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());

            conn.Open();

            string query = "SELECT *  FROM  users;";
            cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                user temp = new user(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetString(5), reader.GetInt32(6));
                allUsers.Add(temp);
            }
            reader.Close();
            conn.Close();
            return allUsers;
        }

        protected List<chat> fetchMessages()
        {
           
            chat temp = new chat(currUser.getUserID());
            List<chat> allMessages = temp.viewMessage(currUser.getUserID());
            return allMessages;
        }

        protected void sendMsgController(object sender, EventArgs e)
        {
            inboxErrorHandler inboxHandler = new inboxErrorHandler();
            if (sendTo.Text == "")
                inboxHandler.emptyUserField();

            if (msgSubject.Text == "")
                inboxHandler.emptySubjectField();

            if (msgField.Text == "")
                inboxHandler.emptyMessageField();

            user targetUser = fetchUserObject(sendTo.Text);

            if (targetUser == null)
                inboxHandler.noSuchUser();

            if (inboxHandler.error == "")
            {
                chat newChat = new chat(currUser.getUserID(), sendTo.Text, msgSubject.Text, msgField.Text);
                newChat.sendMessage();
                Session["success"] = "msgSent";
                general_dialog.Visible = false;
                Response.Redirect("main.aspx");
            }

            else
            {
                general_dialog.InnerHtml = inboxHandler.error;
                general_dialog.Visible = true;
            }

        }

        protected void adminSuspendUserController(object sender, EventArgs e)
        {
            registrationErrorHandler suspendHandler = new registrationErrorHandler();

            if (suspendUserField.Text == "")
                suspendHandler.emptyUserName();

            // Fetch the user object that needs to be suspended
            user suspendedUser = fetchUserObject(suspendUserField.Text);

            if (suspendedUser == null)
                suspendHandler.userNameNotExists();

            if (suspendHandler.error == "")
            {
                // Change user to suspended status
                suspendedUser.setStatus(1);

                // Write back the user object to database
                currAdmin.suspendUser(suspendedUser);
                adminDialog.Visible = false;
                Response.Redirect("main.aspx");
                
            }

            else
            {
                adminDialog.InnerHtml = suspendHandler.error;
                adminDialog.Visible = true;
            }

        }

        protected void giveRatingTourGuideController(object sender, EventArgs e)
        {
            ratingErrorHandler ratingHandler = new ratingErrorHandler();

            if (rateTourGuideID.Text == "")
                ratingHandler.emptyIDField();

            // Fetch tourGuide object that user wants to rate
            user tourGuide = fetchUserObject(rateTourGuideID.Text);

            if (tourGuide == null)
                ratingHandler.noSuchUser();


            if (ratingHandler.error == "")
            {
                // Create new rating object
                rating newRating = new rating(tourGuide.getUserID(), currUser.getUserID(), int.Parse(ddTourGuideStars.SelectedValue), "tourguide");

                // Execute write rating to database
                newRating.createRating();

                Session["success"] = "giveRating";
                general_dialog.Visible = false;
                Response.Redirect("main.aspx");
            }

            else
            {
                general_dialog.InnerHtml = ratingHandler.error;
                general_dialog.Visible = true;
            }

        }

        protected void giveRatingTouristController(object sender, EventArgs e)
        {
            ratingErrorHandler ratingHandler = new ratingErrorHandler();
            if (rateTouristID.Text == "")
                ratingHandler.emptyIDField();

            // Fetch tourist object that user wants to rate
            user tourist = fetchUserObject(rateTouristID.Text);

            if (tourist == null)
                ratingHandler.noSuchUser();

            if (ratingHandler.error == "")
            {
                // Create new rating object
                rating newRating = new rating(tourist.getUserID(), currUser.getUserID(), int.Parse(ddTouristStars.SelectedValue), "tourist");

                // Execute write rating to database
                newRating.createRating();

                Session["success"] = "giveRating";
                general_dialog.Visible = false;
                Response.Redirect("main.aspx");
            }

            else
            {
                general_dialog.InnerHtml = ratingHandler.error;
                general_dialog.Visible = true;
            }
        }

        protected void filterToursController(object sender, EventArgs e)
        {

            if(ddFilterTour.SelectedValue == "Price" && ddFilterCriteria.SelectedValue =="Ascending")
            {
                Session["filterType"] = "Price";
                Session["criteria"] = "Ascending";
            }

            else if (ddFilterTour.SelectedValue == "Price" && ddFilterCriteria.SelectedValue == "Descending")
            {
                Session["filterType"] = "Price";
                Session["criteria"] = "Descending";
            }

            else if (ddFilterTour.SelectedValue == "Rating" && ddFilterCriteria.SelectedValue == "Ascending")
            {
                Session["filterType"] = "Rating";
                Session["criteria"] = "Ascending";
            }

            else if (ddFilterTour.SelectedValue == "Rating" && ddFilterCriteria.SelectedValue == "Descending")
            {
                Session["filterType"] = "Rating";
                Session["criteria"] = "Descending";
            }

            else
            {
                Session["filterType"] = "Default";
                Session["criteria"] = "Default";
            }

            general_dialog.Visible = false;
            Response.Redirect("main.aspx#touristTabs");
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            Session["userID"] = "";
            Session["status"] = "";
            Session["loggedIn"] = "false";
            Session["userType"] = "";
            Session["filterType"] = "Default";
            Session["criteria"] = "Default";
            Response.Redirect("default.aspx");
        }



    }
}
