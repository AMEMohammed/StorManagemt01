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
                t.Text = dr["IDCode"].ToString() + " - " + dr["AcountNm"].ToString();
                t.Name = dr["IDCode"].ToString();
             
                t.Tag = dt.Rows.IndexOf(dr);
               
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
    }
}
