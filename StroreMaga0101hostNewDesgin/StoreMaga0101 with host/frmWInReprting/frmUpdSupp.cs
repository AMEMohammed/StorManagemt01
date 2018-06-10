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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;

namespace frmWInReprting
{
    public partial class frmUpdSupp : Form
    {
        RepotFunction rf;
        int UserID;
        ServiceReference1.IserviceClient rfHost = new ServiceReference1.IserviceClient();
        bool HostConnection;
        public frmUpdSupp()
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
        public frmUpdSupp(string ServerNm, string DbNm, string UserSql, string PassSql, int Userid,bool hostConnection,string iphost)
        {
            InitializeComponent();
            try
            {
                HostConnection = hostConnection;
                if (HostConnection == false)
                {
                    rf = new RepotFunction(ServerNm, DbNm, UserSql, PassSql);
                }
                else
                {
                    rfHost = new ServiceReference1.IserviceClient();
                    EndpointAddress endp = new EndpointAddress(iphost);
                    rfHost.Endpoint.Address = endp;
                }
                UserID = Userid;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

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
                if (HostConnection == false)
                {
                    if (textBox4.Text.Length > 0)
                    {
                        dataGridView1.DataSource = rf.GetUpdateSupplyByIDSupply(Convert.ToInt32(textBox4.Text));
                    }
                    else if (radioButton1.Checked)

                    {
                        dataGridView1.DataSource = rf.GetUpdateSupplyByDate(d1, d2);
                    }
                    else if (radioButton2.Checked)
                    {
                        dataGridView1.DataSource = rf.GetUpdateSupplyByDateUpdateWithDate(d1, d2);

                    }
                    else
                    {
                        dataGridView1.DataSource = rf.GetUpdateSupplyByDateDeleteWithDate(d1, d2);
                    }
                }
                else//connection host
                {
                    if (textBox4.Text.Length > 0)
                    {
                        dataGridView1.DataSource =ConvertMemorytoDB( rfHost.GetUpdateSupplyByIDSupply(Convert.ToInt32(textBox4.Text)));
                    }
                    else if (radioButton1.Checked)

                    {
                        dataGridView1.DataSource =ConvertMemorytoDB( rfHost.GetUpdateSupplyByDate(d1, d2));
                    }
                    else if (radioButton2.Checked)
                    {
                        dataGridView1.DataSource =ConvertMemorytoDB( rfHost.GetUpdateSupplyByDateUpdateWithDate(d1, d2));

                    }
                    else
                    {
                        dataGridView1.DataSource =ConvertMemorytoDB( rfHost.GetUpdateSupplyByDateDeleteWithDate(d1, d2));
                    }

                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
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
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                ///////////////////  أاضافة سطور 
                try
                {
                    foreach (DataGridViewRow dgr in dataGridView1.SelectedRows)
                    {
                        DataRow dr = ((DataRowView)dgr.DataBoundItem).Row;
                        int id = Convert.ToInt32(dr[0].ToString());
                        int idSu = Convert.ToInt32(dr[1].ToString());
                        string NCat = dr[2].ToString();
                        string TypeCA = dr[3].ToString();
                        int Qunit = Convert.ToInt32(dr[4].ToString());
                        int Price = Convert.ToInt32(dr[5].ToString());
                        string currn = dr[6].ToString();
                        string namee = dr[7].ToString();
                        DateTime dd = DateTime.Parse(dr[8].ToString());
                        string dec = dr[9].ToString();
                        string nameUser;
                        if (HostConnection == false)
                        {
                            nameUser = rf.GetUserNameBYIdUser(UserID);
                        }
                        else
                        {
                            nameUser = rfHost.GetUserNameBYIdUser(UserID);
                        }
                        dt.Rows.Add(id, idSu, NCat, TypeCA, string.Format("{0:##,##}", Qunit), string.Format("{0:##,##}", Price), currn, namee, dd.Date.ToShortDateString(), dec, " ", nameUser);


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    frmReprt frmr = new frmReprt(dt, null, 6);
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

            if (dataGridView1.Rows.Count > 0)
            {
                DataTable dt = new DataTable();


                ////////// اضافة الاعمدة 
                try
                {
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dt.Columns.Add(dataGridView1.Columns[i].HeaderText);


                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                ///////////////////  أاضافة سطور 
                try
                {
                    foreach (DataGridViewRow dgr in dataGridView1.Rows)
                    {
                        DataRow dr = ((DataRowView)dgr.DataBoundItem).Row;
                        int id = Convert.ToInt32(dr[0].ToString());
                        int idSu = Convert.ToInt32(dr[1].ToString());
                        string NCat = dr[2].ToString();
                        string TypeCA = dr[3].ToString();
                        int Qunit = Convert.ToInt32(dr[4].ToString());
                        int Price = Convert.ToInt32(dr[5].ToString());
                        string currn = dr[6].ToString();
                        string namee = dr[7].ToString();
                        DateTime dd = DateTime.Parse(dr[8].ToString());
                        string dec = dr[9].ToString();

                        dt.Rows.Add(id, idSu, NCat, TypeCA, string.Format("{0:##,##}", Qunit), string.Format("{0:##,##}", Price), currn, namee, dd.Date.ToShortDateString(), dec);


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    frmReprt frmrr = new frmReprt(dt, null, 6);
                    frmrr.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void frmUpdSupp_Load(object sender, EventArgs e)
        {
            comboBox5.SelectedIndex = 0;
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
        //convert memoryStreem to datatable
        DataTable ConvertMemorytoDB(MemoryStream ms)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            ms.Seek(0, SeekOrigin.Begin);
            DataTable dt = (DataTable)formatter.Deserialize(ms);
            return dt;
        }
    }
    }
            