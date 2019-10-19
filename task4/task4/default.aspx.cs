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

namespace task4
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["task4"].ToString());

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string uid = txtUserName.Text;
                string pass = txtPassword.Text;
                con.Open();
                string query = "SELECT * from users where userID='" + uid + "' and password='" + pass + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    lblStatus.Text = "Logged in successfully! ";
                    Session["loggedIn"] = "true";
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

    }

}
