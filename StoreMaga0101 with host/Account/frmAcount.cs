using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
namespace Account
{
    public partial class frmAcount : Form
    {
        DataTable dt = new DataTable();
        AccountNm Acn;
        ServiceReference1.IserviceClient AcnHost;
        int IDUSER;
        int IdAcount;
        int CodeAddAcount;
        string TpyeAcount;
        int IDParentAdd;
        bool ADDing = false;
        bool chak1;
        bool HostConnection =false;

        public frmAcount()
        {
            InitializeComponent();
            try
            {
                
                Acn = new AccountNm(@".\s2008", "StoreManagement1", null, null);
                IDUSER = 1;
                dt = Acn.GetAllAccount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        ///
        public frmAcount(string ServNm, string DbNm, string UesrSql, string PassSql, int UserId,bool ConectionHost)
        {
            InitializeComponent();
            try
            {
                IDUSER = UserId;
                HostConnection = ConectionHost;
                if (HostConnection ==false)
                {
                    Acn = new AccountNm(ServNm, DbNm, UesrSql, PassSql);
                    dt = Acn.GetAllAccount();
                }
                else
                {
                    AcnHost = new ServiceReference1.IserviceClient();
                    dt =ConvertMemorytoDB (AcnHost.GetAllAccount());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }



        ////////////////////
        ///////
        ////////loding TreeView
        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {

            TreeNode childNode;
         

            foreach (DataRow dr in dt.Select("[IDParentAcount]=" + parentId))// جلب السطر برقم معين
            { 
                TreeNode t = new TreeNode();
                t.Text = dr["IDCode"].ToString() +"-"+dr["AcountNm"].ToString();
                t.Name = dr["IDCode"].ToString();

                t.Tag = dr["IDAcountNm"].ToString(); //ترقيم العقدة برقم  السطر في الجدول
                 
                if (parentNode == null)
                {
                    treeView1.Nodes.Add(t);
                    childNode = t;
                }
                else
                {
                    parentNode.Nodes.Add(t);
                    childNode = t;
                }
                PopulateTreeView(Convert.ToInt32(dr["IDCode"].ToString()), childNode);
              
            }
        }

        private void frmAcount_Load(object sender, EventArgs e)
        { try
            {
                PopulateTreeView(0, null);
                if (HostConnection == false)
                {
                    dataGridView1.DataSource = Acn.GetAllAcountnAr();
                }
                else
                {
                    dataGridView1.DataSource =ConvertMemorytoDB( AcnHost.GetAllAcountnAr());
                }
                
                
                dataGridView1.Columns[8].Visible = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void اختيارالحسابToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  MessageBox.Show(treeView1.SelectedNode.Text);
            //if (treeView1.SelectedNode.Level >= 0)
            {
                try
                {
                    ShowTreeNode(treeView1.SelectedNode);
                }
                catch
                {
                    // MessageBox.Show("يرجى تحديد الحساب  ");

                }
            }
        }
        /// <summary>
        /// /////////
        /// </summary>
        /// <param name="t"></param>
        void ShowTreeNode(TreeNode t)
        { 
            TreeNode tParent = new TreeNode();
          
            tParent = t.Parent;
             comboBox1.Text = tParent.Text;
             textBox1.Text = t.Text;
             textBox4.Text = t.Name;
          
           
            IdAcount = Convert.ToInt32(t.Tag.ToString());
            idcode = Convert.ToInt32(t.Name);
            if (HostConnection == false)
            {
                comboBox2.Text = Acn.TypeAccount(IdAcount);//get Type Account;
                checkBox1.Checked = !Acn.GetCheckAccount(IdAcount);

            }
            else
            {
                comboBox2.Text = AcnHost.TypeAccount(IdAcount);//get Type Account;
                checkBox1.Checked = !AcnHost.GetCheckAccount(IdAcount);
            }




        }
        /// <summary>
        /// اضافة حساب
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddSup_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox4.Text.Length > 0 && comboBox2.Text.Length > 0)
            { if (MessageBox.Show("هل تريد اضافة الحساب", "تاكيد", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        int IdType;
                        if (checkBox1.Checked)
                        {
                            IdType = 0;

                        }
                        else
                        {
                            IdType = 1;
                        }
                        if (HostConnection == false)
                        {
                            if (!Acn.GetCheckAccountHere(Convert.ToInt32(textBox4.Text)))
                            {
                                Acn.AddNewAcountNm(textBox1.Text, Convert.ToInt32(textBox4.Text), CodeAddAcount, comboBox2.Text, IdType, DateTime.Now, IDUSER);
                                RefrshTreeNode();
                                textBox1.Text = "";
                            }
                            else
                            {
                                MessageBox.Show("رقم الحساب مضاف مسبقا يرجى التاكد من رقم الحساب");
                            }
                        }
                        else // connection host
                        {
                            if (!AcnHost.GetCheckAccountHere(Convert.ToInt32(textBox4.Text)))
                            {
                                AcnHost.AddNewAcountNm(textBox1.Text, Convert.ToInt32(textBox4.Text), CodeAddAcount, comboBox2.Text, IdType, DateTime.Now, IDUSER);
                                RefrshTreeNode();
                                textBox1.Text = "";
                            }
                            else
                            {
                                MessageBox.Show("رقم الحساب مضاف مسبقا يرجى التاكد من رقم الحساب");
                            }

                        }
                    }

                   catch (Exception ex)
                    {
                       MessageBox.Show(ex.Message);
                    }
                }
                }
        }

        /// <summary>
        /// ///// updat Buottn
        /// </summary> تعديل حساب
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefrsh_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length>0 && IdAcount>=1)
            { 
                try
                {
                   
                    string AccounNm = new String(textBox1.Text.Where(c => c != '-' && (c < '0' || c > '9')).ToArray());
                    int chackAccount;
                    if (HostConnection == false)
                    {
                        Acn.UpdateAccountNm(IdAcount, AccounNm, !checkBox1.Checked);
                    }
                    else
                    {
                        AcnHost.UpdateAccountNm(IdAcount, AccounNm, !checkBox1.Checked);
                    }
                    RefrshTreeNode();
                    ADDing = false;
                    textBox1.Text = "";
                    comboBox1.Text = "";
                    comboBox2.Text = "";
                    textBox4.Text = "";
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// ////////تحديث البيانات
        /// </summary>
        void RefrshTreeNode()
        {
            try
            {
                if (HostConnection == false)
                {
                    dt = Acn.GetAllAccount();
                }
                else
                {
                    dt = ConvertMemorytoDB(AcnHost.GetAllAccount());
                }
                treeView1.Nodes.Clear();
                PopulateTreeView(0, null);
                IdAcount = -1;
                if (HostConnection == false)
                {
                    dataGridView1.DataSource = Acn.GetAllAcountnAr();
                }
                else
                {
                 
                    dataGridView1.DataSource =ConvertMemorytoDB( AcnHost.GetAllAcountnAr());

                }

                dataGridView1.Columns[8].Visible = false;
            }
            catch
            {

            }
        }
        /// <summary>
        /// ////اضافة حساب  رئيسي
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void اضافةحسابفرعيToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeView1.SelectedNode.Level >= 0)
                {
                    try
                    {
                        TreeNode t = new TreeNode();

                        t = treeView1.SelectedNode;
                        IdAcount = Convert.ToInt32(t.Tag.ToString());
                        if (HostConnection == false)
                        {
                            if (Acn.TypeAccount(IdAcount).Equals("رئيسي"))
                            {
                                textBox1.Text = "";
                                textBox1.Focus();
                                comboBox1.Text = t.Text;

                                CodeAddAcount = Convert.ToInt32(t.Name);// رقم حساب الاب

                                int idcode = Acn.GetMaxCode(CodeAddAcount);
                                idcode += 1;
                                textBox4.Text = idcode.ToString();
                                comboBox2.Text = "رئيسي";
                                ADDing = true;
                            }
                            else
                            {
                                MessageBox.Show("لايمكن اضافة حساب الى الحساب الفرعي");
                            }
                        }
                        else
                        {
                            if (AcnHost.TypeAccount(IdAcount).Equals("رئيسي"))
                            {
                                textBox1.Text = "";
                                textBox1.Focus();
                                comboBox1.Text = t.Text;

                                CodeAddAcount = Convert.ToInt32(t.Name);// رقم حساب الاب

                                int idcode = AcnHost.GetMaxCode(CodeAddAcount);
                                idcode += 1;
                                textBox4.Text = idcode.ToString();
                                comboBox2.Text = "رئيسي";
                                ADDing = true;
                            }
                            else
                            {
                                MessageBox.Show("لايمكن اضافة حساب الى الحساب الفرعي");
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// /////اضافة حساب فرعي
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void اضافةحسابفرعيToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeView1.SelectedNode.Level >= 0)
            {
                
                    TreeNode t = new TreeNode();

                    t = treeView1.SelectedNode;
                    IdAcount = Convert.ToInt32(t.Tag.ToString());
                    if (HostConnection == false)
                    {
                        if (Acn.TypeAccount(IdAcount).Equals("رئيسي"))
                        {
                            textBox1.Text = "";
                            textBox1.Focus();
                            comboBox1.Text = t.Text;

                            CodeAddAcount = Convert.ToInt32(t.Name);// رقم حساب الاب

                            int idcode = Acn.GetMaxCode(CodeAddAcount);
                            idcode += 1;
                            textBox4.Text = idcode.ToString();
                            comboBox2.Text = "فرعي";
                            ADDing = true;

                        }
                        else
                        {
                            MessageBox.Show("لايمكن اضافة حساب الى الحساب الفرعي");
                        }
                    }
                    else
                    {
                        if (AcnHost.TypeAccount(IdAcount).Equals("رئيسي"))
                        {
                            textBox1.Text = "";
                            textBox1.Focus();
                            comboBox1.Text = t.Text;

                            CodeAddAcount = Convert.ToInt32(t.Name);// رقم حساب الاب

                            int idcode = AcnHost.GetMaxCode(CodeAddAcount);
                            idcode += 1;
                            textBox4.Text = idcode.ToString();
                            comboBox2.Text = "فرعي";
                            ADDing = true;

                        }
                        else
                        {
                            MessageBox.Show("لايمكن اضافة حساب الى الحساب الفرعي");
                        }
                    }
                
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
        int IDParnt1;
        int IDCod1;

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefrshTreeNode();
        }
        /// <summary>
        /// بحث
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (HostConnection == false)
                {
                    dataGridView1.DataSource = Acn.SearchAcount(txtSaerch.Text);
                }
                else
                {
                    dataGridView1.DataSource = ConvertMemorytoDB( AcnHost.SearchAcount(txtSaerch.Text));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            try
            {
                ShowTreeNode(treeView1.SelectedNode);
            }
            catch
            {
                // MessageBox.Show("يرجى تحديد الحساب  ");

            }
        }
        public int idcode;
        private void dataGridView1_CellEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    IDParnt1 = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                    IDCod1 = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    comboBox1.Text = IDParnt1 + "-" + dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                    textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    comboBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    textBox4.Text = IDCod1.ToString();
                    bool chek = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
                    checkBox1.Checked = !chek;
                    IdAcount = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[8].Value.ToString());
                    idcode = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                if (idcode >= 1)
                {
                    if (MessageBox.Show("هل تريد حذف الحساب المحدد", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (HostConnection == false)
                        {
                            if (Acn.CheckAccountinDetlis(idcode) || Acn.CheckAccounthaschalid(idcode))
                            {
                                MessageBox.Show("لا يمكن حذف السجل لانه مرتبط بسجلات اخرى", "رسالة", MessageBoxButtons.OK);
                            }
                            else
                            {

                                Acn.DelteAccount2(idcode);

                                RefrshTreeNode();

                                textBox1.Text = "";
                                comboBox1.Text = "";
                                comboBox2.Text = "";
                                textBox4.Text = "";
                            }
                        }
                        else
                        {
                            if (AcnHost.CheckAccountinDetlis(idcode) || AcnHost.CheckAccounthaschalid(idcode))
                            {
                                MessageBox.Show("لا يمكن حذف السجل لانه مرتبط بسجلات اخرى", "رسالة", MessageBoxButtons.OK);
                            }
                            else
                            {

                                AcnHost.DelteAccount2(idcode);

                                RefrshTreeNode();

                                textBox1.Text = "";
                                comboBox1.Text = "";
                                comboBox2.Text = "";
                                textBox4.Text = "";
                            }
                        }

                    }

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        ///  //convert MemmoryToDB
        DataTable ConvertMemorytoDB(MemoryStream ms)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            ms.Seek(0, SeekOrigin.Begin);
            DataTable dt = (DataTable)formatter.Deserialize(ms);
            return dt;
        }
    }
    }

