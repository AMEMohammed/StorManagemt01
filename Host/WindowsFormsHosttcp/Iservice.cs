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
        void SENDUSERTOSERVER(int falg, string name);

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
        
        // Out صرف
        #region
        [OperationContract]
        MemoryStream PrintRequstOut(int Check, int UserId, int user);
        [OperationContract]
        MemoryStream printrequstOutExit(int Check, int UserId, int user);
        [OperationContract]
        int GetIdUser(string NameUser);
        [OperationContract]
        MemoryStream GetCatagoryInAccount();
        [OperationContract]
        int GetAccountLinkCate(int IDcate);
        [OperationContract]
        MemoryStream GetAllPlace();
        [OperationContract]
        MemoryStream GetAllDebit();
        [OperationContract]
        MemoryStream GetAllCreditor();
        [OperationContract]
        MemoryStream SearchINRequstOutDate(DateTime d1, DateTime d2);
        [OperationContract]
        MemoryStream GetTypeInAccount(int IdCate);
        [OperationContract]
        MemoryStream GetCurrencyINAccount(int idcat, int idtyp);
        [OperationContract]
        MemoryStream GetAccountIDs(int IdCAte, int IdTpe, int idcurrnt);
        [OperationContract]
        int GetMaxCheckInRequsetOut();
        [OperationContract]
        int GetQuntityInAccount(int IDAcount);
        [OperationContract]
        int GetPriceAccount(int iDAccount);
        [OperationContract]
        int UpdateQuntityAccount(int IDAccount, int newquntity);
        [OperationContract]
        int AddNewRequstOut(int Quntity, int IDCategory, int IDType, int idcurrnt, int IDPlace, string NameOut, string DesOut, DateTime DateOut, int Chack, string NameSend, int price, int UserId, int debit, int cred);
        [OperationContract]
        int GetAndCheckQuntityAccountAndAddRqustNew(int IDAccount, int QuntityMust, int IDCategory, int IDType, int idcurrn, int IDPlace, string NameOut, string DesOut, DateTime DateOut, int Chack, string NameSend, int debit, int credi, int UserId, string NMIDCA, string NMTYpe, string NMPlus, string NMMins, string nmCurnncy);
        [OperationContract]
        int GetQunitiyinAccount2(int Idcae, int IdType, int idcurrnt);
        [OperationContract]
        MemoryStream SearchINRequsetOuttxt(string s);
        [OperationContract]
        MemoryStream SearchINRequsetOutTxtAndDate(int s, DateTime d1, DateTime d2);
        [OperationContract]
        MemoryStream SearchINRequsetOutTxtAndDate2(string s, DateTime d1, DateTime d2);
        [OperationContract]
        string GetUserNameBYIdUser(int IdUser);
        [OperationContract]
        MemoryStream GetRequstOutSngle(int IDOutRequst);
        [OperationContract]
        int AddNewUpdOut(int IDOut, int IdCate, int IdType, int IdPlace, int Quntity, string NameOUt, string NameSend, int Price, int IdCurrent, string TxtReson, DateTime DateUpdate, int UserId);
        [OperationContract]
        int CheckAccountIsHere(int IDCategory, int IDType, int price, int idcurrnt);
        [OperationContract]
        int DeleteRqustOut(int IdRequstOut, int IdUser);
        [OperationContract]
        int UpdateRequstOut(int IDOut, int IdPlace, string NameOut, string NameSend, string Reson, DateTime d1, int UserId, int debt, int crd);
        [OperationContract]
        int GetMAxIDOUt();
        [OperationContract]
        MemoryStream GetALLAcountNm();
        [OperationContract]
        bool CheckAccontTotalInOut(int IDcode, int IDCurrncy);
        [OperationContract]
        int AddNewAccountTotalInOut(int IDCOde, int Mony, int idCurrncy);
        [OperationContract]
        int GetBalanceInOut(int Idcode, int IDCur);
        [OperationContract]
        int UpdateAccountTotalInOut(int IDCOde, int Mony, int idCurrncy);
        [OperationContract]
        int AddNewAccountDetalisInOut(int idcode, int monay, int idsupply, int idout, string Detalis, DateTime d1, int userid, int idcurrn);
        [OperationContract]
        int DeleteSuuplyFrmAccountDitalis(int idSupply);
        [OperationContract]
        int DeleteSuuplyFrmAccountDitalis2(int idout);
        [OperationContract]
        int GetIDAccountPalce(int IDACcount, int idplace);

        #endregion
        //end Out 

        //Supply توريد
        #region
       [OperationContract]
        int GetIdUserINSupply(string NameUser);
        [OperationContract]
        string GetUserNameBYIdUserINSupply(int IdUser);
        [OperationContract]
        MemoryStream PrintRequstSupply(int IDreqSup, int UserId, int user);
        [OperationContract]
        MemoryStream printrequstOutExit1(int IDreqSup, int UserId, int user);
        [OperationContract]
        int GetAccountLinkCateInSupply(int IDcate);
        [OperationContract]
        int AddNewRequsetSupply(int IDCategory, int IDType, int Quntity, int Price, int idcurrnt, string NameSupply, string DescSupply, DateTime DateSupply, int IDuser, int chek, int debi, int cred);
        [OperationContract]
        int GetMaxIdSupply();
        [OperationContract]
        MemoryStream SearchINRequsetSupplyDate(DateTime d1, DateTime d2);
        [OperationContract]
        MemoryStream GetAllCreditorINSuplly();
        [OperationContract]
        int CheckAccountIsHereInSuplly(int IDCategory, int IDType, int price, int idcurrnt);
        [OperationContract]
        int UpdateQuntityAccountInSuplly(int IDAccount, int newquntity);
        [OperationContract]
        int AddNewAccount(int IDCategory, int IDType, int Quntity, int Price, int idcurrnt);
        [OperationContract]
        int GetMaxCheckSupply();
        [OperationContract]
        MemoryStream GetAllCategoryAR();
        [OperationContract]
        MemoryStream GetAllTypeQuntity();
        [OperationContract]
        MemoryStream GetAllCurrencyInSupply();
        [OperationContract]
        int GetQuntityInAccountInSupply(int IDAcount);
        [OperationContract]
        MemoryStream SearchINRequsetSupply(string txt);
        [OperationContract]
        MemoryStream SearchINRequsetSupplyTxtAndDate(string txt, DateTime d1, DateTime d2);
        [OperationContract]
        MemoryStream GetRequstSupply(int IDreqSup);
        [OperationContract]
        int CheckQuntityISHereInCheckQuntity(int IDCategory, int IDType);
        [OperationContract]
        int ADDNewUPDSupply(int IDSup, int IDCategory, int IDType, int Quntity, int Price, int idcunnt, string NameSupply, DateTime dateAdd, DateTime dateUpd, string decNew, int userid);
        [OperationContract]
        int DeleteRequstSupply(int Id);
        [OperationContract]
        int UPateRequstSupply(int IDSup, int IDCategory, int IDType, int Quntity, int Price, int idcurrn, string NameSupply, string DescSupply, int debit, int crd);
        [OperationContract]
        MemoryStream GetALLAcountNmInSupply();
        [OperationContract]
        bool CheckAccontTotalInSuuly(int IDcode, int IDCurrncy);
        [OperationContract]
        int AddNewAccountTotalInSuuply(int IDCOde, int Mony, int idCurrncy);
        [OperationContract]
        int GetBalanceInSupply(int Idcode, int IDCur);
        [OperationContract]
        int UpdateAccountTotalInSupply(int IDCOde, int Mony, int idCurrncy);
        [OperationContract]
        int AddNewAccountDetalisINSupply(int idcode, int monay, int idsupply, int idout, string Detalis, DateTime d1, int userid, int idCurrnt, int IDSimple);
        [OperationContract]
        int DeleteSuuplyFrmAccountDitalisInSupply(int idSupply);
        [OperationContract]
        int DeleteSuuplyFrmAccountDitalis2InSupply(int idout);

        #endregion
        // end Supply
        //Configtion الاعدادات
        #region
        [OperationContract]
        MemoryStream GetCategoryByName(string NMCate);
        [OperationContract]
        int AddNewCategory(string NameCategory, object IDAccount);
        [OperationContract]
        int GetMaxIDCate();
        [OperationContract]
        int UpdateCategory(int id, string name, object IDAccount);
        [OperationContract]
        int DeleteCategory(int id);
        [OperationContract]
        MemoryStream chackCatagory(int IDCA);
        [OperationContract]
        MemoryStream GetAllTypeQuntityInConf();
        [OperationContract]
        MemoryStream GetTypeQuntityByName(string nameTYPe);
        [OperationContract]
        int AddNewTypeQuntity(string name);
        [OperationContract]
        int UpdateTypeQuntity(int id, string name);
        [OperationContract]
        int DeleteQuntity(int id);
        [OperationContract]
        MemoryStream chackTapy(int IDtype);
        [OperationContract]
        MemoryStream GetPlaceByName(string NAMEPLACE);
        [OperationContract]
        int AddNewPlaceSend(string name);
        [OperationContract]
        int UpdatePlaceSend(int id, string name);
        [OperationContract]
        int DeletePlaceSend(int id);
        [OperationContract]
        MemoryStream  chackPlace(int IDplace);
        [OperationContract]
        MemoryStream GETCurrencyBYName(string NameCurrncy);
        [OperationContract]
        int AddNewCurrency(string name);
        [OperationContract]
        int UpdateCurrency(int id, string name);
        [OperationContract]
        int DeleteCurrency(int id);
        [OperationContract]
        MemoryStream chackCurncy(int IDcur);
        [OperationContract]
        int AddNewGroup(int GroupSourceID, string GroupName, string GroupDescription, int UserID, DateTime EnterTime);
        [OperationContract]
        MemoryStream GetOneGroup(int IDGroup);
        [OperationContract]
        MemoryStream GetAllGroup();
        [OperationContract]
        MemoryStream GetGroupByName(string NAME);
        [OperationContract]
        MemoryStream DeleteGroup(int IDGROUP);
        [OperationContract]
        bool CheckGroupItems(int IDGROUP);
        [OperationContract]
        MemoryStream GetSourecGroup();
        [OperationContract]
        int GetMaxIDGroup();
        [OperationContract]
        int UpdateGroup(int ID, int GroupSourceID, string GroupName, string GroupDescription, int UserID);
        [OperationContract]
        MemoryStream GetAllAccountSubNoInGroup(int GroupID);
        [OperationContract]
        MemoryStream GetAllAccountSubInGroup(int GroupID);
        [OperationContract]
        int DeleteItemsONGroupDetalis(int IDGroup);
        [OperationContract]
        int AddItemsONGroupDetalis(int IDGroup, int IDItems, int UserID);
        [OperationContract]
        int AddConnectionAccountwithPlace(int palce, int idMadeen, int idDaan);
        [OperationContract]
        int UpdateConnectionAccountwithPlace(int ID, int palce, int idMadeen, int idDaan);
        [OperationContract]
        int DeleteConnectionAccountwithPlace(int ID);
        [OperationContract]
        MemoryStream GetConnectionAccountwithPlace();
        #endregion
        //ENd Configtion
        //frmWInReprting الطباعة
        #region
        [OperationContract]   
        MemoryStream GetGroupsCate();
        [OperationContract]
        MemoryStream GetAllUserinReprt()
        [OperationContract]
        MemoryStream GetAllUserAR();
        [OperationContract]
        MemoryStream PrintRequstRPT(DateTime d1, DateTime d2, int IDCate, int IDType, int IDCurrn, string txt, int iduser);
        [OperationContract]
        MemoryStream PrintRequstRPTAll(DateTime d1, DateTime d2, string txt);
        [OperationContract]
        MemoryStream PrintRequstRPTIDcat(DateTime d1, DateTime d2, string txt, int IDCate);
        [OperationContract]
        MemoryStream PrintOutAllwithDate(int idca, int idtyp, int idpalce, int idcurrnt, string name, DateTime d1, DateTime d2, int iduser);
        [OperationContract]
        MemoryStream PrintOutAllwithDateAll(string name, DateTime d1, DateTime d2);
        [OperationContract]
        MemoryStream PrintOutAllwithDateWithIDca(string name, DateTime d1, DateTime d2, int idca);
        [OperationContract]
        MemoryStream PrintOutAllwithDateWithIDcaPLAC(string name, DateTime d1, DateTime d2, int idca, int idpalce);
        [OperationContract]
        MemoryStream PrintOutAllwithDateWithPLAC(string name, DateTime d1, DateTime d2, int idpalce);
        [OperationContract]
        MemoryStream PrintAccountQuntity(int idcat, int idtyp, int idcu);
        [OperationContract]
        MemoryStream PrintAccountQuntityIDac(int idcu);
        [OperationContract]
        MemoryStream PrintAccountQuntityWithGroup(int IDGroup);
        [OperationContract]
        MemoryStream PrintAccountQuntityAll();
        [OperationContract]
        MemoryStream GetUpdateSupplyByIDSupply(int Id);
        [OperationContract]
        MemoryStream GetUpdateSupplyByDate(DateTime d1, DateTime d2);
        [OperationContract]
        MemoryStream GetUpdateSupplyByDateUpdateWithDate(DateTime d1, DateTime d2);
        [OperationContract]
        MemoryStream GetUpdateSupplyByDateDeleteWithDate(DateTime d1, DateTime d2);
        [OperationContract]
        MemoryStream GetUpdtOutByIDOut(int idOUt);
        [OperationContract]
        MemoryStream GetUpdOutByDate(DateTime d1, DateTime d2);
        [OperationContract]
        MemoryStream GetUpdOutByDateUpdtewithdate(DateTime d1, DateTime d2);
        [OperationContract]
        MemoryStream GetUpdOutByDateDetle2tewithdate(DateTime d1, DateTime d2);
       
        #endregion
        // end frmWInReprting
    }

}
    



