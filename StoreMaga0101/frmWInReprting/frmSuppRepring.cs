using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrmRports;
using Excel = Microsoft.Office.Interop.Excel;
namespace frmWInReprting
{
    public partial class frmSuppRepring : Form
    { RepotFunction rf;
        int UserID;
        public frmSuppRepring()
        {
            InitializeComponent();
            try
            {
                UserID = 1;
                rf = new RepotFunction(@".\s2008", "StoreManagement1", null, null);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public frmSuppRepring(string ServerNm,string DbNm,string UserSql,string PassSql,int Userid)
        {
            InitializeComponent();
            try
            {

                rf = new RepotFunction(ServerNm, DbNm, UserSql, PassSql);
                UserID = Userid;
            }
            catch(Exception ex )
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void frmSuppRepring_Load(object sender, EventArgs e)
        {
          //  try
            {/////////
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox5.SelectedIndex = 0;
                getDate1();
                ////////////
                changeLanguage();
                MessageBoxManager.Yes = "نعم";
                MessageBoxManager.No = "الغاء";
                MessageBoxManager.Register();
                ///////

            }
          //  catch (Exception ex)
            {
            //    MessageBox.Show(ex.Message);
            }

        }
        /// //////
        /// </summary>
        void getDate1()
        {
           // try
            {
                comboBox1.DisplayMember = "اسم الصنف";
                comboBox1.ValueMember = "رقم الصنف";
                comboBox1.DataSource = rf.GetAllCategoryAR();
                comboBox2.DisplayMember = "اسم النوع";
                comboBox2.ValueMember = "رقم النوع";
                comboBox2.DataSource = rf.GetAllTypeQuntity();
                comboBox4.DisplayMember = "اسم العملة";
                comboBox4.ValueMember = "رقم العملة";
                comboBox4.DataSource = rf.GetAllCurrency();
                comboBox3.ValueMember = "رقم الموظف";
               comboBox3.DisplayMember = "اسم الموظف";
                comboBox3.DataSource = rf.GetAllUserAR();
            }
          //  catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// /////////
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
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
            if (checkBox3.Checked == true)
            {
                comboBox4.Enabled = false;
            }
            else
            {
                comboBox4.Enabled = true;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                comboBox3.Enabled = false;
            }
            else
            {
                comboBox3.Enabled = true;
            }
        }

        private void btnRefrsh_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataTable dt = new DataTable();

                try
                {
                    ////////// اضافة الاعمدة 
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dt.Columns.Add(dataGridView1.Columns[i].HeaderText);


                    }
                    dt.Columns.Add("اسم الموظفف");
                    dt.Columns.Add("اجمالي الكمية");
                    dt.Columns.Add("اجمالي السعر");
                    dt.Columns.Add("اجمالي الاجمالي");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                ///////////////////  أاضافة سطور 
                try
                {
                    int sumQint = 0;
                    int SumPrice = 0;
                    int SumAll = 0;
                    foreach (DataGridViewRow dgr in dataGridView1.SelectedRows)
                    {

                        DataRow dr = ((DataRowView)dgr.DataBoundItem).Row;
                        int idS = Convert.ToInt32(dr[0].ToString());
                        string nmCa = dr[1].ToString();
                        string nmty = dr[2].ToString();
                        int Qun = Convert.ToInt32(dr[3].ToString());
                        sumQint += Qun;
                        int prs = Convert.ToInt32(dr[4].ToString());
                        SumPrice += prs;
                        int totl = Convert.ToInt32(dr[5].ToString());
                        SumAll += totl;
                        string currn = dr[6].ToString();
                        DateTime dd = DateTime.Parse(dr[7].ToString());
                        string namee = dr[8].ToString();
                        string dec = dr[9].ToString();
                        string nameUser = rf.GetUserNameBYIdUser(UserID);
                        dt.Rows.Add(idS, nmCa, nmty, string.Format("{0:##,##}", Qun), string.Format("{0:##,##}", prs), string.Format("{0:##,##}", totl), currn, dd.Date.ToShortDateString(), namee, dec, nameUser, string.Format("{0:##,##}", sumQint), string.Format("{0:##,##}", SumPrice), string.Format("{0:##,##}", SumAll));

                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }


                try
                {

                    this.Cursor = Cursors.WaitCursor;
                    frmReprt frr = new frmReprt(dt, dt, 3);
                    frr.ShowDialog();
                 
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox5.SelectedIndex==1)
            {
                label2.Visible = false;
                dateTimePicker1.Visible = false;
                label3.Visible = true;
                dateTimePicker2.Visible = true;
            }
            else if(comboBox5.SelectedIndex==4)
            {
                label2.Visible = true;
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
                label3.Visible = true;
            }else
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

                if (checkBox2.Checked == false)
                {
                    dataGridView1.DataSource = rf.PrintRequstRPTIDcat(d1, d2, textBox4.Text, Convert.ToInt32(comboBox1.SelectedValue));
                }
                else
                {
                    dataGridView1.DataSource = rf.PrintRequstRPTAll(d1, d2, textBox4.Text);
                }
                this.Cursor = Cursors.Default;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DataTable dt = new DataTable();

                try
                {
                    ////////// اضافة الاعمدة 
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dt.Columns.Add(dataGridView1.Columns[i].HeaderText);


                    }
                    dt.Columns.Add("اسم الموظفف");
                    dt.Columns.Add("اجمالي الكمية");
                    dt.Columns.Add("اجمالي السعر");
                    dt.Columns.Add("اجمالي الاجمالي");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                ///////////////////  أاضافة سطور 
                try
                {
                    int sumQint = 0;
                    int SumPrice = 0;
                    int SumAll = 0;
                    foreach (DataGridViewRow dgr in dataGridView1.Rows)
                    {

                        DataRow dr = ((DataRowView)dgr.DataBoundItem).Row;
                        int idS = Convert.ToInt32(dr[0].ToString());
                        string nmCa = dr[1].ToString();
                        string nmty = dr[2].ToString();
                        int Qun = Convert.ToInt32(dr[3].ToString());
                        sumQint += Qun;
                        int prs = Convert.ToInt32(dr[4].ToString());
                        SumPrice += prs;
                        int totl = Convert.ToInt32(dr[5].ToString());
                        SumAll += totl;
                        string currn = dr[6].ToString();
                        DateTime dd = DateTime.Parse(dr[7].ToString());
                        string namee = dr[8].ToString();
                        string dec = dr[9].ToString();
                        string nameUser = rf.GetUserNameBYIdUser(UserID);
                        dt.Rows.Add(idS, nmCa, nmty, string.Format("{0:##,##}", Qun), string.Format("{0:##,##}", prs), string.Format("{0:##,##}", totl), currn, dd.Date.ToShortDateString(), namee, dec, nameUser, string.Format("{0:##,##}", sumQint), string.Format("{0:##,##}", SumPrice), string.Format("{0:##,##}", SumAll));

                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }


                try
                {

                    this.Cursor = Cursors.WaitCursor;
                    frmReprt frr = new frmReprt(dt, dt, 3);
                    frr.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
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
            if(dataGridView1.SelectedRows.Count > 0)
            {
                int count11 = dataGridView1.SelectedRows.Count;
                int sum = 0;
                for (int i = 0; i < count11; i++)
                {
                    sum += Convert.ToInt32(dataGridView1.SelectedRows[i].Cells[3].Value.ToString());

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
                    sum += Convert.ToInt32(dataGridView1.SelectedRows[i].Cells[4].Value.ToString());
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
                    sum += Convert.ToInt32(dataGridView1.SelectedRows[i].Cells[5].Value.ToString());
                }
                MessageBox.Show(sum.ToString());

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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
