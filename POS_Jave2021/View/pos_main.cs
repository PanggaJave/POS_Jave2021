using Newtonsoft.Json;
using POS_Jave2021.Class;
using POS_Jave2021.Model;
using POS_Jave2021.View;
using POS_Jave2021.View.Cashier;
using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Jave2021
{
    public partial class pos_main : Form
    {
        public static OleDbConnection conn;
        UserRegistration UR_serviceClass = new UserRegistration();
        UserLogin UserLogin = new UserLogin();
        userModel usermodel = new userModel();
        public static DataTable userDetails;
        logsClass _logsClass;
        public pos_main()
        {
            InitializeComponent();
            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Jino\\POS\\POS_Jave2021\\DBSource\\POS_DB.accdb");
            _logsClass = new logsClass();
        }

        private void pos_main_Load(object sender, EventArgs e)
        {
            conn = connectionClass.connect();
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string passsword = txt_newpassword.Text.Trim();
                label15.Text = passwordValidator.CheckStrength(passsword).ToString();
            }
            catch (Exception)
            {
                label15.Text = "Invalid Password..";
            }
        }

        private void txt_confirmpassword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_newpassword.Text.Trim() == txt_confirmpassword.Text.Trim())
                {
                    label16.Text = "Password match!";
                }
                else
                {
                    label16.Text = "Password not match!";
                }
            }
            catch (Exception)
            {
                label16.Text = "Password not match!";
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

        private void button11_Click(object sender, EventArgs e)
        {
            //groupBox6.Enabled = false;
            //groupBox5.Enabled = true;
            try
            {
                string retpass = passwordHash.encryptor(txt_confirmpassword.Text.Trim());
            }
            catch (Exception)
            {

                throw;
            }

            DateTime dtm = DateTime.Now;

                usermodel.username = textBox13.Text;
                usermodel.password = txt_confirmpassword.Text;
                usermodel.user_id = dtm.ToString("yyyyMMddhhmmss");
                usermodel.is_active = true;
                usermodel.is_deleted = true;         
                groupBox6.Enabled = false;
                groupBox7.Enabled = true;

        }
         
        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                // image filters  
                open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    // display image in picture box  
                    pictureBox5.Image = new Bitmap(open.FileName);
                    // image file path  
                    label17.Text = open.FileName;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (validator.email(txtemail.Text.Trim()))
            {
                label18.Text = "Email is valid!";
            }
            else
            {
                label18.Text = "Invalid Email!";
            }
        }

        //private void textBox8_Enter(object sender, EventArgs e)
        //{

        //}

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            label23.Text = validator.phonenumber(txtphone.Text.Trim());
        }

        private void btn_userinfo_Click(object sender, EventArgs e)
        {
           
            usermodel.lastname = txt_lastname.Text;
            usermodel.firstname = txt_firstname.Text;
            usermodel.middlename = txt_middlename.Text;
            usermodel.phone_no = txtphone.Text;
            usermodel.email = txtemail.Text;
            usermodel.usertype = cmbusertype.SelectedIndex;
            usermodel.factor_authen = true;
            usermodel.factor_authen = false;
            usermodel.user_status = true;

            groupBox6.Enabled = true;
            groupBox5.Enabled = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            groupBox6.Enabled = false;
            groupBox5.Enabled = true;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                usermodel = new userModel();
                usermodel.username = txtUserName.Text;
                usermodel.password = txtUserPassword.Text;
                conn.Close();
                userDetails = UserLogin.login(usermodel, conn);
                if (userDetails.Rows.Count > 0)
                {
                    if (bool.Parse(userDetails.Rows[0]["auth_factor"].ToString()) == true)
                    {
                        tabControl.SelectedIndex = 1;
                    }
                    else
                    {
                        _logsClass.writelineLogs("Login Successfully.....");
                        _logsClass.writelineLogs("USER JSON : " + JsonConvert.SerializeObject(userDetails));
                        MessageBox.Show("Login Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //this.Hide();
                        shiftstart ss = new shiftstart(conn);
                        if (!ss.shiftvalidate(userDetails.Rows[0]["user_id"].ToString()))
                        {
                            var form = new Shift_Start();
                            form.ShowDialog();
                            var shift_status = form.is_status;
                            if (shift_status)
                            {                                
                                CashierHome cashier = new CashierHome(userDetails, conn);
                                cashier.Show();
                                this.Hide();
                            }
                        }
                        else
                        {
                            CashierHome cashier = new CashierHome(userDetails, conn);
                            cashier.Show();
                            this.Hide();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Login Failed. Please check your username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {

                throw;
            }
                 
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
        private void button14_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are your sure...", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (d == DialogResult.OK)
            {
                conn.Close();
               var retval = UR_serviceClass.register(usermodel, conn);
                if(retval.is_Success)
                {
                    MessageBox.Show(retval.message, retval.title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabControl.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show(retval.message, retval.title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }             

            }
            if (d == DialogResult.Cancel)
            {
                MessageBox.Show("You clicked Cancel"); 
            }
          
        }
        

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            groupBox7.Enabled = false;
            groupBox6.Enabled = true;

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

       
        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtUserPassword.Text))
            {
                btnLogin.Enabled = true;
            }
            else
            {
                btnLogin.Enabled = false;
            }
        }

        private void txtUserPassword_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtUserPassword.Text))
            {
                btnLogin.Enabled = true;
            }
            else
            {
                btnLogin.Enabled = false;
            }
        }
    }
}
