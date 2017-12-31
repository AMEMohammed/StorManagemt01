using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Account
{
    public partial class frmAcount : Form
    {
        DataTable dt = new DataTable();
        AccountNm Acn;
        int IDUSER;
        int IdAcount;
       
        public frmAcount()
        {
            InitializeComponent();
            try
            {
                Acn = new AccountNm(@".\s2008", "StoreManagement1", null, null);
                IDUSER = 1;
                dt = Acn.GetAllAccount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        ///
        public frmAcount(string ServNm, string DbNm, string UesrSql, string PassSql, int UserId)
        {
            InitializeComponent();
            try
            {
                Acn = new AccountNm(ServNm, DbNm, UesrSql, PassSql);
                IDUSER = UserId;
                dt = Acn.GetAllAccount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }



        ////////////////////
        ///////
        ////////loding TreeView
        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {

            TreeNode childNode;

            foreach (DataRow dr in dt.Select("[IDParentAcount]=" + parentId))// جلب السطر برقم معين
            { 
                TreeNode t = new TreeNode();
                t.Text = dr["IDCode"].ToString() +"-"+dr["AcountNm"].ToString();
                t.Name = dr["IDCode"].ToString();

                t.Tag = dr["IDAcountNm"].ToString(); //ترقيم العقدة برقم  السطر في الجدول
                 
                if (parentNode == null)
                {
                    treeView1.Nodes.Add(t);
                    childNode = t;
                }
                else
                {
                    parentNode.Nodes.Add(t);
                    childNode = t;
                }
                PopulateTreeView(Convert.ToInt32(dr["IDCode"].ToString()), childNode);
            }
        }

        private void frmAcount_Load(object sender, EventArgs e)
        {
            PopulateTreeView(0, null);
        }

        private void اختيارالحسابToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  MessageBox.Show(treeView1.SelectedNode.Text);
            try
            {
                ShowTreeNode(treeView1.SelectedNode);
            }
            catch (Exception ex)
            {
                MessageBox.Show("يرجى تحديد الحساب  ");

            }
        }
        /// <summary>
        /// /////////
        /// </summary>
        /// <param name="t"></param>
        void ShowTreeNode(TreeNode t)
        { 
            TreeNode tParent = new TreeNode();
          
            tParent = t.Parent;
             comboBox1.Text = tParent.Text;
             textBox1.Text = t.Text;
             textBox4.Text = t.Name;
         
           
           
            IdAcount = Convert.ToInt32(t.Tag.ToString());
            comboBox2.Text = Acn.TypeAccount(IdAcount);//get Type Account;





        }

        private void btnAddSup_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// ///// updat Buottn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefrsh_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length>0 && IdAcount>=1)
            {
                string AccounNm = new String(textBox1.Text.Where(c => c != '-' && (c < '0' || c > '9')).ToArray());
                Acn.UpdateAccountNm(IdAcount, AccounNm );
                RefrshTreeNode();
            }
        }
        void RefrshTreeNode()
        {
            dt = Acn.GetAllAccount();
            treeView1.Nodes.Clear();
            PopulateTreeView(0, null);
            IdAcount = -1;
        }

        private void اضافةحسابفرعيToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode t = new TreeNode();
            t = treeView1.SelectedNode;
            IdAcount = Convert.ToInt32(t.Tag.ToString());
            if(Acn.TypeAccount(IdAcount).Equals("رئيسي"))
            {

            }
            else
{
                MessageBox.Show("لايمكن اضافة حساب الى الحساب الفرعي");
            }

        }
    }
}
