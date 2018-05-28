using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.ServiceModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Users
{
    public partial class frmAddUser : Form
    {
        int UserID = 0;
        int idd = 0;
        UsersSQl Us;
        ServiceReference1.IserviceClient UsHost;
        public frmAddUser()
        {
            InitializeComponent();
           try
            {
                UserID = 1;
                MessageBox.Show(Properties.Settings.Default.ConnectionHost.ToString());
                if (!ConServer.ConnectionWithHost)
                {
                    // connection loacl
                    Us = new UsersSQl(@".\s2008", "StoreManagement1", null, null);
                }
             else
                {  
                    // connection host
                   
                    UsHost = new ServiceReference1.IserviceClient();   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public frmAddUser(string SerNm, string DbNm, string UserSql, string PassSql, int UserI)
        {
            InitializeComponent();
            try
            {
                UserID = UserI;
                if (!ConServer.ConnectionWithHost)
                {    // connection loacl
                    Us = new UsersSQl(SerNm, DbNm, UserSql, PassSql);
                }
                else
                {  // connection host
                    UsHost = new ServiceReference1.IserviceClient();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmAddUser_Load(object sender, EventArgs e)
        {
            LoadTree();
            LoadDate();
        }
        /// <summary>
        ///  fir loading TreeView
        /// </summary>
        void LoadTree()
        {
            comboBox1.SelectedIndex = 0;
            treeView1.CheckBoxes = true;
            TreeNode t1 = new TreeNode();
            t1.Text = "توريد";
            TreeNode t11 = new TreeNode();
            t11.Text = "اضافة";
            TreeNode t12 = new TreeNode();
            t12.Text = "تعديل";
            t1.Nodes.Add(t11);
            t1.Nodes.Add(t12);
            TreeNode t2 = new TreeNode();
            t2.Text = "صرف";
            TreeNode t21 = new TreeNode();
            t21.Text = "اضافة";
            TreeNode t22 = new TreeNode();
            t22.Text = "تعديل";
            t2.Nodes.Add(t21);
            t2.Nodes.Add(t22);
            TreeNode t3 = new TreeNode();
            t3.Text = "طباعة التقارير";
            TreeNode t31 = new TreeNode();
            t31.Text = "تقارير اوامر التوريد";
            TreeNode t32 = new TreeNode();
            t32.Text = "تقارير اوامرالصرف";
            TreeNode t33 = new TreeNode();
            t33.Text = "تقارير المخزون";
            TreeNode t34 = new TreeNode();
            t34.Text = "تقارير تعديل الوارد";
            TreeNode t35 = new TreeNode();
            t35.Text = "تقارير تعديل الصرف";
            t3.Nodes.Add(t31);
            t3.Nodes.Add(t32);
            t3.Nodes.Add(t33);
            t3.Nodes.Add(t34);
            t3.Nodes.Add(t35);
            TreeNode t4 = new TreeNode();
            t4.Text = "المستخدمين";
            TreeNode t41 = new TreeNode();
            t41.Text = "اضافة مستخدمين";
            t4.Nodes.Add(t41);
            TreeNode t5 = new TreeNode();
            t5.Text = "تهيئة النظام";
            TreeNode t51 = new TreeNode();
            t51.Text = "تهيئة الاصناف";
            TreeNode t52 = new TreeNode();
            t52.Text = "تهيئة الانواع";
            TreeNode t53 = new TreeNode();
            t53.Text = "تهيئة الحسابات";
            TreeNode t54 = new TreeNode();
            t54.Text = "تهيئة العملات";
            TreeNode t55 = new TreeNode();
            t55.Text = "تهيئة الجهات";
            t5.Nodes.Add(t51);
            t5.Nodes.Add(t52);
            t5.Nodes.Add(t53);
            t5.Nodes.Add(t54);
            t5.Nodes.Add(t55);

            treeView1.Nodes.Add(t1);
            treeView1.Nodes.Add(t2);
            treeView1.Nodes.Add(t3);
            treeView1.Nodes.Add(t4);
            treeView1.Nodes.Add(t5);
        }
        void checkNodeChald(TreeNode Parent)
        {

            foreach (TreeNode chald in Parent.Nodes)
            {

                chald.Checked = Parent.Checked;
            }
        }


        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {

            checkNodeChald(e.Node);
        }
        /// <summary>
        /// ///////////////
        /// </summary>
        /// <returns></returns>
        bool[] GetBooling()
        {
            bool[] chek = new bool[15];
            int i = 0;
            foreach (TreeNode tp in treeView1.Nodes)
            {
                // chek[i++] = tp.Checked;
                foreach (TreeNode tc in tp.Nodes)
                {

                    chek[i++] = tc.Checked;

                }
            }
            return chek;
        }
        /// <summary>
        /// ///////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefrsh_Click(object sender, EventArgs e)
        {
            bool[] GetBool = GetBooling();
            bool active = true;
            if (comboBox1.SelectedIndex == 0)
            {
                active = true;
            }
            else
            {
                active = false;
            }
            if (textBox3.Text.Length > 0 && textBox4.Text.Length > 0 && textBox1.Text.Length > 0)
            {
                if (!ConServer.ConnectionWithHost)
                {
                    Us.UpdUsers(idd, textBox1.Text, textBox3.Text, textBox4.Text, GetBool[0], GetBool[1], GetBool[2], GetBool[3], GetBool[4], GetBool[5], GetBool[6], GetBool[7], GetBool[8], GetBool[9], active, GetBool[10], GetBool[11], GetBool[12], GetBool[13], GetBool[14]);
                }
                else
                {
                    UsHost.UpdUsers(idd, textBox1.Text, textBox3.Text, textBox4.Text, GetBool[0], GetBool[1], GetBool[2], GetBool[3], GetBool[4], GetBool[5], GetBool[6], GetBool[7], GetBool[8], GetBool[9], active, GetBool[10], GetBool[11], GetBool[12], GetBool[13], GetBool[14]);
                }
                LoadDate();
            }

            
        }
        void LoadDate()
        { if (!ConServer.ConnectionWithHost)
            {
                dataGridView1.DataSource = Us.GetAllUser();
            }
         else
            {
                 dataGridView1.DataSource = ConvertMemorytoDB(UsHost.GetAllUser());
              
            }
        }

        private void btnAddSup_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("هل تريد المتابعة", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bool[] GetBool = GetBooling();
                    bool active = true;
                    if (comboBox1.SelectedIndex == 0)
                    {
                        active = true;
                    }
                    else
                    {
                        active = false;
                    }
                    if (textBox3.Text.Length > 0 && textBox1.Text.Length > 0)
                    {
                        if (!ConServer.ConnectionWithHost)
                        {
                            Us.AddNewUser(textBox1.Text, textBox3.Text, ChangePass("1"), GetBool[0], GetBool[1], GetBool[2], GetBool[3], GetBool[4], GetBool[5], GetBool[6], GetBool[7], GetBool[8], GetBool[9], active, GetBool[10], GetBool[11], GetBool[12], GetBool[13], GetBool[14], UserID);
                        }
                        else
                        {
                            UsHost.AddNewUser(textBox1.Text, textBox3.Text, ChangePass("1"), GetBool[0], GetBool[1], GetBool[2], GetBool[3], GetBool[4], GetBool[5], GetBool[6], GetBool[7], GetBool[8], GetBool[9], active, GetBool[10], GetBool[11], GetBool[12], GetBool[13], GetBool[14], UserID);
                        }
                        LoadDate();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    textBox4.Enabled = false;
                    UNchakTreeView();
                    idd = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                    bool[] chek1 = new bool[16];
                    chek1[0] = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                    chek1[1] = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
                    chek1[2] = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[6].Value.ToString());
                    chek1[3] = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[7].Value.ToString());
                    chek1[4] = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[8].Value.ToString());
                    chek1[5] = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[9].Value.ToString());
                    chek1[6] = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[10].Value.ToString());
                    chek1[7] = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[11].Value.ToString());
                    chek1[8] = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[12].Value.ToString());
                    chek1[9] = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[13].Value.ToString());
                    chek1[10] = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[14].Value.ToString());
                    chek1[11] = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[15].Value.ToString());
                    chek1[12] = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[16].Value.ToString());
                    chek1[13] = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[17].Value.ToString());
                    chek1[14] = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[18].Value.ToString());
                    chek1[15] = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[19].Value.ToString());
                    if (chek1[0] == true || chek1[1] == true)
                    {
                        treeView1.Nodes[0].Checked = true;
                        treeView1.Nodes[0].Nodes[0].Checked = chek1[0];
                        treeView1.Nodes[0].Nodes[1].Checked = chek1[1];
                    }
                    if (chek1[2] == true || chek1[3] == true)
                    {
                        treeView1.Nodes[1].Checked = true;
                        treeView1.Nodes[1].Nodes[0].Checked = chek1[2];
                        treeView1.Nodes[1].Nodes[1].Checked = chek1[3];
                    }
                    if (chek1[4] == true || chek1[5] == true || chek1[6] == true || chek1[7] == true || chek1[8] == true)
                    {
                        treeView1.Nodes[2].Checked = true;
                        treeView1.Nodes[2].Nodes[0].Checked = chek1[4];
                        treeView1.Nodes[2].Nodes[1].Checked = chek1[5];
                        treeView1.Nodes[2].Nodes[2].Checked = chek1[6];
                        treeView1.Nodes[2].Nodes[3].Checked = chek1[7];
                        treeView1.Nodes[2].Nodes[4].Checked = chek1[8];
                    }

                    if (chek1[9] == true)
                    {
                        treeView1.Nodes[3].Checked = true;
                        treeView1.Nodes[3].Nodes[0].Checked = chek1[9];
                        treeView1.Refresh();
                    }
                    if (chek1[10] == true)
                    {

                        comboBox1.SelectedIndex = 0;
                    }
                    else
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (chek1[11] == true || chek1[12] == true || chek1[13] == true || chek1[14] == true || chek1[15] == true)
                    {
                        treeView1.Nodes[4].Checked = true;
                        treeView1.Nodes[4].Nodes[0].Checked = chek1[11];
                        treeView1.Nodes[4].Nodes[1].Checked = chek1[12];
                        treeView1.Nodes[4].Nodes[2].Checked = chek1[13];
                        treeView1.Nodes[4].Nodes[3].Checked = chek1[14];
                        treeView1.Nodes[4].Nodes[4].Checked = chek1[15];
                    }
                }


            }
            //  catch (Exception ex)
            {
                ///    MessageBox.Show(ex.Message);
            }
        }


        void UNchakTreeView()
        {
            foreach (TreeNode tp in treeView1.Nodes)
            {
                tp.Checked = false;
                foreach (TreeNode tc in tp.Nodes)
                {

                    tc.Checked = false;

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// تشفير كلمة المرور
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        string ChangePass(string pass)
        {
            byte[] tmpSource = new UTF8Encoding().GetBytes(pass);
            return Convert.ToBase64String(tmpSource);
        }
        //فك تشفير كلمة المورر
        //
        string GetPassNormal(string passDecode)
        {
            byte[] tmpData = Convert.FromBase64String(passDecode);
            return (new UTF8Encoding().GetString(tmpData));
        }

        private void button1_Click(object sender, EventArgs e)
        {
           try
            {  if (!ConServer.ConnectionWithHost)
                {
                    Us.UpatePassword(idd, ChangePass("1"));
                }
                else
                {
                    UsHost.UpatePassword(idd, ChangePass("1"));
                }
                LoadDate();
                MessageBox.Show("تم اعادة تعين كلمة المرور .. كلمة المرور هي 1 .  يرجى تغيرها ", "رسالة", MessageBoxButtons.OK);

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
