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
                    config.AddNewGroup((int)comboBox1.SelectedValue, txtNameGroup.Text, group.Text, USERID, DateTime.Now);
                    

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
            try
            {
                Refrish();
                dataGridView1.DataSource = config.GetAllGroup();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //// refrish Data
        private void Refrish()
        {
            comboBox1.SelectedIndex = 0;
            group.Text = "";
            txtIDgroup.Text = "";
            txtNameGroup.Text = "";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            { if (dataGridView1.SelectedRows.Count > 0)
                     txtIDgroup.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                     txtNameGroup.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                     txtDecrp.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                     comboBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

            }
            catch
            {

            }
        }

        private void btnRefrsh_Click(object sender, EventArgs e)
        {
            if(txtIDgroup.Text.Length>0)
            {
                try
                {
                    if (MessageBox.Show("هل تريد التعديل","",MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(txtIDgroup.Text);
                        config.UpdateGroup(id, (int)comboBox1.SelectedValue, txtNameGroup.Text, txtDecrp.Text, USERID);
                        dataGridView1.DataSource = config.GetOneGroup(id);

                        Refrish();
                    }
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtIDgroup.Text.Length > 0)
            {
                new frmAdditms((int)comboBox1.SelectedValue, Convert.ToInt32(txtIDgroup.Text), USERID, Users.ConServer.ServerNM,Users. ConServer.DBNM, Users.ConServer.UserSql, Users.ConServer.PassSql).ShowDialog();

            }


        }

        private void txtNameGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Shift && e.KeyCode==Keys.Enter)
            {
               dataGridView1.DataSource= config.GetGroupByName(txtNameGroup.Text);
            }
        }
        /// <summary>
        /// btn delete group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtIDgroup.Text.Length > 0)
            {
                if (config.CheckGroupItems((Convert.ToInt32(txtIDgroup.Text))))
                {
                    MessageBox.Show("لا يمكن حذف المجوعة لانها تحتوي على عناصر");

                }
                else
                {
                    config.DeleteGroup(Convert.ToInt32(txtIDgroup.Text));
                }


           }
        }

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
