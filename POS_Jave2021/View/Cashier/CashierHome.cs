using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json;
using POS_Jave2021.Class;
using POS_Jave2021.Model;
using POS_Jave2021.View.Cashier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Jave2021.View
{
    public partial class CashierHome : Form
    {
        logsClass _logClass;
        OleDbConnection _conn;
        OrderTransactionClass _otClass;
        public static DataTable _userDetails;
        public static string _userDetailsName;
        public static DataTable _dtInv;
        public static DataTable _dtOrders;
        customOrderLayoutModel orderModels;
        public decimal _totalOrderAmount;
        public static decimal _cashorder;
        public static decimal _cashChange;
        public static string _remarks = string.Empty;
        cashierBtnEnabilityModel btnEM = new cashierBtnEnabilityModel();
        InventoryClass _invClass;

        bool dtgribBTN = false;
        /// Shift Out
        DataTable _dtMySalesInv = new DataTable();
        DataTable _dtMySalesPOS = new DataTable();
        DataTable _dtMyShiftIn = new DataTable();
        shiftstart _ssClass;
        shiftOffModel _soModel;
        public static DataTable _userAdminDetails;
        public static bool _isAdminVerified = false;
        decimal totalDebtCreditAmount = 0;
        PosSaleReportModel _posSaleReportModel = new PosSaleReportModel();
        PosInvSoldModel _posInvSoldModel = new PosInvSoldModel();
        public CashierHome(DataTable userDetails, OleDbConnection conn)
        {
            InitializeComponent();
            _userDetails = userDetails;
            _conn = conn;
            _logClass = new logsClass();
            _otClass = new OrderTransactionClass(_conn);
            _invClass = new InventoryClass(_conn);
            _ssClass = new shiftstart(_conn);
            _userDetailsName = _userDetails.Rows[0]["user_id"].ToString() + " - " + _userDetails.Rows[0]["username"].ToString();
            _soModel = new shiftOffModel();
        }

        private void CashierTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CashierTab.SelectedIndex == 0)
            {
                CashierHome_Load(null, null);
            }
            else if (CashierTab.SelectedIndex == 1)
            {
                CashierTab_window2();
            }

            if(!dtgribBTN)
            {
                #region datagridButton PosReport
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                dgv_saleReportPOS.Columns.Add(btn); // .Columns.Add(btn);
                btn.HeaderText = "Option";
                btn.Text = "View";
                btn.Name = "btnClick";
                btn.UseColumnTextForButtonValue = true;
                btn.DisplayIndex = 0;

                DataGridViewButtonColumn btn1 = new DataGridViewButtonColumn();
                dgv_salesReportInvSold.Columns.Add(btn1); // .Columns.Add(btn);
                btn1.HeaderText = "Option";
                btn1.Text = "View";
                btn1.Name = "btnClick";
                btn1.UseColumnTextForButtonValue = true;
                btn1.DisplayIndex = 0;
                #endregion
                dtgribBTN = true;
            }
        }

        #region Cashier POS
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void logs(string log)
        {
            rtb_logs.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss -- ")+log+ "\n");
            _logClass.writelineLogs(_userDetailsName + " - "+log);
        }

        private void CashierHome_Load(object sender, EventArgs e)
        {        
            _totalOrderAmount = 0;
            _cashorder = 0;
            _cashChange = 0;
            processOnOff(true);
            txtPosScreenSearch.Focus();
            _dtInv = _invClass.getListofInventory();
            dt_productlist.DataSource = _dtInv;
            _dtOrders = _invClass.customOrderLayout();
            dt_orderlist.DataSource = _dtOrders;
            rtb_logs.HideSelection = false;
            // Btn accessability
            btnSetEnability( new cashierBtnEnabilityModel
            {
                _isProceed = false,
                _IsReset = false,
                _isReady = true,
                _IsEnterCash = false,
                _IsVoid = false,
                _IsDebtCredit = false,
            });
            processOnOff(false);
        }

        public void btnSetEnability(cashierBtnEnabilityModel model)
        {
            btnEM = model;
            btn_ready.Enabled = model._isReady;
            btn_Proceed.Enabled = model._isProceed;
            btn_reset.Enabled = model._IsReset;
            btn_EnterCash.Enabled = model._IsEnterCash;
            btn_void.Enabled = model._IsVoid;
            btnDebtCredit.Enabled = model._IsDebtCredit;
        }

        private void txtPosScreenSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                logs("SCAN PRODUCTID : " + txtPosScreenSearch.Text);
                scanProductID(txtPosScreenSearch.Text);                
                txtPosScreenSearch.Text = "";
            }
        }

        public void scanProductID(string id)
        {
            try
            {
                DataTable ret = _dtInv.AsEnumerable().Where(
                    w => w.Field<string>("InvID").Trim()  == id.Trim()).CopyToDataTable();

                if (ret.Rows.Count > 0)
                {
                    foreach (DataRow r in ret.Rows)
                    {
                        int orderID = _dtOrders.Rows.Count;
                        orderModels = new customOrderLayoutModel
                        {
                            ID = orderID + 1,
                            InvID = r["InvID"].ToString(),
                            Name = r["Name"].ToString(),
                            QTY = 1,
                            Price = decimal.Parse(r["Price"].ToString()),
                            TotalPrice = decimal.Parse(r["Price"].ToString()),
                            IsCancel = false,
                            Remarks = string.Empty,
                            
                        };
                        addRowsOrder(orderModels);
                        break;
                    }
                    logs("Product JSON: " + JsonConvert.SerializeObject(ret));
                    lbl_ordersCount.Text = _dtOrders.Rows.Count.ToString();
                }
                else
                {
                    logs("Product JSON: No Result!");
                    MessageBox.Show("No Item Found!", "No Matching Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                if(ex.Message.ToLower().Trim()  == "the source contains no datarows.")
                {
                    MessageBox.Show("No Item Found!", "No Matching Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    logs("Catch : No Item Found!");
                }
                else
                {
                    logs("Catch : " + ex.Message);
                }
            }
        }

        public void addRowsOrder(customOrderLayoutModel model)
        {
            _dtOrders.Rows.Add(model.ID, model.InvID, model.Name, model.QTY, model.Price, model.TotalPrice, model.IsCancel, model.Remarks);
            _totalOrderAmount = _totalOrderAmount + model.TotalPrice;
            lblTotalPriceOrder.Text = Convert.ToDecimal(_totalOrderAmount).ToString("C");
            dt_orderlist.DataSource = _dtOrders;

        }

        private void CashierHome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 && btnEM._IsReset)
            {
                btn_reset_Click(null, null); //btn_reset_Click
            }
            else if (e.KeyCode == Keys.F2 && btnEM._IsEnterCash)
            {
                button1_Click(null, null);
            }
            else if (e.KeyCode == Keys.R && btnEM._isReady)
            {
                btn_ready_Click(null, null );
            }
            else if (e.KeyCode == Keys.F12 && btnEM._isProceed)
            {
                button2_Click(null, null);
            }
            else if (e.KeyCode == Keys.Escape && btnEM._IsVoid)
            {
                btn_void_Click(null, null);
            }
            else if(e.KeyCode == Keys.F9)
            {
                btnDebtCredit_Click(null, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            logs("POS Enter Cash Order: Started");
            try
            {
                CashPayment cpf = new CashPayment();
                cpf.ShowDialog();
                _cashChange = _cashorder - _totalOrderAmount;
                lblCashLabel.Text = _cashorder.ToString("C");
                lblCashChange.Text = _cashChange.ToString("C");
                logs("Enter Custumer Cash: " + _cashorder.ToString("C"));
                logs("Custumer total order: " + _totalOrderAmount.ToString("C"));
                logs("Custumer Change: " + _cashChange.ToString("C"));

                btnSetEnability(new cashierBtnEnabilityModel
                {
                    _isProceed = true,
                    _IsReset = false,
                    _isReady = false,
                    _IsEnterCash = true,
                    _IsVoid = true,
                });
            }
            catch (Exception ex)
            {
                logs("POS Enter Cash Order Catch Error: " + ex.Message);
                throw ex;
            }
            logs("POS Enter Cash Order: Started");
        }

        private void btn_ready_Click(object sender, EventArgs e)
        {

            _totalOrderAmount = 0;
            _cashorder = 0;
            _cashChange = 0;
            lblTotalPriceOrder.Text = _totalOrderAmount.ToString("C");
            lblCashLabel.Text = _cashorder.ToString("C");
            lblCashChange.Text = _cashChange.ToString("C");

            _dtOrders.Rows.Clear();
            _dtOrders.AcceptChanges();
            dt_orderlist.DataSource = _dtOrders;
            btnSetEnability(new cashierBtnEnabilityModel
            {
                _isProceed = false,
                _IsReset = true,
                _isReady = false,
                _IsEnterCash = true,
                _IsVoid = false,
                _IsDebtCredit = true,
            });
            txtPosScreenSearch.Focus();
            logs("POS Ready!!");
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if(dt_orderlist.Rows.Count == 0)
            {
                MessageBox.Show("No available item in order list.", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if((_cashorder >= _totalOrderAmount))
                {
                    logs("POS Orders Transaction: Started");
                    processOnOff(true);
                    await transaction(false, false);
                    CashierHome_Load(null, null);
                    processOnOff(false);
                    logs("POS Orders Transaction: End");
                }
                else
                {
                    MessageBox.Show("Payment is not input Or lessthan compare to ordered amount! \nPlease ask the customer to pay first to proceed!!.", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public async Task transaction(bool isVoid, bool isDebtCredit)
        {
            var model = new PosModel
            {
                total_inv = dt_orderlist.Rows.Count.ToString(),
                total_price = _totalOrderAmount,
                cash = _cashorder,
                change = _cashChange,
                user_id = _userDetails.Rows[0]["user_id"].ToString()
            };
            logs("Orders POS: " + JsonConvert.SerializeObject(model));
            var response = _otClass.InsertPosTransaction(model, isVoid, isDebtCredit, _remarks);
            logs("Orders POS response: " + JsonConvert.SerializeObject(response));
            if (response.is_Success)
            {
                logs("Orders [tbl_inv_sold] : waiting");
                response = _otClass.InsertInvSold(_dtOrders, _dtInv, response.message, isVoid);
                logs("Orders [tbl_inv_sold]: " + JsonConvert.SerializeObject(response));
                if (response.is_Success)
                {
                    MessageBox.Show("Transaction Complete!", "Successfully Insert!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Transaction Incomplete!", "Unsuccessfully Insert to [tbl_inv_sold]!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Transaction Complete!", "Unsuccessfully Insert to [tbl_pos]!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void processOnOff(bool is_on)
        {
            if(is_on)
            {
                pictureBox1.Show();
                pictureBox2.Show();
                pictureBox3.Show();
                txtPosScreenSearch.Enabled = false;
                tlp_Controls.Enabled = false;
            }
            else
            {
                pictureBox1.Hide();
                pictureBox2.Hide();
                pictureBox3.Hide();
                txtPosScreenSearch.Enabled = true;
                tlp_Controls.Enabled = true;
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            _totalOrderAmount = 0;
            _cashorder = 0;
            _cashChange = 0;
            logs("POS Clear Order: Started");
            DialogResult d = MessageBox.Show("Are your sure...", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (d == DialogResult.OK)
            {
                logs("Clear Orders......");
                _remarks = string.Empty;
                Remarks fr = new Remarks();
                fr.ShowDialog();
                if(!string.IsNullOrEmpty(_remarks))
                {
                    _dtOrders.Clear();
                    _totalOrderAmount = 0;
                    _cashorder = 0;
                    _cashChange = 0;
                    lblTotalPriceOrder.Text = _totalOrderAmount.ToString("C");
                    lblCashLabel.Text = _cashorder.ToString("C");
                    _cashChange = _cashorder - _totalOrderAmount;
                    lblCashChange.Text = _cashChange.ToString("C");
                    lbl_ordersCount.Text = _dtOrders.Rows.Count.ToString();
                    logs("Remarks : " + _remarks);
                }
            }
            logs("POS Clear Order: End");
        }

        private void rtb_logs_TextChanged(object sender, EventArgs e)
        {

        }

        private void dt_orderlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void btn_void_Click(object sender, EventArgs e)
        {
            if (dt_orderlist.Rows.Count == 0)
            {
                MessageBox.Show("No available item in order list.", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {    _remarks = null;
                Remarks fr = new Remarks();
                fr.ShowDialog();
                if(!string.IsNullOrEmpty(_remarks))
                {
                    if ((_cashorder >= _totalOrderAmount))
                    {
                        logs("POS VOid Orders Transaction: Started");
                        processOnOff(true);
                        await transaction(true, false);
                        CashierHome_Load(null, null);
                        processOnOff(false);
                        logs("POS VOid Orders Transaction: End");
                    }
                    else
                    {
                        MessageBox.Show("Payment is not input Or lessthan compare to ordered amount! \nPlease ask the customer to pay first to proceed!!.", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Remark is null or empty!.", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        #endregion

        public async Task CashierTab_window2()
        {
            btn_shiftOut.Enabled = false;
            processOnOff(true);
            logs("Cashier window 2 is open...");
            try
            {
                lbl_userid.Text = _userDetails.Rows[0]["user_id"].ToString();
                lbl_username.Text = _userDetails.Rows[0]["username"].ToString();
                lbl_userfullname.Text = _userDetails.Rows[0]["lastname"].ToString() + ", " + _userDetails.Rows[0]["firstname"].ToString();
                lbl_usertype.Text = Int32.Parse(_userDetails.Rows[0]["usertype"].ToString()) == 0 ? "Cashier" : "Administrator";
                await getSaleINV();
                await getSalePOS();
                await getShiftIN();
                _soModel = new shiftOffModel
                {
                    ShiftInAmount = decimal.Parse(_dtMyShiftIn.Rows[0]["shift_In_amount"].ToString()),
                    TotalSales = gettotalSales(false),
                    VoidSales = gettotalSales(true),
                    totalDebtCredit = gettotalDebtCreditSales(),
                    IsBalance= false,
                    ExpectedCashOnHand = decimal.Parse(_dtMyShiftIn.Rows[0]["shift_In_amount"].ToString()) + gettotalSales(false),
                    CashOnHand = 0,
                };
                _soModel.TotalSalesWithOutDebtCredit = _soModel.TotalSales - _soModel.totalDebtCredit;

                lbl_shiftInAmount.Text = _soModel.ShiftInAmount.ToString("C");
                lbl_shiftTotalSales.Text = _soModel.TotalSales.ToString("C");
                lbl_shiftVoidSales.Text = _soModel.VoidSales.ToString("C");
                lbl_isBalance.Text = _soModel.IsBalance ? "Yes" : "No";
                lbl_expectCashOnHand.Text = _soModel.ExpectedCashOnHand.ToString("C");
                lbl_TotalDebtCreditAmount.Text = _soModel.totalDebtCredit.ToString("C");

                

                //btn = new DataGridViewButtonColumn();
                //dgv_saleReportPOS.Columns.Add(btn); // .Columns.Add(btn);
                //btn.HeaderText = "IsVoid";
                //btn.Text = ;
                //btn.UseColumnTextForButtonValue = true;
                //btn.DisplayIndex = 0;

                dgv_saleReportPOS.DataSource = _dtMySalesPOS;
                dgv_saleReportPOS.Columns[9].ValueType = typeof(string);
                //dgv_saleReportPOS.Columns[9].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Catch Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            processOnOff(false);
        }

        public decimal gettotalSales(bool isVoid)
        {
            decimal totalSales = 0;
            try
            {
                DataTable ret = _dtMySalesPOS.AsEnumerable().Where(
                    w => w.Field<bool>("is_void") == isVoid).CopyToDataTable();
                if (ret.Rows.Count > 0)
                {
                    foreach (DataRow row in ret.Rows)
                    {
                        totalSales = totalSales + Decimal.Parse(row["total_price"].ToString());
                    }
                }
                return totalSales;
            }
            catch (Exception)
            {
                return totalSales;
            }
        }

        public decimal gettotalDebtCreditSales()
        {
            decimal totalSales = 0;
            try
            {
                DataTable ret = _dtMySalesPOS.AsEnumerable().Where(
                    w => w.Field<bool>("is_void") == true).CopyToDataTable();
                if (ret.Rows.Count > 0)
                {
                    foreach (DataRow row in ret.Rows)
                    {
                        totalSales = totalSales + Decimal.Parse(row["total_price"].ToString());
                    }
                }
                return totalSales;
            }
            catch (Exception)
            {
                return totalSales;
            }
        }

        public async Task getSaleINV()
        {

            logs("getSaleINV....wait");
            try
            {
                _dtMySalesInv = await _invClass.getInvSold();
            }
            catch (Exception ex)
            {
                logs("getSaleINV Error Catch :" + ex.Message);
                throw ex;
            }
            logs("getSaleINV....end");
        }
        public async Task getSalePOS()
        {
            logs("getSalePOS....wait");
            try
            {
                _dtMySalesPOS = _otClass.getPOSlist();
            }
            catch (Exception ex)
            {
                logs("getSalePOS Error Catch :" + ex.Message);
                throw ex;
            }
            logs("getSaleINV....end");
        }
        public async Task getShiftIN()
        {
            logs("getShiftIN....wait");
            try
            {
                _dtMyShiftIn = _ssClass.getShiftIn(_userDetails.Rows[0]["user_id"].ToString());
            }
            catch (Exception ex)
            {
                logs("getShiftIN Error Catch :" + ex.Message);
                throw ex;
            }
            logs("getShiftIN....end");
        }


        private void btn_refresh_Click(object sender, EventArgs e)
        {
            CashierTab_window2();
        }


        private void txt_ReportCashOnHand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_shiftOut.Enabled = true;
                decimal cashonHand = decimal.Parse(txt_ReportCashOnHand.Text);
                _soModel.DisbalanceAmount = cashonHand - (_soModel.ExpectedCashOnHand - _soModel.totalDebtCredit);
                lbl_disbalance.Text = _soModel.DisbalanceAmount.ToString("C");
                _soModel.IsBalance = _soModel.DisbalanceAmount == 0 ? true : false;
                lbl_isBalance.Text = _soModel.IsBalance ? "Yes" : "No";

                if(_soModel.DisbalanceAmount < 0)
                {
                    // Is balance
                    label12.BackColor = Color.Red;
                    label12.ForeColor = Color.White;
                    lbl_isBalance.BackColor = Color.Red;
                    lbl_isBalance.ForeColor = Color.White;

                    //Disbalance
                    label13.BackColor = Color.Red;
                    label13.ForeColor = Color.White;
                    lbl_disbalance.BackColor = Color.Red;
                    lbl_disbalance.ForeColor = Color.White;
                }
                else
                {
                    // Is balance
                    label12.BackColor = Color.White;
                    label12.ForeColor = Color.Black ;
                    lbl_isBalance.BackColor = Color.White;
                    lbl_isBalance.ForeColor = Color.Black;

                    //Disbalance
                    label13.BackColor = Color.White;
                    label13.ForeColor = Color.Black;
                    lbl_disbalance.BackColor = Color.White;
                    lbl_disbalance.ForeColor = Color.Black;
                }
            }
        }
        private void dgv_salesReportInvSold_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                _posInvSoldModel = new PosInvSoldModel();
                _posInvSoldModel.ProductID = int.Parse(dgv_salesReportInvSold.Rows[e.RowIndex].Cells[2].Value.ToString());
                _posInvSoldModel.Cost = decimal.Parse(dgv_salesReportInvSold.Rows[e.RowIndex].Cells[3].Value.ToString());
                _posInvSoldModel.Price = decimal.Parse(dgv_salesReportInvSold.Rows[e.RowIndex].Cells[4].Value.ToString());
                _posInvSoldModel.tdt = DateTime.Parse(dgv_salesReportInvSold.Rows[e.RowIndex].Cells[5].Value.ToString());
                _posInvSoldModel.QTY = int.Parse(dgv_salesReportInvSold.Rows[e.RowIndex].Cells[6].Value.ToString());
                _posInvSoldModel.IsCancel = bool.Parse(dgv_salesReportInvSold.Rows[e.RowIndex].Cells[8].Value.ToString());
                _posInvSoldModel.Remarks = dgv_salesReportInvSold.Rows[e.RowIndex].Cells[9].Value.ToString();
                displayInvSoldDetails(_posInvSoldModel);
            }
        }

        public void displayInvSoldDetails(PosInvSoldModel model)
        {
            lblInvSold_ProductID.Text = model.ProductID.ToString();
            lblInvSold_Cost.Text = model.Cost.ToString("C");
            lblInvSold_Price.Text = model.Price.ToString("C");
            lblInvSold_TDT.Text = model.tdt.ToString();
            lblInvSold_IsCancel.Text = model.IsCancel? "YES" : "NO";
            rtb_InvSold_Remarks.Text = model.Remarks.ToString();
            lblInvSold_QTY.Text = model.QTY.ToString();
        }

        private void dgv_saleReportPOS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string posID = string.Empty;
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                posID = dgv_saleReportPOS.Rows[e.RowIndex].Cells[1].Value.ToString();
                _posSaleReportModel = new PosSaleReportModel();
                _posSaleReportModel.QTY = int.Parse(dgv_saleReportPOS.Rows[e.RowIndex].Cells[2].Value.ToString());
                _posSaleReportModel.Price = decimal.Parse(dgv_saleReportPOS.Rows[e.RowIndex].Cells[3].Value.ToString());
                _posSaleReportModel.Cash = decimal.Parse(dgv_saleReportPOS.Rows[e.RowIndex].Cells[4].Value.ToString());
                _posSaleReportModel.Change = decimal.Parse(dgv_saleReportPOS.Rows[e.RowIndex].Cells[5].Value.ToString());
                _posSaleReportModel.tdt = DateTime.Parse(dgv_saleReportPOS.Rows[e.RowIndex].Cells[6].Value.ToString());
                _posSaleReportModel.UserID = dgv_saleReportPOS.Rows[e.RowIndex].Cells[8].Value.ToString();
                _posSaleReportModel.IsCancel = bool.Parse(dgv_saleReportPOS.Rows[e.RowIndex].Cells[9].Value.ToString());
                _posSaleReportModel.IsDebtCredit = bool.Parse(dgv_saleReportPOS.Rows[e.RowIndex].Cells[10].Value.ToString());
                _posSaleReportModel.Remarks = dgv_saleReportPOS.Rows[e.RowIndex].Cells[11].Value.ToString();                
                displaySalesReportDetails(_posSaleReportModel);
            }

            var retval = (from inv in _dtMySalesInv.AsEnumerable()
                          where inv.Field<string>("transaction_id") == posID
                          select inv);
            if(retval.Any())
            {                
                dgv_salesReportInvSold.DataSource = retval.CopyToDataTable();
            }
            else
            {
                dgv_salesReportInvSold.DataSource = null;
            }
        }

        public void displaySalesReportDetails(PosSaleReportModel model)
        {
            lbl_posCash.Text = model.Cash.ToString("C");
            lbl_posQTY.Text = model.QTY.ToString();
            lbl_posPrice.Text = model.Price.ToString("C");
            lbl_posChange.Text = model.Change.ToString("C");
            lbl_posTDT.Text = model.tdt.ToString();
            lbl_posIsCancel.Text = model.IsCancel ? "YES" : "NO";
            lbl_posIsDeptCredit.Text = model.IsDebtCredit ? "YES" : "NO";
            lbl_posUserID.Text = model.UserID.ToString();
            rtb_posRemark.Text = model.Remarks;

        }


        private void btn_shiftOut_Click(object sender, EventArgs e)
        {
            logs("Shift Out Started");
            try
            {
                if (_soModel.IsBalance || _soModel.DisbalanceAmount > 0 || !string.IsNullOrEmpty(richTextRemarks.Text))
                {
                    if (_ssClass.shiftOutAssistValidation())
                    {
                        logs("Shift Out -- Admin Credential Login.. ");
                        var fr = new AdminCredential(_conn);
                        fr.ShowDialog();
                        if (_isAdminVerified)
                        {
                            logs("Shift Out -- Admin Credential Verified.. ");
                            logs("Shift Out -- Assisted By: " + _userAdminDetails.Rows[0]["lastname"].ToString() + ", " + _userAdminDetails.Rows[0]["firstname"].ToString());
                            cashierShiftOutAssisted(true, _userAdminDetails.Rows[0]["user_id"].ToString());
                            logs("Shift Out -- Finish.. ");
                            MessageBox.Show("Shift-out Reported Successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            logs("Shift Out -- User Credential is not an Admin.. ");
                        }
                    }
                    else
                    {
                        logs("Shift Out -- Admin Credential Is not Required.. ");
                        logs("Shift Out -- None");
                        cashierShiftOutAssisted(true, string.Empty);
                        logs("Shift Out -- Finish.. ");
                    }
                    _isAdminVerified = false;
                }
                else
                {
                    string notif = _soModel.DisbalanceAmount < 0 ? "Please Input Remarks below!" : string.IsNullOrEmpty(richTextRemarks.Text) ? "Remarks is empty! \nPlease Input Remarks below!" : string.Empty;
                    logs("CATCH ERR: " + notif);
                    MessageBox.Show(notif, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                logs("CATCH ERR: " + ex.Message);
                MessageBox.Show(ex.Message, "Catch Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void cashierShiftOutAssisted(bool isNeedAssisst, string adminID)
        {
            try
            {
                var model = new shiftmodel
                {
                    ID = Int32.Parse(_dtMyShiftIn.Rows[0]["ID"].ToString()),
                    user_id = _userDetails.Rows[0]["user_id"].ToString(),
                    shift_date = DateTime.Now.ToString("MM/dd/yyyy"),
                    assist_by = adminID,
                    shift_amount = decimal.Parse(txt_ReportCashOnHand.Text),
                    is_balance = _soModel.IsBalance,
                    total_void_sales = _soModel.VoidSales,
                    disbalance_amount = _soModel.DisbalanceAmount,
                };
                _ssClass.shiftOutUpdate(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private async void btnDebtCredit_Click(object sender, EventArgs e)
        {
            if (dt_orderlist.Rows.Count == 0)
            {
                MessageBox.Show("No available item in order list.", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                _remarks = null;
                Remarks fr = new Remarks();
                fr.ShowDialog();
                if (!string.IsNullOrEmpty(_remarks))
                {
                    logs("POS Orders Transaction: Started");
                    processOnOff(true);
                    await transaction(false, true);
                    CashierHome_Load(null, null);
                    processOnOff(false);
                    logs("POS Orders Transaction: End");
                }               
            }
        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            Inventory frm = new Inventory(_userDetails, _conn, this.Text);
            frm.Show();
            this.Hide();
        }
    }
    
}
