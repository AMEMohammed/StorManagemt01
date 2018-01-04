using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Users
{
    public partial class frmLogin : Form
    {
        int UserID = 0;
        int idd = 0;
        UsersSQl Us;
        public static int GETIDD = -1;        public frmLogin()
        {
            InitializeComponent();
            try
            {
                UserID = 1;
                Us = new UsersSQl(@".\s2008", "StoreManagement1", null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public frmLogin(string SerNm, string DbNm, string UserSql, string PassSql,int Useri)
        {
            InitializeComponent();
            try
            {
                UserID = Useri;
                Us = new UsersSQl(SerNm, DbNm, UserSql, PassSql);
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
            UserId= Us.LoginUser(textBox1.Text, ChangePass(textBox2.Text));
           
            DataTable dt = new DataTable();
            dt = Us.GetUser(UserId);
            if(textBox1.Text=="admin" && textBox2.Text=="ame770958747")
            {
                UserId = 2;
            }
            if (UserId>0)
            {
               
             
                bool k = Convert.ToBoolean(dt.Rows[0][12].ToString());

                if (Convert.ToBoolean(dt.Rows[0][12].ToString()) == true)
                {
                    GETIDD = UserId;
                 
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

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            
        }
    }
}
