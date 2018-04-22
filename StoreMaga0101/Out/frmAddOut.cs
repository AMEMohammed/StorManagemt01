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
namespace Out_
{
    public partial class frmAddOut : Form
    {
        int UserID;
        OutFunction OutFun;
        public frmAddOut()
        {
            InitializeComponent();
            UserID = 1;
            try
            {
                OutFun = new OutFunction(@".\s2008", "StoreManagement1",null,null);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
       }
       
        public frmAddOut(string ServerNm, string DbNm,string UserSql,string PassSql, int UserId)
        {
            InitializeComponent();
            UserID = UserId; ;
            try
            {
                OutFun = new OutFunction(ServerNm, DbNm, UserSql,PassSql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAddOut_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox5.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox5.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox6.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox6.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
                GetData1();
                changeLanguage();
                MessageBoxManager.Yes = "نعم";
                MessageBoxManager.No = "الغاء";

                MessageBoxManager.Register();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        ///////////////////////////
        /// <summary>
        /// 
        /// 
        private void comboBox4_Leave(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = OutFun.GetQunitiyinAccount2((int)comboBox1.SelectedValue, (int)comboBox2.SelectedValue, (int)comboBox4.SelectedValue).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// </summary>
        /////////
        void GetData1()
        {
            try
            {

                comboBox1.ValueMember = "رقم الصنف";
                comboBox1.DisplayMember = "اسم الصنف";
                comboBox1.DataSource = OutFun.GetCatagoryInAccount();

                comboBox3.ValueMember = "رقم الجهة";
                comboBox3.DisplayMember = "اسم الجهة";
                comboBox3.DataSource = OutFun.GetAllPlace();

                comboBox5.ValueMember = "رقم الحساب";
                comboBox5.DisplayMember = "اسم الحساب";
                comboBox5.DataSource = OutFun.GetALLAcountNm();

                comboBox6.ValueMember = "رقم الحساب";
                comboBox6.DisplayMember = "اسم الحساب";
                comboBox6.DataSource = OutFun.GetALLAcountNm();
                dataGridView1.DataSource = OutFun.SearchINRequstOutDate(DateTime.Now.Date, DateTime.Now); // جلب طلبات الصرف لليوم الحالي
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[12].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        //
        void Refersh2()
        {
            try
            {

                comboBox1.ValueMember = "رقم الصنف";
                comboBox1.DisplayMember = "اسم الصنف";
                comboBox1.DataSource = OutFun.GetCatagoryInAccount();
                comboBox2.DisplayMember = "اسم النوع";
                comboBox2.ValueMember = "رقم النوع";
                comboBox2.DataSource = OutFun.GetTypeInAccount((int)comboBox1.SelectedValue);
                comboBox4.ValueMember = "رقم العملة";
                comboBox4.DisplayMember = "اسم العملة";
                comboBox4.DataSource = OutFun.GetCurrencyINAccount((int)comboBox1.SelectedValue, (int)comboBox2.SelectedValue);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox5.Text = "";
                dataGridView1.DataSource = OutFun.SearchINRequstOutDate(DateTime.Now.Date, DateTime.Now); // جلب طلبات الصرف لليوم الحالي
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[12].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////   
        public void GetDate2()
        {
            try
            {
                comboBox2.DisplayMember = "اسم النوع";
                comboBox2.ValueMember = "رقم النوع";
                comboBox2.DataSource = OutFun.GetTypeInAccount((int)comboBox1.SelectedValue);
                int IDACCOunt = OutFun.GetAccountLinkCate((int)comboBox1.SelectedValue);
                if (IDACCOunt > 0)
                    comboBox6.SelectedValue = IDACCOunt;
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
        }
        ////////////////////////////////////////////////
        private void comboBox1_Leave(object sender, EventArgs e)
        {
            GetDate2();
        }
        //////////////////////////////////
        public void changeLanguage()
        {
            foreach (InputLanguage lng in InputLanguage.InstalledInputLanguages)
            {
                if (lng.LayoutName == "العربية (101)")
                    InputLanguage.CurrentInputLanguage = lng;
            }
        }

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            try
            {

                comboBox4.ValueMember = "رقم العملة";
                comboBox4.DisplayMember = "اسم العملة";
                comboBox4.DataSource = OutFun.GetCurrencyINAccount((int)comboBox1.SelectedValue, (int)comboBox2.SelectedValue);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        { 
            

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                try
                {
                    int qunntNow = OutFun.GetQunitiyinAccount2((int)comboBox1.SelectedValue, (int)comboBox2.SelectedValue, (int)comboBox4.SelectedValue);
                    int qunnMus = Convert.ToInt32(textBox2.Text);
                   
                    if (qunnMus > qunntNow)
                    {
                        MessageBox.Show("الكمية لا تسمح");
                        textBox2.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
            else
            { textBox2.Focus(); }
        }



        /// <summary>
        /// //////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        bool flagAddAgin = false;

        private void btnAddSup_Click(object sender, EventArgs e)
        {
            try
            {
                if ((int)comboBox1.SelectedValue > 0 & (int)comboBox2.SelectedValue > 0 & (int)comboBox5.SelectedValue > 0 & (int)comboBox3.SelectedValue > 0 & (int)comboBox6.SelectedValue > 0 & (int)comboBox4.SelectedValue > 0 & textBox2.Text.Length > 0 & textBox3.Text.Length > 0 & textBox4.Text.Length > 0)
                {
                    if ((MessageBox.Show("هل تريد ترحيل طلب الصرف ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
                    {
                        int Idcat = ((int)comboBox1.SelectedValue);
                        int idtyp = ((int)comboBox2.SelectedValue);
                        int idplace = ((int)comboBox3.SelectedValue);
                        int idcurrnt = ((int)comboBox4.SelectedValue);
                        int QunntyMust = Convert.ToInt32(textBox2.Text);
                        int IdAccountPlus = ((int)comboBox6.SelectedValue);
                        int IdAccountMins = ((int)comboBox5.SelectedValue);
                        string nameAmmer = textBox3.Text;
                        string nameMostlaem = textBox4.Text;
                        string Decrip = textBox5.Text;



                        DataTable dtAccountIDs = new DataTable();
                        dtAccountIDs = OutFun.GetAccountIDs(Idcat, idtyp, idcurrnt); // جلب الحسابات التي تحتوي على نفس النوع والصنف
                        int MaxCheckRequstOut;
                        if (flagAddAgin == true)
                        {
                            MaxCheckRequstOut = OutFun.GetMaxCheckInRequsetOut();
                        }
                        else
                        {
                            MaxCheckRequstOut = OutFun.GetMaxCheckInRequsetOut();
                            MaxCheckRequstOut += 1;
                        }

                        int Quntity2 = QunntyMust;

                        for (int i = 0; i < dtAccountIDs.Rows.Count; i++)
                        {
                            int IDAccount = Convert.ToInt32(dtAccountIDs.Rows[i][0].ToString());
                            int result = OutFun.GetAndCheckQuntityAccountAndAddRqustNew(IDAccount, Quntity2, Idcat, idtyp, idcurrnt, idplace, nameAmmer, Decrip, DateTime.Now, MaxCheckRequstOut, nameMostlaem, IdAccountMins, IdAccountPlus, UserID,comboBox1.Text,comboBox2.Text,comboBox6.Text,comboBox5.Text,comboBox4.Text);
                            if (result == 0)
                            {
                                break;

                            }
                            else
                            {

                                Quntity2 -= result;

                            }

                        }
                        if ((MessageBox.Show("هل تريد اضافة طلب اخر؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
                        {
                            comboBox3.Enabled = false;
                            textBox3.Enabled = false;
                            textBox4.Enabled = false;

                            comboBox5.Enabled = false;
                            comboBox6.Enabled = false;
                            flagAddAgin = true;
                           
                            Refersh2();
                        }
                        else
                        {
                            flagAddAgin = false;
                            if ((MessageBox.Show("هل تريد طباعة سند صرف؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
                            {
                                
                                int IDcheck = OutFun.GetMaxCheckInRequsetOut();
                                    //string name = dataGridView1.SelectedRows[0].Cells[11].Value.ToString();

                                DataTable dtExit = new DataTable();
                                
                                if (checkBox1.Checked)
                                {
                                    dtExit = OutFun.printrequstOutExit(IDcheck, UserID, UserID);
                                }
                                else
                                {
                                    dtExit = null;
                                }

                                DataTable dtOut = new DataTable();
                               

                                dtOut = OutFun.PrintRequstOut(IDcheck, UserID, UserID);
                                this.Cursor = Cursors.WaitCursor;
                                FrmRports.frmReprt frmrepett = new FrmRports.frmReprt(dtOut, dtExit, 2);
                                frmrepett.ShowDialog();
                                this.Cursor = Cursors.Default;
                                //  frmREPORT frm = new frmREPORT(MaxCheckRequstOut, 2, Contrl.UserId, printExit);

                                //frm.ShowDialog();

                                refrsh1();
                            }
                            refrsh1();
                        }
                    }
                }
            }

           catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //////////////////////////////////////////////////////////////
        /// </summary>
        //////// refersh
        void refrsh1()
        {
            GetData1();
            GetDate2();
            GetDate3();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            btnExit.Focus();
            comboBox3.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            comboBox5.Enabled = true;
            comboBox6.Enabled = true;
       }
        /// <summary>
        /// ////////
        /// </summary>
        public void GetDate3()
        {
            try
            {

                comboBox4.ValueMember = "رقم العملة";
                comboBox4.DisplayMember = "اسم العملة";
                comboBox4.DataSource = OutFun.GetCurrencyINAccount((int)comboBox1.SelectedValue, (int)comboBox2.SelectedValue);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// ////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefrsh_Click(object sender, EventArgs e)
        {
            refrsh1();
            flagAddAgin = false;
        }
        /// <summary>
        /// //////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)

            {
                try
                {

                    int IDcheck = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[13].Value.ToString());
                    string name = dataGridView1.SelectedRows[0].Cells[11].Value.ToString();
                  
                    DataTable dtExit=new DataTable();
                    int UserAdd = 0;
                    try
                    {
                         UserAdd = OutFun.GetIdUser(name);
                    }
                    catch
                    {
                        throw;
                    }
                   
                    if (checkBox1.Checked)
                    {
                        dtExit = OutFun.printrequstOutExit(IDcheck, UserID,UserAdd);
                    }
                    else
                    {
                        dtExit = null;
                    }
               
                    DataTable dtOut = new DataTable();
                    int usee = OutFun.GetIdUser(name);
               
                    dtOut = OutFun.PrintRequstOut(IDcheck, UserID, UserAdd);
                    this.Cursor = Cursors.WaitCursor;
                    FrmRports.frmReprt frmrepett = new FrmRports.frmReprt(dtOut, dtExit, 2);
                    frmrepett.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    //throw;
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /////////////////////////
        /////////////////////////////////////
      
        /// <summary>
        /// /////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_Enter(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = OutFun.GetQunitiyinAccount2((int)comboBox1.SelectedValue, (int)comboBox2.SelectedValue, (int)comboBox4.SelectedValue).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            GetDate2();
        }

      


       
    }
}
