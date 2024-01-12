using MySql.Data.MySqlClient;
using System.Data;

namespace Kenya_Wear.Models
{
    public class DBHandler : IDisposable
    {
        public readonly IConfiguration config;
        private MySqlConnection connection;
        private string connectionstring;

        public DBHandler(string connstring)
        {
            connection = new MySqlConnection(connstring);
            this.connection.Open();
            this.connectionstring = connstring;
        }

        public void Dispose()
        {
            connection.Close();
        }

        #region DB
        public enum DataBaseObject
        {
            HostDB,
            BrokerDB
        }

        public string GetDataBaseConnection(DataBaseObject dbobject)
        {
            string connection_string = connectionstring;
            switch (dbobject)
            {
                case DataBaseObject.HostDB:
                    connection_string = connectionstring;
                    break;
                default:
                    connection_string = connectionstring;
                    break;

            }
            return connection_string;
        }
        #endregion

        #region Authentication
        public DataTable ValidateUserLogin(string user_type, string email_address)
        {
            DataTable dt = new DataTable();

            try
            {
                using MySqlConnection conn = new MySqlConnection(GetDataBaseConnection(DataBaseObject.HostDB));
                using MySqlCommand cmd = new MySqlCommand("validate_login", conn);
                using MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", email_address);
                cmd.Parameters.AddWithValue("@profiletype", user_type);
                sd.Fill(dt);
            }
            catch (Exception ex)
            {

                throw;
            }
            return dt;
        }
        #endregion
    }
}
