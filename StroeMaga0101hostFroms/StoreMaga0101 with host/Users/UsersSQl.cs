using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Users
{
public     class UsersSQl
    {
        
        MSqlConnection sql;
        public UsersSQl(string ServerNm,string DbNm,string UserSql,string PassSql)
        {
          
            
                if (string.IsNullOrEmpty(UserSql) || string.IsNullOrEmpty(PassSql))
                {
                   
                    sql = new MSqlConnection(ServerNm, DbNm);
                }

                else
                {

                    sql = new MSqlConnection(ServerNm, DbNm, UserSql, PassSql);
                }
            
      
        }
        ////// users
        // add new Users
        public int AddNewUser(string name, string user, string pass, bool Addsupply,bool UpdSupply ,bool Addoutt, bool updOut, bool PrintSupply,bool PrintOut, bool PrintQuntity,bool UpdSupp1,bool UpdOut1, bool userAdd, bool Active,bool Cate ,bool type1,bool account,bool Monay,bool Place, int UserID)
        {
            string Query= "INSERT INTO [Users](Name,UserName,Password,AddSupply,UpdSupply,AddOut,UpdOut,PrintSupply,PrintOut,PrintQuntity,AddUser,Active,UserID,UpdSupp1,UpdOut1,Cate,type1,account,Monay,Place) VALUES(@Name,@UserName,@Password,@AddSupply,@UpdSupply,@AddOut,@UpdOut,@PrintSupply,@PrintOut,@PrintQuntity,@AddUser,@Active,@UserID,@UpdSupp1,@UpdOut1,@Cate,@type1,@account,@Monay,@Place)";
            SqlParameter[] parm = new SqlParameter[20];
            parm[0] = new SqlParameter("@Name", name);
            parm[1] = new SqlParameter("@UserName", user);
            parm[2] = new SqlParameter("@Password", pass);
            parm[3] = new SqlParameter("@AddSupply", Addsupply);
            parm[4] = new SqlParameter("@UpdSupply", UpdSupply);
            parm[5] = new SqlParameter("@AddOut", Addoutt);
            parm[6] = new SqlParameter("@UpdOut", updOut);
            parm[7] = new SqlParameter("@PrintSupply", PrintSupply);
            parm[8] = new SqlParameter("@PrintOut", PrintOut);
            parm[9] = new SqlParameter("@PrintQuntity", PrintQuntity);
            parm[10] = new SqlParameter("@AddUser", userAdd);
            parm[11] = new SqlParameter("@Active", Active);
            parm[12] = new SqlParameter("@UserID", UserID);
            parm[13] = new SqlParameter("@UpdSupp1", UpdSupp1);
            parm[14] = new SqlParameter("@UpdOut1", UpdOut1);
            parm[15] = new SqlParameter("@Cate", Cate);
            parm[16] = new SqlParameter("@type1", type1);
            parm[17] = new SqlParameter("@account", account);
            parm[18] = new SqlParameter("@Monay", Monay);
            parm[19]= new SqlParameter("@Place", Place);
            return sql.ExcuteQuery(Query, parm);
        }
        /////////////////////
        //////
        /////
        public int UpatePassword(int idUser,string Pass)
        { 
            string Query = "Update Users set Password=@Password  where IDUSER=@IDUSER";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@Password", Pass);
            parm[1] = new SqlParameter("@IDUSER", idUser);
            return sql.ExcuteQuery(Query, parm);
        }
        ////////Updte Users
        public int UpdUsers(int IDUser, string name, string user, string pass, bool Addsupply, bool UpdSupply, bool Addoutt, bool updOut, bool PrintSupply, bool PrintOut, bool PrintQuntity, bool UpdSupp1, bool UpdOut1, bool userAdd, bool Active, bool Cate, bool type1, bool account, bool Monay, bool Place)
        {
            string Query= "Update Users set Name=@Name,UserName=@UserName,Password=@Password,AddSupply=@AddSupply,UpdSupply=@UpdSupply,AddOut=@AddOut,UpdOut=@UpdOut,PrintSupply=@PrintSupply,PrintOut=@PrintOut,PrintQuntity=@PrintQuntity,AddUser=@AddUser,Active=@Active , UpdSupp1=@UpdSupp1,UpdOut1=@UpdOut1,Cate=@Cate,type1=@type1,account=@account,Monay=@Monay,Place=@Place where IDUSER=@IDUSER";
            SqlParameter[] parm = new SqlParameter[20];
            parm[0] = new SqlParameter("@Name", name);
            parm[1] = new SqlParameter("@UserName", user);
            parm[2] = new SqlParameter("@Password", pass);
            parm[3] = new SqlParameter("@AddSupply", Addsupply);
            parm[4] = new SqlParameter("@UpdSupply", UpdSupply);
            parm[5] = new SqlParameter("@AddOut", Addoutt);
            parm[6] = new SqlParameter("@UpdOut", updOut);
            parm[7] = new SqlParameter("@PrintSupply", PrintSupply);
            parm[8] = new SqlParameter("@PrintOut", PrintOut);
            parm[9] = new SqlParameter("@PrintQuntity", PrintQuntity);
            parm[10] = new SqlParameter("@AddUser", userAdd);
            parm[11] = new SqlParameter("@Active", Active);
            parm[12] = new SqlParameter("@IDUSER", IDUser);
            parm[13] = new SqlParameter("@UpdSupp1", UpdSupp1);
            parm[14] = new SqlParameter("@UpdOut1", UpdOut1);
            parm[15] = new SqlParameter("@Cate", Cate);
            parm[16] = new SqlParameter("@type1", type1);
            parm[17] = new SqlParameter("@account", account);
            parm[18] = new SqlParameter("@Monay", Monay);
            parm[19] = new SqlParameter("@Place", Place);
          
            return sql.ExcuteQuery(Query, parm);

        }
        /////////////
       /////////

       //// Get All Users
         public DataTable GetAllUser()
        {
            string Query= " SELECT  [IDUSER] as 'الرقم' ,[Name] as 'اسم الموظف',[UserName] as 'اسم المستخدم' ,[Password] as 'كلمة المرور'  ,[AddSupply]as 'اضافة طلب توريد' ,[UpdSupply] as 'تعديل طلب توريد' ,[AddOut]as 'اضافة طلب صرف'  ,[UpdOut] as'تعديل طلب صرف' ,[PrintSupply]as'طباعة تقرير التوريد' ,[PrintOut] as 'طباعة تقرير الصرف' ,[PrintQuntity] as 'طباعة المخزون' ,UpdSupp1 as 'تعديلات الوارد' ,UpdOut1 as 'تعديلات الصرف' ,[AddUser] as 'اضافة مستخدم' ,[Active] as'تفعيل',Cate as 'نهيئة الاصناف' ,type1 as 'تهيئة الانواع',account as 'تهيئة الحسابات',Monay as 'تهيئة العملات',Place as'تهيئة الجهات',UserID as 'اسم الموظف'  FROM[Users]";
            return sql.SelectData(Query, null);
        }

        // get user
        public int LoginUser(string User, string Pass)
        {
            int check = 0;
           try
            {
              
                string Query= "select IDUSER from Users where UserName=@UserName and Password= @PassWord";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0]= new SqlParameter("@UserName", User);
                parm[1] = new SqlParameter("@PassWord", Pass);
    
                    check = (int)sql.ExcuteQueryValue(Query, parm);
                
              

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
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@IDUSER", IdUs);
            DataTable dt = new DataTable();
                             
             dt=  sql.SelectData(Query, parm);
                    
           
            return dt;
        }
    }
}
