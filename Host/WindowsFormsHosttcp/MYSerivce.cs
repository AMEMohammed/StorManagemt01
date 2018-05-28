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
using Account;
namespace WindowsFormsHosttcp
{ [ServiceBehavior(InstanceContextMode =InstanceContextMode.Single)]
    class MYSerivce : Iservice
    {   /// Classes
        // Users    
        UsersSQl users = new UsersSQl(Properties.Settings.Default.ServerNm, Properties.Settings.Default.DBNM, null, null);
        // Account
        AccountNm accountNm = new AccountNm(Properties.Settings.Default.ServerNm, Properties.Settings.Default.DBNM, null, null);
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

        #endregion
        //end User
        //
        // Account الحسابات
        #region
         // جلب كافة الحسابات
        public MemoryStream GetAllAccount()
        {
           return  ConvertDBtoMomery(accountNm.GetAllAccount());
        }
        // جلب كافة الحسابات عربي
        public MemoryStream GetAllAcountnAr()
        {
            return ConvertDBtoMomery(accountNm.GetAllAcountnAr());
        }
        // Uppdate Account Name and Active
        public int UpdateAccountNm(int iDAccounNm, string Name, bool active)
        {
            return accountNm.UpdateAccountNm(iDAccounNm, Name, active);
        }
        //
        public bool CheckAccountinDetlis(int idcode)
        {
            return accountNm.CheckAccountinDetlis(idcode);
        }

        public bool CheckAccounthaschalid(int idcode)
        {
            return accountNm.CheckAccounthaschalid(idcode);
        }

        public string TypeAccount(int IDAccount)
        {
            return accountNm.TypeAccount(IDAccount);
        }

        public bool GetCheckAccount(int IDAccount)
        {
            return accountNm.GetCheckAccount(IDAccount);
        }

        public bool GetCheckAccountHere(int IDAccount)
        {
            return accountNm.GetCheckAccountHere(IDAccount);
        }

        public int GetMaxCode(int CodeParent)
        {
            return accountNm.GetMaxCode(CodeParent);
        
        }

        public int AddNewAcountNm(string AcountNm, int IdCOde, int IdParnt, string Type, int Active, DateTime DateStart, int UserId)
        {
            return accountNm.AddNewAcountNm(AcountNm,  IdCOde,  IdParnt,  Type,  Active,  DateStart,  UserId);
        }

        public int DelteAccount(int IDCount)
        {
            return accountNm.DelteAccount(IDCount);
         }

        public int DelteAccount2(int IDcode)
        {
            return accountNm.DelteAccount2(IDcode);
        }

        public MemoryStream SearchAcount(string name)
        {
            return ConvertDBtoMomery(accountNm.SearchAcount(name));
        }

        public MemoryStream GETALLAccountPrime()
        {
            return ConvertDBtoMomery(accountNm.GETALLAccountPrime());
        }

        public MemoryStream GETALLAccountSub()
        {
            return ConvertDBtoMomery(accountNm.GETALLAccountSub());
        }

        public MemoryStream GetAllCurrency()
        {
            return ConvertDBtoMomery(accountNm.GetAllCurrency());
        }

        public MemoryStream GetBalanceAccount(int IDcode, int IDCurrncy, string NmIDcurrmcy)
        {
            return ConvertDBtoMomery(accountNm.GetBalanceAccount( IDcode,  IDCurrncy,  NmIDcurrmcy));
        }

        public MemoryStream GetBalanceAccountALLCunncy(int IDcode)
        {
            return ConvertDBtoMomery(accountNm.GetBalanceAccountALLCunncy(IDcode));
        }

        public string GETNMCurrncy(int IDCur)
        {
            return accountNm.GETNMCurrncy(IDCur);
        }

        public MemoryStream GetBalanceALLAccountALLCunncy(int idcurrncy)
        {
            return ConvertDBtoMomery(accountNm.GetBalanceALLAccountALLCunncy(idcurrncy));
        }

        public MemoryStream GETNMAccount(int IDCOde)
        {
            return ConvertDBtoMomery(accountNm.GETNMAccount(IDCOde));
        }

        public MemoryStream GETAccountDitalis(int IDcode, int IDCurnncy, DateTime d1, DateTime d2)
        {
            return ConvertDBtoMomery(accountNm.GETAccountDitalis(IDcode,  IDCurnncy, d1,  d2));
        }

        public int getOldMony(int IDcode, int IDCurnncy, DateTime d2)
        {
            return accountNm.getOldMony(IDcode, IDCurnncy, d2);
        }

        public MemoryStream GETAcountDitlis(int IDcode, int IDCurnncy, DateTime d1, DateTime d2)
        {
            return ConvertDBtoMomery(accountNm.GETAcountDitlis(IDcode, IDCurnncy, d1, d2));
        }

        public string GetUserNM(int IDuser)
        {
            return accountNm.GetUserNM(IDuser);
        }

        public MemoryStream GetGroupsAsAccounts()
        {
            return ConvertDBtoMomery(accountNm.GetGroupsAsAccounts());
        }

        public MemoryStream GetAccountesMOnayInGroup(int IDGroup)
        {
            return ConvertDBtoMomery(accountNm.GetAccountesMOnayInGroup(IDGroup));
        }

        public bool CheckAccontTotal(int IDcode, int IDCurrncy)
        {
            return accountNm.CheckAccontTotal(IDcode, IDCurrncy);
        }

        public int GetBalance(int Idcode, int IDCur)
        {
            return accountNm.GetBalance( Idcode, IDCur);
        }

        public int UpdateAccountTotal(int IDCOde, int Mony, int idCurrncy)
        {
            return accountNm.UpdateAccountTotal(IDCOde, Mony, idCurrncy);
        }

        public int AddNewAccountTotal(int IDCOde, int Mony, int idCurrncy)
        {
            return accountNm.AddNewAccountTotal(IDCOde, Mony, idCurrncy);
        }

        public int AddNewAccountDetalis(int idcode, int monay, int idsupply, int idout, string Detalis, DateTime d1, int userid, int idCurrnt, int IDSimple)
        {
            return accountNm.AddNewAccountDetalis(idcode, monay, idsupply, idout, Detalis, d1, userid, idCurrnt, IDSimple);
        }

        public int AddSimpleConstraint(int IDdaan, int IdMAden, int Mony, int idCurnncy, int UserId, DateTime datetime, string Note)
        {
            return accountNm.AddSimpleConstraint(IDdaan, IdMAden, Mony, idCurnncy, UserId, datetime, Note);
        }

        public int GetMaxIDSimpleConstraint()
        {
            return accountNm.GetMaxIDSimpleConstraint();
        }

        public MemoryStream GetAllSimpleConstraintOneDay(DateTime day1, DateTime day2)
        {
            return ConvertDBtoMomery(accountNm.GetAllSimpleConstraintOneDay(day1, day2));
        }

      
        #endregion
        //end Account
    }

}
