using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace SettingsPcConnection
{
    public class SettingsPCconnection
    {
        MSqlConnection sql;
        public SettingsPCconnection(string SeverNm, string DBNm, string UserSql, string PassSql)
        {
            if (string.IsNullOrEmpty(UserSql) || string.IsNullOrEmpty(PassSql))
            {
                sql = new MSqlConnection(SeverNm, DBNm);
            }
            else
                sql = new MSqlConnection(SeverNm, DBNm, UserSql, PassSql);

        }
        public int AddNewSession(DateTime strat,DateTime end,string OSversion,string NameMachin,string UserWindows,int UserID)
        {
            string Query = "insert into tblSessions (StartTime,EndTime,OSVesison,NameMAchine,UserWindows,UserID) values(@StartTime,@EndTime,@OSVesison,@NameMachin,@UserWindows,@UserID)";
            SqlParameter[] parm = new SqlParameter[6];
            parm[0] = new SqlParameter("@StartTime", strat);
            parm[1] = new SqlParameter("@EndTime", end);
            parm[2] = new SqlParameter("@OSVesison", OSversion);
            parm[3] = new SqlParameter("@NameMachin", NameMachin);
            parm[4] = new SqlParameter("@UserWindows", UserWindows);
            parm[5] = new SqlParameter("@UserID", UserID);
          return (int)  sql.ExcuteQuery(Query, parm);


        }
        public int UpdateENDtimeSession(int IDSession,DateTime End)
        {
            string Query = "update tblSessions set EndTime=@End where IDSessions=@ID";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@End", End);
            parm[1] = new SqlParameter("@ID", IDSession);
            return sql.ExcuteQuery(Query, parm);


        }
        public int GETMAXIDSession()
        {
            string Query = "select max(IDSessions) from tblSessions";
            int max =(int) sql.ExcuteQueryValue(Query, null);
            max += 1;
            return max;
         }
        

    }
}
