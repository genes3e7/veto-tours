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
        protected void Page_Load(object sender, EventArgs e)
        {
           if (Session["loggedIn"] == "true")
           {
                SqlConnection conn = null;
                SqlCommand cmd = null;
                SqlDataReader reader = null;

                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());

                conn.Open();

                // Pull Tour Guides Created Tours
                string query = "SELECT tourID AS 'Tour ID', userID AS 'Tour Guide Name', tourName AS 'Tour Name', capacity AS Capacity, location AS Location, description AS Description, " +
                    "FORMAT(startDate, 'd', 'en-gb') AS 'Start Date', FORMAT(endDate, 'd', 'en-gb') AS 'End Date', duration AS Duration, price AS Price, status AS Status  FROM  tours WHERE userID='" + Session["userID"].ToString() + "';";

                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                createdToursView.DataSource = reader;
                createdToursView.DataBind();
                reader.Close();

                // Pull all Available tours that are ahead of current system time
                query = "SELECT tourID AS 'Tour ID', userID AS 'Tour Guide Name', tourName AS 'Tour Name', capacity AS Capacity, location AS Location, description AS Description, " +
                    "FORMAT(startDate, 'd', 'en-gb') AS 'Start Date', FORMAT(endDate, 'd', 'en-gb') AS 'End Date', duration AS Duration, price AS Price, status AS Status  FROM  tours WHERE startDate >= GETDATE();";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                availableToursView.DataSource = reader;
                availableToursView.DataBind();
                reader.Close();


                // Pull all booked tours that have yet to start
                query = "SELECT tourID AS 'Tour ID', userID AS 'Tour Guide Name', tourName AS 'Tour Name', capacity AS Capacity, location AS Location, description AS Description, " +
                "FORMAT(startDate, 'd', 'en-gb') AS 'Start Date', FORMAT(endDate, 'd', 'en-gb') AS 'End Date', duration AS Duration, price AS Price, status AS Status  FROM  tours WHERE startDate >= GETDATE() AND " +
                "tourID IN (SELECT tourID FROM bookings WHERE userID='" + Session["userID"].ToString() + "');";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                bookedToursView.DataSource = reader;
                bookedToursView.DataBind();
                reader.Close();

                // Pull booking history where the events have ended
                query = "SELECT tourID AS 'Tour ID', userID AS 'Tour Guide Name', tourName AS 'Tour Name', capacity AS Capacity, location AS Location, description AS Description, " +
                "FORMAT(startDate, 'd', 'en-gb') AS 'Start Date', FORMAT(endDate, 'd', 'en-gb') AS 'End Date', duration AS Duration, price AS Price, status AS Status  FROM  tours WHERE startDate < GETDATE() AND " +
                "tourID IN (SELECT tourID FROM bookings WHERE userID='" + Session["userID"].ToString() + "');";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                bookingHistoryView.DataSource = reader;
                bookingHistoryView.DataBind();
                reader.Close();

                // Pull User Profile Information
                query = "SELECT userID AS 'Username', name AS 'Real Name', email AS 'Email', phoneNumber AS 'Phone Number', description AS 'Description' FROM users WHERE userID='" + Session["userID"].ToString() + "';";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                myProfileView.DataSource = reader;
                myProfileView.DataBind();
                reader.Close();

            }
        }

        protected void createTour_Click(object sender, EventArgs e)
        {
            string tourName = createTourName.Text;
            int capacity = int.Parse(createCapacity.Text);
            string location = createLocation.Text;
            string description = createDescription.Text;
            string startDate = createStartDate.Text;
            string endDate = createEndDate.Text;
            string duration = createDuration.Text;
            string price = createPrice.Text;
            string status = createStatus.Text;

            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());

            conn.Open();

            string query = "INSERT INTO tours (userID, tourName, capacity, location, description, startDate, endDate, duration, price, status) VALUES ('" 
                            + Session["userID"].ToString() + "', '" + tourName + "', '" + capacity + "', '" + location + "', '" + description + "', CAST('" + startDate.ToString() + "' AS date), CAST('" + endDate.ToString() + "' AS date), '" + duration + "', '" + price + "', '" + status + "')";

            cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            Response.Redirect("main.aspx");

        }

        protected void editTour_Click(object sender, EventArgs e)
        {
            int tourID = int.Parse(editID.Text);
            string tourName = editName.Text;
            int capacity = int.Parse(editCapacity.Text);
            string location = editLocation.Text;
            string description = editDescription.Text;
            string startDate = editStartDate.Text;
            string endDate = editEndDate.Text;
            string duration = editDuration.Text;
            string price = editPrice.Text;
            string status = editStatus.Text;

            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());

            conn.Open();

            // Check if tour rightfully belongs to user trying to edit
            string query = "SELECT " + tourID + " FROM tours WHERE userID='" + Session["userID"].ToString() +"';";
            cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();


            if (reader.Read())
            {
                query = "UPDATE tours SET tourName = '" + tourName + "', capacity = '" + capacity + "', location = '" + location + "', description = '" + description + "', startDate = CAST('" + startDate.ToString() + "' AS date), endDate = CAST('" + startDate.ToString() + "' AS date)," +
                    "duration = '" + duration + "', price = '" + price + "', status = '" + status + "' WHERE tourID=" + tourID + ";";

                reader.Close();
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                Response.Redirect("main.aspx");
            }

            else
            {
                outcome.Text = "COULD NOT FIND A TOUR ID ASSOCIATED WITH THIS USER";
            }



        }

        protected void createBooking_Click(object sender, EventArgs e)
        {
            int tourID = int.Parse(createBooking.Text);

            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());

            conn.Open();

            string query = "INSERT INTO bookings (userID, tourID) VALUES('" + Session["userID"].ToString() + "', " + tourID + ");";
            cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();

        }


        protected void editProfile_Click(object sender, EventArgs e)
        {
            int phone = int.Parse(newPhoneNumber.Text);
            string description = newDescription.Text;

            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());

            conn.Open();

            string query = "UPDATE users SET phoneNumber=" + phone + ", description ='" + description + "' WHERE userID='" + Session["userID"].ToString() + "';";
            cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            reader.Close();
            Response.Redirect("main.aspx");

        }




        protected void filterBooks_Click(object sender, EventArgs e)
        {
            string query;
            string selected = rdoFilter.SelectedItem.Value.ToString();

            if (selected == "Genre")
            {
                query = "SELECT * FROM BOOKS ORDER BY bookGenre";
            }

            else if (selected == "Price")
            {
                query = "SELECT * FROM BOOKS ORDER BY bookPrice";
            }

            else
            {
                query = "SELECT * FROM BOOKS ORDER BY author";
            }


            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());

            conn.Open();

            cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            GridView1.DataSource = reader;
            GridView1.DataBind();
        }



        protected void btnAddBook_Click(object sender, EventArgs e)
        {

                int bookID = int.Parse(add_bookID.Text);
                string bookName = add_bookName.Text;
                string bookGenre = add_bookGenre.Text;
                int bookPrice = int.Parse(addPrice.Value);
                string author = add_bookAuthor.Text;

                SqlConnection conn = null;
                SqlCommand cmd = null;
                SqlDataReader reader = null;

                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());

                conn.Open();

                string query = "INSERT INTO books VALUES('" + bookID + "', '" + bookName + "', '" + bookGenre + "', '" + bookPrice + "', '" + author + "')";

                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                Response.Redirect("main.aspx");

        }


        protected void btnEditBook_Click(object sender, EventArgs e)
        {

                int bookID = int.Parse(target_bookID.Text);
                string bookName = new_bookName.Text;
                string bookGenre = new_bookGenre.Text;
                string author = new_bookAuthor.Text;
                int bookPrice = int.Parse(spinner.Value);

                SqlConnection conn = null;
                SqlCommand cmd = null;
                SqlDataReader reader = null;

                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vetoTours"].ToString());

                conn.Open();

                string query = "UPDATE books SET bookName = '" + bookName + "', bookGenre = '" + bookGenre + "', bookPrice = " + bookPrice + ", author = '" + author + "' WHERE bookID =" + bookID + ";";

                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();

        }

    }
}