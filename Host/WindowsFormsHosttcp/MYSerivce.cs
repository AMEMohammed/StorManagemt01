using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Users;
namespace WindowsFormsHosttcp
{ [ServiceBehavior(InstanceContextMode =InstanceContextMode.Single)]
    class MYSerivce : Iservice
    {   /// Classes
        UsersSQl users = new UsersSQl(Properties.Settings.Default.ServerNm, Properties.Settings.Default.DBNM, null, null);
       /// end Classes
       /// 
        public delegate void ResDT(int falg,string nm);
        public ResDT restd;

       
        // convert dataTable to MemoryStream
        public MemoryStream ConvertDBtoMomery(DataTable dt)
        {
            MemoryStream ms = new MemoryStream();
            System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            formatter.Serialize(ms, dt);
            return ms;
        }


        public void SENDUSERTOSERVER(int falg,string name)
        {
            restd(falg,name);
        }

        #region  
        ///////
        //// USERS 

        // Login User By name and pass
        public int LoginUser(string User, string Pass)
        {
            return users.LoginUser(User, Pass);
        }
        /// add New User
        public  int AddNewUser(string name, string user, string pass, bool Addsupply, bool UpdSupply, bool Addoutt, bool updOut, bool PrintSupply, bool PrintOut, bool PrintQuntity, bool UpdSupp1, bool UpdOut1, bool userAdd, bool Active, bool Cate, bool type1, bool account, bool Monay, bool Place, int UserID)
        {
            return users.AddNewUser(name, user, pass, Addsupply, UpdSupply, Addoutt, updOut, PrintSupply, PrintOut, PrintQuntity, UpdSupp1, UpdOut1, userAdd, Active, Cate, type1, account, Monay, Place, UserID);
        }
        //update Password for users
        public int UpatePassword(int idUser, string Pass)
        {
            return users.UpatePassword(idUser, Pass);
        }
        // update User
        public int UpdUsers(int IDUser, string name, string user, string pass, bool Addsupply, bool UpdSupply, bool Addoutt, bool updOut, bool PrintSupply, bool PrintOut, bool PrintQuntity, bool UpdSupp1, bool UpdOut1, bool userAdd, bool Active, bool Cate, bool type1, bool account, bool Monay, bool Place)
        {
            return users.UpdUsers(IDUser,  name,  user,  pass,  Addsupply, UpdSupply,  Addoutt,  updOut,  PrintSupply,  PrintOut, PrintQuntity, UpdSupp1, UpdOut1,  userAdd,  Active,  Cate,  type1,  account, Monay, Place);
        }
        // get all users
        public MemoryStream GetAllUser()
        {
            return ConvertDBtoMomery(users.GetAllUser());
        }
        // get one users
       public MemoryStream GetUser(int IdUs)
        {
            return ConvertDBtoMomery(users.GetUser(IdUs));
        }
        /// end USERS 
        #endregion
    }

}
