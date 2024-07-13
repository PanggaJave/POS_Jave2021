using Newtonsoft.Json;
using POS_Jave2021.Class;
using POS_Jave2021.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Jave2021.View.Cashier
{
    public partial class Shift_Start : Form
    {
        logsClass lc;
        public Shift_Start()
        {
            InitializeComponent();
            lc = new logsClass();
        }
        public bool is_status = false;
        private void Shift_Start_Load(object sender, EventArgs e)
        {
            var userDetails = Login.userDetails;
            txtShiftName.Text = userDetails.Rows[0]["lastname"].ToString() + ", " + userDetails.Rows[0]["firstname"].ToString();
            txtShiftDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            txtUserID.Text = userDetails.Rows[0]["user_id"].ToString();

        }

        private void btn_ShiftProceed_Click(object sender, EventArgs e)
        {
            try
            {
                var model = new shiftmodel
                {
                    user_id = txtUserID.Text,
                    shift_date = txtShiftDate.Text,
                    shift_amount = decimal.Parse(txtShiftAmount.Text),
                };
                shiftstart ss = new shiftstart(Login.conn);
                var res = ss.Insert(model);
                if(res.is_Success)
                {
                    lc.writelineLogs("USER Shift CashOnHand : " + JsonConvert.SerializeObject(model));
                    is_status = true;
                    this.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
