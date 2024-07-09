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

namespace POS_Jave2021.View.Cashier
{
    public partial class Inventory : Form
    {
        logsClass _logClass;
        InventoryClass _invClass;
        OleDbConnection _conn;
        DataTable _userDetails;
        DataTable _invData;
        DataTable _productData;
        bool dtgribBTN = false;
        public Inventory(DataTable userDetails, OleDbConnection conn)
        {
            InitializeComponent();
            _userDetails = userDetails;
            _conn = conn;
            _invClass = new InventoryClass(_conn);
            _logClass = new logsClass();
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            try
            {
                if (!dtgribBTN)
                {
                    #region datagridButton PosReport
                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                    dgvInvList.Columns.Add(btn); // .Columns.Add(btn);
                    btn.HeaderText = "Option";
                    btn.Text = "Add";
                    btn.Name = "btnClick";
                    btn.UseColumnTextForButtonValue = true;
                    btn.DisplayIndex = 0;

                    //DataGridViewButtonColumn btn1 = new DataGridViewButtonColumn();
                    //dgvProductList.Columns.Add(btn1); // .Columns.Add(btn);
                    //btn1.HeaderText = "Option";
                    //btn1.Text = "View";
                    //btn1.Name = "btnClick";
                    //btn1.UseColumnTextForButtonValue = true;
                    //btn1.DisplayIndex = 0;
                    #endregion
                    dtgribBTN = true;
                }
                

                _invData = _invClass.getlistOfAllInventory();
                _productData = _invClass.getlistOfProductList();
                dgvInvList.DataSource = _invData;
                dgvProductList.DataSource = _productData;
            }
            catch (Exception ex)
            {
                _logClass.writelineLogs("Catch Error: "+ ex.Message);
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchInv_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            try
            {
                var rets = from inv in _invData.AsEnumerable()
                           where inv.Field<string>("inv_id").Trim() == txtSearchInv.Text.Trim()
                           select inv;
                if (rets.Any())
                {
                    dt = rets.CopyToDataTable();
                    var frm = new AddUpdateInventory(true, dt, _productData, txtSearchInv.Text.Trim(), _conn);
                    frm.ShowDialog();
                }
                else
                {
                    var frm = new AddUpdateInventory(false, null, _productData, txtSearchInv.Text.Trim(), _conn);
                    frm.ShowDialog();
                }
                Inventory_Load(null, null);
            }
            catch (Exception ex)
            {
                _logClass.writelineLogs("Catch Err: " + ex.Message);
                MessageBox.Show("No record!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtSearchInv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchInv_Click(null, null);
                txtSearchInv.Text = string.Empty; txtSearchInv.Focus();
            }

        }

        private void txtSearchProduct_KeyDown(object sender, KeyEventArgs e)
        {
            btnSearchProduct_Click(null, null);
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
        }

        private void dgvInvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                var id = dgvInvList.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtSearchInv.Text = id;
                btnSearchInv_Click(null, null);
                txtSearchInv.Text = string.Empty;
                //var rets = from prd in _productList.AsEnumerable()
                //           where prd.Field<string>("item_id") == id
                //           select prd;
                //if (rets.Any())
                //{
                //    DataTable dt = rets.CopyToDataTable();
                //    dgvInvList.DataSource = dt;
                //    dgvInvList(rets.CopyToDataTable());
                //}
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //btnSearchInv_Click(null, null);
        }
    }
}
