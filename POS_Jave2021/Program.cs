using POS_Jave2021.Class;
using POS_Jave2021.Model;
using POS_Jave2021.View;
using POS_Jave2021.View.Cashier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Jave2021
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());


            //var conn = connectionClass.connect();
            //var model = new userModel
            //{
            //    username = "testuser",
            //    password = "test12",
            //};
            //UserLogin ul = new UserLogin();
            //var dtuser = ul.login(model, conn);
            //Application.Run(new CashierHome(dtuser, conn));
            //Application.Run(new Inventory(dtuser, conn)); //ValidateArguments

        }
    }
}
