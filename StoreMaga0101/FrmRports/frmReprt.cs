using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace FrmRports
{
    public partial class frmReprt : Form
    {
        int Tag1 = -1;
        int Id = -1;
        int User = 0;
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        public frmReprt()
        {
            InitializeComponent();
        }
        public frmReprt(int id ,int user,int Tag)
        {
            InitializeComponent();
            Id = id;
            User = user;
            Tag1 = Tag;
        }
        public frmReprt(DataTable dt, DataTable d,int Tag)
        {
            InitializeComponent();
            dt1 = dt;
            dt2 = d;
            Tag1 = Tag;
        }
        private void frmReprt_Load(object sender, EventArgs e)
        {
            try
            {
                switch (Tag1)
                {
                    case 1:
                        {
                            PrintReSupply(Id);
                            break;
                        }
                    case 2:
                        {
                            PrintReOut(Id,dt1,dt2);
                            break;
                        }
                    case 3:
                        {
                            PrintSupplyAll(dt1);
                            break;
                        }
                    case 4:
                        {
                            printOutALL(dt1);
                            
                            break;
                        }
                    case 5:
                        {
                           PrintAccountQuntity(dt1);
                            break;
                        }
                    case 6:
                        {
                            PrintUpdteSupply(dt1);
                            break;
                        }
                    case 7:
                        {
                       PrintUpdteOUt(dt1);
                            break;
                        }
                    case 8:
                        {
                           
                            PrintAccountReveal(dt1);
                            break;
                        }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        public void PrintAccountReveal(DataTable Dt12)
        {
            RpotAccountReveal rp = new RpotAccountReveal();
            rp.SetDataSource(Dt12);
            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Refresh();

        }
        public void PrintReOut(int id,DataTable dt22,DataTable dt23)
        {
            RequstOut rt = new RequstOut();
            DataTable dtttt = new DataTable();
            dtttt = dt22;

            rt.SetDataSource(dtttt);

            crystalReportViewer1.ReportSource = rt;

            crystalReportViewer1.Refresh();
            if (dt23 !=null)
            {
                DataTable dtt1 = new DataTable();
                ExitStatement rt1 = new ExitStatement();
                dtt1 = dt23;

                for (int i = 0; i < dtt1.Rows.Count; i++)
                {
                    dtt1.Rows[i][6] = "تصريح خروج مواد";
                }
                rt1.SetDataSource(dtt1);
                rt1.PrintToPrinter(1, false, 0, 0); //print dicret

            }

        }
        /////
        public void PrintReSupply(int id)
        {

            RptRqustSupply rt = new RptRqustSupply();

            rt.SetDataSource(dt1);
            crystalReportViewer1.ReportSource = rt;

            crystalReportViewer1.Refresh();
            if (dt2 != null)
            {

                ExitStatement rt1 = new ExitStatement();

                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    dt2.Rows[i][4] = "مسؤول المخازن";
                    dt2.Rows[i][6] = "تصريح توريد مخزني";
                    dt2.Rows[i][7] = "المخازن";
                }
                rt1.SetDataSource(dt2);
                rt1.PrintToPrinter(1, false, 0, 0); //print dicret



            }
        }
      
        //
        public void PrintSupplyAll(DataTable dtt)
        {

           RptRequstSupplyAll rt = new RptRequstSupplyAll();
            rt.SetDataSource(dtt);

            crystalReportViewer1.ReportSource = rt;
            crystalReportViewer1.Refresh();
        }
        public void printOutALL(DataTable dttt)
        {
            OutAll outall = new OutAll();
            outall.SetDataSource(dt1);
            crystalReportViewer1.ReportSource = outall;
            crystalReportViewer1.Refresh();
        }

        public void PrintAccountQuntity(DataTable ddd)
        {
            AccountQintity outall = new AccountQintity();
            outall.SetDataSource(ddd);
            crystalReportViewer1.ReportSource = outall;
            crystalReportViewer1.Refresh();
        }
        public void PrintUpdteSupply(DataTable dt11)
        {
            UpdSupply updSupp = new UpdSupply();
            updSupp.SetDataSource(dt11);
            crystalReportViewer1.ReportSource = updSupp;
            crystalReportViewer1.Refresh();
        }
        public void PrintUpdteOUt(DataTable dt11)
        {
            UpdOut updSupp = new UpdOut();
            updSupp.SetDataSource(dt11);
            crystalReportViewer1.ReportSource = updSupp;
            crystalReportViewer1.Refresh();

        }
    }
}
