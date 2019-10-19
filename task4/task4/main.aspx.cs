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

namespace task4
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

                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["task4"].ToString());

                conn.Open();

                string query = "SELECT * FROM books";

                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                GridView1.DataSource = reader;
                GridView1.DataBind();


            }
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

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["task4"].ToString());

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

                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["task4"].ToString());

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

                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["task4"].ToString());

                conn.Open();

                string query = "UPDATE books SET bookName = '" + bookName + "', bookGenre = '" + bookGenre + "', bookPrice = " + bookPrice + ", author = '" + author + "' WHERE bookID =" + bookID + ";";

                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                Response.Redirect("main.aspx");

        }

    }
}