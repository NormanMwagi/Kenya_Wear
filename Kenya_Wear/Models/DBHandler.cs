using ComplaintManagement.Helpers;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using NLog;
using System.Data;


namespace Kenya_Wear.Models
{
    public class DBHandler : IDisposable
    {
        public readonly IConfiguration config;
        private MySqlConnection connection;
        private string connectionstring;
        private static Logger logger = LogManager.GetCurrentClassLogger();

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

                // Log the exception
                logger.Error(ex, "An error occurred: {ErrorMessage}", ex.Message);
            }
            return dt;
        }
        public bool AddAuditTrail(AuditTrailModel mymodel)
        {
            try
            {
                int i = 0;
                using (MySqlConnection connect = new MySqlConnection(GetDataBaseConnection(DataBaseObject.HostDB)))
                {
                    using MySqlCommand cmd = new MySqlCommand("add_audit_trail", connect);
                    connect.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", MySqlDbType.Int64).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@in_user_name", mymodel.user_name);
                    cmd.Parameters.AddWithValue("@in_action_type", mymodel.action_type);
                    cmd.Parameters.AddWithValue("@in_action_description", mymodel.action_description);
                    cmd.Parameters.AddWithValue("@in_page_accessed", mymodel.page_accessed);
                    cmd.Parameters.AddWithValue("@in_client_ip_address", mymodel.client_ip_address);
                    cmd.Parameters.AddWithValue("@in_session_id", mymodel.session_id);
                    i = (int)cmd.ExecuteNonQuery();
                }

                if (i >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                logger.Error("ERROR", "AddAuditTrail | Exception ->" + ex.Message);
                return false;
            }
        }
        //Users
        public List<PortalUsersModel> GetPortalUsers()
        {
            List<PortalUsersModel> recordlist = new List<PortalUsersModel>();

            try
            {
                DataTable dt = new DataTable();

                dt = GetRecords("portal_users");

                foreach (DataRow dr in dt.Rows)
                {
                    recordlist.Add(
                    new PortalUsersModel
                    {
                        id = Convert.ToInt32(dr["id"]),
                        role_id = Convert.ToInt32(dr["role_id"]),
                        mobile = Convert.ToString(dr["mobile"]),
                        email = Convert.ToString(dr["email"]),
                        name = Convert.ToString(dr["name"]),
                        password = Convert.ToString(dr["password"]),
                        avatar = Convert.ToString(dr["avatar"]),
                        locked = Convert.ToBoolean(dr["locked"]),
                        google_authenticate = Convert.ToBoolean(dr["google_authenticate"]),
                        sec_key = Convert.ToString(dr["sec_key"])
                    });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, "An error occurred: {ErrorMessage}", ex.Message);
            }

            return recordlist;
        }

        public bool AddPortalUser(PortalUsersModel mymodel)
        {
            try
            {
                int i = 0;
                using (MySqlConnection connect = new MySqlConnection(GetDataBaseConnection(DataBaseObject.HostDB)))
                {
                    using MySqlCommand cmd = new MySqlCommand("add_portal_user", connect);
                    connect.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@in_role_id", mymodel.role_id);
                    cmd.Parameters.AddWithValue("@in_mobile", mymodel.mobile);
                    cmd.Parameters.AddWithValue("@in_email", mymodel.email);
                    cmd.Parameters.AddWithValue("@in_name", mymodel.name);
                    cmd.Parameters.AddWithValue("@in_password", mymodel.password);
                    cmd.Parameters.AddWithValue("@in_avatar", mymodel.avatar);
                    cmd.Parameters.AddWithValue("@in_locked", mymodel.locked);
                    cmd.Parameters.AddWithValue("@in_google_authenticate", mymodel.google_authenticate);
                    cmd.Parameters.AddWithValue("@in_created_by", mymodel.created_by);
                    cmd.Parameters.AddWithValue("@in_sec_key", mymodel.sec_key);

                    i = (int)cmd.ExecuteNonQuery();
                }

                if (i >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, "An error occurred: {ErrorMessage}", ex.Message);
                return false;
            }
        }

        public bool UpdatePortalUser(PortalUsersModel mymodel)
        {
            try
            {
                int i = 0;
                using (MySqlConnection connect = new MySqlConnection(GetDataBaseConnection(DataBaseObject.HostDB)))
                {
                    using MySqlCommand cmd = new MySqlCommand("update_portal_user", connect);
                    connect.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@in_id", mymodel.id);
                    cmd.Parameters.AddWithValue("@in_role_id", mymodel.role_id);
                    cmd.Parameters.AddWithValue("@in_mobile", mymodel.mobile);
                    cmd.Parameters.AddWithValue("@in_email", mymodel.email);
                    cmd.Parameters.AddWithValue("@in_name", mymodel.name);
                    cmd.Parameters.AddWithValue("@in_password", mymodel.password);
                    cmd.Parameters.AddWithValue("@in_avatar", mymodel.avatar);
                    cmd.Parameters.AddWithValue("@in_locked", mymodel.locked);
                    cmd.Parameters.AddWithValue("@in_google_authenticate", mymodel.google_authenticate);
                    cmd.Parameters.AddWithValue("@in_sec_key", mymodel.sec_key);
                    i = (int)cmd.ExecuteNonQuery();
                }

                if (i >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, "An error occurred: {ErrorMessage}", ex.Message);
                return false;
            }
        }
        //End Users

        //Register
        public List<RegistrationModel> GetExternalPortalUsers()
        {
            List<RegistrationModel> recordlist = new List<RegistrationModel>();

            try
            {
                DataTable dt = new DataTable();

                dt = GetRecords("external_portal_users");

                foreach (DataRow dr in dt.Rows)
                {
                    recordlist.Add(
                    new RegistrationModel
                    {
                        id = Convert.ToInt32(dr["id"]),
                        role_id = Convert.ToInt32(dr["role_id"]),
                        mobile = Convert.ToString(dr["mobile"])!,
                        email = Convert.ToString(dr["email"])!,
                        name = Convert.ToString(dr["name"])!,
                        password = Convert.ToString(dr["password"])!,
                        avatar = Convert.ToString(dr["avatar"])!,
                        locked = Convert.ToBoolean(dr["locked"]),
                        google_authenticate = Convert.ToBoolean(dr["google_authenticate"]),
                        sec_key = Convert.ToString(dr["sec_key"])!
                    });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, "An error occurred: {ErrorMessage}", ex.Message);
            }

            return recordlist;
        }

        public bool RegisterUser(RegistrationModel model)
        {
            try
            {

                using (MySqlConnection connect = new MySqlConnection(GetDataBaseConnection(DataBaseObject.HostDB)))
                {
                    using MySqlCommand cmd = new MySqlCommand("add_external_portal_user", connect);
                    connect.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", MySqlDbType.Int64).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@in_name", model.name);
                    cmd.Parameters.AddWithValue("@in_email", model.email);
                    cmd.Parameters.AddWithValue("@in_password", model.password);
                    cmd.Parameters.AddWithValue("@in_mobile", model.mobile);
                    cmd.Parameters.AddWithValue("@in_avatar", model.avatar);
                    cmd.Parameters.AddWithValue("@in_locked", model.locked);
                    cmd.Parameters.AddWithValue("@in_sec_key", model.sec_key);
                    cmd.Parameters.AddWithValue("@in_google_authenticate", model.google_authenticate);
                    cmd.Parameters.AddWithValue("@in_role_id", model.role_id);
                    cmd.ExecuteNonQuery();

                    Int64 id = Convert.ToInt64(cmd.Parameters["@id"].Value);

                    if (id > 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, "An error occurred: {ErrorMessage}", ex.Message);
                return false;
            }
        }
        public bool UpdateExternalPortalUser(RegistrationModel mymodel)
        {
            try
            {
                int i = 0;
                using (MySqlConnection connect = new MySqlConnection(GetDataBaseConnection(DataBaseObject.HostDB)))
                {
                    using MySqlCommand cmd = new MySqlCommand("update_external_portal_user", connect);
                    connect.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@in_id", mymodel.id);
                    cmd.Parameters.AddWithValue("@in_role_id", mymodel.role_id);
                    cmd.Parameters.AddWithValue("@in_mobile", mymodel.mobile);
                    cmd.Parameters.AddWithValue("@in_email", mymodel.email);
                    cmd.Parameters.AddWithValue("@in_name", mymodel.name);
                    cmd.Parameters.AddWithValue("@in_password", mymodel.password);
                    cmd.Parameters.AddWithValue("@in_avatar", mymodel.avatar);
                    cmd.Parameters.AddWithValue("@in_locked", mymodel.locked);
                    cmd.Parameters.AddWithValue("@in_google_authenticate", mymodel.google_authenticate);
                    cmd.Parameters.AddWithValue("@in_sec_key", mymodel.sec_key);
                    i = (int)cmd.ExecuteNonQuery();
                }

                if (i >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, "An error occurred: {ErrorMessage}", ex.Message);
                return false;
            }
        }

        #endregion

        public DataTable GetRecords(string module, string param1 = "", string param2 = "")
        {
            DataTable dt = new DataTable();

            try
            {
                using MySqlConnection connect = new MySqlConnection(GetDataBaseConnection(DataBaseObject.HostDB));
                using MySqlCommand cmd = new MySqlCommand("get_records", connect);
                using MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
                connect.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@module", module);
                cmd.Parameters.AddWithValue("@param1", param1);
                cmd.Parameters.AddWithValue("@param2", param2);
                sd.Fill(dt);
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, "An error occurred: {ErrorMessage}", ex.Message);
            }

            return dt;
        }

        public DataTable GetUnapprovedRecords(string module, string param1 = "")
        {
            DataTable dt = new DataTable();

            try
            {
                using MySqlConnection connect = new MySqlConnection(GetDataBaseConnection(DataBaseObject.HostDB));
                using MySqlCommand cmd = new MySqlCommand("get_records_unapproved", connect);
                using MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@module", module);
                //cmd.Parameters.AddWithValue("@param1", param1);
                sd.Fill(dt);
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, "An error occurred: {ErrorMessage}", ex.Message);
            }

            return dt;
        }

        public DataTable GetRecordsById(string module, Int64 id, string param1 = "", string param2 = "")
        {
            DataTable dt = new DataTable();

            try
            {
                using MySqlConnection connect = new MySqlConnection(GetDataBaseConnection(DataBaseObject.HostDB));
                using MySqlCommand cmd = new MySqlCommand("get_records_by_id", connect);
                using MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
                connect.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@module", module);
                cmd.Parameters.AddWithValue("@id", id);
                sd.Fill(dt);
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, "An error occurred: {ErrorMessage}", ex.Message);
            }

            return dt;
        }

        public string GetScalarItem(string sql)
        {
            string scalaritem = "";

            try
            {
                using MySqlConnection connect = new MySqlConnection(GetDataBaseConnection(DataBaseObject.HostDB));
                using MySqlCommand command = new MySqlCommand(sql, connect);
                connect.Open();
                scalaritem = (string)(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, "An error occurred: {ErrorMessage}", ex.Message);
                scalaritem = "";
            }
            return scalaritem;
        }

        public bool DeleteRecord(Int64 id, int deleted_by, string module)
        {
            try
            {
                int i = 0;
                using (MySqlConnection connect = new MySqlConnection(GetDataBaseConnection(DataBaseObject.HostDB)))
                {
                    using MySqlCommand cmd = new MySqlCommand("delete_records", connect);
                    connect.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@in_recordid", id);
                    cmd.Parameters.AddWithValue("@in_deleted_by", deleted_by);
                    cmd.Parameters.AddWithValue("@in_module", module);
                    i = (int)cmd.ExecuteNonQuery();
                }

                if (i >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, "An error occurred: {ErrorMessage}", ex.Message);
                return false;
            }
        }

    }
}
