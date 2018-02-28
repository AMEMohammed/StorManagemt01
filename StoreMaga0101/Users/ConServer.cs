using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users
{
   public  class ConServer
    {
        public static string ServerNM = Properties.Settings.Default.ServerNm;
        public static string DBNM = Properties.Settings.Default.DBNM;
        public static string UserSql = Properties.Settings.Default.UserSql;
        public static string PassSql = Properties.Settings.Default.PassSql;
    }
}
