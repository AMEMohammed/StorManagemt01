using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace Supplly
{
    public class SupplyRequset

    {
        MSqlConnection sql;
      
       
        public SupplyRequset(string serv, string db,string user,string pass)
        { if(user ==null || pass==null)
                sql = new MSqlConnection(serv, db);
            else
            sql = new MSqlConnection(serv, db,user,pass);
         
        }
        public int GetIdUser(string NameUser)
        {


            string Query = "select UserID from Users where Name=@name";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@name", NameUser);
            int re = (int)sql.ExcuteQueryValue(Query, parm);

            return re;

        }
        ////////////////////////////////////////////
        /////// GetRequstSupply

        /// add new Requst Supply
        /////////////////////
        ////////////////////////////////////////////
        /////// GetRequstSupply
        /// 
        public string GetUserNameBYIdUser(int IdUser)
        {
           
            string Query="select Name from Users where UserID=@userid";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0]=new SqlParameter("@userid", IdUser);
            return (string)sql.ExcuteQueryValue(Query, parm);
           
        }
        public DataTable PrintRequstSupply(int IDreqSup, int UserId, int user)
        {
          
           string Query="select IDSupply as 'رقم الطلب' ,  Category.NameCategory  as 'الصنف', TypeQuntity.NameType  as'النوع' , RequstSupply.Quntity  as 'الكمية', RequstSupply.Price as 'السعر'  , RequstSupply.Quntity * RequstSupply.Price as 'الاجمالي' ,Currency.NameCurrency as 'العملة', RequstSupply.DateSupply as 'تاريخ' , RequstSupply.NameSupply  as'اسم المورد',Users.Name as 'اسم الموظف', RequstSupply.DescSupply AS 'ملاحظات',Debit.NameTypeAccount as 'مدين' ,Creditor.NameTypeAccount as 'دائن'  from Debit,Creditor ,Category,Users,TypeQuntity, RequstSupply,Currency where RequstSupply.IDCategory = Category.IDCategory and Debit.IdTypeAccount=RequstSupply.Debit and Creditor.IdTypeAccount=RequstSupply.Creditor and RequstSupply.IDType = TypeQuntity.IDType and RequstSupply.IDCurrency=Currency.IDCurrency and Users.UserID=@UserId  and RequstSupply.chek =@id and RequstSupply.UserId =@uuu ";
            SqlParameter[] parm = new SqlParameter[3];
           parm[0]=new SqlParameter("@id", IDreqSup);
            parm[1]=new SqlParameter("@UserId", UserId);
            parm[2] = new SqlParameter("@uuu", user);
            return sql.SelectData(Query, parm);

        }
    

        public DataTable printrequstOutExit1(int IDreqSup, int UserId, int user)
        {
            string Query="select IDSupply as 'الرقم المخزني' ,  Category.NameCategory  as 'الاسم', TypeQuntity.NameType  as'النوع' , RequstSupply.Quntity  as 'الكمية',    RequstSupply.NameSupply  as'اسم المستلم',Users.Name as 'اسم الموظف' , Users.Name as 'العنوان' ,Users.Name as 'الجهة'  from Debit,Creditor ,Category,Users,TypeQuntity, RequstSupply,Currency where RequstSupply.IDCategory = Category.IDCategory and Debit.IdTypeAccount=RequstSupply.Debit and Creditor.IdTypeAccount=RequstSupply.Creditor and RequstSupply.IDType = TypeQuntity.IDType and RequstSupply.IDCurrency=Currency.IDCurrency and Users.UserID=@UserId  and RequstSupply.chek =@id and RequstSupply.UserId =@uuu ";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0]=new SqlParameter("@id", IDreqSup);
            parm[1]=new SqlParameter("@UserId", UserId);
            parm[2]=new SqlParameter("@uuu", user);
            return sql.SelectData(Query, parm);
        }

        public int AddNewRequsetSupply(int IDCategory, int IDType, int Quntity, int Price, int idcurrnt, string NameSupply, string DescSupply, DateTime DateSupply, int IDuser, int chek, int debi, int cred)
        {
            int resl = 0;
            string Query = "insert into RequstSupply(IDCategory,IDType,Quntity,Price,NameSupply,DescSupply,DateSupply,IDCurrency,UserId,chek,Debit,Creditor) values(@IDCategory,@IDType,@Quntity,@Price,@NameSupply,@DescSupply,@DateSupply,@IDCurrency,@userId,@chek,@deb,@crd)";
            SqlParameter[] parm = new SqlParameter[12];
            parm[0] = new SqlParameter("@IDCategory", IDCategory);
            parm[1] = new SqlParameter("@IDType", IDType);
            parm[2] = new SqlParameter("@Quntity", Quntity);
            parm[3] = new SqlParameter("@Price", Price);
            parm[4] = new SqlParameter("@NameSupply", NameSupply);
            parm[5] = new SqlParameter("@DescSupply", DescSupply);
            parm[6] = new SqlParameter("@DateSupply", DateSupply);
            parm[7] = new SqlParameter("@IDCurrency", idcurrnt);
            parm[8] = new SqlParameter("@userId", IDuser);
            parm[9] = new SqlParameter("@chek", chek);
            parm[10] = new SqlParameter("@deb", debi);
            parm[11] = new SqlParameter("@crd", cred);
           return resl=   sql.ExcuteQuery(Query, parm);
        }
        public int GetMaxIdSupply()
        {
            string Query = " select MAX(RequstSupply.IDSupply) from RequstSupply";
            return (int)sql.ExcuteQueryValue(Query, null);
        }
        //////////////////////
        ////
       public DataTable    SearchINRequsetSupplyDate(DateTime d1,DateTime d2)
        {
            string Query = "select IDSupply as 'رقم الطلب' ,Category.NameCategory as 'اسم الصنف', TypeQuntity.NameType as 'نوع الكمية', RequstSupply.Quntity as 'الكمية', RequstSupply.Price as 'سعر الوحدة', RequstSupply.Quntity * RequstSupply.Price as 'الاجمالي',Currency.NameCurrency as 'العملة',RequstSupply.DateSupply as'تاريخ التوريد', RequstSupply.NameSupply as 'اسم المورد',Users.Name as 'اسم الموظف', RequstSupply.DescSupply as 'ملاحظات',RequstSupply.chek  from Category, TypeQuntity, RequstSupply,Currency, Users where RequstSupply.UserId=Users.UserID and  RequstSupply.IDCategory = Category.IDCategory and RequstSupply.IDCurrency=Currency.IDCurrency and RequstSupply.IDType = TypeQuntity.IDType and DateSupply between @d1 and @d2  order by RequstSupply.DateSupply,RequstSupply.IDCurrency,RequstSupply.IDType,RequstSupply.IDCategory";
                  DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@d1", d1);
            parm[1] = new SqlParameter("@d2", d2);
          dt=  sql.SelectData(Query, parm);
            return dt;
        }
        //////////////
        public DataTable GetAllCreditor()
        {
            DataTable dt = new DataTable();
            string Query = "select  IdTypeAccount as 'الرقم' ,NameTypeAccount as 'نوع الحساب' from Creditor";
            dt = sql.SelectData(Query, null);
             return dt;

        }
        ////////////////
        public int CheckAccountIsHere(int IDCategory, int IDType, int price, int idcurrnt)
        {
            int reslt = 0;
            try
            {
                string query = "select IDAccount from Account where IDCategory = @IDCategory and IDType = @IDType and Price = @Price  and IDCurrency = @IDCurrency";
                SqlParameter[] parm = new SqlParameter[4];
                parm[0] = new SqlParameter("@IDCategory", IDCategory);
                parm[1] = new SqlParameter("@IDType", IDType);
                parm[2] = new SqlParameter("@Price", price);
                parm[3] = new SqlParameter("@IDCurrency", idcurrnt);

               reslt= (int)sql.ExcuteQueryValue(query, parm);
            }
            catch
            {
                reslt = 0;
            }
            return reslt;
        
          
           

        }
        /// Update quntity account
        /// 
        public int UpdateQuntityAccount(int IDAccount, int newquntity)
         {
            
      
            string query = "UPDATE [Account]   SET [Quntity] = @newquntity WHERE IDAccount =@IDAccount";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@newquntity", newquntity);
            parm[1] = new SqlParameter("@IDAccount", IDAccount);
            return sql.ExcuteQuery(query, parm);
           }
        //////
        /// add new account
        /// 
        public int AddNewAccount(int IDCategory, int IDType, int Quntity, int Price, int idcurrnt)
        {

            string query = "insert into Account (IDCategory,IDType,Quntity,Price,IDCurrency) values(@IDCategory,@IDType,@Quntity,@Price,@IDCurrency)";
           SqlParameter [] parm=new  SqlParameter[5];
            parm[0] = new SqlParameter("@IDCategory", IDCategory);
        
            parm[1] = new SqlParameter("@IDType", IDType);
            parm[2] = new SqlParameter("@Quntity", Quntity);
            parm[3] = new SqlParameter("@Price", Price);
            parm[4] = new SqlParameter("@IDCurrency", idcurrnt);
            return sql.ExcuteQuery(query, parm);
        }

        /// Get Max ChechSupply
        /// 
        public int GetMaxCheckSupply()
        {

            string query = "select max(chek) from RequstSupply ";
            return (int)sql.ExcuteQueryValue(query, null);
        }
        public DataTable GetAllCategoryAR()
        {
            string qury = "SELECT [IDCategory] as 'رقم الصنف',[NameCategory] as 'اسم الصنف' FROM  [Category]";
            return sql.SelectData(qury,null);

        }

        public DataTable GetAllTypeQuntity()
        {
            string qury = "SELECT [IDType] as 'رقم النوع' ,[NameType] as 'اسم النوع' FROM [TypeQuntity]";
            return sql.SelectData(qury,null);
        }

        ////////////////////////////////
        //////// get AllCurrency
        public DataTable GetAllCurrency()
        {
           
            string query = "select IDCurrency as'رقم العملة',NameCurrency as 'اسم العملة' from Currency ";
            return sql.SelectData(query, null);
          

        }

       


        ///
        /// get currentQuntity in Account
        /// 
        public int GetQuntityInAccount(int IDAcount)
        {
            int res = 0;
            string query = "select Quntity from Account where IDAccount=@IDAccount";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@IDAccount", IDAcount);

         
            res=     (int)sql.ExcuteQueryValue(query, parm);
           
            return res;
            

        }


        //////////////////////////////
        //////////////// search in RequsetSupply
        public DataTable SearchINRequsetSupply(string txt)
        { //
            
            txt = "%" + txt + "%";
            string query = "select IDSupply as 'رقم الطلب' ,Category.NameCategory as 'اسم الصنف', TypeQuntity.NameType as 'نوع الكمية', RequstSupply.Quntity as 'الكمية', RequstSupply.Price as 'سعر الوحدة', RequstSupply.Quntity * RequstSupply.Price as 'الاجمالي',Currency.NameCurrency as 'العملة',RequstSupply.DateSupply as'تاريخ التوريد', RequstSupply.NameSupply as 'اسم المورد',Users.Name as 'اسم الموظف',RequstSupply.DescSupply as 'ملاحظات' ,RequstSupply.chek  from Users, Category, TypeQuntity, RequstSupply,Currency where RequstSupply.UserId=Users.UserID and RequstSupply.IDCategory = Category.IDCategory and RequstSupply.IDCurrency=Currency.IDCurrency and RequstSupply.IDType = TypeQuntity.IDType and convert(varchar,IDSupply)+convert(varchar,Quntity)+convert(varchar,Quntity)+convert(varchar,Price)+Category.NameCategory+Currency.NameCurrency+ TypeQuntity.NameType + RequstSupply.NameSupply + RequstSupply.DescSupply +Users.Name  like @txt order by RequstSupply.DateSupply,RequstSupply.IDCurrency,RequstSupply.IDType,RequstSupply.IDCategory ";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@txt", txt);
            return sql.SelectData(query, parm);
        
        }

        //////////////////////////////
        //////////////// search in RequsetSupplythwTXT and Date
        public DataTable SearchINRequsetSupplyTxtAndDate(string txt, DateTime d1, DateTime d2)
        { //
            
            txt = "%" + txt + "%";
            string query = "select IDSupply as 'رقم الطلب' ,Category.NameCategory as 'اسم الصنف', TypeQuntity.NameType as 'نوع الكمية', RequstSupply.Quntity as 'الكمية', RequstSupply.Price as 'سعر الوحدة', RequstSupply.Quntity * RequstSupply.Price as 'الاجمالي',Currency.NameCurrency as 'العملة',RequstSupply.DateSupply as'تاريخ التوريد', RequstSupply.NameSupply as 'اسم المورد',Users.Name as 'اسم الموظف', RequstSupply.DescSupply as 'ملاحظات' ,RequstSupply.chek  from Users, Category, TypeQuntity, RequstSupply,Currency where RequstSupply.UserId=Users.UserID and RequstSupply.IDCategory = Category.IDCategory and RequstSupply.IDCurrency=Currency.IDCurrency and  RequstSupply.IDType = TypeQuntity.IDType and convert(varchar,IDSupply)+convert(varchar,Quntity)+convert(varchar,Quntity)+convert(varchar,Price)+ Category.NameCategory + TypeQuntity.NameType + RequstSupply.NameSupply + RequstSupply.DescSupply+Currency.NameCurrency+Users.Name  like @txt and DateSupply between @d1 and @d2 order by RequstSupply.DateSupply,RequstSupply.IDCurrency,RequstSupply.IDType,RequstSupply.IDCategory ";
            SqlParameter[] parm = new SqlParameter[3];
           parm[0] =new SqlParameter("@txt", txt);
           parm[1]=new SqlParameter("@d1", d1);
           parm[2]=new SqlParameter("@d2", d2);
            return sql.SelectData(query, parm);
        }

        /// GetUserNameById
        ///// 
        //public string GetUserNameBYIdUser(int IdUser)
        //{
            
        //  string query ="select Name from Users where UserID=@userid";
        //    SqlParameter[] parm = new SqlParameter[1];
        //    parm[0] = new SqlParameter("@userid", IdUser);

        //    return (string)sql.ExcuteQueryValue(query, parm);
          
        //}












        ///////////////////////////upadt frm
        ////////////////////////////////////////////
        /////// GetRequstSupply
        public DataTable GetRequstSupply(int IDreqSup)
        {
            string query = "select IDSupply,IDCategory,IDType,Quntity,Price,IDCurrency,DateSupply,NameSupply,DescSupply,Debit,Creditor  from  RequstSupply where IDSupply=@id ";
            SqlParameter[] parm = new SqlParameter[1];
                 parm[0] =new SqlParameter("@id", IDreqSup);
            return sql.SelectData(query, parm);

        }
        ///////////
        //////////
        //////
        //// Check Qunnty is Here InCheckQuntity
        public int CheckQuntityISHereInCheckQuntity(int IDCategory, int IDType)
        {

            string query = "select IDCheck from CheckQuntity where IDCategory=@IDCategory and IDType=@IDType";
            SqlParameter[] parm = new SqlParameter[2];
      parm[0]=new SqlParameter("@IDCategory", IDCategory);
               parm[1]=new SqlParameter("@IDType", IDType);

            return (int)sql.ExcuteQueryValue(query, parm);
            

        }
        ////////////////////////////////////////////////////////////
        //////////// Add New in UPD supply
        //////////////////////////////////

        /// 
        public int ADDNewUPDSupply(int IDSup, int IDCategory, int IDType, int Quntity, int Price, int idcunnt, string NameSupply, DateTime dateAdd, DateTime dateUpd, string decNew, int userid)
        {

            string query = "INSERT INTO [UpdSupply]([IDSupply] ,[IDCategory],[IDType],[Quntity],[Price],[IDCurrency],[NameSupply],[DateSupply],[DescUpd] ,[dateUpd],[UserId]) VALUES (@IDSupply,@IDCategory,@IDType,@Quntity,@Price,@IDCurrency,@NameSupply,@deteAdd,@descup,@dateup,@userid)";
            SqlParameter[] parm = new SqlParameter[11];

            parm[0]=new SqlParameter("@IDSupply", IDSup);
            parm[1]=new SqlParameter("@IDCategory", IDCategory);
            parm[2]=new SqlParameter("@IDType", IDType);
            parm[3]=new SqlParameter("@Quntity", Quntity);
            parm[4]=new SqlParameter("@Price", Price);
            parm[5]=new SqlParameter("@NameSupply", NameSupply);
            parm[6]=new SqlParameter("@deteAdd", dateAdd);
            parm[7]=new SqlParameter("@descup", decNew);
            parm[8]=new SqlParameter("@dateup", dateUpd);
            parm[9]=new SqlParameter("@IDCurrency", idcunnt);
            parm[10]=new SqlParameter("@userid", userid);
            return sql.ExcuteQuery(query, parm);

        }
        /////////////////////
        ////// 
        // Delete RequstSupply
        public int DeleteRequstSupply(int Id)
        {

            string query = "delete from RequstSupply where IDSupply=@id";
            SqlParameter[] parm = new SqlParameter[1];  
           parm[0]=new SqlParameter("@id", Id);
            return sql.ExcuteQuery(query, parm);

            
                    }
        /// 
        public int UPateRequstSupply(int IDSup, int IDCategory, int IDType, int Quntity, int Price, int idcurrn, string NameSupply, string DescSupply, int debit, int crd)
        {
            
           string Query="Update RequstSupply set IDCategory=@IDCategory,IDType=@IDType,Quntity=@Quntity,Price=@Price,NameSupply=@NameSupply,DescSupply=@DescSupply,IDCurrency=@idcurrn ,Debit=@debit,Creditor=@crd where IDSupply=@IDSupply";
            SqlParameter[] parm = new SqlParameter[10];

          parm[0]=new SqlParameter("@IDSupply", IDSup);
         parm[1]=new SqlParameter("@IDCategory", IDCategory);
            parm[2]=new SqlParameter("@IDType", IDType);
         parm[3]=new SqlParameter("@Quntity", Quntity);
           parm[4]=new SqlParameter("@Price", Price);
            parm[5]=new SqlParameter("@NameSupply", NameSupply);
           parm[6]=new SqlParameter("@DescSupply", DescSupply);
            parm[7]=new SqlParameter("@idcurrn", idcurrn);
          parm[8]=new SqlParameter("@debit", debit);
            parm[9]=new SqlParameter("@crd", crd);
            return sql.ExcuteQuery(Query, parm);
          
        }
        ////////////////////
        ////////////
        ///ACCount Tables
        ///

        ///////////////
        /// GetAllCount
        /// 
        public DataTable GetALLAcountNm()
        {

            string query = "  select AccountNm.IDCode as  'رقم الحساب' ,AccountNm.AcountNm as 'اسم الحساب' from AccountNm where AcountType='فرعي' ";
            return sql.SelectData(query, null);

        }

    ////////////
    //////////
    // Chack AcoountTotal is Here
    public   bool CheckAccontTotal(int IDcode,int IDCurrncy)
        {
            bool x;
            try
            {
                string Query = " select AccountTotal.IDCode from AccountTotal where AccountTotal.IDCode=@idc and AccountTotal.IDCurrncy=@idcu and Active=1";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@idc", IDcode);
                parm[1] = new SqlParameter("@idcu", IDCurrncy);
              int zzz=(int)   sql.ExcuteQueryValue(Query, parm);
                x = true;
            }
            catch
            {
                x = false;
            }
            return x;

        }
        ////////////
        /// <summary>
        /// /// add new AccountTotal 
        /// // 
        /// </summary>
        public int AddNewAccountTotal(int IDCOde, int Mony,int idCurrncy)
        {
            string Query = "insert into AccountTotal (IDCode,Balance,IDCurrncy) values(@idco,@many,@idcur)";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@idco", IDCOde);
            parm[1] = new SqlParameter("@many", Mony);
            parm[2] = new SqlParameter("@idcur", idCurrncy);
            return sql.ExcuteQuery(Query, parm);

        }
        ////////////
        //////////
        ///Get  Balance for AccountTotal
        public int GetBalance(int Idcode,int IDCur)
        {
            string Query = "select AccountTotal.Balance from AccountTotal where AccountTotal.IDCode=@idco and AccountTotal.IDCurrncy=@idCu";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@idco", Idcode);
            parm[1] = new SqlParameter("@idCu", IDCur);
            return (int)sql.ExcuteQueryValue(Query, parm);
        }

        ///////
        /// update Account Total
        ///
        public int UpdateAccountTotal(int IDCOde, int Mony, int idCurrncy)
        {
            int NewBalance = GetBalance(IDCOde, idCurrncy) + Mony;

            string Query = " update AccountTotal set Balance=@balanc where IDCode=@idco and IDCurrncy=@idcur";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@idco", IDCOde);
            parm[1] = new SqlParameter("@many", NewBalance);
            parm[2] = new SqlParameter("@idcur", idCurrncy);
            return sql.ExcuteQuery(Query, parm);

        }
        ////////////
        //////////
        //// Add new AccountDetalis 
        public int AddNewAccountDetalis(int idcode,int monay,int idsupply,int idout,string Detalis,DateTime d1,int userid)
        {
            string Query = "insert into AccountDetalis(IDCode,Mony,IDSupply,IDOut,Detalis,DateEnter,UserID) values(@IDCode,@Mony,@IDSupply,@IDOut,@Detalis,@DateEnter,@UserID)";
            SqlParameter[] parm = new SqlParameter[7];
            parm[0] = new SqlParameter("@IDCode", idcode);
            parm[1] = new SqlParameter("@Mony", monay);
            parm[2] = new SqlParameter("@IDSupply", idsupply);
            parm[3] = new SqlParameter("@IDOut", idout);
            parm[4] = new SqlParameter("@Detalis", Detalis);
            parm[5] = new SqlParameter("@DateEnter", d1);
            parm[6] = new SqlParameter("@UserID", userid);
            return sql.ExcuteQuery(Query,parm);
        }
        ////////////////
        //////////

    }
}
