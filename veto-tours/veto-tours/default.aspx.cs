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
						Session["userType"] = "user";

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
			try
			{
				string uid = regUserName.Text;
				string pass = regPassword.Text;
				string name = regRealName.Text;
				string email = regEmail.Text;
				int phone = int.Parse(regPhone.Text);
				string desc = regDescription.Text;
				string accountType = "user";
				int status = 0;

				SqlCommand cmd = null;
				SqlDataReader reader = null;
				con.Open();

				string query = "INSERT INTO users VALUES('" + uid + "', '" + pass + "', '" + name + "', '" + email + "', '" + phone + "', '" + accountType + "', '" + desc + "', '" + status + "')";

				cmd = new SqlCommand(query, con);
				reader = cmd.ExecuteReader();

				regStatus.Text = "Successfully Registered!";

			}

			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}

		}

	}


}
