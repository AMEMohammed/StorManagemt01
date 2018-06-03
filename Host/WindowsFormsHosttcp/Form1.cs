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
using System.Data;
namespace WindowsFormsHosttcp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ServiceHost host;
        MYSerivce my;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                my= new MYSerivce();
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
        void ReciveNameFRMCLINET(int flag, int SessionID, DateTime start, DateTime end, string NameMachine, string UserWindow, string OSVersion, string NameUser, int USerID)
        {



            string[] mass = new string[] { NameUser, start.ToShortDateString()+"  "+start.ToLongTimeString(), SessionID.ToString() };
           if (flag == 0)
            {
                
                for(int i=0;i<dataGridView1.Rows.Count;i++)
                {
                    if (string.Equals(dataGridView1[2,i].Value , SessionID.ToString()))
                    {
                        my.UpdateENDtimeSession(SessionID, end);
                        dataGridView1.Rows.RemoveAt(i);
                        return;
                    }
                }
              


            }
            else
            {
               
                dataGridView1.Rows.Add(mass);
                my.AddNewSession(start, end, OSVersion, NameMachine, UserWindow, USerID);

            }
        }
    }
}
