using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;

namespace Users
{
    public partial class frmLogin : Form
    {
        int UserID = 0;
        int idd = 0;
        UsersSQl Us;
        ServiceReference1.IserviceClient US1;
        public static int GETIDD = -1;
        public frmLogin()
        {
            InitializeComponent();
            try
            {
                if (!ConServer.ConnectionWithHost)
                {
                    Us = new UsersSQl(@".\s20", "StoreManagement1", null, null);
                }
                else
                {
                    US1 = new ServiceReference1.IserviceClient();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public frmLogin(string SerNm, string DbNm, string UserSql, string PassSql,string HostIp)
        {
            InitializeComponent();
            try
            {
              
                if (!ConServer.ConnectionWithHost)
                {

                    Us = new UsersSQl(SerNm, DbNm, UserSql, PassSql);
                }
                else
                {
                    
                    US1 = new ServiceReference1.IserviceClient();
                    EndpointAddress endp = new EndpointAddress(HostIp);
                    US1.Endpoint.Address = endp;
             


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            changeLanguage();
            

        }
        int UserId;
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        string ChangePass(string pass)
        {
            byte[] tmpSource = new UTF8Encoding().GetBytes(pass);
            return Convert.ToBase64String(tmpSource);
        }
        //فك تشفير كلمة المورر
        //
        string GetPassNormal(string passDecode)
        {
            byte[] tmpData = Convert.FromBase64String(passDecode);
            return (new UTF8Encoding().GetString(tmpData));
        }

        private void btnAddSup_Click(object sender, EventArgs e)
        { 
            if (!ConServer.ConnectionWithHost)
            {
             UserId = Us.LoginUser(textBox1.Text, ChangePass(textBox2.Text));
            }
          else
            {

                try
                {
                   
                    UserId = US1.LoginUser(textBox1.Text, ChangePass(textBox2.Text));
                  

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               
            }
          
      
            DataTable dt = new DataTable();
          
            //if(textBox1.Text=="admin" && textBox2.Text=="ame770958747")
            //{
            //    UserId = 2;
            //}
            if (UserId>0)//في حالة لدخول 
            {  
                if (!ConServer.ConnectionWithHost)//conncetion loacl
                {

                    dt = Us.GetUser(UserId); // جلب صلاحيات المستخدم
                }
                else // connection host
                {
                   
                    dt =ConvertMemorytoDB( US1.GetUser(UserId)); // جلب صلاحيات المستخدم

                }
                bool k = Convert.ToBoolean(dt.Rows[0][12].ToString());

                if (Convert.ToBoolean(dt.Rows[0][12].ToString()) == true) // التاكد في حالة ان المستخدم موقف او لا
                {
                    GETIDD = UserId;
                    // connection host
                    if (ConServer.ConnectionWithHost)
                    {
                        // MessageBox.Show(US1);
                        ConServer.SessionID = US1.GETMAXIDSession();
                        US1.SENDUSERTOSERVER(1,ConServer.SessionID,DateTime.Now,DateTime.Now,System.Environment.MachineName,System.Environment.UserName,System.Environment.OSVersion.ToString(),textBox1.Text, GETIDD);
                    }
                        this.Close();
                }
                else
                {
                    MessageBox.Show("تم توقيف المستخدم الخاص بك .... يرجى مراجعة مدير النظام");
                }

            }
            else
            {
                MessageBox.Show("تاكد من البيانات المدخله");
                textBox2.Text = "";
                textBox2.Focus();

            }
        }
  
        private void button4_Click(object sender, EventArgs e)
        {
            GETIDD = -1;
             this.Close();
        }
        public void changeLanguage()
        {
            foreach (InputLanguage lng in InputLanguage.InstalledInputLanguages)
            {
                if (lng.LayoutName == "العربية (101)")
                    InputLanguage.CurrentInputLanguage = lng;
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            new FrmConServer().ShowDialog();
        }
        /////
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
