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
                loginErrorHandler loginHandler = new loginErrorHandler();
                string uid = txtUserName.Text;
                string pass = txtPassword.Text;

                if (txtUserName.Text == "")
                    loginHandler.emptyUserName();

                if (txtPassword.Text == "")
                    loginHandler.emptyPassword();

                if (loginHandler.error == "")
                {
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
                        Session["filterType"] = "default";
                        Session["criteria"] = "default";

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
                        loginHandler.noSuchUser();
                        loginErrorDialog.InnerHtml = loginHandler.error;
                        loginErrorDialog.Visible = true;

                    }
                    con.Close();
                }

                else
                {
                    loginErrorDialog.InnerHtml = loginHandler.error;
                    loginErrorDialog.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
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

            // Check username exists
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            con.Open();
            string query = "SELECT * FROM users WHERE userID='" + regUserName.Text + "';";
            cmd = new SqlCommand(query, con);
            reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                if (reader.GetString(0) == regUserName.Text)
                    regHandler.userNameExists();
            }
            con.Close();

            if (regHandler.error == "")
            {
                user newUser = new user(regUserName.Text, regPassword.Text, regRealName.Text, regEmail.Text, int.Parse(regPhone.Text), regDescription.Text, 0);
                newUser.createAccount();
                successfulRegistration.InnerHtml = "You have successfully registered!";
                successfulRegistration.Visible = true;
                Response.Redirect("default.aspx");
                
            }

            else
            {
                errorDialog.InnerHtml = regHandler.error;
                errorDialog.Visible = true;
            }

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
            string query = "INSERT INTO users VALUES('" + userID + "', '" + password + "', '" + name + "', '" + email + "', '" + phoneNumber + "', '" + personalDescription + "', '" + status + "')";
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
                    "startDate AS 'Start Date', endDate AS 'End Date', price AS Price, status AS Status  FROM  tours WHERE startDate < SYSDATETIME() AND " +
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
                    "startDate AS 'Start Date', endDate AS 'End Date', price AS Price, status AS Status  FROM  tours WHERE startDate >= SYSDATETIME() AND " +
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
                    "startDate AS 'Start Date', endDate AS 'End Date', price AS Price, status AS Status  FROM  tours WHERE userID='" + userID + "';";
            cmd = new SqlCommand(query, con);
            reader = cmd.ExecuteReader();
            createdToursView.DataSource = reader;
            createdToursView.DataBind();
        }

        public void fetchAvgUserRatings()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            con.Open();
            string query = "SELECT AVG(stars) FROM ratings WHERE ratingTo='" + userID + "' AND type='tourguide';";
            cmd = new SqlCommand(query, con);
            reader = cmd.ExecuteReader();
            if (reader.Read() && !reader.IsDBNull(0))
                ratingTourGuide = reader.GetInt32(0);
            else
                ratingTourGuide = 0;

            query = "SELECT AVG(stars) FROM ratings WHERE ratingTo='" + userID + "' AND type='tourist';";
            cmd = new SqlCommand(query, con);
            reader = cmd.ExecuteReader();
            if (reader.Read() && !reader.IsDBNull(0))
                ratingTourist = reader.GetInt32(0);
            else
                ratingTourist = 0;
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
        private DateTime startDate;
        private DateTime endDate;
        private double price;
        private string status;

        public tour(string userID, string tourName, int capacity, string location, string tourDescription, DateTime startDate, DateTime endDate, double price, string status)
        {
            this.userID = userID;
            this.tourName = tourName;
            this.capacity = capacity;
            this.location = location;
            this.tourDescription = tourDescription;
            this.startDate = startDate;
            this.endDate = endDate;
            this.price = price;
            this.status = status;
        }

        public tour(int tourID, string userID, string tourName, int capacity, string location, string tourDescription, DateTime startDate, DateTime endDate, double price, string status)
        {
            this.tourID = tourID;
            this.userID = userID;
            this.tourName = tourName;
            this.capacity = capacity;
            this.location = location;
            this.tourDescription = tourDescription;
            this.startDate = startDate;
            this.endDate = endDate;
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

        public void setStartDate(DateTime startDate)
        {
            this.startDate = startDate;
        }

        public void setEndDate(DateTime endDate)
        {
            this.endDate = endDate;
        }


        public void setPrice(double price)
        {
            this.price = price;
        }

        public void setStatus(string status)
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

        public DateTime getStartDate()
        {
            return startDate;
        }

        public DateTime getEndDate()
        {
            return endDate;
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
            string format = "yyy-MM-dd HH:mm:ss";
            string query = "INSERT INTO tours (userID, tourName, capacity, location, description, startDate, endDate, price, status) VALUES ('"
                            + userID + "', '" + tourName + "', '" + capacity + "', '" + location + "', '" + tourDescription + "', '" + startDate.ToString(format) + "', '" + endDate.ToString(format) + "', '" + price.ToString() + "', '" + status + "');";
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
            string format = "yyy-MM-dd HH:mm:ss";
            if (reader.Read())
            {
                query = "UPDATE tours SET tourName = '" + tourName + "', capacity = '" + capacity + "', location = '" + location + "', description = '" + tourDescription + "', startDate ='" + startDate.ToString(format) + "', endDate ='" + endDate.ToString(format) + "'," +
                    "price = '" + price + "', status = '" + status + "' WHERE tourID=" + tourID + ";";

                reader.Close();
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
            }
            con.Close();

        }

        public int fetchTourGuideRating()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            con.Open();
            string query = "SELECT AVG(stars) FROM ratings WHERE ratingTo='" + userID + "' AND type='tourguide';";
            cmd = new SqlCommand(query, con);
            reader = cmd.ExecuteReader();
            if (reader.Read() && !reader.IsDBNull(0))
            {
                int ratingTourGuide = reader.GetInt32(0);
                return ratingTourGuide;
            }

            else
            {
                return 0;
            }

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

        public chat(string sender)
        {
            this.sender = sender;
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
                            + sender + "', '" + recipient + "', '" + subject + "', '" + message + "', '" + dateTime + "');"; ;

            cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            reader.Close();

        }

        public List<chat> viewMessage(string recipient)
        {
            List<chat> allMessages = new List<chat>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());

            conn.Open();

            string query = "SELECT *  FROM  chat WHERE recipient='" + recipient + "';";
            cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                chat temp = new chat(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetDateTime(5));
                allMessages.Add(temp);
            }
            reader.Close();

            return allMessages;

        }
    }

    public class rating
    {
        private int ratingID;
        private string ratingTo;
        private string ratingFrom;
        private int stars;
        private string type;

        public rating(string ratingTo, string ratingFrom, int stars, string type)
        {
            this.ratingTo = ratingTo;
            this.ratingFrom = ratingFrom;
            this.stars = stars;
            this.type = type;
        }

        public int getRatingID()
        {
            return ratingID;
        }

        public string getRatingTo()
        {
            return ratingTo;
        }

        public string getRatingFrom()
        {
            return ratingFrom;
        }

        public int getStars()
        {
            return stars;
        }

        public string getType()
        {
            return type;
        }

        public void setRatingTo(string ratingTo)
        {
            this.ratingTo = ratingTo;
        }

        public void setRatingFrom(string ratingFrom)
        {
            this.ratingFrom = ratingFrom;
        }

        public void setStars(int stars)
        {
            this.stars = stars;
        }

        public void setType(string type)
        {
            this.type = type;
        }

        public void createRating()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());

            conn.Open();

            string query = "INSERT INTO ratings (ratingTo, ratingFrom, stars, type) VALUES ('"
                            + ratingTo + "', '" + ratingFrom + "', '" + stars + "', '" + type + "');"; ;

            cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            reader.Close();

        }
    }

    public class registrationErrorHandler
    {
        public string label;
        public string error;

        public registrationErrorHandler()
        {
            label = "The following errors were found during the registration process: <br />";
            error = "";
        }

        public void emptyUserName()
        {
            error += "- Username Field Empty <br />";
        }

        public void emptyPassword()
        {
            error += "- Password Field Empty <br />";
        }

        public void emptyRealName()
        {
            error += "- Real Name Field Empty <br />";
        }

        public void emptyEmail()
        {
            error += "- Email Field Empty <br />";
        }

        public void invalidEmail()
        {
            error += "- Email does not contain @ sign <br />";
        }

        public void emptyPhoneNumber()
        {
            error += "- Phone Number Field Empty <br />";
        }

        public void invalidPhoneNumber()
        {
            error += "- Invalid Phone Number Entry <br />";
        }

        public void emptyDescription()
        {
            error += "- Description Field Empty <br />";
        }

        public void userNameExists()
        {
            error += "- The username has been taken <br />";
        }
    }

    public class loginErrorHandler
    {
        public string label;
        public string error;

        public loginErrorHandler()
        {
            label = "The following errors were found during the login process: <br />";
            error = "";
        }

        public void emptyUserName()
        {
            error += "- Username Field Empty <br />";
        }

        public void emptyPassword()
        {
            error += "- Password Field Empty <br />";
        }

        public void noSuchUser()
        {
            error += "- Username and password does not exist <br />";
        }
    }
}
