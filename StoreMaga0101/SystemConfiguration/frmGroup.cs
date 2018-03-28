﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemConfiguration
{
    public partial class frmGroup : Form
    {
        int USERID;
        Config config;
        public frmGroup()
        {
            InitializeComponent();
            USERID = 1;
            config = new Config(@".\s2008", "StoreManagement1", null, null);
        }
        
        public frmGroup(string ServerNm, string DbNm, string UserSql, string PassSql,int UserID)
        {
            InitializeComponent();
            try
            {// connection to server
                config = new Config(ServerNm, DbNm, UserSql, PassSql);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            USERID = UserID;

        }
        //
        // from loading
        private void frmGroup_Load(object sender, EventArgs e)
        {
            try
            { // loading Group source

                comboBox1.DataSource = config.GetSourecGroup();
                comboBox1.ValueMember = "رقم المصدر";
                comboBox1.DisplayMember = "اسم المصدر";
                comboBox1.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        //
        // btn add new Group
        private void btnAddSup_Click(object sender, EventArgs e)
        {
            if((int)comboBox1.SelectedValue>0 && txtNameGroup.Text.Length>0)
            {
               try
                {   /// add new group
                    config.AddNewGroup((int)comboBox1.SelectedValue, txtNameGroup.Text, txtDecr.Text, USERID, DateTime.Now);
                    

                    if (dataGridView1.RowCount > 0)
                    {   // عرض البيانات الموجودة مسبقا مع البيانات المضافه 
                        DataTable dt = new DataTable();
                        DataTable dt2 = new DataTable();
                        dt = (DataTable)dataGridView1.DataSource;
                        dt2  = config.GetOneGroup(config.GetMaxIDGroup());
                        dt2.Merge(dt);
                        dataGridView1.DataSource = dt2;
                    }
                    else
                    {
                        dataGridView1.DataSource= config.GetOneGroup(config.GetMaxIDGroup());
                    }
                      
                    Refrish();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnRefrish_Click(object sender, EventArgs e)
        {
            Refrish();
            dataGridView1.DataSource = config.GetAllGroup();
        }

        //// refrish Data
        private void Refrish()
        {
            comboBox1.SelectedIndex = 0;
            txtDecr.Text = "";
            txtIDgroup.Text = "";
            txtNameGroup.Text = "";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
