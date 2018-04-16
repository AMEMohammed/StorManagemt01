using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supplly
{
    public partial class frmupdatesupply2 : Form
    {
        Supplly.SupplyRequset SuRe;
        DataTable dt = new DataTable();
        int UserID;
        public frmupdatesupply2()
        {
            InitializeComponent();
            SuRe = new SupplyRequset(@".\s2008", "StoreManagement1",null ,null);
            UserID = 1;
        }
      

        public frmupdatesupply2(string ServerNm, string DBnm, string UserSql, string PassSql, int UserId)
        {
            InitializeComponent();
            SuRe = new SupplyRequset(ServerNm, DBnm, UserSql, PassSql);
            UserID = UserId;
        }
        private void btnAddSup_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && (int)comboBox1.SelectedValue > 0 && (int)comboBox4.SelectedValue > 0 && (int)comboBox5.SelectedValue > 0 && (int)comboBox2.SelectedValue > 0 && (int)comboBox3.SelectedValue > 0 && textBox5.Text.Length > 0)
            {
                if ((MessageBox.Show("هل تريد ترحيل طلب  تعديل التوريد واعتماده ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
                {   ////////////////////////////////////
                    // حذف الكمية السابقة من جدول الحسابات
                    try
                    {
                        int oldQuntity = Convert.ToInt32(dt.Rows[0]["Quntity"].ToString());
                        int oldTotal = Convert.ToInt32(dt.Rows[0]["Quntity"]) * Convert.ToInt32(dt.Rows[0]["Price"]);
                        int idAcount2 = SuRe.CheckAccountIsHere(Convert.ToInt32(dt.Rows[0]["IDCategory"].ToString()), Convert.ToInt32(dt.Rows[0]["IDType"].ToString()), Convert.ToInt32(dt.Rows[0]["Price"].ToString()), Convert.ToInt32(dt.Rows[0]["IDCurrency"].ToString()));
                        int oldIdCurrncy = Convert.ToInt32(dt.Rows[0]["IDCurrency"]);
                        int dAccountMinsOld= Convert.ToInt32(dt.Rows[0]["Debit"]);
                        int dAccountPulsOld = Convert.ToInt32(dt.Rows[0]["Creditor"]);
                        int QuntityHere = SuRe.GetQuntityInAccount(idAcount2);
                       
                        if (QuntityHere >= oldQuntity)
                        {
                            int qu = QuntityHere - oldQuntity;
                            SuRe.UpdateQuntityAccount(idAcount2, qu);
                            //////////////////////////////////////
                            //// عملية ادخل القيمة الجديدة في المخزون
                            int newQuntity = Convert.ToInt32(textBox1.Text);
                            int NewPrice = Convert.ToInt32(textBox2.Text);
                            int IDCAT = (int)comboBox1.SelectedValue;
                            int IDTYPE = (int)comboBox2.SelectedValue;
                            int idcurrn = (int)comboBox3.SelectedValue;
                            int IdAccountMins = (int)comboBox4.SelectedValue;
                            int IdAccountPlus = (int)comboBox5.SelectedValue;
                            string nameNEW = textBox3.Text;
                            string decNew = textBox5.Text;
                            int idAcount = SuRe.CheckAccountIsHere(IDCAT, IDTYPE, NewPrice, idcurrn);
                            int NewTotla = NewPrice*newQuntity;
                            if (idAcount > 0) // في حالة الحساب موجود من قبل
                            {   //  تعديل الحساب بالكمية الجديدة
                                int oldQunt =SuRe.GetQuntityInAccount(idAcount);

                                int newQunt = oldQunt + newQuntity;

                                SuRe.UpdateQuntityAccount(idAcount, newQunt);
                                ///// Acount Detilas
                                SuRe.DeleteSuuplyFrmAccountDitalis(idAcount); // حذف الحساب من جدول التفاصيل 
                                string DitalisMis = "تم قيد عليكم مبلغ وقدره " + string.Format("{0:##,##}", (NewTotla).ToString()) + " " + comboBox3.Text +"  "+ "مقابل امر توريد ب  " + newQuntity.ToString() + " " + comboBox1.Text + " " + comboBox2.Text + "  الى حساب" + comboBox5.Text + "رقم الطلب " + idAcount;
                                string DitalisPlus = "تم قيد لكم مبلغ وقدره" + string.Format("{0:##,##}", (NewTotla).ToString())+" "+ comboBox3.Text+"  " + "مقابل امر توريد ب " + newQuntity.ToString() + " " + comboBox1.Text + " " + comboBox2.Text + "  من حساب " + comboBox4.Text + "رقم الطلب " + idAcount;

                                SuRe.AddNewAccountDetalis(IdAccountPlus, NewTotla, idAcount, 0, DitalisMis, DateTime.Now, UserID, idcurrn,0);//اضافة الحساب الدائن المعدل الى جدول التفاصيل
                                SuRe.AddNewAccountDetalis(IdAccountMins, (-1 * NewTotla), idAcount, 0, DitalisPlus, DateTime.Now, UserID, idcurrn,0);//اضافة الحساب المدين المعدل الى جدول التفاصيل

                            }
                            else //  في حالة الحساب جديد
                            {
                                string DitalisMis = "تم قيد عليكم مبلغ وقدره " + string.Format("{0:##,##}", (NewTotla).ToString()) + " " + comboBox3.Text +"  "+ "مقابل امر توريد ب  " + newQuntity.ToString() + " " + comboBox1.Text + " " + comboBox2.Text + "  الى حساب" + comboBox5.Text + "رقم الطلب " + idAcount;
                                string DitalisPlus = "تم قيد لكم مبلغ وقدره" + string.Format("{0:##,##}", (NewTotla).ToString()) + " " + comboBox3.Text +"  "+ "مقابل امر توريد ب " + newQuntity.ToString() + " " + comboBox1.Text + " " + comboBox2.Text + "  من حساب " + comboBox4.Text + "رقم الطلب " + idAcount;
                                SuRe.AddNewAccount(IDCAT, IDTYPE, newQuntity, NewPrice, idcurrn);// اضافة حساب جديد
                                SuRe.DeleteSuuplyFrmAccountDitalis(idAcount); // حذف الحساب من جدول التفاصيل 
                                SuRe.AddNewAccountDetalis(IdAccountPlus, NewTotla, SuRe.GetMaxIdSupply(), 0, DitalisMis, DateTime.Now, UserID, idcurrn,0);//اضافة الحساب الدائن المعدل الى جدول التفاصيل
                                SuRe.AddNewAccountDetalis(IdAccountMins, (-1 * NewTotla), SuRe.GetMaxIdSupply(), 0, DitalisPlus, DateTime.Now, UserID, idcurrn,0);//اضافة الحساب المدين المعدل الى جدول التفاصيل
                            }
                            //////////////////////////////
                            ///////////////// 
                            ///// AccountTotal 
                            SuRe.UpdateAccountTotal(dAccountMinsOld, (-1 * oldTotal), oldIdCurrncy); // حذف القيمة من حساب الدائن
                            SuRe.UpdateAccountTotal(dAccountPulsOld, oldTotal, oldIdCurrncy);// حذف القيمة من حساب المدين
                          ///////////////////////////////////////////////////////////////
                            

                            if(SuRe.CheckAccontTotal(IdAccountPlus,idcurrn)) // في حالة كان الحساب الدائن موجود من قبل
                            {
                                SuRe.UpdateAccountTotal(IdAccountPlus, NewTotla, idcurrn);

                            }
                            else
                            {
                                SuRe.AddNewAccountTotal(IdAccountPlus, NewTotla, idcurrn); // اضافة حساب جديد دائن
                            }

                            ///////////
                            if(SuRe.CheckAccontTotal(IdAccountMins,idcurrn)) // في حالة كان الحساب المدين موجود من قبل
                            {
                                SuRe.UpdateAccountTotal(IdAccountMins,(-1) *NewTotla, idcurrn);
                            }
                            else
                            {
                                SuRe.AddNewAccountTotal(IdAccountMins, (-1)* NewTotla, idcurrn); // اضافة حساب جديد مدين 
                            }
                            /////////////////////////////
                          
                            /////////////////////////////////
                            ///////////////////////////////////////////////////////////////
                            // عملية التعديل في جدول التوريد
                            SuRe.UPateRequstSupply(IDSupply, IDCAT, IDTYPE, newQuntity, NewPrice, idcurrn, nameNEW, dt.Rows[0]["DescSupply"].ToString(), IdAccountMins, IdAccountPlus);
                            //////////////////
                            ////////////
                            // عملية الحفظ في جدول التعديلات
                            SuRe.ADDNewUPDSupply(IDSupply, Convert.ToInt32(dt.Rows[0]["IDCategory"].ToString()), Convert.ToInt32(dt.Rows[0]["IDType"].ToString()), Convert.ToInt32(dt.Rows[0]["Quntity"].ToString()), Convert.ToInt32(dt.Rows[0]["Price"].ToString()), Convert.ToInt32(dt.Rows[0]["IDCurrency"].ToString()), dt.Rows[0]["NameSupply"].ToString(), DateTime.Parse(dt.Rows[0]["DateSupply"].ToString()), DateTime.Now, decNew, UserID);
                            MessageBox.Show("تم التعديل");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("تاكد من الكمية المخزنة");
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    this.Close();
                }
                
            }
            else
            {
                MessageBox.Show("يجب اكمل جميع الصناديق");
            }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

        }
        int IDSupply ;
        private void frmupdatesupply2_Load(object sender, EventArgs e)
        {
            try
            {
                IDSupply = (int)this.Tag;
              
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
                ConfDate();
                //////////// 
                changeLanguage();
                MessageBoxManager.Yes = "نعم";
                MessageBoxManager.No = "الغاء";
                MessageBoxManager.Register();
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }

        }
        /////////////////////////////////////////
        ///////////////////
        void getDate1()
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
            comboBox4.ValueMember = "رقم الحساب";
            comboBox4.DisplayMember = "اسم الحساب";
            comboBox4.DataSource = SuRe.GetALLAcountNm();
            comboBox5.ValueMember = "رقم الحساب";
            comboBox5.DisplayMember = "اسم الحساب";
            comboBox5.DataSource = SuRe.GetALLAcountNm();
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
        public void ConfDate()
        {

            dt = SuRe.GetRequstSupply(IDSupply);
            comboBox1.SelectedValue = Convert.ToInt32(dt.Rows[0]["IDCategory"].ToString());
            comboBox2.SelectedValue = Convert.ToInt32(dt.Rows[0]["IDType"].ToString());
            comboBox3.SelectedValue = Convert.ToInt32(dt.Rows[0]["IDCurrency"].ToString());
            comboBox4.SelectedValue = Convert.ToInt32(dt.Rows[0]["Debit"].ToString());
            comboBox5.SelectedValue = Convert.ToInt32(dt.Rows[0]["Creditor"].ToString());
            textBox1.Text = dt.Rows[0]["Quntity"].ToString();
            textBox2.Text = dt.Rows[0]["Price"].ToString();
            textBox3.Text = dt.Rows[0]["NameSupply"].ToString();
            //  textBox5.Text = dt.Rows[0]["DescSupply"].ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
