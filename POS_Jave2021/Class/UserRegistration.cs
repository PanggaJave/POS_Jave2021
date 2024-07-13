using MySql.Data.MySqlClient;
using POS_Jave2021.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Jave2021
{
    public class UserRegistration
    {        
        public ResponseModel register(userModel model, OleDbConnection conn)
        {
            try
            {

                string query = "INSERT INTO [tbl_users] (user_id, username, [password], lastname, firstname, middlename, phone_no, email, usertype, auth_factor, is_active, is_deleted) " +
               "VALUES (@user_id, @username, @password, @lastname, @firstname, @middlename, @phone_no, @email, @usertype, @auth_factor, @is_active, @is_deleted)";

                using (OleDbCommand command = new OleDbCommand(query, conn))
                {

                        conn.Open();
                        //command.Parameters.AddWithValue("@ID", 0);
                        command.Parameters.AddWithValue("@user_id", model.user_id);
                        command.Parameters.AddWithValue("@username", model.username);
                        command.Parameters.AddWithValue("@password", model.password);
                        command.Parameters.AddWithValue("@lastname", model.lastname);
                        command.Parameters.AddWithValue("@firstname", model.firstname);
                        command.Parameters.AddWithValue("@middlename", model.middlename);
                        command.Parameters.AddWithValue("@phone_no", model.phone_no);
                        command.Parameters.AddWithValue("@email", model.email);
                        command.Parameters.AddWithValue("@usertype", model.usertype);
                        command.Parameters.AddWithValue("@auth_factor", model.auth_factor);
                        command.Parameters.AddWithValue("@is_active", model.is_active);
                        command.Parameters.AddWithValue("@is_deleted", false);

                        int rowsAffected = command.ExecuteNonQuery();
                        conn.Close();
                     if(rowsAffected > 0)
                    {
                        return new ResponseModel{
                            is_catch = false,
                            is_Success = true,
                            message = "User Successfully Inserted!!",
                            title = "Success"
                           
                        };
                    }
                    else
                    {
                        return new ResponseModel {
                            is_catch = false,
                            is_Success = false,
                            message = "User Unsuccessfully Inserted!!",
                            title = "Success"
                        };
                    }
                }
            
            
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    is_catch = true,
                    is_Success = false,
                    message = ex.Message
                };
            }
        }

        public static DataTable getListOfUser(OleDbConnection _conn)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "Select user_id, username, lastname, firstname, middlename, email, phone_no, usertype from [tbl_users]";
                using (OleDbCommand command = new OleDbCommand(query, _conn))
                {
                    _conn.Open();
                    command.Parameters.AddWithValue("@is_active", true);
                    command.Parameters.AddWithValue("@is_deleted", false);
                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    da.Fill(dt);
                    _conn.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
