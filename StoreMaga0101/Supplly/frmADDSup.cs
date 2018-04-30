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
namespace Supplly
{
    public partial class frmADDSup : Form
    {
        Supplly.SupplyRequset SuRe;
        int UserID;
        public frmADDSup()
        {
            InitializeComponent();
            try
            {
                UserID = 1;
                SuRe = new SupplyRequset(@".\s2008", "StoreManagement1",null,null);
               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    
        public frmADDSup(string ServerNm, string DBNm ,string UserSql,string PassSql, int UserId )
        {
            InitializeComponent();
            try
            {
                UserID = UserId;
                SuRe = new SupplyRequset(ServerNm, DBNm, UserSql, PassSql);
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox5.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox5.AutoCompleteSource = AutoCompleteSource.ListItems;
                getDate1();
                getdata22();
                changeLanguage();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void getDate1()
        {
            try
            {
                comboBox1.DisplayMember = "اسم الصنف";
                comboBox1.ValueMember = "رقم الصنف";
                comboBox1.DataSource = SuRe.GetAllCategoryAR();
                comboBox2.DisplayMember = "اسم النوع";
                comboBox2.ValueMember = "رقم النوع";
                comboBox2.DataSource = SuRe.GetAllTypeQuntity();
                comboBox3.DisplayMember = "اسم العملة";
                comboBox3.ValueMember = "رقم العملة";
                comboBox3.DataSource = SuRe.GetAllCurrency();

                dataGridView1.DataSource = SuRe.SearchINRequsetSupplyDate(DateTime.Now.Date, DateTime.Now);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message + "4"); }
        }


        void getdata22()
        {
            try
            {
                comboBox4.ValueMember = "رقم الحساب";
                comboBox4.DisplayMember = "اسم الحساب";
                comboBox4.DataSource = SuRe.GetALLAcountNm();

                comboBox5.ValueMember = "رقم الحساب";
                comboBox5.DisplayMember = "اسم الحساب";
                comboBox5.DataSource = SuRe.GetALLAcountNm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "5");
            }
        }
        void Refrsh1()
        {
            getDate1();
            getdata22();
            textBox1.Text = "";
            textBox2.Text = "";

            textBox3.Text = "";
            textBox4.Text = "";
           
            comboBox4.Enabled = true;
            comboBox5.Enabled = true;
        }
        void Refrsh12()
        {
            getDate1();

            textBox1.Text = "";
            textBox2.Text = "";

            //  textBox4.Text = "";

            comboBox4.Enabled = false;
            comboBox5.Enabled = false;
        }
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
        bool flagAddAgian = false;
        /// <summary>
        /// ///
        /// btn add suplly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddSup_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && (int)comboBox1.SelectedValue > 0 && (int)comboBox2.SelectedValue > 0 && (int)comboBox4.SelectedValue > 0 && (int)comboBox5.SelectedValue > 0 && (int)comboBox3.SelectedValue > 0)
                {
                    if ((MessageBox.Show("هل تريد ترحيل طلب التوريد واعتماده ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
                    {
                      try
                        {
                           
                          
                                int idcate = (int)comboBox1.SelectedValue;

                                int idtype = (int)comboBox2.SelectedValue;
                                int idCurrnt = (int)comboBox3.SelectedValue;
                                int qunt = Convert.ToInt32(textBox1.Text);
                                int price = Convert.ToInt32(textBox2.Text);
                                int mins = (int)comboBox4.SelectedValue; // من حساب 
                                int plus= (int)comboBox5.SelectedValue;//الى حساب
                                string name = textBox3.Text;
                                string dec = textBox4.Text;
                                int idAcount = SuRe.CheckAccountIsHere(idcate, idtype, price, idCurrnt);
                            /////////////////////
                            /////////////////
                            
                         
                                
                            

                            if (idAcount > 0) // في حالة الحساب موجود من قبل
                            {   //  تعديل الحساب بالكمية الجديدة
                                int oldQunt = SuRe.GetQuntityInAccount(idAcount);
                                int newQunt = oldQunt + qunt;
                                SuRe.UpdateQuntityAccount(idAcount, newQunt);


                            }
                            else //  في حالة الحساب جديد
                            {
                                SuRe.AddNewAccount(idcate, idtype, qunt, price, idCurrnt);// اضافة حساب جديد
                            }
                            /////////////////////////////////

                            /////////////////////////////////////////////////////////
                            // التاكد من ان الطلب يضاف كطلب جديد او اضافة الى طلب
                            int check = 0;
                            if (flagAddAgian == true)
                            {
                                check = SuRe.GetMaxCheckSupply();

                            }
                            else
                            {
                                check = SuRe.GetMaxCheckSupply();
                                check += 1;

                            }

                            // اضافة الى جدول التوريد
                            SuRe.AddNewRequsetSupply(idcate, idtype, qunt, price, idCurrnt, name, dec, DateTime.Now, UserID, check, mins, plus);//اضافة طلب جديد

                            /////////////////
                            ///////////
                            string.Format("{0:##,##}", (qunt * price).ToString());
                            ////////////////
                            ////////////// من حساب mins المدين
                            if (SuRe.CheckAccontTotal(mins, idCurrnt))// الحساب مضاف مسبقا
                            {
                                SuRe.UpdateAccountTotal(mins, (-1 * qunt * price), idCurrnt);
                            }
                            else //   في جدول الاجمالي اضافة حساب جديد
                            {
                                SuRe.AddNewAccountTotal(mins, (-1 * qunt * price), idCurrnt);
                            }
                            //// (اضافة الامر الى جدول تفاصيل الحساب )مدين(
                            string DitalisMis = "تم قيد عليكم مبلغ وقدره " + string.Format("{0:##,##}", (qunt * price).ToString())+" " +comboBox3.Text+"  "+ "مقابل امر توريد ب  " + qunt.ToString() + " " + comboBox1.Text + " " + comboBox2.Text+"  الى حساب  "+comboBox5.Text+"رقم الطلب "+ SuRe.GetMaxIdSupply();
                            SuRe.AddNewAccountDetalis(mins, (-1 * qunt * price), SuRe.GetMaxIdSupply(),0, DitalisMis, DateTime.Now, UserID, idCurrnt,0);//// اضافة الى جدول التفاصيل
                          /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                          //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                          ////// الى حساب plus الدائن
                          //////////////////
                          if(SuRe.CheckAccontTotal(plus,idCurrnt)) // الحساب مضاف مسبقا
                            {
                                SuRe.UpdateAccountTotal(plus, (qunt * price), idCurrnt);
                            }
                            else // اضافة حساب جديد في جدول الاجمالي
                            {
                                SuRe.AddNewAccountTotal(plus, (qunt * price), idCurrnt);
                            }
                            /////////////////////////////
                            //  اضافة الامر الى جدول التفاصيل (دائن)
                          string DitalisPlus = "تم قيد لكم مبلغ وقدره" + string.Format("{0:##,##}", (qunt * price).ToString()) + " " + comboBox3.Text + "  "+ "مقابل امر توريد ب " + qunt.ToString() + " " + comboBox1.Text + " " + comboBox2.Text + "  من حساب " + comboBox4.Text + "رقم الطلب " + SuRe.GetMaxIdSupply();
                         
                          SuRe.AddNewAccountDetalis(plus, ( qunt * price), SuRe.GetMaxIdSupply(), 0, DitalisPlus, DateTime.Now, UserID, idCurrnt,0);//// اضافة الى جدول التفاصيل

                            //////////////////////
                            ////////
                            if ((MessageBox.Show("هل تريد اضافة طلب  اخر", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
                            {
                                flagAddAgian = true;
                                Refrsh12();

                                // button2_Click(sender, e);
                            }
                            else
                            {
                                flagAddAgian = false;
                                if ((MessageBox.Show("هل تريد طباعة سند توريد؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
                                {
                                    try
                                    {
                                        Refrsh1();
                                       
                                        int IDRequstSupply = SuRe.GetMaxCheckSupply();

                                    
                                        DataTable dtSupp = new DataTable();
                                        DataTable dtExite = new DataTable();
                                        if (checkBox1.Checked)
                                        {
                                            dtExite = SuRe.printrequstOutExit1(IDRequstSupply, UserID, UserID);
                                        }
                                        else
                                        {
                                            dtExite = null;
                                        }
                                        dtSupp = SuRe.PrintRequstSupply(IDRequstSupply, UserID, UserID);
                                        frmReprt frmRp = new frmReprt(dtSupp, dtExite, 1);
                                        frmRp.ShowDialog();
                                    }
                                    catch (Exception ex)
                                   {
                                     MessageBox.Show(ex.Message + "1");

                                    }
                                }
                                else
                                {
                                    Refrsh1();
                                }

                            }
                       }
                        catch (Exception ex)
                      {
                         MessageBox.Show(ex.Message + "2");
                      }

                    }

                }
           }
           catch (Exception ex)
          {
              MessageBox.Show(ex.Message + "3");
           }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Length <= 0)
            {
                ((TextBox)sender).Focus();
            }
        }

        private void btnRefrsh_Click(object sender, EventArgs e)
        {
            Refrsh1();
            flagAddAgian = false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[11].Value.ToString());
                    string name = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();

                    DataTable dtSupp = new DataTable();
                    DataTable dtExite = new DataTable();
                    this.Cursor = Cursors.WaitCursor;
                   
                    if (checkBox1.Checked)
                    {
                        dtExite = SuRe.printrequstOutExit1(id, UserID, SuRe.GetIdUser(name));
                    }
                    else
                    {
                        dtExite = null;
                    }
                   
                    dtSupp = SuRe.PrintRequstSupply(id, UserID, SuRe.GetIdUser(name));
                    frmReprt frmRp = new frmReprt(dtSupp, dtExite, 1);
                    frmRp.ShowDialog();

                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //
        // event Combbox Leave when select categaory and link with cate
        private void comboBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                int IDACCOunt = SuRe.GetAccountLinkCate((int)comboBox1.SelectedValue);
                if (IDACCOunt > 0)
                    comboBox4.SelectedValue = IDACCOunt;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
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
    
    }
}

