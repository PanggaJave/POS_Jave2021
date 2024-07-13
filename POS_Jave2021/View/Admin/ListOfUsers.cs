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
    public partial class ListOfUsers : Form
    {
        OleDbConnection _conn;
        DataTable _userDetails;
        DataTable _listOfusers = new DataTable();
        public string _backString;
        public ListOfUsers(DataTable userDetails, OleDbConnection conn, string backString)
        {
            _conn = conn;
            _userDetails = userDetails;
            _backString = backString;

            InitializeComponent();
        }

        private void ListOfUsers_Load(object sender, EventArgs e)
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dgvListOfUsers.Columns.Add(btn); // .Columns.Add(btn);
            btn.HeaderText = "Option";
            btn.Text = "Select";
            btn.Name = "btnClick";
            btn.UseColumnTextForButtonValue = true;
            btn.DisplayIndex = 0;

            _listOfusers = UserRegistration.getListOfUser(_conn);
            dgvListOfUsers.DataSource = _listOfusers;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new AddNewUsers(_userDetails, _conn, new Model.userModel(), this.Text);
            frm.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                dgvListOfUsers.DataSource = _listOfusers;
            }
            else
            {
                var rets = from inv in _listOfusers.AsEnumerable()
                           where inv.Field<string>("user_id").Trim().ToUpper().Contains(textBox1.Text.Trim().ToUpper()) ||
                           inv.Field<string>("username").Trim().ToUpper().Contains(textBox1.Text.Trim().ToUpper()) ||
                           inv.Field<string>("lastname").ToUpper().Contains(textBox1.Text.Trim().ToUpper()) ||
                           inv.Field<string>("firstname").ToUpper().Contains(textBox1.Text.Trim().ToUpper()) ||
                           inv.Field<string>("middlename").ToUpper().Contains(textBox1.Text.Trim().ToUpper()) ||
                           inv.Field<string>("phone_no").ToUpper().Contains(textBox1.Text.Trim().ToUpper()) ||
                           inv.Field<string>("email").ToUpper().Contains(textBox1.Text.Trim().ToUpper())
                           select inv;
                if (rets.Any())
                {
                    dgvListOfUsers.DataSource = rets.CopyToDataTable();
                }
                else
                {
                    dgvListOfUsers.DataSource = null;
                }
            }
        }

        private void ListOfUsers_Leave(object sender, EventArgs e)
        {

        }

        private void dgvListOfUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                txtUserID.Text = dgvListOfUsers.Rows[e.RowIndex].Cells["user_id"].Value.ToString();
                txtUserName.Text = dgvListOfUsers.Rows[e.RowIndex].Cells["username"].Value.ToString();
                txtUserLastName.Text = dgvListOfUsers.Rows[e.RowIndex].Cells["lastname"].Value.ToString();
                txtUserFirstName.Text = dgvListOfUsers.Rows[e.RowIndex].Cells["firstname"].Value.ToString();
                txtUserMiddleName.Text = dgvListOfUsers.Rows[e.RowIndex].Cells["middlename"].Value.ToString();
                txtUserPhone.Text = dgvListOfUsers.Rows[e.RowIndex].Cells["phone_no"].Value.ToString();
                txtUserEmail.Text = dgvListOfUsers.Rows[e.RowIndex].Cells["email"].Value.ToString();
                txtUserType.Text = dgvListOfUsers.Rows[e.RowIndex].Cells["usertype"].Value.ToString() == "1"? "Administrator" : "Cashier";
            }
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
