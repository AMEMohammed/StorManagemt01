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
namespace Out_
{
    public partial class frmUpdateOut : Form
    {
        int UserID;
        OutFunction OutFun;
  
        public frmUpdateOut()
        {
            InitializeComponent();
            UserID = 1;
       
            try
            {
                OutFun = new OutFunction(@".\s2008", "StoreManagement1",null,null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        
        public frmUpdateOut(string ServerNm, string DbNm,string UserSql,string PassSql, int UserId)
        {
            InitializeComponent();
            UserID = UserId;
            try
            {
                OutFun = new OutFunction(ServerNm, DbNm,UserSql,PassSql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
         /// <summary>
         ///
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void frmUpdateOut_Load(object sender, EventArgs e)
        {
            MessageBoxManager.Yes = "نعم";
            MessageBoxManager.No = "الغاء";
            MessageBoxManager.Register();
            try
            {
                changeLanguage();
                dataGridView1.DataSource = OutFun.SearchINRequstOutDate(DateTime.Now.AddDays(-1), DateTime.Now);
                comboBox1.SelectedIndex = 0;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }


        }

        /// <summary>
        /// // change language
        /// </summary>
        public void changeLanguage()
        {
            try
            {
                foreach (InputLanguage lng in InputLanguage.InstalledInputLanguages)
                {
                    if (lng.LayoutName == "العربية (101)")
                        InputLanguage.CurrentInputLanguage = lng;
                }
            }
            catch
            {

            }
        }

        private void btnRefrsh_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)

            {
                try
                {
                    int IDcheck = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[13].Value.ToString());
                    string name = dataGridView1.SelectedRows[0].Cells[11].Value.ToString();
                   
                    DataTable dtexit = new DataTable();
                    DataTable dtOut = new DataTable();
                    if (checkBox2.Checked)
                    {
                        dtexit = OutFun.printrequstOutExit(IDcheck, UserID, OutFun.GetIdUser(name));
                    }
                    else
                    {
                        dtexit = null;
                    }
                    //  frmREPORT frm = new frmREPORT(IDcheck, 2, dbsql.GetIdUser(name), printExit);
                    dtOut = OutFun.PrintRequstOut(IDcheck, UserID, OutFun.GetIdUser(name));
                    this.Cursor = Cursors.WaitCursor;
                    frmReprt frmrepprt = new frmReprt(dtOut ,dtexit, 2);
                    frmrepprt.ShowDialog();
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

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataTable dt = new DataTable();


                ////////// اضافة الاعمدة 
                try
                {
                    for (int i = 0; i < dataGridView1.Columns.Count - 1; i++)
                    {
                        dt.Columns.Add(dataGridView1.Columns[i].HeaderText);


                    }
                    dt.Columns.Add("اسم الموظفف");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ///////////////////  أاضافة سطور 
                try
                {
                    int sumQu = 0;
                    int sumPrice = 0;
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
                        sumPrice += prs;
                        int totl = Convert.ToInt32(dr[6].ToString());
                        sumPrice += totl;
                        string currn = dr[7].ToString();
                        string amer = dr[8].ToString();
                        string astalm = dr[9].ToString(); ;
                        DateTime dd = DateTime.Parse(dr[10].ToString());

                        string dec = dr[11].ToString();
                        string nameUser = OutFun.GetUserNameBYIdUser(UserID);
                        dt.Rows.Add(ido, nmCa, nmty, palce, string.Format("{0:##,##}", Qun), string.Format("{0:##,##}", prs), string.Format("{0:##,##}", totl), currn, amer, astalm, dd.Date.ToShortDateString(), dec, " ", nameUser, string.Format("{0:##,##}", sumQu), string.Format("{0:##,##}", sumPrice), string.Format("{0:##,##}", sumAll));
                        this.Cursor = Cursors.Default;
                  
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    frmReprt frmrepprt = new frmReprt(dt,dt, 4);
                    frmrepprt.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count > 0)

            //{
            //    try
            //    {
            //        int IDcheck = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[13].Value.ToString());
            //        string name = dataGridView1.SelectedRows[0].Cells[11].Value.ToString();
            //        bool printExit = false;
            //        if (checkBox2.Checked)
            //        {
            //            printExit = true;
            //        }
            //        else
            //        {
            //            printExit = false;
            //        }
            //        frmREPORT frm = new frmREPORT(IDcheck, 2, dbsql.GetIdUser(name), printExit);
            //        this.Cursor = Cursors.WaitCursor;
            //        frm.ShowDialog();
            //        this.Cursor = Cursors.Default;
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);

            //    }
            //}
        }

        private void تعديلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    frmUpdateOut2 frmu = new frmUpdateOut2();
                    frmu.Tag = id;
                    this.Cursor = Cursors.WaitCursor;

                    frmu.ShowDialog();
                    this.Cursor = Cursors.Default;
                    dataGridView1.DataSource = OutFun.SearchINRequstOutDate(DateTime.Now.AddDays(-3), DateTime.Now);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void btnAddSup_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex==0)
            {
                dataGridView1.DataSource = OutFun.SearchINRequsetOutTxtAndDate(textBox3.Text, DateTime.Now.AddDays(-1), DateTime.Now);
            }
            else if(comboBox1.SelectedIndex==1)
            {
                dataGridView1.DataSource = OutFun.SearchINRequsetOutTxtAndDate(textBox3.Text,Convert.ToDateTime("2016/01/01"), dateTimePicker2.Value);
            }
            else if(comboBox1.SelectedIndex==2)
            {
                dataGridView1.DataSource = OutFun.SearchINRequsetOutTxtAndDate(textBox3.Text, DateTime.Now.AddDays(-7), DateTime.Now);
            }
            else if(comboBox1.SelectedIndex==3)
            {
                dataGridView1.DataSource = OutFun.SearchINRequsetOutTxtAndDate(textBox3.Text, DateTime.Now.AddDays(-30), DateTime.Now);
            }
            else  if(comboBox1.SelectedIndex==4)
            {
                dataGridView1.DataSource = OutFun.SearchINRequsetOutTxtAndDate(textBox3.Text, dateTimePicker1.Value, dateTimePicker2.Value);
            }

        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int IdOut = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    //MessageBox.Show(IdOut.ToString());
           if (MessageBox.Show("هل تريد استرداد الكمية المصروفة رقم الطلب " + IdOut + "", "استرداد طلب صرف", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes)
                    { 
                        OutFun.DeleteRqustOut(IdOut, UserID);
                        dataGridView1.DataSource = OutFun.SearchINRequstOutDate(DateTime.Now.AddDays(-3), DateTime.Now);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex==4)
            {
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
            }
            else if(comboBox1.SelectedIndex == 1)
            {
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = true;
                label2.Visible = false;
                label3.Visible = true;
            }
            else
            {
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
            }
        }
    }
}