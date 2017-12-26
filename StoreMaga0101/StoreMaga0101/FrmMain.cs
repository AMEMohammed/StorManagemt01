using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Supplly;
using Users;
namespace StoreMaga0101
{
    public partial class FrmMain : Form
    {
        frmADDSup frmADdSup;
        frmLogin frm = new frmLogin();
        public FrmMain()
        {
            InitializeComponent();
            frmADdSup = new frmADDSup();
            
           

        } 

        
        /// <summary>
        ///  AddSupply
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            FormCollection fromco = Application.OpenForms;
            bool foundFrom = false;
            foreach (Form frm in fromco)
            {
                if (frm.Name == "frmADDSup")
                {
                    frm.Focus();

                    foundFrom = true;

                }

            }
            if (foundFrom == false)
            {
                frmADdSup.Show();
            }
            this.Cursor = Cursors.Default;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            frm.ShowDialog();
           if(frmLogin.GETIDD==-1)
            {
                Application.Exit();
            }
           else
            {
                UsersSQl us = new UsersSQl(@".\s2008", "StoreManagement1", null, null);
                DataTable dt = new DataTable();
                 dt=  us.GetUser(frmLogin.GETIDD);
                toolStripMenuItem3.Visible = true;
                toolStripMenuItem13.Visible = true;
                toolStripMenuItem7.Visible = true;
                toolStripMenuItem1.Visible = true;
                toolStripMenuItem23.Visible = true;
                toolStripMenuItem5.Visible = true;
                toolStripMenuItem17.Visible = true;

                toolStripMenuItem14.Visible  = Convert.ToBoolean(dt.Rows[0][4].ToString());
                toolStripMenuItem15.Visible = Convert.ToBoolean(dt.Rows[0][5].ToString());
                toolStripMenuItem8.Visible = Convert.ToBoolean(dt.Rows[0][6].ToString());
                toolStripMenuItem9.Visible = Convert.ToBoolean(dt.Rows[0][7].ToString());
                toolStripMenuItem2.Visible = Convert.ToBoolean(dt.Rows[0][8].ToString());
                toolStripMenuItem10.Visible = Convert.ToBoolean(dt.Rows[0][9].ToString());
                toolStripMenuItem11.Visible = Convert.ToBoolean(dt.Rows[0][10].ToString());
                toolStripMenuItem19.Visible = Convert.ToBoolean(dt.Rows[0][11].ToString());
                toolStripMenuItem18.Visible = Convert.ToBoolean(dt.Rows[0][12].ToString());
                toolStripMenuItem12.Visible = Convert.ToBoolean(dt.Rows[0][13].ToString());
                toolStripMenuItem16.Visible = Convert.ToBoolean(dt.Rows[0][14].ToString());
                toolStripMenuItem24.Visible = Convert.ToBoolean(dt.Rows[0][15].ToString());
                toolStripMenuItem25.Visible = Convert.ToBoolean(dt.Rows[0][16].ToString());
                toolStripMenuItem26.Visible = Convert.ToBoolean(dt.Rows[0][17].ToString());
                toolStripMenuItem27.Visible = Convert.ToBoolean(dt.Rows[0][18].ToString());
                toolStripMenuItem28.Visible = Convert.ToBoolean(dt.Rows[0][19].ToString());

            }
        }
    }
}
