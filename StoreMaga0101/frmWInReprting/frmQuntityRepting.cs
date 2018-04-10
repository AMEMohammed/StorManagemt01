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
namespace frmWInReprting
{
    public partial class frmQuntityRepting : Form
    {
        RepotFunction rf;
        int UserID;
        public frmQuntityRepting()
        {
            InitializeComponent();
            try
            {
                UserID = 1;
                rf = new RepotFunction(@".\s2008", "StoreManagement1", null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public frmQuntityRepting(string ServerNm, string DbNm, string UserSql, string PassSql, int Userid)
        {
            InitializeComponent();
            try
            {

                rf = new RepotFunction(ServerNm, DbNm, UserSql, PassSql);
                UserID = Userid;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void frmQuntityRepting_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
                CombboxGroup.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                CombboxGroup.AutoCompleteSource = AutoCompleteSource.ListItems;
                GetData1();
                MessageBoxManager.Yes = "نعم";
                MessageBoxManager.No = "الغاء";
                MessageBoxManager.Register();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// //
        /// </summary>
        void GetData1()
        {
            try
            {
                comboBox1.DisplayMember = "اسم الصنف";
                comboBox1.ValueMember = "رقم الصنف";
                comboBox1.DataSource = rf.GetAllCategoryAR();
                comboBox2.DisplayMember = "اسم النوع";
                comboBox2.ValueMember = "رقم النوع";
                comboBox2.DataSource = rf.GetAllTypeQuntity();
                comboBox3.DisplayMember = "اسم العملة";
                comboBox3.ValueMember = "رقم العملة";
                comboBox3.DataSource = rf.GetAllCurrency();
                CombboxGroup.DisplayMember = "اسم المجموعة";
                CombboxGroup.ValueMember = "رقم المجموعة";
                CombboxGroup.DataSource = rf.GetGroupsCate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddSup_Click(object sender, EventArgs e)
        { try
            {
                this.Cursor = Cursors.WaitCursor;
                if (checkBox1.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false)
                {
                    dataGridView1.DataSource = rf.PrintAccountQuntity(Convert.ToInt32(comboBox1.SelectedValue), Convert.ToInt32(comboBox2.SelectedValue), Convert.ToInt32(comboBox3.SelectedValue));
                }
                else if (checkBox1.Checked == false)
                {
                    dataGridView1.DataSource = rf.PrintAccountQuntityIDac(Convert.ToInt32(comboBox1.SelectedValue));

                }
                else if(checkBox2.Checked) // select data from Group
                {
                    //MessageBox.Show(CombboxGroup.SelectedValue.ToString());
                    if ((int)CombboxGroup.SelectedValue > 0)
                    {
                        rf.PrintAccountQuntityWithGroup((int)CombboxGroup.SelectedValue);
                    }

                }
                else
                {
                    dataGridView1.DataSource = rf.PrintAccountQuntityAll();
                }
                this.Cursor = Cursors.Default;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefrsh_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {

                DataTable dt = new DataTable();
                try
                {

                    ////////// اضافة الاعمدة 
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dt.Columns.Add(dataGridView1.Columns[i].HeaderText);


                    }
                    dt.Columns.Add("اسم الموظفف");
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }

                ///////////////////  أاضافة سطور 
                try
                {
                    foreach (DataGridViewRow dgr in dataGridView1.SelectedRows)
                    {

                        DataRow dr = ((DataRowView)dgr.DataBoundItem).Row;
                        string nmca = dr[0].ToString();
                        string nmty = dr[1].ToString();
                        int qunt = Convert.ToInt32(dr[2].ToString());
                        int pres = Convert.ToInt32(dr[3].ToString());
                        string currnt = dr[4].ToString();
                        string nameUser = rf.GetUserNameBYIdUser(UserID);
                        dt.Rows.Add(nmca, nmty, string.Format("{0:##,##}", qunt), string.Format("{0:##,##}", pres), currnt, nameUser);
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    frmReprt frmrep = new frmReprt(dt, null, 5);
                    frmrep.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {

                DataTable dt = new DataTable();
                try
                {

                    ////////// اضافة الاعمدة 
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dt.Columns.Add(dataGridView1.Columns[i].HeaderText);


                    }
                    dt.Columns.Add("اسم الموظفف");
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }

                ///////////////////  أاضافة سطور 
                try
                {
                    foreach (DataGridViewRow dgr in dataGridView1.Rows)
                    {

                        DataRow dr = ((DataRowView)dgr.DataBoundItem).Row;
                        string nmca = dr[0].ToString();
                        string nmty = dr[1].ToString();
                        int qunt = Convert.ToInt32(dr[2].ToString());
                        int pres = Convert.ToInt32(dr[3].ToString());
                        string currnt = dr[4].ToString();
                        string nameUser = rf.GetUserNameBYIdUser(UserID);
                        dt.Rows.Add(nmca, nmty, string.Format("{0:##,##}", qunt), string.Format("{0:##,##}", pres), currnt, nameUser);
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    frmReprt frmrep = new frmReprt(dt, null, 5);
                    frmrep.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }   
        }


        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                comboBox2.Enabled = false;
            }
            else

            {
                comboBox2.Enabled = true;
            }
        }



        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                comboBox3.Enabled = false;
            }
            else

            {
                comboBox3.Enabled = true;
            }
        }
       

      
        

        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                comboBox1.Enabled = false;
            }
            else

            {
                comboBox1.Enabled = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                CombboxGroup.Enabled = true;


            }
            else
            {
                CombboxGroup.Enabled = false;
            }

        }
    }
}
