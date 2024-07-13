using POS_Jave2021.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Jave2021.Class
{
    public class ProductClass
    {
        OleDbConnection _conn;
        public ProductClass(OleDbConnection conn)
        {
            _conn = conn;
        }

        public ResponseModel Insert(ProductDetails model)
        {
            string query = "INSERT INTO [tbl_products] (item_id, item_name, item_description, item_catigory, cdt, udt, is_active, is_deleted) " +
                " VALUES (@item_id, @item_name, @item_description, @item_catigory, @cdt, @udt, @is_active, @is_deleted)";
            using (OleDbCommand command = new OleDbCommand(query, _conn))
            {
                _conn.Open(); 
                command.Parameters.AddWithValue("@item_id", model.item_id);
                command.Parameters.AddWithValue("@item_name", model.item_name);
                command.Parameters.AddWithValue("@item_description", model.item_description);
                command.Parameters.AddWithValue("@item_catigory", model.item_catigory);
                command.Parameters.AddWithValue("@cdt", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                command.Parameters.AddWithValue("@udt", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                command.Parameters.AddWithValue("@is_active", true);
                command.Parameters.AddWithValue("@is_deleted", false);
                int rowsAffected = command.ExecuteNonQuery();
                _conn.Close();
                if (rowsAffected > 0)
                {
                    return new ResponseModel
                    {
                        is_catch = false,
                        is_Success = true,
                        message = "Saved Successfully!",
                        title = "Success!",
                    };
                }
                else
                {
                    return new ResponseModel
                    {
                        is_catch = false,
                        is_Success = true,
                        message = "Saved Unsuccessfully!",
                        title = "Error!",
                    };
                }
            }                
        }


        public ResponseModel Update(ProductDetails model)
        {
            string query = "UPDATE [tbl_products] SET  item_name = @item_name, item_description = @item_description, " +
                " item_catigory = @item_catigory, udt = @udt " +
                " WHERE item_id = @item_id";
            using (OleDbCommand command = new OleDbCommand(query, _conn))
            {
                _conn.Open();
                command.Parameters.AddWithValue("@item_name", model.item_name);
                command.Parameters.AddWithValue("@item_description", model.item_description);
                command.Parameters.AddWithValue("@item_catigory", model.item_catigory);
                command.Parameters.AddWithValue("@udt", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                command.Parameters.AddWithValue("@item_id", model.item_id.Trim());
                int rowsAffected = command.ExecuteNonQuery();
                _conn.Close();
                if (rowsAffected > 0)
                {
                    return new ResponseModel
                    {
                        is_catch = false,
                        is_Success = true,
                        message = "Updated Successfully!",
                        title = "Success!",
                    };
                }
                else
                {
                    return new ResponseModel
                    {
                        is_catch = false,
                        is_Success = true,
                        message = "Updated Unsuccessfully!",
                        title = "Error!",
                    };
                }
            }
        }

        public ResponseModel delete(string id)
        {
            string query = "UPDATE [tbl_products] SET  is_active = @is_active, is_deleted = @is_deleted, " +
                " udt = @udt " +
                " WHERE item_id = @item_id";
            using (OleDbCommand command = new OleDbCommand(query, _conn))
            {
                _conn.Open();
                command.Parameters.AddWithValue("@is_active", false);
                command.Parameters.AddWithValue("@is_deleted", true);
                command.Parameters.AddWithValue("@udt", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                command.Parameters.AddWithValue("@item_id", id.Trim());
                int rowsAffected = command.ExecuteNonQuery();
                _conn.Close();
                if (rowsAffected > 0)
                {
                    return new ResponseModel
                    {
                        is_catch = false,
                        is_Success = true,
                        message = "Deleted Successfully!",
                        title = "Success!",
                    };
                }
                else
                {
                    return new ResponseModel
                    {
                        is_catch = false,
                        is_Success = true,
                        message = "Deleted Unsuccessfully!",
                        title = "Error!",
                    };
                }
            }
        }

        public DataTable getProductCategor()
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT ID, '('&shortcode & ') ' & name as Name FROM [tbl_catigory] where is_active = @is_active AND is_deleted = @is_deleted";
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
