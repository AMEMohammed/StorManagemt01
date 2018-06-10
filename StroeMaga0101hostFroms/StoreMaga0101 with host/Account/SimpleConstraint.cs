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
using System.ServiceModel;
namespace Account
{
    public partial class SimpleConstraint : Form
    {
        AccountNm Acn;
        ServiceReference1.IserviceClient AcnHost;
        int IDUSER;
        bool HostConnection = false;
     
        public SimpleConstraint()
        {
            InitializeComponent();
            try
            {
                Acn = new AccountNm(@".\s2008", "StoreManagement1", null, null);
                IDUSER = 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        public SimpleConstraint(string ServNm, string DbNm, string UesrSql, string PassSql, int UserId,bool hostCOnnection,string HostIp)
        {
            InitializeComponent();
            HostConnection = hostCOnnection;
            try
            {  if (HostConnection == false)
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

        //Event TxtBox key press
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

       
        //
        private void SimpleConstraint_Load(object sender, EventArgs e)
        { try
            {
                comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
                combAccount1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                combAccount1.AutoCompleteSource = AutoCompleteSource.ListItems;
                combAccount2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                combAccount2.AutoCompleteSource = AutoCompleteSource.ListItems;
                LoadData();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //
        private void LoadData()
        {
            if (HostConnection == false)
            {
                comboBox3.DataSource = Acn.GetAllCurrency();
                combAccount1.DataSource = Acn.GETALLAccountSub();
                combAccount2.DataSource = Acn.GETALLAccountSub();
            }
         else
            {
               
                comboBox3.DataSource =ConvertMemorytoDB(AcnHost.GetAllCurrency());
                combAccount1.DataSource =ConvertMemorytoDB(AcnHost.GETALLAccountSub());
                combAccount2.DataSource =ConvertMemorytoDB(AcnHost.GETALLAccountSub());
            }
            comboBox3.DisplayMember = "اسم العملة";
            comboBox3.ValueMember = "رقم العملة";
           
            combAccount1.DisplayMember = "اسم الحساب";
            combAccount1.ValueMember = "رقم الحساب";
         
            combAccount2.DisplayMember = "اسم الحساب";
            combAccount2.ValueMember = "رقم الحساب";
           //جلب القيود ليوم واحد
           if(HostConnection==false)
                dataGrideSimple.DataSource = Acn.GetAllSimpleConstraintOneDay(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
           else
                dataGrideSimple.DataSource =ConvertMemorytoDB(AcnHost.GetAllSimpleConstraintOneDay(DateTime.Now.Date, DateTime.Now.Date.AddDays(1)));


        }

        private void btnAddSup_Click(object sender, EventArgs e)
        {
            if(textBox2.Text.Length>0 && (int)comboBox3.SelectedValue>0 && (int) combAccount1.SelectedValue>0 && (int) combAccount2.SelectedValue>0)
            {
                try
                {
                    if (MessageBox.Show("هل تريد اضافة القيد", "قيد بسيط", MessageBoxButtons.YesNo) == DialogResult.Yes) 
                    {
                        int idCurrnt = (int)comboBox3.SelectedValue;
                        int Mony = Convert.ToInt32(textBox2.Text);
                        int IDdaenAccount = (int)combAccount2.SelectedValue;
                        int IDMadenAccount = (int)combAccount1.SelectedValue;



                        ///add new tblSimpleConstraint
                        ///// اضافة القيد الى جدول القيود
                        // connection local
                        #region
                        if (HostConnection == false)// connection Local
                        {
                            Acn.AddSimpleConstraint(IDdaenAccount, IDMadenAccount, Mony, idCurrnt, IDUSER, DateTime.Now, txtNote.Text);
                            ////////////// من حساب mins المدين
                            if (Acn.CheckAccontTotal(IDMadenAccount, idCurrnt))// الحساب مضاف مسبقا
                            {
                                Acn.UpdateAccountTotal(IDMadenAccount, (-1 * Mony), idCurrnt);
                            }
                            else //   في جدول الاجمالي اضافة حساب جديد
                            {
                                Acn.AddNewAccountTotal(IDMadenAccount, (-1 * Mony), idCurrnt);
                            }
                            //// (اضافة الامر الى جدول تفاصيل الحساب )مدين(

                            string DitalisMis = "تم قيد عليكم مبلغ وقدره " + string.Format("{0:##,##}", (Mony).ToString()) + " " + comboBox3.Text + "  " + "مقابل قيد بسيط   " + "  الى حساب  " + combAccount2.Text + " رقم القيد" + Acn.GetMaxIDSimpleConstraint();
                            Acn.AddNewAccountDetalis(IDMadenAccount, (-1 * Mony), 0, 0, DitalisMis, DateTime.Now, IDUSER, idCurrnt, Acn.GetMaxIDSimpleConstraint());
                            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            ///      الحساب الدائن                                                                                                                     
                            if (Acn.CheckAccontTotal(IDdaenAccount, idCurrnt)) // الحساب مضاف مسبقا
                            {
                                Acn.UpdateAccountTotal(IDdaenAccount, Mony, idCurrnt);
                            }
                            else // اضافة حساب جديد في جدول الاجمالي
                            {
                                Acn.AddNewAccountTotal(IDdaenAccount, Mony, idCurrnt);
                            }
                            //  اضافة الامر الى جدول التفاصيل (دائن)
                            string DitalisPlus = "تم قيد لكم مبلغ وقدره" + string.Format("{0:##,##}", (Mony).ToString()) + " " + comboBox3.Text + "  " + "مقابل قيد بسيط  " + "  من حساب " + combAccount1.Text + " رقم القيد  " + Acn.GetMaxIDSimpleConstraint();
                            Acn.AddNewAccountDetalis(IDdaenAccount, (Mony), 0, 0, DitalisPlus, DateTime.Now, IDUSER, idCurrnt, Acn.GetMaxIDSimpleConstraint());//// اضافة الى جدول التفاصيل
                            dataGrideSimple.DataSource = Acn.GetAllSimpleConstraintOneDay(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
                        }
                        #endregion
                        ///// connection host
                        #region
                        else
                        {
                            AcnHost.AddSimpleConstraint(IDdaenAccount, IDMadenAccount, Mony, idCurrnt, IDUSER, DateTime.Now, txtNote.Text);
                            ////////////// من حساب mins المدين
                            if (AcnHost.CheckAccontTotal(IDMadenAccount, idCurrnt))// الحساب مضاف مسبقا
                            {
                                AcnHost.UpdateAccountTotal(IDMadenAccount, (-1 * Mony), idCurrnt);
                            }
                            else //   في جدول الاجمالي اضافة حساب جديد
                            {
                                AcnHost.AddNewAccountTotal(IDMadenAccount, (-1 * Mony), idCurrnt);
                            }
                            //// (اضافة الامر الى جدول تفاصيل الحساب )مدين(


                            string DitalisMis = "تم قيد عليكم مبلغ وقدره " + string.Format("{0:##,##}", (Mony).ToString()) + " " + comboBox3.Text + "  " + "مقابل قيد بسيط   " + "  الى حساب  " + combAccount2.Text + " رقم القيد" + AcnHost.GetMaxIDSimpleConstraint();
                            AcnHost.AddNewAccountDetalis(IDMadenAccount, (-1 * Mony), 0, 0, DitalisMis, DateTime.Now, IDUSER, idCurrnt, AcnHost.GetMaxIDSimpleConstraint());
                            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            ///      الحساب الدائن                                                                                                                     
                            if (AcnHost.CheckAccontTotal(IDdaenAccount, idCurrnt)) // الحساب مضاف مسبقا
                            {
                                AcnHost.UpdateAccountTotal(IDdaenAccount, Mony, idCurrnt);
                            }
                            else // اضافة حساب جديد في جدول الاجمالي
                            {
                                AcnHost.AddNewAccountTotal(IDdaenAccount, Mony, idCurrnt);
                            }
                            string DitalisPlus = "تم قيد لكم مبلغ وقدره" + string.Format("{0:##,##}", (Mony).ToString()) + " " + comboBox3.Text + "  " + "مقابل قيد بسيط  " + "  من حساب " + combAccount1.Text + " رقم القيد  " + AcnHost.GetMaxIDSimpleConstraint();

                            AcnHost.AddNewAccountDetalis(IDdaenAccount, (Mony), 0, 0, DitalisPlus, DateTime.Now, IDUSER, idCurrnt, AcnHost.GetMaxIDSimpleConstraint());//// اضافة الى جدول التفاصيل
                            dataGrideSimple.DataSource = ConvertMemorytoDB(AcnHost.GetAllSimpleConstraintOneDay(DateTime.Now.Date, DateTime.Now.Date.AddDays(1)));
                        }
                        #endregion

                        LoadData();
                        textBox2.Text = "";
                        txtNote.Text = "";
                    }
                }
                catch (Exception ex)
                {
                   MessageBox.Show(ex.Message);
                }
            }
            else

            {
                MessageBox.Show("يجب تعبئة جميع الحقول");
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                //جلب القيود ليوم واحد
                if (HostConnection == false)
                {
                    dataGrideSimple.DataSource = Acn.GetAllSimpleConstraintOneDay(dateTimePicker1.Value.Date, dateTimePicker1.Value.Date.AddDays(1));
                }
                else
                {
                    dataGrideSimple.DataSource =ConvertMemorytoDB(AcnHost.GetAllSimpleConstraintOneDay(dateTimePicker1.Value.Date, dateTimePicker1.Value.Date.AddDays(1)));
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
            if (dataGrideSimple.Rows.Count > 0)
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
                    for (int i1 = 1; i1 < dataGrideSimple.Columns.Count + 1; i1++)
                    {
                        xlWorkSheet.Cells[1, i1] = dataGrideSimple.Columns[i1 - 1].HeaderText;
                    }
                    int i = 0;
                    int j = 0;

                    for (i = 0; i <= dataGrideSimple.RowCount - 1; i++)
                    {
                        for (j = 0; j <= dataGrideSimple.ColumnCount - 1; j++)
                        {
                            DataGridViewCell cell = dataGrideSimple[j, i];
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


        ////
        //// 

    }
}
