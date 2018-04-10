using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace frmWInReprting
{
    class RepotFunction
    {
        MSqlConnection sql;
        public RepotFunction(string ServerNm, string NmDb, string UserSql, string PassSql)
        {
            sql = new MSqlConnection(ServerNm, NmDb, UserSql, PassSql);
        }
        // get all category

        public DataTable GetAllCategoryAR()
        {
            string Query = "SELECT [IDCategory] as 'رقم الصنف',[NameCategory] as 'اسم الصنف' FROM  [Category]";
            return sql.SelectData(Query, null);

        }
        ///    /////
        ///    
        /// /Get All Place send
        public DataTable GetAllPlace()
        {
            string Query= "SELECT [IDPlace]as 'رقم الجهة' ,[NamePlace] as 'اسم الجهة'  FROM [PlaceSend]";
            return sql.SelectData(Query, null);

        }


        ///    //Get All TypeQuntity
        ///    
        public DataTable GetAllTypeQuntity()
        {
            DataTable dtt11 = new DataTable();
            //dtt11.Columns.Add("رقم النوع");
            //dtt11.Columns.Add("اسم النوع");
            //  dtt11.Rows.Add(new Object[] { -1, "الكل" });

            string Query = "SELECT [IDType] as 'رقم النوع' ,[NameType] as 'اسم النوع' FROM [TypeQuntity]";
            DataTable dt88 = new DataTable();
            dt88 = sql.SelectData(Query, null);
            DataTable dq = new DataTable();
            dtt11.Merge(dt88);

            return dtt11;
        }

        ////////////////////////////////
        //////// get AllCurrency
        public DataTable GetAllCurrency()
        {

            string Query = "select IDCurrency as'رقم العملة',NameCurrency as 'اسم العملة' from Currency ";
            return sql.SelectData(Query, null);

        }
        /////////////
        // get  groups Cate
        public DataTable GetGroupsCate()
        {
            string Query = "  select tblGroup.ID as 'رقم المجموعة' ,tblGroup.GroupName as 'اسم المجموعة'  from tblGroup where tblGroup.GroupSourceID=2";
            return sql.SelectData(Query, null);
           
        }
        //// Get All Users
        public DataTable GetAllUser()
        {


            string Query = "select Users.IDUSER as 'رقم الموظف' ,Users.Name as 'اسم الموظف',Users.UserName as 'اسم المستخدم',Users.Password as 'كلمة المرور',Users.Supply as 'امر توريد',Users.Out as 'امر صرف',Users.PrintRE as 'طباعة تقارير',Users.UpdteDe as'تعديل / حذف' ,Users.Active as 'تفعيل',Users.UserAdd as'اضافة مستخدمين' from Users ";
            return sql.SelectData(Query, null);
        }
        //// Get All UsersAR
        public DataTable GetAllUserAR()
        {


            string Query = "select Users.IDUSER as 'رقم الموظف',Users.Name as 'اسم الموظف'  from Users ";
            return sql.SelectData(Query, null);
        }

        //////////////////////////////
        //////////////// search in RequsetSupplythwTXT and Date
        public DataTable PrintRequstRPT(DateTime d1, DateTime d2, int IDCate, int IDType, int IDCurrn, string txt, int iduser )
        {
            string Query;
            SqlParameter[] parm =new SqlParameter[7];
            txt = "%" + txt + "%";

            Query = "select IDSupply as 'رقم الطلب' ,Category.NameCategory as 'اسم الصنف', TypeQuntity.NameType as 'نوع الكمية', RequstSupply.Quntity as 'الكمية', RequstSupply.Price as 'سعر الوحدة', RequstSupply.Quntity * RequstSupply.Price as 'الاجمالي',Currency.NameCurrency as 'العملة',RequstSupply.DateSupply as'تاريخ التوريد', RequstSupply.NameSupply as 'اسم المورد', RequstSupply.DescSupply as 'ملاحظات' from Category, TypeQuntity, RequstSupply,Currency where RequstSupply.IDCategory = Category.IDCategory and RequstSupply.IDCurrency=Currency.IDCurrency and  RequstSupply.IDType = TypeQuntity.IDType and RequstSupply.IDCurrency = @IDCu and RequstSupply.IDType = @IDTy and RequstSupply.IDCategory = @IDCa"; 
                parm[0] = new SqlParameter("@IDCa", IDCate);
                parm[1] = new SqlParameter("@IDTy", IDType);
                parm[2] = new SqlParameter("@IDCu", IDCurrn);
                parm[3] = new SqlParameter("@iduser", iduser);
                parm[4] = new SqlParameter("@txt", txt);
                parm[5] = new SqlParameter("@d1", d1);
                parm[6] = new SqlParameter("@d2", d2);
            return sql.SelectData(Query, parm);
            
        }
        public string GetUserNameBYIdUser(int IdUser)
        {

            string Query = "select Name from Users where IDUSER=@userid";
            SqlParameter[] parm = new SqlParameter[1];

            parm[0] = new SqlParameter("@userid", IdUser);
            return (string)sql.ExcuteQueryValue(Query, parm);
        }
        //////////////// search in RequsetSupplythwTXT and Date
        public DataTable PrintRequstRPTAll(DateTime d1, DateTime d2,string txt)
        {
            string Query;
            SqlParameter[] parm;
            txt = "%" + txt + "%";

            Query = "select IDSupply as 'رقم الطلب' ,Category.NameCategory as 'اسم الصنف', TypeQuntity.NameType as 'نوع الكمية', RequstSupply.Quntity as 'الكمية', RequstSupply.Price as 'سعر الوحدة', RequstSupply.Quntity * RequstSupply.Price as 'الاجمالي',Currency.NameCurrency as 'العملة',RequstSupply.DateSupply as'تاريخ التوريد', RequstSupply.NameSupply as 'اسم المورد', RequstSupply.DescSupply as 'ملاحظات' from Category, TypeQuntity, RequstSupply,Currency where RequstSupply.IDCategory = Category.IDCategory and RequstSupply.IDCurrency=Currency.IDCurrency and  RequstSupply.IDType = TypeQuntity.IDType and  NameSupply like @txt and DateSupply between @d1 and @d2   order by RequstSupply.IDSupply";
            parm = new SqlParameter[3];
          
            parm[0] = new SqlParameter("@d1", d1);
            parm[1] = new SqlParameter("@d2", d2);
            parm[2] = new SqlParameter("@txt", txt);
            return sql.SelectData(Query, parm);

        }
        //////////////// search in RequsetSupplythwTXT and Date
        public DataTable PrintRequstRPTIDcat(DateTime d1, DateTime d2, string txt,int IDCate)
        {
            string Query;
            SqlParameter[] parm;
            txt = "%" + txt + "%";

            Query = "select IDSupply as 'رقم الطلب' ,Category.NameCategory as 'اسم الصنف', TypeQuntity.NameType as 'نوع الكمية', RequstSupply.Quntity as 'الكمية', RequstSupply.Price as 'سعر الوحدة', RequstSupply.Quntity * RequstSupply.Price as 'الاجمالي',Currency.NameCurrency as 'العملة',RequstSupply.DateSupply as'تاريخ التوريد', RequstSupply.NameSupply as 'اسم المورد', RequstSupply.DescSupply as 'ملاحظات' from Category, TypeQuntity, RequstSupply,Currency where RequstSupply.IDCategory = Category.IDCategory and RequstSupply.IDCurrency=Currency.IDCurrency and  RequstSupply.IDType = TypeQuntity.IDType and  NameSupply like @txt and DateSupply between @d1 and @d2 and RequstSupply.IDCategory = @IDCa  order by RequstSupply.IDSupply";
            parm = new SqlParameter[4];

            parm[0] = new SqlParameter("@d1", d1);
            parm[1] = new SqlParameter("@d2", d2);
            parm[2] = new SqlParameter("@txt", txt);
            parm[3] = new SqlParameter("@IDCa", IDCate);
            return sql.SelectData(Query, parm);

        }






        /// <summary>
        /// /////////////////
        /// </summary>
        /// <param name="idca"></param>
        /// <param name="idtyp"></param>
        /// <param name="idpalce"></param>
        /// <param name="idcurrnt"></param>
        /// <param name="name"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="iduser"></param>
        /// <returns></returns>
        // print OUt

        //////////////////

        public DataTable PrintOutAllwithDate(int idca, int idtyp, int idpalce, int idcurrnt, string name, DateTime d1, DateTime d2, int iduser)
        {
        
            name = "%" + name + "%";
       
            string Query="select RequstOut.IDOut as'رقم الطلب' ,Category.NameCategory as 'اسم الصنف' ,TypeQuntity.NameType as 'نوع الكمية',PlaceSend.NamePlace AS 'الجهة المستفيدة' ,RequstOut.Quntity as 'الكمية',RequstOut.Price AS 'سعر الوحدة',RequstOut.Quntity*RequstOut.Price as 'الاجمالي' ,Currency.NameCurrency AS 'العملة',RequstOut.NameOut AS 'يصرف بامر',RequstOut.NameSend as 'باستلام',RequstOut.DateOut as'تاريخ الصرف' ,RequstOut.DesOut as 'الملاحظات' from RequstOut,Category,TypeQuntity,Currency,PlaceSend where RequstOut.IDCategory=Category.IDCategory and RequstOut.IDType=TypeQuntity.IDType and RequstOut.IDPlace=PlaceSend.IDPlace and RequstOut.IDCurrency=Currency.IDCurrency and RequstOut.IDCategory  = @idca and RequstOut.IDType = @idty and RequstOut.UserId = @iduser and  RequstOut.IDCurrency = @idcurr and  RequstOut.IDPlace = @idplac and RequstOut.NameSend like @nmsend and RequstOut.DateOut between @d1 and @d2 order by RequstOut.IDOut";
            SqlParameter[] parm = new SqlParameter[8];
         
           parm[0]=new SqlParameter("@idca", idca);
            parm[1] = new SqlParameter("@idty", idtyp);
            parm[2] = new SqlParameter("@idcurr", idcurrnt);
            parm[3] = new SqlParameter("@idplac", idpalce);
            parm[4] = new SqlParameter("@nmsend", name);
            parm[5] = new SqlParameter("@d1", d1);
            parm[6] = new SqlParameter("@d2", d2);
            parm[7] = new SqlParameter("@iduser", iduser);
            return sql.SelectData(Query, parm);
        }

        public DataTable PrintOutAllwithDateAll( string name, DateTime d1, DateTime d2)
        {

            name = "%" + name + "%";

            string Query = "select RequstOut.IDOut as'رقم الطلب' ,Category.NameCategory as 'اسم الصنف' ,TypeQuntity.NameType as 'نوع الكمية',PlaceSend.NamePlace AS 'الجهة المستفيدة' ,RequstOut.Quntity as 'الكمية',RequstOut.Price AS 'سعر الوحدة',RequstOut.Quntity*RequstOut.Price as 'الاجمالي' ,Currency.NameCurrency AS 'العملة',RequstOut.NameOut AS 'يصرف بامر',RequstOut.NameSend as 'باستلام',RequstOut.DateOut as'تاريخ الصرف' ,RequstOut.DesOut as 'الملاحظات' from RequstOut,Category,TypeQuntity,Currency,PlaceSend where RequstOut.IDCategory=Category.IDCategory and RequstOut.IDType=TypeQuntity.IDType and RequstOut.IDPlace=PlaceSend.IDPlace and RequstOut.IDCurrency=Currency.IDCurrency  and RequstOut.NameSend like @nmsend and RequstOut.DateOut between @d1 and @d2 order by RequstOut.IDOut";
            SqlParameter[] parm = new SqlParameter[3];

          
            parm[0] = new SqlParameter("@nmsend", name);
            parm[1] = new SqlParameter("@d1", d1);
            parm[2] = new SqlParameter("@d2", d2);
            
            return sql.SelectData(Query, parm);
        }
        public DataTable PrintOutAllwithDateWithIDca(string name, DateTime d1, DateTime d2 ,int idca)
        {

            name = "%" + name + "%";

            string Query = "select RequstOut.IDOut as'رقم الطلب' ,Category.NameCategory as 'اسم الصنف' ,TypeQuntity.NameType as 'نوع الكمية',PlaceSend.NamePlace AS 'الجهة المستفيدة' ,RequstOut.Quntity as 'الكمية',RequstOut.Price AS 'سعر الوحدة',RequstOut.Quntity*RequstOut.Price as 'الاجمالي' ,Currency.NameCurrency AS 'العملة',RequstOut.NameOut AS 'يصرف بامر',RequstOut.NameSend as 'باستلام',RequstOut.DateOut as'تاريخ الصرف' ,RequstOut.DesOut as 'الملاحظات' from RequstOut,Category,TypeQuntity,Currency,PlaceSend where RequstOut.IDCategory=Category.IDCategory and RequstOut.IDType=TypeQuntity.IDType and RequstOut.IDPlace=PlaceSend.IDPlace and RequstOut.IDCurrency=Currency.IDCurrency and RequstOut.IDCategory  = @idca  and RequstOut.NameSend like @nmsend and RequstOut.DateOut between @d1 and @d2 order by RequstOut.IDOut";
            SqlParameter[] parm = new SqlParameter[4];


            parm[0] = new SqlParameter("@nmsend", name);
            parm[1] = new SqlParameter("@d1", d1);
            parm[2] = new SqlParameter("@d2", d2);
            parm[3] = new SqlParameter("@idca", idca);
            return sql.SelectData(Query, parm);
        }
        public DataTable PrintOutAllwithDateWithIDcaPLAC(string name, DateTime d1, DateTime d2, int idca, int idpalce)
        {

            name = "%" + name + "%";

            string Query = "select RequstOut.IDOut as'رقم الطلب' ,Category.NameCategory as 'اسم الصنف' ,TypeQuntity.NameType as 'نوع الكمية',PlaceSend.NamePlace AS 'الجهة المستفيدة' ,RequstOut.Quntity as 'الكمية',RequstOut.Price AS 'سعر الوحدة',RequstOut.Quntity*RequstOut.Price as 'الاجمالي' ,Currency.NameCurrency AS 'العملة',RequstOut.NameOut AS 'يصرف بامر',RequstOut.NameSend as 'باستلام',RequstOut.DateOut as'تاريخ الصرف' ,RequstOut.DesOut as 'الملاحظات' from RequstOut,Category,TypeQuntity,Currency,PlaceSend where RequstOut.IDCategory=Category.IDCategory and RequstOut.IDType=TypeQuntity.IDType and RequstOut.IDPlace=PlaceSend.IDPlace and RequstOut.IDCurrency=Currency.IDCurrency and RequstOut.IDCategory  = @idca  and RequstOut.NameSend like @nmsend and   RequstOut.IDPlace = @idplac and RequstOut.DateOut between @d1 and @d2 order by RequstOut.IDOut";
            SqlParameter[] parm = new SqlParameter[5];


            parm[0] = new SqlParameter("@nmsend", name);
            parm[1] = new SqlParameter("@d1", d1);
            parm[2] = new SqlParameter("@d2", d2);
            parm[3] = new SqlParameter("@idca", idca);
            parm[4] = new SqlParameter("@idplac", idpalce);
            return sql.SelectData(Query, parm);
        }

        public DataTable PrintOutAllwithDateWithPLAC(string name, DateTime d1, DateTime d2,  int idpalce)
        {

            name = "%" + name + "%";

            string Query = "select RequstOut.IDOut as'رقم الطلب' ,Category.NameCategory as 'اسم الصنف' ,TypeQuntity.NameType as 'نوع الكمية',PlaceSend.NamePlace AS 'الجهة المستفيدة' ,RequstOut.Quntity as 'الكمية',RequstOut.Price AS 'سعر الوحدة',RequstOut.Quntity*RequstOut.Price as 'الاجمالي' ,Currency.NameCurrency AS 'العملة',RequstOut.NameOut AS 'يصرف بامر',RequstOut.NameSend as 'باستلام',RequstOut.DateOut as'تاريخ الصرف' ,RequstOut.DesOut as 'الملاحظات' from RequstOut,Category,TypeQuntity,Currency,PlaceSend where RequstOut.IDCategory=Category.IDCategory and RequstOut.IDType=TypeQuntity.IDType and RequstOut.IDPlace=PlaceSend.IDPlace and RequstOut.IDCurrency=Currency.IDCurrency   and RequstOut.NameSend like @nmsend and   RequstOut.IDPlace = @idplac and RequstOut.DateOut between @d1 and @d2 order by RequstOut.IDOut";
            SqlParameter[] parm = new SqlParameter[4];


            parm[0] = new SqlParameter("@nmsend", name);
            parm[1] = new SqlParameter("@d1", d1);
            parm[2] = new SqlParameter("@d2", d2);
         
            parm[3] = new SqlParameter("@idplac", idpalce);
            return sql.SelectData(Query, parm);
        }
        //////////////////////
        //////////////////
        //// printQuntity
        //////////////////////////////////
        /////////// print Account Quntity
        public DataTable PrintAccountQuntity(int idcat, int  idtyp, int  idcu)
        {
            
           
            string Query="select  Category.NameCategory as 'اسم الصنف' ,TypeQuntity.NameType as 'نوع الكمية' ,Account.Quntity as 'الكمية الحالية',Account.Price as 'السعر' ,Currency.NameCurrency as 'العملة' from Account,Category,TypeQuntity,Currency where Account.IDCategory=Category.IDCategory and Account.IDType =TypeQuntity.IDType and Account.IDCurrency=Currency.IDCurrency and Account.Quntity>0 and    Account.IDCategory = @idcat and Account.IDType = @idtyp and Account.IDCurrency = @idcur  order by Account.IDCategory";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0]=new SqlParameter("@idcat", idcat);
            parm[1]=new SqlParameter("@idtyp", idtyp);
            parm[2]=new SqlParameter("@idcur", idcu);
            return sql.SelectData(Query, parm);

        }
        /////////// print Account Quntity
        public DataTable PrintAccountQuntityIDac( int idcu)
        {


            string Query = "select  Category.NameCategory as 'اسم الصنف' ,TypeQuntity.NameType as 'نوع الكمية' ,Account.Quntity as 'الكمية الحالية',Account.Price as 'السعر' ,Currency.NameCurrency as 'العملة' from Account,Category,TypeQuntity,Currency where Account.IDCategory=Category.IDCategory and Account.IDType =TypeQuntity.IDType and Account.IDCurrency=Currency.IDCurrency and Account.Quntity>0 and    Account.IDCategory =@idcur   order by Account.IDCategory";
            SqlParameter[] parm = new SqlParameter[1];
            
            parm[0] = new SqlParameter("@idcur", idcu);
            return sql.SelectData(Query, parm);

        }
        ////// Get Quntity withGroup
        public DataTable PrintAccountQuntityWithGroup(int IDGroup)
        {
            string Query = "SELECT     dbo.Category.NameCategory as 'اسم الصنف',dbo.TypeQuntity.NameType as 'نوع الكمية',dbo.Account.Quntity as 'الكمية الحالية', dbo.Account.Price as 'السعر'  ,dbo.Currency.NameCurrency as 'العملة' FROM   dbo.Account INNER JOIN  dbo.Category ON dbo.Account.IDCategory = dbo.Category.IDCategory INNER JOIN dbo.TypeQuntity ON dbo.Account.IDType = dbo.TypeQuntity.IDType INNER JOIN   dbo.Currency ON dbo.Account.IDCurrency = dbo.Currency.IDCurrency  where dbo.Account.IDCategory in(select GroupIDItem from GroupDetalis where GroupID=@GroupID)";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@GroupID", IDGroup);
            return sql.SelectData(Query, parm);

        }
        /////////// print Account Quntity
        public DataTable PrintAccountQuntityAll()
        {


            string Query = "select  Category.NameCategory as 'اسم الصنف' ,TypeQuntity.NameType as 'نوع الكمية' ,Account.Quntity as 'الكمية الحالية',Account.Price as 'السعر' ,Currency.NameCurrency as 'العملة' from Account,Category,TypeQuntity,Currency where Account.IDCategory=Category.IDCategory and Account.IDType =TypeQuntity.IDType and Account.IDCurrency=Currency.IDCurrency and Account.Quntity>0   order by Account.IDCategory";
        
            return sql.SelectData(Query, null);

        }
        /////////////////////
        ////////////////////
        /////////////////////
        //////////////////
        /// reporting UpdSupply
        /// 
        /////////////////////////////// 
        ///////////Get form Update supply
        //// get by id supply
        public DataTable GetUpdateSupplyByIDSupply(int Id)
        {
          
            string Query= "SELECT  dbo.UpdSupply.IDUpt as 'رقم' , dbo.UpdSupply.IDSupply as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف' , dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdSupply.Quntity as 'الكمية', dbo.UpdSupply.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',dbo.UpdSupply.NameSupply as 'اسم المورد' , dbo.UpdSupply.dateUpd as 'تاريخ التعديل', dbo.UpdSupply.DescUpd  as 'التعديل',Users.Name as 'اسم الموظف'  FROM Users, dbo.TypeQuntity CROSS JOIN   dbo.Category INNER JOIN  dbo.UpdSupply ON dbo.Category.IDCategory = dbo.UpdSupply.IDCategory CROSS JOIN   dbo.Currency  where UpdSupply.IDCategory=Category.IDCategory and UpdSupply.IDType=TypeQuntity.IDType and UpdSupply.IDCurrency =Currency.IDCurrency and UpdSupply.UserId = Users.IDUSER and dbo.UpdSupply.IDSupply=@id ";
            SqlParameter[] pram = new SqlParameter[1];
            pram[0]=new SqlParameter("@id", Id);
            return sql.SelectData(Query, pram);

        }
        // get by Date
        public DataTable GetUpdateSupplyByDate(DateTime d1, DateTime d2)
        {

            string Query = "SELECT  dbo.UpdSupply.IDUpt as 'رقم', dbo.UpdSupply.IDSupply as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف' , dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdSupply.Quntity as 'الكمية', dbo.UpdSupply.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',dbo.UpdSupply.NameSupply as 'اسم المورد', dbo.UpdSupply.dateUpd as 'تاريخ التعديل', dbo.UpdSupply.DescUpd  as 'التعديل' ,Users.Name as 'اسم الموظف' FROM Users, dbo.TypeQuntity CROSS JOIN   dbo.Category INNER JOIN  dbo.UpdSupply ON dbo.Category.IDCategory = dbo.UpdSupply.IDCategory CROSS JOIN   dbo.Currency  where UpdSupply.IDCategory=Category.IDCategory and UpdSupply.IDType=TypeQuntity.IDType and UpdSupply.IDCurrency =Currency.IDCurrency and UpdSupply.UserId = Users.IDUSER and dbo.UpdSupply.dateUpd between @d1 and @d2  ";
            SqlParameter[] parm = new SqlParameter[2];
          parm[0]=new SqlParameter("@d1", d1);
          parm[1]=new SqlParameter("@d2", d2);
            return sql.SelectData(Query, parm);
        }
        /// 
        public DataTable GetUpdateSupplyByDateUpdateWithDate(DateTime d1, DateTime d2)
        {
          string Query= "SELECT  dbo.UpdSupply.IDUpt as 'رقم', dbo.UpdSupply.IDSupply as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف' , dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdSupply.Quntity as 'الكمية', dbo.UpdSupply.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',dbo.UpdSupply.NameSupply as 'اسم المورد', dbo.UpdSupply.dateUpd as 'تاريخ التعديل', dbo.UpdSupply.DescUpd  as 'التعديل' ,Users.Name as 'اسم الموظف' FROM Users, dbo.TypeQuntity CROSS JOIN   dbo.Category INNER JOIN  dbo.UpdSupply ON dbo.Category.IDCategory = dbo.UpdSupply.IDCategory CROSS JOIN   dbo.Currency  where UpdSupply.IDCategory=Category.IDCategory and UpdSupply.IDType=TypeQuntity.IDType and UpdSupply.IDCurrency =Currency.IDCurrency and UpdSupply.UserId = Users.IDUSER and dbo.UpdSupply.dateUpd between @d1 and @d2  and UpdSupply.DescUpd !=N'تم حذف الطلب' ";
            SqlParameter[] parm = new SqlParameter[2];
         parm[0]=new SqlParameter("@d1", d1);
           parm[1]=new SqlParameter("@d2", d2);
            return sql.SelectData(Query, parm);
        }
        /// 
        /// 
        public DataTable GetUpdateSupplyByDateDeleteWithDate(DateTime d1, DateTime d2)
        {
          
            string Query= "SELECT  dbo.UpdSupply.IDUpt as 'رقم', dbo.UpdSupply.IDSupply as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف' , dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdSupply.Quntity as 'الكمية', dbo.UpdSupply.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',dbo.UpdSupply.NameSupply as 'اسم المورد', dbo.UpdSupply.dateUpd as 'تاريخ التعديل', dbo.UpdSupply.DescUpd  as 'التعديل' ,Users.Name as 'اسم الموظف' FROM Users, dbo.TypeQuntity CROSS JOIN   dbo.Category INNER JOIN  dbo.UpdSupply ON dbo.Category.IDCategory = dbo.UpdSupply.IDCategory CROSS JOIN   dbo.Currency  where UpdSupply.IDCategory=Category.IDCategory and UpdSupply.IDType=TypeQuntity.IDType and UpdSupply.IDCurrency =Currency.IDCurrency and UpdSupply.UserId = Users.IDUSER and dbo.UpdSupply.dateUpd between @d1 and @d2  and UpdSupply.DescUpd =N'تم حذف الطلب' ";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@d1", d1);
            parm[1] = new SqlParameter("@d2", d2);
            return sql.SelectData(Query, parm);
        }



        /////////////////////
        /////////////
        // reporting Upd OUt
        /// get of UpdtOut Uing IdOut
        public DataTable GetUpdtOutByIDOut(int idOUt)
        {
            
            string Query= "SELECT     dbo.UpdateOut.IdUpdate as 'رقم', dbo.UpdateOut.IDOut as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف', dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdateOut.Quntity as 'الكمية', dbo.UpdateOut.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',  dbo.PlaceSend.NamePlace as 'الجهة المستفيدة', dbo.UpdateOut.NameOUt as 'بامر من ', dbo.UpdateOut.NameSend as 'اسم المستلم', dbo.UpdateOut.TxtReson as 'سبب التعديل', dbo.UpdateOut.DateUpdate as 'تاريخ التعديل' ,Users.Name as 'اسم الموظف' FROM Users,   dbo.UpdateOut INNER JOIN   dbo.PlaceSend ON dbo.UpdateOut.IdPlace = dbo.PlaceSend.IDPlace CROSS JOIN   dbo.TypeQuntity CROSS JOIN  dbo.Category CROSS JOIN   dbo.Currency where UpdateOut.IdCate = Category.IDCategory and UpdateOut.IdCurrent = Currency.IDCurrency and UpdateOut.IdPlace = PlaceSend.IDPlace and UpdateOut.UserId=Users.IDUSER and UpdateOut.IdType = TypeQuntity.IDType and UpdateOut.IDOut=@id";
            SqlParameter[] parm = new SqlParameter[1];
            parm [0]=new SqlParameter("@id", idOUt);
           return  sql.SelectData(Query, parm);
        }
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public DataTable GetUpdOutByDate(DateTime d1, DateTime d2)
        {

            string Query= "SELECT    dbo.UpdateOut.IdUpdate as 'رقم', dbo.UpdateOut.IDOut as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف', dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdateOut.Quntity as 'الكمية', dbo.UpdateOut.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',  dbo.PlaceSend.NamePlace as 'الجهة المستفيدة', dbo.UpdateOut.NameOUt as 'بامر من ', dbo.UpdateOut.NameSend as 'اسم المستلم', dbo.UpdateOut.TxtReson as 'سبب التعديل', dbo.UpdateOut.DateUpdate as 'تاريخ التعديل' ,Users.Name as 'اسم الموظف' FROM  Users, dbo.UpdateOut INNER JOIN   dbo.PlaceSend ON dbo.UpdateOut.IdPlace = dbo.PlaceSend.IDPlace CROSS JOIN   dbo.TypeQuntity CROSS JOIN  dbo.Category CROSS JOIN   dbo.Currency where UpdateOut.IdCate = Category.IDCategory and UpdateOut.IdCurrent = Currency.IDCurrency and UpdateOut.IdPlace = PlaceSend.IDPlace and UpdateOut.UserId=Users.IDUSER and  UpdateOut.IdType = TypeQuntity.IDType and UpdateOut.DateUpdate between @d1 and @d2";
            SqlParameter[] parm = new SqlParameter[2];
         parm[0]=new SqlParameter("@d1", d1);
          parm[1]=new SqlParameter("@d2", d2);
            return sql.SelectData(Query, parm);
        }
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public DataTable GetUpdOutByDateUpdtewithdate(DateTime d1, DateTime d2)
        {
           
          string Query= "SELECT    dbo.UpdateOut.IdUpdate as 'رقم', dbo.UpdateOut.IDOut as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف', dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdateOut.Quntity as 'الكمية', dbo.UpdateOut.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',  dbo.PlaceSend.NamePlace as 'الجهة المستفيدة', dbo.UpdateOut.NameOUt as 'بامر من ', dbo.UpdateOut.NameSend as 'اسم المستلم', dbo.UpdateOut.TxtReson as 'سبب التعديل', dbo.UpdateOut.DateUpdate as 'تاريخ التعديل' ,Users.Name as 'اسم الموظف' FROM  Users, dbo.UpdateOut INNER JOIN   dbo.PlaceSend ON dbo.UpdateOut.IdPlace = dbo.PlaceSend.IDPlace CROSS JOIN   dbo.TypeQuntity CROSS JOIN  dbo.Category CROSS JOIN   dbo.Currency where UpdateOut.IdCate = Category.IDCategory and UpdateOut.IdCurrent = Currency.IDCurrency and UpdateOut.IdPlace = PlaceSend.IDPlace and UpdateOut.UserId=Users.IDUSER and  UpdateOut.IdType = TypeQuntity.IDType and UpdateOut.DateUpdate between @d1 and @d2 and UpdateOut.TxtReson !=N'تم حذف الطلب'";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@d1", d1);
            parm[1] = new SqlParameter("@d2", d2);
            return sql.SelectData(Query, parm);
        }
        ///////
        /// <param name="d2"></param>
        /// <returns></returns>
        public DataTable GetUpdOutByDateDetle2tewithdate(DateTime d1, DateTime d2)
        {

            string Query = "SELECT    dbo.UpdateOut.IdUpdate as 'رقم', dbo.UpdateOut.IDOut as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف', dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdateOut.Quntity as 'الكمية', dbo.UpdateOut.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',  dbo.PlaceSend.NamePlace as 'الجهة المستفيدة', dbo.UpdateOut.NameOUt as 'بامر من ', dbo.UpdateOut.NameSend as 'اسم المستلم', dbo.UpdateOut.TxtReson as 'سبب التعديل', dbo.UpdateOut.DateUpdate as 'تاريخ التعديل' ,Users.Name as 'اسم الموظف' FROM  Users, dbo.UpdateOut INNER JOIN   dbo.PlaceSend ON dbo.UpdateOut.IdPlace = dbo.PlaceSend.IDPlace CROSS JOIN   dbo.TypeQuntity CROSS JOIN  dbo.Category CROSS JOIN   dbo.Currency where UpdateOut.IdCate = Category.IDCategory and UpdateOut.IdCurrent = Currency.IDCurrency and UpdateOut.IdPlace = PlaceSend.IDPlace and UpdateOut.UserId=Users.IDUSER and  UpdateOut.IdType = TypeQuntity.IDType and UpdateOut.DateUpdate between @d1 and @d2 and UpdateOut.TxtReson =N'تم حذف الطلب'";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@d1", d1);
            parm[1] = new SqlParameter("@d2", d2);
            return sql.SelectData(Query, parm);
        }

    }
}
