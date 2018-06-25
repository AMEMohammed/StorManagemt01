﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsHosttcp
{
    public partial class ConectionDb : Form
    {
        public ConectionDb()
        {
            InitializeComponent();
        }

        private void ConectionDb_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.ServerNm;
            textBox2.Text = Properties.Settings.Default.DBNM;
            textBox3.Text = Properties.Settings.Default.UserSql;
            textBox4.Text = Properties.Settings.Default.PassSql;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ServerNm = textBox1.Text ;
            Properties.Settings.Default.DBNM = textBox2.Text ;
             Properties.Settings.Default.UserSql= textBox3.Text ;
             Properties.Settings.Default.PassSql= textBox4.Text ;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
