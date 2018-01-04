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
    public partial class frmSearchAccountNM : Form
    {
        AccountNm Acn;
        int IDUSER;
        public frmSearchAccountNM()
        {
            InitializeComponent();
            try
            {
                Acn = new AccountNm(@".\s2008", "StoreManagement1", null, null);
                IDUSER = 1; 
              
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        ///
        public frmSearchAccountNM(string ServNm, string DbNm, string UesrSql, string PassSql, int UserId)
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
        int IDTypeAccontPrime;// حساب رئيسي 1 او فرعي 2
        int IdAllAcount; // كافة الحسابات الفرعية 1 او لا2
        int IDYType; // نو الحساب اجمالي 1 او تق=فصيلي 2
         

        private void frmSearchAccountNM_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox2.SelectedIndex = 0;
                IDYType = 1;
                GETCurrncy();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void GETCurrncy()
        {
            comboBox4.DataSource = Acn.GetAllCurrency();
            comboBox4.ValueMember = "رقم العملة";
            comboBox4.DisplayMember = "اسم العملة";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                comboBox4.Enabled = false;

            }
            else
            {
                comboBox4.Enabled = true;
            }
        }
        /// <summary>
        /// / الحساب الرئيسي
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            groupBox4.Enabled = false;
                groupBox5.Enabled =true;
            IdAllAcount = 2;
            IDTypeAccontPrime = 1;
            IDYType = 1;
            comboBox1.DataSource = Acn.GETALLAccountPrime();
            comboBox1.ValueMember = "رقم الحساب";
            comboBox1.DisplayMember = "اسم الحساب";
        }
        /// <summary>
        /// ///// الحسابات الفرعية
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            groupBox4.Enabled = false;
            groupBox5.Enabled = false;
            IdAllAcount = 1;
            IDTypeAccontPrime = 2;
            IDYType = 1;
            comboBox1.DataSource = Acn.GETALLAccountSub();
            comboBox1.ValueMember = "رقم الحساب";
            comboBox1.DisplayMember = "اسم الحساب";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox4.Enabled =true;
            groupBox5.Enabled = true;
            IdAllAcount = 2;
            IDTypeAccontPrime = 2;
            IDYType = 1;
            comboBox1.DataSource = Acn.GETALLAccountSub();
            comboBox1.ValueMember = "رقم الحساب";
            comboBox1.DisplayMember = "اسم الحساب";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        { if (radioButton1.Checked)
            {
                IDYType = 1;
                groupBox9.Visible = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                IDYType = 2;
                groupBox9.Visible = true;
                checkBox1.Checked = false;
                comboBox4.Enabled = true;
            }
        }

        private void btnAddSup_Click(object sender, EventArgs e)
        {
            DateTime d1 = DateTime.Now ;
            DateTime d2= DateTime.Now;
            if (comboBox2.SelectedIndex == 0)
            {
                d1 = DateTime.Now.AddDays(-1);
                d2 = DateTime.Now;
            }           
           if(comboBox2.SelectedIndex==1)
            {
                d1 = Convert.ToDateTime("01/01/2017");
                d2 = dateTimePicker2.Value;
            }
           if(comboBox2.SelectedIndex==2)
            {
                d1 = DateTime.Now.AddDays(-7);
                d2 = DateTime.Now;

            }
           if(comboBox2.SelectedIndex==3)
            {
                d1 = DateTime.Now.AddDays(-30);
                d2 = DateTime.Now;
            }
           if(comboBox2.SelectedIndex==4)
            {
                d1 = dateTimePicker1.Value;
                d2 = dateTimePicker2.Value;

            }
            ///  جلب كشاف حساب فرعي بجميع العملات اجمالي
              if (IDYType == 1 && checkBox1.Checked && IDTypeAccontPrime==2 && IdAllAcount==2)//نوع الحساب اجمالي ,وكافة العملات
            {
                dataGridView1.DataSource = Acn.GetBalanceAccountALLCunncy((int)comboBox1.SelectedValue);


            }// جلب كشف حساب فرعي بعملة واحدة اجمالي 
          else if (IDYType == 1 && checkBox1.Checked == false && IDTypeAccontPrime == 2 && IdAllAcount == 2)//نوع الحساب اجمالي وعملة محددة
            {
                int IDcurrncy = (int)comboBox4.SelectedValue;

                 dataGridView1.DataSource= Acn.GetBalanceAccount((int)comboBox1.SelectedValue, IDcurrncy,comboBox4.Text);
            }
           ///   جلب كشف حسابات جميع الحسابات الفرعية بجميع العملات اجمالي
           else if(IDYType == 1 && checkBox1.Checked  && IDTypeAccontPrime == 2 && IdAllAcount == 1)
            {
              dataGridView1.DataSource = Acn.GetBalanceALLAccountALLCunncy(-1);
            }
           // جلب جميع الحسابات الفرعية بعملة واحدة اجمالي
          else if(IDYType == 1 && checkBox1.Checked == false && IDTypeAccontPrime == 2 && IdAllAcount == 1)
            {
                int IDcurrncy = (int)comboBox4.SelectedValue;
               dataGridView1.DataSource = Acn.GetBalanceALLAccountALLCunncy(IDcurrncy);
            } 
              // جلب كشف حساب فرعي تفصليلي عملة واحدة
            else if (IDYType == 2 && checkBox1.Checked == false && IDTypeAccontPrime == 2 && IdAllAcount == 2)//نوع الحساب تفصيلي وعملة محددة
            {
                int IDcurrncy = (int)comboBox4.SelectedValue;

                dataGridView1.DataSource = Acn.GETAccountDitalis((int)comboBox1.SelectedValue, IDcurrncy,d1,d2);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex == 4)
            {
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
            }
            else if (((ComboBox)sender).SelectedIndex == 1)
            {
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = true;
                label2.Visible = false;
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
    }
}
