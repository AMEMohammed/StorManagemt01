using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Out_
{
    public partial class frmUpdateOut2 : Form
    {
        int UserID;
        OutFunction OutFun;
        public frmUpdateOut2()
        {
            InitializeComponent();
            UserID = 1;
            try
            {
                OutFun = new OutFunction(@".\s2008", "StoreManagement1",null,null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      
        public frmUpdateOut2(string ServerNm, string DbNm,string UserSql,string PassSql ,int UserId)
        {
            InitializeComponent();
            UserID = UserId;
            try
            {
                OutFun = new OutFunction(ServerNm, DbNm, UserSql, PassSql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int IdOut;
        private void frmUpdateOut2_Load(object sender, EventArgs e)
        {
            IdOut = (int)this.Tag;
      
         
            OutFun.GetRequstOutSngle(IdOut);
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
            comboBox6.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox6.AutoCompleteSource = AutoCompleteSource.ListItems;
            getDate1();
            ConfDate();
            ////////////
            changeLanguage();
            MessageBoxManager.Yes = "نعم";
            MessageBoxManager.No = "الغاء";
            MessageBoxManager.Register();

        }
        DataTable dt = new DataTable();
        ///////////////////
        void getDate1()
        {
            try
            {
                comboBox1.DisplayMember = "اسم الصنف";
                comboBox1.ValueMember = "رقم الصنف";
                comboBox1.DataSource = OutFun.GetCatagoryInAccount();
                comboBox2.DisplayMember = "اسم النوع";
                comboBox2.ValueMember = "رقم النوع";
                comboBox2.DataSource = OutFun.GetTypeInAccount((int)comboBox1.SelectedValue);
                comboBox3.ValueMember = "رقم العملة";
                comboBox3.DisplayMember = "اسم العملة";
                comboBox3.DataSource = OutFun.GetCurrencyINAccount((int)comboBox1.SelectedValue, (int)comboBox2.SelectedValue);
                comboBox4.ValueMember = "رقم الجهة";
                comboBox4.DisplayMember = "اسم الجهة";
                comboBox4.DataSource = OutFun.GetAllPlace();
                comboBox5.ValueMember = "الرقم";
                comboBox5.DisplayMember = "نوع الحساب";
                comboBox5.DataSource = OutFun.GetAllDebit();
                comboBox6.ValueMember = "الرقم";
                comboBox6.DisplayMember = "نوع الحساب";
                comboBox6.DataSource = OutFun.GetAllDebit();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /////////////////////////////////
        ////////////////

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
        ///////////////////////////////////
        /////////////////
        /////////////////
        public void ConfDate()
        {

            dt = OutFun.GetRequstOutSngle(IdOut);
            comboBox1.SelectedValue = Convert.ToInt32(dt.Rows[0]["IDCategory"].ToString());
            comboBox2.SelectedValue = Convert.ToInt32(dt.Rows[0]["IDType"].ToString());
            comboBox3.SelectedValue = Convert.ToInt32(dt.Rows[0]["IDCurrency"].ToString());
            comboBox4.SelectedValue = Convert.ToInt32(dt.Rows[0]["IDPlace"].ToString());
            comboBox5.SelectedValue = Convert.ToInt32(dt.Rows[0]["Debit"].ToString());
            comboBox6.SelectedValue = Convert.ToInt32(dt.Rows[0]["Creditor"].ToString());

            textBox1.Text = dt.Rows[0]["Quntity"].ToString();
            textBox2.Text = dt.Rows[0]["Price"].ToString();
            textBox3.Text = dt.Rows[0]["NameOut"].ToString();
            textBox4.Text = dt.Rows[0]["NameSend"].ToString();
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnupdate_Click(object sender, EventArgs e)
        {
            if ((int)comboBox4.SelectedValue > 0 && (int)comboBox5.SelectedValue > 0 && (int)comboBox6.SelectedValue > 0 && textBox4.Text.Length > 0 && textBox5.Text.Length > 0 && textBox3.Text.Length > 0)
            {
                if ((MessageBox.Show("هل تريد ترحيل طلب  تعديل الصرف واعتماده ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
                {
                    OutFun.DeleteSuuplyFrmAccountDitalis2(IdOut);/// حذف الطلب من جدول التفاصيل 
                    int IDcounoldPlus = Convert.ToInt32(dt.Rows[0]["Creditor"].ToString());
                    int IdCountOldMins = Convert.ToInt32(dt.Rows[0]["Debit"].ToString());
                    int OldMony= Convert.ToInt32(dt.Rows[0]["Quntity"].ToString()) * Convert.ToInt32(dt.Rows[0]["Price"].ToString());
                    int oldIDCurrncy= Convert.ToInt32(dt.Rows[0]["IDCurrency"].ToString()); ;
                    int IDcoutNEWPlus = (int)comboBox6.SelectedValue;
                    int IdCoutNEWMins = (int)comboBox5.SelectedValue;
                    int mony = Convert.ToInt32(textBox1.Text) * Convert.ToInt32(textBox2.Text);
                    int idcurncy = Convert.ToInt32(dt.Rows[0]["IDCurrency"].ToString());
                    string DitalisMis = "تم قيد عليكم مبلغ وقدره " + (mony).ToString() + "مقابل امر صرف ب  " +textBox1.Text + " " + comboBox1.Text+ " " + comboBox2.Text + "  الى حساب  " + comboBox6.Text + "رقم الطلب " + IdOut;
                    string DatlisPlus = "تم قيد لكم مبلغ وقدره" + (mony).ToString() + "مقابل امر توريد ب " + textBox1.Text+ " " +comboBox1.Text+ " " + comboBox2.Text + "  من حساب " + comboBox5.Text + "رقم الطلب " + IdOut;
                    OutFun.AddNewAccountDetalis(IDcoutNEWPlus, mony, 0, IdOut, DatlisPlus, DateTime.Now, UserID, idcurncy);////اضافة الدائن الى جدول التفاصيل
                    OutFun.AddNewAccountDetalis(IdCoutNEWMins, (-1 * mony), 0, IdOut, DitalisMis, DateTime.Now, UserID, idcurncy);//// اضافة المدين لى جدول التفاصيل
                    //////////////
                    /////// التعديل جدول اجمالي الحسابات
                    OutFun.UpdateAccountTotal(IDcounoldPlus, (-1 * OldMony), idcurncy);// حذف القمية من حساب الدائن
                    OutFun.UpdateAccountTotal(IdCountOldMins, OldMony, idcurncy);//  ارجاع القمية الى حساب المدين
                    if (OutFun.CheckAccontTotal(IDcoutNEWPlus, idcurncy))/// في حالة انم الحساب الدائن موجود مسبقا
                    {
                        OutFun.UpdateAccountTotal(IDcoutNEWPlus, mony, idcurncy);
                    }
                    else// في حالة الحساب الدائن جديد نضيف حساب جديد
                    {
                        OutFun.AddNewAccountTotal(IDcoutNEWPlus, mony, idcurncy);//
                    }
                    if (OutFun.CheckAccontTotal(IdCoutNEWMins, idcurncy)) //في حالة الحساب المدين موجودمسبقا
                      {
                        OutFun.UpdateAccountTotal(IdCoutNEWMins, (-1 * mony), idcurncy);
                    }
                    else //في حالة الحساب المدين جديد اضافة حساب جديد
                    {
                        OutFun.AddNewAccountTotal(IdCoutNEWMins, (-1 * mony), idcurncy);1
                    }
                    OutFun.UpdateRequstOut(IdOut, (int)comboBox4.SelectedValue, textBox3.Text, textBox4.Text, textBox5.Text, DateTime.Now, UserID, (int)comboBox5.SelectedValue, (int)comboBox6.SelectedValue);
                    
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("تاكد من تعبئة جميع الصناديق");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
