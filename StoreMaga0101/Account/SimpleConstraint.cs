using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Account
{
    public partial class SimpleConstraint : Form
    {
        AccountNm Acn;
        int IDUSER;
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
        public SimpleConstraint(string ServNm, string DbNm, string UesrSql, string PassSql, int UserId)
        {
            InitializeComponent();
            try
            {
                Acn = new AccountNm(ServNm, DbNm, UesrSql, PassSql);
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
            comboBox3.DataSource = Acn.GetAllCurrency();
            comboBox3.DisplayMember = "اسم العملة";
            comboBox3.ValueMember = "رقم العملة";
            combAccount1.DataSource = Acn.GETALLAccountSub();
            combAccount1.DisplayMember = "اسم الحساب";
            combAccount1.ValueMember = "رقم الحساب";
            combAccount2.DataSource = Acn.GETALLAccountSub();
            combAccount2.DisplayMember = "اسم الحساب";
            combAccount2.ValueMember = "رقم الحساب";
           //جلب القيود ليوم واحد
            dataGrideSimple.DataSource = Acn.GetAllSimpleConstraintOneDay(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
            

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
                        Acn.AddSimpleConstraint(IDdaenAccount, IDMadenAccount, Mony, IDUSER, DateTime.Now, txtNote.Text);
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

                        string DitalisMis = "تم قيد عليكم مبلغ وقدره " + string.Format("{0:##,##}", (Mony).ToString()) + " " + comboBox3.Text + "  " + "مقابل قيد بسيط ب  " + "  الى حساب  " + combAccount2.Text + " رقم القيد" + Acn.GetMaxIDSimpleConstraint();
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
                        /////////////////////////////
                        //  اضافة الامر الى جدول التفاصيل (دائن)
                        string DitalisPlus = "تم قيد لكم مبلغ وقدره" + string.Format("{0:##,##}", (Mony).ToString()) + " " + comboBox3.Text + "  " + "مقابل قيد بسيط ب " + "  من حساب " + combAccount1.Text + " رقم القيد  " + Acn.GetMaxIDSimpleConstraint();

                        Acn.AddNewAccountDetalis(IDdaenAccount, (Mony), 0, 0, DitalisPlus, DateTime.Now, IDUSER, idCurrnt, Acn.GetMaxIDSimpleConstraint());//// اضافة الى جدول التفاصيل
                        dataGrideSimple.DataSource = Acn.GetAllSimpleConstraintOneDay(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
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
                dataGrideSimple.DataSource = Acn.GetAllSimpleConstraintOneDay(dateTimePicker1.Value.Date, dateTimePicker1.Value.Date.AddDays(1));
               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

    
        }
    }
}
