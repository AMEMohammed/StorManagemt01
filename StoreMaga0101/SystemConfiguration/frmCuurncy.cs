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
    public partial class frmCuurncy : Form
    {
        Config config;
        public frmCuurncy()
        {
            InitializeComponent();
            try
            {
                config = new Config(@".\s2008", "StoreManagement1", null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public frmCuurncy(string ServerNm, string DbNm, string UserSql, string PassSql)
        {
            InitializeComponent();
            try
            {
                config = new Config(ServerNm, DbNm, UserSql, PassSql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// /////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCuurncy_Load(object sender, EventArgs e)
        {
            try
            {
                textBox4.Focus();
                dataGridView1.DataSource = config.GetAllCurrency();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddSup_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Length > 0)
            {
                try
                {
                    config.AddNewCurrency(textBox4.Text);
                    dataGridView1.DataSource = config.GetAllCurrency();
                    textBox4.Text = "";
                    textBox4.Focus();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnRefrsh_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Length > 0)
            {
                try
                {
                    if (MessageBox.Show("هل تريد التعديل", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        config.UpdateCurrency(Convert.ToInt32(textBox3.Text), textBox4.Text);


                    dataGridView1.DataSource = config.GetAllCurrency();
                    textBox4.Focus();
                    textBox4.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length > 0)
            {
                try
                {
                    if (MessageBox.Show("هل تريد حذف العملة", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DataTable dt = new DataTable();
                        int IDcur = Convert.ToInt32(textBox3.Text);
                        dt = config.chackCurncy(IDcur);
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("لايكمن حذف السجل .مرتبط بسجلات اخرى", "", MessageBoxButtons.OK);
                        }
                        else
                        {
                            config.DeleteCurrency(IDcur);


                            dataGridView1.DataSource = config.GetAllCurrency();
                            textBox4.Focus();
                            textBox4.Text = "";
                            textBox3.Text = "";
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    textBox3.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    textBox4.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();


                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.Message);
                }
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Shift && e.KeyCode==Keys.Enter)
            {
                dataGridView1.DataSource = config.GETCurrencyBYName(textBox4.Text);
            }
        }
    }
}