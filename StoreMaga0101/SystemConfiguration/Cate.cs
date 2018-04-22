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
    public partial class Cate : Form
    {
        Config config;
        /// <summary>
        /// /counstpcort
        /// </summary>
        public Cate()
        {
            InitializeComponent();
            try
            {
                config = new Config(@".\s2008", "StoreManagement1", null, null);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /////
        /// Cunst With Connction sql server
        public Cate(string ServerNm,string DbNm,string UserSql,string PassSql)
        {
            InitializeComponent();
            try
            {
                config = new Config(ServerNm, DbNm,UserSql, PassSql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //
        // Evset Load Frm
        private void Cate_Load(object sender, EventArgs e)
        {
            try
            {
                combAccont.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                combAccont.AutoCompleteSource = AutoCompleteSource.ListItems;
                textBox4.Focus();
                dataGridView1.DataSource = config.GetAllCategoryAR();
                combAccont.DataSource = config.GETALLAccountSub();
                combAccont.ValueMember = "رقم الحساب";
                combAccont.DisplayMember = "اسم الحساب";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        //
        // btn add New Cate
        private void btnAddSup_Click(object sender, EventArgs e)
        {
            if(textBox4.Text.Length>0 &&(int) combAccont.SelectedValue>0)
            {
                try
                {
                    
                    if (checkBox1.Checked && (int)combAccont.SelectedValue > 0)
                    {
                        config.AddNewCategory(textBox4.Text,combAccont.SelectedValue);//add in tbl Category

                    }
                    else
                    {
                        config.AddNewCategory(textBox4.Text, null);
                    }
                     dataGridView1.DataSource = config.GetAllCategoryAR();
                    
                    textBox4.Text = "";
                    textBox4.Focus();

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("يرجى تعبة جميع البيانات");
            }
        }
        /// <summary>
        /// تعديل
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefrsh_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Length > 0 && (int) combAccont.SelectedValue>0)
            {
                try
                { if (MessageBox.Show("هل تريد التعديل", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (checkBox1.Checked && (int)combAccont.SelectedValue > 0)
                        {
                            config.UpdateCategory(Convert.ToInt32(textBox3.Text), textBox4.Text, (int)combAccont.SelectedValue);
                        }
                        else
                        {
                          
                            config.UpdateCategory(Convert.ToInt32(textBox3.Text), textBox4.Text, null);

                        }

                    }
                    dataGridView1.DataSource = config.GetAllCategoryAR();
                    textBox4.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //
        // btn Delete cate
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length > 0)
            {
                try
                {
                    if (MessageBox.Show("هل تريد حذف الصنف", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    { DataTable dt = new DataTable();
                        int IDCAT = Convert.ToInt32(textBox3.Text);
                        dt = config.chackCatagory(IDCAT);
                        if(dt.Rows.Count>0)
                        {
                            MessageBox.Show("لايكمن حذف السجل .مرتبط بسجلات اخرى", "", MessageBoxButtons.OK);
                        }
                        else
                        {
                            config.DeleteCategory(IDCAT);

                          
                            dataGridView1.DataSource = config.GetAllCategoryAR();
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
        //
        // Btn Close Form
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //
        // Date gride Chosse
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count>0)
            {
                try
                {
                    textBox3.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    textBox4.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    checkBox1.Checked = false;
                    combAccont.Enabled = false;
                    combAccont.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

      
        //
        // change CheckBox
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                combAccont.Enabled = true;
            else
                combAccont.Enabled = false;

        }
        //
        // btn refirsh 
        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox4.Text = "";
            combAccont.DataSource = config.GETALLAccountSub();

        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Enter)
            {
                dataGridView1.DataSource = config.GetCategoryByName(textBox4.Text);
            }
        }
    }
}
