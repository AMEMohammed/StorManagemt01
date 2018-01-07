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
namespace Supplly
{
    public partial class frmUpdatSupply : Form
    {
        Supplly.SupplyRequset SuRe;
        int UserID;
        public frmUpdatSupply()
        {
            InitializeComponent();
            SuRe = new SupplyRequset(@".\s2008", "StoreManagement1",null,null);
            UserID = 1;

        }
        

        public frmUpdatSupply(string ServerNm, string DBnm,string UserSql,string PassSql, int UserId)
        {
            InitializeComponent();
            SuRe = new SupplyRequset(ServerNm, DBnm,UserSql,PassSql);
            UserID = UserId;
        }
        private void groupBox11_Enter(object sender, EventArgs e)
        {

        }

        private void frmUpdatSupply_Load(object sender, EventArgs e)
        {

            try
            {
                changeLanguage();
                comboBox1.SelectedIndex = 0;

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
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

        private void btnAddSup_Click(object sender, EventArgs e)
        {
         
            if(comboBox1.SelectedIndex==0)
            {
                dataGridView1.DataSource = SuRe.SearchINRequsetSupplyTxtAndDate(textBox3.Text, DateTime.Now.AddDays(-1), DateTime.Now);
            }
            else   if(comboBox1.SelectedIndex==1)
            {
                dataGridView1.DataSource = SuRe.SearchINRequsetSupplyTxtAndDate(textBox3.Text,  Convert.ToDateTime("2016/01/01"), dateTimePicker2.Value);

            }
            else if(comboBox1.SelectedIndex==2)
            {
                dataGridView1.DataSource = SuRe.SearchINRequsetSupplyTxtAndDate(textBox3.Text, DateTime.Now.AddDays(-7), DateTime.Now);
            }
            else if(comboBox1.SelectedIndex==3)
            {
                dataGridView1.DataSource = SuRe.SearchINRequsetSupplyTxtAndDate(textBox3.Text, DateTime.Now.AddDays(-30), DateTime.Now);
            }
            else if  (comboBox1.SelectedIndex == 4)
            {
                dataGridView1.DataSource = SuRe.SearchINRequsetSupplyTxtAndDate(textBox3.Text,dateTimePicker1.Value, dateTimePicker2.Value);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(((ComboBox)sender).SelectedIndex==4)
            {
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
            }
            else if(((ComboBox)sender).SelectedIndex == 1)
            {
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = true;
                label2.Visible = false  ;
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    if ((MessageBox.Show("هل تريد ترحيل طلب  حذف التوريد واعتماده ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))

                    {/// جلب رقم الطلب 
                        int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        DataTable dt = new DataTable();
                        /// جلب بيانات الطلب
                        dt = SuRe.GetRequstSupply(id);
                        int oldQuntity = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
                        int idAcount2 = SuRe.CheckAccountIsHere(Convert.ToInt32(dt.Rows[0]["IDCategory"].ToString()), Convert.ToInt32(dt.Rows[0]["IDType"].ToString()), Convert.ToInt32(dt.Rows[0]["Price"].ToString()), Convert.ToInt32(dt.Rows[0]["IDCurrency"].ToString()));

                        int QuntityHere = SuRe.GetQuntityInAccount(idAcount2);
                        if (QuntityHere >= oldQuntity)
                        {
                            // حذف الطلب
                            int qu = QuntityHere - oldQuntity;
                            SuRe.UpdateQuntityAccount(idAcount2, qu); // تعديل الكمية في جدول المخزون
                                                                      //اضافة الطلب في جدول التعديلات
                            SuRe.ADDNewUPDSupply(id, Convert.ToInt32(dt.Rows[0]["IDCategory"].ToString()), Convert.ToInt32(dt.Rows[0]["IDType"].ToString()), Convert.ToInt32(dt.Rows[0]["Quntity"].ToString()), Convert.ToInt32(dt.Rows[0]["Price"].ToString()), Convert.ToInt32(dt.Rows[0]["IDCurrency"].ToString()), dt.Rows[0]["NameSupply"].ToString(), DateTime.Parse(dt.Rows[0]["DateSupply"].ToString()), DateTime.Now, "تم حذف الطلب", UserID);

                            /// Account Monay Totla
                            DataTable dtSupply = SuRe.GetRequstSupply(id);
                            int IDAccounPlus = Convert.ToInt32(dtSupply.Rows[0]["Creditor"].ToString());
                            int IDAccountMins = Convert.ToInt32(dtSupply.Rows[0]["Debit"].ToString());
                            int IDcurrncy = Convert.ToInt32(dtSupply.Rows[0]["IDCurrency"].ToString());
                            int TotalMony = Convert.ToInt32(dtSupply.Rows[0]["Quntity"].ToString()) * Convert.ToInt32(dtSupply.Rows[0]["Price"].ToString());
                            SuRe.UpdateAccountTotal(IDAccounPlus, (-1 * TotalMony), IDcurrncy);// حذف القيمة من حساب الدائن
                            SuRe.UpdateAccountTotal(IDAccountMins, TotalMony, IDcurrncy);// ارجاع القيمة الى حساب المدين

                            ////////
                            //////
                            SuRe.DeleteSuuplyFrmAccountDitalis(id);//حذف الطلب من جدول تفاصيل الحساب



                            SuRe.DeleteRequstSupply(id); //حذف الطلب من جدول الطلبات

                            dataGridView1.DataSource = SuRe.SearchINRequsetSupplyDate(DateTime.Now.AddDays(-7), DateTime.Now);

                        }
                        else
                        {
                            MessageBox.Show("تاكد من الكيمة المخزونة");
                        }
                      


                        //////////////////////////////////////////////////////////
                        ///////////////////////
                        ////////////
                        ///

                        // 

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void تعديلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                  frmupdatesupply2 frmu = new frmupdatesupply2();
                    frmu.Tag = id;
                    this.Cursor = Cursors.WaitCursor;

                    frmu.ShowDialog();
                    this.Cursor = Cursors.Default;
                    dataGridView1.DataSource = SuRe.SearchINRequsetSupplyDate(DateTime.Now.AddDays(-7), DateTime.Now);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// ////// print one requst
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefrsh_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[11].Value.ToString());
                    string name = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
                    this.Cursor = Cursors.WaitCursor;

                    DataTable dtSupp = new DataTable();
                    DataTable dtExite = new DataTable();
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

        private void btnPrint_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataTable dt = new DataTable();


                ////////// اضافة الاعمدة 
                try
                {
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dt.Columns.Add(dataGridView1.Columns[i].HeaderText);


                    }
                    dt.Columns.Add("اسم الموظفف");
                    dt.Columns.Add("اجمالي الكمية");
                    dt.Columns.Add("اجمالي السعر");
                    dt.Columns.Add("اجمالي الاجمالي");
                }

                catch (Exception ex) { MessageBox.Show(ex.Message); }
                ///////////////////  أاضافة سطور 

               // try
                {
                    int sumQu = 0;
                    int SumPrs = 0;
                    int SumTot = 0;
                    foreach (DataGridViewRow dgr in dataGridView1.SelectedRows)
                    {
                        DataRow dr = ((DataRowView)dgr.DataBoundItem).Row;
                        int idS = Convert.ToInt32(dr[0].ToString());
                        string nmCa = dr[1].ToString();
                        string nmty = dr[2].ToString();
                        int Qun = Convert.ToInt32(dr[3].ToString());
                        sumQu += Qun;
                      

                        int prs = Convert.ToInt32(dr[4].ToString());
                        SumPrs += prs;
                        int totl = Convert.ToInt32(dr[5].ToString());
                        SumTot += totl;
                        string currn = dr[6].ToString();
                        DateTime dd = DateTime.Parse(dr[7].ToString());
                        string namee = dr[8].ToString();
                        string dec = dr[9].ToString();
                        string nameUser = SuRe.GetUserNameBYIdUser(UserID);
                        MessageBox.Show(string.Format("{0:##,##}", sumQu));

                        dt.Rows.Add(idS, nmCa, nmty, string.Format("{0:##,##}", Qun), string.Format("{0:##,##}", prs), string.Format("{0:##,##}", totl), currn, dd.Date.ToShortDateString(), namee, dec, nameUser, string.Format("{0:##,##}",sumQu), string.Format("{0:##,##}", SumPrs), string.Format("{0:##,##}", SumTot));


                    }
                }
               // catch (Exception ex)
                {
                 //   MessageBox.Show(ex.Message);
                }
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    frmReprt frmrep = new frmReprt(dt, dt, 3);
                  
                    frmrep.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
