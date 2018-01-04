using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace Out_
{
    class OutFunction
    {
        MSqlConnection sql;
       
        public OutFunction(string SeverNm, string DBNm,string UserSql,string PassSql)
        {    if(UserSql ==null || PassSql==null)
            {
                sql = new MSqlConnection(SeverNm, DBNm);
            }
        else
            sql = new MSqlConnection(SeverNm, DBNm,UserSql,PassSql);

        }
        /// 
 //////// print requst out
        public DataTable PrintRequstOut(int Check, int UserId, int user)
        {
          

            string Query= "select RequstOut.IDOut as  'رقم الطلب',Category.NameCategory as 'اسم الصنف',TypeQuntity.NameType as 'نوع الكمية',PlaceSend.NamePlace as'الجهة المستفيدة' ,RequstOut.Quntity as'الكمية',RequstOut.Price as 'سعر الوحدة',RequstOut.Quntity*RequstOut.Price as'الاجمالي', Currency.NameCurrency as 'العملة',RequstOut.NameOut as'يصرف بامر',RequstOut.NameSend as'باستلام',RequstOut.DateOut as'تاريخ الصرف',Users.Name as 'اسم الموظف',RequstOut.DesOut as 'الملاحظات' ,t.AcountNm as 'مدين' ,t1.AcountNm as 'دائن'  from Users,RequstOut,Category,TypeQuntity,PlaceSend,Currency,AccountNm as t,AccountNm as t1 where RequstOut.IDCategory = Category.IDCategory and Users.IDUSER=@idUser and RequstOut.IDType = TypeQuntity.IDType and RequstOut.IDCurrency=Currency.IDCurrency and t.IDCode=RequstOut.Debit and t1.IDCode=RequstOut.Creditor  and RequstOut.IDPlace = PlaceSend.IDPlace and RequstOut.Chack=@check and RequstOut.UserId=@uuu ";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0]=new SqlParameter   ("@check", Check);
            parm[1]=new SqlParameter("@idUser", UserId);
            parm[2]=new SqlParameter("@uuu", user);

            return sql.SelectData(Query, parm);

           
        }
        /// ////// print exit Rerquset
        /// </summary>
        /// <param name="Check"></param>
        /// <param name="UserId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataTable printrequstOutExit(int Check, int UserId, int user)
        {
           

            string Query= "select RequstOut.IDOut as  'الرقم المخزني',Category.NameCategory as 'الاسم',TypeQuntity.NameType as 'النوع',RequstOut.Quntity as'الكمية',RequstOut.NameSend as'اسم المستلم',Users.Name as 'اسم الموظف' ,Users.Name as 'العنوان' ,PlaceSend.NamePlace as 'الجهة' from Users,RequstOut,Category,TypeQuntity,PlaceSend,Currency,Debit,Creditor where RequstOut.IDCategory = Category.IDCategory and Users.IDUSER=@idUser and RequstOut.IDType = TypeQuntity.IDType and RequstOut.IDCurrency=Currency.IDCurrency and Debit.IdTypeAccount=RequstOut.Debit and Creditor.IdTypeAccount=RequstOut.Creditor  and RequstOut.IDPlace = PlaceSend.IDPlace and RequstOut.Chack=@check and RequstOut.UserId=@uuu ";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] =new SqlParameter("@check", Check);
            parm[1]=new SqlParameter("@idUser", UserId);
            parm[2]=new SqlParameter("@uuu", user);
            return sql.SelectData(Query, parm);
        }

        public int GetIdUser(string NameUser)
        {


            string Query = "select UserID from Users where Name=@name";
             SqlParameter[] parm = new SqlParameter[1];
              parm[0] = new SqlParameter("@name",NameUser);
             int re= (int)sql.ExcuteQueryValue(Query, parm);
            
            return re;
           
        }

           
           
              
              

          
  
        /// <summary>
        /// /////////////
        /// </summary>
        /// <returns></returns>
        public DataTable GetCatagoryInAccount()
        {
           
            string Query = "select DISTINCT Category.IDCategory  as 'رقم الصنف',Category.NameCategory as 'اسم الصنف' from Category,Account where Account.IDCategory =Category.IDCategory and Account.Quntity>0";


            return sql.SelectData(Query, null);
        }
        //Get All Place send
        public DataTable GetAllPlace()
        {
            string qury = "SELECT [IDPlace]as 'رقم الجهة' ,[NamePlace] as 'اسم الجهة'  FROM  [PlaceSend]";
            return sql.SelectData(qury,null);

        }
        //////////////
        /// GetAllDebit
        /// 
        public DataTable GetAllDebit()
        {
            string Query = "select IdTypeAccount as 'الرقم' ,NameTypeAccount as 'نوع الحساب' from Debit ";
            return sql.SelectData(Query, null);

        }
        /// GetAllCreditor
        /// 
        public DataTable GetAllCreditor()
        {

            string Query = "select  IdTypeAccount as 'الرقم' ,NameTypeAccount as 'نوع الحساب' from Creditor ";
            return sql.SelectData(Query, null);

        }
        ///////////
        //////////////// 
        public DataTable SearchINRequstOutDate(DateTime d1, DateTime d2)
        {
            string Query= "select RequstOut.IDOut as 'رقم الطلب',Category.NameCategory as 'اسم الصنف' ,TypeQuntity.NameType as 'نوع الكمية',PlaceSend.NamePlace as'الجهة المستفيدة' ,RequstOut.Quntity as'الكمية',RequstOut.Price as 'سعر الوحدة',RequstOut.Quntity*RequstOut.Price as'الاجمالي', Currency.NameCurrency as 'العملة',RequstOut.NameOut as'يصرف بامر',RequstOut.NameSend as'باستلام',RequstOut.DateOut as'تاريخ الصرف',Users.Name as 'اسم الموظف' ,RequstOut.DesOut as 'ملاحظات',RequstOut.Chack from Users, Category,TypeQuntity,PlaceSend,RequstOut,Currency where RequstOut.IDCategory=Category.IDCategory and RequstOut.IDType=TypeQuntity.IDType and RequstOut.IDPlace =PlaceSend.IDPlace and RequstOut.UserId=Users.IDUSER and RequstOut.IDCurrency =Currency.IDCurrency  and DateOut between @d1 and @d2 order by RequstOut.Chack desc ";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0]=new SqlParameter("@d1", d1);
            parm[1] = new SqlParameter("@d2", d2);
            return sql.SelectData(Query, parm);
     
        }
        public DataTable GetTypeInAccount(int IdCate)
        {
            string Query="select DISTINCT TypeQuntity.IDType as 'رقم النوع' ,TypeQuntity.NameType as 'اسم النوع' from TypeQuntity,Account where Account.IDType = TypeQuntity.IDType and Account.IDCategory =@IDCategory and Account.Quntity>0";
            SqlParameter[] parm = new SqlParameter[1];
           parm[0]=new SqlParameter("@IDCategory", IdCate);
            return sql.SelectData(Query, parm);

        }
        /// </summary>
        /// <returns></returns>
        public DataTable GetCurrencyINAccount(int idcat, int idtyp)
        {


            string Query = "select DISTINCT Currency.IDCurrency as 'رقم العملة', Currency.NameCurrency as 'اسم العملة' from Currency,Account where Account.IDCurrency=Currency.IDCurrency and Account.IDCategory=@IDCategory and Account.IDType=@IDType and Account.Quntity>0";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@IDCategory", idcat);
            parm[1] = new SqlParameter("@IDType", idtyp);
            return sql.SelectData(Query, parm);
        }
        public DataTable GetAccountIDs(int IdCAte, int IdTpe, int idcurrnt)
        {
            string Query = "select Account.IDAccount from Account where Account.IDCategory = @IDCategory and Account.IDType = @IDType and Account.IDCurrency=@IDCurrency and Account.Quntity>0";
            SqlParameter[] parm = new SqlParameter[3];
                parm[0]=new SqlParameter("@IDCategory", IdCAte);
               parm[1]=new SqlParameter("@IDType", IdTpe);
                parm[2]=new SqlParameter("@IDCurrency", idcurrnt);
            return sql.SelectData(Query, parm);

          }
        //////// get max check in requstOut
        public int GetMaxCheckInRequsetOut()
        {
            int r = 0;
            
            try
            {
               string Query="select max(Chack) from RequstOut";
                r = (int)sql.ExcuteQueryValue(Query,null);
            }
            catch
            {

                r = 0;
            }
           
            return r;

        }
        // get currentQuntity in Account
        /// 
        public int GetQuntityInAccount(int IDAcount)
        {
            
           

                string Query = "select Quntity from Account where IDAccount=@IDAccount";
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@IDAccount", IDAcount);
                return (int)sql.ExcuteQueryValue(Query, parm);
          
            
            
           

        }
        //////////////////
        private int GetPriceAccount(int iDAccount)
        {
           string Query="select Price from Account where IDAccount=@IDAccount";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0]=new SqlParameter("@IDAccount", iDAccount);
            return (int)sql.ExcuteQueryValue(Query ,parm);

        }
        ////////
        /// Update quntity account
        /// 
        public int UpdateQuntityAccount(int IDAccount, int newquntity)
        {
           
            string Query="UPDATE [Account]   SET [Quntity] = @newquntity WHERE IDAccount =@IDAccount";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0]=new SqlParameter("@IDAccount", IDAccount);
           parm[1]=new SqlParameter("@newquntity", newquntity);
            return sql.ExcuteQuery(Query, parm);
        }
        //////////////
        /////// Add New requstOut
        public int AddNewRequstOut(int Quntity, int IDCategory, int IDType, int idcurrnt, int IDPlace, string NameOut, string DesOut, DateTime DateOut, int Chack, string NameSend, int price, int UserId , int debit, int cred)
        {
            string Query = "insert into RequstOut (Chack,DateOut,DesOut,IDCategory,IDPlace,IDType,NameOut,NameSend,Quntity,Price,IDCurrency,UserId,Debit,Creditor) values(@Chack,@DateOut,@DesOut,@IDCategory,@IDPlace,@IDType,@NameOut,@NameSend,@Quntity,@Price,@IDCurrency,@userId,@dib,@cred)";
            SqlParameter[] parm = new SqlParameter[14];
            parm[0] = new SqlParameter("@Chack", Chack);
            parm[1] = new SqlParameter("@DateOut", DateOut);
            parm[2] = new SqlParameter("@DesOut", DesOut);
            parm[3] = new SqlParameter("@IDCategory", IDCategory);
            parm[4] = new SqlParameter("@IDPlace", IDPlace);
            parm[5] = new SqlParameter("@IDType", IDType);
            parm[6] = new SqlParameter("@NameOut", NameOut);
            parm[7] = new SqlParameter("@NameSend", NameSend);
            parm[8] = new SqlParameter("@Quntity", Quntity);
            parm[9] = new SqlParameter("@Price", price);
            parm[10] = new SqlParameter("@IDCurrency", idcurrnt);
            parm[11] = new SqlParameter("@userId", UserId);
            parm[12] = new SqlParameter("@dib", debit);
            parm[13] = new SqlParameter("@cred", cred);
            return sql.ExcuteQuery(Query, parm);
        }

        /////////////////  التكد من ان الحساب يغطي الطلب وارجاع صفر في حالة تم الطلب او ارجاع الكمية المتبقة المطلوبه
        public int GetAndCheckQuntityAccountAndAddRqustNew(int IDAccount, int QuntityMust, int IDCategory, int IDType, int idcurrn, int IDPlace, string NameOut, string DesOut, DateTime DateOut, int Chack, string NameSend, int debit, int credi,int UserId,string NMIDCA,string NMTYpe,string NMPlus,string NMMins,string nmCurnncy)
        {
            int r = -1;
            int QuntityOld = GetQuntityInAccount(IDAccount);
            int Total = 0;
            int Price = GetPriceAccount(IDAccount);// دالة جلب السعر
            int Quntity12 ;
            if (QuntityOld >= QuntityMust)
            {
                int newQuntity = QuntityOld - QuntityMust;
                UpdateQuntityAccount(IDAccount, newQuntity);/// تعديل الحساب بالكمية الجديدة
                AddNewRequstOut(QuntityMust, IDCategory, IDType, idcurrn, IDPlace, NameOut, DesOut, DateOut, Chack, NameSend, Price, UserId, debit, credi);// اضافة طلب جديد
                Total = QuntityMust * Price;
                Quntity12 = QuntityMust;
                r = 0;
            }

            else
            {


                int newQun = QuntityMust - QuntityOld;
                UpdateQuntityAccount(IDAccount, 0);
                AddNewRequstOut(QuntityOld, IDCategory, IDType, idcurrn, IDPlace, NameOut, DesOut, DateOut, Chack, NameSend, Price, UserId, debit, credi);// اضافة طلب جديد
                Total = QuntityOld * Price;
                Quntity12 = QuntityOld;
               r = QuntityOld;

            }
            /////////////////////////////////////////
            /////////////////////// Acount Total
            if(CheckAccontTotal(credi, idcurrn))///التاكرمن وجود الحساب الدائن موجود من قبل اولا
            {
                UpdateAccountTotal(credi, Total, idcurrn);

              
                
            }
            else
                {
                AddNewAccountTotal(credi, Total, idcurrn);

            }
            if(CheckAccontTotal(debit, idcurrn))///التاكرمن وجود الحساب المدين موجود من قبل اولا
            {
                UpdateAccountTotal(debit, (-1* Total), idcurrn);
               

            }
            else
                {

                AddNewAccountTotal(debit, (-1 * Total), idcurrn);

            }
            /////////////////
            ///////////// adding for Account datalis
            /// اضافة حساب دائن
            /// string.Format("{0:##,##}", (Total).ToString())+" "+ nmCurnncy
            string DitalisMis = "تم قيد عليكم مبلغ وقدره " + string.Format("{0:##,##}", (Total).ToString()) + " " + nmCurnncy +"  "+ "مقابل امر صرف ب  " + Quntity12 + " " + NMIDCA+ " " + NMTYpe + "  الى حساب  " + NMPlus +"رقم الطلب " + GetMAxIDOUt();
            string DatlisPlus = "تم قيد لكم مبلغ وقدره" + string.Format("{0:##,##}", (Total).ToString()) + " " + nmCurnncy +"  " + "مقابل امر توريد ب " + Quntity12 + " " + NMIDCA + " " + NMTYpe + "  من حساب " +NMMins + "رقم الطلب " + GetMAxIDOUt();
            AddNewAccountDetalis(credi, Total, 0, GetMAxIDOUt(), DatlisPlus, DateTime.Now, UserId, idcurrn);
            ////////// اضافة حساب مدين
           
            AddNewAccountDetalis(debit, (-1 * Total), 0, GetMAxIDOUt(), DitalisMis, DateTime.Now, UserId, idcurrn);

            return r;

        }
        ////////// get quntity in Account
        public int GetQunitiyinAccount2(int Idcae, int IdType, int idcurrnt)
        {
               string Query= "select SUM(Account.Quntity) from Account  where Account.IDCategory = @IDCategory and Account.IDType = @IDType and Account.IDCurrency=@IDCurrency";
             SqlParameter[] parm = new SqlParameter[3];

               parm[0]=new SqlParameter("@IDCategory", Idcae);
                parm[1]=new SqlParameter("@IDType", IdType);
               parm[2]=new SqlParameter("@IDCurrency", idcurrnt);
            return (int)sql.ExcuteQueryValue(Query, parm);
        }
        //////////////////////////////////
        ////////
        public DataTable SearchINRequsetOuttxt(string s)
        {
            s = "%" + s + "%";
          string Query="select RequstOut.IDOut as 'رقم الطلب',Category.NameCategory as 'اسم الصنف' ,TypeQuntity.NameType as 'نوع الكمية',PlaceSend.NamePlace as'الجهة المستفيدة' ,RequstOut.Quntity as'الكمية',RequstOut.Price as 'سعر الوحدة',RequstOut.Quntity*RequstOut.Price as'الاجمالي', Currency.NameCurrency as 'العملة',RequstOut.NameOut as'يصرف بامر',RequstOut.NameSend as'باستلام',RequstOut.DateOut as'تاريخ الصرف' ,Users.Name as 'اسم الموظف',RequstOut.DesOut as 'ملاحظات',RequstOut.Chack from Users,Category,TypeQuntity,PlaceSend,RequstOut,Currency where RequstOut.UserId=Users.UserID and RequstOut.IDCategory=Category.IDCategory and RequstOut.IDType=TypeQuntity.IDType and RequstOut.IDPlace =PlaceSend.IDPlace and RequstOut.IDCurrency =Currency.IDCurrency  and Category.NameCategory + TypeQuntity.NameType + PlaceSend.NamePlace +RequstOut.DesOut+RequstOut.NameOut+ RequstOut.NameSend+CONVERT(varchar,RequstOut.IDOut)+CONVERT(varchar,RequstOut.Price)+CONVERT(varchar,RequstOut.Quntity) like @txt  order by RequstOut.IDCategory,RequstOut.IDPlace,RequstOut.IDType,RequstOut.IDCurrency,RequstOut.Chack,RequstOut.DateOut";
            SqlParameter[] parm = new SqlParameter[1];
           parm[0]=new SqlParameter("@txt", s);
            return sql.SelectData(Query, parm);
        }
        ////////// 
        public DataTable SearchINRequsetOutTxtAndDate(string s, DateTime d1, DateTime d2)
        {
            s = "%" + s + "%";


            string Query = "select RequstOut.IDOut as 'رقم الطلب',Category.NameCategory as 'اسم الصنف' ,TypeQuntity.NameType as 'نوع الكمية',PlaceSend.NamePlace as'الجهة المستفيدة' ,RequstOut.Quntity as'الكمية',RequstOut.Price as 'سعر الوحدة',RequstOut.Quntity*RequstOut.Price as'الاجمالي', Currency.NameCurrency as 'العملة',RequstOut.NameOut as'يصرف بامر',RequstOut.NameSend as'باستلام',RequstOut.DateOut as'تاريخ الصرف' ,Users.Name as 'اسم الموظف',RequstOut.DesOut as 'ملاحظات',RequstOut.Chack from Users, Category,TypeQuntity,PlaceSend,RequstOut,Currency where RequstOut.IDCategory=Category.IDCategory and RequstOut.IDType=TypeQuntity.IDType and RequstOut.IDPlace =PlaceSend.IDPlace and RequstOut.IDCurrency =Currency.IDCurrency  and RequstOut.UserId =Users.UserID and Category.NameCategory + TypeQuntity.NameType + PlaceSend.NamePlace +RequstOut.DesOut+RequstOut.NameOut+ RequstOut.NameSend+CONVERT(varchar,RequstOut.IDOut)+CONVERT(varchar,RequstOut.Price)+CONVERT(varchar,RequstOut.Quntity) like @txt and DateOut between @d1 and @d2 order by RequstOut.IDCategory,RequstOut.IDPlace,RequstOut.IDType,RequstOut.IDCurrency,RequstOut.Chack,RequstOut.DateOut ";
            SqlParameter[] parm = new SqlParameter[3];

            parm[0]=new SqlParameter("@txt", s);
            parm[1] = new SqlParameter("@d1", d1);
            parm[2] = new SqlParameter("@d2", d2);

            return sql.SelectData(Query, parm);


        }
        /// 
        public string GetUserNameBYIdUser(int IdUser)
        {

            string Query = "select Name from Users where UserID=@userid";
            SqlParameter[] parm = new SqlParameter[1];

          parm[0]= new SqlParameter("@userid", IdUser);
            return (string)sql.ExcuteQueryValue(Query, parm);
        }
        /////
        //
        public DataTable GetRequstOutSngle(int IDOutRequst)
        {
            
            string Query="select * from RequstOut where IDOut=@id";
            SqlParameter[] parm = new SqlParameter[1];

            parm[0]=new SqlParameter("@id", IDOutRequst);
            return sql.SelectData(Query, parm);

        }
        /// upd Out
        /// Add in Upd Out
        public int AddNewUpdOut(int IDOut, int IdCate, int IdType, int IdPlace, int Quntity, string NameOUt, string NameSend, int Price, int IdCurrent, string TxtReson, DateTime DateUpdate, int UserId)
        {

            string Query = "insert into UpdateOut(IDOut,IdCate,IdType,IdPlace,Quntity,NameOUt,DateUpdate,NameSend,Price,IdCurrent,TxtReson,UserId) values(@IDOut,@IdCate,@IdType,@IdPlace,@Quntity,@NameOUt,@Date,@NameSend,@Price,@IdCurrent,@TxtReson,@UserId)";
            SqlParameter[] parm = new SqlParameter[12];
            parm[0]=new SqlParameter("@IDOut", IDOut);
            parm[1] = new SqlParameter("@IdCate", IdCate);
            parm[2] = new SqlParameter("@IdType", IdType);
            parm[3] = new SqlParameter("@IdPlace", IdPlace);
            parm[4] = new SqlParameter("@Quntity", Quntity);
            parm[5] = new SqlParameter("@NameOUt", NameOUt);
            parm[6] = new SqlParameter("@NameSend", NameSend);
            parm[7] = new SqlParameter("@Price", Price);
            parm[8] = new SqlParameter("@IdCurrent", IdCurrent);
            parm[9] = new SqlParameter("@TxtReson", TxtReson);
            parm[10] = new SqlParameter("@Date", DateUpdate);
            parm[11] = new SqlParameter("@UserId", UserId);
            return sql.ExcuteQuery(Query, parm);
               
        }
        ///
        /// CHACKE is account is here ro not
        public int CheckAccountIsHere(int IDCategory, int IDType, int price, int idcurrnt)
        {
            string Query = "select IDAccount from Account where IDCategory=@IDCategory and IDType=@IDType and Price=@Price  and IDCurrency=@IDCurrency";
            SqlParameter[] parm = new SqlParameter[4];
            parm[0]=new SqlParameter("@IDCategory", IDCategory);
            parm[1] = new SqlParameter("@IDType", IDType);
            parm[2] = new SqlParameter("@Price", price);
            parm[3] = new SqlParameter("@IDCurrency", idcurrnt);
            return (int)sql.ExcuteQueryValue(Query, parm);
            
        }
        //////
        ///// delete from requst out
        public int DeleteRqustOut(int IdRequstOut, int IdUser)
        {
           
            DataTable dt = new DataTable();
            // جلب معلومات عن الطلب المراد حذفه
            dt = GetRequstOutSngle(IdRequstOut);
            // اضافة البيانات الى جدول التعديلات 

            AddNewUpdOut(IdRequstOut, Convert.ToInt32(dt.Rows[0]["IDCategory"].ToString()), Convert.ToInt32(dt.Rows[0]["IDType"].ToString()), Convert.ToInt32(dt.Rows[0]["IDPlace"].ToString()), Convert.ToInt32(dt.Rows[0]["Quntity"].ToString()), dt.Rows[0]["NameOut"].ToString(), dt.Rows[0]["NameSend"].ToString(), Convert.ToInt32(dt.Rows[0]["Price"].ToString()), Convert.ToInt32(dt.Rows[0]["IDCurrency"].ToString()), "تم حذف الطلب", DateTime.Now, IdUser);

            // التعديل في جدول الحسابات
            // ارجاع الكمية الى المخزون
            int IdAccount = CheckAccountIsHere(Convert.ToInt32(dt.Rows[0]["IDCategory"].ToString()), Convert.ToInt32(dt.Rows[0]["IDType"].ToString()), Convert.ToInt32(dt.Rows[0]["Price"].ToString()), Convert.ToInt32(dt.Rows[0]["IDCurrency"].ToString()));
            int QuntOld = GetQuntityInAccount(IdAccount);
            int QuntNew = QuntOld + Convert.ToInt32(dt.Rows[0]["Quntity"].ToString());
            UpdateQuntityAccount(IdAccount, QuntNew);
            // حذف الطلب من جدول طلبات الصرف
            string Query="delete from RequstOut where IDOut =@id";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0]=new SqlParameter("@id", IdRequstOut);
            return sql.ExcuteQuery(Query, parm);
           
        }
        /////
        //
        /////////////
        ////////
        /// upadte Rqust oU
        /// 
        public int UpdateRequstOut(int IDOut, int IdPlace, string NameOut, string NameSend, string Reson, DateTime d1, int UserId, int debt, int crd)
        {
            int res = 0;
            /// اضافة التعديل الى جدول التعديلات
            /// 
            DataTable dt = new DataTable();
            // جلب معلومات عن الطلب المراد تعديله
            dt = GetRequstOutSngle(IDOut);
            // اضافة البيانات الى جدول التعديلات 

            AddNewUpdOut(IDOut, Convert.ToInt32(dt.Rows[0]["IDCategory"].ToString()), Convert.ToInt32(dt.Rows[0]["IDType"].ToString()), Convert.ToInt32(dt.Rows[0]["IDPlace"].ToString()), Convert.ToInt32(dt.Rows[0]["Quntity"].ToString()), dt.Rows[0]["NameOut"].ToString(), dt.Rows[0]["NameSend"].ToString(), Convert.ToInt32(dt.Rows[0]["Price"].ToString()), Convert.ToInt32(dt.Rows[0]["IDCurrency"].ToString()), Reson, DateTime.Now, UserId);
            //////////////////////////////////
            //التعديل في جدول التعديلات
            string Query = "Update RequstOut set IDPlace=@idplace , NameOut=@nameout ,NameSend=@namesend,Creditor=@crd,Debit=@dibt where IDOut=@idOut ";
            SqlParameter[] parm = new SqlParameter[6];

            parm[0]=new SqlParameter("@idplace", IdPlace);
            parm[1] = new SqlParameter("@nameout", NameOut);
            parm[2] = new SqlParameter("@namesend", NameSend);
            parm[3] = new SqlParameter("@idOut", IDOut);
            parm[4] = new SqlParameter("@dibt", debt);
            parm[5] = new SqlParameter("@crd", crd);
            return sql.ExcuteQuery(Query, parm);

        }
        ///////////////////////////////////
        //////////////////////
        //////////////// Acount NM
       //// get max IDOut
       public int GetMAxIDOUt()
        {
            string Query = "  select max(IDOut) from RequstOut";
            return (int) sql.ExcuteQueryValue(Query, null);
        
        }

        ///////////////
        /// GetAllCount
        /// 
        public DataTable GetALLAcountNm()
        {

            string query = "  select AccountNm.IDCode as  'رقم الحساب' ,AccountNm.AcountNm as 'اسم الحساب' from AccountNm where AcountType='فرعي'  and Active=1";
            return sql.SelectData(query, null);

        }
        ////////////
        //////////
        // Chack AcoountTotal is Here
        public bool CheckAccontTotal(int IDcode, int IDCurrncy)
        {
            bool x;
            try
            {
                string Query = " select AccountTotal.IDCode from AccountTotal where AccountTotal.IDCode=@idc and AccountTotal.IDCurrncy=@idcu ";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@idc", IDcode);
                parm[1] = new SqlParameter("@idcu", IDCurrncy);
                int zzz = (int)sql.ExcuteQueryValue(Query, parm);
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
        public int AddNewAccountTotal(int IDCOde, int Mony, int idCurrncy)
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
        public int GetBalance(int Idcode, int IDCur)
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
            parm[1] = new SqlParameter("@balanc", NewBalance);
            parm[2] = new SqlParameter("@idcur", idCurrncy);
            return sql.ExcuteQuery(Query, parm);

        }
        ////////////
        //////////
        //// Add new AccountDetalis 
        public int AddNewAccountDetalis(int idcode, int monay, int idsupply, int idout, string Detalis, DateTime d1, int userid,int idcurrn)
        {
            string Query = "insert into AccountDetalis(IDCode,Mony,IDSupply,IDOut,Detalis,DateEnter,UserID,IDCurrncy) values(@IDCode,@Mony,@IDSupply,@IDOut,@Detalis,@DateEnter,@UserID,@IDCurrncy)";
            SqlParameter[] parm = new SqlParameter[8];
            parm[0] = new SqlParameter("@IDCode", idcode);
            parm[1] = new SqlParameter("@Mony", monay);
            parm[2] = new SqlParameter("@IDSupply", idsupply);
            parm[3] = new SqlParameter("@IDOut", idout);
            parm[4] = new SqlParameter("@Detalis", Detalis);
            parm[5] = new SqlParameter("@DateEnter", d1);
            parm[6] = new SqlParameter("@UserID", userid);
            parm[7] = new SqlParameter("@IDCurrncy", IDCurrncy);
            return sql.ExcuteQuery(Query, parm);
        }
        public int DeleteSuuplyFrmAccountDitalis(int idSupply)
        {
            string Query = "  delete from AccountDetalis where AccountDetalis.IDSupply=@idsup";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@idsup", idSupply);
            return sql.ExcuteQuery(Query, parm);
        }
        public int DeleteSuuplyFrmAccountDitalis2(int idout)
        {
            string Query = "  delete from AccountDetalis where AccountDetalis.IDOut=@idsup";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@idsup", idout);
            return sql.ExcuteQuery(Query, parm);
        }
    }

}
