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
    public partial class frmUpdOutcs : Form
    {
        RepotFunction rf;
        int UserID;
        public frmUpdOutcs()
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

        public frmUpdOutcs(string ServerNm, string DbNm, string UserSql, string PassSql, int Userid)
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
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex == 1)
            {
                label2.Visible = false;
                dateTimePicker1.Visible = false;
                label3.Visible = true;
                dateTimePicker2.Visible = true;
            }
            else if (comboBox5.SelectedIndex == 4)
            {
                label2.Visible = true;
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
                label3.Visible = true;
            }
            else
            {
                label2.Visible = false;
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                label3.Visible = false;
            }
        }

        private void frmUpdOutcs_Load(object sender, EventArgs e)
        {
            comboBox5.SelectedIndex = 0;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void btnAddSup_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                DateTime d1;
                DateTime d2;
                if (comboBox5.SelectedIndex == 0)
                {

                    d1 = DateTime.Now.AddDays(-1);
                    d2 = DateTime.Now;

                }
                else if (comboBox5.SelectedIndex == 1)
                {
                    d1 = Convert.ToDateTime("01/01/2016");
                    d2 = dateTimePicker2.Value;

                }
                else if (comboBox5.SelectedIndex == 2)
                {
                    d1 = DateTime.Now.AddDays(-7);
                    d2 = DateTime.Now;

                }
                else if (comboBox5.SelectedIndex == 3)
                {
                    d1 = DateTime.Now.AddDays(-30);
                    d2 = DateTime.Now;

                }
                else
                {
                    d1 = dateTimePicker1.Value;
                    d2 = dateTimePicker2.Value;

                }
                if (textBox4.Text.Length > 0)
                {
                    dataGridView1.DataSource = rf.GetUpdtOutByIDOut(Convert.ToInt32(textBox4.Text));
                }
                else if (radioButton1.Checked)

                {
                    dataGridView1.DataSource = rf.GetUpdOutByDate(d1, d2);
                }
                else if (radioButton2.Checked)
                {
                    dataGridView1.DataSource = rf.GetUpdOutByDateUpdtewithdate(d1, d2);

                }
                else
                {
                    dataGridView1.DataSource = rf.GetUpdOutByDateDetle2tewithdate(d1, d2);
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefrsh_Click(object sender, EventArgs e)
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
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                ///////////////////  أاضافة سطور 
                try
                {
                    foreach (DataGridViewRow dgr in dataGridView1.SelectedRows)
                    {
                        DataRow dr = ((DataRowView)dgr.DataBoundItem).Row;
                        int id = Convert.ToInt32(dr[0].ToString());
                        int idSu = Convert.ToInt32(dr[1].ToString());
                        string NCat = dr[2].ToString();
                        string TypeCA = dr[3].ToString();
                        int Qunit = Convert.ToInt32(dr[4].ToString());
                        int Price = Convert.ToInt32(dr[5].ToString());
                        string currn = dr[6].ToString();
                        string namee = dr[9].ToString();
                        DateTime dd = DateTime.Parse(dr[11].ToString());
                        string dec = dr[10].ToString();
                        string nameUser = rf.GetUserNameBYIdUser(UserID);

                        dt.Rows.Add(id, idSu, NCat, TypeCA, string.Format("{0:##,##}", Qunit), string.Format("{0:##,##}", Price), currn, " ", " ", namee, dec, dd.Date.ToShortDateString(), " ", nameUser);


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    frmReprt frmn = new frmReprt(dt, null, 7);
                    frmn.ShowDialog();
                 
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
            if (dataGridView1.RowCount > 0)
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
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                ///////////////////  أاضافة سطور 
                try
                {
                    foreach (DataGridViewRow dgr in dataGridView1.Rows)
                    {
                        DataRow dr = ((DataRowView)dgr.DataBoundItem).Row;
                        int id = Convert.ToInt32(dr[0].ToString());
                        int idSu = Convert.ToInt32(dr[1].ToString());
                        string NCat = dr[2].ToString();
                        string TypeCA = dr[3].ToString();
                        int Qunit = Convert.ToInt32(dr[4].ToString());
                        int Price = Convert.ToInt32(dr[5].ToString());
                        string currn = dr[6].ToString();
                        string namee = dr[9].ToString();
                        DateTime dd = DateTime.Parse(dr[11].ToString());
                        string dec = dr[10].ToString();
                        string nameUser =rf.GetUserNameBYIdUser(UserID);

                        dt.Rows.Add(id, idSu, NCat, TypeCA, string.Format("{0:##,##}", Qunit), string.Format("{0:##,##}", Price), currn, " ", " ", namee, dec, dd.Date.ToShortDateString(), " ", nameUser);


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                     frmReprt frmn = new frmReprt(dt, null, 7);
                    frmn.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
