using POS_Jave2021.View;
using POS_Jave2021.View.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Jave2021.Class
{
    public class BackClass
    {
        public static bool action( DataTable userDetails, OleDbConnection conn, string actString)
        {
            if(string.IsNullOrEmpty(actString))
            {
                return true;
            }

            var frm = new ListOfUsers(userDetails, conn, null);
            if (actString.Trim() == frm.Text)
            {
                frm.Show();
                return true;
            }
            var frm1 = new AdminDashboard(userDetails, conn);
            if (actString.Trim() == frm1.Text)
            {
                frm1.Show();
                return true;
            }
            //CashierHome
            var frm2 = new CashierHome(userDetails, conn);
            if (actString.Trim() == frm2.Text)
            {
                frm2.Show();
                return true;
            }

            return false;
        }
    }
}
