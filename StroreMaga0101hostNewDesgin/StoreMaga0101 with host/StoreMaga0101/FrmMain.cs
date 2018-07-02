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
using System.ServiceModel;
namespace StoreMaga0101
{
    public partial class FrmMain : Form
    {

        //Drow X in PageTap
        private Point _imageLocation = new Point(13, 5);
        private Point _imgHitArea = new Point(13, 2);
        int UserID;
      

        frmLogin frm;// Login from
        ServiceReference1.IserviceClient serHost;
       

        public FrmMain()
        {  
            InitializeComponent();
           
            try
            {
                
                if (ConServer.ConnectionWithHost)
                {
                    serHost=new ServiceReference1.IserviceClient();
                    

                    EndpointAddress endp = new EndpointAddress(ConServer.HostIp);
                    serHost.Endpoint.Address = endp;
                    
                   
                }
                frm = new frmLogin(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, ConServer.HostIp);
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
        /// // اشافة طلب توريد
        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                bool isHere = false;
                frmADDSup frm = new frmADDSup(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID, ConServer.ConnectionWithHost, ConServer.HostIp);
                krbTabControl1.Visible = true;
                panel1.Visible = true;
                foreach (TabPage b in krbTabControl1.TabPages)
                {
                    if (b.Text.Trim() == ((string)frm.Tag).Trim())
                    {
                        isHere = true;
                        krbTabControl1.SelectedTab = b;
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                if (!isHere)
                {
                    TabPage newTabPage = new TabPage((string)frm.Tag);
                    newTabPage.ImageIndex = 0;
                    krbTabControl1.TabPages.Add(newTabPage);
                    Label newLabel = new Label();
                    newTabPage.Font = new Font("Tahoma", 8);
                    newLabel.Text = newTabPage.Text;
                    newTabPage.Controls.Add(newLabel);
                    krbTabControl1.SelectedTab = newTabPage;
                    frm.TopLevel = false;
                    frm.Parent = newTabPage;
                    frm.Visible = true;
                    frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
        }
           
           

           
       
        // loading
        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
               
             

                string NameUser;
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
                        NameUser = us.GetUserNameByID2(frmLogin.GETIDD);
                    }
                    else //connection Host
                    {

                      
                        dt = ConvertMemorytoDB(serHost.GetUser(frmLogin.GETIDD));
                        NameUser = serHost.GetUserNM(frmLogin.GETIDD);


                    }
                    toolStripStatusLabel1.Text = "اسم الموظف : " + NameUser;
                    if (ConServer.ConnectionWithHost)
                    {
                        toolStripStatusLabel2.Text = " عنوان المخدم: " + ConServer.iphost + ":" + ConServer.port;
                    }
                    else
                    {
                        toolStripStatusLabel2.Text = "قاعدة البيانات : " + ConServer.DBNM;
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
                   // tabControl1.Visible = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       

        
        //exite
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
        /// بحث وتعديل التوريد
        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                bool isHere = false;
                frmUpdatSupply frm = new frmUpdatSupply(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID, ConServer.ConnectionWithHost, ConServer.HostIp);
                krbTabControl1.Visible = true;
                panel1.Visible = true;
                foreach (TabPage b in krbTabControl1.TabPages)
                {
                    if (b.Text.Trim() == ((string)frm.Tag).Trim())
                    {
                        isHere = true;
                        krbTabControl1.SelectedTab = b;
                        return;
                    }
                }
                if (!isHere)
                {
                    TabPage newTabPage = new TabPage((string)frm.Tag);
                    newTabPage.ImageIndex = 1;
                    krbTabControl1.TabPages.Add(newTabPage);
                    Label newLabel = new Label();
                    newTabPage.Font = new Font("Tahoma", 8);
                    newLabel.Text = newTabPage.Text;
                    newTabPage.Controls.Add(newLabel);
                    krbTabControl1.SelectedTab = newTabPage;
                    frm.TopLevel = false;
                    frm.Parent = newTabPage;
                    frm.Visible = true;
                    frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
           


        }
        /// <summary>
        /// ///////// ADD Out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// اضافة امر صرف
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                bool isHere = false;
                frmAddOut frm = new frmAddOut(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID, ConServer.ConnectionWithHost, ConServer.HostIp);
                krbTabControl1.Visible = true;
                panel1.Visible = true;
                foreach (TabPage b in krbTabControl1.TabPages)
                {
                    if (b.Text.Trim() == ((string)frm.Tag).Trim())
                    {
                        isHere = true;
                        krbTabControl1.SelectedTab = b;
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                if (!isHere)
                {
                    TabPage newTabPage = new TabPage((string)frm.Tag);
                    newTabPage.ImageIndex = 0;
                    krbTabControl1.TabPages.Add(newTabPage);
                    Label newLabel = new Label();
                    newTabPage.Font = new Font("Tahoma", 8);
                    newLabel.Text = newTabPage.Text;
                    newTabPage.Controls.Add(newLabel);
                    krbTabControl1.SelectedTab = newTabPage;
                    frm.TopLevel = false;
                    frm.Parent = newTabPage;
                    frm.Visible = true;
                    frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// //////// Add User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // ااضافة يوزر جديد
        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                bool isHere = false;
                frmAddUser frm = new frmAddUser(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID, ConServer.HostIp,Application.StartupPath);
                krbTabControl1.Visible = true;

                panel1.Visible = true;
                foreach (TabPage b in krbTabControl1.TabPages)
                {
                    if (b.Text.Trim() == ((string)frm.Tag).Trim())
                    {
                        isHere = true;
                        this.Cursor = Cursors.Default;
                        krbTabControl1.SelectedTab = b;
                        return;
                    }
                }
                if (!isHere)
                {
                    TabPage newTabPage = new TabPage((string)frm.Tag);
                    newTabPage.ImageIndex = 11;
                    krbTabControl1.TabPages.Add(newTabPage);
                    Label newLabel = new Label();
                    newTabPage.Font = new Font("Tahoma", 8);
                    newLabel.Text = newTabPage.Text;
                    newTabPage.Controls.Add(newLabel);
                    krbTabControl1.SelectedTab = newTabPage;
                    frm.TopLevel = false;
                    frm.Parent = newTabPage;
                    frm.Visible = true;
                    frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
            //    try
            //    {
            //        this.Cursor = Cursors.WaitCursor;
            //        bool isHere = false;
            //        tabControl1.Visible = true; ;
            //        frmAddUser frmAddUser = new frmAddUser(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID, ConServer.HostIp);
            //        foreach (TabPage b in tabControl1.TabPages)
            //        {
            //            if (b.Text == (string)frmAddUser.Tag)
            //            {
            //                isHere = true;
            //                tabControl1.SelectedTab = b;
            //                this.Cursor = Cursors.Default;
            //                return;

            //            }
            //        }
            //        if (!isHere)
            //        {

            //            TabPage tab = new TabPage((string)frmAddUser.Tag);

            //            frmAddUser.TopLevel = false;

            //            frmAddUser.Parent = tab;

            //            frmAddUser.Visible = true;

            //            tabControl1.TabPages.Add(tab);

            //            frmAddUser.Location = new Point((tab.Width - frmAddUser.Width) / 2, (tab.Height - frmAddUser.Height) / 2);

            //            tabControl1.SelectedTab = tab;
            //            this.Cursor = Cursors.Default;
            //        }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //this.Cursor = Cursors.Default;

        }
        /// <summary>
        /// ///upadt user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// اضافة يوزر جديد
        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                bool isHere = false;
                frmAddUser frm = new frmAddUser(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID, ConServer.HostIp,Application.StartupPath);
                krbTabControl1.Visible = true;
                panel1.Visible = true;
                foreach (TabPage b in krbTabControl1.TabPages)
                {
                    if (b.Text.Trim() == ((string)frm.Tag).Trim())
                    {
                        isHere = true;
                        this.Cursor = Cursors.Default;
                        krbTabControl1.SelectedTab = b;
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                if (!isHere)
                {
                    TabPage newTabPage = new TabPage((string)frm.Tag);
                    newTabPage.ImageIndex = 12;
                    krbTabControl1.TabPages.Add(newTabPage);
                    Label newLabel = new Label();
                    newTabPage.Font = new Font("Tahoma", 8);
                    newLabel.Text = newTabPage.Text;
                    newTabPage.Controls.Add(newLabel);
                    krbTabControl1.SelectedTab = newTabPage;
                    frm.TopLevel = false;
                    frm.Parent = newTabPage;
                    frm.Visible = true;
                    frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    bool isHere = false;
            //    tabControl1.Visible = true; ;
            //    frmAddUser frmAddUser = new frmAddUser(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID, ConServer.HostIp);
            //    foreach (TabPage b in tabControl1.TabPages)
            //    {
            //        if (b.Text == (string)frmAddUser.Tag)
            //        {
            //            isHere = true;
            //            tabControl1.SelectedTab = b;
            //            this.Cursor = Cursors.Default;
            //            return;

            //        }
            //    }
            //    if (!isHere)
            //    {

            //        TabPage tab = new TabPage((string)frmAddUser.Tag);

            //        frmAddUser.TopLevel = false;

            //        frmAddUser.Parent = tab;

            //        frmAddUser.Visible = true;

            //        tabControl1.TabPages.Add(tab);

            //        frmAddUser.Location = new Point((tab.Width - frmAddUser.Width) / 2, (tab.Height - frmAddUser.Height) / 2);

            //        tabControl1.SelectedTab = tab;
            //        this.Cursor = Cursors.Default;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //this.Cursor = Cursors.Default;


        }
        /// <summary>
        ///  upat out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// تعديل لبحث الصرف
        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                bool isHere = false;
                frmUpdateOut frm = new frmUpdateOut(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID, ConServer.ConnectionWithHost, ConServer.HostIp);
                krbTabControl1.Visible = true;
                panel1.Visible = true;
                foreach (TabPage b in krbTabControl1.TabPages)
                {
                    if (b.Text.Trim() == ((string)frm.Tag).Trim())
                    {
                        isHere = true;
                        krbTabControl1.SelectedTab = b;
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                if (!isHere)
                {
                    TabPage newTabPage = new TabPage((string)frm.Tag);
                    newTabPage.ImageIndex = 1;
                    krbTabControl1.TabPages.Add(newTabPage);
                    Label newLabel = new Label();
                    newTabPage.Font = new Font("Tahoma", 8);
                    newLabel.Text = newTabPage.Text;
                    newTabPage.Controls.Add(newLabel);
                    krbTabControl1.SelectedTab = newTabPage;
                    frm.TopLevel = false;
                    frm.Parent = newTabPage;
                    frm.Visible = true;
                    frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
        }

         

        
        /// <summary>
        /// /////Repoting Supply
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// تقارير الوارد
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                bool isHere = false;
                frmSuppRepring frm = new frmSuppRepring(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID, ConServer.ConnectionWithHost, ConServer.HostIp);

                krbTabControl1.Visible = true;
                panel1.Visible = true;
                foreach (TabPage b in krbTabControl1.TabPages)
                {
                    if (b.Text.Trim() == ((string)frm.Tag).Trim())
                    {
                        isHere = true;
                        krbTabControl1.SelectedTab = b;
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                if (!isHere)
                {
                    TabPage newTabPage = new TabPage((string)frm.Tag);
                    newTabPage.ImageIndex = 0;
                    krbTabControl1.TabPages.Add(newTabPage);
                    Label newLabel = new Label();
                    newTabPage.Font = new Font("Tahoma", 8);
                    newLabel.Text = newTabPage.Text;
                    newTabPage.Controls.Add(newLabel);
                    krbTabControl1.SelectedTab = newTabPage;
                    frm.TopLevel = false;
                    frm.KeyPreview = true;
                    frm.Parent = newTabPage;
                    frm.Visible = true;
                    frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }



            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    frmSuppRepring frmRepotingSuppply = new frmSuppRepring(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID, ConServer.ConnectionWithHost, ConServer.HostIp); //from update Supply
            //    FormCollection fromco = Application.OpenForms;
            //    bool foundFrom = false;
            //    foreach (Form frm in fromco)
            //    {
            //        if (frm.Name == "frmSuppRepring")
            //        {
            //            frm.Focus();

            //            foundFrom = true;

            //        }

            //    }
            //    if (foundFrom == false)
            //    {
            //        frmRepotingSuppply.Show();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //this.Cursor = Cursors.Default;
        }
        /// <summary>
        /// //Repoting OUt 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// // تقارير الصرف
        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                bool isHere = false;
                frmOutRepting frm = new frmOutRepting(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID, ConServer.ConnectionWithHost, ConServer.HostIp);

                krbTabControl1.Visible = true;
                panel1.Visible = true;
                foreach (TabPage b in krbTabControl1.TabPages)
                {
                    if (b.Text.Trim() == ((string)frm.Tag).Trim())
                    {
                        isHere = true;
                        krbTabControl1.SelectedTab = b;
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                if (!isHere)
                {
                    TabPage newTabPage = new TabPage((string)frm.Tag);
                    newTabPage.ImageIndex = 0;
                    krbTabControl1.TabPages.Add(newTabPage);
                    Label newLabel = new Label();
                    newTabPage.Font = new Font("Tahoma", 8);
                    newLabel.Text = newTabPage.Text;
                    newTabPage.Controls.Add(newLabel);
                    krbTabControl1.SelectedTab = newTabPage;
                    frm.TopLevel = false;
                    frm.Parent = newTabPage;
                    frm.Visible = true;
                    frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    frmOutRepting frmRepotingout = new frmOutRepting(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID,ConServer.ConnectionWithHost,ConServer.HostIp); //from update Supply
            //    FormCollection fromco = Application.OpenForms;
            //    bool foundFrom = false;
            //    foreach (Form frm in fromco)
            //    {
            //        if (frm.Name == "frmOutRepting")
            //        {
            //            frm.Focus();

            //            foundFrom = true;

            //        }

            //    }
            //    if (foundFrom == false)
            //    {
            //        frmRepotingout.Show();
            //    }
            //}
            //catch(Exception ex)
            //{ MessageBox.Show(ex.Message); }
            //this.Cursor = Cursors.Default;
        }
        /// <summary>
        /// //////// Repoting Quntity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// تقارير المحزون
        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                bool isHere = false;
                frmQuntityRepting frm = new frmQuntityRepting(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID, ConServer.ConnectionWithHost, ConServer.HostIp);

                krbTabControl1.Visible = true;
                panel1.Visible = true;
                foreach (TabPage b in krbTabControl1.TabPages)
                {
                    if (b.Text.Trim() == ((string)frm.Tag).Trim())
                    {
                        isHere = true;
                        krbTabControl1.SelectedTab = b;
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                if (!isHere)
                {
                    TabPage newTabPage = new TabPage((string)frm.Tag);
                    newTabPage.ImageIndex = 0;
                    krbTabControl1.TabPages.Add(newTabPage);
                    Label newLabel = new Label();
                    newTabPage.Font = new Font("Tahoma", 8);
                    newLabel.Text = newTabPage.Text;
                    newTabPage.Controls.Add(newLabel);
                    krbTabControl1.SelectedTab = newTabPage;
                    frm.TopLevel = false;
                    frm.Parent = newTabPage;
                    frm.Visible = true;
                    frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    frmQuntityRepting frmRepotingQuntity = new frmQuntityRepting(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID, ConServer.ConnectionWithHost, ConServer.HostIp);
            //    FormCollection fromco = Application.OpenForms;
            //    bool foundFrom = false;
            //    foreach (Form frm in fromco)
            //    {
            //        if (frm.Name == "frmQuntityRepting")
            //        {
            //            frm.Focus();

            //            foundFrom = true;

            //        }

            //    }
            //    if (foundFrom == false)
            //    {
            //        frmRepotingQuntity.Show();
            //    }
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //this.Cursor = Cursors.Default;
        }
        /// <summary>
        /// ///repoting Updta Supply
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// تقارير تعديلات الوارد
        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                bool isHere = false;
                frmUpdSupp frm = new frmUpdSupp(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID, ConServer.ConnectionWithHost, ConServer.HostIp);

                krbTabControl1.Visible = true;
                panel1.Visible = true;
                foreach (TabPage b in krbTabControl1.TabPages)
                {
                    if (b.Text.Trim() == ((string)frm.Tag).Trim())
                    {
                        isHere = true;
                        krbTabControl1.SelectedTab = b;
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                if (!isHere)
                {
                    TabPage newTabPage = new TabPage((string)frm.Tag);
                    newTabPage.ImageIndex = 0;
                    krbTabControl1.TabPages.Add(newTabPage);
                    Label newLabel = new Label();
                    newTabPage.Font = new Font("Tahoma", 8);
                    newLabel.Text = newTabPage.Text;
                    newTabPage.Controls.Add(newLabel);
                    krbTabControl1.SelectedTab = newTabPage;
                    frm.TopLevel = false;
                    frm.Parent = newTabPage;
                    frm.Visible = true;
                    frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    frmUpdSupp frmRepotingSupp = new frmUpdSupp(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID,ConServer.ConnectionWithHost,ConServer.HostIp);
            //    FormCollection fromco = Application.OpenForms;
            //    bool foundFrom = false;
            //    foreach (Form frm in fromco)
            //    {
            //        if (frm.Name == "frmUpdSupp")
            //        {
            //            frm.Focus();

            //            foundFrom = true;

            //        }

            //    }
            //    if (foundFrom == false)
            //    {
            //        frmRepotingSupp.Show();
            //    }
            //}
            //catch(Exception ex)
            //{ MessageBox.Show(ex.Message); }
            //this.Cursor = Cursors.Default;

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
                bool isHere = false;
                frmUpdOutcs frm = new frmUpdOutcs(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID, ConServer.ConnectionWithHost, ConServer.HostIp);

                krbTabControl1.Visible = true;
                panel1.Visible = true;
                foreach (TabPage b in krbTabControl1.TabPages)
                {
                    if (b.Text.Trim() == ((string)frm.Tag).Trim())
                    {
                        isHere = true;
                        krbTabControl1.SelectedTab = b;
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                if (!isHere)
                {
                    TabPage newTabPage = new TabPage((string)frm.Tag);
                    newTabPage.ImageIndex = 0;
                    krbTabControl1.TabPages.Add(newTabPage);
                    Label newLabel = new Label();
                    newTabPage.Font = new Font("Tahoma", 8);
                    newLabel.Text = newTabPage.Text;
                    newTabPage.Controls.Add(newLabel);
                    krbTabControl1.SelectedTab = newTabPage;
                    frm.TopLevel = false;
                    frm.Parent = newTabPage;
                    frm.Visible = true;
                    frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
            //try
            //{


            //    this.Cursor = Cursors.WaitCursor;
            //    frmUpdOutcs frmRepotingUpdOut = new frmUpdOutcs(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID,ConServer.ConnectionWithHost,ConServer.HostIp);
            //    FormCollection fromco = Application.OpenForms;
            //    bool foundFrom = false;
            //    foreach (Form frm in fromco)
            //    {
            //        if (frm.Name == "frmUpdOutcs")
            //        {
            //            frm.Focus();

            //            foundFrom = true;

            //        }

            //    }
            //    if (foundFrom == false)
            //    {
            //        frmRepotingUpdOut.Show();
            //    }
            //}
            //catch(Exception ex)
            //{ MessageBox.Show(ex.Message); }
            //this.Cursor = Cursors.Default;
        }
        // تهيئة الاصناف
        private void toolStripMenuItem24_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Cate frm = new Cate(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, ConServer.ConnectionWithHost, ConServer.HostIp);
                bool isHere = false;
                krbTabControl1.Visible = true;
                panel1.Visible = true;
                foreach (TabPage b in krbTabControl1.TabPages)
                {
                    if (b.Text.Trim() == ((string)frm.Tag).Trim())
                    {
                        isHere = true;
                        krbTabControl1.SelectedTab = b;
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                if (!isHere)
                {
                    TabPage  newTabPage = new TabPage((string)frm.Tag);
                    newTabPage.ImageIndex = 6;
                    krbTabControl1.TabPages.Add(newTabPage);
                    Label newLabel = new Label();
                    newTabPage.Font = new Font("Tahoma", 8);
                    newLabel.Text = newTabPage.Text;
                   // newTabPage.Controls.Add(newLabel);
                    krbTabControl1.SelectedTab = newTabPage;
                    frm.TopLevel = false;
                    frm.Parent = newTabPage;
                    frm.Visible = true;
                    frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    bool isHere = false;
            //    Cate frm = new Cate(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, ConServer.ConnectionWithHost, ConServer.HostIp);
             //foreach (KRBTabControl.TabPageEx b in krbTabControl1.TabPages)
            //    {
            //        if (b.Text.Trim() == ((string)frm.Tag).Trim())
            //        {
            //            isHere = true;
            //            krbTabControl1.SelectedTab = b;
            //            return;
            //        }
            //    }
            //    if (!isHere)
            //    {
            //        KRBTabControl.TabPageEx newTabPage = new KRBTabControl.TabPageEx((string)frm.Tag);
            //        newTabPage.ImageIndex = 2;
            //        krbTabControl1.TabPages.Add(newTabPage);
            //        Label newLabel = new Label();
            //        newTabPage.Font = new Font("Tahoma", 8);
            //        newLabel.Text = newTabPage.Text;
            //        newTabPage.Controls.Add(newLabel);
            //        krbTabControl1.SelectedTab = newTabPage;
            //        frm.TopLevel = false;
            //        frm.Parent = newTabPage;
            //        frm.Visible = true;
            //        frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
            //        this.Cursor = Cursors.Default;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    this.Cursor = Cursors.Default;
            //}

        }
       // تهيئة الانواع
        private void toolStripMenuItem25_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmType frm = new frmType(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, ConServer.ConnectionWithHost, ConServer.HostIp);
                bool isHere = false;
                krbTabControl1.Visible = true;
                this.Cursor = Cursors.Default;
                panel1.Visible = true;
                foreach (TabPage b in krbTabControl1.TabPages)
                {
                    if (b.Text.Trim() == ((string)frm.Tag).Trim())
                    {
                        isHere = true;
                        krbTabControl1.SelectedTab = b;
                        return;
                    }
                }
                if (!isHere)
                {
                    TabPage newTabPage = new TabPage((string)frm.Tag);
                    newTabPage.ImageIndex = 7;
                    krbTabControl1.TabPages.Add(newTabPage);
                    Label newLabel = new Label();
                    newTabPage.Font = new Font("Tahoma", 8);
                    newLabel.Text = newTabPage.Text;
                    newTabPage.Controls.Add(newLabel);
                    krbTabControl1.SelectedTab = newTabPage;
                    frm.TopLevel = false;
                    frm.Parent = newTabPage;
                    frm.Visible = true;
                    frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
          
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    bool isHere = false;
            //    tabControl1.Visible = true; ;
            //    frmType frm = new frmType(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, ConServer.ConnectionWithHost, ConServer.HostIp);
            //    foreach (TabPage b in tabControl1.TabPages)
            //    {
            //        if (b.Text == (string)frm.Tag)
            //        {
            //            isHere = true;
            //            tabControl1.SelectedTab = b;
            //            this.Cursor = Cursors.Default;
            //            return;

            //        }
            //    }
            //    if (!isHere)
            //    {

            //        TabPage tab = new TabPage((string)frm.Tag);

            //        frm.TopLevel = false;

            //        frm.Parent = tab;

            //        frm.Visible = true;

            //        tabControl1.TabPages.Add(tab);

            //        frm.Location = new Point((tab.Width - frm.Width) / 2, (tab.Height - frm.Height) / 2);

            //        tabControl1.SelectedTab = tab;
            //        this.Cursor = Cursors.Default;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //this.Cursor = Cursors.Default;


        }
        //تهيئة الجهات
        private void toolStripMenuItem28_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmPlace frm = new frmPlace(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, ConServer.ConnectionWithHost, ConServer.HostIp);
                bool isHere = false;
                krbTabControl1.Visible = true;
                this.Cursor = Cursors.Default;
                panel1.Visible = true;
                foreach (TabPage b in krbTabControl1.TabPages)
                {
                    if (b.Text.Trim() == ((string)frm.Tag).Trim())
                    {
                        isHere = true;
                        krbTabControl1.SelectedTab = b;
                        return;
                    }
                }
                 if (!isHere)
                 {
                    TabPage newTabPage = new TabPage((string)frm.Tag);
                    newTabPage.ImageIndex = 9;
                    krbTabControl1.TabPages.Add(newTabPage);
                    Label newLabel = new Label();
                    newTabPage.Font = new Font("Tahoma", 8);
                    newLabel.Text = newTabPage.Text;
                    newTabPage.Controls.Add(newLabel);
                    krbTabControl1.SelectedTab = newTabPage;
                    frm.TopLevel = false;
                    frm.Parent = newTabPage;
                    frm.Visible = true;
                    frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    bool isHere = false;
            //    tabControl1.Visible = true; ;
            //    frmPlace frm = new frmPlace(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, ConServer.ConnectionWithHost, ConServer.HostIp);
            //    foreach (TabPage b in tabControl1.TabPages)
            //    {
            //        if (b.Text == (string)frm.Tag)
            //        {
            //            isHere = true;
            //            tabControl1.SelectedTab = b;
            //            this.Cursor = Cursors.Default;
            //            return;

            //        }
            //    }
            //    if (!isHere)
            //    {

            //        TabPage tab = new TabPage((string)frm.Tag);

            //        frm.TopLevel = false;

            //        frm.Parent = tab;

            //        frm.Visible = true;

            //        tabControl1.TabPages.Add(tab);

            //        frm.Location = new Point((tab.Width - frm.Width) / 2, (tab.Height - frm.Height) / 2);

            //        tabControl1.SelectedTab = tab;
            //        this.Cursor = Cursors.Default;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //this.Cursor = Cursors.Default;

        }
         // تهيئة العملات
        private void toolStripMenuItem27_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmCuurncy frm = new frmCuurncy(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, ConServer.ConnectionWithHost, ConServer.HostIp);
                bool isHere = false;
                krbTabControl1.Visible = true;
                panel1.Visible = true;
                this.Cursor = Cursors.Default;
                foreach (TabPage b in krbTabControl1.TabPages)
                {
                    if (b.Text.Trim() == ((string)frm.Tag).Trim())
                    {
                        isHere = true;
                        krbTabControl1.SelectedTab = b;
                        return;
                    }
                }
                if (!isHere)
                {
                    TabPage newTabPage = new TabPage((string)frm.Tag);
                    newTabPage.ImageIndex = 8;
                    krbTabControl1.TabPages.Add(newTabPage);
                    Label newLabel = new Label();
                    newTabPage.Font = new Font("Tahoma", 8);
                    newLabel.Text = newTabPage.Text;
                    newTabPage.Controls.Add(newLabel);
                    krbTabControl1.SelectedTab = newTabPage;
                    frm.TopLevel = false;
                    frm.Parent = newTabPage;
                    frm.Visible = true;
                    frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    bool isHere = false;
            //    tabControl1.Visible = true; ;
            //    frmCuurncy frm = new frmCuurncy(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, ConServer.ConnectionWithHost, ConServer.HostIp);
            //    foreach (TabPage b in tabControl1.TabPages)
            //    {
            //        if (b.Text == (string)frm.Tag)
            //        {
            //            isHere = true;
            //            tabControl1.SelectedTab = b;
            //            this.Cursor = Cursors.Default;
            //            return;

            //        }
            //    }
            //    if (!isHere)
            //    {

            //        TabPage tab = new TabPage((string)frm.Tag);

            //        frm.TopLevel = false;

            //        frm.Parent = tab;

            //        frm.Visible = true;

            //        tabControl1.TabPages.Add(tab);

            //        frm.Location = new Point((tab.Width - frm.Width) / 2, (tab.Height - frm.Height) / 2);

            //        tabControl1.SelectedTab = tab;
            //        this.Cursor = Cursors.Default;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //this.Cursor = Cursors.Default;

        }

        //شجرة الحسابات
        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                bool isHere = false;
                frmAcount frm = new frmAcount(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID, ConServer.ConnectionWithHost, ConServer.HostIp);
                krbTabControl1.Visible = true;
                panel1.Visible = true;
                foreach (TabPage b in krbTabControl1.TabPages)
                {
                    if (b.Text.Trim() == ((string)frm.Tag).Trim())
                    {
                        isHere = true;
                        krbTabControl1.SelectedTab = b;
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                if (!isHere)
                {
                    TabPage newTabPage = new TabPage((string)frm.Tag);
                    newTabPage.ImageIndex = 2;
                    krbTabControl1.TabPages.Add(newTabPage);
                    Label newLabel = new Label();
                    newTabPage.Font = new Font("Tahoma", 8);
                    newLabel.Text = newTabPage.Text;
                    newTabPage.Controls.Add(newLabel);
                    krbTabControl1.SelectedTab = newTabPage;
                    frm.TopLevel = false;
                    frm.Parent = newTabPage;
                    frm.Visible = true;
                    frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
         
        }
        // كشف حساب
        private void toolStripMenuItem26_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                bool isHere = false;
                frmSearchAccountNM frm = new frmSearchAccountNM(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID, ConServer.ConnectionWithHost, ConServer.HostIp);
                krbTabControl1.Visible = true;
                panel1.Visible = true;
                foreach (TabPage b in krbTabControl1.TabPages)
                {
                    if (b.Text.Trim() == ((string)frm.Tag).Trim())
                    {
                        isHere = true;
                        krbTabControl1.SelectedTab = b;
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                if (!isHere)
                {
                    TabPage newTabPage = new TabPage((string)frm.Tag);
                    newTabPage.ImageIndex = 3;
                    krbTabControl1.TabPages.Add(newTabPage);
                    Label newLabel = new Label();
                    newTabPage.Font = new Font("Tahoma", 8);
                    newLabel.Text = newTabPage.Text;
                    newTabPage.Controls.Add(newLabel);
                    krbTabControl1.SelectedTab = newTabPage;
                    frm.TopLevel = false;
                    frm.Parent = newTabPage;
                    frm.Visible = true;
                    frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }

        }
        // تغير كلة المرور
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
        // تهيئة المجموعات
        private void toolStripMenuItem33_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                bool isHere = false;
                frmGroup frm = new frmGroup(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID, ConServer.ConnectionWithHost, ConServer.HostIp); panel1.Visible = true;
                foreach (TabPage b in krbTabControl1.TabPages)
                {
                    if (b.Text.Trim() == ((string)frm.Tag).Trim())
                    {
                        isHere = true;
                        krbTabControl1.SelectedTab = b;
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                if (!isHere)
                {
                    TabPage newTabPage = new TabPage((string)frm.Tag);
                    newTabPage.ImageIndex = 5;
                    krbTabControl1.TabPages.Add(newTabPage);
                    Label newLabel = new Label();
                    newTabPage.Font = new Font("Tahoma", 8);
                    newLabel.Text = newTabPage.Text;
                    newTabPage.Controls.Add(newLabel);
                    krbTabControl1.SelectedTab = newTabPage;
                    frm.TopLevel = false;
                    frm.Parent = newTabPage;
                    frm.Visible = true;
                    frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }

            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    frmGroup frmgroup = new frmGroup(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID,ConServer.ConnectionWithHost,ConServer.HostIp);
            //    FormCollection fromco = Application.OpenForms;
            //    bool foundFrom = false;
            //    foreach (Form frm in fromco)
            //    {
            //        if (frm.Name == "frmGroupr")
            //        {
            //            frm.Focus();

            //            foundFrom = true;

            //        }

            //    }
            //    if (foundFrom == false)
            //    {
            //        frmgroup.Show();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //this.Cursor = Cursors.Default;
        }
         // القيد البسيط
        private void toolStripMenuItem34_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                bool isHere = false;
                SimpleConstraint frm = new SimpleConstraint(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql, UserID, ConServer.ConnectionWithHost, ConServer.HostIp); krbTabControl1.Visible = true;
                panel1.Visible = true;
                foreach (TabPage b in krbTabControl1.TabPages)
                {
                    if (b.Text.Trim() == ((string)frm.Tag).Trim())
                    {
                        isHere = true;
                        krbTabControl1.SelectedTab = b;
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                if (!isHere)
                {
                    TabPage newTabPage = new TabPage((string)frm.Tag);
                    newTabPage.ImageIndex = 4;
                    krbTabControl1.TabPages.Add(newTabPage);
                    Label newLabel = new Label();
                    newTabPage.Font = new Font("Tahoma", 8);
                    newLabel.Text = newTabPage.Text;
                    newTabPage.Controls.Add(newLabel);
                    krbTabControl1.SelectedTab = newTabPage;
                    frm.TopLevel = false;
                    frm.Parent = newTabPage;
                    frm.Visible = true;
                    frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
           
        }
        // الحسابات والجهات
        private void toolStripMenuItem35_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmConnectionPlaceWithAccounts frm = new frmConnectionPlaceWithAccounts(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql,ConServer.ConnectionWithHost,ConServer.HostIp);
                bool isHere = false;
                krbTabControl1.Visible = true;
                this.Cursor = Cursors.Default;
                panel1.Visible = true;
                foreach (TabPage b in krbTabControl1.TabPages)
                {
                    if (b.Text.Trim() == ((string)frm.Tag).Trim())
                    {
                        isHere = true;
                        krbTabControl1.SelectedTab = b;
                        return;
                    }
                }
                if (!isHere)
                {
                    TabPage newTabPage = new TabPage((string)frm.Tag);
                    newTabPage.ImageIndex = 10;
                    krbTabControl1.TabPages.Add(newTabPage);
                    Label newLabel = new Label();
                    newTabPage.Font = new Font("Tahoma", 8);
                    newLabel.Text = newTabPage.Text;
                    newTabPage.Controls.Add(newLabel);
                    krbTabControl1.SelectedTab = newTabPage;
                    frm.TopLevel = false;
                    frm.Parent = newTabPage;
                    frm.Visible = true;
                    frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    bool isHere = false;
            //    tabControl1.Visible = true; ;
            //    frmConnectionPlaceWithAccounts frm = new frmConnectionPlaceWithAccounts(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql,ConServer.ConnectionWithHost,ConServer.HostIp);
            //    foreach (TabPage b in tabControl1.TabPages)
            //    {
            //        if (b.Text == (string)frm.Tag)
            //        {
            //            isHere = true;
            //            tabControl1.SelectedTab = b;
            //            this.Cursor = Cursors.Default;
            //            return;

            //        }
            //    }
            //    if (!isHere)
            //    {

            //        TabPage tab = new TabPage((string)frm.Tag);

            //        frm.TopLevel = false;

            //        frm.Parent = tab;

            //        frm.Visible = true;

            //        tabControl1.TabPages.Add(tab);

            //        frm.Location = new Point((tab.Width - frm.Width) / 2, (tab.Height - frm.Height) / 2);

            //        tabControl1.SelectedTab = tab;
            //        this.Cursor = Cursors.Default;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //this.Cursor = Cursors.Default;

        }
        //convert MemmoryToDB
        DataTable ConvertMemorytoDB(MemoryStream ms)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            ms.Seek(0, SeekOrigin.Begin);
            DataTable dt = (DataTable)formatter.Deserialize(ms);
            return dt;
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                serHost.SENDUSERTOSERVER(0, ConServer.SessionID, DateTime.Now, DateTime.Now, System.Environment.MachineName, System.Environment.UserName, System.Environment.OSVersion.ToString(), null, 0);
            }
            catch
            {


            }
        }

       
        private void krbTabControl1_TabClosing(object sender, TabControlCancelEventArgs e)
        {
            if (krbTabControl1.TabPages.Count == 1)
                panel1.Visible = false;
        }

        private void toolStripMenuItem36_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmBranches frm = new frmBranches(ConServer.ServerNM, ConServer.DBNM, ConServer.UserSql, ConServer.PassSql,UserID, ConServer.ConnectionWithHost, ConServer.HostIp,1);
                bool isHere = false;
                krbTabControl1.Visible = true;
                this.Cursor = Cursors.Default;
                panel1.Visible = true;
                foreach (TabPage b in krbTabControl1.TabPages)
                {
                    if (b.Text.Trim() == ((string)frm.Tag).Trim())
                    {
                        isHere = true;
                        krbTabControl1.SelectedTab = b;
                        return;
                    }
                }
                if (!isHere)
                {
                    TabPage newTabPage = new TabPage((string)frm.Tag);
                    newTabPage.ImageIndex = 10;
                    krbTabControl1.TabPages.Add(newTabPage);
                    Label newLabel = new Label();
                    newTabPage.Font = new Font("Tahoma", 8);
                    newLabel.Text = newTabPage.Text;
                    newTabPage.Controls.Add(newLabel);
                    krbTabControl1.SelectedTab = newTabPage;
                    frm.TopLevel = false;
                    frm.Parent = newTabPage;
                    frm.Visible = true;
                    frm.Location = new Point((newTabPage.Width - frm.Width) / 2, ((newTabPage.Height - frm.Height) / 2));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Application.StartupPath.ToString());
        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {

        }
    }
}
