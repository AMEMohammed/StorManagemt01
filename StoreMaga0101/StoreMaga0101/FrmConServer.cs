using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace StoreMaga0101
{
    public partial class FrmConServer : Form
    { 
        public FrmConServer()
        {
            InitializeComponent();
        }

        private void btnCHek_Click(object sender, EventArgs e)
        {
            if(txtServer.Text.Length>0 &&txtDB.Text.Length>0)
            { if (txtUser.Text.Length > 0 && txtpass.Text.Length > 0)
                {
                    SqlConnection sql = new SqlConnection(@"Data Source=" + txtServer.Text + ";Initial Catalog=" + txtDB.Text + ";User ID=" + txtUser.Text + ";Password=" + txtpass.Text);
                    }

            }
            else
            {
                MessageBox.Show("يجب كتابة اسم  السيرفر و اسم قاعدة البيانات", "", MessageBoxButtons.OK);
            }
        }

        private void FrmConServer_Load(object sender, EventArgs e)
        {

        }
    }
}
