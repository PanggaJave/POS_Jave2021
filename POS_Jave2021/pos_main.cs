﻿using MySql.Data.MySqlClient;
using POS_Jave2021.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Jave2021
{
    public partial class pos_main : Form
    {
        public pos_main()
        {
            InitializeComponent();
        }
        MySqlConnection conn;
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pos_main_Load(object sender, EventArgs e)
        {
            connectionClass.connect();
            conn = connectionClass.conn;
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
    }
}
