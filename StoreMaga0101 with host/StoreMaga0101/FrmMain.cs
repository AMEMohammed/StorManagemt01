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
using Out_;
using frmWInReprting;
using SystemConfiguration;
using Account;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace StoreMaga0101
{
    public partial class FrmMain : Form
    {
        int UserID;
      
        frmLogin frm;// Login from
        public FrmMain()
        {
            InitializeComponent();
            try
            {

                frm = new frmLogin(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        /// <summary>
        ///  AddSupply
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
             
                frmADDSup frmADdSup = new frmADDSup(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID);// from AddSupply

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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                frm.ShowDialog();
                if (frmLogin.GETIDD == -1)
                {
                    Application.Exit();
                }
                else
                {  
                    groupBox1.Visible = true;
                    DataTable dt = new DataTable();
                    if (!ConServer.ConnectionWithHost)//connection local
                    {
                        UsersSQl us = new UsersSQl(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql);
                        dt = us.GetUser(frmLogin.GETIDD);
                    }
                    else //connection Host
                    {

                        ServiceReference1.IserviceClient UsHost = new ServiceReference1.IserviceClient();
                        dt = ConvertMemorytoDB(UsHost.GetUser(frmLogin.GETIDD));


                    }
                   
                 
                    
                    UserID = Convert.ToInt32(dt.Rows[0][0].ToString());
                    toolStripMenuItem3.Visible = true;
                    toolStripMenuItem13.Visible = true;
                    toolStripMenuItem7.Visible = true;
                    toolStripMenuItem1.Visible = true;
                    toolStripMenuItem23.Visible = true;
                    toolStripMenuItem5.Visible = true;
                    toolStripMenuItem17.Visible = true;
                    toolStripMenuItem21.Visible = true;
                    toolStripMenuItem14.Visible = Convert.ToBoolean(dt.Rows[0][4].ToString());
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
                    toolStripMenuItem24.Visible = Convert.ToBoolean(dt.Rows[0][15].ToString()); // add cate
                    toolStripMenuItem25.Visible = Convert.ToBoolean(dt.Rows[0][16].ToString());
                    toolStripMenuItem26.Visible = Convert.ToBoolean(dt.Rows[0][17].ToString());
                    toolStripMenuItem22.Visible = Convert.ToBoolean(dt.Rows[0][17].ToString());
                    toolStripMenuItem27.Visible = Convert.ToBoolean(dt.Rows[0][18].ToString());
                    toolStripMenuItem28.Visible = Convert.ToBoolean(dt.Rows[0][19].ToString());
                    toolStripMenuItem35.Visible= Convert.ToBoolean(dt.Rows[0][15].ToString());

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        { 
          if(  MessageBox.Show("هل تريد الخروج", "تنبيه", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)

            {
                Application.Exit();
            }
          
        }
        /// <summary>
        /// update Supply
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmUpdatSupply frmUp = new frmUpdatSupply(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID); //from update Supply
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
                   }
        /// <summary>
        /// ///////// ADD Out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmAddOut frmou = new frmAddOut(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID); //from update Supply
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// //////// Add User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmAddUser frmAddUser = new frmAddUser(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID); //from update Supply
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
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            try
            {


                this.Cursor = Cursors.WaitCursor;
                frmAddUser frmAddUser = new frmAddUser(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID); //from update Supply
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
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmUpdateOut frmupdout = new frmUpdateOut(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID); //from update Supply
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
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmSuppRepring frmRepotingSuppply = new frmSuppRepring(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID); //from update Supply
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
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmOutRepting frmRepotingout = new frmOutRepting(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID); //from update Supply
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
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }
        /// <summary>
        /// //////// Repoting Quntity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmQuntityRepting frmRepotingQuntity = new frmQuntityRepting(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID);
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
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmUpdSupp frmRepotingSupp = new frmUpdSupp(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID);
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
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;

        }
        /// <summary>
        /// // repoting Update OUt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            try
            {


                this.Cursor = Cursors.WaitCursor;
                frmUpdOutcs frmRepotingUpdOut = new frmUpdOutcs(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID);
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
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Cate frmcat = new Cate(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql);
                FormCollection fromco = Application.OpenForms;
                bool foundFrom = false;
                foreach (Form frm in fromco)
                {
                    if (frm.Name == "Cate")
                    {
                        frm.Focus();

                        foundFrom = true;

                    }

                }
                if (foundFrom == false)
                {
                    frmcat.Show();
                }
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;
        }

        private void toolStripMenuItem25_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmType frmtype = new frmType(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql);
                FormCollection fromco = Application.OpenForms;
                bool foundFrom = false;
                foreach (Form frm in fromco)
                {
                    if (frm.Name == "frmType")
                    {
                        frm.Focus();

                        foundFrom = true;

                    }

                }
                if (foundFrom == false)
                {
                    frmtype.Show();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            this.Cursor = Cursors.Default;

        }
        //تهيئة الجهات
        private void toolStripMenuItem28_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmPlace frmplace = new frmPlace(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql);
                FormCollection fromco = Application.OpenForms;
                bool foundFrom = false;
                foreach (Form frm in fromco)
                {
                    if (frm.Name == "frmPlace")
                    {
                        frm.Focus();

                        foundFrom = true;

                    }

                }
                if (foundFrom == false)
                {
                    frmplace.Show();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;

        }

        private void toolStripMenuItem27_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmCuurncy frmCurrncy = new frmCuurncy(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql);
                FormCollection fromco = Application.OpenForms;
                bool foundFrom = false;
                foreach (Form frm in fromco)
                {
                    if (frm.Name == "frmCuurncy")
                    {
                        frm.Focus();

                        foundFrom = true;

                    }

                }
                if (foundFrom == false)
                {
                    frmCurrncy.Show();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {

        }
        //شجرة الحسابات
        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmAcount frmCurrncy = new frmAcount(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID);
                FormCollection fromco = Application.OpenForms;
                bool foundFrom = false;
                foreach (Form frm in fromco)
                {
                    if (frm.Name == "frmAcount")
                    {
                        frm.Focus();

                        foundFrom = true;

                    }

                }
                if (foundFrom == false)
                {
                    frmCurrncy.Show();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void toolStripMenuItem26_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmSearchAccountNM frmCurrncy = new frmSearchAccountNM(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID);
                FormCollection fromco = Application.OpenForms;
                bool foundFrom = false;
                foreach (Form frm in fromco)
                {
                    if (frm.Name == "frmSearchAccountNM")
                    {
                        frm.Focus();

                        foundFrom = true;

                    }

                }
                if (foundFrom == false)
                {
                    frmCurrncy.Show();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmChangPass frmCurrncy = new frmChangPass(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID);
                FormCollection fromco = Application.OpenForms;
                bool foundFrom = false;
                foreach (Form frm in fromco)
                {
                    if (frm.Name == "frmChangPass")
                    {
                        frm.Focus();

                        foundFrom = true;

                    }

                }
                if (foundFrom == false)
                {
                    frmCurrncy.Show();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

      
        private void toolStripMenuItem33_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmGroup frmgroup = new frmGroup(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID);
                FormCollection fromco = Application.OpenForms;
                bool foundFrom = false;
                foreach (Form frm in fromco)
                {
                    if (frm.Name == "frmGroupr")
                    {
                        frm.Focus();

                        foundFrom = true;

                    }

                }
                if (foundFrom == false)
                {
                    frmgroup.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void toolStripMenuItem34_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                SimpleConstraint frmgroup = new SimpleConstraint(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID);
                FormCollection fromco = Application.OpenForms;
                bool foundFrom = false;
                foreach (Form frm in fromco)
                {
                    if (frm.Name == "SimpleConstraint")
                    {
                        frm.Focus();

                        foundFrom = true;

                    }

                }
                if (foundFrom == false)
                {
                    frmgroup.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }
        // الحسابات والجهات
        private void toolStripMenuItem35_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmConnectionPlaceWithAccounts frmConnectionPlaceWithAccounts1 = new frmConnectionPlaceWithAccounts(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql);
                FormCollection fromco = Application.OpenForms;
                bool foundFrom = false;
                foreach (Form frm in fromco)
                {
                    if (frm.Name == "frmConnectionPlaceWithAccounts")
                    {
                        frm.Focus();

                        foundFrom = true;

                    }

                }
                if (foundFrom == false)
                {
                    frmConnectionPlaceWithAccounts1.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }
        //convert MemmoryToDB
        DataTable ConvertMemorytoDB(MemoryStream ms)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            ms.Seek(0, SeekOrigin.Begin);
            DataTable dt = (DataTable)formatter.Deserialize(ms);
            return dt;
        }
    }
}
