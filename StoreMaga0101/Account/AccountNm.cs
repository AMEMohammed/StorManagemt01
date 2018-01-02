using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace Account
{ 
    class AccountNm
    {
        MSqlConnection sql;

      public  AccountNm(string SerNm,string DBNm,string UserSql,string PassSql)
        {
            if(UserSql ==null || PassSql ==null)
            {
                sql = new MSqlConnection(SerNm, DBNm);
            }
            else
            {
                sql = new MSqlConnection(SerNm, DBNm, UserSql, PassSql);
            }

            
        }
        /// <summary>
        /// ///Get all Acount
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllAccount()
        {
            string Query = "select * from AccountNm";
            return sql.SelectData(Query, null);
        }
        /// <summary>
        /// Get ALL Acount
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllAcountnAr()
        {
            string Query = "select t.IDCode as'رقم الحساب' ,t.AcountNm as 'اسم الحساب ' ,t.AcountType as'نوع الحساب' ,s.AcountNm as'الحساب الاب',s.IDCode as 'رقم حساب الاب' ,t.Active as 'تفعيل' ,t.StartEnter as 'تاريخ الادخال' ,Users.Name as 'اسم الموظف' ,t.IDAcountNm from AccountNm as s, AccountNm as t,Users where t.IDParentAcount = s.IDCode and Users.IDUSER = t.UserID order by IDAcountNm";
            return sql.SelectData(Query, null);
        }
        /// <summary>
        /// //Update Account
        /// </summary>
        /// <param name="iDAccounNm"></param>
        /// <param name="Name"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public int UpdateAccountNm(int iDAccounNm,string Name,bool active)
        { int x = 0;
            if (active==true)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            string Query = "Update AccountNm set AcountNm=@namee ,Active=@x where IDAcountNm=@id1";
            
           SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@id1", iDAccounNm);
             parm[1] = new SqlParameter("@namee", Name);
               parm[2] = new SqlParameter("@x", x);

            return sql.ExcuteQuery(Query, parm);
        }
        /// <summary>
        /// GetTYpe Account
        /// </summary>
        /// <param name="IDAccount"></param>
        /// <returns></returns>
        public string TypeAccount(int IDAccount)
        {
            string Query = "select AcountType from AccountNm where IDAcountNm=@id ";
            SqlParameter[] parm=new SqlParameter[1];
            parm[0] = new SqlParameter("@id", IDAccount);
            return (string)sql.ExcuteQueryValue(Query, parm);
        }
        /// <summary>
        /// Get Check Account
        /// </summary>
        /// <param name="IDAccount"></param>
        /// <returns></returns>
        public bool GetCheckAccount(int IDAccount)
        {
            string Query = "select Active from AccountNm where IDAcountNm=@id ";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", IDAccount);
            return (bool)sql.ExcuteQueryValue(Query, parm);
        }
        /// <summary>
        /// /Get Max IDCODde
        /// </summary>
        /// <param name="CodeParent"></param>
        /// <returns></returns>
        public int GetMaxCode (int CodeParent)
        { int x =0 ;
            try
            {
                string Query = " select max(IDCode) from AccountNm where IDParentAcount=@idPanent";
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@idPanent", CodeParent);
                return (int)sql.ExcuteQueryValue(Query, parm);
            }
            catch
            {
                string s = CodeParent.ToString() + "00";
                return Convert.ToInt32(s);
            }
            
        }
        /// <summary>
        /// Add Acount
        /// </summary>
        /// <param name="AcountNm"></param>
        /// <param name="IdCOde"></param>
        /// <param name="IdParnt"></param>
        /// <param name="Type"></param>
        /// <param name="Active"></param>
        /// <param name="DateStart"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int AddNewAcountNm(string AcountNm,int IdCOde,int IdParnt,string Type,int Active,DateTime DateStart,int UserId)
        { 
            string Query = "insert into AccountNm (AcountNm,IDCode,IDParentAcount,AcountType,Active,StartEnter,UserID) values(@AcountNm,@IDCode,@IDParentAcount,@AcountType,@Active,@StartEnter,@UserID)";
            SqlParameter[] parm = new SqlParameter[7];
            parm[0] = new SqlParameter("@AcountNm", AcountNm);
            parm[1] = new SqlParameter("@IDCode", IdCOde);
            parm[2] = new SqlParameter("@IDParentAcount", IdParnt);
            parm[3] = new SqlParameter("@AcountType", Type);
            parm[4] = new SqlParameter("@Active", Active);
            parm[5] = new SqlParameter("@StartEnter", DateStart);
            parm[6] = new SqlParameter("UserID", UserId);
            return sql.ExcuteQuery(Query, parm);



        }
      ////////////
      /// <summary>
      /// detel Accoumt
      /// </summary>
      /// <param name="IDCount"></param>
      /// <returns></returns>
      /////////////
      public int DelteAccount(int IDCount)
        {
            string Query = "delete from AccountNm where IDAcountNm=@id";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", IDCount);
            return sql.ExcuteQuery(Query, parm);
        }


        /////////
        /// Search AcoutnNm
        /// 
        public DataTable SearchAcount(string name)
        {
            name = "%" + name + "%";
            string Query = "select t.IDCode as'رقم الحساب' ,t.AcountNm as 'اسم الحساب ' ,t.AcountType as'نوع الحساب' ,s.AcountNm as'الحساب الاب',s.IDCode as 'رقم حساب الاب' ,t.Active as 'تفعيل' ,t.StartEnter as 'تاريخ الادخال' ,Users.Name as 'اسم الموظف' ,t.IDAcountNm from AccountNm as s, AccountNm as t,Users where t.IDParentAcount = s.IDCode and Users.IDUSER = t.UserID and t.AcountNm like @name order by IDAcountNm";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@name", name);
            return sql.SelectData(Query, parm); 

        }
        ////////////////////////////
        //
    }
}
