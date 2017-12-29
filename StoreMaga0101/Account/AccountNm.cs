using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
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

    }
}
