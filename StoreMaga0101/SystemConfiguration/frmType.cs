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
    public partial class frmType : Form
    {
        Config config;
        public frmType()
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
        public frmType(string ServerNm, string DbNm, string UserSql, string PassSql)
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

        private void frmType_Load(object sender, EventArgs e)
        {
            try
            {
                textBox3.Focus();
                dataGridView1.DataSource = config.GetAllTypeQuntity();
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
                    config.AddNewTypeQuntity(textBox4.Text);
                    dataGridView1.DataSource = config.GetAllTypeQuntity();
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
                        config.UpdateTypeQuntity(Convert.ToInt32(textBox3.Text), textBox4.Text);


                    dataGridView1.DataSource = config.GetAllTypeQuntity();
                    textBox4.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    if (MessageBox.Show("هل تريد حذف النوع", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DataTable dt = new DataTable();
                        int IDtype = Convert.ToInt32(textBox3.Text);
                        dt = config.chackTapy(IDtype);
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("لايكمن حذف السجل .مرتبط بسجلات اخرى", "", MessageBoxButtons.OK);
                        }
                        else
                        {
                            config.DeleteQuntity(IDtype);


                            dataGridView1.DataSource = config.GetAllTypeQuntity();
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
    }
}