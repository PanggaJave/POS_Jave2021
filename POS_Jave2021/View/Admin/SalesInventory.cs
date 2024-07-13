using POS_Jave2021.Class;
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

namespace POS_Jave2021.View.Admin
{
    public partial class SalesInventory : Form
    {
        DataTable _userDetails;
        OleDbConnection _conn;
        string _backString;
        OrderTransactionClass _service;
        DataTable _dtSales = new DataTable();
        public SalesInventory(DataTable userDetails, OleDbConnection conn, string backString)
        {
            _userDetails = userDetails;
            _conn = conn;  
            _backString = backString;
            _service = new OrderTransactionClass(_conn);
            InitializeComponent();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            _dtSales = _service.getPOSlist(DateTime.Parse(dateTimePicker1.Text), DateTime.Parse(dateTimePicker2.Text)
                , isVoid(), isCreditTo());
            dgvSales.DataSource = _dtSales;
            dgvSales.Columns["tdt"].Visible = false;
            dgvSales.Columns["upd"].Visible = false;
            dgvSales.Columns["userid"].Visible = false;
            SalesComputation();
        }

        public void SalesComputation()
        {
            /// Total Amount
            var sumTotal = (from t in _dtSales.AsEnumerable()
                            where t.Field<bool>("is_void") == false &&
                            t.Field<bool>("is_debt_credit") == false
                            select t
                            );
            if(sumTotal.Any())
            {
                DataTable dtSales = sumTotal.CopyToDataTable();
                var total = dtSales.AsEnumerable().Sum(data => data.Field<decimal>("total_price"));
                totalSalesAmount.Text = total.ToString("C");
            }
            else
            {
                totalSalesAmount.Text = "";
            }

            /// Total Void
            var sumVoid = (from t in _dtSales.AsEnumerable()
                            where t.Field<bool>("is_void") == true &&
                            t.Field<bool>("is_debt_credit") == false
                            select t
                            );
            if (sumVoid.Any())
            {
                DataTable dtSales = sumVoid.CopyToDataTable();
                var total = dtSales.AsEnumerable().Sum(data => data.Field<decimal>("total_price"));
                totalVoidAmount.Text = total.ToString("C");
            }
            else
            {
                totalVoidAmount.Text = "";
            }

            /// Total Debt Credit
            var sumCredit = (from t in _dtSales.AsEnumerable()
                           where t.Field<bool>("is_void") == false &&
                           t.Field<bool>("is_debt_credit") == true
                           select t
                            );
            if (sumCredit.Any())
            {
                DataTable dtSales = sumCredit.CopyToDataTable();
                var total = dtSales.AsEnumerable().Sum(data => data.Field<decimal>("total_price"));
                totalCreditAmount.Text = total.ToString("C");
            }
            else
            {
                totalCreditAmount.Text = "";
            }
        }

        public bool isVoid()
        {
            if(isVoidYes.Checked)
            {
                return true;
            }
            if (isVoidNo.Checked)
            {
                return false;
            }
            return false;
        }
        public bool isCreditTo()
        {
            if (creditToYes.Checked)
            {
                return true;
            }
            if (creditToNo.Checked)
            {
                return false;
            }
            return false;
        }

        private void SalesInventory_Load(object sender, EventArgs e)
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dgvSales.Columns.Add(btn); // .Columns.Add(btn);
            btn.HeaderText = "Option";
            btn.Text = "Select";
            btn.Name = "btnClick";
            btn.UseColumnTextForButtonValue = true;
            btn.DisplayIndex = 0;
            btnSearch_Click(null, null);
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvSales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {            
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                string id = dgvSales.Rows[e.RowIndex].Cells["pos_id"].Value.ToString();
                PriceAmount.Text = dgvSales.Rows[e.RowIndex].Cells["total_price"].Value.ToString();
                cashAmount.Text = dgvSales.Rows[e.RowIndex].Cells["cash"].Value.ToString();
                changeAmount.Text = dgvSales.Rows[e.RowIndex].Cells["change"].Value.ToString();
                NumOfItems.Text = dgvSales.Rows[e.RowIndex].Cells["total_inv"].Value.ToString();
                remark.Text = dgvSales.Rows[e.RowIndex].Cells["remarks"].Value.ToString();
                DataTable dt = _service.getTransactionDetails(id);
                dataGridView1.DataSource = dt;
            }
        }
    }
}
