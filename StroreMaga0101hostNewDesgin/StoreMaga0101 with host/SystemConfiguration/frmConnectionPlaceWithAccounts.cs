using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SystemConfiguration
{
    public partial class frmConnectionPlaceWithAccounts : Form
    {
        Config config;
        ServiceReference1.IserviceClient configHost;
        bool HostConection;
        public frmConnectionPlaceWithAccounts()
        {
            InitializeComponent();
            config= new Config(@".\s2008", "StoreManagement1", null, null);
            GetDate();
        }
        public frmConnectionPlaceWithAccounts(string Serv,string DBNm,string UserSql,string PassSql,bool hostconnection,string iphost)
        {
            InitializeComponent();
            try
            {
                HostConection = hostconnection;
                if (HostConection == false)
                {
                    config = new Config(Serv, DBNm, UserSql, PassSql);

                }
                else
                {
                    configHost = new ServiceReference1.IserviceClient();
                    EndpointAddress endp = new EndpointAddress(iphost);
                    configHost.Endpoint.Address = endp;

                }
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
            
            combPalce.ValueMember = "رقم الجهة";
            combPalce.DisplayMember = "اسم الجهة";        
            combAccountIDMAdden.ValueMember = "رقم الحساب";
            combAccountIDMAdden.DisplayMember = "اسم الحساب";
            combAccountIDDaan.ValueMember = "رقم الحساب";
            combAccountIDDaan.DisplayMember = "اسم الحساب";
            if (HostConection == false)
            {
                combPalce.DataSource = config.GetAllPlace();
                combAccountIDMAdden.DataSource = config.GETALLAccountSub();
                combAccountIDDaan.DataSource = config.GETALLAccountSub();
            }
            else
            {
                combPalce.DataSource =ConvertMemorytoDB(configHost.GetAllPlace());
                combAccountIDMAdden.DataSource =ConvertMemorytoDB( configHost.GETALLAccountSub());
                combAccountIDDaan.DataSource =ConvertMemorytoDB( configHost.GETALLAccountSub());
            }

        }
        // btn ADD
        private void btnAddSup_Click(object sender, EventArgs e)
        {
            try
            {

                if ((int)combAccountIDDaan.SelectedValue > 0 && (int)combAccountIDMAdden.SelectedValue > 0 && (int)combPalce.SelectedValue > 0)
                {
                    if (MessageBox.Show("هل تريد الاضافة ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {  if (HostConection == false)
                        {
                            config.AddConnectionAccountwithPlace((int)combPalce.SelectedValue, (int)combAccountIDMAdden.SelectedValue, (int)combAccountIDDaan.SelectedValue);
                           // dataGridView1.DataSource = config.GetConnectionAccountwithPlace();
                        }
                        else
                        {
                            configHost.AddConnectionAccountwithPlace((int)combPalce.SelectedValue, (int)combAccountIDMAdden.SelectedValue, (int)combAccountIDDaan.SelectedValue);
                         //   dataGridView1.DataSource =ConvertMemorytoDB( configHost.GetConnectionAccountwithPlace());
                        }
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
                {if (HostConection == false)
                    {
                        if (MessageBox.Show("هل تريد التعديل", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            config.UpdateConnectionAccountwithPlace(Convert.ToInt32(txtNumber.Text), (int)combPalce.SelectedValue, (int)combAccountIDMAdden.SelectedValue, (int)combAccountIDDaan.SelectedValue);
                        }

                        dataGridView1.DataSource = config.GetConnectionAccountwithPlace();
                    }
                else// connection host
                    {
                        if (MessageBox.Show("هل تريد التعديل", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            configHost.UpdateConnectionAccountwithPlace(Convert.ToInt32(txtNumber.Text), (int)combPalce.SelectedValue, (int)combAccountIDMAdden.SelectedValue, (int)combAccountIDDaan.SelectedValue);
                        }

                        dataGridView1.DataSource =ConvertMemorytoDB( configHost.GetConnectionAccountwithPlace());
                    }
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
                    if (HostConection == false)
                    {
                        if (MessageBox.Show("هل تريد الحذف ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            config.DeleteConnectionAccountwithPlace(Convert.ToInt32(txtNumber.Text));
                        dataGridView1.DataSource = config.GetConnectionAccountwithPlace();
                    }
                    else
                    {
                        if (MessageBox.Show("هل تريد الحذف ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            configHost.DeleteConnectionAccountwithPlace(Convert.ToInt32(txtNumber.Text));
                        dataGridView1.DataSource =ConvertMemorytoDB( configHost.GetConnectionAccountwithPlace());
                    }

                }
            }
            catch
            {

            }
        }
        // btn  refeish
        private void btnRefrish_Click(object sender, EventArgs e)
        { if (HostConection == false)
            {
                dataGridView1.DataSource = config.GetConnectionAccountwithPlace();
            }
            else
            {
                dataGridView1.DataSource =ConvertMemorytoDB( configHost.GetConnectionAccountwithPlace());

            }
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
        ///  //convert MemmoryToDB
        DataTable ConvertMemorytoDB(MemoryStream ms)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            ms.Seek(0, SeekOrigin.Begin);
            DataTable dt = (DataTable)formatter.Deserialize(ms);
            return dt;
        }

    }
}
