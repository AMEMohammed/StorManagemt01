﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace SystemConfiguration
{
    public partial class frmCuurncy : Form
    {
        Config config;
        public frmCuurncy()
        {
            InitializeComponent();
            try
            {
                config = new Config(@".\s2008", "StoreManagement1", null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public frmCuurncy(string ServerNm, string DbNm, string UserSql, string PassSql)
        {
            InitializeComponent();
            try
            {
                config = new Config(ServerNm, DbNm, UserSql, PassSql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// /////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCuurncy_Load(object sender, EventArgs e)
        {
            try
            {
                textBox4.Focus();
                dataGridView1.DataSource = config.GetAllCurrency();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddSup_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Length > 0)
            {
                try
                {
                    config.AddNewCurrency(textBox4.Text);
                    dataGridView1.DataSource = config.GetAllCurrency();
                    textBox4.Text = "";
                    textBox4.Focus();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnRefrsh_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Length > 0)
            {
                try
                {
                    if (MessageBox.Show("هل تريد التعديل", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        config.UpdateCurrency(Convert.ToInt32(textBox3.Text), textBox4.Text);


                    dataGridView1.DataSource = config.GetAllCurrency();
                    textBox4.Focus();
                    textBox4.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length > 0)
            {
                try
                {
                    if (MessageBox.Show("هل تريد حذف العملة", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DataTable dt = new DataTable();
                        int IDcur = Convert.ToInt32(textBox3.Text);
                        dt = config.chackCurncy(IDcur);
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("لايكمن حذف السجل .مرتبط بسجلات اخرى", "", MessageBoxButtons.OK);
                        }
                        else
                        {
                            config.DeleteCurrency(IDcur);


                            dataGridView1.DataSource = config.GetAllCurrency();
                            textBox4.Focus();
                            textBox4.Text = "";
                            textBox3.Text = "";
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    textBox3.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    textBox4.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();


                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.Message);
                }
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Shift && e.KeyCode==Keys.Enter)
            {
                dataGridView1.DataSource = config.GETCurrencyBYName(textBox4.Text);
            }
        }
        /// <summary>
        /// تصدير الى اكسل
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void تصديرالىاكسلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog ofd = new SaveFileDialog();
                ofd.Filter = "EXCEL FILE | *.xls";
                ofd.ShowDialog();

                if (ofd.FileName != "")
                {
                    Excel.Application xlApp;
                    Excel.Workbook xlWorkBook;
                    Excel.Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;

                    xlApp = new Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Add(misValue);
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    xlWorkSheet.Name = this.Text;
                    // storing header part in Excel  
                    for (int i1 = 1; i1 < dataGridView1.Columns.Count + 1; i1++)
                    {
                        xlWorkSheet.Cells[1, i1] = dataGridView1.Columns[i1 - 1].HeaderText;
                    }
                    int i = 0;
                    int j = 0;

                    for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                    {
                        for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                        {
                            DataGridViewCell cell = dataGridView1[j, i];
                            xlWorkSheet.Cells[i + 2, j + 1] = cell.Value;
                        }
                    }

                    xlWorkBook.SaveAs(ofd.FileName + ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

                    xlWorkBook.Close(true, misValue, misValue);

                    xlApp.Quit();

                    releaseObject(xlWorkSheet);
                    releaseObject(xlWorkBook);
                    releaseObject(xlApp);
                }
            }
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }

        }
    }
}