using Newtonsoft.Json;
using POS_Jave2021.Class;
using POS_Jave2021.Model;
using POS_Jave2021.View.Admin;
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
    public partial class Login : Form
    {
        public userModel userModel;
        public static OleDbConnection conn;
        UserRegistration UR_serviceClass = new UserRegistration();
        UserLogin UserLogin = new UserLogin();
        userModel usermodel = new userModel();
        public static DataTable userDetails;
        logsClass _logsClass;
        public Login()
        {
            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Jino\\POS\\POS_Jave2021\\DBSource\\POS_DB.accdb");
            _logsClass = new logsClass();
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            conn = connectionClass.connect();
        }
        private void btnLogin_Click(object sender, EventArgs e)
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
                    _logsClass.writelineLogs("Login Successfully.....");
                    _logsClass.writelineLogs("USER JSON : " + JsonConvert.SerializeObject(userDetails));
                    MessageBox.Show("Login Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.Hide();
                    if(userDetails.Rows[0]["usertype"].ToString() == "1")
                    {
                        var frm = new AdminDashboard(userDetails, conn);
                        frm.Show();
                        this.Hide();
                    }
                    else
                    {
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

    }
}
