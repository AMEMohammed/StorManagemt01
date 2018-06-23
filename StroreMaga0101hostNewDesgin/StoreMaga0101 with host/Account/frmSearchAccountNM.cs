using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Users;
using FrmRports;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
namespace Account
{
    public partial class frmSearchAccountNM : Form
    {
        AccountNm Acn;
        ServiceReference1.IserviceClient AcnHost;
        bool HostConnection = false;
        int IDUSER;
        public frmSearchAccountNM()
        {
            InitializeComponent();
            try
            {
                Acn = new AccountNm(@".\s2008", "StoreManagement1", null, null);
                IDUSER = 1; 
              
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        ///
        public frmSearchAccountNM(string ServNm, string DbNm, string UesrSql, string PassSql, int UserId,bool hostConnection,string HostIp)
        {
            InitializeComponent();
            try
            {
                HostConnection = hostConnection;
                if (hostConnection == false)
                {
                    Acn = new AccountNm(ServNm, DbNm, UesrSql, PassSql);
                }
                else
                {
                    AcnHost = new ServiceReference1.IserviceClient();
                    EndpointAddress endp = new EndpointAddress(HostIp);
                    AcnHost.Endpoint.Address = endp;
                }
                IDUSER = UserId;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        int IDTypeAccontPrime;// حساب رئيسي 1 او فرعي 2
        int IdAllAcount; // كافة الحسابات الفرعية 1 او لا2
        int IDYType; // نو الحساب اجمالي 1 او تق=فصيلي 2
         

        private void frmSearchAccountNM_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox2.SelectedIndex = 0;
                IDYType = 1;
                GETCurrncy();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void GETCurrncy()
        {  if (HostConnection == false)
            {
                comboBox4.DataSource = Acn.GetAllCurrency();
            }
        else
            {
                comboBox4.DataSource =ConvertMemorytoDB(AcnHost.GetAllCurrency());
            }
            comboBox4.ValueMember = "رقم العملة";
            comboBox4.DisplayMember = "اسم العملة";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                comboBox4.Enabled = false;

            }
            else
            {
                comboBox4.Enabled = true;
            }
        }
        /// <summary>
        /// / الحساب الرئيسي
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            groupBox4.Enabled = false;
            groupBox2.Enabled =false;
            groupBox5.Enabled = true; 
            IdAllAcount = 2;
            IDTypeAccontPrime = 1;
            IDYType = 1;
            if (HostConnection == false)
            {
                comboBox1.DataSource = Acn.GetGroupsAsAccounts();
            }
            else
            {
                comboBox1.DataSource = ConvertMemorytoDB(AcnHost.GetGroupsAsAccounts());
            }

            comboBox1.ValueMember = "رقم المجموعة";
            comboBox1.DisplayMember = "اسم المجموعة";
        }
        /// <summary>
        /// ///// الحسابات الفرعية
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            groupBox4.Enabled = false;
            groupBox5.Enabled = false;
            IdAllAcount = 1;
            IDTypeAccontPrime = 2;
            IDYType = 1;
            if (HostConnection == false)
            {
                comboBox1.DataSource = Acn.GETALLAccountSub();
            }
            else
            {
                comboBox1.DataSource = ConvertMemorytoDB(AcnHost.GETALLAccountSub());
            }
            comboBox1.ValueMember = "رقم الحساب";
            comboBox1.DisplayMember = "اسم الحساب";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox4.Enabled =true;
            groupBox5.Enabled = true;
            IdAllAcount = 2;
            IDTypeAccontPrime = 2;
            IDYType = 1;
            if (HostConnection == false)
            {
                comboBox1.DataSource = Acn.GETALLAccountSub();
            }
            else
            {
                comboBox1.DataSource =ConvertMemorytoDB ( AcnHost.GETALLAccountSub());
            }
            comboBox1.ValueMember = "رقم الحساب";
            comboBox1.DisplayMember = "اسم الحساب";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        { if (radioButton1.Checked)
            {
                IDYType = 1;
                groupBox9.Visible = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked==true)
            {
                IDYType = 2;
                groupBox9.Visible = true;
                checkBox1.Checked = false;
                comboBox4.Enabled = true;
            }
        }

        private void btnAddSup_Click(object sender, EventArgs e)
        {
            // try
            //   {
            DateTime d1 = DateTime.Now;
            DateTime d2 = DateTime.Now;
            if (comboBox2.SelectedIndex == 0)
            {
                d1 = DateTime.Now.AddDays(-1);
                d2 = DateTime.Now;
            }
            if (comboBox2.SelectedIndex == 1)
            {
                d1 = Convert.ToDateTime("01/01/2017");
                d2 = dateTimePicker2.Value;
            }
            if (comboBox2.SelectedIndex == 2)
            {
                d1 = DateTime.Now.AddDays(-7);
                d2 = DateTime.Now;

            }
            if (comboBox2.SelectedIndex == 3)
            {
                d1 = DateTime.Now.AddDays(-30);
                d2 = DateTime.Now;
            }
            if (comboBox2.SelectedIndex == 4)
            {
                d1 = dateTimePicker1.Value;
                d2 = dateTimePicker2.Value;

            }
            ///  جلب كشاف حساب فرعي بجميع العملات اجمالي
            if (IDYType == 1 && checkBox1.Checked && IDTypeAccontPrime == 2 && IdAllAcount == 2)//نوع الحساب اجمالي ,وكافة العملات
            {
                if (HostConnection == false)
                {
                    dataGridView1.DataSource = Acn.GetBalanceAccountALLCunncy((int)comboBox1.SelectedValue);
                }
                else
                {
                    dataGridView1.DataSource = ConvertMemorytoDB(AcnHost.GetBalanceAccountALLCunncy((int)comboBox1.SelectedValue));

                }

            }// جلب كشف حساب فرعي بعملة واحدة اجمالي 
            else if (IDYType == 1 && checkBox1.Checked == false && IDTypeAccontPrime == 2 && IdAllAcount == 2)//نوع الحساب اجمالي وعملة محددة
            {
                int IDcurrncy = (int)comboBox4.SelectedValue;
                if (HostConnection == false)
                {
                    dataGridView1.DataSource = Acn.GetBalanceAccount((int)comboBox1.SelectedValue, IDcurrncy, comboBox4.Text);
                }
                else
                {
                    dataGridView1.DataSource = ConvertMemorytoDB(AcnHost.GetBalanceAccount((int)comboBox1.SelectedValue, IDcurrncy, comboBox4.Text));
                }
            }
            ///   جلب كشف حسابات جميع الحسابات الفرعية بجميع العملات اجمالي
            else if (IDYType == 1 && checkBox1.Checked && IDTypeAccontPrime == 2 && IdAllAcount == 1)
            {
                if (HostConnection == false)
                    dataGridView1.DataSource = Acn.GetBalanceALLAccountALLCunncy(-1);
                else
                    dataGridView1.DataSource = ConvertMemorytoDB(AcnHost.GetBalanceALLAccountALLCunncy(-1));
            }
            // جلب جميع الحسابات الفرعية بعملة واحدة اجمالي
            else if (IDYType == 1 && checkBox1.Checked == false && IDTypeAccontPrime == 2 && IdAllAcount == 1)
            {
                int IDcurrncy = (int)comboBox4.SelectedValue;
                if (HostConnection == false)
                    dataGridView1.DataSource = Acn.GetBalanceALLAccountALLCunncy(IDcurrncy);
                else
                    dataGridView1.DataSource = ConvertMemorytoDB(AcnHost.GetBalanceALLAccountALLCunncy(IDcurrncy));
            }
            // جلب كشف حساب فرعي تفصليلي عملة واحدة
            else if (IDYType == 2 && checkBox1.Checked == false && IDTypeAccontPrime == 2 && IdAllAcount == 2)//نوع الحساب تفصيلي وعملة محددة
            {
                int IDcurrncy = (int)comboBox4.SelectedValue;
                if (HostConnection == false)
                    dataGridView1.DataSource = Acn.GETAccountDitalis((int)comboBox1.SelectedValue, IDcurrncy, d1, d2);
                else
                    dataGridView1.DataSource = ConvertMemorytoDB(AcnHost.GETAccountDitalis((int)comboBox1.SelectedValue, IDcurrncy, d1, d2));
            }// جلب كشف حساب لمجمعة محددة من الحسابات
            else if (IDTypeAccontPrime == 1)
            {
                if ((int)comboBox1.SelectedValue > 0)
                {
                    if (HostConnection == false)
                        dataGridView1.DataSource = Acn.GetAccountesMOnayInGroup(Convert.ToInt32(comboBox1.SelectedValue.ToString()));
                    else
                        dataGridView1.DataSource = ConvertMemorytoDB(AcnHost.GetAccountesMOnayInGroup(Convert.ToInt32(comboBox1.SelectedValue.ToString())));
                }


            }
            // }
            // catch(Exception ex)
            // { MessageBox.Show(ex.Message); }
        }

       //
       /// <summary>
       /// 
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex == 4)
            {
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
            }
            else if (((ComboBox)sender).SelectedIndex == 1)
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        ///  طباعة كشف حساب
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (radioButton2.Checked)
            {
                try
                {
                    MessageBox.Show(comboBox1.Text);
                    DataTable dt = new DataTable();
                    dt.Columns.Add("اسم_الحساب");
                    dt.Columns.Add("نوع الحساب");
                    dt.Columns.Add("تاريخ البحث");
                    dt.Columns.Add("دائن");
                    dt.Columns.Add("مدين");
                    dt.Columns.Add("عملة العملية");
                    dt.Columns.Add("العملية");
                    dt.Columns.Add("تاريخ العملية");
                    dt.Columns.Add("البيان");
                    dt.Columns.Add("");
                    dt.Rows.Add();
                  
                    dt.Rows[0][0] = comboBox1.Text;// comboBox1.SelectedText;
                    dt.Rows[0][1] = GetTypeAccount();
                    dt.Rows[0][2] = GetDateSearching();
                    int i = 0;

                    foreach (DataGridViewRow drg in dataGridView1.Rows)
                    {
                        DataRow dr = ((DataRowView)drg.DataBoundItem).Row;

                        dt.Rows[i][3] = string.Format("{0:##,##}", dr[0].ToString());
                        dt.Rows[i][4] = string.Format("{0:##,##}", dr[1].ToString());
                        dt.Rows[i][5] = dr[2].ToString();
                        dt.Rows[i][6] = dr[3].ToString();
                        dt.Rows[i][7] = dr[4].ToString();
                        dt.Rows[i][8] = dr[5].ToString();
                        dt.Rows.Add();
                        i++;
                    }
                    if (HostConnection == false)
                    {
                        dt.Rows[0][9] = Acn.GetUserNM(IDUSER);
                    }
                    else
                    {
                        dt.Rows[0][9] =AcnHost.GetUserNM(IDUSER);
                    }
                    /// print Report
                    frmReprt frmrepot = new frmReprt(dt, dt, 8);
                    frmrepot.ShowDialog();

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            this.Cursor = Cursors.Default;

        }

        private void btnRefrsh_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// // get type Account
        /// </summary>
        /// <returns></returns>
        string GetTypeAccount()
        {
            string re = "";
            if (radioButton1.Checked)
            {
               re= "الاجمالي";
            }
           else if(radioButton2.Checked)
            {
               re="التفصيلي";
            }
            return re;
        }
        /// <summary>
        ///  get DataSearching
        /// </summary>
        /// <returns></returns>
        string GetDateSearching()
        {
            string date = "";
            if (comboBox2.SelectedIndex == 4)
            {
                date = dateTimePicker1.Value.ToShortDateString() + " الى " + dateTimePicker2.Value.ToShortDateString();
                  
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                date = "الى " + dateTimePicker2.Value.ToShortDateString();
               
            }
            else if(comboBox2.SelectedIndex==2)
            {
                date = "خلال اسبوع";

            }
            else if(comboBox2.SelectedIndex==3)
            {
                date = "خلال شهر";

            }
            else if (comboBox2.SelectedIndex == 0)
            {
                date = "خلال يوم";

            }
            return date;

        }
        /// <summary>
        /// ضغط على تصدير اكسل 
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
        ///  //convert MemmoryToDB
        DataTable ConvertMemorytoDB(MemoryStream ms)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            ms.Seek(0, SeekOrigin.Begin);
            DataTable dt = (DataTable)formatter.Deserialize(ms);
            return dt;
        }

        private void عددالاسطرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dataGridView1.RowCount.ToString());
        }
    }
}
