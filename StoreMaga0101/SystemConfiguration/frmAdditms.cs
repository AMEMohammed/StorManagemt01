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
        int IdGroup;
        Dictionary<string, int> ItemsSeleced = new Dictionary<string, int>();
        public frmAdditms(int idgroup)
        {
            InitializeComponent();
            try
            {
                config = new Config(@".\s2008", "StoreManagement1", null, null);


                Type1 = 1;
                IdGroup = idgroup;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /////
        public frmAdditms(int Type,int idgroup, string ServerNm,string DbNm,string UserSql,string PassSql)
        {
            InitializeComponent();
            config = new Config(ServerNm, DbNm, UserSql, PassSql);
          
            Type1 = Type;
            IdGroup = idgroup;

        }
        private void frmAdditms_Load(object sender, EventArgs e)
        {
            try
            {
               // comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
               // comboBox1.AutoCompleteMode = AutoCompleteMode.Append;
              //  comboBox1.AutoCompleteSource = AutoCompleteSource.None;
          
               /////// if type is Accountes
                if (Type1 == 1)
                {
                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    /// get accountes is no group
                    dt1= config.GetAllAccountSubNoInGroup(IdGroup);
                    /// get Accountes in group
                    dt2 = config.GetAllAccountSubInGroup(IdGroup);
                    //
                    foreach(DataRow dr in dt1.Rows )
                    {
                        comboBox1.Items.Add(dr["اسم الحساب"]);
                    }
                    //
                    foreach (DataRow dr in dt2.Rows)
                    {
                        comboBox2.Items.Add(dr["اسم الحساب"]);
                    }
                    //
                    comboBox1.SelectedIndex = 0;

                    //comboBox1.DataSource = config.GetAllAccountSubNoInGroup(IdGroup);
                    //comboBox1.DisplayMember = "اسم الحساب";
                    //comboBox1.ValueMember = "رقم الحساب";
                    //comboBox2.DataSource = config.GetAllAccountSubInGroup(IdGroup);
                    //comboBox2.DisplayMember = "اسم الحساب";
                    //comboBox2.ValueMember = "رقم الحساب";
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
        ////////
        ///////
        /// Move One Item To Left
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if ( comboBox1.SelectedIndex >= 0)
                {

                    comboBox2.Items.Add(comboBox1.GetItemText(comboBox1.SelectedItem));// add item to left

                    comboBox1.Items.Remove(comboBox1.SelectedItem); // Remove item
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         }
     

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        ////////
        //////
        /// Move All Item To left
        private void button2_Click(object sender, EventArgs e)
        {
           // try
            {
                if (comboBox1.Items.Count > 0)
                {
                    foreach (string Itm in comboBox1.Items)
                    {


                        comboBox2.Items.Add(Itm);
                        comboBox1.Items.Remove(Itm);

                    }

                }
            }
            //catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        ////////
        ///////
        /// Move One Item To Right
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                if (comboBox2.SelectedIndex > 0)
                {

                    comboBox1.Items.Add(comboBox2.GetItemText(comboBox1.SelectedItem));// add item to right

                    comboBox2.Items.Remove(comboBox2.SelectedItem); // Remove item
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /////
        // move All item to Right
        private void button4_Click(object sender, EventArgs e)
        {
           // try
            {
                //if (comboBox2.Items.Count > 0)
                {
                    foreach (string Itm in comboBox2.Items)
                    {
                        comboBox1.Items.Add(Itm);
                        comboBox2.Items.Remove(Itm);
                    }
                }
            }
          //  catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
    }
}
