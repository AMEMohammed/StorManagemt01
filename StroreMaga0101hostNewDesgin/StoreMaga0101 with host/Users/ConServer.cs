using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users
{
   public class ConServer
    {
        public static string ServerNM = Properties.Settings.Default.ServerNm;
        public static string DBNM = Properties.Settings.Default.DBNM;
        public static string UserSql = Properties.Settings.Default.UserSql;
        public static string PassSql = Properties.Settings.Default.PassSql;
        public static bool ConnectionWithHost = Properties.Settings.Default.ConnectionHost;
        public static string HostIp = Properties.Settings.Default.HostIP;
        public static string iphost = Properties.Settings.Default.iphost;
        public static string port = Properties.Settings.Default.port;
        public static int SessionID = 0;
    }
}
