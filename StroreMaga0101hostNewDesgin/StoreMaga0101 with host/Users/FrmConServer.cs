﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Users
{
    public partial class FrmConServer : Form
    {
        public FrmConServer()
        {
            InitializeComponent();

        }

        private void btnCHek_Click(object sender, EventArgs e)
        {
            SqlConnection sqlCon;
            if (txtServer.Text.Length > 0 && txtDB.Text.Length > 0)

            {
                if (txtUser.Text.Length > 0 && txtpass.Text.Length > 0)
                {
                    sqlCon = new SqlConnection(@"Data Source=" + txtServer.Text + ";Initial Catalog=" + txtDB.Text + ";User ID=" + txtUser.Text + ";Password=" + txtpass.Text);
                }
                else
                {
                    sqlCon = new SqlConnection(@"Data Source=" + txtServer.Text + ";Initial Catalog=" + txtDB.Text + ";;Integrated Security=true;");

                }
                try
                {
                    sqlCon.Open();
                    label7.Text = "متصل";
                    sqlCon.Close();
                }
                catch
                {
                    label7.Text = "غير متصل";
                }



            }
            else
            {
                MessageBox.Show("يجب كتابة اسم  السيرفر و اسم قاعدة البيانات", "", MessageBoxButtons.OK);
            }
        }

        private void FrmConServer_Load(object sender, EventArgs e)
        {
            try
            {
                txtServer.Text = Properties.Settings.Default.ServerNm;
                txtDB.Text = Properties.Settings.Default.DBNM;
                txtUser.Text = Properties.Settings.Default.UserSql;
                txtpass.Text = Properties.Settings.Default.PassSql;
               
                texthostip.Text = Properties.Settings.Default.iphost;
                txtport.Text=Properties.Settings.Default.port;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ServerNm = txtServer.Text;
            Properties.Settings.Default.DBNM = txtDB.Text;
            Properties.Settings.Default.UserSql = txtUser.Text;
            Properties.Settings.Default.PassSql = txtpass.Text;
            Properties.Settings.Default.ConnectionHost = checkConnectionHost.Checked;
            string host = "net.tcp://" + texthostip.Text + ":" + txtport.Text + "/StoreService";
            Properties.Settings.Default.HostIP = host;
            Properties.Settings.Default.iphost = texthostip.Text;
            Properties.Settings.Default.port = txtport.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkConnectionHost_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkConnectionHost_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkConnectionHost.Checked)
             {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = true;
            }
            else
            {
                groupBox1.Enabled =true;
                groupBox2.Enabled = true;
                groupBox3.Enabled = false;
            }
        }
    }
}
