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
