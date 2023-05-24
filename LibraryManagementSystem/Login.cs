using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{

    public class Login
    {
        public SqlConnection con;
        public SqlDataAdapter user_Adp;
        public DataSet ds;

        public Login()
        {
             con = new SqlConnection("Server=US-8ZBJZH3; database=LibMS; Integrated Security=true");
             ds = new DataSet();
             user_Adp = new SqlDataAdapter("SELECT * FROM Login_Details", con);
             user_Adp.Fill(ds, "Login_Details");
         }
        public bool GetLogin(string userid, string password)
        {
            
            string query = $"Userr_Id = '{userid}' AND Passwordd = '{password}'";

            DataRow[] rows = ds.Tables["Login_Details"].Select(query);

            if (rows.Length > 0)
            {
                return true;
            }
            return false;
        }
    }
}
