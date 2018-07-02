using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
namespace Users
{
    class UsersAccess
    {



           AccessConnection Acc;
            public UsersAccess(string PathAccess)
            {
              Acc = new AccessConnection(PathAccess);
             
            }
            ////// users
            // add new Users
            public int AddNewUser(string name, string user, string pass, bool Addsupply, bool UpdSupply, bool Addoutt, bool updOut, bool PrintSupply, bool PrintOut, bool PrintQuntity, bool UpdSupp1, bool UpdOut1, bool userAdd, bool Active, bool Cate, bool type1, bool account, bool Monay, bool Place, int UserID)
            {
                string Query = "INSERT INTO [Users](Name,UserName,Password,AddSupply,UpdSupply,AddOut,UpdOut,PrintSupply,PrintOut,PrintQuntity,AddUser,Active,UserID,UpdSupp1,UpdOut1,Cate,type1,account,Monay,Place) VALUES(@Name,@UserName,@Password,@AddSupply,@UpdSupply,@AddOut,@UpdOut,@PrintSupply,@PrintOut,@PrintQuntity,@AddUser,@Active,@UserID,@UpdSupp1,@UpdOut1,@Cate,@type1,@account,@Monay,@Place)";
                OleDbParameter[] parm = new OleDbParameter[20];
                parm[0] = new OleDbParameter("@Name", name);
                parm[1] = new OleDbParameter("@UserName", user);
                parm[2] = new OleDbParameter("@Password", pass);
                parm[3] = new OleDbParameter("@AddSupply", Addsupply);
                parm[4] = new OleDbParameter("@UpdSupply", UpdSupply);
                parm[5] = new OleDbParameter("@AddOut", Addoutt);
                parm[6] = new OleDbParameter("@UpdOut", updOut);
                parm[7] = new OleDbParameter("@PrintSupply", PrintSupply);
                parm[8] = new OleDbParameter("@PrintOut", PrintOut);
                parm[9] = new OleDbParameter("@PrintQuntity", PrintQuntity);
                parm[10] = new OleDbParameter("@AddUser", userAdd);
                parm[11] = new OleDbParameter("@Active", Active);
                parm[12] = new OleDbParameter("@UserID", UserID);
                parm[13] = new OleDbParameter("@UpdSupp1", UpdSupp1);
                parm[14] = new OleDbParameter("@UpdOut1", UpdOut1);
                parm[15] = new OleDbParameter("@Cate", Cate);
                parm[16] = new OleDbParameter("@type1", type1);
                parm[17] = new OleDbParameter("@account", account);
                parm[18] = new OleDbParameter("@Monay", Monay);
                parm[19] = new OleDbParameter("@Place", Place);
                return Acc.ExcuteQuery(Query, parm);
            }
            /////////////////////
            //////
            /////
            public int UpatePassword(int idUser, string Pass)
            {
                string Query = "Update Users set Password=@Password  where IDUSER=@IDUSER";
                OleDbParameter[] parm = new OleDbParameter[2];
                parm[0] = new OleDbParameter("@Password", Pass);
                parm[1] = new OleDbParameter("@IDUSER", idUser);
                return Acc.ExcuteQuery(Query, parm);
            }
            ////////Updte Users
            public int UpdUsers(int IDUser, string name, string user, string pass, bool Addsupply, bool UpdSupply, bool Addoutt, bool updOut, bool PrintSupply, bool PrintOut, bool PrintQuntity, bool UpdSupp1, bool UpdOut1, bool userAdd, bool Active, bool Cate, bool type1, bool account, bool Monay, bool Place)
            {
                string Query = "Update Users set Name=@Name,UserName=@UserName,Password=@Password,AddSupply=@AddSupply,UpdSupply=@UpdSupply,AddOut=@AddOut,UpdOut=@UpdOut,PrintSupply=@PrintSupply,PrintOut=@PrintOut,PrintQuntity=@PrintQuntity,AddUser=@AddUser,Active=@Active , UpdSupp1=@UpdSupp1,UpdOut1=@UpdOut1,Cate=@Cate,type1=@type1,account=@account,Monay=@Monay,Place=@Place where IDUSER=@IDUSER";
                OleDbParameter[] parm = new OleDbParameter[20];
                parm[0] = new OleDbParameter("@Name", name);
                parm[1] = new OleDbParameter("@UserName", user);
                parm[2] = new OleDbParameter("@Password", pass);
                parm[3] = new OleDbParameter("@AddSupply", Addsupply);
                parm[4] = new OleDbParameter("@UpdSupply", UpdSupply);
                parm[5] = new OleDbParameter("@AddOut", Addoutt);
                parm[6] = new OleDbParameter("@UpdOut", updOut);
                parm[7] = new OleDbParameter("@PrintSupply", PrintSupply);
                parm[8] = new OleDbParameter("@PrintOut", PrintOut);
                parm[9] = new OleDbParameter("@PrintQuntity", PrintQuntity);
                parm[10] = new OleDbParameter("@AddUser", userAdd);
                parm[11] = new OleDbParameter("@Active", Active);
                parm[12] = new OleDbParameter("@IDUSER", IDUser);
                parm[13] = new OleDbParameter("@UpdSupp1", UpdSupp1);
                parm[14] = new OleDbParameter("@UpdOut1", UpdOut1);
                parm[15] = new OleDbParameter("@Cate", Cate);
                parm[16] = new OleDbParameter("@type1", type1);
                parm[17] = new OleDbParameter("@account", account);
                parm[18] = new OleDbParameter("@Monay", Monay);
                parm[19] = new OleDbParameter("@Place", Place);

                return Acc.ExcuteQuery(Query, parm);

            }
            /////////////
            /////////

            //// Get All Users
            public DataTable GetAllUser()
            {
                string Query = " SELECT  [IDUSER] as 'الرقم' ,[Name] as 'اسم الموظف',[UserName] as 'اسم المستخدم' ,[Password] as 'كلمة المرور'  ,[AddSupply]as 'اضافة طلب توريد' ,[UpdSupply] as 'تعديل طلب توريد' ,[AddOut]as 'اضافة طلب صرف'  ,[UpdOut] as'تعديل طلب صرف' ,[PrintSupply]as'طباعة تقرير التوريد' ,[PrintOut] as 'طباعة تقرير الصرف' ,[PrintQuntity] as 'طباعة المخزون' ,UpdSupp1 as 'تعديلات الوارد' ,UpdOut1 as 'تعديلات الصرف' ,[AddUser] as 'اضافة مستخدم' ,[Active] as'تفعيل',Cate as 'نهيئة الاصناف' ,type1 as 'تهيئة الانواع',account as 'تهيئة الحسابات',Monay as 'تهيئة العملات',Place as'تهيئة الجهات',UserID as 'اسم الموظف'  FROM[Users]";
                return Acc.SelectData(Query, null);
            }

            // get user
            public int LoginUser(string User, string Pass)
            {
                int check = 0;
                try
                {

                    string Query = "select IDUSER from Users where UserName=@UserName and Password= @PassWord";
                    OleDbParameter[] parm = new OleDbParameter[2];
                    parm[0] = new OleDbParameter("@UserName", User);
                    parm[1] = new OleDbParameter("@PassWord", Pass);

                    check = (int)Acc.ExcuteQueryValue(Query, parm);



                }
                catch
                {
                    check = 0;

                }

                return check;

            }

            /// GEt AllUsers 
            public DataTable GetUser(int IdUs)
            {
                string Query = " SELECT  IDUSER,Name,UserName,Password,AddSupply,UpdSupply,AddOut,UpdOut,PrintSupply,PrintOut,PrintQuntity,AddUser,Active ,UpdSupp1,UpdOut1,Cate,type1,account,Monay,Place    FROM[Users] where IDUSER=@IDUSER ";
                OleDbParameter[] parm = new OleDbParameter[1];
                parm[0] = new OleDbParameter("@IDUSER", IdUs);
                DataTable dt = new DataTable();

                dt = Acc.SelectData(Query, parm);


                return dt;
            }
            /// get user name
            public string GetUserNameByID(int IDUSER)
            {
                string Query = "select UserName from Users where IDUSER=@IDUSER ";
                OleDbParameter[] parm = new OleDbParameter[1];
                parm[0] = new OleDbParameter("@IDUSER", IDUSER);
                return (string)Acc.ExcuteQueryValue(Query, parm);
            }
            /// get user name
            public string GetUserNameByID2(int IDUSER)
            {
                string Query = "select Name from Users where IDUSER=@IDUSER ";
                OleDbParameter[] parm = new OleDbParameter[1];
                parm[0] = new OleDbParameter("@IDUSER", IDUSER);
                return (string)Acc.ExcuteQueryValue(Query, parm);
            }
        
    }
}
