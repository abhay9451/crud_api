using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace crud_api.Models.DbAccess
{
    public class AdminDbAccess
    {
        public ConnectDb db = new ConnectDb();
        public string LoginAdmin(AdminLogin admin)
        {
            string message = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand("login_user", db.connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if(db.connection.State==System.Data.ConnectionState.Closed)
                
                db.connection.Open();
                cmd.Parameters.AddWithValue("@email", admin.Email);
                cmd.Parameters.AddWithValue("@password", admin.Password);
                int r = (int)cmd.ExecuteScalar();
                if(r==1)
                {
                    message = "ok";
                }
                    else
                    {
                        message = "fail";
                    }
                db.connection.Close();

            }
            catch (Exception ex)
            {

                message = ex.Message;
            }
            return message;
        }
    }
}
