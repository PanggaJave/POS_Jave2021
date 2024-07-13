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
        public ListOfUsers(DataTable userDetails, OleDbConnection conn)
        {
            _conn = conn;
            _userDetails = userDetails;

            InitializeComponent();
        }

        private void ListOfUsers_Load(object sender, EventArgs e)
        {
            _listOfusers = UserRegistration.getListOfUser(_conn);
            dgvListOfUsers.DataSource = _listOfusers;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
