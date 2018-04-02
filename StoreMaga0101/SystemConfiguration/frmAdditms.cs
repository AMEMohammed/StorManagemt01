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
        //// event Loading
        private void frmAdditms_Load(object sender, EventArgs e)
        {
            try
            {                         
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

                 
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

       
        //////
        /// btn Close 
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
     

       
        ////////
        //////
        /// Move All Item To left
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Items.Count > 0)
                {
                    int count = comboBox1.Items.Count;

                    for (int i=0;i<count;i++)
                    {
                        comboBox2.Items.Add(comboBox1.Items[i].ToString());
                     
                    }
                    for (int i = 0; i < count; i++)
                    {
                       
                        comboBox1.Items.Remove(comboBox1.Items[0]);
                    }


                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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

                    comboBox1.Items.Add(comboBox2.GetItemText(comboBox2.SelectedItem));// add item to right

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
            try
            {
                if (comboBox2.Items.Count > 0)
                {
                    int count = comboBox2.Items.Count;

                    for (int i = 0; i < count; i++)
                    {
                        comboBox1.Items.Add(comboBox2.Items[i].ToString());

                    }
                    for (int i = 0; i < count; i++)
                    {

                        comboBox2.Items.Remove(comboBox2.Items[0]);
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
