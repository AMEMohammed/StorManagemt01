using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using System.Data;
namespace WindowsFormsHosttcp
{
    class MSqlConnection
    {
        private string ConnectionString;
        SqlConnection sqlcon;
        public MSqlConnection(string ServerSql, string DbSql, string UserSql, string PassSql)
        {
           
                ConnectionString = @"Data Source=" + ServerSql + ";Initial Catalog=" + DbSql + ";User ID=" + UserSql + ";Password=" + PassSql;
                sqlcon = new SqlConnection(ConnectionString);
            
           
        }
        public MSqlConnection(string ServerSql, string DbSql)
        {
            

             ConnectionString = @"Data Source=" + ServerSql + ";Initial Catalog=" + DbSql + ";Integrated Security=true;";
                sqlcon = new SqlConnection(ConnectionString);

            
        }


        private void Open()
        {
            
                if (sqlcon.State != System.Data.ConnectionState.Open)
                {
                    sqlcon.Open();
                }


        }
        private void Close()
        {
                if (sqlcon.State != System.Data.ConnectionState.Closed)
                {
                    sqlcon.Close();
                }

           
        }

        /// <summary>
        /// //////////// select data
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        public DataTable SelectData(string Query, SqlParameter[] parm)
        {
            DataTable dt = new DataTable();
           
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = Query;
                sqlcmd.CommandType = CommandType.Text;
                if (parm != null)
                {
                    for (int i = 0; i < parm.Length; i++)
                    {
                        sqlcmd.Parameters.Add(parm[i]);
                    }
                }


                SqlDataAdapter sqlad = new SqlDataAdapter(sqlcmd);

                sqlad.Fill(dt);
                return dt;
           

        }
        public DataTable SelectDataNoParm(string Query)
        {
            DataTable dt = new DataTable();

            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlcon;
            sqlcmd.CommandText = Query;
            sqlcmd.CommandType = CommandType.Text;
          

            SqlDataAdapter sqlad = new SqlDataAdapter(sqlcmd);

            sqlad.Fill(dt);
            return dt;


        }
        /////////////////////////////////////////////////////
        //////////
        public int ExcuteQuery(string Query, SqlParameter[] parm)

        {
            int result = 0;
            
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandText = Query;
                sqlcmd.CommandType = CommandType.Text;
                if (parm != null)
                {
                    for (int i = 0; i < parm.Length; i++)
                    {
                        sqlcmd.Parameters.Add(parm[i]);
                    }
                }
                Open();
                sqlcmd.ExecuteNonQuery();
                Close();
                result = 1;
                return result;

           

        }
        /////////////////////////////////////////////////////
        //////////
        public int  ExcuteQueryValue(string Query, SqlParameter[] parm)

        {
            
            int  result;

            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlcon;
            sqlcmd.CommandText = Query;
            sqlcmd.CommandType = CommandType.Text;
            if (parm != null)
            {
                for (int i = 0; i < parm.Length; i++)
                {
                    sqlcmd.Parameters.Add(parm[i]);
                }
            }
            Open();
            result=Convert.ToInt32( sqlcmd.ExecuteScalar());
            Close();
           
            return result;



        }
        //////////
        public int ExcuteQueryValue2(string Query

        {

            int result;

            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlcon;
            sqlcmd.CommandText = Query;
            sqlcmd.CommandType = CommandType.Text;
          
            Open();
            result = Convert.ToInt32(sqlcmd.ExecuteScalar());
            Close();

            return result;



        }

    }
}

    
   
   
