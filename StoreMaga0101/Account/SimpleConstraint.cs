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

        }
    }
}
