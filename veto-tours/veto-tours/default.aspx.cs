// Nicholas Leung Jun Yen
// UOW ID: 5987325

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient; //this namespace is for sqlclient server  
using System.Configuration; // this namespace is add I am adding connection name in web config file config connection name  

namespace vetoTours
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string uid = txtUserName.Text;
                string pass = txtPassword.Text;
				SqlDataReader sdr;

				con.Open();

				// If userType is regular user
				if (userType.SelectedValue == "admin")
				{
					string query = "SELECT * from admins where userID='" + uid + "' and password='" + pass + "'";
					SqlCommand cmd = new SqlCommand(query, con);
					sdr = cmd.ExecuteReader();

				}

				else
				{
					string query = "SELECT * from users where userID='" + uid + "' and password='" + pass + "'";
					SqlCommand cmd = new SqlCommand(query, con);
					sdr = cmd.ExecuteReader();
				}

                if (sdr.Read())
                {
                    lblStatus.Text = "Logged in successfully! ";
                    Session["loggedIn"] = "true";
                    Session["userID"] = uid;

                    if (userType.SelectedValue == "admin")
                        Session["userType"] = "admin";
                    else
                    {
                        Session["userType"] = "user";

                        if (sdr.GetInt32(6) == 0)
                            Session["status"] = "normal";
                        else
                            Session["status"] = "suspended";
                    }

					Response.Redirect("main.aspx");
                }
                else
                {
                    Session["loggedIn"] = "false";
                    lblStatus.Text = "Username or password is invalid ";

                }
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

		protected void btnRegister_Click(object sender, EventArgs e)
		{
            user newUser = new user(regUserName.Text, regPassword.Text, regRealName.Text, regEmail.Text, int.Parse(regPhone.Text), regDescription.Text, 0);
            newUser.createAccount();
            Response.Redirect("default.aspx");
			regStatus.Text = "Successfully Registered!";
			
		}

	}

    public class user
    {
        private string userID;
        private string password;
        private string name;
        private string email;
        private int phoneNumber;
        private string personalDescription;
        private int status;
        private int ratingTourist;
        private int ratingTourGuide;

        public user(string userID, string password, string name, string email, int phoneNumber, string desc, int status)
        {
            this.userID = userID;
            this.password = password;
            this.name = name;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.personalDescription = desc;
            this.status = status;
        }
        public void setPassword(string password)
        {
            this.password = password;
        }

        public void setName(string name) 
        {
            this.name = name;
        }

        public void setEmail(string email) 
        {
            this.email = email;
        }


        public void setPhoneNumber(int phoneNumber)
        {
            this.phoneNumber = phoneNumber;
        }

        public void setPersonalDescription(string personalDescription)
        {
            this.personalDescription = personalDescription;
        }

        public void setStatus(int status)
        {
            this.status = status;
        }

        public void setRatingTourist(int rating)
        {
            ratingTourist = rating;
        }

        public void setRatingTourGuide(int rating)
        {
            ratingTourGuide = rating;
        }

        public string getUserID()
        {
            return userID;
        }

        public string getPassword()
        {
            return password;
        }

        public string getName()
        {
            return name;
        }

        public string getEmail()
        {
            return email;
        }

        public int getPhoneNumber()
        {
            return phoneNumber;
        }

        public string getPersonalDescription()
        {
            return personalDescription;
        }

        public int getStatus()
        {
            return status;
        }

        public int getRatingTourist()
        {
            return ratingTourist;
        }

        public int getRatingTourGuide()
        {
            return ratingTourGuide;
        }

        public void createAccount()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            con.Open();
            string query = "INSERT INTO users VALUES('" + userID + "', '" + password + "', '" + name + "', '" + email + "', '" + phoneNumber  + "', '" + personalDescription + "', '" + status + "')";
            cmd = new SqlCommand(query, con);
            reader = cmd.ExecuteReader();
            con.Close();
        }

        public void modifyAccount(int phoneNumber, string personalDescription)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            con.Open();
            string query = "UPDATE users SET password= '" + password + "', name='" + name + "', email ='" + email + "', phoneNumber=" + phoneNumber + ", description ='" + personalDescription + "', status=" + status +
                " WHERE userID='" + userID + "';";
            cmd = new SqlCommand(query, con);
            reader = cmd.ExecuteReader();
            con.Close();
        }

        public void getBookingHistory(GridView bookingHistoryView)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            con.Open();
            string query = "SELECT tourID AS 'Tour ID', userID AS 'Tour Guide Name', tourName AS 'Tour Name', capacity AS Capacity, location AS Location, description AS Description, " +
                    "FORMAT(startDate, 'd', 'en-gb') AS 'Start Date', FORMAT(endDate, 'd', 'en-gb') AS 'End Date', duration AS Duration, price AS Price, status AS Status  FROM  tours WHERE startDate < GETDATE() AND " +
                    "tourID IN (SELECT tourID FROM bookings WHERE userID='" + userID + "');";
            cmd = new SqlCommand(query, con);
            reader = cmd.ExecuteReader();
            bookingHistoryView.DataSource = reader;
            bookingHistoryView.DataBind();
        }


        public void getUpcomingBookings(GridView bookedToursView)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            con.Open();
            string query = "SELECT tourID AS 'Tour ID', userID AS 'Tour Guide Name', tourName AS 'Tour Name', capacity AS Capacity, location AS Location, description AS Description, " +
                "FORMAT(startDate, 'd', 'en-gb') AS 'Start Date', FORMAT(endDate, 'd', 'en-gb') AS 'End Date', duration AS Duration, price AS Price, status AS Status  FROM  tours WHERE startDate >= GETDATE() AND " +
                "tourID IN (SELECT tourID FROM bookings WHERE userID='" + userID + "');";
            cmd = new SqlCommand(query, con);
            reader = cmd.ExecuteReader();
            bookedToursView.DataSource = reader;
            bookedToursView.DataBind();
        }

        public void getProfileDetails(GridView myProfileView)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            con.Open();
            string query = "SELECT userID AS 'Username', name AS 'Real Name', email AS 'Email', phoneNumber AS 'Phone Number', description AS 'Description' FROM users WHERE userID='" + userID + "';";
            cmd = new SqlCommand(query, con);
            reader = cmd.ExecuteReader();
            myProfileView.DataSource = reader;
            myProfileView.DataBind();
        }

        public void getCreatedTours(GridView createdToursView)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            con.Open();
            string query = "SELECT tourID AS 'Tour ID', userID AS 'Tour Guide Name', tourName AS 'Tour Name', capacity AS Capacity, location AS Location, description AS Description, " +
                    "FORMAT(startDate, 'd', 'en-gb') AS 'Start Date', FORMAT(endDate, 'd', 'en-gb') AS 'End Date', duration AS Duration, price AS Price, status AS Status  FROM  tours WHERE userID='" + userID + "';";
            cmd = new SqlCommand(query, con);
            reader = cmd.ExecuteReader();
            createdToursView.DataSource = reader;
            createdToursView.DataBind();


        }
    }

    public class tour
    {
        private int tourID;
        private string userID;
        private string tourName;
        private int capacity;
        private string location;
        private string tourDescription;
        private string startDate;
        private string endDate;
        private string duration;
        private double price;
        private string status;

        public tour (string userID, string tourName, int capacity, string location, string tourDescription, string startDate, string endDate, string duration, double price, string status)
        {
            this.userID = userID;
            this.tourName = tourName;
            this.capacity = capacity;
            this.location = location;
            this.tourDescription = tourDescription;
            this.startDate = startDate;
            this.endDate = endDate;
            this.duration = duration;
            this.price = price;
            this.status = status;
        }

        public tour(int tourID, string userID, string tourName, int capacity, string location, string tourDescription, string startDate, string endDate, string duration, double price, string status)
        {
            this.tourID = tourID;
            this.userID = userID;
            this.tourName = tourName;
            this.capacity = capacity;
            this.location = location;
            this.tourDescription = tourDescription;
            this.startDate = startDate;
            this.endDate = endDate;
            this.duration = duration;
            this.price = price;
            this.status = status;
        }

        public void setTourName(string tourName)
        {
            this.tourName = tourName;
        }

        public void setCapacity(int capacity)
        {
            this.capacity = capacity;
        }

        public void setLocation(string location)
        {
            this.location = location;
        }

        public void setTourDescription(string tourDescription)
        {
            this.tourDescription = tourDescription;
        }

        public void setStartDate(string startDate)
        {
            this.startDate = startDate;
        }

        public void setEndDate (string endDate)
        {
            this.endDate = endDate;
        }

        public void setDuration (string duration)
        {
            this.duration = duration;
        }

        public void setPrice (double price)
        {
            this.price = price;
        }

        public void setStatus (string status)
        {
            this.status = status;
        }

        public int getTourID()
        {
            return tourID;
        }

        public string getUserID()
        {
            return userID;
        }

        public string getTourName()
        {
            return tourName;
        }

        public int getCapacity()
        {
            return capacity;
        }

        public string getLocation()
        {
            return location;
        }

        public string getTourDescription()
        {
            return tourDescription;
        }

        public string getStartDate()
        {
            return startDate;
        }

        public string getEndDate()
        {
            return endDate;
        }

        public string getDuration()
        {
            return duration;
        }

        public double getPrice()
        {
            return price;
        }

        public string getStatus()
        {
            return status;
        }

        public void createTour()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            con.Open();
            string query = "INSERT INTO tours (userID, tourName, capacity, location, description, startDate, endDate, duration, price, status) VALUES ('"
                            + userID + "', '" + tourName + "', '" + capacity + "', '" + location + "', '" + tourDescription + "', CAST('" + startDate.ToString() + "' AS date), CAST('" + endDate.ToString() + "' AS date), '" + duration + "', '" + price.ToString() + "', '" + status + "');";
            cmd = new SqlCommand(query, con);
            reader = cmd.ExecuteReader();
            con.Close();

        }

        public void modifyTour()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            con.Open();
            string query = "SELECT " + tourID + " FROM tours WHERE userID='" + userID + "';";
            cmd = new SqlCommand(query, con);
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                query = "UPDATE tours SET tourName = '" + tourName + "', capacity = '" + capacity + "', location = '" + location + "', description = '" + tourDescription + "', startDate = CAST('" + startDate.ToString() + "' AS date), endDate = CAST('" + startDate.ToString() + "' AS date)," +
                    "duration = '" + duration + "', price = '" + price + "', status = '" + status + "' WHERE tourID=" + tourID + ";";

                reader.Close();
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
            }
            con.Close();

        }
    }

    public class booking
    {
        private int bookingID;
        private string userID;
        private int tourID;

        public booking(string userID, int tourID)
        {
            this.userID = userID;
            this.tourID = tourID;
        }

        public void setBookingID(int bookingID)
        {
            this.bookingID = bookingID;
        }

        public void setUserID(string userID)
        {
            this.userID = userID;
        }

        public void setTourID(int tourID)
        {
            this.tourID = tourID;
        }

        public int getBookingID()
        {
            return bookingID;
        }

        public string getUserID()
        {
            return userID;
        }

        public int getTourID()
        {
            return tourID;
        }

        public void createBooking()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());

            conn.Open();

            string query = "INSERT INTO bookings (userID, tourID) VALUES('" + userID + "', " + tourID + ");";
            cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
        }
    }

    public class admin
    {
        private string adminID;
        private string password;

        public admin(string adminID, string password)
        {
            this.adminID = adminID;
            this.password = password;
        }
        public void createUser(user newUser)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());

            conn.Open();

            string query = "INSERT INTO users VALUES('" + newUser.getUserID() + "', '" + newUser.getPassword() + "', '" + newUser.getName() + "', '" + newUser.getEmail() + "', '" + newUser.getPhoneNumber() + "', '" 
                                + newUser.getPersonalDescription() + "', '" + newUser.getStatus() + "')";

            cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            reader.Close();

        }

        public void editUser(user targetUser)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());

            conn.Open();

            string query = "UPDATE users SET password= '" + targetUser.getPassword() + "', name='" + targetUser.getName() + "', email ='" + targetUser.getEmail() + "', phoneNumber=" + targetUser.getPhoneNumber() 
                            + ", description ='" + targetUser.getPersonalDescription() + "', status=" + targetUser.getStatus() + " WHERE userID='" + targetUser.getUserID() + "';";
            cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            reader.Close();

        }

    }

    public class chat
    {
        private int chatID;
        private string sender;
        private string recipient;
        private string subject;
        private string message;
        private DateTime dateTime;

        public chat(int chatID, string sender, string recipient, string subject, string message, DateTime dateTime)
        {
            this.chatID = chatID;
            this.sender = sender;
            this.recipient = recipient;
            this.subject = subject;
            this.message = message;
            this.dateTime = dateTime;
        }

        public chat(string sender, string recipient, string subject, string message)
        {
            this.sender = sender;
            this.recipient = recipient;
            this.subject = subject;
            this.message = message;
            dateTime = DateTime.Now;
        }

        public void setChatID(int chatID)
        {
            this.chatID = chatID;
        }

        public void setSender(string sender)
        {
            this.sender = sender;
        }

        public void setRecipient(string recipient)
        {
            this.recipient = recipient;
        }

        public void setSubject(string subject)
        {
            this.subject = subject;
        }

        public void setMessage(string message)
        {
            this.message = message;
        }

        public void setDateTime(DateTime dateTime)
        {
            this.dateTime = dateTime;
        }

        public int getChatID()
        {
            return chatID;
        }

        public string getSender()
        {
            return sender;
        }

        public string getRecipient()
        {
            return recipient;
        }

        public DateTime getDateTime()
        {
            return this.dateTime;
        }

        public string getSubject()
        {
            return subject;
        }

        public string getMessage()
        {
            return message;
        }

        public void sendMessage()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());

            conn.Open();

            string query = "INSERT INTO chat (sender, recipient, subject, message, dateTime) VALUES ('"
                            + sender + "', '" + recipient + "', '" + subject + "', '" + message + "', '"+ dateTime + "');"; ;

            cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            reader.Close();

        }

        public List<chat> viewMessage()
        {
            List <chat> allMessages = new List<chat>();
            return allMessages;
        }
    }


}
