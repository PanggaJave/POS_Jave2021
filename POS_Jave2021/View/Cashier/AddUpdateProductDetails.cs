using POS_Jave2021.Class;
using POS_Jave2021.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Jave2021.View.Cashier
{
    public partial class AddUpdateProductDetails : Form
    {

        OleDbConnection _conn;
        ProductDetails _productDetails;
        ProductClass _service;
        DataTable _dtProdCat;
        string _id;
        string _itemCatPrefix = "GG";
        bool _isUpdate = false;

        public AddUpdateProductDetails(OleDbConnection conn, ProductDetails model, bool isUpdate)
        {
            _conn = conn;
            InitializeComponent();
            _productDetails = model;
            _isUpdate = isUpdate;
            _id = DateTime.Now.ToString("yyyyMMddHHmmss");
            _service = new ProductClass(_conn);
        }
        private void AddUpdateProductDetails_Load(object sender, EventArgs e)
        {
            try
            {
                _dtProdCat = _service.getProductCategor();
                if (_productDetails.item_id == null)
                {
                    _productDetails.item_id = _itemCatPrefix + _id;
                }
                displayDetails();
                if(_isUpdate)
                {
                    cbxItemCategory.Enabled = false;
                    btnSave.Text = "Update";
                    btnDelete.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Catch!");
                this.Close();
            }
            
        }

        public void displayDetails()
        {
            txtItemId.Text = _productDetails.item_id;
            txtItemName.Text = _productDetails.item_name;
            txtItemDescription.Text = _productDetails.item_description;
            ///////
            cbxItemCategory.DataSource = _dtProdCat;
            cbxItemCategory.DisplayMember = "name";
            cbxItemCategory.ValueMember = "ID";
            //cmbCompName.DataBind();
            cbxItemCategory.Enabled = true;
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            setValues();
            if(_isUpdate)
            {
                var res = _service.Update(_productDetails);
                if (res.is_Success)
                {
                    MessageBox.Show(res.message, res.title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(res.message, res.title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                var res = _service.Insert(_productDetails);
                if (res.is_Success)
                {
                    MessageBox.Show(res.message, res.title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(res.message, res.title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void setValues()
        {
            _productDetails.item_id = txtItemId.Text;
            _productDetails.item_name = txtItemName.Text;
            _productDetails.item_description = txtItemDescription.Text;
        }

        private void cbxItemCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _productDetails.item_catigory = Int32.Parse(cbxItemCategory.SelectedValue.ToString());
                _itemCatPrefix = cbxItemCategory.Text.Substring(1, 2);
                txtItemId.Text = _itemCatPrefix + _id;
            }
            catch (Exception)
            {
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are sure you want to delete this item?.", "Confirmation!", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                // Do something
                var res = _service.delete(txtItemId.Text.Trim());
                if (res.is_Success)
                {
                    MessageBox.Show(res.message, res.title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(res.message, res.title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
