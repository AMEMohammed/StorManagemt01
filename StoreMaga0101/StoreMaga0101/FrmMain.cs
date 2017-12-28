﻿using System;
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
using Out_;
using frmWInReprting;
namespace StoreMaga0101
{
    public partial class FrmMain : Form
    {
   
     
        frmLogin frm;// Login from
        public FrmMain()
        {
            InitializeComponent();
           
         
            frm= new frmLogin();


        }


        /// <summary>
        ///  AddSupply
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            frmADDSup frmADdSup = new frmADDSup();// from AddSupply
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
                groupBox1.Visible = true;
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
                toolStripMenuItem19.Visible = Convert.ToBoolean(dt.Rows[0][11].ToString());// add user
                toolStripMenuItem18.Visible = Convert.ToBoolean(dt.Rows[0][11].ToString());// update User
                toolStripMenuItem12.Visible = Convert.ToBoolean(dt.Rows[0][13].ToString());
                toolStripMenuItem16.Visible = Convert.ToBoolean(dt.Rows[0][14].ToString());
                toolStripMenuItem24.Visible = Convert.ToBoolean(dt.Rows[0][15].ToString());
                toolStripMenuItem25.Visible = Convert.ToBoolean(dt.Rows[0][16].ToString());
                toolStripMenuItem26.Visible = Convert.ToBoolean(dt.Rows[0][17].ToString());
                toolStripMenuItem27.Visible = Convert.ToBoolean(dt.Rows[0][18].ToString());
                toolStripMenuItem28.Visible = Convert.ToBoolean(dt.Rows[0][19].ToString());

            }
        }

        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// update Supply
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            frmUpdatSupply frmUp = new frmUpdatSupply(); //from update Supply
            FormCollection fromco = Application.OpenForms;
            bool foundFrom = false;
            foreach (Form frm in fromco)
            {
                if (frm.Name == "frmUpdatSupply")
                {
                    frm.Focus();

                    foundFrom = true;

                }

            }
            if (foundFrom == false)
            {
                frmUp.Show();
            }
            this.Cursor = Cursors.Default;

            
                   }
        /// <summary>
        /// ///////// ADD Out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
           frmAddOut frmou = new frmAddOut(); //from update Supply
            FormCollection fromco = Application.OpenForms;
            bool foundFrom = false;
            foreach (Form frm in fromco)
            {
                if (frm.Name == "frmAddOut")
                {
                    frm.Focus();

                    foundFrom = true;

                }

            }
            if (foundFrom == false)
            {
                frmou.Show();
            }
            this.Cursor = Cursors.Default;
        }
        /// <summary>
        /// //////// Add User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
           frmAddUser frmAddUser = new frmAddUser(); //from update Supply
            FormCollection fromco = Application.OpenForms;
            bool foundFrom = false;
            foreach (Form frm in fromco)
            {
                if (frm.Name == "frmAddUser")
                {
                    frm.Focus();

                    foundFrom = true;

                }

            }
            if (foundFrom == false)
            {
                frmAddUser.Show();
            }
            this.Cursor = Cursors.Default;
        }
        /// <summary>
        /// ///upadt user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            frmAddUser frmAddUser = new frmAddUser(); //from update Supply
            FormCollection fromco = Application.OpenForms;
            bool foundFrom = false;
            foreach (Form frm in fromco)
            {
                if (frm.Name == "frmAddUser")
                {
                    frm.Focus();

                    foundFrom = true;

                }

            }
            if (foundFrom == false)
            {
                frmAddUser.Show();
            }
            this.Cursor = Cursors.Default;
        }
        /// <summary>
        ///  upat out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            frmUpdateOut frmupdout = new frmUpdateOut(); //from update Supply
            FormCollection fromco = Application.OpenForms;
            bool foundFrom = false;
            foreach (Form frm in fromco)
            {
                if (frm.Name == "frmUpdateOut")
                {
                    frm.Focus();

                    foundFrom = true;

                }

            }
            if (foundFrom == false)
            {
                frmupdout.Show();
            }
            this.Cursor = Cursors.Default;
        }
        /// <summary>
        /// /////Repoting Supply
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            frmSuppRepring frmRepotingSuppply = new frmSuppRepring(); //from update Supply
            FormCollection fromco = Application.OpenForms;
            bool foundFrom = false;
            foreach (Form frm in fromco)
            {
                if (frm.Name == "frmSuppRepring")
                {
                    frm.Focus();

                    foundFrom = true;

                }

            }
            if (foundFrom == false)
            {
                frmRepotingSuppply.Show();
            }
            this.Cursor = Cursors.Default;
        }
        /// <summary>
        /// //Repoting OUt 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            frmOutRepting frmRepotingout = new frmOutRepting(); //from update Supply
            FormCollection fromco = Application.OpenForms;
            bool foundFrom = false;
            foreach (Form frm in fromco)
            {
                if (frm.Name == "frmOutRepting")
                {
                    frm.Focus();

                    foundFrom = true;

                }

            }
            if (foundFrom == false)
            {
                frmRepotingout.Show();
            }
            this.Cursor = Cursors.Default;
        }
        /// <summary>
        /// //////// Repoting Quntity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
            frmQuntityRepting frmRepotingQuntity = new frmQuntityRepting(); 
            FormCollection fromco = Application.OpenForms;
            bool foundFrom = false;
            foreach (Form frm in fromco)
            {
                if (frm.Name == "frmQuntityRepting")
                {
                    frm.Focus();

                    foundFrom = true;

                }

            }
            if (foundFrom == false)
            {
                frmRepotingQuntity.Show();
            }
            this.Cursor = Cursors.Default;
        }
        /// <summary>
        /// ///repoting Updta Supply
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            frmUpdSupp frmRepotingSupp = new frmUpdSupp();
            FormCollection fromco = Application.OpenForms;
            bool foundFrom = false;
            foreach (Form frm in fromco)
            {
                if (frm.Name == "frmUpdSupp")
                {
                    frm.Focus();

                    foundFrom = true;

                }

            }
            if (foundFrom == false)
            {
                frmRepotingSupp.Show();
            }
            this.Cursor = Cursors.Default;

        }
        /// <summary>
        /// // repoting Update OUt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            frmUpdOutcs frmRepotingUpdOut = new frmUpdOutcs();
            FormCollection fromco = Application.OpenForms;
            bool foundFrom = false;
            foreach (Form frm in fromco)
            {
                if (frm.Name == "frmUpdOutcs")
                {
                    frm.Focus();

                    foundFrom = true;

                }

            }
            if (foundFrom == false)
            {
                frmRepotingUpdOut.Show();
            }
            this.Cursor = Cursors.Default;
        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e)
        {

        }
    }
}