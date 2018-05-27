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
    public partial class frmChangPass : Form
    {
        int  UserID;
        UsersSQl Us;
        ServiceReference1.IserviceClient UsHost;
        public frmChangPass(int UserId)
        {
            InitializeComponent();
            try
            {
                UserID = UserId;
                if (!ConServer.ConnectionWithHost)
                {
                    Us = new UsersSQl(@".\s2008", "StoreManagement1", null, null);

                }
                else
                {
                    UsHost = new ServiceReference1.IserviceClient();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public frmChangPass(string SerNm, string DbNm, string UserSql, string PassSql, int Useri)
        {
            InitializeComponent();
            try
            {
                    UserID = Useri;
                if (!ConServer.ConnectionWithHost)
                {
                    Us = new UsersSQl(SerNm, DbNm, UserSql, PassSql);
                }
                else
                {
                    UsHost = new ServiceReference1.IserviceClient();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmChangPass_Load(object sender, EventArgs e)
        {

        }

        private void btnAddSup_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length>0)
            {
                if(textBox1.Text.Equals(textBox2.Text))
                { if (!ConServer.ConnectionWithHost)
                    {
                        Us.UpatePassword(UserID, ChangePass(textBox1.Text));
                    }
                    else
                    {
                        UsHost.UpatePassword(UserID, ChangePass(textBox1.Text));
                    }
                    MessageBox.Show("تم تعديل كلمة المرور", "رسالة", MessageBoxButtons.OK);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("لا يوجد تتطايق في كلمة المرور", "رسالة", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("يجب كتابة كلمة المرور", "رسالة", MessageBoxButtons.OK);
            }
        }
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
