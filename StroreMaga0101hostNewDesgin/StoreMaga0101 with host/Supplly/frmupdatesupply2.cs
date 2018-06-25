using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Supplly
{
    public partial class frmupdatesupply2 : Form
    {
        Supplly.SupplyRequset SuRe;
        DataTable dt = new DataTable();
        bool HostConnection;
        ServiceReference1.IserviceClient SureHost;
        int UserID;
        public frmupdatesupply2()
        {
            InitializeComponent();
            SuRe = new SupplyRequset(@".\s2008", "StoreManagement1",null ,null);
            UserID = 1;
        }
      

        public frmupdatesupply2(string ServerNm, string DBnm, string UserSql, string PassSql, int UserId,bool hostconnection,string Iphost)
        {
            InitializeComponent();
            HostConnection = hostconnection;
            UserID = UserId;
            if (HostConnection == false)
            {
                SuRe = new SupplyRequset(ServerNm, DBnm, UserSql, PassSql);
            }
            else
            {
                SureHost = new ServiceReference1.IserviceClient();
                EndpointAddress endp = new EndpointAddress(Iphost);
                SureHost.Endpoint.Address = endp;
            }

            
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
                        int idAcount2;
                        if (HostConnection == false)
                        {
                            idAcount2 = SuRe.CheckAccountIsHere(Convert.ToInt32(dt.Rows[0]["IDCategory"].ToString()), Convert.ToInt32(dt.Rows[0]["IDType"].ToString()), Convert.ToInt32(dt.Rows[0]["Price"].ToString()), Convert.ToInt32(dt.Rows[0]["IDCurrency"].ToString()));

                        }
                        else
                        {
                            idAcount2 = SureHost.CheckAccountIsHereInSuplly(Convert.ToInt32(dt.Rows[0]["IDCategory"].ToString()), Convert.ToInt32(dt.Rows[0]["IDType"].ToString()), Convert.ToInt32(dt.Rows[0]["Price"].ToString()), Convert.ToInt32(dt.Rows[0]["IDCurrency"].ToString()));

                        }
                        int oldIdCurrncy = Convert.ToInt32(dt.Rows[0]["IDCurrency"]);
                        int dAccountMinsOld= Convert.ToInt32(dt.Rows[0]["Debit"]);
                        int dAccountPulsOld = Convert.ToInt32(dt.Rows[0]["Creditor"]);
                        int QuntityHere;
                        if (HostConnection == false)
                        {
                            QuntityHere = SuRe.GetQuntityInAccount(idAcount2);
                        }
                        else
                        {
                            QuntityHere = SureHost.GetQuntityInAccountInSupply(idAcount2);

                        }
                       
                        if (QuntityHere >= oldQuntity)
                        {
                            int qu = QuntityHere - oldQuntity;
                            if (HostConnection == false)
                            {
                                SuRe.UpdateQuntityAccount(idAcount2, qu);
                            }
                            else
                            {
                                SureHost.UpdateQuntityAccountInSuplly(idAcount2, qu);
                            }
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
                            int idAcount;
                            if (HostConnection == false)
                            {
                                idAcount = SuRe.CheckAccountIsHere(IDCAT, IDTYPE, NewPrice, idcurrn);
                            }
                            else
                            {
                                idAcount = SureHost.CheckAccountIsHereInSuplly(IDCAT, IDTYPE, NewPrice, idcurrn);
                            }

                                int NewTotla = NewPrice*newQuntity;
                            if (idAcount > 0) // في حالة الحساب موجود من قبل
                            {
                                //  تعديل الحساب بالكمية الجديدة
                                int oldQunt;
                                if (HostConnection == false)
                                {
                                    oldQunt = SuRe.GetQuntityInAccount(idAcount);
                                }
                                else
                                {
                                    oldQunt = SureHost.GetQuntityInAccountInSupply(idAcount);
                                }


                                int newQunt = oldQunt + newQuntity;
                                if (HostConnection == false)
                                {
                                    SuRe.UpdateQuntityAccount(idAcount, newQunt);
                                    ///// Acount Detilas
                                   
                                    SuRe.DeleteSuuplyFrmAccountDitalis(IDSupply); // حذف الحساب من جدول التفاصيل 
                                }
                                else
                                {
                                    SureHost.UpdateQuntityAccountInSuplly(idAcount, newQunt);
                                    ///// Acount Detilas
                                    SureHost.DeleteSuuplyFrmAccountDitalisInSupply(IDSupply); // حذف الحساب من جدول التفاصيل 
                                }
                                string DitalisMis = "تم قيد عليكم مبلغ وقدره " +" "+ string.Format("{0:##,##}", (NewTotla).ToString()) + " " + comboBox3.Text + "  " + "مقابل امر توريد ب  " + " "+newQuntity.ToString() + " " + comboBox1.Text + " " + comboBox2.Text + " الى حساب" +" "+ comboBox5.Text + "رقم الطلب " + " "+IDSupply;
                                string DitalisPlus = "تم قيد لكم مبلغ وقدره" +" "+ string.Format("{0:##,##}", (NewTotla).ToString()) + " " + comboBox3.Text + "  " + "مقابل امر توريد ب " + newQuntity.ToString() + " " + comboBox1.Text + " " + comboBox2.Text + "  من حساب " + comboBox4.Text + "رقم الطلب " +" "+ IDSupply;
                                if (HostConnection == false)
                                {
                                    SuRe.AddNewAccountDetalis(IdAccountPlus, NewTotla, IDSupply, 0, DitalisMis, DateTime.Now, UserID, idcurrn, 0);//اضافة الحساب الدائن المعدل الى جدول التفاصيل
                                    SuRe.AddNewAccountDetalis(IdAccountMins, (-1 * NewTotla), IDSupply, 0, DitalisPlus, DateTime.Now, UserID, idcurrn, 0);//اضافة الحساب المدين المعدل الى جدول التفاصيل
                                }
                                else
                                {
                                    SureHost.AddNewAccountDetalisINSupply(IdAccountPlus, NewTotla,IDSupply, 0, DitalisMis, DateTime.Now, UserID, idcurrn, 0);//اضافة الحساب الدائن المعدل الى جدول التفاصيل
                                    SureHost.AddNewAccountDetalisINSupply(IdAccountMins, (-1 * NewTotla), IDSupply, 0, DitalisPlus, DateTime.Now, UserID, idcurrn, 0);//اضافة ال

                                }
                            }
                            else //  في حالة الحساب جديد
                            {
                                string DitalisMis = "تم قيد عليكم مبلغ وقدره " + string.Format("{0:##,##}", (NewTotla).ToString()) + " " + comboBox3.Text + "  " + "مقابل امر توريد ب  " + newQuntity.ToString() + " " + comboBox1.Text + " " + comboBox2.Text + "  الى حساب" + comboBox5.Text + "رقم الطلب " + IDSupply;
                                string DitalisPlus = "تم قيد لكم مبلغ وقدره" + string.Format("{0:##,##}", (NewTotla).ToString()) + " " + comboBox3.Text + "  " + "مقابل امر توريد ب " + newQuntity.ToString() + " " + comboBox1.Text + " " + comboBox2.Text + "  من حساب " + comboBox4.Text + "رقم الطلب " + IDSupply;
                                if (HostConnection == false)
                                {
                                    SuRe.AddNewAccount(IDCAT, IDTYPE, newQuntity, NewPrice, idcurrn);// اضافة حساب جديد
                                    SuRe.DeleteSuuplyFrmAccountDitalis(IDSupply); // حذف الحساب من جدول التفاصيل 
                                    SuRe.AddNewAccountDetalis(IdAccountPlus, NewTotla, SuRe.GetMaxIdSupply(), 0, DitalisMis, DateTime.Now, UserID, idcurrn, 0);//اضافة الحساب الدائن المعدل الى جدول التفاصيل
                                    SuRe.AddNewAccountDetalis(IdAccountMins, (-1 * NewTotla), SuRe.GetMaxIdSupply(), 0, DitalisPlus, DateTime.Now, UserID, idcurrn, 0);//اضافة الحساب المدين المعدل الى جدول التفاصيل
                                }
                                else//connection host
                                {
                                    SureHost.AddNewAccount(IDCAT, IDTYPE, newQuntity, NewPrice, idcurrn);// اضافة حساب جديد
                                    SureHost.DeleteSuuplyFrmAccountDitalisInSupply(IDSupply); // حذف الحساب من جدول التفاصيل 
                                    SureHost.AddNewAccountDetalisINSupply(IdAccountPlus, NewTotla, SureHost.GetMaxIdSupply(), 0, DitalisMis, DateTime.Now, UserID, idcurrn, 0);//اضافة الحساب الدائن المعدل الى جدول التفاصيل
                                    SureHost.AddNewAccountDetalisINSupply(IdAccountMins, (-1 * NewTotla), SureHost.GetMaxIdSupply(), 0, DitalisPlus, DateTime.Now, UserID, idcurrn, 0);//اضافة الحساب المدين المعدل الى جدول التفاصيل

                                }

                            }

                            if (HostConnection == false)
                            {
                                //////////////////////////////
                                ///////////////// 
                                ///// AccountTotal 
                                SuRe.UpdateAccountTotal(dAccountPulsOld, (-1 * oldTotal), oldIdCurrncy); // حذف القيمة من حساب الدائن
                                SuRe.UpdateAccountTotal(dAccountMinsOld, oldTotal, oldIdCurrncy);// حذف القيمة من حساب المدين
                                                                                                 ///////////////////////////////////////////////////////////////


                                if (SuRe.CheckAccontTotal(IdAccountPlus, idcurrn)) // في حالة كان الحساب الدائن موجود من قبل
                                {
                                    SuRe.UpdateAccountTotal(IdAccountPlus, NewTotla, idcurrn);

                                }
                                else
                                {
                                    SuRe.AddNewAccountTotal(IdAccountPlus, NewTotla, idcurrn); // اضافة حساب جديد دائن
                                }

                                ///////////
                                if (SuRe.CheckAccontTotal(IdAccountMins, idcurrn)) // في حالة كان الحساب المدين موجود من قبل
                                {
                                    SuRe.UpdateAccountTotal(IdAccountMins, (-1) * NewTotla, idcurrn);
                                }
                                else
                                {
                                    SuRe.AddNewAccountTotal(IdAccountMins, (-1) * NewTotla, idcurrn); // اضافة حساب جديد مدين 
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
                            }
                            else //Connection Host
                            { //////////////////////////////
                                ///////////////// 
                                ///// AccountTotal 
                                SureHost.UpdateAccountTotalInSupply(dAccountPulsOld, (-1 * oldTotal), oldIdCurrncy); // حذف القيمة من حساب الدائن
                                SureHost.UpdateAccountTotalInSupply(dAccountMinsOld, oldTotal, oldIdCurrncy);// حذف القيمة من حساب المدين
                                                                                                 ///////////////////////////////////////////////////////////////


                                if (SureHost.CheckAccontTotalInSuuly(IdAccountPlus, idcurrn)) // في حالة كان الحساب الدائن موجود من قبل
                                {
                                    SureHost.UpdateAccountTotalInSupply(IdAccountPlus, NewTotla, idcurrn);

                                }
                                else
                                {
                                    SureHost.AddNewAccountTotalInSuuply(IdAccountPlus, NewTotla, idcurrn); // اضافة حساب جديد دائن
                                }

                                ///////////
                                if (SureHost.CheckAccontTotalInSuuly(IdAccountMins, idcurrn)) // في حالة كان الحساب المدين موجود من قبل
                                {
                                    SureHost.UpdateAccountTotalInSupply(IdAccountMins, (-1) * NewTotla, idcurrn);
                                }
                                else
                                {
                                    SureHost.AddNewAccountTotalInSuuply(IdAccountMins, (-1) * NewTotla, idcurrn); // اضافة حساب جديد مدين 
                                }
                                /////////////////////////////

                                /////////////////////////////////
                                ///////////////////////////////////////////////////////////////
                                // عملية التعديل في جدول التوريد
                                SureHost.UPateRequstSupply(IDSupply, IDCAT, IDTYPE, newQuntity, NewPrice, idcurrn, nameNEW, dt.Rows[0]["DescSupply"].ToString(), IdAccountMins, IdAccountPlus);
                                //////////////////
                                ////////////
                                // عملية الحفظ في جدول التعديلات
                                SureHost.ADDNewUPDSupply(IDSupply, Convert.ToInt32(dt.Rows[0]["IDCategory"].ToString()), Convert.ToInt32(dt.Rows[0]["IDType"].ToString()), Convert.ToInt32(dt.Rows[0]["Quntity"].ToString()), Convert.ToInt32(dt.Rows[0]["Price"].ToString()), Convert.ToInt32(dt.Rows[0]["IDCurrency"].ToString()), dt.Rows[0]["NameSupply"].ToString(), DateTime.Parse(dt.Rows[0]["DateSupply"].ToString()), DateTime.Now, decNew, UserID);

                            }

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
           
            comboBox2.DisplayMember = "اسم النوع";
            comboBox2.ValueMember = "رقم النوع";
           
            comboBox3.DisplayMember = "اسم العملة";
            comboBox3.ValueMember = "رقم العملة";
        
            comboBox4.ValueMember = "رقم الحساب";
            comboBox4.DisplayMember = "اسم الحساب";
           
            comboBox5.ValueMember = "رقم الحساب";
            comboBox5.DisplayMember = "اسم الحساب";
            if (HostConnection == false)
            {
                comboBox1.DataSource = SuRe.GetAllCategoryAR();
                comboBox2.DataSource = SuRe.GetAllTypeQuntity();
                comboBox3.DataSource = SuRe.GetAllCurrency();
                comboBox4.DataSource = SuRe.GetALLAcountNm();
                comboBox5.DataSource = SuRe.GetALLAcountNm();
            }
            else
            {
                comboBox1.DataSource =ConvertMemorytoDB(SureHost.GetAllCategoryAR());
                comboBox2.DataSource =ConvertMemorytoDB( SureHost.GetAllTypeQuntity());
                comboBox3.DataSource =ConvertMemorytoDB( SureHost.GetAllCurrency());
                comboBox4.DataSource =ConvertMemorytoDB( SureHost.GetALLAcountNm());
                comboBox5.DataSource =ConvertMemorytoDB( SureHost.GetALLAcountNm());
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
        public void ConfDate()
        {
            if (HostConnection == false)
            {
                dt = SuRe.GetRequstSupply(IDSupply);
            }
            else
            {
                dt =ConvertMemorytoDB( SureHost.GetRequstSupply(IDSupply));
            }
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
