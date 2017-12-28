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
            if(UserSql ==null || PassSql==null)
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



    }
}
