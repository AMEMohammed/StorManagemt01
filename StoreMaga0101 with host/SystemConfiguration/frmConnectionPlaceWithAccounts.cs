using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemConfiguration
{
    public partial class frmConnectionPlaceWithAccounts : Form
    {
        Config config;
        public frmConnectionPlaceWithAccounts()
        {
            InitializeComponent();
            config= new Config(@".\s2008", "StoreManagement1", null, null);
            GetDate();
        }
        public frmConnectionPlaceWithAccounts(string Serv,string DBNm,string UserSql,string PassSql)
        {
            InitializeComponent();
            try
            {
                config = new Config(Serv, DBNm, UserSql, PassSql);
                GetDate();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmConnectionPlaceWithAccounts_Load(object sender, EventArgs e)
        {
            combAccountIDDaan.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            combAccountIDDaan.AutoCompleteSource = AutoCompleteSource.ListItems;
            combAccountIDMAdden.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            combAccountIDMAdden.AutoCompleteSource = AutoCompleteSource.ListItems;
            combPalce.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            combPalce.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        //////
        //GetDate
        void GetDate()
        {
            combPalce.DataSource = config.GetAllPlace();
            combPalce.ValueMember = "رقم الجهة";
            combPalce.DisplayMember = "اسم الجهة";
            combAccountIDMAdden.DataSource = config.GETALLAccountSub();
            combAccountIDMAdden.ValueMember = "رقم الحساب";
            combAccountIDMAdden.DisplayMember = "اسم الحساب";
            combAccountIDDaan.DataSource = config.GETALLAccountSub();
            combAccountIDDaan.ValueMember = "رقم الحساب";
            combAccountIDDaan.DisplayMember = "اسم الحساب";

        }
        // btn ADD
        private void btnAddSup_Click(object sender, EventArgs e)
        {
            try
            {

                if ((int)combAccountIDDaan.SelectedValue > 0 && (int)combAccountIDMAdden.SelectedValue > 0 && (int)combPalce.SelectedValue > 0)
                {
                    if (MessageBox.Show("هل تريد الاضافة ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        config.AddConnectionAccountwithPlace((int)combPalce.SelectedValue, (int)combAccountIDMAdden.SelectedValue, (int)combAccountIDDaan.SelectedValue);
                        dataGridView1.DataSource = config.GetConnectionAccountwithPlace();
                        GetDate();
                    }
                }
                else
                {
                    MessageBox.Show("يجب اختيار الجهة والحساب الدائن والحسساب المدين");
                }
            }
            catch
            {
                MessageBox.Show("يجب اختيار الجهة والحساب الدائن والحسساب المدين");

            }
        }
        /// <summary>
        /// btn update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefrsh_Click(object sender, EventArgs e)
        {
            try
            {
                if ((int)combAccountIDDaan.SelectedValue > 0 && (int)combAccountIDMAdden.SelectedValue > 0 && (int)combPalce.SelectedValue > 0 && txtNumber.Text != "")
                {
                    if (MessageBox.Show("هل تريد التعديل", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        config.UpdateConnectionAccountwithPlace(Convert.ToInt32(txtNumber.Text), (int)combPalce.SelectedValue, (int)combAccountIDMAdden.SelectedValue, (int)combAccountIDDaan.SelectedValue);
                    dataGridView1.DataSource = config.GetConnectionAccountwithPlace();
                }
                else
                {

                }
            }
            catch
            {

            }

        }
        //btn Delete
        private void btnDele_Click(object sender, EventArgs e)
        {try
            {
                if (txtNumber.Text != "")
                {
                    if (MessageBox.Show("هل تريد الحذف ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        config.DeleteConnectionAccountwithPlace(Convert.ToInt32(txtNumber.Text));
                    dataGridView1.DataSource = config.GetConnectionAccountwithPlace();
                }
            }
            catch
            {

            }
        }
        // btn  refeish
        private void btnRefrish_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = config.GetConnectionAccountwithPlace();
            GetDate();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)

            {
                try
                {
                    txtNumber.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    combPalce.SelectedValue = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    combAccountIDDaan.SelectedValue = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    combAccountIDMAdden.SelectedValue = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                }
                catch
                {

                }

            }
        }
    }
}
