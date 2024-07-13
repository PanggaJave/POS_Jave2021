using POS_Jave2021.Class;
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

namespace POS_Jave2021.View.Admin
{
    public partial class AdminDashboard : Form
    {
        OleDbConnection _conn;
        DataTable _userDetails;
        public AdminDashboard(DataTable userDetails, OleDbConnection conn)
        {
            _conn = conn;
            _userDetails = userDetails;
            InitializeComponent();
        }
        private void AdminDashboard_Load(object sender, EventArgs e)
        {

        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pointOfSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shiftstart ss = new shiftstart(_conn);
            if (!ss.shiftvalidate(_userDetails.Rows[0]["user_id"].ToString()))
            {
                var form = new Shift_Start();
                form.ShowDialog();
                var shift_status = form.is_status;
                if (shift_status)
                {
                    CashierHome cashier = new CashierHome(_userDetails, _conn);
                    cashier.Show();
                }
            }
            else
            {
                CashierHome cashier = new CashierHome(_userDetails, _conn);
                cashier.Show();
            }
        }

        private void listOfInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inventory frm = new Inventory(_userDetails, _conn);
            frm.Show();
            this.Hide();
        }
    }
}
