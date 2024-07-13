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

namespace POS_Jave2021.View.Admin
{
    public partial class AddNewUsers : Form
    {        
        OleDbConnection _conn;
        DataTable _userDetails;
        userModel _userModel = new userModel();
        UserRegistration _service = new UserRegistration();
        public string _backString;
        
        public AddNewUsers(DataTable userDetails, OleDbConnection conn, userModel model, string backString)
        {
            _conn = conn;
            _userDetails = userDetails;
            _userModel = model;
            _backString = backString;
            InitializeComponent();
        }

        private void AddNewUsers_Load(object sender, EventArgs e)
        {

        }

        private void btn_userinfo_Click(object sender, EventArgs e)
        {
            _userModel.lastname = txt_lastname.Text;
            _userModel.firstname = txt_firstname.Text;
            _userModel.middlename = txt_middlename.Text;
            _userModel.phone_no = txtphone.Text;
            _userModel.email = txtemail.Text;
            _userModel.usertype = cmbusertype.SelectedIndex;
            _userModel.factor_authen = false;
            _userModel.user_status = true;
            groupBox6.Enabled = true;
            groupBox5.Enabled = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            groupBox6.Enabled = false;
            groupBox5.Enabled = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            
            try
            {
                string retpass = passwordHash.encryptor(txt_confirmpassword.Text.Trim());
            }
            catch (Exception)
            {

                throw;
            }

            DateTime dtm = DateTime.Now;

            _userModel.username = textBox13.Text;
            _userModel.password = txt_confirmpassword.Text;
            _userModel.user_id = dtm.ToString("yyyyMMddhhmmss");
            _userModel.is_active = true;
            _userModel.is_deleted = true;
            groupBox6.Enabled = false;
            groupBox7.Enabled = true;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are your sure?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (d == DialogResult.OK)
            {
                _conn.Close();
                var retval = _service.register(_userModel, _conn);
                if (retval.is_Success)
                {
                    MessageBox.Show(retval.message, retval.title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(retval.message, retval.title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Hide();
            }
            if (d == DialogResult.Cancel)
            {
                MessageBox.Show("You clicked Cancel");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (button15.Text == "Hide")
            {
                txt_newpassword.UseSystemPasswordChar = true;
                button15.Text = "Show";
            }
            else
            {
                txt_newpassword.UseSystemPasswordChar = false;
                button15.Text = "Hide";
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (button16.Text == "Hide")
            {
                txt_confirmpassword.UseSystemPasswordChar = true;
                button16.Text = "Show";
            }
            else
            {
                txt_confirmpassword.UseSystemPasswordChar = false;
                button16.Text = "Hide";
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            groupBox7.Enabled = false;
            groupBox6.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BackClass.action(_userDetails, _conn, _backString);
            this.Hide();
        }

        private void AddNewUsers_Leave(object sender, EventArgs e)
        {

        }
    }
}
