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
        int IDBranch;
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
                IDBranch = idbranch;
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
            {if (MessageBox.Show("هل تريد اضافة فرع جديد","",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (textID.Text.Length > 0)
                    {
                        if (MessageBox.Show("سيتم منح رقم تلقائي", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            config.AddNewBranch(Convert.ToInt32(textID.Text), textName.Text, Convert.ToInt32(textNumIDACOunt), textNote.Text, textNameEn.Text, textPhone.Text, textfax.Text, textAddress.Text, UserID, IDBranch);

                        }

                    }
                    else
                    {
                        config.AddNewBranch(Convert.ToInt32(textID.Text), textName.Text, Convert.ToInt32(textNumIDACOunt), textNote.Text, textNameEn.Text, textPhone.Text, textfax.Text, textAddress.Text, UserID, IDBranch);

                    }
                }
           
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
        /// comb account SelectIndex
        private void comboAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            textNumIDACOunt.Text = (string)comboAccount.SelectedValue;
        }
    }
}
