using POS_Jave2021.Class;
using POS_Jave2021.Model;
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
    public partial class AddUpdateInventory : Form
    {
        public bool _IsUpdate;
        public DataTable _invDetails;
        public DataTable _productList;
        public string _InvID;
        bool dtgribBTN = true;
        OleDbConnection _conn;
        public InventoryClass _invClass;
        public AddUpdateInventory(bool isUpdate, DataTable invDetails, DataTable productlist, string invID)
        {
            InitializeComponent();
            _IsUpdate = isUpdate;
            _invDetails = invDetails;
            _productList = productlist;
            _InvID = invID;
            _invClass = new InventoryClass(_conn);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public void AddButtonInGrid()
        {
            if(dtgribBTN)
            {
                #region datagridButton PosReport
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                dgv_ProductList.Columns.Add(btn); // .Columns.Add(btn);
                btn.HeaderText = "Option";
                btn.Text = "Select";
                btn.Name = "btnClick";
                btn.UseColumnTextForButtonValue = true;
                btn.DisplayIndex = 0;
                dtgribBTN = false;
                #endregion
            }

        }

        private void AddUpdateInventory_Load(object sender, EventArgs e)
        {
            AddButtonInGrid();
            dgv_ProductList.DataSource = _productList;
            if(_invDetails != null)
            {
                var rets = from prd in _productList.AsEnumerable()
                           where prd.Field<string>("item_id") == _invDetails.Rows[0]["product_id"].ToString()
                           select prd;
                if(rets.Any())
                {
                    dgv_ProductDetails.DataSource = rets.CopyToDataTable();
                    DisplayDetails(rets.CopyToDataTable());
                }
                else
                {
                    DisplayDetails(null);
                }
            }
            else
            {
                DisplayDetails(null);
            }
        }

        public void DisplayDetails(DataTable dt)
        {
           if(dt != null || _invDetails != null)
            {
                if(_invDetails != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        txtInventoryID.Text = _InvID;
                        txtProductID.Text = _invDetails.Rows[0]["product_id"].ToString();
                        txtPriceCost.Text = _invDetails.Rows[0]["price_cost"].ToString();
                        txtSellingPrice.Text = _invDetails.Rows[0]["selling_price"].ToString();
                        txtProductName.Text = dt.Rows[0]["item_name"].ToString();
                        txtProductInventory.Text = _invDetails.Rows[0]["available_inv"].ToString();
                        txtProductTotalInventory.Text = _invDetails.Rows[0]["overall_inv"].ToString();
                        txtProductCreateDate.Text = _invDetails.Rows[0]["cdt"].ToString();
                        txtProductDetails.Text = dt.Rows[0]["item_description"].ToString();
                    }
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        txtInventoryID.Text = _InvID;
                        txtProductID.Text = dt.Rows[0]["item_id"].ToString();
                        txtProductName.Text = dt.Rows[0]["item_name"].ToString();
                        txtProductCreateDate.Text = DateTime.Now.ToString();
                        txtProductDetails.Text = dt.Rows[0]["item_description"].ToString();
                    }
                }
                
            }
            else
            {
                txtInventoryID.Text = _InvID;
                txtProductCreateDate.Text = DateTime.Now.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var model = new InventoryModel();
                model.inv_id = _InvID;
                model.product_id = txtProductID.Text;
                model.price_cost = decimal.Parse(txtPriceCost.Text);
                model.selling_price = decimal.Parse(txtSellingPrice.Text);
                model.udt   = DateTime.Now;
                model.available_inv = int.Parse(txtProductTotalInventory.Text);

                if(_IsUpdate) // update
                {

                }
                else // add
                {

                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dgv_ProductList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                var id = dgv_ProductList.Rows[e.RowIndex].Cells[2].Value.ToString();
                var rets = from prd in _productList.AsEnumerable()
                           where prd.Field<string>("item_id") == id
                           select prd;
                if (rets.Any())
                {
                    DataTable dt = rets.CopyToDataTable();
                    dgv_ProductDetails.DataSource = dt;
                    DisplayDetails(rets.CopyToDataTable());
                }
            }
        }
    }
}
