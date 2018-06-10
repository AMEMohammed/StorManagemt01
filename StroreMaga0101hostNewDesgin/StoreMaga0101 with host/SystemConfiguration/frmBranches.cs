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
        public frmBranches()
        {
            InitializeComponent();
        }
        public frmBranches(string ServerNm, string DBNm, string UserSql, string PassSql, int UserId, bool hostconectopn, string iphost)
        {
            InitializeComponent();
            try
            {
                UserID = UserId;
                HostConnction = hostconectopn;
                IPHost = iphost;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmBranches_Load(object sender, EventArgs e)
        {

        }

    }
}
