using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using System.Transactions;
namespace WindowsFormsHosttcp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ServiceHost host;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                 MYSerivce my = new MYSerivce();
                 my.restd = new MYSerivce.ResDT(ReciveNameFRMCLINET);
               
                  host=  new ServiceHost(my);

                 host.Open();
                 textBox1.Text = host.Description.Endpoints[0].Address.ToString()+"  "+DateTime.Now.ToString();
           
            

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }
        void ReciveNameFRMCLINET(int flag, string Massege)
        {

            
          
            string[] mass = Massege.Split('.');
          //  if (flag == 0)
            { 
              
             //dataGridView1.Rows.RemoveAt()
            }
            //else 
            {
                dataGridView1.Rows.Add(mass);
            }
        }
    }
}
