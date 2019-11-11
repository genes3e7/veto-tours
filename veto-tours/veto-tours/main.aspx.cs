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
        protected void createTour_Click(object sender, EventArgs e)
        {
            string tempStart = createStartDate.Text;
            string tempEnd = createEndDate.Text;
            DateTime startDate = DateTime.ParseExact(tempStart, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(tempEnd, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            tour newTour = new tour(currUser.getUserID(), createTourName.Text, int.Parse(createCapacity.Text), createLocation.Text, createDescription.Text, startDate, endDate, double.Parse(createPrice.Text), ddCreateStatus.SelectedValue);
            newTour.createTour();
            
            Response.Redirect("main.aspx");

        }

        // Edit Tour
        protected void editTour_Click(object sender, EventArgs e)
        {
            
            int tourID = int.Parse(editID.Text);

            // Query database to pull out tour information based on given tourID into a tour object
            tour editTour = fetchTourObject(tourID);

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

            Response.Redirect("main.aspx");

        }

        protected void createBooking_Click(object sender, EventArgs e)
        {
            int tourID = int.Parse(createBooking.Text);

            booking newBooking = new booking(currUser.getUserID(), tourID);
            newBooking.createBooking();
            Response.Redirect("main.aspx");

        }


        protected void editProfile_Click(object sender, EventArgs e)
        {
            int phone = int.Parse(newPhoneNumber.Text);
            string description = newDescription.Text;
            currUser.modifyAccount(phone, description);
            Response.Redirect("main.aspx");

        }

        protected void editUser_Click(object sender, EventArgs e)
        {

            // Fetch the user object from database 
            user targetUser = fetchUserObject(editUserID.Text);

            // Edit the user object based on the provided fields
            targetUser.setPassword(editPassword.Text);
            targetUser.setName(editRealName.Text);
            targetUser.setEmail(editEmail.Text);
            targetUser.setPhoneNumber(int.Parse(editPhone.Text));
            targetUser.setPersonalDescription(editDesc.Text);
            targetUser.setStatus(int.Parse(editStat.Text));

            currAdmin.editUser(targetUser);

            Response.Redirect("main.aspx");

        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {

            user newUser = new user(regUserName.Text, regPassword.Text, regRealName.Text, regEmail.Text, int.Parse(regPhone.Text), regDescription.Text, int.Parse(regStatus.Text));
            currAdmin.createUser(newUser);

            Response.Redirect("main.aspx");

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
                return temp;
            }

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
                return temp;
            }

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
                return temp;
            }

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

            return allUsers;
        }

        protected List<chat> fetchMessages()
        {
           
            chat temp = new chat(currUser.getUserID());
            List<chat> allMessages = temp.viewMessage(currUser.getUserID());
            return allMessages;
        }

        protected void sendMsg_Click(object sender, EventArgs e)
        {

            chat newChat = new chat(currUser.getUserID(),sendTo.Text, msgSubject.Text, msgField.Text);
            newChat.sendMessage();

            Response.Redirect("main.aspx");

        }

        protected void btnSuspendUser_Click(object sender, EventArgs e)
        {
            // Fetch the user object that needs to be suspended
            user suspendedUser = fetchUserObject(suspendUserField.Text);

            // Change user to suspended status
            suspendedUser.setStatus(1);

            // Write back the user object to database
            currAdmin.editUser(suspendedUser);

            // Change all their tours to suspended status
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            con.Open();
            string query = "UPDATE tours SET status= 'suspended' WHERE userID='" + suspendedUser.getUserID() + "';"; 
            cmd = new SqlCommand(query, con);
            reader = cmd.ExecuteReader();
            con.Close();

            Response.Redirect("main.aspx");

        }

        protected void giveRatingTourGuide_Click(object sender, EventArgs e)
        {

            // Fetch tourGuide object that user wants to rate
            user tourGuide = fetchUserObject(rateTourGuideID.Text);

            // Create new rating object
            rating newRating = new rating(tourGuide.getUserID(), currUser.getUserID(), int.Parse(setRating.Value), "tourguide");

            // Execute write rating to database
            newRating.createRating();

            Response.Redirect("main.aspx");

        }

        protected void giveRatingTourist_Click(object sender, EventArgs e)
        {

            // Fetch tourist object that user wants to rate
            user tourist = fetchUserObject(rateTouristID.Text);

            // Create new rating object
            rating newRating = new rating(tourist.getUserID(), currUser.getUserID(), int.Parse(setRatingTourist.Value), "tourist");

            // Execute write rating to database
            newRating.createRating();

            Response.Redirect("main.aspx");

        }

        protected void filterTours_Click(object sender, EventArgs e)
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
