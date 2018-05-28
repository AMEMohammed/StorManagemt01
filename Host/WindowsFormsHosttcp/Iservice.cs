using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Users;
namespace WindowsFormsHosttcp
{
    [ServiceContract]
    interface Iservice
    {
        [OperationContract]
        void SENDUSERTOSERVER(int falg,string name);
      
        ///Users
        #region
        [OperationContract]
        int LoginUser(string User, string Pass);
        [OperationContract]
        int AddNewUser(string name, string user, string pass, bool Addsupply, bool UpdSupply, bool Addoutt, bool updOut, bool PrintSupply, bool PrintOut, bool PrintQuntity, bool UpdSupp1, bool UpdOut1, bool userAdd, bool Active, bool Cate, bool type1, bool account, bool Monay, bool Place, int UserID);
        [OperationContract]
        int UpatePassword(int idUser, string Pass);
        [OperationContract]
        int UpdUsers(int IDUser, string name, string user, string pass, bool Addsupply, bool UpdSupply, bool Addoutt, bool updOut, bool PrintSupply, bool PrintOut, bool PrintQuntity, bool UpdSupp1, bool UpdOut1, bool userAdd, bool Active, bool Cate, bool type1, bool account, bool Monay, bool Place);
        [OperationContract]
        MemoryStream GetAllUser();
        [OperationContract]
        MemoryStream GetUser(int IdUs);
        
        #endregion
        //end Users
        //

        // Account  الحسابات
        #region
        [OperationContract]
        MemoryStream GetAllAccount();
        [OperationContract]
        MemoryStream GetAllAcountnAr();
        [OperationContract]
        int UpdateAccountNm(int iDAccounNm, string Name, bool active);
        [OperationContract]
        bool CheckAccountinDetlis(int idcode);
        [OperationContract]
        bool CheckAccounthaschalid(int idcode);
        [OperationContract]
        string TypeAccount(int IDAccount);
        [OperationContract]
        bool GetCheckAccount(int IDAccount);
        [OperationContract]
        bool GetCheckAccountHere(int IDAccount);
        [OperationContract]
        int GetMaxCode(int CodeParent);
        [OperationContract]
        int AddNewAcountNm(string AcountNm, int IdCOde, int IdParnt, string Type, int Active, DateTime DateStart, int UserId);
        [OperationContract]
        int DelteAccount(int IDCount);
        [OperationContract]
        int DelteAccount2(int IDcode);
        [OperationContract]
        MemoryStream SearchAcount(string name);
        [OperationContract]
        MemoryStream GETALLAccountPrime();
        [OperationContract]
        MemoryStream GETALLAccountSub();
        [OperationContract]
        MemoryStream GetAllCurrency();
        [OperationContract]
        MemoryStream GetBalanceAccount(int IDcode, int IDCurrncy, string NmIDcurrmcy);
        [OperationContract]
        MemoryStream GetBalanceAccountALLCunncy(int IDcode);
        [OperationContract]
        string GETNMCurrncy(int IDCur);
        [OperationContract]
        MemoryStream GetBalanceALLAccountALLCunncy(int idcurrncy);
        [OperationContract]
        MemoryStream GETNMAccount(int IDCOde);
        [OperationContract]
        MemoryStream GETAccountDitalis(int IDcode, int IDCurnncy, DateTime d1, DateTime d2);
        [OperationContract]
        int getOldMony(int IDcode, int IDCurnncy, DateTime d2);
        [OperationContract]
        MemoryStream GETAcountDitlis(int IDcode, int IDCurnncy, DateTime d1, DateTime d2);
        [OperationContract]
        string GetUserNM(int IDuser);
        [OperationContract]
        MemoryStream GetGroupsAsAccounts();
        [OperationContract]
        MemoryStream GetAccountesMOnayInGroup(int IDGroup);
        [OperationContract]
        bool CheckAccontTotal(int IDcode, int IDCurrncy);
        [OperationContract]
        int GetBalance(int Idcode, int IDCur);
        [OperationContract]
        int UpdateAccountTotal(int IDCOde, int Mony, int idCurrncy);
        [OperationContract]
        int AddNewAccountTotal(int IDCOde, int Mony, int idCurrncy);
        [OperationContract]
        int AddNewAccountDetalis(int idcode, int monay, int idsupply, int idout, string Detalis, DateTime d1, int userid, int idCurrnt, int IDSimple);
        [OperationContract]
        int AddSimpleConstraint(int IDdaan, int IdMAden, int Mony, int idCurnncy, int UserId, DateTime datetime, string Note);
        [OperationContract]
        int GetMaxIDSimpleConstraint();
        [OperationContract]
        MemoryStream GetAllSimpleConstraintOneDay(DateTime day1, DateTime day2);
        
        #endregion
        // end Account
    }
    [ServiceContract]
    interface IservicUsers
    {
       
       
    }
    [ServiceContract]
    interface Iservice2
    {

    }
    [ServiceContract]
    interface Iservice4
    {

    }
    [ServiceContract]
    interface Iservice5
    {


    }


}
