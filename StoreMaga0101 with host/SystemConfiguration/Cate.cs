using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.ServiceModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SystemConfiguration
{ 
    public partial class Cate : Form
    {
        Config config;
        ServiceReference1.IserviceClient configHost;
        bool HostConnection;
        /// <summary>
        /// /counstpcort
        /// </summary>
        public Cate()
        {
            InitializeComponent();
            try
            { 
                config = new Config(@".\s2008", "StoreManagement1", null, null);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /////
        /// Cunst With Connction sql server
        public Cate(string ServerNm,string DbNm,string UserSql,string PassSql,bool hostconnection,string ipHost)
        {
            InitializeComponent();
            HostConnection = hostconnection;
            try
            {
                if (HostConnection == false)
                {
                    config = new Config(ServerNm, DbNm, UserSql, PassSql);
                }
                else
                {
                    configHost = new ServiceReference1.IserviceClient();
                    EndpointAddress endp = new EndpointAddress(ipHost);
                    configHost.Endpoint.Address = endp;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //
        // Evset Load Frm
        private void Cate_Load(object sender, EventArgs e)
        {
            try
            {
                combAccont.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                combAccont.AutoCompleteSource = AutoCompleteSource.ListItems;
                textBox4.Focus();
                combAccont.ValueMember = "رقم الحساب";
                combAccont.DisplayMember = "اسم الحساب";
                if (HostConnection == false)
                {
                    dataGridView1.DataSource = config.GetAllCategoryAR();
                    combAccont.DataSource = config.GETALLAccountSub();
                }
                else
                {
                    dataGridView1.DataSource =ConvertMemorytoDB( configHost.GetAllCategoryAR());
                    combAccont.DataSource =ConvertMemorytoDB( configHost.GETALLAccountSub());

                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        //
        // btn add New Cate
        private void btnAddSup_Click(object sender, EventArgs e)
        {
            if(textBox4.Text.Length>0 &&(int) combAccont.SelectedValue>0)
            {
                try
                { if (HostConnection == false)
                    {

                        if (checkBox1.Checked && (int)combAccont.SelectedValue > 0)
                        {
                            config.AddNewCategory(textBox4.Text, combAccont.SelectedValue);//add in tbl Category

                        }
                        else
                        {
                            config.AddNewCategory(textBox4.Text, null);
                        }
                        dataGridView1.DataSource = config.GetAllCategoryAR();

                        textBox4.Text = "";
                        textBox4.Focus();
                    }
                    else //conection host
                    {
                        if (checkBox1.Checked && (int)combAccont.SelectedValue > 0)
                        {
                            configHost.AddNewCategory(textBox4.Text, combAccont.SelectedValue);//add in tbl Category

                        }
                        else
                        {
                            configHost.AddNewCategory(textBox4.Text, null);
                        }
                        dataGridView1.DataSource =ConvertMemorytoDB( configHost.GetAllCategoryAR());

                        textBox4.Text = "";
                        textBox4.Focus();

                    }

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("يرجى تعبة جميع البيانات");
            }
        }
        /// <summary>
        /// تعديل
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefrsh_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Length > 0 && (int) combAccont.SelectedValue>0)
            { if (HostConnection == false)
                {
                    try
                    {
                        if (MessageBox.Show("هل تريد التعديل", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            if (checkBox1.Checked && (int)combAccont.SelectedValue > 0)
                            {
                                config.UpdateCategory(Convert.ToInt32(textBox3.Text), textBox4.Text, (int)combAccont.SelectedValue);
                            }
                            else
                            {

                                config.UpdateCategory(Convert.ToInt32(textBox3.Text), textBox4.Text, null);

                            }

                        }
                        dataGridView1.DataSource = config.GetAllCategoryAR();
                        textBox4.Focus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else// connection host
                {
                    try
                    {
                        if (MessageBox.Show("هل تريد التعديل", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            if (checkBox1.Checked && (int)combAccont.SelectedValue > 0)
                            {
                                configHost.UpdateCategory(Convert.ToInt32(textBox3.Text), textBox4.Text, (int)combAccont.SelectedValue);
                            }
                            else
                            {

                                configHost.UpdateCategory(Convert.ToInt32(textBox3.Text), textBox4.Text, null);

                            }

                        }
                        dataGridView1.DataSource =ConvertMemorytoDB( configHost.GetAllCategoryAR());
                        textBox4.Focus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }


            }
        }
        //
        // btn Delete cate
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length > 0)
            {
                try
                {
                    if (HostConnection == false)
                    {                        
                        if (MessageBox.Show("هل تريد حذف الصنف", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            DataTable dt = new DataTable();
                            int IDCAT = Convert.ToInt32(textBox3.Text);
                            dt = config.chackCatagory(IDCAT);
                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show("لايكمن حذف السجل .مرتبط بسجلات اخرى", "", MessageBoxButtons.OK);
                            }
                            else
                            {
                                config.DeleteCategory(IDCAT);


                                dataGridView1.DataSource = config.GetAllCategoryAR();
                                textBox4.Focus();
                                textBox4.Text = "";
                                textBox3.Text = "";
                            }
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("هل تريد حذف الصنف", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            DataTable dt = new DataTable();
                            int IDCAT = Convert.ToInt32(textBox3.Text);
                            dt =ConvertMemorytoDB( configHost.chackCatagory(IDCAT));
                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show("لايكمن حذف السجل .مرتبط بسجلات اخرى", "", MessageBoxButtons.OK);
                            }
                            else
                            {
                                configHost.DeleteCategory(IDCAT);


                                dataGridView1.DataSource =ConvertMemorytoDB( configHost.GetAllCategoryAR());
                                textBox4.Focus();
                                textBox4.Text = "";
                                textBox3.Text = "";
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //
        // Btn Close Form
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //
        // Date gride Chosse
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count>0)
            {
                try
                {
                    textBox3.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    textBox4.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    checkBox1.Checked = false;
                    combAccont.Enabled = false;
                    combAccont.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

      
        //
        // change CheckBox
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                combAccont.Enabled = true;
            else
                combAccont.Enabled = false;

        }
        //
        // btn refirsh 
        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox4.Text = "";
            combAccont.DataSource = config.GETALLAccountSub();

        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Enter)
            { if(HostConnection==false)
                  dataGridView1.DataSource = config.GetCategoryByName(textBox4.Text);
            else
                    dataGridView1.DataSource =ConvertMemorytoDB( configHost.GetCategoryByName(textBox4.Text));

            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

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
        DataTable ConvertMemorytoDB(MemoryStream ms)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            ms.Seek(0, SeekOrigin.Begin);
            DataTable dt = (DataTable)formatter.Deserialize(ms);
            return dt;
        }
    }
}
