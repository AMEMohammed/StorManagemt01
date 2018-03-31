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
    public partial class frmAdditms : Form
    {
        Config config;
        int Type1;
        Dictionary<string, int> ItemsSeleced = new Dictionary<string, int>();
        public frmAdditms()
        {
            InitializeComponent();
            try
            {
                config = new Config(@".\s2008", "StoreManagement1", null, null);


                Type1 = 1;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /////
        public frmAdditms(int Type,string ServerNm,string DbNm,string UserSql,string PassSql)
        {
            InitializeComponent();
            config = new Config(ServerNm, DbNm, UserSql, PassSql);
          
            Type1 = Type;

        }
        private void frmAdditms_Load(object sender, EventArgs e)
        {
            try
            {
               // comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
               // comboBox1.AutoCompleteMode = AutoCompleteMode.Append;
             //  comboBox1.AutoCompleteSource = AutoCompleteSource.None;
            foreach(var it in comboBox2.Items)
                {
                    
                }
                if (Type1 == 1)
                {
                    comboBox1.DataSource = config.GETALLAccountSub();
                    comboBox1.DisplayMember = "اسم الحساب";
                    comboBox1.ValueMember = "رقم الحساب";

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Add(new { Text = comboBox1.GetItemText(comboBox1.SelectedItem) ,Value=comboBox1.SelectedValue});
          //  comboBox2.ValueMember = "Value";
          //  comboBox2.DisplayMember = "Text";
            comboBox1.Items.RemoveAt(0);


           




        }
        private class ComboboxItem
        {
             public string Text { get; set; }
            public int Value { get; set; }
            public override string ToString()
            {
                return Text;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(comboBox2.SelectedValue.ToString());
        }
    }
}
