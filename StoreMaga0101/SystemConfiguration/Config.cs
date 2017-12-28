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
            string Query = "SELECT [IDType] as 'رقم النوع' ,[NameType] as 'اسم النوع' FROM [TypeQuntity] order by IDType disc";
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
        /////////



    }
}
