using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace SystemConfiguration
{
 public   class Config
    {
        MSqlConnection sql;
        public Config(string ServerNm,string DbNm,string UserSql,string PassSql)
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
        public DataTable GetAllCategoryAR()
        {
            string Query= "SELECT dbo.Category.IDCategory as 'رقم الصنف', dbo.Category.NameCategory as 'اسم الصنف' , dbo.AccountNm.AcountNm as 'اسم الحساب' FROM  dbo.Category LEFT OUTER JOIN   dbo.AccountNm ON dbo.Category.IDAccount = dbo.AccountNm.IDCode order by   dbo.Category.IDCategory desc";
            return sql.SelectData(Query, null);
         

        }
        ///get Name CAtegory by name
        public DataTable GetCategoryByName(string NMCate)
        {
            NMCate = "%"+ NMCate +"%";
            string Query = "SELECT dbo.Category.IDCategory as 'رقم الصنف', dbo.Category.NameCategory as 'اسم الصنف' , dbo.AccountNm.AcountNm as 'اسم الحساب' FROM  dbo.Category LEFT OUTER JOIN   dbo.AccountNm ON dbo.Category.IDAccount = dbo.AccountNm.IDCode where dbo.Category.NameCategory like @nameCate order by   dbo.Category.IDCategory desc ";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@nameCate", NMCate);
            return sql.SelectData(Query, parm);
        }
        // </summary>
        /// <param name="NameCategory"></param> this name a category
        /// <returns></returns>
        public int AddNewCategory(string NameCategory,object IDAccount)
        {
            string Query = "INSERT INTO [Category]([NameCategory],IDAccount)VALUES(@name,@IDAccount)";
            SqlParameter[] parm = new SqlParameter[2];
            if (IDAccount == null)
            {
                parm[0] = new SqlParameter("@name", NameCategory);
                parm[1] = new SqlParameter("@IDAccount", null);
            }
            else
            {
                parm[0] = new SqlParameter("@name", NameCategory);
                parm[1] = new SqlParameter("@IDAccount",(int) IDAccount);
            }
            return sql.ExcuteQuery(Query, parm);
        }
        //////
        //get max Cate iD
        public int GetMaxIDCate()
        {
            string Query = "select MAX(Category.IDCategory) from Category";
            return (int)sql.ExcuteQueryValue(Query, null);
        }
        // 
        

      // Update Category
        public int UpdateCategory(int id, string name,object IDAccount)
        {


            string Query;
                  SqlParameter[] parm;
            if (IDAccount == null)
            {
                Query = "UPDATE [Category] SET [NameCategory]=@name1  WHERE  IDCategory=@id1";
                parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@name1", name);
                parm[1] = new SqlParameter("@id1", id);
        
              

            }
            else
            {
                Query = "UPDATE [Category] SET [NameCategory]=@name1,[IDAccount]=@IDAccount  WHERE  IDCategory=@id1";
                parm = new SqlParameter[3];
                parm[0] = new SqlParameter("@name1", name);
                parm[1] = new SqlParameter("@id1", id);
                parm[2] = new SqlParameter("@IDAccount", (int)IDAccount);
            }
            return sql.ExcuteQuery(Query, parm);


        }
        //
        // delete Category
        public int DeleteCategory(int id)
        {
            string Query= "DELETE FROM [Category] WHERE IDCategory=@id";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);
            return sql.ExcuteQuery(Query, parm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IDCA"></param>
        /// <returns></returns>
        public DataTable chackCatagory(int IDCA)
        {
            string Query = "select * from RequstSupply where IDCategory=@id";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", IDCA);
            return sql.SelectData(Query, parm);

            }
        ////////////////////
        /////////////
        //////////////
        ///    //Get All TypeQuntity
        ///    
        public DataTable GetAllTypeQuntity()
        {
            string Query = "SELECT [IDType] as 'رقم النوع' ,[NameType] as 'اسم النوع' FROM [TypeQuntity] order by IDType desc";
            return sql.SelectData(Query, null);
        }

        ///    //Get  TypeQuntity by name
        ///    
        public DataTable GetTypeQuntityByName(string nameTYPe)
        {
            nameTYPe = "%" + nameTYPe + "%";
            string Query = "SELECT [IDType] as 'رقم النوع' ,[NameType] as 'اسم النوع' FROM [TypeQuntity] where NameType like @nameTYPe  order by IDType desc";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@nameTYPe", nameTYPe);

            return sql.SelectData(Query, parm);
        }
        //add new TypeQuntity
        public int AddNewTypeQuntity(string name)
        {
            string Query = "insert into TypeQuntity (NameType) values (@name)";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@name", name);

            return sql.ExcuteQuery(Query, parm);

        }
        ///////////
        /// Updte TypeQuntit
        /// 
        public int UpdateTypeQuntity(int id, string name)
        {
            string Query = "UPDATE [TypeQuntity]  SET [NameType] = @name WHERE IDType=@id";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@id",id);
            parm[1] = new SqlParameter("@name", name);
            return sql.ExcuteQuery(Query, parm);
        }

        ///
        // delete TypeQuntity
        public int DeleteQuntity(int id)
        {
            string Query= "DELETE FROM [TypeQuntity]WHERE IDType = @id";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);
            return sql.ExcuteQuery(Query, parm);
        }

        ///////
        /// <param name="IDType"></param>
        /// <returns></returns>
        public DataTable chackTapy(int IDtype)
        {
            string Query = "select * from RequstSupply where IDType=@id";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", IDtype);
            return sql.SelectData(Query, parm);

        }
        /////////?//////////
        //////////////////
        //// place
        //Get All Place send
        public DataTable GetAllPlace()
        {
            string Query = "SELECT [IDPlace]as 'رقم الجهة' ,[NamePlace] as 'اسم الجهة'  FROM [PlaceSend]  order by IDPlace desc";

            return sql.SelectData(Query,null);

        }

        //////////////////
        //// place
        //Get Place send by name
        public DataTable GetPlaceByName(string NAMEPLACE)
        {
            NAMEPLACE = "%" + NAMEPLACE + "%";
            string Query = "SELECT [IDPlace]as 'رقم الجهة' ,[NamePlace] as 'اسم الجهة'  FROM [PlaceSend] where NamePlace like @NAMEPLACE order by IDPlace  desc";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@NAMEPLACE", NAMEPLACE);
            return sql.SelectData(Query, parm);

        }


        // add new PlaceSend
        public int AddNewPlaceSend(string name)
        {
            string Query = "INSERT INTO [PlaceSend] ([NamePlace]) VALUES  (@name)";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@name", name);
            return sql.ExcuteQuery(Query, parm);

        }


        //
        // update Place send
        public int UpdatePlaceSend(int id, string name)
        {
            string Query = "UPDATE  [PlaceSend] SET [NamePlace] =@name  WHERE IDPlace =@id";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@name", name);
            parm[1] = new SqlParameter("@id", id);
            return sql.ExcuteQuery(Query, parm);
        }

        ////
        // delete PalceSend
        public int DeletePlaceSend(int id)
        {
            string Query = "delete from PlaceSend where IDPlace=@id";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);
            return sql.ExcuteQuery(Query, parm);

        }
        ///////
        /// <param name="IDPlace"></param>
        /// <returns></returns>
        public DataTable chackPlace(int IDplace)
        {
            string Query = "select * from RequstOut where IDPlace=@id";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", IDplace);
            return sql.SelectData(Query, parm);

        }
        /////////////////
        /////////
        /////////
        /// currncy
        /// 
         //////// get AllCurrency
        public DataTable GetAllCurrency()
        {
          
            string Query="select IDCurrency as'رقم العملة',NameCurrency as 'اسم العملة' from Currency ";
            return sql.SelectData(Query, null);
          

        }
        /////
        /// <summary>
        /// GET Currency BY NAME
        public DataTable GETCurrencyBYName(string NameCurrncy)
        { NameCurrncy = "%" + NameCurrncy + "%";
            string Query = "select IDCurrency as'رقم العملة',NameCurrency as 'اسم العملة' from Currency where NameCurrency like @NAMeCUr";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@NAMeCUr", NameCurrncy);
            return sql.SelectData(Query, parm);
           
        }
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>

        //////// جدول العملاتط
        // 
        public int AddNewCurrency(string name)
        {
            
            string Query="INSERT INTO [Currency]  ([NameCurrency])  VALUES(@txt)";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0]=new SqlParameter("@txt", name);
            return sql.ExcuteQuery(Query, parm);
        }
      
          /////////////////////////////////////////////////
        ///// Update Currency
        public int UpdateCurrency(int id, string name)
        {
            string Query="update Currency set NameCurrency=@name where IDCurrency=@id ";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0]=new SqlParameter("@name", name);
            parm[1]=new SqlParameter("@id", id);
            return sql.ExcuteQuery(Query, parm);

        }
        ///////////////////////////////////
        ////// delete Currency
        public int DeleteCurrency(int id)
        {
            
            string Query="delete from Currency where IDCurrency=@id";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0]=new SqlParameter("@id", id);
            return sql.ExcuteQuery(Query, parm);

        }


        ///////
        /// <param name="IDCurnncy"></param>
        /// <returns></returns>
        public DataTable chackCurncy(int IDcur)
        {
            string Query = "select * from RequstSupply where IDCurrency=@id";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", IDcur);
            return sql.SelectData(Query, parm);

        }
        /////////
        ///////
        /// TABLE Group
        /// add new Group
        public int AddNewGroup(int GroupSourceID ,string GroupName,string GroupDescription,int UserID,DateTime EnterTime)
        {
            string Query = "insert into tblGroup (GroupSourceID,GroupName,GroupDescription,UserID,EnterTime) values(@GroupSourceID,@GroupName,@GroupDescription,@UserID,@EnterTime)";
            SqlParameter[] parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@GroupSourceID", GroupSourceID);
            parm[1] = new SqlParameter("@GroupName", GroupName);
            parm[2] = new SqlParameter("@GroupDescription", GroupDescription);
            parm[3] = new SqlParameter("@UserID", UserID);
            parm[4] = new SqlParameter("@EnterTime", EnterTime);
            return sql.ExcuteQuery(Query, parm);

        }
        /////////////////
        //////
        // select one group
        public DataTable GetOneGroup(int IDGroup)
        {
            string Query = "SELECT   dbo.tblGroup.ID AS [رقم المجموعة], dbo.SourecGroup.Name AS [مصدر المجموعة], dbo.tblGroup.GroupName AS [اسم المجموعة], dbo.tblGroup.GroupDescription AS ملاحظات,dbo.Users.Name AS[اسم المستخدم], dbo.tblGroup.EnterTime AS[تاريخ الادخال] FROM dbo.tblGroup INNER JOIN   dbo.SourecGroup ON dbo.tblGroup.GroupSourceID = dbo.SourecGroup.ID INNER JOIN     dbo.Users ON dbo.tblGroup.UserID = dbo.Users.IDUSER where dbo.tblGroup.ID=@id";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", IDGroup);
            return sql.SelectData(Query, parm);
        }
        ///////////
        ///
        /// Get All Groups
        public DataTable GetAllGroup()
        {
            string Query = "SELECT   dbo.tblGroup.ID AS [رقم المجموعة], dbo.SourecGroup.Name AS [مصدر المجموعة], dbo.tblGroup.GroupName AS [اسم المجموعة], dbo.tblGroup.GroupDescription AS ملاحظات,dbo.Users.Name AS[اسم المستخدم], dbo.tblGroup.EnterTime AS[تاريخ الادخال] FROM dbo.tblGroup INNER JOIN   dbo.SourecGroup ON dbo.tblGroup.GroupSourceID = dbo.SourecGroup.ID INNER JOIN     dbo.Users ON dbo.tblGroup.UserID = dbo.Users.IDUSER";
            return sql.SelectData(Query, null);
        }
        /// Get  Groups by name
        public DataTable GetGroupByName( string NAME)
        {
            string Query = "SELECT   dbo.tblGroup.ID AS [رقم المجموعة], dbo.SourecGroup.Name AS [مصدر المجموعة], dbo.tblGroup.GroupName AS [اسم المجموعة], dbo.tblGroup.GroupDescription AS ملاحظات,dbo.Users.Name AS[اسم المستخدم], dbo.tblGroup.EnterTime AS[تاريخ الادخال] FROM dbo.tblGroup INNER JOIN   dbo.SourecGroup ON dbo.tblGroup.GroupSourceID = dbo.SourecGroup.ID INNER JOIN     dbo.Users ON dbo.tblGroup.UserID = dbo.Users.IDUSER where dbo.SourecGroup.Name like @NAMEGROUP";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@NAMEGROUP", NAME);

            return sql.SelectData(Query, parm);
        }
        /////delelte Group
        public DataTable DeleteGroup(int IDGROUP)
        {
            string Query = "delete  from tblGroup where ID = @IDGOUP";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@IDGOUP", IDGROUP);
            return sql.SelectData(Query, parm);
             
        }
        /////Chack Group have ITem
        public bool CheckGroupItems(int IDGROUP)
        {
            string Query = "select * from GroupDetalis where GroupDetalis.GroupID=@IDGROUP";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@IDGROUP", IDGROUP);
            DataTable dt = new DataTable();
            dt = sql.SelectData(Query, parm);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;

        }
        ///
        //Get Sourec group
        public DataTable GetSourecGroup()
        {
            string Query = "SELECT ID AS [رقم المصدر], Name AS [اسم المصدر] FROM  dbo.SourecGroup";
            return sql.SelectData(Query, null);
        }
        ////////
        // get max id Group
        public int GetMaxIDGroup()
        {
            string Query = "select Max(ID) from dbo.tblGroup";
            return (int)sql.ExcuteQueryValue(Query, null);

        }
        //////////
        //////////
        /// update group
        public int UpdateGroup(int ID, int GroupSourceID, string GroupName, string GroupDescription, int UserID)
        {
            string Query = "update dbo.tblGroup set  GroupSourceID=@GroupSourceID , GroupName=@GroupName ,GroupDescription=@GroupDescription,UserID=@UserID where ID=@ID ";
            SqlParameter[] parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@ID", ID);
            parm[1] = new SqlParameter("@GroupSourceID", GroupSourceID);
            parm[2] = new SqlParameter("@GroupName", GroupName);
            parm[3] = new SqlParameter("@GroupDescription", GroupDescription);
            parm[4] = new SqlParameter("@UserID", UserID);
            return (int)sql.ExcuteQuery(Query, parm);
        }
        ////
        public DataTable GETALLAccountSub()
        {
            string Query = "select AccountNm.IDCode as 'رقم الحساب',AccountNm.AcountNm as'اسم الحساب'  from AccountNm where AcountType='فرعي'";
            return sql.SelectData(Query, null);
        }
        //// get Accountes not in Group
        public DataTable GetAllAccountSubNoInGroup(int GroupID)
        {
            string Query = "select AccountNm.IDCode as 'رقم الحساب', AccountNm.AcountNm as'اسم الحساب'  from  AccountNm where AccountNm.AcountType='فرعي' and   AccountNm.IDCode  not in(select GroupDetalis.GroupIDItem from GroupDetalis where GroupDetalis.GroupID=@GroupID) ";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@GroupID", GroupID);
            return sql.SelectData(Query, parm);
        }
        /// get accounts in group
        public DataTable GetAllAccountSubInGroup(int GroupID)
        {
            string Query = "select AccountNm.IDCode as 'رقم الحساب', AccountNm.AcountNm as'اسم الحساب'  from  AccountNm where AccountNm.AcountType='فرعي' and   AccountNm.IDCode  in(select GroupDetalis.GroupIDItem from GroupDetalis where GroupDetalis.GroupID=@GroupID) ";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@GroupID", GroupID);
            return sql.SelectData(Query, parm);
        }
        /// delete All items From GroupDetalis
        public int DeleteItemsONGroupDetalis(int IDGroup)
        {
            string Query = "delete  from GroupDetalis where GroupDetalis.GroupID=@GroupID";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@GroupID", IDGroup);
            return sql.ExcuteQuery(Query, parm);
        }
        // add Items  from Group Detalis
        public int AddItemsONGroupDetalis(int IDGroup,int IDItems,int UserID)
        {
            string Query = " insert into GroupDetalis (GroupID,GroupIDItem,UserID) values(@GroupID,@GroupIDItem,@UserID)";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@GroupID", IDGroup);
            parm[1] = new SqlParameter("@GroupIDItem", IDItems);
            parm[2] = new SqlParameter("@UserID", UserID);
            return sql.ExcuteQuery(Query, parm);
        }
        //////////
        //////////
        ///////
        /// tbl Connection Account with 
        /// add Connection account with place
        public int AddConnectionAccountwithPlace(int palce,int idMadeen,int idDaan)
        {
            string Query = "insert into tblConnectionAccountWithPlace (IDPalce,IDAcccountDaan,IDAccountMadden)values(@IDPalce,@IDAcccountDaan,@IDAccountMadden)";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@IDPalce", palce);
            parm[1] = new SqlParameter("@IDAccountMadden", idMadeen);
            parm[2] = new SqlParameter("@IDAcccountDaan", idDaan);
            return sql.ExcuteQuery(Query,parm);
        }
        /// update  Connection account with place
        public int UpdateConnectionAccountwithPlace(int ID,int palce, int idMadeen, int idDaan)
        {
            string Query = "update tblConnectionAccountWithPlace  set IDPalce=@IDPalce ,IDAcccountDaan=@IDAcccountDaan,IDAccountMadden=@IDAccountMadden where ID=@ID ";
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@IDPalce",palce);
            parm[1] = new SqlParameter("@IDAcccountDaan",idDaan);
            parm[2] = new SqlParameter("@IDAccountMadden",idMadeen);
            parm[3] = new SqlParameter("@ID",ID);
            return sql.ExcuteQuery(Query,parm);
        }
        ///  delete  Connection account with place
        public int DeleteConnectionAccountwithPlace(int ID)
        {
            string Query = "delete from  tblConnectionAccountWithPlace where ID=@ID";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@ID", ID);
            return sql.ExcuteQuery(Query, parm);
        }
        /// get all  Connectiones account with place
        public DataTable GetConnectionAccountwithPlace()
        {
            string Query = "  select tblConnectionAccountWithPlace.ID as 'الرقم' ,PlaceSend.NamePlace as 'اسم الجهة',AccountNm.AcountNm as 'الحساب الدائن' ,AccountNm1.AcountNm as 'الحساب المدين'   from tblConnectionAccountWithPlace inner join PlaceSend on PlaceSend.IDPlace = tblConnectionAccountWithPlace.IDPalce   inner   join AccountNm on tblConnectionAccountWithPlace.IDAcccountDaan = AccountNm.IDCode inner  join AccountNm as AccountNm1 on tblConnectionAccountWithPlace.IDAccountMadden = AccountNm1.IDCode order by tblConnectionAccountWithPlace.ID";
            return sql.SelectData(Query, null);
        }
        ////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////
        // function for tblBraanch
        // AddNEWBARNCH
        public int AddNewBranch(int number,string branchName,int Idaccount,string Note,string nameEnglish,string phone,string fax,string Adderss,int Userid,int baranchId)
        {
            string Query = "INSERT INTO [dbo].[tblBranches]([TheNumber],[BranchName],[AccountID],[Notes],[EnglishName],[phone],[fax],[Address],[UserID],[BranchID])VALUES(@number,@branchName,@Idaccount,@Note,@nameEnglish,@phone,@fax,@Adderss,@Userid,@baranchId)";
            SqlParameter[] parm = new SqlParameter[10];
            parm[0] = new SqlParameter("@number", number);
            parm[1] = new SqlParameter("@branchName", branchName);
            parm[2] = new SqlParameter("@Idaccount", Idaccount);
            parm[3] = new SqlParameter("@Note", Note);
            parm[4] = new SqlParameter("@nameEnglish", nameEnglish);
            parm[5] = new SqlParameter("@phone", phone);
            parm[6] = new SqlParameter("@fax", fax);
            parm[7] = new SqlParameter("@Adderss", Adderss);
            parm[8] = new SqlParameter("@Userid", Userid);
            parm[9] = new SqlParameter("@baranchId", baranchId);
            return sql.ExcuteQuery(Query, parm);

        }
        ////////
        ///// uodate tblBarnch
        public int UpdateBranch(int number, string branchName, int Idaccount, string Note, string nameEnglish, string phone, string fax, string Adderss, int Userid, int baranchId, int ID)
        { 
            string Query = "UPDATE [dbo].[tblBranches] SET [TheNumber] = @number,[BranchName] =@branchName  ,[AccountID] =@Idaccount ,[Notes] =@Note  ,[EnglishName] =@nameEnglish ,[phone] =@phone,[fax] =@fax ,[Address] =@Adderss  ,[UserID] =@Userid ,[BranchID] = @baranchId  WHERE IDBranch=@ID";
            SqlParameter[] parm = new SqlParameter[11];
            parm[0] = new SqlParameter("@number", number);
            parm[1] = new SqlParameter("@branchName", branchName);
            parm[2] = new SqlParameter("@Idaccount", Idaccount);
            parm[3] = new SqlParameter("@Note", Note);
            parm[4] = new SqlParameter("@nameEnglish", nameEnglish);
            parm[5] = new SqlParameter("@phone", phone);
            parm[6] = new SqlParameter("@fax", fax);
            parm[7] = new SqlParameter("@Adderss", Adderss);
            parm[8] = new SqlParameter("@Userid", Userid);
            parm[9] = new SqlParameter("@baranchId", baranchId);
            parm[10] = new SqlParameter("@ID", ID);
            return sql.ExcuteQuery(Query, parm);

        }
        /////
        /// delete from tbl barnch
        public int DeletefromBarnch(int IDBARNCH)
        {
            string Query = "Delete from  [dbo].[tblBranches] where IDBranch=@IDBARNCH";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@IDBARNCH", IDBARNCH);
            return sql.ExcuteQuery(Query, parm);

        }
        ///////
        /// get Max NUmber barnch
        public int GETMaxNumberBarnch()
        {
            string Query = "select max(TheNumber) from [dbo].[tblBranches]";
            return (int)sql.ExcuteQueryValue(Query, null);

        }
        ///get max id from branch
        public int GetMaxIDBranch()
        {
            string Query = "select max(IDBranch) from [dbo].[tblBranches]";
            return (int)sql.ExcuteQueryValue(Query, null);
        }
        /// get id Barnch From Number
        public int GetIdBranchFromNumber(int Number)
        {
            string Query = "select IDBranch from  [dbo].[tblBranches] where  [TheNumber]=@Number";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@Number", Number);
            return (int)sql.ExcuteQueryValue(Query, parm);
         }
        /// get all Branches
        public DataTable GetAllBarnch()
        {
            string Query = "select Top 1000 tblBranches.TheNumber as 'الرقم' ,tblBranches.BranchName as 'اسم الفرع' ,tblBranches.EnglishName as'الاسم الاجنبي', AccountNm.AcountNm as 'الحساب الرئيسي',tblBranches.phone as 'رقم التلفون',tblBranches.fax as 'الفاكس',tblBranches.Address as 'العنوان',tblBranches.Notes as 'ملاحظات',Users.UserName as'اسم المستخدم',tblBranches.EnterTime as'وقت الادخال',tblBranches1.BranchName as 'فرع الادخال'  from tblBranches inner join AccountNm on tblBranches.AccountID=AccountNm.IDCode inner join Users on tblBranches.UserID=Users.IDUSER inner join tblBranches as tblBranches1 on  tblBranches.BranchID = tblBranches1.IDBranch";
            return sql.SelectData(Query, null);

        }
       //GET Accounts main
       public DataTable Getbranch(int IdBranch)
        {
            
            if (IdBranch == 0)
            {
                IdBranch = this.GetMaxIDBranch();
            }
            
            string Query = "select Top 1000 tblBranches.TheNumber as 'الرقم' ,tblBranches.BranchName as 'اسم الفرع' ,tblBranches.EnglishName as'الاسم الاجنبي', AccountNm.AcountNm as 'الحساب الرئيسي',tblBranches.phone as 'رقم التلفون',tblBranches.fax as 'الفاكس',tblBranches.Address as 'العنوان',tblBranches.Notes as 'ملاحظات',Users.UserName as'اسم المستخدم',tblBranches.EnterTime as'وقت الادخال',tblBranches1.BranchName as 'فرع الادخال'  from tblBranches inner join AccountNm on tblBranches.AccountID=AccountNm.IDCode inner join Users on tblBranches.UserID=Users.IDUSER inner join tblBranches as tblBranches1 on  tblBranches.BranchID = tblBranches1.IDBranch where tblBranches.IDBranch=@id";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("id", IdBranch);
            DataTable dt = new DataTable();
            dt = sql.SelectData(Query, parm);
            
            return dt;

        }
        // get all account main
        public DataTable GetAccountsMain()
        {
            string Query = "select AccountNm.AcountNm as'اسم الحساب',AccountNm.IDCode as'رقم الحساب' from AccountNm where AcountType='رئيسي'";
            return sql.SelectData(Query, null);

        }

      
       



    }
}
