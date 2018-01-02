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
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            IDYType = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                IDYType = 2;
        }
    }
}
