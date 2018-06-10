using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrmRports;
using System.ServiceModel;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Data;

namespace Supplly
{

    
 public   class ADDSup
    {
        Supplly.SupplyRequset SuRe;
        int UserID;
        bool HostConnction;
        string IPHost;
        ServiceReference1.IserviceClient SureHost;
        public ADDSup(string ServerNm, string DBNm, string UserSql, string PassSql, int UserId, bool hostconectopn, string iphost)
        {
            HostConnction = hostconectopn;
            IPHost = iphost;
             try
            {
                UserID = UserId;
                if (HostConnction == false)
                {
                    SuRe = new SupplyRequset(ServerNm, DBNm, UserSql, PassSql);
                }
                else
                {
                    SureHost = new ServiceReference1.IserviceClient();
                    EndpointAddress endp = new EndpointAddress(iphost);
                    SureHost.Endpoint.Address = endp;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //GetAllTypeQuntity
        public DataTable GetAllTypeQuntity()
        { 
            if(HostConnction==false)
            {
                return SuRe.GetAllTypeQuntity(); ;
            }
            else
            {
               return  ConvertMemorytoDB(SureHost.GetAllTypeQuntity());
            }

        }
        //////GetAllCurrency
        public DataTable GetAllCurrency()
        {
            if (HostConnction==false)
            {
                return SuRe.GetAllCurrency();
            }
            else
            {
             return    ConvertMemorytoDB(SureHost.GetAllCurrencyInSupply());
            }

        }
        ///GetAllCategoryAR
        public DataTable GetAllCategoryAR()
        {
            if (HostConnction == false)
            {
                return SuRe.GetAllCategoryAR();
            }
            else
            {
                return ConvertMemorytoDB(SureHost.GetAllCategoryAR());
            }

        }
        /// SearchINRequsetSupplyDate(DateTime.Now.Date, DateTime.Now)
        public DataTable SearchINRequsetSupplyDate(DateTime d1,DateTime d2)
        {
            if(HostConnction ==false)
            {
                return SuRe.SearchINRequsetSupplyDate(d1, d2);
            }
            else
            {
                return ConvertMemorytoDB(SureHost.SearchINRequsetSupplyDate(d1, d2));
            }
        }
        ///  GetALLAcountNm()
        public DataTable GetALLAcountNm()
        {
            if (HostConnction == false)
            {
                return SuRe.GetALLAcountNm();
            }
            else
            {
                return ConvertMemorytoDB(SureHost.GetALLAcountNmInSupply());
            }

        }
        /// CheckAccountIsHere
        public int CheckAccountIsHere(int IDCategory, int IDType, int price, int idcurrnt)
        {
            if(HostConnction==false)
            {
                return SuRe.CheckAccountIsHere(IDCategory, IDType, price, idcurrnt);
            }
            else
            {
              return   SureHost.CheckAccountIsHereInSuplly(IDCategory, IDType, price, idcurrnt);
            }
        }
        /// GetQuntityInAccount
        public int GetQuntityInAccount(int IDAcount)
        {
            if(HostConnction==false)
            {
                return SuRe.GetQuntityInAccount(IDAcount);
            }
            else
            {
                return SureHost.GetQuntityInAccountInSupply(IDAcount);
            }
        }
        ///UpdateQuntityAccount
        public int UpdateQuntityAccount(int IDAccount, int newquntity)
        {
            if(HostConnction==false)
            {
                return SuRe.UpdateQuntityAccount( IDAccount,  newquntity);
            }
            else
            {
                return SureHost.UpdateQuntityAccountInSuplly(IDAccount, newquntity);
            }
        }
        // AddNewAccount
        public int AddNewAccount(int idcate, int idtype, int qunt, int price, int idCurrnt)
        {
            if (HostConnction == false)
            {
                return SuRe.AddNewAccount(idcate, idtype, qunt, price, idCurrnt);
            }
            else
            {
                return SureHost.AddNewAccount(idcate, idtype, qunt, price, idCurrnt);
            }

        }
        // GetMaxCheckSupply()
        public int GetMaxCheckSupply()
        {
            if(HostConnction==false)
            {
                return SuRe.GetMaxCheckSupply();
            }
            else
            {
                return SureHost.GetMaxCheckSupply();
            }
        }
        //AddNewRequsetSupply
        public int AddNewRequsetSupply(int  idcate,int  idtype,int  qunt,int  price,int  idCurrnt,string  name,string  dec, DateTime d,int  UserID,int  check,int  mins,int  plus)
        {
            if(HostConnction==false)
            {
                return SuRe.AddNewRequsetSupply(idcate, idtype, qunt, price, idCurrnt, name, dec, DateTime.Now, UserID, check, mins, plus);
            }
            else
            {
                return SureHost.AddNewRequsetSupply(idcate, idtype, qunt, price, idCurrnt, name, dec, DateTime.Now, UserID, check, mins, plus);
            }
        }
        //CheckAccontTotal
        public bool CheckAccontTotal( int mins,int  idCurrnt)
        {
            if(HostConnction==false)
            {
                return SuRe.CheckAccontTotal(mins, idCurrnt);
            }
            else
            {
                return SureHost.CheckAccontTotalInSuuly(mins, idCurrnt);
            }
        }
        ///  UpdateAccountTotal
        public int UpdateAccountTotal(int IDCOde, int Mony, int idCurrncy)
        {
            if(HostConnction==false)
            {
                return SuRe.UpdateAccountTotal(IDCOde, Mony, idCurrncy);
            }
            else
            {
                return SureHost.UpdateAccountTotalInSupply(IDCOde, Mony, idCurrncy);
            }
        }
        //AddNewAccountTotal
        public int AddNewAccountTotal(int IDCOde, int Mony, int idCurrncy)
        {
            if (HostConnction == false)
            {
                return SuRe.AddNewAccountTotal( IDCOde,  Mony, idCurrncy);
            }
            else
            {
                return SureHost.AddNewAccountTotalInSuuply(IDCOde, Mony, idCurrncy);
            }
        }
        //GetMaxIdSupply
        public int GetMaxIdSupply()
        {
            if(HostConnction==false)
            {
                return SuRe.GetMaxIdSupply();
            }
            else
            {
                return SureHost.GetMaxIdSupply();
            }
        }
        //printrequstOutExit
        public DataTable printrequstOutExit1(int IDreqSup, int UserId, int user)
        {
            if(HostConnction==false)
            {
                return SuRe.printrequstOutExit1(IDreqSup, UserId, user);
            }
            else
            {
                return ConvertMemorytoDB(SureHost.printrequstOutExit1(IDreqSup, UserId, user));
            }

        }
        //PrintRequstSupply
        public DataTable PrintRequstSupply(int IDreqSup, int UserId, int user)
        {
            if(HostConnction==false)
            {
                return SuRe.PrintRequstSupply(IDreqSup,  UserId,  user);
            }
            else
            {
                return ConvertMemorytoDB(SureHost.PrintRequstSupply(IDreqSup, UserId, user));
            }
        }
        //AddNewAccountDetalis
        public int AddNewAccountDetalis(int idcode, int monay, int idsupply, int idout, string Detalis, DateTime d1, int userid, int idCurrnt, int IDSimple)
        {
            if(HostConnction==false)
            {
                return SuRe.AddNewAccountDetalis(idcode, monay, idsupply, idout, Detalis, d1, userid, idCurrnt, IDSimple);
            }
            else
            {
                return SureHost.AddNewAccountDetalisINSupply(idcode, monay, idsupply, idout, Detalis, d1, userid, idCurrnt, IDSimple);
            }
        }
        //Get Account Link Cate
        public int GetAccountLinkCate(int IDcate)
        {
            if(HostConnction==false)
            {
                return SuRe.GetAccountLinkCate(IDcate);
            }
            else
            {
                return SureHost.GetAccountLinkCateInSupply(IDcate);
            }
        }


























        //convert MemmoryToDB
        DataTable ConvertMemorytoDB(MemoryStream ms)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            ms.Seek(0, SeekOrigin.Begin);
            DataTable dt = (DataTable)formatter.Deserialize(ms);
            return dt;
        }


    }
}
