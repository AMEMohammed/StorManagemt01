using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace Account
{ 
    class AccountNm
    {
        MSqlConnection sql;

      public  AccountNm(string SerNm,string DBNm,string UserSql,string PassSql)
        {
            if(UserSql ==null || PassSql ==null)
            {
                sql = new MSqlConnection(SerNm, DBNm);
            }
            else
            {
                sql = new MSqlConnection(SerNm, DBNm, UserSql, PassSql);
            }

            
        }
        public DataTable GetAllAccount()
        {
            string Query = "select * from AccountNm";
            return sql.SelectData(Query, null);
        }
        public int UpdateAccountNm(int iDAccounNm,string Name)
        {
            string Query = "Update AccountNm set AcountNm=@name where IDAcountNm=@id";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@id", iDAccounNm);
            parm[1] = new SqlParameter("@name", Name);
            return sql.ExcuteQuery(Query, parm);
        }
        public string TypeAccount(int IDAccount)
        {
            string Query = "select AcountType from AccountNm where IDAcountNm=@id ";
            SqlParameter[] parm=new SqlParameter[1];
            parm[0] = new SqlParameter("@id", IDAccount);
            return (string)sql.ExcuteQueryValue(Query, parm);
        }
    }
}
