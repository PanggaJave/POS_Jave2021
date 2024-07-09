using POS_Jave2021.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Jave2021.Class
{
    public class InventoryClass
    {
        public static OleDbConnection _conn = null;
        public InventoryClass(OleDbConnection conn)
        {
            _conn = conn;
        }

        public DataTable getListofInventory()
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "Select inv.inv_id as ID, inv.product_id as ProductID, pro.item_name as Name, pro.item_description as Description, inv.selling_price as Price, inv.available_inv as Stocks from [tbl_inv] as inv inner join [tbl_products] as pro on inv.product_id = pro.item_id" +
                    " where inv.available_inv > 0";
                using (OleDbCommand command = new OleDbCommand(query, _conn))
                {
                    _conn.Open();
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

        public async Task<DataTable> getInvSold()
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT * FROM [tbl_inv_sold] WHERE tdt = @tdt";
                using (OleDbCommand command = new OleDbCommand(query, _conn))
                {
                    _conn.Open();
                    command.Parameters.AddWithValue("@tdt", DateTime.Now.ToString("MM/dd/yyyy"));
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

        public DataTable customOrderLayout()
        { 
            var dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("InvID");
            dt.Columns.Add("Name");
            dt.Columns.Add("QTY");
            dt.Columns.Add("Price");
            dt.Columns.Add("TotalPrice");
            dt.Columns.Add("IsCancel");
            dt.Columns.Add("Remarks");
            return dt;
        }

        public DataTable getlistOfAllInventory()
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT * FROM [tbl_inv]";
                using (OleDbCommand command = new OleDbCommand(query, _conn))
                {
                    _conn.Open();
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

        public DataTable getlistOfProductList() 
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT * FROM [tbl_products]";
                using (OleDbCommand command = new OleDbCommand(query, _conn))
                {
                    _conn.Open();
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

        public ResponseModel updateInventory(InventoryModel model)
        {
            try
            {
                string query = "UPDATE [tbl_inv] set " +
                    "product_id = @product_id, " +
                    "price_cost = @price_cost, " +
                    "selling_price = @selling_price, " +
                    "overall_inv = overall_inv + @available_inv, " +
                    "available_inv = available_inv + @available_inv, " +
                    "udt = @tdt " +
                    "Where inv_id = @inv_id";
                using (OleDbCommand command = new OleDbCommand(query, _conn))
                {
                    _conn.Open();
                    command.Parameters.AddWithValue("@product_id", model.product_id);
                    command.Parameters.AddWithValue("@price_cost", model.price_cost);
                    command.Parameters.AddWithValue("@selling_price", model.selling_price);
                    command.Parameters.AddWithValue("@available_inv", model.available_inv);
                    command.Parameters.AddWithValue("@tdt", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                    command.Parameters.AddWithValue("@inv_id", model.inv_id);
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
                            is_Success = false,
                            message = "Update Unsuccessfully!",
                            title = "Error!"
                        };                       
                    }
                }                    
            }
            catch (Exception ex)
            {
                return new ResponseModel {
                    is_catch = true,
                    is_Success = false,
                    message = ex.Message,
                    title = "Catch Error!",                    
                };
            }
        }

        public ResponseModel insertInventory(InventoryModel model)
        {
            try
            {
                string query = "INSERT INTO [tbl_inv] " +
                    "(inv_id, product_id, price_cost, selling_price, cdt, udt, overall_inv, sold_inv, available_inv) " +
                    "VALUES " +
                    "(@inv_id, @product_id, @price_cost, @selling_price, @cdt, @udt, @overall_inv, 0, @available_inv)";
                using (OleDbCommand command = new OleDbCommand(query, _conn))
                {
                    _conn.Open();
                    command.Parameters.AddWithValue("@inv_id", model.inv_id);
                    command.Parameters.AddWithValue("@product_id", model.product_id);
                    command.Parameters.AddWithValue("@price_cost", model.price_cost);
                    command.Parameters.AddWithValue("@selling_price", model.selling_price);
                    command.Parameters.AddWithValue("@cdt", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                    command.Parameters.AddWithValue("@udt", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                    command.Parameters.AddWithValue("@overall_inv", model.available_inv);
                    command.Parameters.AddWithValue("@available_inv", model.available_inv);
                    int rowsAffected = command.ExecuteNonQuery();
                    _conn.Close();
                    if (rowsAffected > 0)
                    {
                        return new ResponseModel
                        {
                            is_catch = false,
                            is_Success = true,
                            message = "Insert Successfully!",
                            title = "Success!",
                        };
                    }
                    else
                    {
                        return new ResponseModel
                        {
                            is_catch = false,
                            is_Success = false,
                            message = "Insert Unsuccessfully!",
                            title = "Error!"
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
                    message = ex.Message,
                    title = "Catch Error!",
                };
            }
        }
    }
}
