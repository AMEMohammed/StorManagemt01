using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
namespace Users
{
    class AccessConnection

    {
        private string  ConnectionString;
        OleDbConnection AccessCon;
        public AccessConnection(string pathAccess)
        {

            //  ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source =" + pathAccess+ @"\StorManagment1.accdb;Persist Security Info=True;";
            ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source =D:\programing1\StoreMaga0101\StroreMaga0101hostNewDesgin\StoreMaga0101 with host\StoreMaga0101\bin\Debug\StorManagment1.accdb;Persist Security Info=True;";
            MessageBox.Show(ConnectionString);
            AccessCon = new OleDbConnection(ConnectionString);
            try
            {
                AccessCon.Open();
                MessageBox.Show("اتصل");
            }
            catch(Exception EX)
            {
                MessageBox.Show(EX.Message);

            }



        }
        


        private void Open()
        {

            if (AccessCon.State != System.Data.ConnectionState.Open)
            {
                AccessCon.Open();
            }


        }
        private void Close()
        {
            if (AccessCon.State != System.Data.ConnectionState.Closed)
            {
                AccessCon.Close();
            }


        }

        /// <summary>
        /// //////////// select data
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        public DataTable SelectData(string Query, OleDbParameter[] parm)
        {
            DataTable dt = new DataTable();

            OleDbCommand AceessCon = new OleDbCommand();
            AceessCon.Connection = AccessCon;
            AceessCon.CommandText = Query;
            AceessCon.CommandType = CommandType.Text;
            if (parm != null)
            {
                for (int i = 0; i < parm.Length; i++)
                {
                    AceessCon.Parameters.Add(parm[i]);
                }
            }


            OleDbDataAdapter sqlad = new OleDbDataAdapter(AceessCon);

            sqlad.Fill(dt);
            return dt;


        }
        /////////////////////////////////////////////////////
        //////////
        public int ExcuteQuery(string Query, OleDbParameter[] parm)

        {
            int result = 0;
            OleDbCommand AccessCon = new OleDbCommand();
            AccessCon.Connection = this.AccessCon;
            AccessCon.CommandText = Query;
            AccessCon.CommandType = CommandType.Text;
            if (parm != null)
            {
                for (int i = 0; i < parm.Length; i++)
                {
                    AccessCon.Parameters.Add(parm[i]);
                }
            }
            Open();
            AccessCon.ExecuteNonQuery();
            Close();
            result = 1;
            return result;



        }
        /////////////////////////////////////////////////////
        //////////
        public object ExcuteQueryValue(string Query, OleDbParameter[] parm)

        {

            object result;

            OleDbCommand AccessCon = new OleDbCommand();
            AccessCon.Connection = this.AccessCon;
            AccessCon.CommandText = Query;
            AccessCon.CommandType = CommandType.Text;
            if (parm != null)
            {
                for (int i = 0; i < parm.Length; i++)
                {
                    AccessCon.Parameters.Add(parm[i]);
                }
            }
            Open();
            result = AccessCon.ExecuteScalar();
            Close();

            return result;



        }

    }
}
