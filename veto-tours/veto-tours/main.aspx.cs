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
           if (Session["loggedIn"] == "true" && Session["userType"] == "user")
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
                var _bind = from a in availableTours
                            select new
                            {
                                Tour_ID = a.getTourID(),
                                Created_By = a.getUserID(),
                                Tour_Name = a.getTourName(),
                                Tour_Capacity = a.getCapacity(),
                                Tour_Location = a.getLocation(),
                                Tour_Description = a.getTourDescription(),
                                Start_Date = a.getStartDate().Substring(0,10),
                                End_Date = a.getEndDate().Substring(0,10),
                                Duration = a.getDuration(),
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

        protected void createTour_Click(object sender, EventArgs e)
        {

            tour newTour = new tour(currUser.getUserID(), createTourName.Text, int.Parse(createCapacity.Text), createLocation.Text, createDescription.Text, createStartDate.Text, createEndDate.Text, createDuration.Text, double.Parse(createPrice.Text), createStatus.Text);
            newTour.createTour();
            
            Response.Redirect("main.aspx");

        }

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
            editTour.setStartDate(editStartDate.Text);
            editTour.setEndDate(editEndDate.Text);
            editTour.setDuration(editDuration.Text);
            editTour.setPrice(double.Parse(editPrice.Text));
            editTour.setStatus(editStatus.Text);

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
                
                tour temp = new tour(reader.GetInt32(0),reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5), reader.GetDateTime(6).ToString(), reader.GetDateTime(7).ToString(), reader.GetTimeSpan(8).ToString(), (double)reader.GetDecimal(9),
                                        reader.GetString(10));
                
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
                tour temp = new tour(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5), reader.GetDateTime(6).ToString(), reader.GetDateTime(7).ToString(), reader.GetTimeSpan(8).ToString(), (double)reader.GetDecimal(9),
                                        reader.GetString(10));
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


    }
}
