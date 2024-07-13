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
    public class OrderTransactionClass
    {
        public OleDbConnection _conn;
        public OrderTransactionClass(OleDbConnection conn)
        {
            _conn = conn;
        }

        public ResponseModel InsertPosTransaction(PosModel model, bool isVoid, bool isDebtCredit, string remarks)
        {
            try
            {
                remarks = isDebtCredit ? "Debt Credit : "+ remarks: remarks;
                model.pos_id = DateTime.Now.ToString("yyyyMMddHHmmss");
                string query = "INSERT INTO [tbl_pos] (pos_id, total_inv, total_price, cash, change, tdt, upd, userid, is_void, is_debt_credit, remarks)" +
                    "VALUES (@pos_id, @total_inv, @total_price, @cash, @change, @tdt, @upd, @userid, @isvoid, @isdebtcredit, @remarks)";
                using (OleDbCommand command = new OleDbCommand(query, _conn))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@pos_id", model.pos_id.ToString());
                    command.Parameters.AddWithValue("@total_inv", Int32.Parse(model.total_inv));
                    command.Parameters.AddWithValue("@total_price", model.total_price);
                    command.Parameters.AddWithValue("@cash", model.cash);
                    command.Parameters.AddWithValue("@change", model.change);
                    command.Parameters.AddWithValue("@tdt", DateTime.Now.ToString("MM/dd/yyyy"));
                    command.Parameters.AddWithValue("@upd", DateTime.Now.ToString("MM/dd/yyyy"));
                    command.Parameters.AddWithValue("@userid", model.user_id.ToString());
                    command.Parameters.AddWithValue("@isvoid", isVoid);
                    command.Parameters.AddWithValue("@isdebtcredit", isDebtCredit);
                    command.Parameters.AddWithValue("@remarks", remarks);
                    _conn.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    _conn.Close();
                    if (rowsAffected > 0)
                    {
                        return new ResponseModel
                        {
                            is_catch = false,
                            is_Success = true,
                            message = model.pos_id,
                            title = "Success"

                        };
                    }
                    else
                    {
                        return new ResponseModel
                        {
                            is_catch = false,
                            is_Success = false,
                            message = "Unsuccessfully Inserted!!",
                            title = "Error"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _conn.Close();
                return new ResponseModel
                {
                    is_catch = true,
                    is_Success = false,
                    message = ex.Message
                };
            }
        }
        public ResponseModel InsertInvSold(DataTable dtorder, DataTable dtInv, string transactionID, bool is_void)
        {

            try
            {
                foreach (DataRow dr in dtorder.Rows)
                {                    
                        string query = "INSERT INTO [tbl_inv_sold] (product_id, price_cost, selling_price, tdt, qty, transaction_id, is_cancel, remarks)" +
                            "VALUES (@product_id, @price_cost, @selling_price, @tdt, @qty, @transaction_id, @is_cancel, @remarks)";

                        using (OleDbCommand command = new OleDbCommand(query, _conn))
                        {
                            _conn.Open();
                            command.Parameters.AddWithValue("@product_id", dr["InvID"].ToString());
                            command.Parameters.AddWithValue("@total_price", dr["TotalPrice"].ToString());
                            command.Parameters.AddWithValue("@selling_price", dr["Price"].ToString());
                            command.Parameters.AddWithValue("@tdt", DateTime.Now.ToString("MM/dd/yyyy"));
                            command.Parameters.AddWithValue("@qty", dr["QTY"].ToString());
                            command.Parameters.AddWithValue("@transaction_id", transactionID);
                            command.Parameters.AddWithValue("@is_cancel", is_void? true : bool.Parse(dr["IsCancel"].ToString()));
                            command.Parameters.AddWithValue("@remarks", dr["Remarks"].ToString());
                            int rowsAffected = command.ExecuteNonQuery();
                            _conn.Close();

                            var is_deduct = is_void ? true : bool.Parse(dr["IsCancel"].ToString());
                            if (!is_deduct)
                            {
                                DeductInvStock(dr["InvID"].ToString());
                            }
                        }                
                }
                return new ResponseModel
                {
                    is_catch = false,
                    is_Success = true,
                    message = "Successfully Inserted",
                    title = "Success"
                };
            }
            catch (Exception ex)
            {
                _conn.Close();
                return new ResponseModel
                {
                    is_catch = true,
                    is_Success = false,
                    message = ex.Message
                };
            }
        }

        public  DataTable getPOSlist()
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT " +
                    " * FROM [tbl_pos] WHERE tdt = @tdt";
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

        public DataTable getTransactionDetails(string posID)
        {
            DataTable dt = new DataTable();
            string query = "SELECT " +
                " PD.item_id AS ID, " +
                " PD.item_name AS ItemName, " +
                " PD.item_description AS ItemDescription, " +
                " I.price_cost AS PriceCost, " +
                " I.selling_price AS SellingPrice " +
                " FROM [tbl_inv_sold] AS I " +
                " INNER JOIN [tbl_products] AS PD ON (I.product_id = PD.item_id) " +
                " WHERE I.transaction_id = @pos_id";
            using (OleDbCommand command = new OleDbCommand(query, _conn))
            {
                _conn.Open(); 
                command.Parameters.AddWithValue("@pos_id", posID);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                da.Fill(dt);
                _conn.Close();
            }
                return dt;
        }

        public DataTable getPOSlist(DateTime from , DateTime to, bool isVoid, bool isCredit)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT " +
                    " * FROM [tbl_pos] WHERE is_void = @isVoid AND is_debt_credit = @isCredit AND " +
                    " tdt BETWEEN @from and @to";
                if(!isVoid && !isCredit)
                {
                    query = "SELECT " +
                    " * FROM [tbl_pos] WHERE " +
                    " tdt BETWEEN @from and @to";
                }
                using (OleDbCommand command = new OleDbCommand(query, _conn))
                {
                    _conn.Open();
                    if(isVoid || isCredit)
                    {
                        command.Parameters.AddWithValue("@isVoid", isVoid);
                        command.Parameters.AddWithValue("@isCredit", isCredit);
                    }
                    command.Parameters.AddWithValue("@from", from.ToString("MM/dd/yyyy"));
                    command.Parameters.AddWithValue("@to", to.ToString("MM/dd/yyyy"));
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
        public void DeductInvStock(string productID)
        {
            try
            {
                string query = "UPDATE [tbl_inv] SET available_inv = available_inv -1, sold_inv = sold_inv + 1 WHERE product_id = @id";

                using (OleDbCommand command = new OleDbCommand(query, _conn))
                {
                    _conn.Open();
                    command.Parameters.AddWithValue("@id", productID);
                    int rowsAffected = command.ExecuteNonQuery();
                    _conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
