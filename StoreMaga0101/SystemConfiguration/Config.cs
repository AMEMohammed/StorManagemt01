using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace SystemConfiguration
{
    class Config
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
            string Query= "SELECT [IDCategory] as 'رقم الصنف',[NameCategory] as 'اسم الصنف' FROM  [Category] order by IDCategory desc ";
            return sql.SelectData(Query, null);

        }
        // </summary>
        /// <param name="NameCategory"></param> this name a category
        /// <returns></returns>
        public int AddNewCategory(string NameCategory)
        {
            string Query = "INSERT INTO [Category]([NameCategory])VALUES(@name)";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@name", NameCategory);
            return sql.ExcuteQuery(Query, parm);
        }
      // Update Category
        public int UpdateCategory(int id, string name)
        {
            
            string Query="UPDATE [Category] SET [NameCategory]=@name1  WHERE  IDCategory=@id1";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0]=new SqlParameter("@name1", name);
            parm[1]=new SqlParameter("@id1", id);
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
            string Query = "select AccountNm.IDCode as 'رقم الحساب', AccountNm.AcountNm as'اسم الحساب'  from  AccountNm where AccountNm.AcountType='فرعي' and   AccountNm.IDAcountNm   not in(select GroupDetalis.GroupIDItem from GroupDetalis where GroupDetalis.GroupID=@GroupID) ";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@GroupID", GroupID);
            return sql.SelectData(Query, parm);
        }
        /// get accounts in group
        public DataTable GetAllAccountSubInGroup(int GroupID)
        {
            string Query = "select AccountNm.IDCode as 'رقم الحساب', AccountNm.AcountNm as'اسم الحساب'  from  AccountNm where AccountNm.AcountType='فرعي' and   AccountNm.IDAcountNm   in(select GroupDetalis.GroupIDItem from GroupDetalis where GroupDetalis.GroupID=@GroupID) ";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@GroupID", GroupID);
            return sql.SelectData(Query, parm);
        }

    }
}
