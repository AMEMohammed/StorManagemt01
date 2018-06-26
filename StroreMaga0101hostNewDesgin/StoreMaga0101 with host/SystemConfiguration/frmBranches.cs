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
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace SystemConfiguration
{
    public partial class frmBranches : Form
    {
        int UserID;
        bool HostConnction;
        string IPHost;
        Config config;
        int IDBranchEnter;
        int IDbranch;
        public frmBranches()
        {
            InitializeComponent();
        }
        public frmBranches(string ServerNm, string DBNm, string UserSql, string PassSql, int UserId, bool hostconectopn, string iphost,int idbranch)
        {
            InitializeComponent();
            try
            {
                UserID = UserId;
                HostConnction = hostconectopn;
                IPHost = iphost;
                IDBranchEnter = idbranch;
                if(HostConnction==false)
                {
                   config = new Config(ServerNm, DBNm, UserSql, PassSql);
                   
                }
                else
                {

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UserID = UserId;

        }
        ///
       
        /// Loading Form
        private void frmBranches_Load(object sender, EventArgs e)
        {
            GetAccountsMain();

        }
        /// btn Add Barnch
        private void btnAddSup_Click(object sender, EventArgs e)
        {
            if ((int)comboAccount.SelectedValue > 0 && textName.Text.Length > 0 && textNumIDACOunt.Text.Length > 0)
            {if (MessageBox.Show("هل تريد اضافة فرع جديد", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {  /// الحصول على رقم الفرع
                    int thenumber = 0;
                    try
                    {  if (HostConnction == false)
                        {
                            thenumber = config.GETMaxNumberBarnch() + 1;
                        }
                        else
                        {

                        }
                    }
                    catch
                    {
                        thenumber = 1;
                    }

                    //// يجب ان يكون الفرع الاول هو الاداره
                    if (HostConnction == false)
                    {
                        config.AddNewBranch(thenumber, textName.Text, Convert.ToInt32(textNumIDACOunt.Text), textNote.Text, textNameEn.Text, textPhone.Text, textfax.Text, textAddress.Text, UserID, IDBranchEnter);
                        dataGridView1.DataSource = config.GetAllBarnch();
                        Clean();
                    }
                    else
                    {

                    }
                    
                }
            }
        }
        
        /// comb account SelectIndex
        private void comboAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
                textNumIDACOunt.Text = Convert.ToInt32(comboAccount.SelectedValue).ToString();
            }
            catch
            {
               
            }
        }
        /// getaCCountMain
        void GetAccountsMain()
        {
            comboAccount.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboAccount.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboAccount.DisplayMember = "اسم الحساب";
            comboAccount.ValueMember = "رقم الحساب";
            if (HostConnction == false)
            {
                comboAccount.DataSource = config.GetAccountsMain();
            }
            else
            {

            }
        }
        void Clean()
        {
            textName.Text = "";
            textNameEn.Text = "";
            textNote.Text = "";
            textNumIDACOunt.Text = "";
            textID.Text = "";
            textPhone.Text = "";
            textfax.Text = "";
         
        }
        ///// btn refrish
        private void btnRefrish_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = config.GetAllBarnch();
             
            }
            catch
            {

            }
        }
        // اختيار سطر من datafride
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)
            {
                try
                {
                    int number = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    IDbranch = config.GetIdBranchFromNumber(number);
                    textID.Text = number.ToString();
                    textName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    textNameEn.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();

                    comboAccount.Text = "";
                    comboAccount.SelectedText = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                    textPhone.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                    textfax.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                    textAddress.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                    textNote.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                }
                catch
                {

                }


            }
        }
    }
    }
    

