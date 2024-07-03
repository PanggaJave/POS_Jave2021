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
        OleDbConnection _conn;
        public AddUpdateInventory(bool isUpdate, DataTable invDetails, DataTable productlist, string invID)
        {
            InitializeComponent();
            _IsUpdate = isUpdate;
            _invDetails = invDetails;
            _productList = productlist;
            _InvID = invID;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddUpdateInventory_Load(object sender, EventArgs e)
        {
            dgv_ProductList.DataSource = _productList;
            if(_invDetails.Rows.Count > 0)
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
        }

        public void DisplayDetails(DataTable dt)
        {
           if(dt.Rows.Count > 0)
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

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
