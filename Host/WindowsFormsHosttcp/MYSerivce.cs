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
using Out_;
namespace WindowsFormsHosttcp
{ [ServiceBehavior(InstanceContextMode =InstanceContextMode.Single)]
    class MYSerivce : Iservice
    {   /// Classes
        // Users    
        UsersSQl users = new UsersSQl(Properties.Settings.Default.ServerNm, Properties.Settings.Default.DBNM, null, null);
        // Account
        AccountNm accountNm = new AccountNm(Properties.Settings.Default.ServerNm, Properties.Settings.Default.DBNM, null, null);
        //Out
        OutFunction OutFun = new OutFunction(Properties.Settings.Default.ServerNm, Properties.Settings.Default.DBNM, null, null);
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
        //
        //Out
        #region 
        public MemoryStream PrintRequstOut(int Check, int UserId, int user)
        {
            return ConvertDBtoMomery(OutFun.PrintRequstOut(Check, UserId, user));
        }

        public MemoryStream printrequstOutExit(int Check, int UserId, int user)
        {
            return ConvertDBtoMomery(OutFun.printrequstOutExit(Check, UserId, user));
        }

        public int GetIdUser(string NameUser)
        {
            return OutFun.GetIdUser(NameUser);
        }

        public MemoryStream GetCatagoryInAccount()
        {
            return ConvertDBtoMomery(OutFun.GetCatagoryInAccount());
        }

        public int GetAccountLinkCate(int IDcate)
        {
            return OutFun.GetAccountLinkCate(IDcate);
        }

        public MemoryStream GetAllPlace()
        {
            return ConvertDBtoMomery(OutFun.GetAllPlace());
        }

        public MemoryStream GetAllDebit()
        {
            return ConvertDBtoMomery(OutFun.GetAllDebit());
        }

        public MemoryStream GetAllCreditor()
        {
            return ConvertDBtoMomery(OutFun.GetAllCreditor());
        }

        public MemoryStream SearchINRequstOutDate(DateTime d1, DateTime d2)
        {
            return ConvertDBtoMomery(OutFun.SearchINRequstOutDate(d1, d2));
        }

        public MemoryStream GetTypeInAccount(int IdCate)
        {
            return ConvertDBtoMomery(OutFun.GetTypeInAccount(IdCate));
        }

        public MemoryStream GetCurrencyINAccount(int idcat, int idtyp)
        {
            return ConvertDBtoMomery(OutFun.GetCurrencyINAccount(idcat, idtyp));
        }

        public MemoryStream GetAccountIDs(int IdCAte, int IdTpe, int idcurrnt)
        {
            return ConvertDBtoMomery(OutFun.GetAccountIDs(IdCAte, IdTpe, idcurrnt));
        }

        public int GetMaxCheckInRequsetOut()
        {
            return OutFun.GetMaxCheckInRequsetOut();
        }

        public int GetQuntityInAccount(int IDAcount)
        {
            return OutFun.GetQuntityInAccount(IDAcount);
        }

        public int GetPriceAccount(int iDAccount)
        {
            return OutFun.GetPriceAccount(iDAccount);
        }

        public int UpdateQuntityAccount(int IDAccount, int newquntity)
        {
            return OutFun.UpdateQuntityAccount(IDAccount, newquntity);
        }

        public int AddNewRequstOut(int Quntity, int IDCategory, int IDType, int idcurrnt, int IDPlace, string NameOut, string DesOut, DateTime DateOut, int Chack, string NameSend, int price, int UserId, int debit, int cred)
        {
            return OutFun.AddNewRequstOut( Quntity,  IDCategory,  IDType, idcurrnt, IDPlace,  NameOut,  DesOut,  DateOut,  Chack,  NameSend,  price,  UserId,  debit, cred);
        }

        public int GetAndCheckQuntityAccountAndAddRqustNew(int IDAccount, int QuntityMust, int IDCategory, int IDType, int idcurrn, int IDPlace, string NameOut, string DesOut, DateTime DateOut, int Chack, string NameSend, int debit, int credi, int UserId, string NMIDCA, string NMTYpe, string NMPlus, string NMMins, string nmCurnncy)
        {
            return OutFun.GetAndCheckQuntityAccountAndAddRqustNew( IDAccount,  QuntityMust, IDCategory, IDType,  idcurrn, IDPlace,  NameOut,  DesOut,  DateOut,  Chack,  NameSend, debit,  credi,  UserId,  NMIDCA, NMTYpe,  NMPlus,  NMMins, nmCurnncy);
        }

        public int GetQunitiyinAccount2(int Idcae, int IdType, int idcurrnt)
        {
            return OutFun.GetQunitiyinAccount2(Idcae,  IdType,  idcurrnt);
        }

        public MemoryStream SearchINRequsetOuttxt(string s)
        {
            return ConvertDBtoMomery(OutFun.SearchINRequsetOuttxt( s));
        }

        public MemoryStream SearchINRequsetOutTxtAndDate(int s, DateTime d1, DateTime d2)
        {
            return ConvertDBtoMomery(OutFun.SearchINRequsetOutTxtAndDate(s, d1, d2));
        }

        public MemoryStream SearchINRequsetOutTxtAndDate2(string s, DateTime d1, DateTime d2)
        {
            return ConvertDBtoMomery(OutFun.SearchINRequsetOutTxtAndDate2(s, d1, d2));
        }

        public string GetUserNameBYIdUser(int IdUser)
        {
            return OutFun.GetUserNameBYIdUser(IdUser);
        }

        public MemoryStream GetRequstOutSngle(int IDOutRequst)
        {
            return ConvertDBtoMomery(OutFun.GetRequstOutSngle(IDOutRequst));
        }

        public int AddNewUpdOut(int IDOut, int IdCate, int IdType, int IdPlace, int Quntity, string NameOUt, string NameSend, int Price, int IdCurrent, string TxtReson, DateTime DateUpdate, int UserId)
        {
            return OutFun.AddNewUpdOut( IDOut,  IdCate,IdType,  IdPlace,  Quntity,  NameOUt,  NameSend,  Price,  IdCurrent,  TxtReson,  DateUpdate, UserId);
        }

        public int CheckAccountIsHere(int IDCategory, int IDType, int price, int idcurrnt)
        {
            return OutFun.CheckAccountIsHere( IDCategory, IDType,  price,  idcurrnt);
        }

        public int DeleteRqustOut(int IdRequstOut, int IdUser)
        {
            return OutFun.DeleteRqustOut(IdRequstOut, IdUser);
        }

        public int UpdateRequstOut(int IDOut, int IdPlace, string NameOut, string NameSend, string Reson, DateTime d1, int UserId, int debt, int crd)
        {
            return OutFun.UpdateRequstOut( IDOut, IdPlace,  NameOut,  NameSend,  Reson,  d1,  UserId,  debt,  crd);
        }

        public int GetMAxIDOUt()
        {
            return OutFun.GetMAxIDOUt();
        }

        public MemoryStream GetALLAcountNm()
        {
            return ConvertDBtoMomery(OutFun.GetALLAcountNm());
        }

        public bool CheckAccontTotalInOut(int IDcode, int IDCurrncy)
        {
            return OutFun.CheckAccontTotal( IDcode,  IDCurrncy);
        }

        public int AddNewAccountTotalInOut(int IDCOde, int Mony, int idCurrncy)
        {
            return OutFun.AddNewAccountTotal(IDCOde,  Mony,  idCurrncy);
        }

        public int GetBalanceInOut(int Idcode, int IDCur)
        {
            return OutFun.GetBalance( Idcode,  IDCur);
        }

        public int UpdateAccountTotalInOut(int IDCOde, int Mony, int idCurrncy)
        {
            return OutFun.UpdateAccountTotal( IDCOde,  Mony,  idCurrncy);
        }

        public int AddNewAccountDetalisInOut(int idcode, int monay, int idsupply, int idout, string Detalis, DateTime d1, int userid, int idcurrn)
        {
            return OutFun.AddNewAccountDetalis( idcode,monay,  idsupply,  idout,  Detalis,  d1,  userid,  idcurrn);
        }

        public int DeleteSuuplyFrmAccountDitalis(int idSupply)
        {
            return OutFun.DeleteSuuplyFrmAccountDitalis( idSupply);
        }

        public int DeleteSuuplyFrmAccountDitalis2(int idout)
        {
            return OutFun.DeleteSuuplyFrmAccountDitalis2(idout);
        }

        public int GetIDAccountPalce(int IDACcount, int idplace)
        {
            return OutFun.GetIDAccountPalce( IDACcount,  idplace);

        }


        #endregion
        //end Out
    }

}
