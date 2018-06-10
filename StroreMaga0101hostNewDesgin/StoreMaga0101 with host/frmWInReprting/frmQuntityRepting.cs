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
    public partial class frmQuntityRepting : Form
    {
        RepotFunction rf;
        int UserID;
        bool HostConnection;
        ServiceReference1.IserviceClient rfHost;
        public frmQuntityRepting()
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
        public frmQuntityRepting(string ServerNm, string DbNm, string UserSql, string PassSql, int Userid,bool hostconnection,string iphost)
        {
            InitializeComponent();
            HostConnection = hostconnection;
            try
            {
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


        private void frmQuntityRepting_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
                CombboxGroup.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                CombboxGroup.AutoCompleteSource = AutoCompleteSource.ListItems;
                GetData1();
                MessageBoxManager.Yes = "نعم";
                MessageBoxManager.No = "الغاء";
                MessageBoxManager.Register();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// //
        /// </summary>
        void GetData1()
        {
            try
            {
                comboBox1.DisplayMember = "اسم الصنف";
                comboBox1.ValueMember = "رقم الصنف";

                comboBox2.DisplayMember = "اسم النوع";
                comboBox2.ValueMember = "رقم النوع";

                comboBox3.DisplayMember = "اسم العملة";
                comboBox3.ValueMember = "رقم العملة";

                CombboxGroup.DisplayMember = "اسم المجموعة";
                CombboxGroup.ValueMember = "رقم المجموعة";
                if (HostConnection == false)
                {
                    comboBox3.DataSource = rf.GetAllCurrency();
                    comboBox2.DataSource = rf.GetAllTypeQuntity();
                    comboBox1.DataSource = rf.GetAllCategoryAR();
                    CombboxGroup.DataSource = rf.GetGroupsCate();
                }
                else
                {
                    comboBox3.DataSource =ConvertMemorytoDB( rfHost.GetAllCurrency());
                    comboBox2.DataSource =ConvertMemorytoDB( rfHost.GetAllTypeQuntity());
                    comboBox1.DataSource =ConvertMemorytoDB( rfHost.GetAllCategoryAR());
                    CombboxGroup.DataSource =ConvertMemorytoDB( rfHost.GetGroupsCate());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddSup_Click(object sender, EventArgs e)
        { try
            {
                this.Cursor = Cursors.WaitCursor;
                if (HostConnection == false)
                {
                    if (checkBox1.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false)
                    {
                        dataGridView1.DataSource = rf.PrintAccountQuntity(Convert.ToInt32(comboBox1.SelectedValue), Convert.ToInt32(comboBox2.SelectedValue), Convert.ToInt32(comboBox3.SelectedValue));
                    }
                    else if (checkBox1.Checked == false)
                    {
                        dataGridView1.DataSource = rf.PrintAccountQuntityIDac(Convert.ToInt32(comboBox1.SelectedValue));

                    }
                    else if (checkBox2.Checked) // select data from Group
                    {
                        //MessageBox.Show(CombboxGroup.SelectedValue.ToString());
                        if ((int)CombboxGroup.SelectedValue > 0)
                        {
                            rf.PrintAccountQuntityWithGroup((int)CombboxGroup.SelectedValue);
                        }

                    }
                    else
                    {
                        dataGridView1.DataSource = rf.PrintAccountQuntityAll();
                    }
                }
                else //connection Host
                {
                    if (checkBox1.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false)
                    {
                        dataGridView1.DataSource =ConvertMemorytoDB (rfHost.PrintAccountQuntity(Convert.ToInt32(comboBox1.SelectedValue), Convert.ToInt32(comboBox2.SelectedValue), Convert.ToInt32(comboBox3.SelectedValue)));
                    }
                    else if (checkBox1.Checked == false)
                    {
                        dataGridView1.DataSource =ConvertMemorytoDB( rfHost.PrintAccountQuntityIDac(Convert.ToInt32(comboBox1.SelectedValue)));

                    }
                    else if (checkBox2.Checked) // select data from Group
                    {
                        //MessageBox.Show(CombboxGroup.SelectedValue.ToString());
                        if ((int)CombboxGroup.SelectedValue > 0)
                        {
                            rfHost.PrintAccountQuntityWithGroup((int)CombboxGroup.SelectedValue);
                        }

                    }
                    else
                    {
                        dataGridView1.DataSource =ConvertMemorytoDB( rfHost.PrintAccountQuntityAll());
                    }
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
                try
                {

                    ////////// اضافة الاعمدة 
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dt.Columns.Add(dataGridView1.Columns[i].HeaderText);


                    }
                    dt.Columns.Add("اسم الموظفف");
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }

                ///////////////////  أاضافة سطور 
                try
                {
                    foreach (DataGridViewRow dgr in dataGridView1.SelectedRows)
                    {

                        DataRow dr = ((DataRowView)dgr.DataBoundItem).Row;
                        string nmca = dr[0].ToString();
                        string nmty = dr[1].ToString();
                        int qunt = Convert.ToInt32(dr[2].ToString());
                        int pres = Convert.ToInt32(dr[3].ToString());
                        string currnt = dr[4].ToString();
                        string nameUser;
                        if (HostConnection == false)
                        {
                            nameUser = rf.GetUserNameBYIdUser(UserID);
                        }
                        else
                        {
                            nameUser = rfHost.GetUserNameBYIdUser(UserID);
                        }
                        dt.Rows.Add(nmca, nmty, string.Format("{0:##,##}", qunt), string.Format("{0:##,##}", pres), currnt, nameUser);
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    frmReprt frmrep = new frmReprt(dt, null, 5);
                    frmrep.ShowDialog();
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
                try
                {

                    ////////// اضافة الاعمدة 
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dt.Columns.Add(dataGridView1.Columns[i].HeaderText);


                    }
                    dt.Columns.Add("اسم الموظفف");
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }

                ///////////////////  أاضافة سطور 
                try
                {
                    foreach (DataGridViewRow dgr in dataGridView1.Rows)
                    {

                        DataRow dr = ((DataRowView)dgr.DataBoundItem).Row;
                        string nmca = dr[0].ToString();
                        string nmty = dr[1].ToString();
                        int qunt = Convert.ToInt32(dr[2].ToString());
                        int pres = Convert.ToInt32(dr[3].ToString());
                        string currnt = dr[4].ToString();
                        string nameUser;
                        if (HostConnection == false)
                        {
                            nameUser = rf.GetUserNameBYIdUser(UserID);
                        }
                        else
                        {
                            nameUser = rfHost.GetUserNameBYIdUser(UserID);
                        }
                        dt.Rows.Add(nmca, nmty, string.Format("{0:##,##}", qunt), string.Format("{0:##,##}", pres), currnt, nameUser);
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    frmReprt frmrep = new frmReprt(dt, null, 5);
                    frmrep.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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
                comboBox1.Enabled = false;
            }
            else

            {
                comboBox1.Enabled = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                CombboxGroup.Enabled = true;


            }
            else
            {
                CombboxGroup.Enabled = false;
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
