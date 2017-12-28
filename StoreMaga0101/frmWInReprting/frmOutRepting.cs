﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrmRports;
namespace frmWInReprting
{
    public partial class frmOutRepting : Form
    {
        RepotFunction rf;
        int UserID;
        public frmOutRepting()
        {
            InitializeComponent();
            try
            {
                UserID = 1;
                rf = new RepotFunction(@".\s2008", "StoreManagement1", null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public frmOutRepting(string ServerNm, string DbNm, string UserSql, string PassSql, int Userid)
        {
            InitializeComponent();
            try
            {

                rf = new RepotFunction(ServerNm, DbNm, UserSql, PassSql);
                UserID = Userid;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// ////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOutRepting_Load(object sender, EventArgs e)
        {
            try
            {/////////
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox6.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox6.AutoCompleteSource = AutoCompleteSource.ListItems;
                getDate1();
                ////////////
                changeLanguage();
                MessageBoxManager.Yes = "نعم";
                MessageBoxManager.No = "الغاء";
                MessageBoxManager.Register();
                comboBox5.SelectedIndex = 0;
                ///////

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// /////
        /// </summary>
        void getDate1()
        {
            try
            {



                comboBox1.DisplayMember = "اسم الصنف";
                comboBox1.ValueMember = "رقم الصنف";
                comboBox1.DataSource = rf.GetAllCategoryAR();
                comboBox2.DisplayMember = "اسم النوع";
                comboBox2.ValueMember = "رقم النوع";
                comboBox2.DataSource = rf.GetAllTypeQuntity();
                comboBox3.DisplayMember = "اسم العملة";
                comboBox3.ValueMember = "رقم العملة";

                comboBox3.DataSource = rf.GetAllCurrency();

                comboBox6.ValueMember = "رقم الجهة";
                comboBox6.DisplayMember = "اسم الجهة";
                comboBox6.DataSource =rf.GetAllPlace();
                comboBox4.ValueMember = "رقم";
                comboBox4.DisplayMember = "اسم الموظف";
                comboBox4.DataSource = rf.GetAllUser();
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //////////////////////////////////
        public void changeLanguage()
        {
            foreach (InputLanguage lng in InputLanguage.InstalledInputLanguages)
            {
                if (lng.LayoutName == "العربية (101)")
                    InputLanguage.CurrentInputLanguage = lng;
            }
        }
        /// <summary>
        /// //comboxSearch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex == 1)
            {
                label2.Visible = false;
                dateTimePicker1.Visible = false;
                label3.Visible = true;
                dateTimePicker2.Visible = true;
            }
            else if (comboBox5.SelectedIndex == 4)
            {
                label2.Visible = true;
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
                label3.Visible = true;
            }
            else
            {
                label2.Visible = false;
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                label3.Visible = false;
            }
        }

        private void btnAddSup_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DateTime d1;
                DateTime d2;
                if (comboBox5.SelectedIndex == 0)
                {

                    d1 = DateTime.Now.AddDays(-1);
                    d2 = DateTime.Now;

                }
                else if (comboBox5.SelectedIndex == 1)
                {
                    d1 = Convert.ToDateTime("01/01/2016");
                    d2 = dateTimePicker2.Value;

                }
                else if (comboBox5.SelectedIndex == 2)
                {
                    d1 = DateTime.Now.AddDays(-7);
                    d2 = DateTime.Now;

                }
                else if (comboBox5.SelectedIndex == 3)
                {
                    d1 = DateTime.Now.AddDays(-30);
                    d2 = DateTime.Now;

                }
                else
                {
                    d1 = dateTimePicker1.Value;
                    d2 = dateTimePicker2.Value;

                }
                if (checkBox1.Checked == false && checkBox2.Checked == false)
                {
                    dataGridView1.DataSource = rf.PrintOutAllwithDateWithIDcaPLAC(textBox4.Text, d1, d2, Convert.ToInt32(comboBox1.SelectedValue), Convert.ToInt32(comboBox6.SelectedValue));
                }
                else if (checkBox1.Checked == false)
                {
                    dataGridView1.DataSource = rf.PrintOutAllwithDateWithPLAC(textBox4.Text, d1, d2, Convert.ToInt32(comboBox6.SelectedValue));
                }
                else if (checkBox2.Checked == false)
                {
                    dataGridView1.DataSource = rf.PrintOutAllwithDateWithIDca(textBox4.Text, d1, d2, Convert.ToInt32(comboBox1.SelectedValue));

                }
                else
                {
                    dataGridView1.DataSource = rf.PrintOutAllwithDateAll(textBox4.Text, d1, d2);

                }
                this.Cursor = Cursors.Default;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }

        private void btnRefrsh_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {


                DataTable dt = new DataTable();

                ////////// اضافة الاعمدة 
                try
                {
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dt.Columns.Add(dataGridView1.Columns[i].HeaderText);


                    }
                    dt.Columns.Add("اسم الموظفف");
                    dt.Columns.Add("اجمالي الكمية");
                    dt.Columns.Add("اجمالي السعر");
                    dt.Columns.Add("اجمالي الاجمالي");
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
                ///////////////////  أاضافة سطور 
                try
                {
                    int sumQu = 0;
                    int sumprice = 0;
                    int sumAll = 0;
                    foreach (DataGridViewRow dgr in dataGridView1.SelectedRows)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        DataRow dr = ((DataRowView)dgr.DataBoundItem).Row;
                        int ido = Convert.ToInt32(dr[0].ToString());
                        string nmCa = dr[1].ToString();
                        string nmty = dr[2].ToString();
                        string palce = dr[3].ToString();
                        int Qun = Convert.ToInt32(dr[4].ToString());
                        sumQu += Qun;
                        int prs = Convert.ToInt32(dr[5].ToString());
                        sumprice += prs;
                        int totl = Convert.ToInt32(dr[6].ToString());
                        sumAll += totl;
                        string currn = dr[7].ToString();
                        string amer = dr[8].ToString();
                        string astalm = dr[9].ToString(); ;
                        DateTime dd = DateTime.Parse(dr[10].ToString());

                        string dec = dr[11].ToString();
                        string nameUser = rf.GetUserNameBYIdUser(UserID);
                        dt.Rows.Add(ido, nmCa, nmty, palce, string.Format("{0:##,##}", Qun), string.Format("{0:##,##}", prs), string.Format("{0:##,##}", totl), currn, amer, astalm, dd.Date.ToShortDateString(), dec, nameUser, string.Format("{0:##,##}", sumQu), string.Format("{0:##,##}", sumprice), string.Format("{0:##,##}", sumAll));
                        this.Cursor = Cursors.Default;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    frmReprt frmr = new frmReprt(dt, null, 4);
                    frmr.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count > 0)
            {


                DataTable dt = new DataTable();

                ////////// اضافة الاعمدة 
                try
                {
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dt.Columns.Add(dataGridView1.Columns[i].HeaderText);


                    }
                    dt.Columns.Add("اسم الموظفف");
                    dt.Columns.Add("اجمالي الكمية");
                    dt.Columns.Add("اجمالي السعر");
                    dt.Columns.Add("اجمالي الاجمالي");
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
                ///////////////////  أاضافة سطور 
                try
                {
                    int sumQu = 0;
                    int sumprice = 0;
                    int sumAll = 0;
                    foreach (DataGridViewRow dgr in dataGridView1.Rows)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        DataRow dr = ((DataRowView)dgr.DataBoundItem).Row;
                        int ido = Convert.ToInt32(dr[0].ToString());
                        string nmCa = dr[1].ToString();
                        string nmty = dr[2].ToString();
                        string palce = dr[3].ToString();
                        int Qun = Convert.ToInt32(dr[4].ToString());
                        sumQu += Qun;
                        int prs = Convert.ToInt32(dr[5].ToString());
                        sumprice += prs;
                        int totl = Convert.ToInt32(dr[6].ToString());
                        sumAll += totl;
                        string currn = dr[7].ToString();
                        string amer = dr[8].ToString();
                        string astalm = dr[9].ToString(); ;
                        DateTime dd = DateTime.Parse(dr[10].ToString());

                        string dec = dr[11].ToString();
                        string nameUser = rf.GetUserNameBYIdUser(UserID);
                        dt.Rows.Add(ido, nmCa, nmty, palce, string.Format("{0:##,##}", Qun), string.Format("{0:##,##}", prs), string.Format("{0:##,##}", totl), currn, amer, astalm, dd.Date.ToShortDateString(), dec, nameUser, string.Format("{0:##,##}", sumQu), string.Format("{0:##,##}", sumprice), string.Format("{0:##,##}", sumAll));
                        this.Cursor = Cursors.Default;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    frmReprt frmr = new frmReprt(dt, null, 4);
                    frmr.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked==true)
            {
                comboBox1.Enabled = false;
            }
            else
            {
                comboBox1.Enabled = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                comboBox2.Enabled = false;
            }
            else
            {
                comboBox2.Enabled = true;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                comboBox3.Enabled = false;
            }
            else
            {
                comboBox3.Enabled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                comboBox6.Enabled = false;
            }
            else
            {
                comboBox6.Enabled = true;
            }
        }

        private void عددالاسطرالمحددةToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                MessageBox.Show(dataGridView1.SelectedRows.Count.ToString());
            }
        }

        private void عددجميعالاسطرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                MessageBox.Show(dataGridView1.RowCount.ToString());
            }
        }

        private void اجماليالكميةالمحددةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int count11 = dataGridView1.SelectedRows.Count;
                int sum = 0;
                for (int i = 0; i < count11; i++)
                {
                    sum += Convert.ToInt32(dataGridView1.SelectedRows[i].Cells[4].Value.ToString());

                }
                MessageBox.Show(sum.ToString());

            }
        }

        private void اجماليالسعرالمحددToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int count11 = dataGridView1.SelectedRows.Count;
                int sum = 0;
                for (int i = 0; i < count11; i++)
                {
                    sum += Convert.ToInt32(dataGridView1.SelectedRows[i].Cells[5].Value.ToString());

                }
                MessageBox.Show(sum.ToString());

            }
        }

        private void اجماليالاجماليالمحددToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int count11 = dataGridView1.SelectedRows.Count;
                int sum = 0;
                for (int i = 0; i < count11; i++)
                {
                    sum += Convert.ToInt32(dataGridView1.SelectedRows[i].Cells[6].Value.ToString());

                }
                MessageBox.Show(sum.ToString());

            }
        }
    }
}