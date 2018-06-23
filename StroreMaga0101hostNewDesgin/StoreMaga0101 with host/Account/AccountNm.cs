using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Users;
namespace Account
{ 
   public class AccountNm
    {
        MSqlConnection sql;

      public  AccountNm(string SerNm,string DBNm,string UserSql,string PassSql)
        {
           if (string.IsNullOrEmpty(UserSql) || string.IsNullOrEmpty(PassSql))
            {
                sql = new MSqlConnection(SerNm, DBNm);
            }
            else
            {
                sql = new MSqlConnection(SerNm, DBNm, UserSql, PassSql);
            }

            
        }
        /// <summary>
        /// 
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
        /// 
        /// 
        ///
        /// 
        /// chaeck account
    public   bool CheckAccountinDetlis(int idcode)
        { bool res;
            string Query = "select * from AccountDetalis where IDCode=@idcode";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@idcode", idcode);
            DataTable dt = new DataTable();
            dt = sql.SelectData(Query, parm);
            if(dt.Rows.Count>0)
            {
                res = true;
            }
            else
            {
                res = false;
                
            }
            return res;

        }
        public bool CheckAccounthaschalid(int idcode)
        {
            bool res;
            string Query = "select * from AccountNm where IDParentAcount =@idcode";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@idcode", idcode);
            DataTable dt = new DataTable();
            dt = sql.SelectData(Query, parm);
            if (dt.Rows.Count > 0)
            {
                res = true;
            }
            else
            {
                res = false;

            }
            return res;

        }

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
        ///       /// <summary>
        ///       chech acount ID is Here
        public bool GetCheckAccountHere(int IDAccount)
        {
            string Query = "select count(IDAcountNm) from AccountNm where IDCode=@id ";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", IDAccount);
            int i=(int)   sql.ExcuteQueryValue(Query, parm);
            if(i>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
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
        /// <returns></returns>
        /////////////
        public int DelteAccount2(int IDcode)
        {
            string Query = "delete from AccountNm where IDCode=@id";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", IDcode);
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
        public DataTable GETALLAccountPrime()
        {
            string Query = "select AccountNm.IDCode as 'رقم الحساب', AccountNm.AcountNm as'اسم الحساب'  from AccountNm where AcountType='رئيسي' and Active=1 ";
            return sql.SelectData(Query, null);
        }
        public DataTable GETALLAccountSub()
        {
            string Query = "select AccountNm.IDCode as 'رقم الحساب',AccountNm.AcountNm as'اسم الحساب'  from AccountNm where AcountType='فرعي' and Active=1";
            return sql.SelectData(Query, null);
        }
        /////////
        ////////////////////////////////
        //////// get AllCurrency
        public DataTable GetAllCurrency()
        {

            string query = "select IDCurrency as'رقم العملة',NameCurrency as 'اسم العملة' from Currency ";
            return sql.SelectData(query, null);


        }

        //////////////
        public DataTable GetBalanceAccount(int IDcode,int IDCurrncy,string NmIDcurrmcy)
        {
            int Valu;
            string Query = "select Balance from AccountTotal where IDCode=@idcode and  IDCurrncy=@idcurrncy";
            SqlParameter[] parm = new SqlParameter[2];
            DataTable dt2 = new DataTable();
            dt2 = GETNMAccount(IDcode);
            parm[0] = new SqlParameter("@idcode", IDcode);
            parm[1] = new SqlParameter("@idcurrncy", IDCurrncy);
            try
            {


                Valu = (int)sql.ExcuteQueryValue(Query, parm);
            }
            catch
            {
                Valu = 0;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("رقم  الحساب");
            dt.Columns.Add("اسم الحساب");
            dt.Columns.Add("دائن");
            dt.Columns.Add("مدين");
            dt.Columns.Add("عملة الحساب");
            dt.Columns.Add("البيان");
            string Detilas;

            if (Valu >0)
            {
                Detilas = "الرصيد لكم بقيمة "+ string.Format("{0:##,##}", Valu) + "  "+NmIDcurrmcy;
                dt.Rows.Add(new string[] {dt2.Rows[0][0].ToString(),dt2.Rows[0][1].ToString(), string.Format("{0:##,##}", Valu), "0" , NmIDcurrmcy , Detilas });

            }
            else
            {
                Valu = -1 * Valu;
                Detilas = "الرصيد عليكم  بقيمة " + string.Format("{0:##,##}", Valu) + "  "+NmIDcurrmcy;
                dt.Rows.Add(new string[] { dt2.Rows[0][0].ToString(), dt2.Rows[0][1].ToString(),"0", string.Format("{0:##,##}", Valu), NmIDcurrmcy, Detilas });
            }
            return dt;
        }

        //////////////
        /// <summary>
        ///  جلب جميع الاحسابات لعميل واحد بجمع العملات
        ///  
        /// </summary>
        /// <param name="IDcode"></param>
        /// <returns></returns>
        //////////////
        public DataTable GetBalanceAccountALLCunncy(int IDcode)
        {
            DataTable dt123 = new DataTable();
            DataTable dt = new DataTable();
            string Query = "select Balance,IDCurrncy from AccountTotal where IDCode=@idCode ";

            DataTable dt2 = new DataTable();
            dt2 = GETNMAccount(IDcode);

            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@idCode", IDcode);
            dt123 = sql.SelectData(Query, parm);
            dt.Columns.Add("رقم الحساب");
            dt.Columns.Add("اسم الحساب");
            dt.Columns.Add("دائن");
            dt.Columns.Add("مدين");
            dt.Columns.Add("عملة الحساب");
            dt.Columns.Add("البيان");
            string Detilas;
            for (int i=0;i<dt123.Rows.Count;i++)
            {
                int Valu = Convert.ToInt32( dt123.Rows[i][0].ToString());
                string NmIDcurrmcy = GETNMCurrncy(Convert.ToInt32(dt123.Rows[i][1].ToString()));

                if (Valu > 0)
                {
                    Detilas = "الرصيد لكم بقيمة " + string.Format("{0:##,##}", Valu) + "  " + NmIDcurrmcy;
                    dt.Rows.Add(new string[] { dt2.Rows[0][0].ToString(),dt2.Rows[0][1].ToString(), string.Format("{0:##,##}", Valu), "0", NmIDcurrmcy, Detilas });

                }
                else
                {
                    Valu = -1 * Valu;
                    Detilas = "الرصيد عليكم  بقيمة " + string.Format("{0:##,##}", Valu) + "  " + NmIDcurrmcy;
                    dt.Rows.Add(new string[] { dt2.Rows[0][0].ToString(), dt2.Rows[0][1].ToString(), "0", string.Format("{0:##,##}", Valu), NmIDcurrmcy, Detilas });
                }
            }
            return dt;

        }
        /// <summary>
        ///  جلب اسم العملة من رقمها
        /// </summary>
        /// <param name="IDCur"></param>
        /// <returns></returns>
        public string GETNMCurrncy(int IDCur)

        {
            string Query = "select NameCurrency from Currency where IDCurrency=@idCur";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@idCur", IDCur);
            return (string)sql.ExcuteQueryValue(Query, parm);


        }

        ////////////////
        /////////////
       // جلب جميع الحسابات الفرعية الاجمالية بعملة وواحدة او كل العملات
        //////////////
        public DataTable GetBalanceALLAccountALLCunncy(int idcurrncy)
        {
            DataTable dt123 = new DataTable();
            DataTable dt = new DataTable();
            string Query;
            if (idcurrncy > 0)
            {
               Query = "select Balance,IDCurrncy,IDCode from AccountTotal where IDCurrncy=@idcurrncy   ";
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@idcurrncy", idcurrncy);
                dt123 = sql.SelectData(Query, parm);
            }
            else

            {
                Query = "select Balance, IDCurrncy,IDCode from AccountTotal order by IDCode ";
                dt123 = sql.SelectData(Query, null);
            }
            dt.Columns.Add("رقم الحساب");
            dt.Columns.Add("اسم الحساب");
            dt.Columns.Add("دائن");
            dt.Columns.Add("مدين");
            dt.Columns.Add("عملة الحساب");
            dt.Columns.Add("البيان");
            string Detilas;
          
            for (int i = 0; i < dt123.Rows.Count; i++)
            {
                int Valu = Convert.ToInt32(dt123.Rows[i][0].ToString());
                string NmIDcurrmcy = GETNMCurrncy(Convert.ToInt32(dt123.Rows[i][1].ToString()));
                DataTable dtAc = new DataTable();
                dtAc = GETNMAccount(Convert.ToInt32(dt123.Rows[i][2].ToString()));
                try
                {
                    string IDCodeAC = dtAc.Rows[0]["IDCode"].ToString();
                    string NMACOUNT = dtAc.Rows[0]["AcountNm"].ToString();
                    if (Valu > 0)
                    {
                        Detilas = "الرصيد لكم بقيمة " + string.Format("{0:##,##}", Valu) + "  " + NmIDcurrmcy;
                        dt.Rows.Add(new string[] { IDCodeAC, NMACOUNT, string.Format("{0:##,##}", Valu), "0", NmIDcurrmcy, Detilas });
                    }
                    else
                    {
                        Valu = -1 * Valu;
                        Detilas = "الرصيد عليكم  بقيمة " + string.Format("{0:##,##}", Valu) + "  " + NmIDcurrmcy;
                        dt.Rows.Add(new string[] { IDCodeAC, NMACOUNT, "0", string.Format("{0:##,##}", Valu), NmIDcurrmcy, Detilas });
                    }
                }
                catch
                { }
                
            }
            return dt;

        }
        ////جلب اسم رقم واسمه ومن رقم الحساب
        public DataTable GETNMAccount(int IDCOde)
        {
            string Query = "select AccountNm.IDCode,AccountNm.AcountNm  from AccountNm where IDCode=@idcode";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@idcode", IDCOde);
            return sql.SelectData(Query, parm);

        }

        ////////////////
        ////////////
        /////////// جلب كشف حساب لللحساب فرعي تفصيليا بعملة واحده او عدة عملات
        public DataTable GETAccountDitalis(int IDcode, int IDCurnncy, DateTime d1, DateTime d2)
        {
            DataTable DtResult = new DataTable();
            DtResult.Columns.Add("دائن");
            DtResult.Columns.Add("مدين");
            DtResult.Columns.Add("عملة العملية");
            DtResult.Columns.Add("العملية");
            DtResult.Columns.Add("تاريخ العملية");
            DtResult.Columns.Add("البيان");
            DtResult.Columns.Add("اسم الموظف");
            /////// اضافة الرصيد السابق قبل تاريخ البحث
            int OldMony = getOldMony(IDcode, IDCurnncy, d1);
            string nmCurrncy = GETNMCurrncy(IDCurnncy);
            int sumtotal = 0;
            if (OldMony>0)
            {
                sumtotal += OldMony;
                DtResult.Rows.Add(new string[] { string.Format("{0:##,##}", OldMony), "0", nmCurrncy, null, "قبل تاريخ " + d1.ToString(), "رصيد سابق", null });
            }
            else if(OldMony<0)
            {
                sumtotal += OldMony;
                DtResult.Rows.Add(new string[] { "0", string.Format("{0:##,##}", OldMony), nmCurrncy, null, "قبل تاريخ " + d1.ToString(), "رصيد سابق", null });

            }
           
            /////////////////
            ///////////////
           
            DataTable dtre1 = new DataTable();
            dtre1 = GETAcountDitlis(IDcode, IDCurnncy, d1, d2);
            for(int i=0;i<dtre1.Rows.Count; i++)
            { int vale = Convert.ToInt32(dtre1.Rows[i][2].ToString());
              int cha1 = Convert.ToInt32(dtre1.Rows[i][3].ToString());
                int cha2 = Convert.ToInt32(dtre1.Rows[i][4].ToString());
                int cha3;
                try
                {
                  cha3  = Convert.ToInt32(dtre1.Rows[i][9].ToString());
                }
                catch
                {
                    cha3 = 0;
                }
               
                string LIKEType="";
                if (cha1>0)
                {
                    LIKEType = "امر توريد";
                }
                else if(cha2>0)
                {
                    LIKEType = "امر صرف";
                }
              else if(cha3>0)
                {
                  LIKEType = "قيد بسيط";
                }
                int userii = Convert.ToInt32(dtre1.Rows[i][7].ToString());
                if (vale > 0)
                {
                    sumtotal += vale;
                    DtResult.Rows.Add(new string[] { string.Format("{0:##,##}", Convert.ToInt32(dtre1.Rows[i][2].ToString())), "0", nmCurrncy, LIKEType, dtre1.Rows[i][6].ToString(), dtre1.Rows[i][5].ToString(), GetUserNM(userii)});
                }
               else
                {
                    sumtotal += vale;
                    DtResult.Rows.Add(new string[] { "0", string.Format("{0:##,##}",(-1* Convert.ToInt32(dtre1.Rows[i][2].ToString()))), nmCurrncy, LIKEType, dtre1.Rows[i][6].ToString(), dtre1.Rows[i][5].ToString(), GetUserNM(userii) });
                }
            }
            //   DataTable DTAll = new DataTable();
            // DTAll = GetBalanceAccount(IDcode, IDCurnncy, nmCurrncy);
            if (sumtotal > 0) // في حالة الدائن
            {
          
                string deit = "الرصيد لكم بقيمة " + string.Format("{0:##,##}", sumtotal) + " " + nmCurrncy;
                DtResult.Rows.Add(new string[] { string.Format("{0:##,##}", sumtotal  ), "0", nmCurrncy, "الاجمالي", DateTime.Now.ToString(), deit, null });
            }
            else if(sumtotal<0) // في حالة المدين
            {
                
                int total = -1 * sumtotal;
                string deit = "الرصيد عليكم بقيمة " + string.Format("{0:##,##}",total) + " " + nmCurrncy;

                DtResult.Rows.Add(new string[] {"0" ,string.Format("{0:##,##}", total), nmCurrncy, "الاجمالي", DateTime.Now.ToString(), deit, null });
            }
          
            return DtResult;

        }
        ///
        /// get oldMonay 
        /// 
     public   int getOldMony(int IDcode, int IDCurnncy, DateTime d2)
        {
           
            DateTime dd = Convert.ToDateTime("2017/01/01");
           
            string Query = "select SUM(Mony) from AccountDetalis where IDCode=@idcode and IDCurrncy=@idcurrncy and DateEnter between @dd and @d2";
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@idcode", IDcode);
            parm[1] = new SqlParameter("@idcurrncy", IDCurnncy);
            parm[2] = new SqlParameter("@d2", d2);
            parm[3] = new SqlParameter("@dd", dd);
            int reslt = 0;
            try
            {
                reslt =Convert.ToInt32(sql.ExcuteQueryValue(Query, parm));
               
            }
            catch
            {
                reslt = 0;
              //  MessageBox.Show(ex.Message);
            }
            return reslt;

        }
       public DataTable GETAcountDitlis(int IDcode, int IDCurnncy, DateTime d1, DateTime d2)
        {
            string Query = "select * from AccountDetalis  where IDCode=@idcode and  IDCurrncy=@idcurrncy and DateEnter between @d1  and @d2";
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@idcode", IDcode);
            parm[1] = new SqlParameter("@idcurrncy", IDCurnncy);
            parm[2] = new SqlParameter("@d1", d1);
            parm[3] = new SqlParameter("@d2", d2);
            return sql.SelectData(Query, parm);

        }
      public string GetUserNM(int IDuser)
        {
            string Query = "select Name from Users where IDUSER =@iduser";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@iduser", IDuser);
            return (string)sql.ExcuteQueryValue(Query, parm);
        }
        /// 
      ////// Group Tables
      ////
      /// get All Groups Accouunts
      public DataTable GetGroupsAsAccounts()
        {
            string Query = "select tblGroup.ID as'رقم المجموعة' ,tblGroup.GroupName as'اسم المجموعة' from tblGroup where tblGroup.GroupSourceID=1";
            return sql.SelectData(Query, null);        
        }
        // جلب قيمة الحسابات المحددة داخل المجموعة
        public DataTable GetAccountesMOnayInGroup(int IDGroup)
        {
            string Query = " select AccountNm.IDCode,AccountNm.AcountNm,AccountTotal.Balance,Currency.NameCurrency  from AccountNm,AccountTotal,Currency where AccountTotal.IDCode=AccountNm.IDCode and   AccountNm.IDCode in(select GroupDetalis.GroupIDItem from GroupDetalis where GroupDetalis.GroupID =@GroupID) and Currency.IDCurrency = AccountTotal.IDCurrncy";

            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@GroupID", IDGroup);
            DataTable dt = new DataTable();
            dt=sql.SelectData(Query, parm);
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("رقم الحساب");
            dt2.Columns.Add("اسم الحساب");
            dt2.Columns.Add("دائن");
            dt2.Columns.Add("مدين");
            dt2.Columns.Add("عملة الحساب");
            dt2.Columns.Add("البيان");
            int totalMany = 0;
            int Mony = 0;
            for (int i=0; i<dt.Rows.Count;i++)
            {
                string deit;
              
                Mony = Convert.ToInt32(dt.Rows[i]["Balance"].ToString());
               
                if (Mony>0)
                {
                    totalMany += Mony;
                   deit = "الرصيد لكم بقيمة " + string.Format("{0:##,##}",Mony )  + " " + dt.Rows[i]["NameCurrency"].ToString();
                    dt2.Rows.Add(new string[] { dt.Rows[i]["IDCode"].ToString(), dt.Rows[i]["AcountNm"].ToString(), string.Format("{0:##,##}", Mony), "0", dt.Rows[i]["NameCurrency"].ToString(),deit });
                }
               else
                {
                   
                    totalMany += Mony;
                    Mony *= -1;
                     deit = "الرصيد عليكم بقيمة " + string.Format("{0:##,##}", Mony)  + " " + dt.Rows[i]["NameCurrency"].ToString();
                    dt2.Rows.Add(new string[] { dt.Rows[i]["IDCode"].ToString(), dt.Rows[i]["AcountNm"].ToString(),"0", string.Format("{0:##,##}", Mony), dt.Rows[i]["NameCurrency"].ToString(), deit });
                }

           
                
            }

            //if (totalMany > 0)
            //{
            //    MessageBox.Show("this yes");
            //    dt2.Rows.Add(new string[] { " ", "الاجمالي", string.Format("{0:##,##}", totalMany), "0 ", dt.Rows[0]["NameCurrency"].ToString(), " " });

            //}
            //else
            //{
            //    MessageBox.Show("this no");
            //    dt2.Rows.Add(new string[] { " ", "الاجمالي", "0 ", string.Format("{0:##,##}", totalMany), dt.Rows[0]["NameCurrency"].ToString(), " " });
            //}
            return dt2;

        }
        //////////
        // Chack AcoountTotal is Here
        public bool CheckAccontTotal(int IDcode, int IDCurrncy)
        {
            bool x;
            try
            {
                string Query = "select AccountTotal.IDCode from AccountTotal where AccountTotal.IDCode=@idc and AccountTotal.IDCurrncy=@idcu ";
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
        ///Get  Balance for AccountTotal
        public int GetBalance(int Idcode, int IDCur)
        {
            string Query = "select AccountTotal.Balance from AccountTotal where AccountTotal.IDCode=@idco and AccountTotal.IDCurrncy=@idCu";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@idco", Idcode);
            parm[1] = new SqlParameter("@idCu", IDCur);
            return (int)sql.ExcuteQueryValue(Query, parm);
        }
        // /// update Account Total
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
        /////
        //////////
        //// Add new AccountDetalis 
        public int AddNewAccountDetalis(int idcode, int monay, int idsupply, int idout, string Detalis, DateTime d1, int userid, int idCurrnt, int IDSimple)
        {
            string Query = "insert into AccountDetalis(IDCode,Mony,IDSupply,IDOut,Detalis,DateEnter,UserID,IDCurrncy,IDSimpleConstraint) values(@IDCode,@Mony,@IDSupply,@IDOut,@Detalis,@DateEnter,@UserID,@IDCurrncy,@IDSimpleConstraint)";
            SqlParameter[] parm = new SqlParameter[9];
            parm[0] = new SqlParameter("@IDCode", idcode);
            parm[1] = new SqlParameter("@Mony", monay);
            parm[2] = new SqlParameter("@IDSupply", idsupply);
            parm[3] = new SqlParameter("@IDOut", idout);
            parm[4] = new SqlParameter("@Detalis", Detalis);
            parm[5] = new SqlParameter("@DateEnter", d1);
            parm[6] = new SqlParameter("@UserID", userid);
            parm[7] = new SqlParameter("@IDCurrncy", idCurrnt);
            parm[8] = new SqlParameter("IDSimpleConstraint", IDSimple);

            return sql.ExcuteQuery(Query, parm);
        }
        //////////////////////
        ////////////
        //
        ///SimpleConstraint
        //////
        public int AddSimpleConstraint(int IDdaan,int IdMAden,int Mony,int idCurnncy,int UserId,DateTime datetime,string Note)
        {
            string Query = "insert into tblSimpleConstraint (IDDaanAccont,IDMaddenAccount,Mony,IDCurnncy,IDUser,EnterTime,Note)values(@IDDaanAccont,@IDMaddenAccount,@Mony,@IDCurnncy,@IDUser,@EnterTime,@Note)";
            SqlParameter[] parm = new SqlParameter[7];
            parm[0] = new SqlParameter("@IDDaanAccont",IDdaan);
            parm[1] = new SqlParameter("@IDMaddenAccount", IdMAden);
            parm[2] = new SqlParameter("@Mony", Mony);
            parm[3] = new SqlParameter("@IDUser", UserId);
            parm[4] = new SqlParameter("@EnterTime", datetime);
            parm[5] = new SqlParameter("@Note", Note);
            parm[6] = new SqlParameter("@IDCurnncy", idCurnncy);
            return sql.ExcuteQuery(Query, parm);
        }
        ///
        /// get max IDSimpleConstraint
        public int GetMaxIDSimpleConstraint()
        {
            string Query = "select max(IDSimpleConstraint) from  tblSimpleConstraint";
            return (int)sql.ExcuteQueryValue(Query, null);

        }
        /////
        // get all simple Constraint for one day
        public DataTable GetAllSimpleConstraintOneDay(DateTime day1,DateTime day2)
        {
            string Query = "select DISTINCT tblSimpleConstraint.IDSimpleConstraint as 'رقم القيد', AccountNm1.AcountNm as 'الحساب المدين',AccountNm.AcountNm as 'الحساب الدائن'  ,tblSimpleConstraint.Mony as 'المبلغ',Currency.NameCurrency as 'العملة' ,tblSimpleConstraint.Note as'ملاحظات' ,tblSimpleConstraint.EnterTime as 'تاريخ القيد',Users.UserName as'الموظف' from  tblSimpleConstraint  inner join AccountNm as AccountNm1 on AccountNm1.IDCode=tblSimpleConstraint.IDMaddenAccount  inner join AccountNm on AccountNm.IDCode=tblSimpleConstraint.IDDaanAccont  left join Users on Users.IDUSER=tblSimpleConstraint.IDUser  left Join Currency on Currency.IDCurrency = tblSimpleConstraint.IDCurnncy  where tblSimpleConstraint.EnterTime between @entertime1 and @entertime2 ";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@entertime1", day1);
            parm[1] = new SqlParameter("@entertime2", day2);
            return sql.SelectData(Query, parm);

        } 
       

    }
}
